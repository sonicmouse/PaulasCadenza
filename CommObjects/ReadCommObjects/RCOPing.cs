﻿using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOPing : CommReadObject
	{
		public override ushort SendType => 275;

		public override void Deserialize(CommReader reader) { }
	}
}
