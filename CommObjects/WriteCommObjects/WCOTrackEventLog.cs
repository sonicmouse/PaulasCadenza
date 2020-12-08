using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOTrackEventLog : CommWriteObject
	{
		private readonly string _param1, _param2, _param3, _param4;
		private readonly int _param5;

		public WCOTrackEventLog(
			string param1, string param2, string param3,
			string param4 = null, int param5 = 0)
		{
			_param1 = param1;
			_param2 = param2;
			_param3 = param3;
			_param4 = param4;
			_param5 = param5;
		}

		public override ushort SendType => 186;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_param1);
			writer.WriteString(_param2);
			writer.WriteString(_param3);
			writer.WriteString(_param4);
			writer.WriteInteger(_param5);
		}
	}
}
