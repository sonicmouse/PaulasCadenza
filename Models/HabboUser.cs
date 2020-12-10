using System;

namespace PaulasCadenza.Models
{
	public class HabboUser : IEquatable<HabboUser>
	{
		public uint EntityId { get; }
		public uint Id { get; set; }
		public string Name { get; set; }
		public string Custom { get; set; }
		public string Figure { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public double Z { get; set; }
		public int Dir { get; set; }
		public bool? IsMale { get; set; }
		public int Score { get; set; }

		public HabboUser(uint entityId)
		{
			EntityId = entityId;
		}

		public override string ToString()
		{
			return $"{Name} ({Id:N0})";
		}

		public bool Equals(HabboUser other)
		{
			return (other != null) && (other.EntityId == EntityId);
		}

		public override int GetHashCode()
		{
			return unchecked((int)EntityId);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as HabboUser);
		}
	}
}
