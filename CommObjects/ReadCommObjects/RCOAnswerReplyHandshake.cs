using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOAnswerReplyHandshake : CommReadObject
	{
		public string AnswerReply { get; private set; }
		public bool ServerEncrypted { get; private set; }

		public override ushort SendType => 2728;

		public override void Deserialize(CommReader reader)
		{
			AnswerReply = reader.ReadString();
			ServerEncrypted = reader.ReadBoolean();
		}
	}
}
