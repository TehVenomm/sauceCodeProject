using System.Collections.Generic;

public class Coop_Model_RegisterACK : Coop_Model_ACK
{
	public int sid;

	public bool of;

	public List<int> ids = new List<int>();

	public List<int> stgids = new List<int>();

	public List<int> stgidxs = new List<int>();

	public List<bool> stghosts = new List<bool>();

	public Coop_Model_RegisterACK()
	{
		base.packetType = PACKET_TYPE.REGISTER_ACK;
	}

	public override string ToString()
	{
		string text = string.Empty;
		int i = 0;
		for (int count = ids.Count; i < count; i++)
		{
			string text2 = text;
			text = text2 + "(" + ids[i] + "," + stgids[i] + "," + stgidxs[i] + "," + stghosts[i] + "),";
		}
		return base.ToString() + ",clients=" + text;
	}
}
