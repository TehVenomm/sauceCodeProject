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
		string str_fiIds = string.Empty;
		if (fiIds != null)
		{
			fiIds.ForEach(delegate(int id)
			{
				str_fiIds = str_fiIds + id + ",";
			});
		}
		string empty = string.Empty;
		empty = empty + ",id=" + str_fiIds.Trim(',');
		string text = empty;
		empty = text + ",pos(" + x + ", " + z + ")";
		empty = empty + ",sec=" + sec;
		return base.ToString() + empty;
	}
}
