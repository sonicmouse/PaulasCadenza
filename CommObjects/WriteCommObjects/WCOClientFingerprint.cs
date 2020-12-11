using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.Utilities;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOClientFingerprint : CommWriteObject
	{
		public override ushort SendType => 135;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(PRNG.Instance.HexString(38)); // machine ID
			writer.WriteString(PRNG.Instance.HexString(16)); // fingerprint
			writer.WriteString("WIN/17,0,0,134"); // flash version
		}
	}
}
