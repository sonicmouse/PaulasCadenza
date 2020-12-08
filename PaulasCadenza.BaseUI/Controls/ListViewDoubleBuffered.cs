using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PaulasCadenza.BaseUI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ListView))]
	public class ListViewDoubleBuffered : ListView
	{
		public ListViewDoubleBuffered()
		{
			DoubleBuffered = true;
		}
	}
}
