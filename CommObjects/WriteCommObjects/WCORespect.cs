using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCORespect : CommWriteObject
	{
		private readonly uint _habboId;

		public WCORespect(uint habboId)
		{
			_habboId = habboId;
		}

		public override ushort SendType => 1875;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteUnsignedInteger(_habboId);
		}
	}
}
