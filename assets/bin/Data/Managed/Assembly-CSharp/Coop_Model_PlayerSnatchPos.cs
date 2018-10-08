using UnityEngine;

public class Coop_Model_PlayerSnatchPos : Coop_Model_ObjectBase
{
	public int enemyId;

	public Vector3 hitPoint;

	public Coop_Model_PlayerSnatchPos()
	{
		base.packetType = PACKET_TYPE.PLAYER_SNATCH_POS;
	}
}
