using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOInitHandshake : CommReadObject
	{
		public string Left { get; private set; }
		public string Right { get; private set; }

		public override ushort SendType => 3821;

		public override void Deserialize(CommReader reader)
		{
			Left = reader.ReadString();
			Right = reader.ReadString();
		}
	}
}
