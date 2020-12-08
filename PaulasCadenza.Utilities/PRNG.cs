using System;
using System.Linq;

namespace PaulasCadenza.Utilities
{
	public sealed class PRNG
	{
		private readonly Random _rng = new Random();

		public static PRNG Instance { get; } = new PRNG();
		private PRNG() { }

		/// <summary>
		/// Returns a non-negative random integer.
		/// </summary>
		public int Next() => _rng.Next();

		/// <summary>
		/// Returns a random integer that is within a specified range.
		/// </summary>
		public int Next(int inclusiveMin, int exclusiveMax) =>
			_rng.Next(inclusiveMin, exclusiveMax);

		/// <summary>
		/// Returns a non-negative random integer that is less than the specified maximum.
		/// </summary>
		public int Next(int exclusiveMax) =>
			_rng.Next(exclusiveMax);

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		public void NextBytes(byte[] buffer) =>
			_rng.NextBytes(buffer);

		/// <summary>
		/// Returns a string made of up random hexadecimal values in range of [0,255].
		/// </summary>
		public string HexString(int count) =>
			string.Concat(Enumerable.Range(0, count).Select(x => $"{Convert.ToByte(Next(256)):x2}"));
	}
}
