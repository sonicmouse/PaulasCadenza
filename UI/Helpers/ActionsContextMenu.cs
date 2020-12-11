using PaulasCadenza.CommObjects.WriteCommObjects;
using PaulasCadenza.UI.Forms;
using System.Windows.Forms;

namespace PaulasCadenza.UI.Helpers
{
	public static class ActionsContextMenu
	{
		private static System.Threading.Timer _tLookAround;
		private static int _lookAt;

		private static void LookAround(bool start, int interval = 500)
		{
			_tLookAround?.Dispose();
			if (start)
			{
				_lookAt = 0;
				_tLookAround = new System.Threading.Timer(_ =>
				{
					CadenzaBots.Instance.LookAtAsync(_lookAt, CadenzaBots.WriteType.Selected);
					if (++_lookAt > 7) { _lookAt = 0; }
				}, null, 0, interval);
			}
		}

		public static ContextMenu Create(IWin32Window owner)
		{
			void Dance(int index)
			{
				CadenzaBots.Instance.WriteCommObjectAsync(
					new WCODance(index), CadenzaBots.WriteType.Selected);
			}
			var menuDance = new MenuItem("Dance");
			menuDance.MenuItems.AddRange(new MenuItem[]
			{
				new MenuItem("Start", (s, e) => Dance(1)),
				new MenuItem("Stop", (s, e) => Dance(0))
			});

			void Gesture(int gest)
			{
				CadenzaBots.Instance.WriteCommObjectAsync(
					new WCOGesture(gest), CadenzaBots.WriteType.Selected);
			}
			void Sit(bool sit)
			{
				CadenzaBots.Instance.WriteCommObjectAsync(
					new WCOSit(sit), CadenzaBots.WriteType.Selected);
			}
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

			void Sign(int? sign)
			{
				CadenzaBots.Instance.ShowSignAsync(sign, CadenzaBots.WriteType.Selected);
			}
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

			void Bubble(bool on)
			{
				CadenzaBots.Instance.WriteCommObjectAsync(
					new WCOTalkBubble(on), CadenzaBots.WriteType.Selected);
			}
			var menuChatBubble = new MenuItem("Chat Bubble");
			menuChatBubble.MenuItems.AddRange(new MenuItem[]
			{
				new MenuItem("On", (s, e) => Bubble(true)),
				new MenuItem("Off", (s, e) => Bubble(false))
			});

			var menuDropItem = new MenuItem("Drop Item", (s, e)
				=> CadenzaBots.Instance.WriteCommObjectAsync(
					new WCODropCarryItem(), CadenzaBots.WriteType.Selected));

			var menuDizzy = new MenuItem("Dizzy");
			menuDizzy.MenuItems.AddRange(new MenuItem[]
			{
				new MenuItem("Start", (s, e) => LookAround(true)),
				new MenuItem("Stop", (s,e) => LookAround(false))
			});

			var menuMotto = new MenuItem("Set Motto", (s, e) =>
			{
				using (var frm = new FormMotto())
				{
					if(frm.ShowDialog(owner) == DialogResult.OK)
					{
						CadenzaBots.Instance.WriteCommObjectAsync(new WCOMotto(frm.Motto), CadenzaBots.WriteType.Selected);
					}
				}
			});

			return new ContextMenu(new MenuItem[]
			{
				menuDance,
				menuActions,
				menuSigns,
				menuChatBubble,
				menuDropItem,
				menuDizzy,
				menuMotto
			});
		}
	}
}
