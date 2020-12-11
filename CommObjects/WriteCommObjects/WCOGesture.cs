using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOGesture : CommWriteObject
	{
		private readonly int _index;
		public WCOGesture(int index)
		{
			_index = index;
		}

		public override ushort SendType => 3696;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteInteger(_index);
		}
	}
}
