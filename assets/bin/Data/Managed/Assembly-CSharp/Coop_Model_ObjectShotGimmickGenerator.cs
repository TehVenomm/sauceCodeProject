using UnityEngine;

public class Coop_Model_ObjectShotGimmickGenerator : Coop_Model_ObjectBase
{
	public Vector3 pos = Vector3.zero;

	public Coop_Model_ObjectShotGimmickGenerator()
	{
		base.packetType = PACKET_TYPE.OBJECT_SHOT_GIMMICK_GENERATOR;
	}
}
