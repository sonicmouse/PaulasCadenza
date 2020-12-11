using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCORoomUserLeave : CommReadObject
	{
		public uint UserEntityId { get; private set; }

		public override ushort SendType => 3455;

		public override void Deserialize(CommReader reader)
		{
			UserEntityId = uint.Parse(reader.ReadString());
		}
	}
}
