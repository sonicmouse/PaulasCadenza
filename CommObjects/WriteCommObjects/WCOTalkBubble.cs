using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOTalkBubble : CommWriteObject
	{
		private readonly bool _on;
		public WCOTalkBubble(bool on)
		{
			_on = on;
		}

		public override ushort SendType => (ushort)(_on ? 3199 : 1497);

		public override void Serialize(CommWriter writer) { }
	}
}
