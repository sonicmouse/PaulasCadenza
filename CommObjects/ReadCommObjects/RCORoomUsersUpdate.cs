using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.Models;
using System.Collections.Generic;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCORoomUsersUpdate : CommReadObject
	{
		public IList<RoomUserUpdateModel> UpdatedUsers = new List<RoomUserUpdateModel>();

		public override ushort SendType => 283;

		public override void Deserialize(CommReader reader)
		{
			var count = reader.ReadUnsignedInteger();
			for(var i = 0; i < count; ++i)
			{
				UpdatedUsers.Add(new RoomUserUpdateModel
				{
					EntityId = reader.ReadUnsignedInteger(),
					X = reader.ReadInteger(),
					Y = reader.ReadInteger(),
					Z = double.Parse(reader.ReadString()),
					Dir1 = reader.ReadInteger() % 8 * 45,
					Dir2 = reader.ReadInteger() % 8 * 45,
					Command = reader.ReadString()
				});
			}
		}
	}
}
