using PaulasCadenza.BaseUI.Forms;
using PaulasCadenza.Models;
using System;

namespace PaulasCadenza.UI.Forms
{
	public partial class FormAccount : FormCadenza
	{
		public AccountModel Account { get; set; }

		public FormAccount()
		{
			InitializeComponent();
			MinimumSize = Size;

			TxtEmail.Text = Account?.Email;
			TxtPassword.Text = Account?.PlainTextPassword;
			CmbHotel.SelectedIndex = 0;
		}

		private void BtnOK_Click(object sender, System.EventArgs e)
		{
			Account = new AccountModel
			{
				Email = TxtEmail.Text,
				PlainTextPassword = TxtPassword.Text,
				Hotel = new Uri((string)CmbHotel.SelectedItem)
			};
		}
	}
}
