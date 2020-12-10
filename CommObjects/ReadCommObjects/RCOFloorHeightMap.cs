using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOFloorHeightMap : CommReadObject
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int FixedWallsHeight { get; private set; }
		public double Scale { get; private set; }
		public override ushort SendType => 329;

		private List<List<int>> _planes;

		public int GetFloorHeightAtPoint(int x, int y)
		{
			if ((x < 0) || (x >= Width) || (y < 0) || (y >= Height))
			{
				return -110;
			};
			return _planes[y][x];
		}

		public override void Deserialize(CommReader reader)
		{
			Scale = reader.ReadBoolean() ? 32 : 64;
			FixedWallsHeight = reader.ReadInteger();

			var text = reader.ReadString();
			var rows = text.Split(new char[] { '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

			Width = rows.Max(x => x.Length);
			Height = rows.Length;

			_planes = new List<List<int>>(capacity: Height);
			for (var y = 0; y < Height; ++y)
			{
				var tmp = new List<int>(capacity: Width);
				for (var x = 0; x < Width; ++x)
				{
					tmp.Add(-110);
				}
				_planes.Add(tmp);
			}

			for (var i = 0; i < rows.Length; ++i)
			{
				var currentRow = rows[i];
				if (currentRow.Length > 0)
				{
					for (var n = 0; n < currentRow.Length; ++n)
					{
						var ch = currentRow[n];
						if (ch != 'x' && ch != 'X')
						{
							_planes[i][n] = (int)Base36.Decode(ch.ToString());
						}
					}
				}
			}
		}
	}
}
