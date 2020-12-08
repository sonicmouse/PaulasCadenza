using PaulasCadenza.HabboDHM.Crypto;

namespace PaulasCadenza.HabboDHM.Crypto.Reduction
{
	internal sealed class BarrettReduction : IReduction
	{
		private readonly BigInteger _m;
		private readonly BigInteger _r2;
		private readonly BigInteger _q3;
		private readonly BigInteger _mu;

		public BarrettReduction(BigInteger m)
		{
			_r2 = new BigInteger();
			_q3 = new BigInteger();
			BigInteger.ONE.DLShiftTo(2 * m.ChunkCount, _r2);
			_mu = _r2.Divide(m);
			_m = m;
		}

		public BigInteger Revert(BigInteger x) => x;

		public void MulTo(BigInteger x, BigInteger y, BigInteger r)
		{
			x.MultiplyTo(y, r);
			Reduce(r);
		}

		public void SqrTo(BigInteger x, BigInteger r)
		{
			x.SquareTo(r);
			Reduce(r);
		}

		public BigInteger Convert(BigInteger x)
		{
			if (x.Sign < 0 || x.ChunkCount > 2 * _m.ChunkCount)
			{
				return x.Mod(_m);
			}
			else if (x.CompareTo(_m) < 0)
			{
				return x;
			}
			else
			{
				var r = new BigInteger();
				x.CopyTo(r);
				Reduce(r);
				return r;
			}
		}

		public void Reduce(BigInteger x)
		{
			x.DRShiftTo(_m.ChunkCount-1,_r2);
			if (x.ChunkCount > _m.ChunkCount + 1)
			{
				x.ChunkCount = _m.ChunkCount + 1;
				x.Clamp();
			}

			_mu.MultiplyUpperTo(_r2, _m.ChunkCount+1, _q3);
			_m.MultiplyLowerTo(_q3, _m.ChunkCount+1, _r2);

			while (x.CompareTo(_r2) < 0)
			{
				x.DAddOffset(1, _m.ChunkCount + 1);
			}
			x.SubTo(_r2,x);
			while (x.CompareTo(_m) >= 0)
			{
				x.SubTo(_m, x);
			}
		}
	}
}
