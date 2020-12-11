using System;
using System.Linq;
using System.Security.Cryptography;

namespace PaulasCadenza.Utilities
{
	public sealed class PRNG
	{
		private readonly RandomNumberGenerator _rng2;

		public static PRNG Instance { get; } = new PRNG();
		private PRNG()
		{
			_rng2 = new RNGCryptoServiceProvider();
		}

		/// <summary>
		/// Returns a non-negative random integer.
		/// </summary>
		public int Next()
		{
			var buf = new byte[sizeof(uint)];
			_rng2.GetBytes(buf);
			return (int)(BitConverter.ToUInt32(buf, 0) & 0x7FFFFFFFU);
		}

		/// <summary>
		/// Returns a random integer that is within a specified range.
		/// </summary>
		public int Next(int inclusiveMin, int exclusiveMax) =>
			inclusiveMin + (Next() % (exclusiveMax - inclusiveMin));

		/// <summary>
		/// Returns a non-negative random integer that is less than the specified maximum.
		/// </summary>
		public int Next(int exclusiveMax) =>
			Next() % exclusiveMax;

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		public void NextBytes(byte[] buffer) =>
			_rng2.GetBytes(buffer);

		/// <summary>
		/// Returns a string made of up random hexadecimal values in range of [0,255].
		/// </summary>
		public string HexString(int count) =>
			string.Concat(Enumerable.Range(0, count).Select(x => $"{Convert.ToByte(Next(256)):x2}"));
	}
}
