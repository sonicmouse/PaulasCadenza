using PaulasCadenza.CommObjects.ReadCommObjects;
using PaulasCadenza.CommObjects.WriteCommObjects;
using PaulasCadenza.Data;
using PaulasCadenza.HabboDHM;
using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.CommEventArgs;
using PaulasCadenza.Models;
using PaulasCadenza.UI.Forms;
using PaulasCadenza.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaulasCadenza
{
	public sealed class CadenzaBots
	{
		private sealed class BotContainer
		{
			public CadenzaBotModel Bot { get; set; }
			public bool Selected { get; set; }
		}

		private readonly ConcurrentDictionary<AccountModel, BotContainer> _bots =
			new ConcurrentDictionary<AccountModel, BotContainer>();

		public static CadenzaBots Instance { get; } = new CadenzaBots();
		private CadenzaBots() { }

		public bool Connect(IWin32Window owner, AccountModel acct, bool selected = true)
		{
			Disconnect(acct);

			using (var frm = new FormBrowser(acct.Hotel, acct.Email, acct.PlainTextPassword))
			{
				if (frm.ShowDialog(owner) == DialogResult.OK)
				{
					_bots[acct] = new BotContainer
					{
						Bot = new CadenzaBotModel(acct, frm.FigureData, frm.FlashVars),
						Selected = selected
					};

					_bots[acct].Bot.Comm.Connected += OnCommConnected;
					_bots[acct].Bot.Comm.Disconnected += OnCommDisconnected;
					_bots[acct].Bot.Comm.ReceivedCommObject += OnCommReceivedCommObject;
					_bots[acct].Bot.Comm.ReceivedUnknownObject += OnCommReceivedUnknownObject;
					_bots[acct].Bot.Comm.SentCommObject += OnCommSentCommObject;

					_bots[acct].Bot.Comm.Tag = acct;
					_bots[acct].Bot.Comm.ConnectAsync();
					return true;
				}
			}

			return false;
		}

		public void Disconnect(AccountModel acct)
		{
			if(_bots.ContainsKey(acct))
			{
				_bots[acct].Bot.Comm.Disconnect();
				_bots.TryRemove(acct, out var c);
				NetworkCommPublisher.Interface.PublishDisconnected(c.Bot.Account);
			}
		}

		public void SelectBot(AccountModel acct, bool selected = true)
		{
			if (_bots.ContainsKey(acct))
			{
				_bots[acct].Selected = selected;
			}
		}

		public RoomUsers GetRoomUsers(AccountModel acct)
		{
			return _bots[acct].Bot.RoomUsers;
		}

		public void ClearAllRoomUsers()
		{
			foreach(var b in _bots)
			{
				b.Value.Bot.RoomUsers.RemoveAllUsers();
			}
		}

		public enum WriteType
		{
			All,
			First,
			Last,
			Random,
			Selected
		}

		private IEnumerable<CadenzaBotModel> GetBotSet(WriteType type)
		{
			var bots = _bots.Values.Select(x => x.Bot);

			switch (type)
			{
				case WriteType.First:
					bots = _bots.Take(1).Select(x => x.Value.Bot);
					break;
				case WriteType.Last:
					bots = _bots.Reverse().Take(1).Select(x => x.Value.Bot);
					break;
				case WriteType.Random:
					bots = _bots.Skip(PRNG.Instance.Next(_bots.Count)).Take(1).Select(x => x.Value.Bot);
					break;
				case WriteType.Selected:
					bots = _bots.Where(x => x.Value.Selected).Select(x => x.Value.Bot);
					break;
			}
			return bots;
		}

		public void WriteCommObjectAsync(CommWriteObject cwo, WriteType type)
		{
			foreach (var bot in GetBotSet(type))
			{
				bot.Comm.WriteCommObjectsAsync(cwo);
			}
		}

		public async Task WriteCommObjectTaskAsync(CommWriteObject cwo, WriteType type, int? throttleBy = null)
		{
			foreach (var bot in GetBotSet(type))
			{
				bot.Comm.WriteCommObjectsAsync(cwo);
				if(throttleBy.HasValue)
				{
					await Task.Delay(throttleBy.Value).ConfigureAwait(false);
				}
			}
		}

		public void ShowSignAsync(int? value, WriteType type)
		{
			foreach(var bot in GetBotSet(type))
			{
				bot.Comm.WriteCommObjectsAsync(new WCOShowSign(value.GetValueOrDefault(PRNG.Instance.Next(18))));
			}
		}

		public void LookAtAsync(int dir, WriteType type)
		{
			// zero is upper left, then clockwise
			if(dir < 0 || dir > 7) { return; }

			var offsets = new (int, int)[] { (-1, -1), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0) };

			foreach(var bot in GetBotSet(type))
			{
				var roomEntity = bot.RoomUsers.GetAllUsers().FirstOrDefault(x => x.HabboId == bot.HabboId);
				if(roomEntity != null)
				{
					bot.Comm.WriteCommObjectsAsync(
						new WCOLookAt(roomEntity.X + offsets[dir].Item1, roomEntity.Y + offsets[dir].Item2));
				}
			}
		}

		public void AssignRandomAvatarAsync(bool hotLook, WriteType type)
		{
			foreach (var bot in GetBotSet(type))
			{
				string avatar;
				bool isMale;
				if (hotLook)
				{
					avatar = bot.Figure.GetRandomHotLookFigure(out isMale);
				}
				else
				{
					avatar = bot.Figure.GetRandomFigure(out isMale);
				}
				bot.Comm.WriteCommObjectsAsync(new WCOAvatar(isMale, avatar));
			}
		}

		public string GetRandomAvatar(bool hotLook, out bool isMale)
		{
			var bot = GetBotSet(WriteType.Random).First();
			if (hotLook)
			{
				return bot.Figure.GetRandomHotLookFigure(out isMale);
			}
			else
			{
				return bot.Figure.GetRandomFigure(out isMale);
			}
		}

		public void MoveToAsync(Point ptStart, Func<int, IEnumerable<Point>> ptFactory, WriteType type)
		{
			var bots = GetBotSet(type);

			var botCount = bots.Count();
			var pts = ptFactory.Invoke(botCount).Take(botCount).ToArray();

			var index = 0;
			foreach (var b in bots)
			{
				b.Comm.WriteCommObjectsAsync(
					new WCOMove(ptStart.X + pts[index].X, ptStart.Y + pts[index].Y));
				++index;
			}
		}

		#region Communication Events

		private void OnCommConnected(object sender, ConnectedEventArgs e)
		{
			var comm = (Communication)sender;

			comm.WriteCommObjectsAsync(
				new WCOHello(),
				new WCOBeginHandshake());

			NetworkCommPublisher.Interface.PublishConnected(comm.Tag as AccountModel);
		}

		private void OnCommDisconnected(object sender, DisconnectedEventArgs e)
		{
			var acct = ((Communication)sender).Tag as AccountModel;
			if (_bots.ContainsKey(acct))
			{
				if(_bots.TryRemove(acct, out var c))
				{
					NetworkCommPublisher.Interface.PublishDisconnected(c.Bot.Account);
				}
			}
		}

		private void OnCommReceivedCommObject(object sender, ReceivedCommObjectEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($"-> RECEIVED OBJECT: {e.CommReadObject.GetType().Name}");

			var acct = ((Communication)sender).Tag as AccountModel;
			if(acct == null || !_bots.ContainsKey(acct))
			{
				return;
			}

			var bot = _bots[acct].Bot;

			NetworkCommPublisher.Interface.PublishCommReadObject(acct, e.CommReadObject);

			if (e.CommReadObject is RCOInitHandshake initHandshake)
			{
				bot.DHM = new DHM(initHandshake.Left, initHandshake.Right);
				bot.Comm.WriteCommObjectsAsync(new WCOAnswerHandshake(bot.DHM.Answer));
			}
			else if (e.CommReadObject is RCOAnswerReplyHandshake answerReply)
			{
				bot.DHM.ApplyServerAnswer(answerReply.AnswerReply);
				bot.Comm.WriteEncryption = new RC4Encryption(bot.DHM.SharedSecret);
				if (answerReply.ServerEncrypted)
				{
					bot.Comm.ReadEncryption = new RC4Encryption(bot.DHM.SharedSecret);
				}
				bot.DHM.Dispose(); bot.DHM = null;

				bot.Comm.WriteCommObjectsAsync(
					new WCOClientVars(401, "https:" + bot.FlashVars.FlashClientUrl,
						bot.FlashVars.ExternalVariablesTxt.ToString()),
					new WCOClientFingerprint(),
					new WCOAuthenticate(bot.FlashVars.SSOTicket));
			}
			else if (e.CommReadObject is RCOPing)
			{
				bot.Comm.WriteCommObjectsAsync(new WCOPong());
			}
			else if (e.CommReadObject is RCOAuthSuccess)
			{
				bot.Comm.WriteCommObjectsAsync(
					new WCOAuthAcknowledge(),
					new WCOTrackEventLog("Login", "socket", "client.auth_ok"));

				NetworkCommPublisher.Interface.PublishAuthenticated(bot.Account);
			}
			else if(e.CommReadObject is RCOEnteredRoom)
			{
				bot.Comm.WriteCommObjectsAsync(new WCORoomSubscribe());
			}
			else if(e.CommReadObject is RCOIdentity identity)
			{
				bot.HabboId = identity.HabboId;
			}
		}

		private void OnCommReceivedUnknownObject(object sender, ReceivedUnknownObjectEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($"!! UNKNOWN OBJECT ({e.SendType}):\n{e.HexDump}");
		}

		private void OnCommSentCommObject(object sender, SentCommObjectEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($"<- SENT OBJECT: {(e.Tag is CommWriteObject cwo ? cwo.GetType().Name : "<unknown>")}");
		}
		#endregion
	}
}
