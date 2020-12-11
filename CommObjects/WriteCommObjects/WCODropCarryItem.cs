using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCODropCarryItem : CommWriteObject
	{
		public override ushort SendType => 3002;

		public override void Serialize(CommWriter writer)
		{
		}
	}
}
