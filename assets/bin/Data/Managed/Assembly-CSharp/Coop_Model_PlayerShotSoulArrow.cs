using System.Collections.Generic;
using UnityEngine;

public class Coop_Model_PlayerShotSoulArrow : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 shotPos = Vector3.zero;

	public Quaternion bowRot = default(Quaternion);

	public List<Vector3> targetPosList = new List<Vector3>();

	public Coop_Model_PlayerShotSoulArrow()
	{
		base.packetType = PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW;
	}
}
