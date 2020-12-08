using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCORoomSubscribe : CommWriteObject
	{
		public override ushort SendType => 2137;

		public override void Serialize(CommWriter writer) { }
	}
}
