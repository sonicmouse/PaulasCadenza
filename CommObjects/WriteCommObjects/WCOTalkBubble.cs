using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOTalkBubble : CommWriteObject
	{
		private readonly bool _on;
		public WCOTalkBubble(bool on)
		{
			_on = on;
		}

		public override ushort SendType => (ushort)(_on ? 1840 : 3308);

		public override void Serialize(CommWriter writer) { }
	}
}
