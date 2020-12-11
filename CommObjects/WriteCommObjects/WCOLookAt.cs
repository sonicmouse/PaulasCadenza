using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOLookAt : CommWriteObject
	{
		private readonly int _x, _y;
		public WCOLookAt(int x, int y)
		{
			_x = x;
			_y = y;
		}

		public override ushort SendType => 664;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteInteger(_x);
			writer.WriteInteger(_y);
		}
	}
}
