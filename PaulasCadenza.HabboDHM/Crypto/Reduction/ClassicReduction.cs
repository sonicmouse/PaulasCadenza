using PaulasCadenza.HabboDHM.Crypto;

namespace PaulasCadenza.HabboDHM.Crypto.Reduction
{
	internal sealed class ClassicReduction : IReduction
	{
		private readonly BigInteger _m;

		public ClassicReduction(BigInteger m)
		{
			_m = m;
		}

		public BigInteger Convert(BigInteger x)
		{
			if (x.Sign < 0 || x.CompareTo(_m) >= 0)
			{
				return x.Mod(_m);
			}
			return x;
		}

		public void MulTo(BigInteger x, BigInteger y, BigInteger r)
		{
			x.MultiplyTo(y, r);
			Reduce(r);
		}

		public void Reduce(BigInteger x)
		{
			x.DivRemTo(_m, null, x);
		}

		public BigInteger Revert(BigInteger x)
		{
			return x;
		}

		public void SqrTo(BigInteger x, BigInteger r)
		{
			x.SquareTo(r);
			Reduce(r);
		}
	}
}
