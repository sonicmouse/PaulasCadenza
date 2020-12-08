using System.Collections.Generic;

namespace PaulasCadenza.Models
{
	public sealed class SearchedRoomModel
	{
		public string RoomName { get; set; }
		public uint RoomId { get; set; }
		public string Description { get; set; }
		public string OwnerName { get; set; }
		public uint OwnerId { get; set; }
		public uint MaxUserCount { get; set; }
		public uint UserCount { get; set; }
		public IReadOnlyList<string> Tags { get; set; }
		public uint Score { get; set; }
		public uint Ranking { get; set; }

		public override string ToString() => $"{RoomName} [{OwnerName}] [{UserCount}/{MaxUserCount}]";
	}
}
