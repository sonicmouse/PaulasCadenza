using System;
using System.Linq;

namespace PaulasCadenza.Utilities.Extensions
{
	public static class EndianExtensions
	{
		public static uint SwapEndianness(this uint value) =>
			((value >> 0) & 0xff) << 24 | ((value >> 8) & 0xff) << 16 |
				((value >> 16) & 0xff) << 8 | ((value >> 24) & 0xff) << 0;

		public static int SwapEndianness(this int value) =>
			((value >> 0) & 0xff) << 24 | ((value >> 8) & 0xff) << 16 |
				((value >> 16) & 0xff) << 8 | ((value >> 24) & 0xff) << 0;

		public static ushort SwapEndianness(this ushort value) =>
			(ushort)(((value >> 0) & 0xff) << 8 | ((value >> 8) & 0xff) << 0);

		public static short SwapEndianness(this short value) =>
			(short)(((value >> 0) & 0xff) << 8 | ((value >> 8) & 0xff) << 0);

		public static float SwapEndianness(this float value) =>
			BitConverter.ToSingle(BitConverter.GetBytes(value).Reverse().ToArray(), 0);

		public static double SwapEndianness(this double value) =>
			BitConverter.ToDouble(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
	}
}
