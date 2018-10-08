using System;

[Serializable]
public class ChatPacketHeader
{
	protected const string reserved = "00";

	public static readonly int LENGTH = 40;

	public int version
	{
		get;
		protected set;
	}

	public int cmd
	{
		get;
		protected set;
	}

	public string fromId
	{
		get;
		protected set;
	}

	public ChatPacketHeader()
	{
	}

	public ChatPacketHeader(int _version, int _cmd, string _fromId)
	{
		version = _version;
		cmd = _cmd;
		fromId = _fromId;
	}

	public override string ToString()
	{
		long result = 0L;
		long.TryParse(fromId, out result);
		return string.Format("{0:D2}{1:D4}{2}{3:D32}", version, cmd, "00", result);
	}

	public static ChatPacketHeader Parse(string str)
	{
		string s = str.Substring(0, 2);
		string s2 = str.Substring(2, 4);
		string fromId = str.Substring(8, 32);
		return new ChatPacketHeader(int.Parse(s), int.Parse(s2), fromId);
	}
}
