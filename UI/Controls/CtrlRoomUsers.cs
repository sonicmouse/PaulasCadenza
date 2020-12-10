using PaulasCadenza.BaseUI.Controls;
using PaulasCadenza.CommObjects.ReadCommObjects;
using PaulasCadenza.Models;
using System;
using System.Linq;

namespace PaulasCadenza.UI.Controls
{
	public partial class CtrlRoomUsers : CadenzaControl
	{
		private AccountModel _targetAccount;
		private readonly object _acctLock = new object();

		public sealed class DoubleClickEventArgs : EventArgs
		{
			public HabboUser HabboUser { get; set; }
		}

		public event EventHandler<DoubleClickEventArgs> DoubleClickList;

		public CtrlRoomUsers()
		{
			NetworkCommPublisher.Interface.NetworkCommReadObjectReceived
				+= OnNetworkCommReadObjectReceived;
			NetworkCommPublisher.Interface.Disconnected += OnDisconnected;
			NetworkCommPublisher.Interface.Authenticated += OnAuthenticated;

			InitializeComponent();
		}

		private void OnAuthenticated(object sender, EventArgs e)
		{
			lock(_acctLock)
			{
				if(_targetAccount == null)
				{
					_targetAccount = sender as AccountModel;
				}
			}
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			lock(_acctLock)
			{
				if (_targetAccount.Equals(sender as AccountModel))
				{
					_targetAccount = null;
				}
			}
		}

		private void OnNetworkCommReadObjectReceived(object sender, NetworkCommPublisher.NetworkCommEventEventArgs e)
		{
			lock(_acctLock)
			{
				if (_targetAccount == null)
				{
					_targetAccount = sender as AccountModel;
				}

				if(!_targetAccount.Equals(sender as AccountModel))
				{
					return;
				}
			}

			if (e.CommReadObject is RCORoomUsers usersJoin)
			{
				var usrs = CadenzaBots.Instance.GetRoomUsers(sender as AccountModel);
				usrs.AddUsers(usersJoin.Users);
				UpdateUsersList(sender as AccountModel, usrs);
			}
			else if (e.CommReadObject is RCORoomUserLeave userLeave)
			{
				var usrs = CadenzaBots.Instance.GetRoomUsers(sender as AccountModel);
				usrs.RemoveUser(userLeave.UserEntityId);
				UpdateUsersList(sender as AccountModel, usrs);
			}
			else if (e.CommReadObject is RCORoomUserDetailsUpdate detailsUpdate)
			{
				if (detailsUpdate.Details.EntityId != unchecked((uint)-1))
				{
					var usrs = CadenzaBots.Instance.GetRoomUsers(sender as AccountModel);
					usrs.UpdateUserDetails(detailsUpdate.Details);
					UpdateUsersList(sender as AccountModel, usrs);
				}
			}
			else if (e.CommReadObject is RCORoomUsersUpdate usersUpdate)
			{
				var usrs = CadenzaBots.Instance.GetRoomUsers(sender as AccountModel);
				usrs.UpdateUsers(usersUpdate.UpdatedUsers);
				UpdateUsersList(sender as AccountModel, usrs);
			}
		}

		private void UpdateUsersList(AccountModel acct, Data.RoomUsers ru)
		{
			Invoke(new Action(() =>
			{
				foreach (var u in ru.GetAllUsers().ToList()) // clone it
				{
					// find it in the list...
					if (!LstRoomUsers.Items.Cast<HabboUser>().Any(x => x.Equals(u)))
					{
						// doesn't exist, add them
						LstRoomUsers.Items.Add(u);
					}
					else
					{
						// exists, update them
						LstRoomUsers.Items[LstRoomUsers.Items.IndexOf(u)] = u;
					}
				}
				// remove all users from list that are no longer on
				LstRoomUsers.Items.Cast<HabboUser>().
					Where(x => !ru.GetAllUsers().Any(y => y.Equals(x))).
						ToList().ForEach(LstRoomUsers.Items.Remove);
			}));
		}

		private void LstRoomUsers_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(LstRoomUsers.SelectedIndex != -1)
			{
				DoubleClickList?.Invoke(this, new DoubleClickEventArgs
				{
					HabboUser = LstRoomUsers.SelectedItem as HabboUser
				});
			}
		}
	}
}
