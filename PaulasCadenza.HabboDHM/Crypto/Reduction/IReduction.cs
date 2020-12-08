namespace PaulasCadenza.HabboDHM.Crypto.Reduction
{
	internal interface IReduction
	{
		BigInteger Convert(BigInteger x);
		BigInteger Revert(BigInteger x);
		void Reduce(BigInteger x);
		void MulTo(BigInteger x, BigInteger y, BigInteger r);
		void SqrTo(BigInteger x, BigInteger r);
	}
}
