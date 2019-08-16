using UnityEngine;

public class Coop_Model_WaveMatchDropCreate : Coop_Model_Base
{
	public int managedId;

	public uint dataId;

	public Vector3 basePos;

	public Vector3 offset;

	public float sec;

	public Coop_Model_WaveMatchDropCreate()
	{
		base.packetType = PACKET_TYPE.WAVEMATCH_DROP_CREATE;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",mid=" + managedId.ToString();
		empty = empty + ",did=" + dataId.ToString();
		string text = empty;
		empty = text + ",pos(" + basePos.x + ", " + basePos.z + ")";
		text = empty;
		empty = text + ",offset(" + offset.x + ", " + offset.z + ")";
		empty = empty + ",sec=" + sec;
		return base.ToString() + empty;
	}
}
