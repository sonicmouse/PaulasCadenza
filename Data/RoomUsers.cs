using PaulasCadenza.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PaulasCadenza.Data
{
	public sealed class RoomUsers
	{
		private readonly ConcurrentDictionary<uint, HabboUserModel> _users =
			new ConcurrentDictionary<uint, HabboUserModel>();

		public void AddUsers(Dictionary<uint, HabboUserModel> d)
		{
			foreach(var u in d)
			{
				_users[u.Key] = u.Value;
			}
		}

		public void RemoveUser(uint entity)
		{
			if(_users.ContainsKey(entity))
			{
				_users.TryRemove(entity, out _);
			}
		}

		public void RemoveAllUsers()
		{
			_users.Clear();
		}

		public void UpdateUsers(IEnumerable<RoomUserUpdateModel> users)
		{
			foreach(var u in users)
			{
				if(_users.ContainsKey(u.EntityId))
				{
					var usr = _users[u.EntityId];
					usr.Dir = u.Dir1;
					usr.X = u.X;
					usr.Y = u.Y;
					usr.Z = u.Z;
				}
			}
		}

		public void UpdateUserDetails(RoomUserDetailsModel details)
		{
			if(_users.ContainsKey(details.EntityId))
			{
				var usr = _users[details.EntityId];
				usr.Custom = details.Custom;
				usr.Figure = details.Avatar;
				usr.IsMale = details.IsMale;
				usr.Score = details.Score;
			}
		}

		public IEnumerable<HabboUserModel> GetAllUsers()
		{
			return _users.Select(x => x.Value);
		}
	}
}
