using System;
using System.Linq;
using System.Security.Cryptography;

namespace PaulasCadenza.Utilities
{
	public interface IPRNG
	{
		/// <summary>
		/// Returns a non-negative random integer.
		/// </summary>
		int Next();

		/// <summary>
		/// Returns a random unsigned integer.
		/// </summary>
		uint NextU();

		/// <summary>
		/// Returns a non-negative random long integer.
		/// </summary>
		/// <returns></returns>
		long NextL();

		/// <summary>
		/// Returns a random long unsigned integer.
		/// </summary>
		/// <returns></returns>
		ulong NextUL();

		/// <summary>
		/// Returns a random integer that is within a specified range.
		/// </summary>
		int Next(int inclusiveMin, int exclusiveMax);

		/// <summary>
		/// Returns a non-negative random integer that is less than the specified maximum.
		/// </summary>
		int Next(int exclusiveMax);

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		byte[] NextBytes(byte[] buffer);

		/// <summary>
		/// Returns a string made of up random hexadecimal values in range of [0,255].
		/// </summary>
		string HexString(int count);
	}

	public sealed class PRNG : IPRNG
	{
		private readonly RandomNumberGenerator _prng;

		public static IPRNG Instance { get; } = new PRNG();
		private PRNG()
		{
			_prng = new RNGCryptoServiceProvider();
		}

		public int Next() =>
			(int)(NextU() & 0x7FFFFFFFU);

		public uint NextU() =>
			BitConverter.ToUInt32(NextBytes(new byte[sizeof(uint)]), 0);

		public long NextL() =>
			(long)(NextUL() & 0x7FFFFFFFFFFFFFFFL);

		public ulong NextUL() =>
			BitConverter.ToUInt64(NextBytes(new byte[sizeof(ulong)]), 0);

		public int Next(int inclusiveMin, int exclusiveMax) =>
			inclusiveMin + (Next() % (exclusiveMax - inclusiveMin));

		public int Next(int exclusiveMax) =>
			Next() % exclusiveMax;

		public byte[] NextBytes(byte[] buffer)
		{
			_prng.GetBytes(buffer);
			return buffer;
		}

		public string HexString(int count) =>
			string.Concat(Enumerable.Range(0, count).Select(x => $"{Convert.ToByte(Next(256)):x2}"));
	}
}
