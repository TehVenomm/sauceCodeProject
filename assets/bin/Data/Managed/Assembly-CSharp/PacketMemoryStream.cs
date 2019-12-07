using System;
using System.IO;

public class PacketMemoryStream : MemoryStream
{
	public PacketMemoryStream()
	{
	}

	public PacketMemoryStream(byte[] data)
		: base(data)
	{
	}

	private byte[] GetBytes(int val)
	{
		byte[] bytes = BitConverter.GetBytes(val);
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse((Array)bytes);
		}
		return bytes;
	}

	private int ToInt32(byte[] bytes)
	{
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse((Array)bytes);
		}
		return BitConverter.ToInt32(bytes, 0);
	}

	public void WriteInt32(int val)
	{
		byte[] bytes = GetBytes(val);
		Write(bytes, 0, bytes.Length);
	}

	public void WriteBytes(byte[] bytes)
	{
		Write(bytes, 0, bytes.Length);
	}

	public int ReadInt32()
	{
		byte[] bytes = GetBytes(0);
		Read(bytes, 0, bytes.Length);
		return ToInt32(bytes);
	}

	public byte[] ReadBytes(int len)
	{
		byte[] array = new byte[len];
		Read(array, 0, array.Length);
		return array;
	}

	public override string ToString()
	{
		return $"{Length}/{Capacity}({Position}): {BitConverter.ToString(ToArray())}";
	}
}
