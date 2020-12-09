using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOHoldSign : CommWriteObject
	{
		private readonly int _sign;

		public WCOHoldSign(int sign)
		{
			_sign = sign;
		}

		public override ushort SendType => 3164;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteInteger(_sign);
		}
	}
}
