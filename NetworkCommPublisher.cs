using PaulasCadenza.HabboNetwork;
using PaulasCadenza.Models;
using System;

namespace PaulasCadenza
{
	public sealed class NetworkCommPublisher
	{
		public sealed class NetworkCommEventEventArgs : EventArgs
		{
			public NetworkCommEventEventArgs(CommReadObject cro) { CommReadObject = cro; }
			public CommReadObject CommReadObject { get; }
		}

		public event EventHandler Connected;
		public event EventHandler Authenticated;
		public event EventHandler Disconnected;
		public event EventHandler<NetworkCommEventEventArgs> NetworkCommReadObjectReceived;

		public static NetworkCommPublisher Interface { get; } = new NetworkCommPublisher();
		private NetworkCommPublisher() { }

		public void PublishCommReadObject(AccountModel acct, CommReadObject cro)
		{
			NetworkCommReadObjectReceived?.Invoke(acct, new NetworkCommEventEventArgs(cro));
		}
		
		public void PublishConnected(AccountModel acct)
		{
			Connected?.Invoke(acct, EventArgs.Empty);
		}

		public void PublishAuthenticated(AccountModel acct)
		{
			Authenticated?.Invoke(acct, EventArgs.Empty);
		}

		public void PublishDisconnected(AccountModel acct)
		{
			Disconnected?.Invoke(acct, EventArgs.Empty);
		}
	}
}
