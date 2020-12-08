using PaulasCadenza.HabboNetwork.CommEventArgs;
using PaulasCadenza.HabboNetwork.IO;
using PaulasCadenza.HabboNetwork.Models;
using PaulasCadenza.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace PaulasCadenza.HabboNetwork
{
	public sealed class Communication
	{
		public event EventHandler<ConnectedEventArgs> Connected;
		public event EventHandler<SentCommObjectEventArgs> SentCommObject;
		public event EventHandler<ReceivedCommObjectEventArgs> ReceivedCommObject;
		public event EventHandler<ReceivedUnknownObjectEventArgs> ReceivedUnknownObject;
		public event EventHandler<DisconnectedEventArgs> Disconnected;

		public string Host { get; }
		public ushort Port { get; }
		public object Tag { get; set; }
		public IEncryption WriteEncryption { get; set; }
		public IEncryption ReadEncryption { get; set; }
		public bool IsConnected => _client != null;

		private TcpClient _client;
		private readonly byte[] _networkRecvBuffer = new byte[1024];
		private readonly List<byte> _recvBufferStorage = new List<byte>(capacity: 2048);
		private readonly ICommReadObjectDelegate _commReadObjectDelegate;

		public Communication(string host, ushort port, ICommReadObjectDelegate @delegate)
		{
			_commReadObjectDelegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			Host = host ?? throw new ArgumentNullException(nameof(host));
			Port = port > 0 ? port : throw new ArgumentException("must be greater than zero", nameof(port));
		}

		public void ConnectAsync()
		{
			Disconnect();
			_client = new TcpClient();

			TryNetworkAction(() =>
			{
				_client.BeginConnect(Host, Port, iar =>
				{
					if (!TryNetworkAction(() =>
					{
						_client.EndConnect(iar);
						_client.GetStream().BeginRead(_networkRecvBuffer, 0, _networkRecvBuffer.Length, OnEndRead, null);
					}))
					{
						return;
					}

					Connected?.BeginInvoke(this, new ConnectedEventArgs
					{
						Host = Host,
						Port = Port
					}, Connected.EndInvoke, null);
				}, null);
			});
		}

		public void Disconnect()
		{
			try
			{
				_client?.GetStream()?.Dispose();
#if NETSTANDARD2_0
				_client?.Dispose();
#endif
			} catch { }
			_client = null;
		}

		private void OnEndRead(IAsyncResult result)
		{
			var bytesRead = 0;
			if (!TryNetworkAction(() =>
			{
				bytesRead = (_client?.GetStream()?.EndRead(result)).GetValueOrDefault();
				if (bytesRead <= 0)
				{
					throw new InvalidOperationException($"The socket was disconnected from the server");
				}
			}))
			{
				return;
			}
			
			try
			{
				if (ReadEncryption != null)
				{
					_recvBufferStorage.AddRange(ReadEncryption.Decrypt(
						_networkRecvBuffer.Take(bytesRead).ToArray()));
				}
				else
				{
					_recvBufferStorage.AddRange(_networkRecvBuffer.Take(bytesRead));
				}
				
				ProcessRecvBuffer();
			}
			finally
			{
				TryNetworkAction(() =>
					_client?.GetStream()?.BeginRead(_networkRecvBuffer, 0, _networkRecvBuffer.Length, OnEndRead, null));
			}
		}

		public void WriteCommObjectsAsync(params CommWriteObject[] commWriteObjects)
		{
			commWriteObjects.ToList().ForEach(WriteCommObjectAsync);
		}

		private void WriteCommObjectAsync(CommWriteObject obj)
		{
			_ = obj ?? throw new ArgumentNullException(nameof(obj));

			using(var ms = new MemoryStream())
			{
				using (var sr = new CommWriter(ms))
				{
					obj.Serialize(sr);
				}
				WriteRawAsync(obj.SendType, ms.ToArray(), !(obj is ISendMessageNeverEncrypt), obj);
			}
		}

		public void WriteRawAsync(ushort sendType, byte[] data, bool encrypt = true, object tag = null)
		{
			data = data ?? new byte[] { };

			MemoryStream msMain = new MemoryStream(), msInner = new MemoryStream();
			CommWriter cwMain = new CommWriter(msMain, false), cwInner = new CommWriter(msInner, false);

			cwInner.WriteUnsignedShort(sendType);
			cwInner.WriteRawBytes(data);
			cwMain.WriteByteArray(msInner.ToArray());

			data = msMain.ToArray();

			cwMain.Dispose();
			cwInner.Dispose();

			if (encrypt && (WriteEncryption != null))
			{
				WriteEncryption.Encrypt(data);
			}

			TryNetworkAction(() =>
				_client.GetStream().BeginWrite(data, 0, data.Length, OnEndSend, tag));
		}

		private void OnEndSend(IAsyncResult result)
		{
			object tag = null;
			if (!TryNetworkAction(() =>
			{
				tag = result.AsyncState;
				_client.GetStream().EndWrite(result);
			})) { return; }

			SentCommObject?.BeginInvoke(this, new SentCommObjectEventArgs { Tag = tag },
				SentCommObject.EndInvoke, null);
		}

		private bool TryNetworkAction(Action action)
		{
			try
			{
				action.Invoke();
				return true;
			}
			catch (Exception ex)
			{
				Disconnect();
				Disconnected?.BeginInvoke(this, new DisconnectedEventArgs { Exception = ex },
					Disconnected.EndInvoke, null);
			}
			return false;
		}

		private static TResult Gulp<TResult>(
			List<byte> lst, int size, bool remove = true,
			Func<byte[], TResult> conv = null)
		{
			var arr = new byte[size];
			lst.CopyTo(0, arr, 0, size);
			if(remove)
			{
				lst.RemoveRange(0, size);
			}
			return conv != null ? conv(arr) : default;
		}

		private void ProcessRecvBuffer()
		{
			while (_recvBufferStorage.Count >= sizeof(uint))
			{
				var packetLen = Gulp(_recvBufferStorage, sizeof(uint), remove: false,
					x => BitConverter.ToUInt32(x, 0).SwapEndianness());

				if (packetLen > (_recvBufferStorage.Count - sizeof(uint)))
				{
					break;
				}
				else
				{
					Gulp<uint>(_recvBufferStorage, sizeof(uint));

					var sendType = Gulp(_recvBufferStorage, sizeof(ushort),
						conv: x => BitConverter.ToUInt16(x, 0).SwapEndianness());

					var data = Gulp(_recvBufferStorage, (int)(packetLen - sizeof(ushort)), conv: x => x);

					var cro = _commReadObjectDelegate.DeriveCommReadObject(sendType);

					if(cro != null)
					{
						using (var sr = new CommReader(data)) { cro.Deserialize(sr); }

						ReceivedCommObject?.BeginInvoke(this, new ReceivedCommObjectEventArgs
						{
							CommReadObject = cro
						}, ReceivedCommObject.EndInvoke, null);
					}
					else
					{
						ReceivedUnknownObject?.BeginInvoke(this, new ReceivedUnknownObjectEventArgs
						{
							SendType = sendType,
							Data = data,
							HexDump = PaulasCadenza.Utilities.HexDump.Process(data)
						}, ReceivedUnknownObject.EndInvoke, null);
					}
				}
			}
		}
	}
}
