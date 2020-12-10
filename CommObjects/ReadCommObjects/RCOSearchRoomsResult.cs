using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.Models;
using System.Collections.Generic;
using System.Linq;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOSearchRoomsResult : CommReadObject
	{
		public IEnumerable<SearchedRoomModel> Rooms { get; private set; }

		public override ushort SendType => 3469;

		public override void Deserialize(CommReader reader)
		{
			var d = new Dictionary<uint, SearchedRoomModel>();

			var searchCode = reader.ReadString();
			var searchFilter = reader.ReadString();
			var sets = reader.ReadUnsignedInteger();
			for(var i = 0; i < sets; ++i)
			{
				var searchCode2 = reader.ReadString();
				var searchText = reader.ReadString();
				var actionAllowed = reader.ReadInteger();
				_ = reader.ReadBoolean();
				var viewMode = reader.ReadInteger();
				var resultCount = reader.ReadUnsignedInteger();
				for(var n = 0; n < resultCount; ++n)
				{
					var room = new SearchedRoomModel
					{
						RoomId = reader.ReadUnsignedInteger(),
						RoomName = reader.ReadString(),
						OwnerId = reader.ReadUnsignedInteger(),
						OwnerName = reader.ReadString()
					};
					_ = reader.ReadInteger();
					room.UserCount = reader.ReadUnsignedInteger();
					room.MaxUserCount = reader.ReadUnsignedInteger();
					room.Description = reader.ReadString();
					_ = reader.ReadInteger();
					room.Score = reader.ReadUnsignedInteger();
					room.Ranking = reader.ReadUnsignedInteger();
					_ = reader.ReadInteger();
					var tagCount = reader.ReadUnsignedInteger();
					var lstTags = new List<string>(capacity: (int)tagCount);
					for(var t = 0; t < tagCount; ++t)
					{
						lstTags.Add(reader.ReadString());
					}
					room.Tags = lstTags;
					var flags = reader.ReadUnsignedInteger();
					if((flags & 1) == 1)
					{
						var officialRoomPicRef = reader.ReadString();
					}
					if((flags & 2) == 2)
					{
						var maybeGroupId = reader.ReadUnsignedInteger();
						var groupName = reader.ReadString();
						var maybeGroupDescription = reader.ReadString();
					}
					if((flags & 4) == 4)
					{
						_ = reader.ReadString();
						_ = reader.ReadString();
						_ = reader.ReadInteger();
					}
					d[room.RoomId] = room;
				}
			}

			Rooms = d.Select(x => x.Value);
		}
	}
}
