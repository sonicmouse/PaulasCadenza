using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public class WCOGotoRoom : CommWriteObject
	{
		private readonly uint _roomId;
		private readonly string _unknown1;
		private readonly int _unknown2;

		public WCOGotoRoom(uint roomId, string unknown1 = null, int unknown2 = -1)
		{
			_roomId = roomId;
			_unknown1 = unknown1;
			_unknown2 = unknown2;
		}

		public override ushort SendType => 1885;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteUnsignedInteger(_roomId);
			writer.WriteString(_unknown1);
			writer.WriteInteger(_unknown2);
		}
	}
}
