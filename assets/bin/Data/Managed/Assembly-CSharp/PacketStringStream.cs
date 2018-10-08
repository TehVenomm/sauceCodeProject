public class PacketStringStream
{
	private string stream = string.Empty;

	private int position;

	public int Position => position;

	public PacketStringStream()
	{
	}

	public PacketStringStream(string stream)
	{
		this.stream = stream;
	}

	public void Write(string str)
	{
		stream += str;
		position += str.Length;
	}

	public void WriteInt(int val)
	{
		Write(val.ToString());
	}

	public string Read(int len)
	{
		string result = stream.Substring(position, len);
		position += len;
		return result;
	}

	public string Read()
	{
		string result = stream.Substring(position);
		position = stream.Length;
		return result;
	}

	public override string ToString()
	{
		return stream;
	}

	public string Substring(int start, int len = 0)
	{
		return (len != 0) ? stream.Substring(start, len) : stream.Substring(start);
	}

	public void Close()
	{
		stream = string.Empty;
		position = 0;
	}
}
