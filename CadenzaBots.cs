using PaulasCadenza.HabboDHM;
using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.CommEventArgs;
using PaulasCadenza.CommObjects.ReadCommObjects;
using PaulasCadenza.CommObjects.WriteCommObjects;
using PaulasCadenza.Data;
using PaulasCadenza.Models;
using PaulasCadenza.UI.Forms;
using PaulasCadenza.Utilities;
using System;
using System.Collections.Concurrent;
using System.Linq;
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

		public void SelectBot(AccountModel acct, bool selected = true)
		{
			if(_bots.ContainsKey(acct))
			{
				_bots[acct].Selected = selected;
			}
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

		public enum WriteType
		{
			All,
			First,
			Last,
			Random,
			Selected
		}

		public void WriteCommObjectAsync(CommWriteObject cwo, WriteType type)
		{
			var bots = _bots.Values.Select(x => x.Bot);

			if (type == WriteType.First)
			{
				bots = _bots.Take(1).Select(x => x.Value.Bot);
			}
			else if(type == WriteType.Last)
			{
				bots = _bots.Reverse().Take(1).Select(x => x.Value.Bot);
			}
			else if(type == WriteType.Random)
			{
				bots = _bots.Skip(PRNG.Instance.Next(_bots.Count)).Take(1).Select(x => x.Value.Bot);
			}
			else if(type == WriteType.Selected)
			{
				bots = _bots.Where(x => x.Value.Selected).Select(x => x.Value.Bot);
			}

			foreach(var bot in bots)
			{
				bot.Comm.WriteCommObjectsAsync(cwo);
			}
		}

		#region Communication Events

		private void OnCommConnected(object sender, ConnectedEventArgs e)
		{
			var comm = (Communication)sender;

			comm.WriteCommObjectsAsync(
				new WCOHello(),
				new WCOBeginHandshake());

			var c = _bots[comm.Tag as AccountModel];
			NetworkCommPublisher.Interface.PublishConnected(c.Bot.Account);
		}

		private void OnCommDisconnected(object sender, DisconnectedEventArgs e)
		{
			var acct = ((Communication)sender).Tag as AccountModel;
			if (_bots.ContainsKey(acct))
			{
				_bots.TryRemove(acct, out var c);
				NetworkCommPublisher.Interface.PublishDisconnected(c.Bot.Account);
			}
		}

		private void OnCommReceivedCommObject(object sender, ReceivedCommObjectEventArgs e)
		{
			Console.WriteLine($"-> RECEIVED PACKET: {e.CommReadObject.GetType().Name}");

			var bot = _bots[((Communication)sender).Tag as AccountModel].Bot;

			NetworkCommPublisher.Interface.PublishCommReadObject(bot.Account, e.CommReadObject);

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
		}

		private void OnCommReceivedUnknownObject(object sender, ReceivedUnknownObjectEventArgs e)
		{
			Console.WriteLine($"UNKNOWN PACKET ({e.SendType}):");
			Console.WriteLine(e.HexDump);
		}

		private void OnCommSentCommObject(object sender, SentCommObjectEventArgs e)
		{
			Console.WriteLine($"<- SENT PACKET: {(e.Tag is CommWriteObject asdf ? asdf.GetType().Name : "<unknown>")}");
		}
		#endregion
	}
}
