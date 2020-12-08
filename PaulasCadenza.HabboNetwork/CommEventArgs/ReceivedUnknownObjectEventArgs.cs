using System;

namespace PaulasCadenza.HabboNetwork.CommEventArgs
{
	public sealed class ReceivedUnknownObjectEventArgs : EventArgs
	{
		public ushort SendType { get; set; }
		public byte[] Data { get; set; }
		public string HexDump { get; set; }
	}
}
