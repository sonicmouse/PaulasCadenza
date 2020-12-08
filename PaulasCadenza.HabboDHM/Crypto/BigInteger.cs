using PaulasCadenza.HabboDHM.ASUtilities;
using PaulasCadenza.HabboDHM.Crypto.Reduction;
using PaulasCadenza.Utilities;
using System;

namespace PaulasCadenza.HabboDHM.Crypto
{
	internal sealed class BigInteger : IEquatable<BigInteger>, IDisposable
	{
		public const int DB = 30; // number of significant bits per chunk
		public const int DV = 1 << DB;
		public const int DM = DV - 1; // Max value in a chunk

		private const int BI_FP = 52;
		private const int F1 = BI_FP - DB;
		private const int F2 = 2 * DB - BI_FP;
		private static readonly double FV = Math.Pow(2, BI_FP);

		public static readonly BigInteger ONE = Nbv(1);
		public static readonly BigInteger ZERO = Nbv(0);
		
		public int ChunkCount { get; set; }
		public int Sign { get; set; }
		public ASArray<long> Data { get; set; } = new ASArray<long>();

		/// <summary>
		/// Instantiates a <see cref="BigInteger"/> object
		/// </summary>
		/// <param name="value">Either a hex <see cref="string"/> or an <see cref="ASByteArray"/></param>
		/// <param name="radix">The base</param>
		/// <param name="unsigned">Is the value signed or unsigned</param>
		public BigInteger(object value = null, int radix = 0, bool unsigned = false)
		{
			if (value is string)
			{
				if ((radix != 0) && (radix != 16))
				{
					FromRadix(value as string, radix);
				}
				else
				{
					value = ASByteArray.HexStringToByteArray(value as string);
					radix = 0;
				}
			}
			if (value is ASByteArray)
			{
				var array = value as ASByteArray;
				var length = radix != 0 ? radix : (array.Length - array.Position);
				FromArray(array, length, unsigned);
			}
		}

		public static BigInteger Nbv(int value)
		{
			var bn = new BigInteger();
			bn.FromInt(value);
			return bn;
		}

		private void FromInt(int value)
		{
			ChunkCount = 1;
			Sign = (value < 0) ? -1 : 0;
			if (value > 0)
			{
				Data[0] = value;
			}
			else if (value < -1)
			{
				Data[0] = value + DV;
			}
			else
			{
				ChunkCount = 0;
			}
		}

		private void FromArray(ASByteArray value, int length, bool unsigned = false)
		{
			var p = value.Position;
			var i = p + length;
			var sh = 0;
			const int k = 8;
			ChunkCount = 0;
			Sign = 0;
			while (--i >= p)
			{
				var x = i < value.Length ? value[i] : 0;
				if (sh == 0)
				{
					Data[ChunkCount++] = x;
				}
				else if (sh + k > DB)
				{
					Data[ChunkCount - 1] |= (long)((x & ((1 << (DB - sh)) - 1)) << sh);
					Data[ChunkCount++] = x >> (DB - sh);
				}
				else
				{
					Data[ChunkCount - 1] |= (long)(x << sh);
				}
				sh += k;
				if (sh >= DB) sh -= DB;
			}
			if (!unsigned && (value[0] & 0x80) == 0x80)
			{
				Sign = -1;
				if (sh > 0)
				{
					Data[ChunkCount - 1] |= (long)(((1 << (DB - sh)) - 1) << sh);
				}
			}
			Clamp();
			value.Position = Math.Min(p + length, value.Length);
		}

		public BigInteger Divide(BigInteger a)
		{
			var r = new BigInteger();
			DivRemTo(a, r, null);
			return r;
		}

		public void MultiplyUpperTo(BigInteger a, int n, BigInteger r)
		{
			--n;
			var i = r.ChunkCount = ChunkCount + a.ChunkCount - n;
			r.Sign = 0; // assumes a,this >= 0
			while (--i >= 0)
			{
				r.Data[i] = 0;
			}
			for (i = Math.Max(n - ChunkCount, 0); i < a.ChunkCount; ++i)
			{
				r.Data[ChunkCount + i - n] = Am(n - i, (int)a.Data[i], r, 0, 0, ChunkCount + i - n);
			}
			r.Clamp();
			r.DRShiftTo(1, r);
		}

		public void MultiplyLowerTo(BigInteger a, int n, BigInteger r)
		{
			var i = Math.Min(ChunkCount + a.ChunkCount, n);
			r.Sign = 0; // assumes a, this >= 0
			r.ChunkCount = i;
			while (i > 0)
			{
				r.Data[--i] = 0;
			}
			var j = 0;
			for (j = r.ChunkCount - ChunkCount; i < j; ++i)
			{
				r.Data[i + ChunkCount] = Am(0, (int)a.Data[i], r, i, 0, ChunkCount);
			}
			for (j = Math.Min(a.ChunkCount, n); i < j; ++i)
			{
				Am(0, (int)a.Data[i], r, i, 0, n - i);
			}
			r.Clamp();
		}

		public void DAddOffset(int n, int w)
		{
			while (ChunkCount <= w)
			{
				Data[ChunkCount++] = 0;
			}
			Data[w] += n; // this causes a wrap.
			while (Data[w] >= DV)
			{
				Data[w] -= DV;
				if (++w >= ChunkCount)
				{
					Data[ChunkCount++] = 0;
				}
				++Data[w];
			}
		}

		public void Clamp()
		{
			var c = Sign & DM;
			while (ChunkCount > 0 && Data[ChunkCount - 1] == c)
			{
				--ChunkCount;
			}
		}

		private static double ChunkSize(double r) =>
			Math.Floor(Math.Log(2) * DB / Math.Log(r));

		private static int IntAt(string str, int index) =>
			(int)Base36.Decode(str[index].ToString());

		public int CompareTo(BigInteger v)
		{
			var r = Sign - v.Sign;
			if (r != 0)
			{
				return r;
			}
			var i = ChunkCount;
			r = i - v.ChunkCount;
			if (r != 0)
			{
				return r;
			}
			while (--i >= 0)
			{
				r = (int)(Data[i] - v.Data[i]);
				if (r != 0)
				{
					return r;
				}
			}
			return 0;
		}

		public BigInteger Mod(BigInteger v)
		{
			var r = new BigInteger();
			Abs().DivRemTo(v, null, r);
			if (Sign < 0 && r.CompareTo(ZERO) > 0)
			{
				v.SubTo(r, r);
			}
			return r;
		}

		public BigInteger Abs() =>
			(Sign < 0) ? Negate() : this;

		public void DivRemTo(BigInteger m, BigInteger q = null, BigInteger r = null)
		{
			var pm = m.Abs();
			if (pm.ChunkCount <= 0) { return; }
			var pt = Abs();
			if (pt.ChunkCount < pm.ChunkCount)
			{
				if (q != null) { q.FromInt(0); }
				if (r != null) { CopyTo(r); }
				return;
			}
			if (r == null) { r = new BigInteger(); }
			var y = new BigInteger();
			var ts = Sign;
			var ms = m.Sign;
			var nsh = DB - NBits(pm.Data[pm.ChunkCount - 1]); // normalize modulus
			if (nsh > 0)
			{
				pm.LShiftTo((int)nsh, y);
				pt.LShiftTo((int)nsh, r);
			}
			else
			{
				pm.CopyTo(y);
				pt.CopyTo(r);
			}
			var ys = y.ChunkCount;
			var y0 = y.Data[ys - 1];
			if (y0 == 0) return;
			double yt = y0 * (1 << F1) + ((ys > 1) ? y.Data[ys - 2] >> F2 : 0);
			double d1 = FV / yt;
			double d2 = (1 << F1) / yt;
			double e = 1 << F2;
			var i = r.ChunkCount;
			var j = i - ys;
			var t = q ?? new BigInteger();
			y.DLShiftTo(j, t);
			if (r.CompareTo(t) >= 0)
			{
				r.Data[r.ChunkCount++] = 1;
				r.SubTo(t, r);
			}
			ONE.DLShiftTo(ys, t);
			t.SubTo(y, y); // "negative" y so we can replace sub with am later.
			while (y.ChunkCount < ys)
			{
				//y.(y.t++, 0);
				System.Diagnostics.Debug.Assert(false, "I DON'T KNOW HOW TO HANDLE THIS IN C#: y.(y.t++, 0);");
				break;
			}
			while (--j >= 0)
			{
				// Estimate quotient digit
				var qd = (int)((r.Data[--i] == y0) ? DM : r.Data[i] * d1 + (r.Data[i - 1] + e) * d2);
				if ((r.Data[i] += y.Am(0, qd, r, j, 0, ys)) < qd)
				{ // Try it out
					y.DLShiftTo(j, t);
					r.SubTo(t, r);
					while (r.Data[i] < --qd)
					{
						r.SubTo(t, r);
					}
				}
			}
			if (q != null)
			{
				r.DRShiftTo(ys, q);
				if (ts != ms)
				{
					ZERO.SubTo(q, q);
				}
			}
			r.ChunkCount = ys;
			r.Clamp();
			if (nsh > 0)
			{
				r.RShiftTo((int)nsh, r);
			}
			if (ts < 0)
			{
				ZERO.SubTo(r, r);
			}
		}

		public int InvDigit()
		{
			if (ChunkCount < 1) return 0;
			var x = Data[0];
			if ((x & 1) == 0) return 0;
			var y = x & 3;
			y = (y * (2 - (x & 0xf) * y)) & 0xf;
			y = (y * (2 - (x & 0xff) * y)) & 0xff;
			y = (y * (2 - (((x & 0xffff) * y) & 0xffff))) & 0xffff;
			y = (y * (2 - x * y % DV)) % DV;
			return (int)((y > 0) ? DV - y : -y);
		}

		private void RShiftTo(int n, BigInteger r)
		{
			r.Sign = Sign;
			var ds = n / DB;
			if (ds >= ChunkCount)
			{
				r.ChunkCount = 0;
				return;
			}
			var bs = n % DB;
			var cbs = DB - bs;
			var bm = (1 << bs) - 1;
			r.Data[0] = Data[ds] >> bs;
			for (var i = ds + 1; i < ChunkCount; ++i)
			{
				r.Data[i - ds - 1] |= (Data[i] & bm) << cbs;
				r.Data[i - ds] = Data[i] >> bs;
			}
			if (bs > 0)
			{
				r.Data[ChunkCount - ds - 1] |= (uint)(Sign & bm) << cbs;
			}
			r.ChunkCount = ChunkCount - ds;
			r.Clamp();
		}

		public void DRShiftTo(int n, BigInteger r) {
			for (var i = n; i < ChunkCount; ++i)
			{
				r.Data[i - n] = Data[i];
			}
			r.ChunkCount = Math.Max(ChunkCount - n, 0);
			r.Sign = Sign;
		}

		public void CopyTo(BigInteger r)
		{
			for (var i = ChunkCount - 1; i >= 0; --i)
			{
				r.Data[i] = Data[i];
			}
			r.ChunkCount = ChunkCount;
			r.Sign = Sign;
		}

		public void DLShiftTo(int n, BigInteger r) {
			for (var i = ChunkCount - 1; i >= 0; --i)
			{
				r.Data[i + n] = Data[i];
			}
			for (var i = n - 1; i >= 0; --i)
			{
				r.Data[i] = 0;
			}
			r.ChunkCount = ChunkCount + n;
			r.Sign = Sign;
		}

		private void LShiftTo(int n, BigInteger r)
		{
			var bs = n % DB;
			var cbs = DB - bs;
			var bm = (1 << cbs) - 1;
			var ds = n / DB;
			var c = (Sign << bs) & DM;

			for (var i = ChunkCount - 1; i >= 0; --i)
			{
				r.Data[i + ds + 1] = ((int)Data[i] >> cbs) | c;
				c = (int)((Data[i] & bm) << bs);
			}
			for (var i = ds - 1; i >= 0; --i)
			{
				r.Data[i] = 0;
			}
			r.Data[ds] = c;
			r.ChunkCount = ChunkCount + ds + 1;
			r.Sign = Sign;
			r.Clamp();
		}

		public BigInteger Negate()
		{
			var r = new BigInteger();
			ZERO.SubTo(this, r);
			return r;
		}

		public void MultiplyTo(BigInteger v, BigInteger r)
		{
			var x = Abs();
			var y = v.Abs();
			var i = x.ChunkCount;
			r.ChunkCount = i + y.ChunkCount;
			while (--i >= 0)
			{
				r.Data[i] = 0;
			}
			for (i = 0; i < y.ChunkCount; ++i)
			{
				r.Data[i + x.ChunkCount] = x.Am(0, (int)y.Data[i], r, i, 0, x.ChunkCount);
			}
			r.Sign = 0;
			r.Clamp();
			if (Sign != v.Sign)
			{
				ZERO.SubTo(r, r);
			}
		}

		public void SquareTo(BigInteger r)
		{
			var x = Abs();
			var i = r.ChunkCount = 2 * x.ChunkCount;
			while (--i >= 0) r.Data[i] = 0;
			for (i = 0; i < x.ChunkCount - 1; ++i)
			{
				var c = x.Am(i, (int)x.Data[i], r, 2 * i, 0, 1);
				if ((r.Data[i + x.ChunkCount] += x.Am(i + 1, (int)(2 * x.Data[i]), r, 2 * i + 1, c, x.ChunkCount - i - 1)) >= DV)
				{
					r.Data[i + x.ChunkCount] -= DV;
					r.Data[i + x.ChunkCount + 1] = 1;
				}
			}
			if (r.ChunkCount > 0)
			{
				r.Data[r.ChunkCount - 1] += x.Am(i, (int)x.Data[i], r, 2 * i, 0, 1);
			}
			r.Sign = 0;
			r.Clamp();
		}

		private void DMultiply(int n)
		{
			Data[ChunkCount] = Am(0, n - 1, this, 0, 0, ChunkCount);
			++ChunkCount;
			Clamp();
		}

		public int Am(int i, int x, BigInteger w, int j, int c, int n)
		{
			var xl = x & 0x7fff;
			var xh = x >> 15;
			while (--n >= 0)
			{
				var l = Data[i] & 0x7fff;
				var h = Data[i++] >> 15;
				var m = xh * l + h * xl;
				l = xl * l + ((m & 0x7fff) << 15) + w.Data[j] + (c & 0x3fffffff);
				c = (int)(((uint)l >> 30) + ((uint)m >> 15) + xh * h + ((uint)c >> 30));
				w.Data[j++] = l & 0x3fffffff;
			}
			return c;
		}

		public void SubTo(BigInteger v, BigInteger r)
		{
			var i = 0;
			var c = 0L;
			var m = Math.Min(v.ChunkCount, ChunkCount);
			while (i < m)
			{
				c += Data[i] - v.Data[i];
				r.Data[i++] = c & DM;
				c >>= DB;
			}
			if (v.ChunkCount < ChunkCount)
			{
				c -= v.Sign;
				while (i < ChunkCount)
				{
					c += Data[i];
					r.Data[i++] = c & DM;
					c >>= DB;
				}
				c += Sign;
			}
			else
			{
				c += Sign;
				while (i < v.ChunkCount)
				{
					c -= v.Data[i];
					r.Data[i++] = c & DM;
					c >>= DB;
				}
				c -= v.Sign;
			}
			r.Sign = (c < 0) ? -1 : 0;
			if (c < -1)
			{
				r.Data[i++] = DV + c;
			}
			else if (c > 0)
			{
				r.Data[i++] = c;
			}
			r.ChunkCount = i;
			r.Clamp();
		}

		public void FromRadix(string s, int b = 10)
		{
			FromInt(0);
			var cs = ChunkSize(b);
			var d = Math.Pow(b, cs);
			var mi = false;
			var j = 0;
			var w = 0;
			for (var i = 0; i < s.Length; ++i)
			{
				var x = IntAt(s, i);
				if (x < 0)
				{
					if (s[i] == '-' && SigNum() == 0)
					{
						mi = true;
					}
					continue;
				}
				w = b * w + x;
				if (++j >= cs)
				{
					DMultiply((int)d);
					DAddOffset(w, 0);
					j = 0;
					w = 0;
				}
			}
			if (j > 0)
			{
				DMultiply((int)Math.Pow(b, j));
				DAddOffset(w, 0);
			}
			if (mi)
			{
				ZERO.SubTo(this, this);
			}
		}

		public string ToRadix(int b = 10)
		{
			if (SigNum() == 0 || b < 2 || b > 32) { return "0"; }
			var cs = ChunkSize(b);
			var a = Math.Pow(b, cs);
			var d = Nbv((int)a);
			var y = new BigInteger();
			var z = new BigInteger();
			var r = string.Empty;
			DivRemTo(d, y, z);
			while (y.SigNum() > 0)
			{
				r = Convert.ToString((long)a + z.IntValue(), b).Substring(1) + r;
				y.DivRemTo(d, y, z);
			}
			return Convert.ToString(z.IntValue(), b) + r;
		}

		public int SigNum()
		{
			if (Sign < 0)
			{
				return -1;
			}
			else if (ChunkCount <= 0 || (ChunkCount == 1 && Data[0] <= 0))
			{
				return 0;
			}
			else
			{
				return 1;
			}
		}

		public long IntValue()
		{
			if (Sign < 0)
			{
				if (ChunkCount == 1)
				{
					return Data[0] - DV;
				}
				else if (ChunkCount == 0)
				{
					return -1;
				}
			}
			else if (ChunkCount == 1)
			{
				return Data[0];
			}
			else if (ChunkCount == 0)
			{
				return 0;
			}
			// assumes 16 < DB < 32
			return ((Data[1] & ((1 << (32 - DB)) - 1)) << DB) | Data[0];
		}

		private bool IsEven() =>
			((ChunkCount > 0) ? (Data[0] & 1) : Sign) == 0;

		private BigInteger Exp(long e, IReduction z)
		{
			if (e > 0xffffffff || e < 1) return ONE;
			var r = new BigInteger();
			var r2 = new BigInteger();
			var g = z.Convert(this);
			var i = (int)(NBits(e) - 1);
			g.CopyTo(r);
			while (--i >= 0)
			{
				z.SqrTo(r, r2);
				if ((e & (1 << i)) > 0)
				{
					z.MulTo(r2, g, r);
				}
				else
				{
					var t = r;
					r = r2;
					r2 = t;
				}
			}
			return z.Revert(r);
		}

		public BigInteger ModPowInt(int e, BigInteger m)
		{
			IReduction z;
			if (e < 256 || m.IsEven())
			{
				z = new ClassicReduction(m);
			}
			else
			{
				z = new MontgomeryReduction(m);
			}
			return Exp(e, z);
		}

		public uint ToArray(ASByteArray array)
		{
			const int k = 8;
			const int km = (1 << 8) - 1;
			var i = ChunkCount;
			var p = DB - i * DB % k;
			var m = false;
			var c = 0U;
			if (i-- > 0)
			{
				int d;
				if (p < DB && (d = (int)(Data[i] >> p)) > 0)
				{
					m = true;
					array.WriteByte((byte)d);
					c++;
				}
				while (i >= 0)
				{
					if (p < k)
					{
						d = (int)((Data[i] & ((1 << p) - 1)) << (k - p));
						d |= (int)(Data[--i] >> (p += DB - k));
					}
					else
					{
						d = (int)((Data[i] >> (p -= k)) & km);
						if (p <= 0)
						{
							p += DB;
							--i;
						}
					}
					if (d > 0)
					{
						m = true;
					}
					if (m)
					{
						array.WriteByte((byte)d);
						c++;
					}
				}
			}
			return c;
		}

		public ASByteArray ToByteArray()
		{
			var i = ChunkCount;
			var r = new ASByteArray();
			r[0] = (byte)Sign;
			var p = DB - (i * DB) % 8;
			var d = 0;
			var k = 0;
			if (i-- > 0)
			{
				if (p < DB && (d = (int)(Data[i] >> p)) != (Sign & DM) >> p)
				{
					r[k++] = (byte)(d | (Sign << (DB - p)));
				}
				while (i >= 0)
				{
					if (p < 8)
					{
						d = (int)((Data[i] & ((1 << p) - 1)) << (8 - p));
						d |= (int)(Data[--i] >> (p += DB - 8));
					}
					else
					{
						d = (int)((Data[i] >> (p -= 8)) & 0xff);
						if (p <= 0)
						{
							p += DB;
							--i;
						}
					}
					if ((d & 0x80) != 0) d |= -256;
					if (k == 0 && (Sign & 0x80) != (d & 0x80)) ++k;
					if (k > 0 || d != Sign) r[k++] = (byte)d;
				}
			}
			return r;
		}

		public int BitLength()
		{
			if (ChunkCount <= 0) return 0;
			return (int)(DB * (ChunkCount - 1) + NBits(Data[ChunkCount - 1] ^ (Sign & DM)));
		}

		private long NBits(long x)
		{
			var r = 1L;
			long t;

			if ((t = (int)((uint)x >> 16)) != 0)
			{
				x = t; r += 16;
			}

			if ((t = x >> 8) != 0) { x = t; r += 8; }
			if ((t = x >> 4) != 0) { x = t; r += 4; }
			if ((t = x >> 2) != 0) { x = t; r += 2; }
			if ((_ = x >> 1) != 0) { r += 1; }
			return r;
		}

		public bool Equals(BigInteger other)
		{
			return CompareTo(other) == 0;
		}

		public BigInteger ModPow(BigInteger e, BigInteger m)
		{
			var i = e.BitLength();
			var r = Nbv(1);
			IReduction z;

			int k;
			if (i <= 0)
			{
				return r;
			}
			else if (i < 18)
			{
				k = 1;
			}
			else if (i < 48)
			{
				k = 3;
			}
			else if (i < 144)
			{
				k = 4;
			}
			else if (i < 768)
			{
				k = 5;
			}
			else
			{
				k = 6;
			}
			if (i < 8)
			{
				z = new ClassicReduction(m);
			}
			else if (m.IsEven())
			{
				z = new BarrettReduction(m);
			}
			else
			{
				z = new MontgomeryReduction(m);
			}
			// precomputation
			var g = new ASArray<BigInteger>();
			var n = 3;
			var k1 = k - 1;
			var km = (1 << k) - 1;
			g[1] = z.Convert(this);
			if (k > 1)
			{
				var g2 = new BigInteger();
				z.SqrTo(g[1], g2);
				while (n <= km)
				{
					g[n] = new BigInteger();
					z.MulTo(g2, g[n - 2], g[n]);
					n += 2;
				}
			}

			var j = e.ChunkCount - 1;
			var is1 = true;
			var r2 = new BigInteger();
			BigInteger t;
			i = (int)NBits(e.Data[j]) - 1;
			while (j >= 0)
			{
				int w;
				if (i >= k1)
				{
					w = (int)((e.Data[j] >> (i - k1)) & km);
				}
				else
				{
					w = (int)((e.Data[j] & ((1 << (i + 1)) - 1)) << (k1 - i));
					if (j > 0)
					{
						w |= (int)(e.Data[j - 1] >> (DB + i - k1));
					}
				}
				n = k;
				while ((w & 1) == 0)
				{
					w >>= 1;
					--n;
				}
				if ((i -= n) < 0)
				{
					i += DB;
					--j;
				}
				if (is1)
				{ // ret == 1, don't bother squaring or multiplying it
					g[w].CopyTo(r);
					is1 = false;
				}
				else
				{
					while (n > 1)
					{
						z.SqrTo(r, r2);
						z.SqrTo(r2, r);
						n -= 2;
					}
					if (n > 0)
					{
						z.SqrTo(r, r2);
					}
					else
					{
						t = r;
						r = r2;
						r2 = t;
					}
					z.MulTo(r2, g[w], r);
				}
				while (j >= 0 && (e.Data[j] & (1 << i)) == 0)
				{
					z.SqrTo(r, r2);
					t = r;
					r = r2;
					r2 = t;
					if (--i < 0)
					{
						i = DB - 1;
						--j;
					}

				}
			}
			return z.Revert(r);
		}

		public void Dispose()
		{
			for(var i = 0; i < (Data?.Count).GetValueOrDefault(); ++i)
			{
				Data[i] = PRNG.Instance.Next();
			}
			Data.Clear();
			ChunkCount = 0;
		}
	}
}
