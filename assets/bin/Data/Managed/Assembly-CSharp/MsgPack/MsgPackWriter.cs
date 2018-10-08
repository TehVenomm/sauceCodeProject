using System;
using System.IO;
using System.Text;

namespace MsgPack
{
	public class MsgPackWriter
	{
		private Stream _strm;

		private Encoder _encoder = Encoding.UTF8.GetEncoder();

		private byte[] _tmp = new byte[9];

		private byte[] _buf = new byte[64];

		public MsgPackWriter(Stream strm)
		{
			_strm = strm;
		}

		public void Write(byte x)
		{
			if (x < 128)
			{
				_strm.WriteByte(x);
			}
			else
			{
				byte[] tmp = _tmp;
				tmp[0] = 204;
				tmp[1] = x;
				_strm.Write(tmp, 0, 2);
			}
		}

		public void Write(ushort x)
		{
			if (x < 256)
			{
				Write((byte)x);
			}
			else
			{
				byte[] tmp = _tmp;
				tmp[0] = 205;
				tmp[1] = (byte)(x >> 8);
				tmp[2] = (byte)x;
				_strm.Write(tmp, 0, 3);
			}
		}

		public void Write(char x)
		{
			Write((ushort)x);
		}

		public void Write(uint x)
		{
			if (x < 65536)
			{
				Write((ushort)x);
			}
			else
			{
				byte[] tmp = _tmp;
				tmp[0] = 206;
				tmp[1] = (byte)(x >> 24);
				tmp[2] = (byte)(x >> 16);
				tmp[3] = (byte)(x >> 8);
				tmp[4] = (byte)x;
				_strm.Write(tmp, 0, 5);
			}
		}

		public void Write(ulong x)
		{
			if (x < 4294967296L)
			{
				Write((uint)x);
			}
			else
			{
				byte[] tmp = _tmp;
				tmp[0] = 207;
				tmp[1] = (byte)(x >> 56);
				tmp[2] = (byte)(x >> 48);
				tmp[3] = (byte)(x >> 40);
				tmp[4] = (byte)(x >> 32);
				tmp[5] = (byte)(x >> 24);
				tmp[6] = (byte)(x >> 16);
				tmp[7] = (byte)(x >> 8);
				tmp[8] = (byte)x;
				_strm.Write(tmp, 0, 9);
			}
		}

		public void Write(sbyte x)
		{
			if (x >= -32 && x <= -1)
			{
				_strm.WriteByte((byte)(0xE0 | (byte)x));
			}
			else if (x >= 0 && x <= 127)
			{
				_strm.WriteByte((byte)x);
			}
			else
			{
				byte[] tmp = _tmp;
				tmp[0] = 208;
				tmp[1] = (byte)x;
				_strm.Write(tmp, 0, 2);
			}
		}

		public void Write(short x)
		{
			if (x >= -128 && x <= 127)
			{
				Write((sbyte)x);
			}
			else
			{
				byte[] tmp = _tmp;
				tmp[0] = 209;
				tmp[1] = (byte)(x >> 8);
				tmp[2] = (byte)x;
				_strm.Write(tmp, 0, 3);
			}
		}

		public void Write(int x)
		{
			if (x >= -32768 && x <= 32767)
			{
				Write((short)x);
			}
			else
			{
				byte[] tmp = _tmp;
				tmp[0] = 210;
				tmp[1] = (byte)(x >> 24);
				tmp[2] = (byte)(x >> 16);
				tmp[3] = (byte)(x >> 8);
				tmp[4] = (byte)x;
				_strm.Write(tmp, 0, 5);
			}
		}

		public void Write(long x)
		{
			if (x >= -2147483648 && x <= 2147483647)
			{
				Write((int)x);
			}
			else
			{
				byte[] tmp = _tmp;
				tmp[0] = 211;
				tmp[1] = (byte)(x >> 56);
				tmp[2] = (byte)(x >> 48);
				tmp[3] = (byte)(x >> 40);
				tmp[4] = (byte)(x >> 32);
				tmp[5] = (byte)(x >> 24);
				tmp[6] = (byte)(x >> 16);
				tmp[7] = (byte)(x >> 8);
				tmp[8] = (byte)x;
				_strm.Write(tmp, 0, 9);
			}
		}

		public void WriteNil()
		{
			_strm.WriteByte(192);
		}

		public void Write(bool x)
		{
			_strm.WriteByte((byte)((!x) ? 194 : 195));
		}

		public void Write(float x)
		{
			byte[] bytes = BitConverter.GetBytes(x);
			byte[] tmp = _tmp;
			tmp[0] = 202;
			if (BitConverter.IsLittleEndian)
			{
				tmp[1] = bytes[3];
				tmp[2] = bytes[2];
				tmp[3] = bytes[1];
				tmp[4] = bytes[0];
			}
			else
			{
				tmp[1] = bytes[0];
				tmp[2] = bytes[1];
				tmp[3] = bytes[2];
				tmp[4] = bytes[3];
			}
			_strm.Write(tmp, 0, 5);
		}

		public void Write(double x)
		{
			byte[] bytes = BitConverter.GetBytes(x);
			byte[] tmp = _tmp;
			tmp[0] = 203;
			if (BitConverter.IsLittleEndian)
			{
				tmp[1] = bytes[7];
				tmp[2] = bytes[6];
				tmp[3] = bytes[5];
				tmp[4] = bytes[4];
				tmp[5] = bytes[3];
				tmp[6] = bytes[2];
				tmp[7] = bytes[1];
				tmp[8] = bytes[0];
			}
			else
			{
				tmp[1] = bytes[0];
				tmp[2] = bytes[1];
				tmp[3] = bytes[2];
				tmp[4] = bytes[3];
				tmp[5] = bytes[4];
				tmp[6] = bytes[5];
				tmp[7] = bytes[6];
				tmp[8] = bytes[7];
			}
			_strm.Write(tmp, 0, 9);
		}

		public void Write(byte[] bytes)
		{
			WriteRawHeader(bytes.Length);
			_strm.Write(bytes, 0, bytes.Length);
		}

		public void WriteRawHeader(int N)
		{
			WriteLengthHeader(N, 32, 160, 218, 219);
		}

		public void WriteArrayHeader(int N)
		{
			WriteLengthHeader(N, 16, 144, 220, 221);
		}

		public void WriteMapHeader(int N)
		{
			WriteLengthHeader(N, 16, 128, 222, 223);
		}

		private void WriteLengthHeader(int N, int fix_length, byte fix_prefix, byte len16bit_prefix, byte len32bit_prefix)
		{
			if (N < fix_length)
			{
				_strm.WriteByte((byte)(fix_prefix | N));
			}
			else
			{
				byte[] tmp = _tmp;
				int count;
				if (N < 65536)
				{
					tmp[0] = len16bit_prefix;
					tmp[1] = (byte)(N >> 8);
					tmp[2] = (byte)N;
					count = 3;
				}
				else
				{
					tmp[0] = len32bit_prefix;
					tmp[1] = (byte)(N >> 24);
					tmp[2] = (byte)(N >> 16);
					tmp[3] = (byte)(N >> 8);
					tmp[4] = (byte)N;
					count = 5;
				}
				_strm.Write(tmp, 0, count);
			}
		}

		public void Write(string x)
		{
			Write(x, false);
		}

		public void Write(string x, bool highProbAscii)
		{
			Write(x, _buf, highProbAscii);
		}

		public void Write(string x, byte[] buf)
		{
			Write(x, buf, false);
		}

		public void Write(string x, byte[] buf, bool highProbAscii)
		{
			Encoder encoder = _encoder;
			char[] chars = x.ToCharArray();
			if (highProbAscii && x.Length <= buf.Length)
			{
				bool flag = true;
				for (int i = 0; i < x.Length; i++)
				{
					int num = x[i];
					if (num > 127)
					{
						flag = false;
						break;
					}
					buf[i] = (byte)num;
				}
				if (flag)
				{
					WriteRawHeader(x.Length);
					_strm.Write(buf, 0, x.Length);
					return;
				}
			}
			WriteRawHeader(encoder.GetByteCount(chars, 0, x.Length, true));
			int num2 = x.Length;
			bool completed = true;
			int num3 = 0;
			while (num2 > 0 || !completed)
			{
				encoder.Convert(chars, num3, num2, buf, 0, buf.Length, false, out int charsUsed, out int bytesUsed, out completed);
				_strm.Write(buf, 0, bytesUsed);
				num2 -= charsUsed;
				num3 += charsUsed;
			}
		}
	}
}
