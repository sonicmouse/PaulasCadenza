using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOShout : CommWriteObject
	{
		private readonly string _text;
		private readonly int _style;

		public WCOShout(string text, int style = 0)
		{
			_text = text ?? string.Empty;
			_style = style;
		}

		public override ushort SendType => 1405;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_text);
			writer.WriteInteger(_style);
		}
	}
}
