using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.HabboNetwork.Models;


namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOAnswerHandshake : CommWriteObject, ISendMessageNeverEncrypt
	{
		private readonly string _answer;

		public WCOAnswerHandshake(string answer)
		{
			_answer = answer;
		}

		public override ushort SendType => 3873;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_answer);
		}
	}
}
