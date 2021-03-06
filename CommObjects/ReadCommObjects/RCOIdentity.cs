﻿using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOIdentity : CommReadObject
	{
		public uint HabboId { get; private set; }
		public string Name { get; private set; }
		public string Figure { get; private set; }
		public bool IsMale { get; private set; }
		public string RealName { get; private set; }

		public override ushort SendType => 1487;

		public override void Deserialize(CommReader reader)
		{
			HabboId = reader.ReadUnsignedInteger();
			Name = reader.ReadString();
			Figure = reader.ReadString();
			IsMale = reader.ReadString().ToUpper() == "M";
			RealName = reader.ReadString();

			_ = reader.ReadString();
			_ = reader.ReadBoolean();
			_ = reader.ReadInteger();
			_ = reader.ReadInteger();
			_ = reader.ReadInteger();
			_ = reader.ReadBoolean();
			_ = reader.ReadString();
			_ = reader.ReadBoolean();
			_ = reader.ReadBoolean();
		}
	}
}
