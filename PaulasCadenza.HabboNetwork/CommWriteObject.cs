﻿using PaulasCadenza.HabboNetwork.IO;
using System;

namespace PaulasCadenza.HabboNetwork
{
	public abstract class CommWriteObject : CommObject, IEquatable<CommWriteObject>
	{
		public abstract void Serialize(CommWriter writer);

		public virtual bool Equals(CommWriteObject other) =>
			(other != null) && (other.SendType == SendType);

		public override bool Equals(object obj) =>
			Equals(obj as CommWriteObject);

		public override int GetHashCode() => SendType;
	}
}
