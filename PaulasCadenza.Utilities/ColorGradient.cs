using System;
using System.Collections.Generic;
using System.Drawing;

namespace PaulasCadenza.Utilities
{
	public static class ColorGradient
	{
		public static IReadOnlyList<Color> Create(Color from, Color to, int totalNumberOfColors)
		{
			double diffR = to.R - from.R;
			double diffG = to.G - from.G;
			double diffB = to.B - from.B;

			var steps = totalNumberOfColors - 1;

			var stepR = diffR / steps;
			var stepG = diffG / steps;
			var stepB = diffB / steps;

			var lst = new List<Color>(capacity: totalNumberOfColors) { from };

			for (var i = 1; i < steps; ++i)
			{
				lst.Add(Color.FromArgb(
					c(from.R, stepR, i),
					c(from.G, stepG, i),
					c(from.B, stepB, i)));

				int c(int fromC, double stepC, int step) =>
					(int)Math.Round(fromC + stepC * step);
			}

			lst.Add(to);

			return lst;
		}
	}
}
