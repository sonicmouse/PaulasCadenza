using PaulasCadenza.BaseUI.Forms;
using PaulasCadenza.Data;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PaulasCadenza.UI.Forms
{
	public partial class FormAbout : FormCadenza
	{
		public FormAbout()
		{
			InitializeComponent();

			LblTitle.Font = new Font(LblTitle.Font, FontStyle.Bold);
			LblVersion.Text =
				$"v{FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion} " +
				$"({(IntPtr.Size == 8 ? "x64" : "x86")})";
		}

		private void LnkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			using (var p = Process.Start(new ProcessStartInfo
			{
				UseShellExecute = true,
				FileName = "https://github.com/sonicmouse/PaulasCadenza"
			})) { };
		}

		private async void FormAbout_Load(object sender, EventArgs e)
		{
			LblBuildDate.Text =
				await Resources.GetResourceAsStringAsync("PaulasCadenza.Resources.BuildDate.txt");
		}
	}
}
