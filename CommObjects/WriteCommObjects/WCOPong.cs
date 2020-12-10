using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOPong : CommWriteObject
	{
		public override ushort SendType => 1914;

		public override void Serialize(CommWriter writer) { }
	}
}
