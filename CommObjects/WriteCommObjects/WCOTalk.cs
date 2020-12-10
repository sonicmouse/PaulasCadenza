using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOTalk : CommWriteObject
	{
		private readonly string _text;
		private readonly int _style;
		private readonly int _unknown;

		public WCOTalk(string text, int style = 0, int unknown = -1)
		{
			_text = text ?? string.Empty;
			_style = style;
			_unknown = unknown;
		}

		public override ushort SendType => 3503;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_text);
			writer.WriteInteger(_style);
			writer.WriteInteger(_unknown);
		}
	}
}
