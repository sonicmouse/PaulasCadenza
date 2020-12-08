﻿using PaulasCadenza.BaseUI.Forms;
using PaulasCadenza.Data;
using PaulasCadenza.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PaulasCadenza.UI.Forms
{
	public partial class FormMain : FormCadenza
	{
		public FormMain()
		{
			NetworkCommPublisher.Interface.Disconnected += OnBotDisconnected;
			NetworkCommPublisher.Interface.Authenticated += OnBotAuthenticated;

			InitializeComponent();
			UpdateAccountList();
			LstViewAccounts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			LstViewAccounts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		private void OnBotAuthenticated(object sender, EventArgs e)
		{
			//
		}

		private void OnBotDisconnected(object sender, EventArgs e)
		{
			//
		}

		private void LstViewAccounts_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if(e.Item.Tag is AccountModel acct)
			{
				CadenzaBots.Instance.SelectBot(acct, e.Item.Checked);
			}
		}

		private void UpdateAccountList()
		{
			LstViewAccounts.BeginUpdate();
			try
			{
				foreach (var acct in Accounts.GetAccounts())
				{
					var lstItem = LstViewAccounts.Items.Cast<ListViewItem>().
						SingleOrDefault(x => ((AccountModel)x.Tag).Equals(acct));
					if (lstItem == null)
					{
						lstItem = new ListViewItem(acct.Email) { Tag = acct, ImageIndex = 1 };
						lstItem.SubItems.Add(acct.Hotel.ToString());
						LstViewAccounts.Items.Add(lstItem);
					}
					else
					{
						lstItem.Text = acct.Email;
						lstItem.SubItems[1].Text = acct.Hotel.ToString();
						lstItem.Tag = acct;
					}
				}
				LstViewAccounts.Items.Cast<ListViewItem>().
					Where(x => !Accounts.GetAccounts().Any(y => y.Equals(x.Tag as AccountModel))).
					ToList().ForEach(LstViewAccounts.Items.Remove);
			}
			finally
			{
				LstViewAccounts.EndUpdate();
			}
		}

		private void TSMIExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void TSMIAddAccount_Click(object sender, EventArgs e)
		{
			using (var frm = new FormAccount())
			{
				if (frm.ShowDialog() == DialogResult.OK)
				{
					Accounts.AddAccount(frm.Account);
					UpdateAccountList();
				}
			}
		}

		private ListViewItem GetSelectedItem() =>
			LstViewAccounts.SelectedItems.Count > 0 ? LstViewAccounts.SelectedItems[0] : null;

		private void TSMIRemoveAccount_Click(object sender, EventArgs e)
		{
			var itm = GetSelectedItem();
			if((itm != null) && (itm.Tag is AccountModel acct))
			{
				if (MessageBox.Show(this, $"Are you sure you want to remove the account?\n\n{acct}", "Confirm",
						MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					Accounts.RemoveAccount(acct);
				}
				UpdateAccountList();
			}
		}

		private void BtnConnect_Click(object sender, EventArgs e)
		{
			var itm = GetSelectedItem();
			if ((itm != null) && (itm.Tag is AccountModel acct))
			{
				CadenzaBots.Instance.Connect(this, acct, itm.Checked);
			}
		}

		private void LstViewAccounts_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			BtnConnect.PerformClick();
		}

		private void BtnDisconnect_Click(object sender, EventArgs e)
		{
			var itm = GetSelectedItem();
			if ((itm != null) && (itm.Tag is AccountModel acct))
			{
				CadenzaBots.Instance.Disconnect(acct);
			}
		}
	}
}