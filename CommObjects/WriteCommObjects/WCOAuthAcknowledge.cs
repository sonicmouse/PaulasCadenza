using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOAuthAcknowledge : CommWriteObject
	{
		public override ushort SendType => 784;

		public override void Serialize(CommWriter writer) { }
	}
}
