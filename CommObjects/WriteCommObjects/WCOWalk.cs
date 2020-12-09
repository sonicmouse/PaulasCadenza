using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOWalk : CommWriteObject
	{
		private readonly int _x, _y;
		public WCOWalk(int x, int y)
		{
			_x = x;
			_y = y;
		}

		public override ushort SendType => 667;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteInteger(_x);
			writer.WriteInteger(_y);
		}
	}
}
