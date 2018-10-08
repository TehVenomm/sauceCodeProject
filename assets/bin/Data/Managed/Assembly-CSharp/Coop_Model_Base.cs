using System;

[Serializable]
public class Coop_Model_Base
{
	public enum CLIENT_TYPE
	{
		NONE,
		ANDROID,
		IOS
	}

	public int c;

	public int id = -1;

	public bool r;

	public float lt;

	public int ct;

	public int u;

	protected PACKET_TYPE packetType
	{
		get
		{
			return (PACKET_TYPE)c;
		}
		set
		{
			c = (int)value;
		}
	}

	public static CLIENT_TYPE GetClientType()
	{
		return CLIENT_TYPE.ANDROID;
	}

	public virtual bool IsPromiseOverAgainCheck()
	{
		return false;
	}

	public override string ToString()
	{
		return $"Packet({(PACKET_TYPE)c}) to object `{id}'. r={r},lt={lt},ct={(CLIENT_TYPE)ct},u={u}";
	}
}
