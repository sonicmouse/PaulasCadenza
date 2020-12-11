using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCORoomSubscribe : CommWriteObject
	{
		public override ushort SendType => 1760;

		public override void Serialize(CommWriter writer) { }
	}
}
