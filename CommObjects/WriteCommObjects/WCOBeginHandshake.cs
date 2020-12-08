using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.HabboNetwork.Models;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOBeginHandshake : CommWriteObject, ISendMessageNeverEncrypt
	{
		public override ushort SendType => 1590;

		public override void Serialize(CommWriter writer) { }
	}
}
