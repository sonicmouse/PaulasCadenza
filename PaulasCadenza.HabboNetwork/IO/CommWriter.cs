using PaulasCadenza.Utilities.Extensions;
using System;
using System.IO;
using System.Text;

namespace PaulasCadenza.HabboNetwork.IO
{
	public class CommWriter : IDisposable
	{
		private readonly Stream _writer;
		private bool _disposed;
		private readonly bool _leaveOpen;

		public CommWriter(Stream stream, bool leaveOpen = true)
		{
			_leaveOpen = leaveOpen;
			_writer = stream;
		}

		public virtual void WriteChar(char data) =>
			_writer.WriteByte((byte)data);

		public virtual void WriteByte(byte data) =>
			_writer.WriteByte(data);

		public virtual void WriteBoolean(bool data) =>
			_writer.WriteByte((byte)(data ? 1 : 0));

		public virtual void WriteInteger(int data) =>
			_writer.Write(BitConverter.GetBytes(data.SwapEndianness()), 0, sizeof(int));

		public virtual void WriteUnsignedInteger(uint data) =>
			_writer.Write(BitConverter.GetBytes(data.SwapEndianness()), 0, sizeof(uint));

		public virtual void WriteShort(short data) =>
			_writer.Write(BitConverter.GetBytes(data.SwapEndianness()), 0, sizeof(short));

		public virtual void WriteUnsignedShort(ushort data) =>
			_writer.Write(BitConverter.GetBytes(data.SwapEndianness()), 0, sizeof(ushort));

		public virtual void WriteFloat(float data) =>
			_writer.Write(BitConverter.GetBytes(data.SwapEndianness()), 0, sizeof(float));

		public virtual void WriteDouble(double data) =>
			_writer.Write(BitConverter.GetBytes(data.SwapEndianness()), 0, sizeof(double));

		public virtual void WriteString(string data, Encoding encoding = null)
		{
			WriteUnsignedShort((ushort)(data?.Length).GetValueOrDefault());
			var dataToWrite = (encoding ?? Encoding.UTF8).GetBytes(data ?? string.Empty);
			_writer.Write(dataToWrite, 0, dataToWrite.Length);
		}

		public virtual void WriteByteArray(byte[] data)
		{
			var len = (data?.Length).GetValueOrDefault();
			WriteUnsignedInteger((uint)len);
			_writer.Write(data ?? new byte[] { }, 0, len);
		}

		public virtual void WriteRawBytes(byte[] data) =>
			_writer.Write(data ?? new byte[] { }, 0, (data?.Length).GetValueOrDefault());

		public virtual void Dispose()
		{
			if (_disposed) { throw new ObjectDisposedException(GetType().Name); }
			_disposed = true;
			if (!_leaveOpen)
			{
				_writer.Dispose();
			}
		}
	}
}
