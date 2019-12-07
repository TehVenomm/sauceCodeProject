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
		string str = "";
		str = str + ",mid=" + managedId.ToString();
		str = str + ",did=" + dataId.ToString();
		str = str + ",pos(" + basePos.x + ", " + basePos.z + ")";
		str = str + ",offset(" + offset.x + ", " + offset.z + ")";
		str = str + ",sec=" + sec;
		return base.ToString() + str;
	}
}
