using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOPong : CommWriteObject
	{
		public override ushort SendType => 2091;

		public override void Serialize(CommWriter writer) { }
	}
}
