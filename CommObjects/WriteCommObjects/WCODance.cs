using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCODance : CommWriteObject
	{
		private readonly int _index;
		public WCODance(int index)
		{
			_index = index;
		}

		public override ushort SendType => 2842;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteInteger(_index);
		}
	}
}
