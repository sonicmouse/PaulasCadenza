using PaulasCadenza.BaseUI.Controls;
using PaulasCadenza.CommObjects.ReadCommObjects;
using PaulasCadenza.CommObjects.WriteCommObjects;
using PaulasCadenza.UI.Controls;
using PaulasCadenza.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

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
			LstUsers.RightClickList += OnRightClickList;

			CmbWalkType.Items.AddRange(Data.PlatoonSgt.GetCadences().ToArray());
			CmbWalkType.SelectedIndex = 0;
		}

		private void OnRightClickList(object sender, CtrlRoomUsers.RightClickEventArgs e)
		{
			if (MessageBox.Show(this,
					$"Give respect to {e.HabboUser.Name}?", "Confirm",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				CadenzaBots.Instance.WriteCommObjectAsync(new WCORespect(e.HabboUser.HabboId), CadenzaBots.WriteType.Selected);
			}
		}

		private void OnDoubleClickList(object sender, CtrlRoomUsers.DoubleClickEventArgs e)
		{
			TxtAvatar.Text = e.HabboUser.Figure;
			ChkAvatarMale.Checked = e.HabboUser.IsMale.GetValueOrDefault();
		}

		private void OnTileClicked(object sender, CtrlFloor.TileClickedEventArgs e)
		{
			var sel = CmbWalkType.SelectedItem as Data.Cadence;
			if(sel != null)
			{
				CadenzaBots.Instance.MoveToAsync(new Point(e.X, e.Y), sel.DeriveOffsets, CadenzaBots.WriteType.Selected);
			}
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
			Helpers.ActionsContextMenu.Create(this).Show(BtnActions, new Point(0, BtnActions.Height));
		}
	}
}
