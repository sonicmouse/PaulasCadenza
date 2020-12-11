using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.Models;
using System.Collections.Generic;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCORoomUsers : CommReadObject
	{
		public Dictionary<uint, HabboUserModel> Users { get; } = new Dictionary<uint, HabboUserModel>();

		public override ushort SendType => 1496;

		public override void Deserialize(CommReader reader)
		{
			var userCount = reader.ReadUnsignedInteger();
			for(var i = 0; i < userCount; ++i)
			{
				var id = reader.ReadUnsignedInteger();
				var name = reader.ReadString();
				var custom = reader.ReadString();
				var figure = reader.ReadString();
				var entityId = reader.ReadUnsignedInteger();
				var x = reader.ReadInteger();
				var y = reader.ReadInteger();
				var z = double.Parse(reader.ReadString());
				var dir = reader.ReadInteger();

				var user = new HabboUserModel(entityId)
				{
					HabboId = id,
					Name = name,
					Custom = custom,
					Figure = figure,
					X = x,
					Y = y,
					Z = z,
					Dir = dir
				};

				var extendedProfileType = reader.ReadInteger();
				if (extendedProfileType == 1)
				{
					user.IsMale = reader.ReadString().ToUpper() == "M";
					var unknown2 = reader.ReadInteger(); // group id?
					var unknown3 = reader.ReadInteger(); // something with groups?
					var groupName = reader.ReadString();
					var unknown4 = reader.ReadString();
					user.Score = reader.ReadInteger();
					var unknown6 = reader.ReadBoolean();
				}
				else if(extendedProfileType == 2)
				{
					var unknown1 = reader.ReadInteger();
					var unknown2 = reader.ReadInteger();
					var ownerName = reader.ReadString();
					var unknown3 = reader.ReadInteger();
					var unknown4 = reader.ReadBoolean();
					var unknown5 = reader.ReadBoolean();
					var unknown6 = reader.ReadBoolean();
					var unknown7 = reader.ReadBoolean();
					var unknown8 = reader.ReadBoolean();
					var unknown9 = reader.ReadBoolean();
					var unknown10 = reader.ReadInteger();
					var unknown11 = reader.ReadString();
				}
				else if(extendedProfileType == 3)
				{
					// nothing
				}
				else if(extendedProfileType == 4) // i think this is a bot
				{
					user.IsMale = reader.ReadString().ToUpper() == "M";
					var id2 = reader.ReadInteger();
					var name2 = reader.ReadString();

					var unknownCount = reader.ReadUnsignedInteger();
					for(var n = 0; n < unknownCount; ++n)
					{
						reader.ReadShort();
					}
				}

				Users[user.EntityId] = user;
			}
		}
	}
}
