using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.Utilities;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOAuthenticate : CommWriteObject
	{
		private readonly string _ssoTicket;
		public WCOAuthenticate(string ssoTicket)
		{
			_ssoTicket = ssoTicket;
		}

		public override ushort SendType => 2571;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_ssoTicket);
			writer.WriteInteger(PRNG.Instance.Next(12000, 25000));
		}
	}
}
