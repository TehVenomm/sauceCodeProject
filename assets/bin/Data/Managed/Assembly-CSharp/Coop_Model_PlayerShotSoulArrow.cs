using System.Collections.Generic;
using UnityEngine;

public class Coop_Model_PlayerShotSoulArrow : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 shotPos = Vector3.get_zero();

	public Quaternion bowRot = default(Quaternion);

	public List<Vector3> targetPosList = new List<Vector3>();

	public Coop_Model_PlayerShotSoulArrow()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW;
	}
}
