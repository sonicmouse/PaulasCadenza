using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOLeaveRoom : CommWriteObject
	{
		public override ushort SendType => 803;

		public override void Serialize(CommWriter writer) { }
	}
}
