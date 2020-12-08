﻿using PaulasCadenza.HabboNetwork.IO;
using System;

namespace PaulasCadenza.HabboNetwork
{
	public abstract class CommReadObject : IEquatable<CommReadObject>
	{
		public abstract ushort SendType { get; }
		public abstract void Deserialize(CommReader reader);

		public virtual bool Equals(CommReadObject other) =>
			(other != null) && (other.SendType == SendType);

		public override bool Equals(object obj) =>
			Equals(obj as CommReadObject);

		public override int GetHashCode() => SendType;
	}
}
