using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOAuthSuccess : CommReadObject
	{
		public override ushort SendType => 2434;

		public override void Deserialize(CommReader reader) { }
	}
}
