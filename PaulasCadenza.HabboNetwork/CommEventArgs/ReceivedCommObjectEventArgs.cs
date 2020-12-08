using System;

namespace PaulasCadenza.HabboNetwork.CommEventArgs
{
	public sealed class ReceivedCommObjectEventArgs: EventArgs
	{
		public CommReadObject CommReadObject { get; set; }
	}
}
