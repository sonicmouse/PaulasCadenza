using System.Collections.Generic;
using System.Drawing;

namespace PaulasCadenza.UI.Helpers
{
	public abstract class Cadence
	{
		public abstract string Title { get; }
		public override string ToString() => Title;
		public abstract IEnumerable<Point> DeriveOffsets(int count);
	}

	public sealed class SingleCadence : Cadence
	{
		public override string Title => "Single";

		public override IEnumerable<Point> DeriveOffsets(int count)
		{
			while(true) { yield return new Point(0, 0); }
		}
	}

	public sealed class DiagonalCadence : Cadence
	{
		public override string Title => "Diagonal";

		public override IEnumerable<Point> DeriveOffsets(int count)
		{
			int x = 0, y = 0;
			while (true) { yield return new Point(x++, y++); }
		}
	}

	public sealed class HorzLineCadence : Cadence
	{
		public override string Title => "Horz. Line";

		public override IEnumerable<Point> DeriveOffsets(int count)
		{
			int x = 0, y = 0;
			while (true) { yield return new Point(x++, y); }
		}
	}

	public sealed class VertLineCadence : Cadence
	{
		public override string Title => "Vert. Line";

		public override IEnumerable<Point> DeriveOffsets(int count)
		{
			int x = 0, y = 0;
			while (true) { yield return new Point(x, y++); }
		}
	}

	public static class PlatoonSgt
	{
		public static IEnumerable<Cadence> GetCadences()
		{
			return new Cadence[]
			{
				new SingleCadence(),
				new DiagonalCadence(),
				new HorzLineCadence(),
				new VertLineCadence()
			};
		}
	}
}
