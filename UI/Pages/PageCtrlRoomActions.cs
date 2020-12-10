using PaulasCadenza.BaseUI.Controls;
using PaulasCadenza.CommObjects.ReadCommObjects;
using PaulasCadenza.CommObjects.WriteCommObjects;
using PaulasCadenza.UI.Controls;
using PaulasCadenza.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaulasCadenza.UI.Pages
{
	public partial class PageCtrlRoomActions : CadenzaControl
	{
		public PageCtrlRoomActions()
		{
			NetworkCommPublisher.Interface.NetworkCommReadObjectReceived +=
				OnNetworkCommReadObjectReceived;

			InitializeComponent();

			CtlFloor.TileClicked += OnTileClicked;
			LstUsers.DoubleClickList += OnDoubleClickList;
		}

		private void OnDoubleClickList(object sender, CtrlRoomUsers.DoubleClickEventArgs e)
		{
			TxtAvatar.Text = e.HabboUser.Figure;
			ChkAvatarMale.Checked = e.HabboUser.IsMale.GetValueOrDefault();
		}

		private void OnTileClicked(object sender, Controls.CtrlFloor.TileClickedEventArgs e)
		{
			CadenzaBots.Instance.WriteCommObjectAsync(new WCOWalk(e.X, e.Y), CadenzaBots.WriteType.Selected);
		}

		private void OnNetworkCommReadObjectReceived(object sender,
			NetworkCommPublisher.NetworkCommEventEventArgs e)
		{
			if(e.CommReadObject is RCOHeightMap heightMap)
			{
				Invoke(new Action(() => CtlFloor.HeightMap = heightMap));
			}
		}

		private void TxtTalk_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				if(ChkShout.Checked)
				{
					CadenzaBots.Instance.WriteCommObjectAsync(
						new WCOShout(TxtTalk.Text, PRNG.Instance.Next(6)), CadenzaBots.WriteType.Selected);
				}
				else
				{
					CadenzaBots.Instance.WriteCommObjectAsync(
						new WCOTalk(TxtTalk.Text, PRNG.Instance.Next(6)), CadenzaBots.WriteType.Selected);
				}
				TxtTalk.Text = string.Empty;
			}
		}

		private void TxtAvatar_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				if(TxtAvatar.Text.Length > 0)
				{
					CadenzaBots.Instance.WriteCommObjectAsync(
						new WCOAvatar(ChkAvatarMale.Checked, TxtAvatar.Text), CadenzaBots.WriteType.Selected);
				}
			}
		}

		private void BtnRndHLAvatar_Click(object sender, EventArgs e)
		{
			CadenzaBots.Instance.AssignRandomAvatarAsync(true, CadenzaBots.WriteType.Selected);

			TxtAvatar.Text = CadenzaBots.Instance.GetRandomAvatar(true, out bool isMale);
			ChkAvatarMale.Checked = isMale;
		}

		private void BtnRndAvatar_Click(object sender, EventArgs e)
		{
			CadenzaBots.Instance.AssignRandomAvatarAsync(false, CadenzaBots.WriteType.Selected);
			
			TxtAvatar.Text = CadenzaBots.Instance.GetRandomAvatar(false, out bool isMale);
			ChkAvatarMale.Checked = isMale;
		}

		private void BtnActions_Click(object sender, EventArgs args)
		{
			void Dance(int index) { CadenzaBots.Instance.WriteCommObjectAsync(
				new WCODance(index), CadenzaBots.WriteType.Selected); }
			var menuDance = new MenuItem("Dance");
			menuDance.MenuItems.AddRange(new MenuItem[]
			{
				new MenuItem("Start", (s, e) => Dance(1)),
				new MenuItem("Stop", (s, e) => Dance(0))
			});

			void Gesture(int gest) { CadenzaBots.Instance.WriteCommObjectAsync(
				new WCOGesture(gest), CadenzaBots.WriteType.Selected); }
			void Sit(bool sit) { CadenzaBots.Instance.WriteCommObjectAsync(
				new WCOSit(sit), CadenzaBots.WriteType.Selected); }
			var menuActions = new MenuItem("Actions");
			menuActions.MenuItems.AddRange(new MenuItem[]
			{
				new MenuItem("Sit", (s, e) => Sit(true)),
				new MenuItem("Stand", (s, e) => Sit(false)),
				new MenuItem("-"),
				new MenuItem("Wave", (s, e) => Gesture(1)),
				new MenuItem("Idle", (s, e) => Gesture(5)),
				new MenuItem("Gang Sign", (s, e) => Gesture(7))
			});

			void Sign(int? sign) { CadenzaBots.Instance.ShowSignAsync(sign, CadenzaBots.WriteType.Selected); }
			var menuSigns = new MenuItem("Signs");
			menuSigns.MenuItems.AddRange(new MenuItem[]
			{
				new MenuItem("Random", (s, e) => Sign(null)), new MenuItem("-"),
				new MenuItem("0", (s, e) => Sign(0)), new MenuItem("1", (s, e) => Sign(1)),
				new MenuItem("2", (s, e) => Sign(2)), new MenuItem("3", (s, e) => Sign(3)),
				new MenuItem("4", (s, e) => Sign(4)), new MenuItem("5", (s, e) => Sign(5)),
				new MenuItem("6", (s, e) => Sign(6)), new MenuItem("7", (s, e) => Sign(7)),
				new MenuItem("8", (s, e) => Sign(8)), new MenuItem("9", (s, e) => Sign(9)),
				new MenuItem("10", (s, e) => Sign(10)), new MenuItem("Heart", (s, e) => Sign(11)),
				new MenuItem("Skull", (s, e) => Sign(12)), new MenuItem("Exclaim", (s, e) => Sign(13)),
				new MenuItem("Ball", (s, e) => Sign(14)), new MenuItem("Smile", (s, e) => Sign(15)),
				new MenuItem("Red Card", (s, e) => Sign(16)), new MenuItem("Yellow Card", (s, e) => Sign(17))
			});

			void Bubble(bool on) { CadenzaBots.Instance.WriteCommObjectAsync(
				new WCOTalkBubble(on), CadenzaBots.WriteType.Selected); }
			var menuChatBubble = new MenuItem("Chat Bubble");
			menuChatBubble.MenuItems.AddRange(new MenuItem[]
			{
				new MenuItem("On", (s, e) => Bubble(true)),
				new MenuItem("Off", (s, e) => Bubble(false))
			});

			var menuDropItem = new MenuItem("Drop Item", (s, e)
				=> CadenzaBots.Instance.WriteCommObjectAsync(
					new WCODropCarryItem(), CadenzaBots.WriteType.Selected));

			new ContextMenu(new MenuItem[]
			{
				menuDance,
				menuActions,
				menuSigns,
				menuChatBubble,
				menuDropItem
			}).Show(BtnActions, new Point(0, BtnActions.Size.Height));
		}
	}
}
