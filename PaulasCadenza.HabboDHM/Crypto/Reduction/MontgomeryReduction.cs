using PaulasCadenza.HabboDHM.Crypto;

namespace PaulasCadenza.HabboDHM.Crypto.Reduction
{
	internal sealed class MontgomeryReduction : IReduction
	{
		private readonly BigInteger _m;
		private readonly int _mp, _mpl, _mph, _um, _mt2;

		public MontgomeryReduction(BigInteger m)
		{
			_m = m;
			_mp = m.InvDigit();
			_mpl = _mp & 0x7fff;
			_mph = _mp >> 15;
			_um = (1 << (BigInteger.DB - 15)) - 1;
			_mt2 = 2 * m.ChunkCount;
		}

		public BigInteger Convert(BigInteger x)
		{
			var r = new BigInteger();
			x.Abs().DLShiftTo(_m.ChunkCount, r);
			r.DivRemTo(_m, null, r);
			if (x.Sign < 0 && r.CompareTo(BigInteger.ZERO) > 0)
			{
				_m.SubTo(r, r);
			}
			return r;
		}

		public void MulTo(BigInteger x, BigInteger y, BigInteger r)
		{
			x.MultiplyTo(y, r);
			Reduce(r);
		}

		public void Reduce(BigInteger x)
		{
			while (x.ChunkCount <= _mt2)
			{
				x.Data[x.ChunkCount++] = 0;
			}
			for (var i = 0; i < _m.ChunkCount; ++i) {
				// faster way of calculating u0 = x[i]*mp mod DV
				var j = (int)(x.Data[i] & 0x7fff);
				var u0 = (j * _mpl + (((j * _mph + (x.Data[i] >> 15) * _mpl) & _um) << 15)) & BigInteger.DM;
				// use am to combine the multiply-shift-add into one call
				j = i + _m.ChunkCount;
				x.Data[j] += _m.Am(0, (int)u0, x, i, 0, _m.ChunkCount);
				// propagate carry
				while (x.Data[j] >= BigInteger.DV)
				{
					x.Data[j] -= BigInteger.DV;
					x.Data[++j]++;
				}
			}
			x.Clamp();
			x.DRShiftTo(_m.ChunkCount, x);
			if (x.CompareTo(_m) >= 0)
			{
				x.SubTo(_m, x);
			}
		}

		public BigInteger Revert(BigInteger x)
		{
			var r = new BigInteger();
			x.CopyTo(r);
			Reduce(r);
			return r;
		}

		public void SqrTo(BigInteger x, BigInteger r)
		{
			x.SquareTo(r);
			Reduce(r);
		}
	}
}
