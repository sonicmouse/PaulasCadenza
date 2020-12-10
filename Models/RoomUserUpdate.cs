namespace PaulasCadenza.Models
{
	public sealed class RoomUserUpdate
	{
		public uint EntityId { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public double Z { get; set; }
		public int Dir1 { get; set; }
		public int Dir2 { get; set; }
		public string Command { get; set; }
	}
}
