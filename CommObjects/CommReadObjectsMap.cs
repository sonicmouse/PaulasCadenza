using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaulasCadenza.CommObjects
{
	public sealed class CommReadObjectsMap : ICommReadObjectDelegate
	{
		private readonly ReadOnlyDictionary<ushort, Type> _commReadObjectTypes;

		public static CommReadObjectsMap Instance { get; }
		static CommReadObjectsMap() { Instance = new CommReadObjectsMap(); }
		private CommReadObjectsMap()
		{
			_commReadObjectTypes = new ReadOnlyDictionary<ushort, Type>(
				GetType().Assembly.GetTypes().Where(
					t => typeof(CommReadObject).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).
				Select(Activator.CreateInstance).Cast<CommReadObject>().ToDictionary(
					x => x.SendType, x => x.GetType()));
		}

		public bool ContainsSendType(ushort index) =>
			_commReadObjectTypes.ContainsKey(index);

		public CommReadObject this[ushort sendType] =>
			ContainsSendType(sendType) ?
				(CommReadObject)Activator.CreateInstance(_commReadObjectTypes[sendType]) : null;

		public CommReadObject DeriveCommReadObject(ushort sendType) => this[sendType];
	}
}
