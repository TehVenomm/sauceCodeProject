using System;
using System.Text;

public class PacketStream
{
	private enum TYPE
	{
		NONE,
		BUFFER,
		STRING
	}

	private object stream;

	private TYPE type;

	public int Length
	{
		get
		{
			switch (type)
			{
			case TYPE.BUFFER:
				return ((byte[])stream).Length;
			case TYPE.STRING:
				return ((string)stream).Length;
			default:
				return 0;
			}
		}
	}

	public PacketStream(object stream)
	{
		this.stream = stream;
		Type type = stream.GetType();
		if (type == typeof(byte[]))
		{
			this.type = TYPE.BUFFER;
		}
		else if (type == typeof(string))
		{
			this.type = TYPE.STRING;
		}
	}

	public bool IsBuffer()
	{
		return type == TYPE.BUFFER;
	}

	public bool IsString()
	{
		return type == TYPE.STRING;
	}

	public byte[] ToBuffer()
	{
		switch (type)
		{
		case TYPE.BUFFER:
			return (byte[])stream;
		case TYPE.STRING:
			return Encoding.ASCII.GetBytes((string)stream);
		default:
			return new byte[0];
		}
	}

	public override string ToString()
	{
		switch (type)
		{
		case TYPE.BUFFER:
			return BitConverter.ToString((byte[])stream);
		case TYPE.STRING:
			return (string)stream;
		default:
			return string.Empty;
		}
	}
}
