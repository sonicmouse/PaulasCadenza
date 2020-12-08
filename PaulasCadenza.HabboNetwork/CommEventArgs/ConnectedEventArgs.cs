using System;

namespace PaulasCadenza.HabboNetwork.CommEventArgs
{
	public sealed class ConnectedEventArgs : EventArgs
	{
		public string Host { get; set; }
		public int Port { get; set; }
	}
}
