using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOMotto : CommWriteObject
	{
		private readonly string _motto;

		public WCOMotto(string motto)
		{
			_motto = motto;
		}

		public override ushort SendType => 955;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_motto);
		}
	}
}
