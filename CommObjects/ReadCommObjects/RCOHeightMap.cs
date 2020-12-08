using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOHeightMap : CommReadObject
	{
		private short[] _values;

		public int Width { get; private set; }
		public int Height { get; private set; }
		public override ushort SendType => 1918;

		public override void Deserialize(CommReader reader)
		{
			Width = reader.ReadInteger();
			var totalValues = reader.ReadInteger();
			Height = totalValues / Width;

			_values = new short[totalValues];
			for (var i = 0; i < totalValues; ++i)
			{
				_values[i] = reader.ReadShort();
			}
		}

		private static readonly int UNKNOWN_FLAG = 0x4000;
		private const int HEIGHT_MASK = 0x3FFF;

		private static double ConvertTileValueToHeight(int value)
		{
			return (value < 0) ? -1.0 : ((value & HEIGHT_MASK) / 256.0);
		}

		private static bool IsUnknownFlag(int k)
		{
			return (k & UNKNOWN_FLAG) == k;
		}

		private static bool IsValidTileValue(int k)
		{
			return k >= 0;
		}

		public double GetHeightAtPoint(int x, int y)
		{
			if ((x < 0) || (x >= Width) || (y < 0) || (y >= Height))
			{
				return -1.0;
			}
			return ConvertTileValueToHeight(_values[(y * Width) + x]);
		}

		public bool IsUnknownFlagAtPoint(int x, int y)
		{
			if ((x < 0) || (x >= Width) || (y < 0) || (y >= Height))
			{
				return true;
			};
			return IsUnknownFlag(_values[(y * Width) + x]);
		}

		public bool IsValidTileAtPoint(int x, int y)
		{
			if ((x < 0) || (x >= Width) || (y < 0) || (y >= Height))
			{
				return false;
			};
			return IsValidTileValue(_values[(y * Width) + x]);
		}
	}
}
