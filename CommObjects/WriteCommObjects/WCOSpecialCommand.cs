using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOSpecialCommand : CommWriteObject
	{
		private readonly string _text;
		private readonly int _unknown1, _unknown2;

		public WCOSpecialCommand(string text, int unknown1 = 0, int unknown2 = -1)
		{
			_text = text;
			_unknown1 = unknown1;
			_unknown2 = unknown2;
		}

		public override ushort SendType => 3503;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_text);
			writer.WriteInteger(_unknown1);
			writer.WriteInteger(_unknown2);
		}
	}
}
