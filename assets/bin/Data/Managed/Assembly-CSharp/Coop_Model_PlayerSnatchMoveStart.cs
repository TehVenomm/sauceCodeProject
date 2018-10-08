using UnityEngine;

public class Coop_Model_PlayerSnatchMoveStart : Coop_Model_ObjectBase
{
	public Vector3 snatchPos = Vector3.zero;

	public Coop_Model_PlayerSnatchMoveStart()
	{
		base.packetType = PACKET_TYPE.PLAYER_SNATCH_MOVE_START;
	}
}
