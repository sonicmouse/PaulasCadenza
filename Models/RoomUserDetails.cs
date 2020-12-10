namespace PaulasCadenza.Models
{
	public sealed class RoomUserDetails
	{
		public uint EntityId { get; set; }
		public string Avatar { get; set; }
		public bool IsMale { get; set; }
		public string Custom { get; set; }
		public int Score { get; set; }
	}
}
