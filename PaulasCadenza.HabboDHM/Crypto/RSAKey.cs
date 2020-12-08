using PaulasCadenza.HabboDHM.ASUtilities;
using PaulasCadenza.Utilities;
using System;

namespace PaulasCadenza.HabboDHM.Crypto
{
	internal sealed class RSAKey : IDisposable
	{
		private delegate BigInteger EncOpFunc(BigInteger block);
		private delegate ASByteArray PadFunc(ASByteArray src, int end, int n, byte type);
		private delegate ASByteArray UnPadFunc(BigInteger src, uint n, byte type);

		// public key
		private readonly int _e; // public exponent. must be <2^31
		private readonly BigInteger _n; // modulus

		// private key
		private readonly BigInteger _d;

		// extended private key
		private readonly BigInteger _p;
		private readonly BigInteger _q;
		private readonly BigInteger _dmp1;
		private readonly BigInteger _dmq1;
		private readonly BigInteger _coeff;

		private readonly bool _canDecrypt;
		private readonly bool _canEncrypt;

		public RSAKey(BigInteger N, int E,
			BigInteger D = null,
			BigInteger P = null, BigInteger Q = null,
			BigInteger DP = null, BigInteger DQ = null,
			BigInteger C = null)
		{
			_n = N;
			_e = E;
			_d = D;
			_p = P;
			_q = Q;
			_dmp1 = DP;
			_dmq1 = DQ;
			_coeff = C;
			_canEncrypt = (_n != null) && (_e != 0);
			_canDecrypt = _canEncrypt && (_d != null);
		}

		public static RSAKey ParsePublicKey(string N, string E) =>
			new RSAKey(new BigInteger(N, 16, true), Convert.ToInt32(E, 16));

		public static RSAKey ParsePrivateKey(string N, string E, string D,
			string P = null, string Q = null,
			string DMP1 = null, string DMQ1 = null, string IQMP = null)
		{
			if (P == null)
			{
				return new RSAKey(new BigInteger(N, 16, true), Convert.ToInt32(E, 16), new BigInteger(D, 16, true));
			}
			else
			{
				return new RSAKey(new BigInteger(N, 16, true), Convert.ToInt32(E, 16), new BigInteger(D, 16, true),
					new BigInteger(P, 16, true), new BigInteger(Q, 16, true),
					new BigInteger(DMP1, 16, true), new BigInteger(DMQ1, 16, true),
					new BigInteger(IQMP, 16, true));
			}
		}

		public void Verify(ASByteArray src, ASByteArray dst, uint length) =>
			DecryptInternal(DoPublic, src, dst, length, null, 0x01);

		public void Encrypt(ASByteArray src, ASByteArray dst, uint length, bool randomize = true)
		{
			if(randomize)
			{
				EncryptInternal(DoPublic, src, dst, length, PKCS1Pad, 0x02);
			}
			else
			{
				EncryptInternal(DoPublic, src, dst, length, PKCS1PadNoRandom, 0x02);
			}
		}

		private uint GetBlockSize() =>
			(uint)((_n.BitLength() + 7) / 8);

		private void EncryptInternal(EncOpFunc op, ASByteArray src, ASByteArray dst, uint length, PadFunc pad, byte padType)
		{
			if (pad == null) pad = PKCS1Pad;

			if (src.Position >= src.Length)
			{
				src.Position = 0;
			}

			var bl = GetBlockSize();
			var end = src.Position + length;
			while (src.Position < end)
			{
				var block = new BigInteger(pad(src, (int)end, (int)bl, padType), (int)bl, true);
				var chunk = op(block);
				chunk.ToArray(dst);
			}
		}

		private void DecryptInternal(EncOpFunc op, ASByteArray src, ASByteArray dst, uint length, UnPadFunc pad, byte padType)
		{
			if (pad == null) pad = PKCS1UnPad;

			if (src.Position >= src.Length)
			{
				src.Position = 0;
			}
			var bl = GetBlockSize();
			var end = src.Position + length;
			while (src.Position < end)
			{
				var block = new BigInteger(src, (int)bl, true);
				var chunk = op(block);
				var b = pad(chunk, bl, padType);
				if (b == null)
				{
					throw new InvalidOperationException("Decrypt error - padding function returned null!");
				}
				dst.WriteBytes(b);
			}
		}

		private static ASByteArray PKCS1PadNoRandom(ASByteArray src, int end, int n, byte type)
		{
			var outa = new ASByteArray();
			var p = src.Position;
			end = Math.Min(end, Math.Min(src.Length, p + n - 11));
			src.Position = end;
			var i = end - 1;
			while (i >= p && n > 11)
			{
				outa[--n] = src[i--];
			}
			outa[--n] = 0;
			while (n > 2)
			{
				outa[--n] = 0xFF;
			}
			outa[--n] = type;
			outa[--n] = 0;
			return outa;
		}

		private static ASByteArray PKCS1Pad(ASByteArray src, int end, int n, byte type)
		{
			var output = new ASByteArray();
			var p = src.Position;
			end = Math.Min(end, Math.Min(src.Length, p + n - 11));
			src.Position = end;
			var i = end - 1;

			while (i >= p && n > 11)
			{
				output[--n] = src[i--];
			}

			output[--n] = 0;

			if (type == 0x02)
			{
				while (n > 2)
				{
					byte x;
					do
					{
						x = (byte)PRNG.Instance.Next(256);
					} while (x == 0);
					output[--n] = x;
				}
			}
			else
			{ // type 1
				while (n > 2)
				{
					output[--n] = 0xFF;
				}
			}

			output[--n] = type;
			output[--n] = 0;
			return output;
		}

		private static ASByteArray PKCS1UnPad(BigInteger src, uint n, byte type = 0x02)
		{
			var b = src.ToByteArray();
			b.Position = 0;

			var i = 0;
			while (i < b.Length && b[i] == 0) ++i;
			if (b.Length - i != n - 1 || b[i] != type)
			{
				return null;
			}

			++i;

			while (b[i] != 0)
			{
				if (++i >= b.Length)
				{
					return null;
				}
			}

			var output = new ASByteArray();

			while (++i < b.Length)
			{
				output.WriteByte(b[i]);
			}

			output.Position = 0;
			return output;
		}

		private BigInteger DoPublic(BigInteger x) =>
			x.ModPowInt(_e, _n);

		public void Dispose()
		{
			_n?.Dispose();
			_d?.Dispose();
		}
	}
}
