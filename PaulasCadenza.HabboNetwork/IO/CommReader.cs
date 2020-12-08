using PaulasCadenza.Utilities.Extensions;
using System;
using System.IO;
using System.Text;

namespace PaulasCadenza.HabboNetwork.IO
{
	public class CommReader : IDisposable
	{
		private readonly Stream _reader;
		private readonly bool _leaveOpen;
		private bool _disposed;

		public CommReader(Stream stream, bool leaveOpen = true)
		{
			_reader = stream;
			_leaveOpen = leaveOpen;
		}

		public CommReader(byte[] array)
			: this(new MemoryStream(array), leaveOpen: false)
		{
		}

		private void EnsureBytesAvailable(uint bytesNeeded)
		{
			if (bytesNeeded > BytesAvailable)
			{
				throw new EndOfStreamException();
			}
		}

		private TResult ReadBytesWithConversion<TResult>(uint bytesNeeded, Func<byte[], TResult> conv)
		{
			EnsureBytesAvailable(bytesNeeded);
			var arr = new byte[bytesNeeded];
			_reader.Read(arr, 0, (int)bytesNeeded);
			return conv.Invoke(arr);
		}

		private TResult PeekBytesWithConversion<TResult>(uint bytesNeeded, Func<byte[], TResult> conv)
		{
			var ret = ReadBytesWithConversion(bytesNeeded, conv);
			_reader.Seek(-bytesNeeded, SeekOrigin.Current);
			return ret;
		}

		public virtual long BytesAvailable => _reader.Length - _reader.Position;

		public virtual bool IsDataAvailable => BytesAvailable > 0;

		public virtual char ReadChar() =>
			ReadBytesWithConversion(sizeof(char), x => Convert.ToChar(x[0]));

		public virtual byte ReadByte() =>
			ReadBytesWithConversion(sizeof(byte), x => x[0]);

		public virtual bool ReadBoolean() =>
			ReadBytesWithConversion(sizeof(bool), x => x[0] != 0);

		public virtual int ReadInteger() =>
			ReadBytesWithConversion(sizeof(int), x => BitConverter.ToInt32(x, 0).SwapEndianness());

		public virtual uint ReadUnsignedInteger() =>
			ReadBytesWithConversion(sizeof(uint), x => BitConverter.ToUInt32(x, 0).SwapEndianness());

		public virtual short ReadShort() =>
			ReadBytesWithConversion(sizeof(short), x => BitConverter.ToInt16(x, 0).SwapEndianness());

		public virtual ushort ReadUnsignedShort() =>
			ReadBytesWithConversion(sizeof(ushort), x => BitConverter.ToUInt16(x, 0).SwapEndianness());

		public virtual float ReadFloat() =>
			ReadBytesWithConversion(sizeof(float), x => BitConverter.ToSingle(x, 0).SwapEndianness());

		public virtual double ReadDouble() =>
			ReadBytesWithConversion(sizeof(double), x => BitConverter.ToDouble(x, 0).SwapEndianness());

		public virtual string ReadString(Encoding encoding = null)
		{
			var lenNeeded = PeekBytesWithConversion(sizeof(ushort), x => BitConverter.ToUInt16(x, 0).SwapEndianness());
			EnsureBytesAvailable((uint)(sizeof(ushort) + lenNeeded));

			var lenString = ReadUnsignedShort();
			return ReadBytesWithConversion(lenString, x => (encoding ?? Encoding.UTF8).GetString(x));
		}

		public virtual byte[] ReadByteArray()
		{
			var lenNeeded = PeekBytesWithConversion(sizeof(uint), x => BitConverter.ToUInt32(x, 0).SwapEndianness());
			EnsureBytesAvailable(sizeof(uint) + lenNeeded);

			var lenString = ReadUnsignedInteger();
			return ReadBytesWithConversion(lenString, x => x);
		}

		public virtual byte[] ReadRawBytes(uint length) =>
			ReadBytesWithConversion(length, x => x);

		public virtual void Dispose()
		{
			if (_disposed) { throw new ObjectDisposedException(GetType().Name); }
			_disposed = true;
			if (!_leaveOpen)
			{
				_reader.Dispose();
			}
		}
	}
}
