using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.Models;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCORoomUserDetailsUpdate : CommReadObject
	{
		public RoomUserDetails Details { get; private set; }

		public override ushort SendType => 1615;

		public override void Deserialize(CommReader reader)
		{
			Details = new RoomUserDetails
			{
				EntityId = reader.ReadUnsignedInteger(),
				Avatar = reader.ReadString(),
				IsMale = reader.ReadString().ToUpper() == "M",
				Custom = reader.ReadString(),
				Score = reader.ReadInteger()
			};
		}
	}
}
