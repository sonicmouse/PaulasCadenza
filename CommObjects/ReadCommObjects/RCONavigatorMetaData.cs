using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using System.Collections.Generic;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCONavigatorMetaData : CommReadObject
	{
		public IReadOnlyList<string> MetaData { get; private set; }

		public override ushort SendType => 1068;

		public override void Deserialize(CommReader reader)
		{
			var lst = new List<string>();

			var count = reader.ReadUnsignedInteger();
			for(var i = 0; i < count; ++i)
			{
				lst.Add(reader.ReadString());

				var savedCount = reader.ReadUnsignedInteger();
				for(var n = 0; n < savedCount; ++n)
				{
					var id = reader.ReadInteger();
					var searchCode = reader.ReadString();
					var filter = reader.ReadString();
					var localization = reader.ReadString();
				}
			}
			MetaData = lst;
		}
	}
}
