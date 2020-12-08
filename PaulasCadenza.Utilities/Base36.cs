using System;
using System.Collections.Generic;
using System.Linq;

namespace PaulasCadenza.Utilities
{
	public static class Base36
	{
		private const string CharList = "0123456789abcdefghijklmnopqrstuvwxyz";

		public static string Encode(long input)
		{
			if (input < 0)
			{
				throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");
			}

			var result = new Stack<char>();
			while (input != 0)
			{
				result.Push(CharList[(int)(input % 36)]);
				input /= 36;
			}

			return new string(result.ToArray());
		}

		public static long Decode(string input)
		{
			var reversed = input.ToLower().Reverse();
			var result = 0L;
			var pos = 0;
			foreach (var c in reversed)
			{
				result += CharList.IndexOf(c) * (long)Math.Pow(36, pos);
				++pos;
			}
			return result;
		}
	}
}
