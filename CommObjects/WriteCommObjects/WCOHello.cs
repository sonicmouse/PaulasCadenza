using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.HabboNetwork.Models;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOHello : CommWriteObject, ISendMessageNeverEncrypt
	{
		public override ushort SendType => 4000;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString("PRODUCTION-202012092202-806932021");
			writer.WriteString("FLASH");
			writer.WriteInteger(1);
			writer.WriteInteger(0);
		}
	}
}
