using UnityEngine;

public class Coop_Model_ObjectShotGimmickGenerator : Coop_Model_ObjectBase
{
	public Vector3 pos = Vector3.get_zero();

	public Coop_Model_ObjectShotGimmickGenerator()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.OBJECT_SHOT_GIMMICK_GENERATOR;
	}
}
