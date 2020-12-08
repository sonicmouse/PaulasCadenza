using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOClientVars : CommWriteObject
	{
		private readonly int _unknown;
		private readonly string _flashClientUrl, _externalVariablesText;

		public WCOClientVars(int unknown, string flashClientUrl, string externalVariablesText)
		{
			_unknown = unknown;
			_flashClientUrl = flashClientUrl;
			_externalVariablesText = externalVariablesText;
		}

		public override ushort SendType => 1981;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteInteger(_unknown);
			writer.WriteString(_flashClientUrl);
			writer.WriteString(_externalVariablesText);
		}
	}
}
