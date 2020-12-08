using System;

namespace PaulasCadenza.HabboNetwork.CommEventArgs
{
	public sealed class SentCommObjectEventArgs : EventArgs
	{
		public object Tag { get; set; }
	}
}
