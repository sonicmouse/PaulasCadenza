using PaulasCadenza.BaseUI.Controls;
using PaulasCadenza.CommObjects.ReadCommObjects;
using PaulasCadenza.CommObjects.WriteCommObjects;
using PaulasCadenza.Models;
using System;
using System.Linq;

namespace PaulasCadenza.UI.Pages
{
	public partial class PageCtrlRoomSearch : CadenzaControl
	{
		private bool _searching;

		private sealed class SearchTypeItem
		{
			public string Display { get; set; }
			public string Value { get; set; }
			public override string ToString()
			{
				return Display;
			}
		}

		public PageCtrlRoomSearch()
		{
			NetworkCommPublisher.Interface.NetworkCommReadObjectReceived +=
				OnNetworkCommReadObjectReceived;

			NetworkCommPublisher.Interface.Authenticated += (s, e)
				=> CadenzaBots.Instance.WriteCommObjectAsync(
					new WCORequestRoomCategories(), CadenzaBots.WriteType.Random);

			InitializeComponent();

			CmbSearchFilter.Items.AddRange(new object[]
			{
				new SearchTypeItem { Display = "Anything", Value = "" },
				new SearchTypeItem { Display = "Owner", Value = "owner:" },
				new SearchTypeItem { Display = "Room Name", Value = "roomname:" },
				new SearchTypeItem { Display = "Tag", Value = "tag:" },
				new SearchTypeItem { Display = "Group", Value = "group:" }
			});
			CmbSearchFilter.SelectedIndex = 0;
		}

		private void OnNetworkCommReadObjectReceived(object sender,
			NetworkCommPublisher.NetworkCommEventEventArgs e)
		{
			if(_searching && (e.CommReadObject is RCOSearchRoomsResult srr))
			{
				_searching = false;

				Invoke(new Action(() =>
				{
					LstRooms.Items.Clear();
					LstRooms.Items.AddRange(srr.Rooms.ToArray());
				}));
			}
			else if(e.CommReadObject is RCONavigatorMetaData metadata)
			{
				Invoke(new Action(() =>
				{
					var existing = CmbSearchType.Items.Cast<string>();
					CmbSearchType.Items.AddRange(metadata.MetaData.Where(x => existing.All(y => y != x)).ToArray());
					if(CmbSearchType.SelectedIndex == -1)
					{
						CmbSearchType.SelectedItem = "hotel_view";
					}
				}));
			}
		}

		private void TxtSearch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				var st = (SearchTypeItem)CmbSearchFilter.SelectedItem;

				_searching = true;
				CadenzaBots.Instance.WriteCommObjectAsync(
					new WCOSearchRooms((string)CmbSearchType.SelectedItem, $"{st.Value}{TxtSearch.Text}"),
					CadenzaBots.WriteType.Random);

				e.SuppressKeyPress = true;
			}
		}

		private void LstRooms_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			BtnEnter.PerformClick();
		}

		private void BtnLeaveRoom_Click(object sender, EventArgs e)
		{
			CadenzaBots.Instance.WriteCommObjectAsync(new WCOLeaveRoom(), CadenzaBots.WriteType.Selected);
		}

		private void BtnEnter_Click(object sender, EventArgs e)
		{
			if (LstRooms.SelectedIndex != -1)
			{
				var room = (SearchedRoomModel)LstRooms.SelectedItem;

				CadenzaBots.Instance.WriteCommObjectAsync(new WCOGotoRoom(room.RoomId), CadenzaBots.WriteType.Selected);
			}
		}
	}
}
