using Ionic.Zlib;
using System.IO;
using System.Text;

public class BinaryTableReader
{
	private BinaryReader reader;

	private int allDataSize;

	private int rowSize;

	private int currentPosition;

	private byte[] tmpBuffer = new byte[1024];

	private int maxByteBufferSize_;

	private int maxCharBufferSize_;

	private byte[] byteBuffer_;

	private char[] charBuffer_;

	private Encoding encoding = new UTF8Encoding();

	public BinaryTableReader(byte[] bytes)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		MemoryStream memoryStream = new MemoryStream(bytes);
		memoryStream.Seek(256L, SeekOrigin.Begin);
		ZlibStream input = new ZlibStream((Stream)memoryStream, 1);
		reader = new BinaryReader((Stream)input);
		allDataSize = bytes.Length - 256;
	}

	public BinaryTableReader(MemoryStream stream)
	{
		reader = new BinaryReader(stream);
		allDataSize = (int)stream.Length;
	}

	public bool MoveNext()
	{
		int num = rowSize - currentPosition;
		if (num > 0)
		{
			reader.Read(tmpBuffer, 0, num);
		}
		if (reader.BaseStream.Position < allDataSize)
		{
			rowSize = reader.ReadInt32();
			currentPosition = 0;
			return true;
		}
		return false;
	}

	public bool ReadBoolean(bool defaultValue = false)
	{
		bool result;
		if (currentPosition < rowSize)
		{
			result = reader.ReadBoolean();
			currentPosition++;
		}
		else
		{
			result = defaultValue;
		}
		return result;
	}

	public int ReadInt32(int defaultValue = 0)
	{
		int result;
		if (currentPosition < rowSize)
		{
			result = reader.ReadInt32();
			currentPosition += 4;
		}
		else
		{
			result = defaultValue;
		}
		return result;
	}

	public uint ReadUInt32(uint defaultValue = 0u)
	{
		uint result;
		if (currentPosition < rowSize)
		{
			result = reader.ReadUInt32();
			currentPosition += 4;
		}
		else
		{
			result = defaultValue;
		}
		return result;
	}

	public float ReadSingle(float defaultValue = 0f)
	{
		float result;
		if (currentPosition < rowSize)
		{
			result = reader.ReadSingle();
			currentPosition += 4;
		}
		else
		{
			result = defaultValue;
		}
		return result;
	}

	public string ReadString(string defaultValue = "")
	{
		string result;
		if (currentPosition < rowSize)
		{
			int num = Read7BitEncodedInt();
			if (0 > num)
			{
				return defaultValue;
			}
			if (num == 0)
			{
				return defaultValue;
			}
			if (maxByteBufferSize_ < num)
			{
				maxByteBufferSize_ = num;
				byteBuffer_ = new byte[maxByteBufferSize_];
				maxCharBufferSize_ = encoding.GetMaxCharCount(maxByteBufferSize_);
				charBuffer_ = new char[maxCharBufferSize_];
			}
			if (byteBuffer_ == null || charBuffer_ == null)
			{
				maxByteBufferSize_ = 0;
				maxCharBufferSize_ = 0;
				return defaultValue;
			}
			reader.Read(byteBuffer_, 0, num);
			int chars = encoding.GetChars(byteBuffer_, 0, num, charBuffer_, 0);
			result = new string(charBuffer_, 0, chars);
			currentPosition += num;
		}
		else
		{
			result = defaultValue;
		}
		return result;
	}

	public byte ReadByte(byte defaultValue = 0)
	{
		byte result;
		if (currentPosition < rowSize)
		{
			result = reader.ReadByte();
			currentPosition++;
		}
		else
		{
			result = defaultValue;
		}
		return result;
	}

	private int Read7BitEncodedInt()
	{
		int num = 0;
		int num2 = 0;
		byte b;
		do
		{
			b = ReadByte(0);
			num |= (b & 0x7F) << num2;
			num2 += 7;
		}
		while ((b & 0x80) != 0);
		return num;
	}
}
