using PaulasCadenza.HabboDHM.Crypto;
using System;
using System.Linq;

namespace PaulasCadenza.HabboDHM
{
	internal sealed class DHMSession : IDisposable
	{
		private BigInteger var_3224;
		private BigInteger var_4720;
		private BigInteger var_3514;
		private BigInteger var_3626;
		private readonly BigInteger var_4293;
		private readonly BigInteger var_5094;
		private readonly BigInteger var_5060;
		private readonly BigInteger var_5146;

		public DHMSession(BigInteger param1, BigInteger param2)
		{
			var_5060 = BigInteger.Nbv(2);
			var_5146 = BigInteger.Nbv(2);
			var_4293 = param1;
			var_5094 = param2;
		}

		public bool Init(string s, int radix = 16)
		{
			var_3224 = new BigInteger();
			var_3224.FromRadix(s, radix);
			var_4720 = var_5094.ModPow(var_3224, var_4293);
			return true;
		}

		public string Init2(string s, int radix = 16)
		{
			var_3514 = new BigInteger();
			var_3514.FromRadix(s, radix);
			var_3626 = var_3514.ModPow(var_3224, var_4293);
			return ApplyRadix2(radix);
		}

		public string ApplyRadix1(int k = 16) =>
			var_4720.ToRadix(k);

		public string ApplyRadix2(int k = 16) =>
			var_3626.ToRadix(k);

		public bool Compare1() =>
			var_3514.CompareTo(var_5060) >= 0;

		public bool Compare2() =>
			var_3626.CompareTo(var_5146) >= 0;

		public void Dispose()
		{
			new BigInteger[]
			{
				var_3224, var_4720, var_3514, var_3626, var_4293, var_5094, var_5060, var_5146
			}.ToList().ForEach(x => x?.Dispose());
			var_3224 = var_4720 = var_3514 = var_3626 = null;
		}
	}
}
