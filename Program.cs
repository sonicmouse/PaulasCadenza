using PaulasCadenza.Data;
using PaulasCadenza.UI.Forms;
using System;
using System.Windows.Forms;

namespace PaulasCadenza
{
	static class Program
	{

#if DEBUG
		static Program()
		{
			System.Diagnostics.Debug.Assert(HabboDHM.DHM.TestCase(), "HabboDHM.DHM.TestCase() failed");
			System.Diagnostics.Debug.Assert(HabboDHM.RC4.TestCase(), "HabboDHM.RC4.TestCase() failed");
		}
#endif

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			CEFSettings.SetupGlobalSettings();
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				using (var frm = new FormMain())
				{
					Application.Run(frm);
				}
			}
			finally
			{
				CEFSettings.TeardownGlobalSettings();
			}
		}
	}
}
