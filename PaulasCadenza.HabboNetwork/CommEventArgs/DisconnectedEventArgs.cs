using System;

namespace PaulasCadenza.HabboNetwork.CommEventArgs
{
	public sealed class DisconnectedEventArgs : EventArgs
	{
		public Exception Exception { get; set; }
	}
}
