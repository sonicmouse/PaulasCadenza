using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCORequestRoomCategories : CommWriteObject
	{
		public override ushort SendType => 3971;

		public override void Serialize(CommWriter writer) { }
	}
}
