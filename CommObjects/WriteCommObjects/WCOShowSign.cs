using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOShowSign : CommWriteObject
	{
		private readonly int _sign;

		public WCOShowSign(int sign)
		{
			_sign = sign;
		}

		public override ushort SendType => 1780;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteInteger(_sign);
		}
	}
}
