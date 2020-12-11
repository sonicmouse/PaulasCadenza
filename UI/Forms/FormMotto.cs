using PaulasCadenza.BaseUI.Forms;

namespace PaulasCadenza.UI.Forms
{
	public partial class FormMotto : FormCadenza
	{
		public string Motto { get; set; }

		public FormMotto()
		{
			InitializeComponent();
			MinimumSize = Size;
			TxtMotto.Text = Motto;
		}

		private void BtnOK_Click(object sender, System.EventArgs e)
		{
			Motto = TxtMotto.Text.Replace("\r\n", "\n");
		}
	}
}
