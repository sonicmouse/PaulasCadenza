using PaulasCadenza.BaseUI.Controls;
using PaulasCadenza.CommObjects.ReadCommObjects;

namespace PaulasCadenza.UI.Pages
{
	public partial class PageCtrlRoomActions : CadenzaControl
	{
		public PageCtrlRoomActions()
		{
			NetworkCommPublisher.Interface.NetworkCommReadObjectReceived +=
				OnNetworkCommReadObjectReceived;

			InitializeComponent();
		}

		private void OnNetworkCommReadObjectReceived(object sender,
			NetworkCommPublisher.NetworkCommEventEventArgs e)
		{
			if(e.CommReadObject is RCOHeightMap heightMap)
			{
				// ignore for now
			}
			else if(e.CommReadObject is RCOFloorHeightMap floorHeightMap)
			{
				for (var x = 0; x < floorHeightMap.Width; ++x)
				{
					for(var y = 0; y < floorHeightMap.Height; ++y)
					{
						var v = floorHeightMap.GetFloorHeightAtPoint(x, y);
						System.Console.Write($"{(v >= 0 ? v.ToString() : " ")}");
					}
					System.Console.WriteLine();
				}
			}
		}
	}
}
