using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOSit : CommWriteObject
	{
		private readonly bool _sit;
		public WCOSit(bool sit)
		{
			_sit = sit;
		}

		public override ushort SendType => 2395;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteInteger(_sit ? 1 : 0);
		}
	}
}
