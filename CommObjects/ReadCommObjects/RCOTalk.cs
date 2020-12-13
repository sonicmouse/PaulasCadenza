using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOTalk : CommReadObject
	{
		public uint EntityId { get; private set; }
		public string Text { get; private set; }
		public int Gesture { get; private set; }
		public int StyleId { get; private set; }

		public override ushort SendType => 1026;

		public override void Deserialize(CommReader reader)
		{
			EntityId = reader.ReadUnsignedInteger();
			Text = reader.ReadString();
			Gesture = reader.ReadInteger();
			StyleId = reader.ReadInteger();
			var countLinks = reader.ReadInteger();
			for (var i = 0; i < countLinks; ++i)
			{
				_ = reader.ReadString();
				_ = reader.ReadString();
				_ = reader.ReadBoolean();
			}
			_ = reader.ReadInteger();
		}
	}
}
