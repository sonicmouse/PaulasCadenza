using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOEnteredRoom : CommReadObject
	{
		public uint RoomID { get; private set; }

		public override ushort SendType => 97;

		public override void Deserialize(CommReader reader)
		{
			RoomID = reader.ReadUnsignedInteger();
		}
	}
}
