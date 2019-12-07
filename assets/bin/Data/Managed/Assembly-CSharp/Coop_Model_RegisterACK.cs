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
		string text = "";
		int i = 0;
		for (int count = ids.Count; i < count; i++)
		{
			text = text + "(" + ids[i] + "," + stgids[i] + "," + stgidxs[i] + "," + stghosts[i].ToString() + "),";
		}
		return base.ToString() + ",clients=" + text;
	}
}
