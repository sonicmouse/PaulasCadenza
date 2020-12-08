using PaulasCadenza.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaulasCadenza.HabboDHM.ASUtilities
{
	internal sealed class ASByteArray
	{
		private readonly List<byte> _arr = new List<byte>();
		private int _pos;

		public int Length
		{
			get => _arr.Count;
			set
			{
				if(value < 0)
				{
					throw new InvalidOperationException("Length must be greater than or equal to 0");
				}
				if(value > _arr.Count)
				{
					_arr.InsertRange(_arr.Count, Enumerable.Repeat<byte>(0, value - _arr.Count));
				}
				else if(value < _arr.Count)
				{
					_arr.RemoveRange(_arr.Count, _arr.Count - value);
					_pos = Math.Min(_pos, _arr.Count);
				}
			}
		}

		public int BytesAvailable => _arr.Count - _pos;

		public int Position
		{
			get => _pos;
			set
			{
				if(value < 0)
				{
					throw new InvalidOperationException("Position must be greater than or equal to 0");
				}
				if(value > _arr.Count)
				{
					throw new InvalidOperationException("Length must be less than or equal to the length of the array");
				}
				_pos = value;
			}
		}

		private void EnsureIndex(int index)
		{
			if ((index + 1) > _arr.Count)
			{
				_arr.InsertRange(_arr.Count, Enumerable.Repeat((byte)0, index + 1 - _arr.Count));
			}
		}

		public byte this[int index]
		{
			get
			{
				EnsureIndex(index);
				return _arr[index];
			}
			set
			{
				EnsureIndex(index);
				_arr[index] = value;
			}
		}
		

		public void Clear()
		{
			_arr.Clear();
			_pos = 0;
		}

		public bool ReadBoolean()
		{
			var val = _arr[_pos++];
			return val != 0;
		}

		public char ReadByte()
		{
			return unchecked((char)_arr[_pos++]);
		}

		public void ReadBytes(ASByteArray bytes, int offset = 0, int length = 0)
		{
			var len = length == 0 ? BytesAvailable : length;

			if(len > BytesAvailable)
			{
				throw new IndexOutOfRangeException();
			}

			var dataToCopy = _arr.GetRange(_pos, len);

			bytes.EnsureSpace(offset + len);
			bytes._arr.RemoveRange(offset, len);
			bytes._arr.InsertRange(offset, dataToCopy);

			_pos += len;
		}

		public double ReadDouble()
		{
			var ret = BitConverter.ToDouble(_arr.GetRange(_pos, sizeof(double)).ToArray(), 0);
			_pos += sizeof(double);
			return ret.SwapEndianness();
		}

		public float ReadFloat()
		{
			var ret = BitConverter.ToSingle(_arr.GetRange(_pos, sizeof(double)).ToArray(), 0);
			_pos += sizeof(float);
			return ret.SwapEndianness();
		}

		public int ReadInt()
		{
			var ret = BitConverter.ToInt32(_arr.GetRange(_pos, sizeof(int)).ToArray(), 0);
			_pos += sizeof(int);
			return ret.SwapEndianness();
		}

		// TODO: readMultiByte(length:uint, charSet:String):String

		public int ReadShort()
		{
			var ret = BitConverter.ToInt16(_arr.GetRange(_pos, sizeof(short)).ToArray(), 0);
			_pos += sizeof(short);
			return ret.SwapEndianness();
		}

		public byte ReadUnsignedByte()
		{
			return _arr[_pos++];
		}

		public uint ReadUnsignedInt()
		{
			var ret = BitConverter.ToUInt32(_arr.GetRange(_pos, sizeof(uint)).ToArray(), 0);
			_pos += sizeof(uint);
			return ret.SwapEndianness();
		}

		public int ReadUnsignedShort()
		{
			var ret = BitConverter.ToUInt16(_arr.GetRange(_pos, sizeof(ushort)).ToArray(), 0);
			_pos += sizeof(ushort);
			return ret.SwapEndianness();
		}

		// TODO: readUTF():String
		// TODO: readUTFBytes(length:uint):String

		private void EnsureSpace(int size)
		{
			if(_pos + size > Length)
			{
				Length = _pos + size;
			}
		}

		public void WriteBoolean(bool b)
		{
			EnsureSpace(sizeof(bool));
			_arr[_pos++] = (byte)(b ? 1 : 0);
		}

		public void WriteByte(byte b)
		{
			EnsureSpace(sizeof(byte));
			_arr[_pos++] = b;
		}

		public void WriteBytes(ASByteArray bytes, int offset = 0, int length = 0)
		{
			var lenToWrite = length == 0 ? bytes.Length : length;

			EnsureSpace(offset + lenToWrite);
			_arr.RemoveRange(_pos + offset, lenToWrite);
			_arr.InsertRange(_pos + offset, bytes._arr.GetRange(offset, lenToWrite));
			_pos += lenToWrite;
		}

		public void WriteDouble(double dbl)
		{
			EnsureSpace(sizeof(double));
			_arr.RemoveRange(_pos, sizeof(double));
			_arr.InsertRange(_pos, BitConverter.GetBytes(dbl.SwapEndianness()));
			_pos += sizeof(double);
		}

		public void WriteFloat(float flt)
		{
			EnsureSpace(sizeof(float));
			_arr.RemoveRange(_pos, sizeof(float));
			_arr.InsertRange(_pos, BitConverter.GetBytes(flt.SwapEndianness()));
			_pos += sizeof(float);
		}

		public void WriteInt(int n)
		{
			EnsureSpace(sizeof(int));
			_arr.RemoveRange(_pos, sizeof(int));
			_arr.InsertRange(_pos, BitConverter.GetBytes(n.SwapEndianness()));
			_pos += sizeof(int);
		}

		public void WriteMultiByte(string value, int codePage)
		{
			var data = System.Text.Encoding.GetEncoding(codePage).GetBytes(value);
			var tmp = new ASByteArray();
			foreach(var b in data)
			{
				tmp.WriteByte(b);
			}
			tmp.Position = 0;
			WriteBytes(tmp);
		}

		public void WriteShort(short n)
		{
			EnsureSpace(sizeof(short));
			_arr.RemoveRange(_pos, sizeof(short));
			_arr.InsertRange(_pos, BitConverter.GetBytes(n.SwapEndianness()));
			_pos += sizeof(short);
		}

		public void WriteUnsignedInt(uint n)
		{
			EnsureSpace(sizeof(uint));
			_arr.RemoveRange(_pos, sizeof(uint));
			_arr.InsertRange(_pos, BitConverter.GetBytes(n.SwapEndianness()));
			_pos += sizeof(uint);
		}

		public void WriteUnsignedShort(ushort n)
		{
			EnsureSpace(sizeof(ushort));
			_arr.RemoveRange(_pos, sizeof(ushort));
			_arr.InsertRange(_pos, BitConverter.GetBytes(n.SwapEndianness()));
			_pos += sizeof(ushort);
		}

		// TODO: writeUTF(value:String):void
		// TODO: writeUTFBytes(value:String):void

		public static ASByteArray HexStringToByteArray(string hex)
		{
			if (string.IsNullOrEmpty(hex)) { return new ASByteArray(); }

			if((hex.Length & 1) == 1)
			{
				hex = "0" + hex;
			}

			var ba = new ASByteArray
			{
				Length = hex.Length / 2
			};

			var bytes = Enumerable.Range(0, hex.Length).
				Where(x => x % 2 == 0).
				Select(x => Convert.ToByte(hex.Substring(x, 2), 16));

			var ind = 0;
			foreach(var b in bytes)
			{
				ba[ind++] = b;
			}

			return ba;
		}

		public static string ByteArrayToHexString(ASByteArray bytes) =>
			string.Concat(bytes._arr.Select(b => b.ToString("x2")));

		public static byte[] ByteArrayToManagedByteArray(ASByteArray bytes) =>
			bytes._arr.ToArray();

		public override string ToString() =>
			string.Concat(_arr.Select(Convert.ToChar));
			
	}
}
