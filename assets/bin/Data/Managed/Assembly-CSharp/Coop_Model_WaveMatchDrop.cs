using System.Collections.Generic;

public class Coop_Model_WaveMatchDrop : Coop_Model_Base
{
	public List<int> fiIds;

	public float sec;

	public int x;

	public int z;

	public Coop_Model_WaveMatchDrop()
	{
		base.packetType = PACKET_TYPE.WAVEMATCH_DROP;
	}

	public override string ToString()
	{
		string str_fiIds = "";
		if (fiIds != null)
		{
			fiIds.ForEach(delegate(int id)
			{
				str_fiIds = str_fiIds + id + ",";
			});
		}
		string str = "";
		str = str + ",id=" + str_fiIds.Trim(',');
		str = str + ",pos(" + x + ", " + z + ")";
		str = str + ",sec=" + sec;
		return base.ToString() + str;
	}
}
