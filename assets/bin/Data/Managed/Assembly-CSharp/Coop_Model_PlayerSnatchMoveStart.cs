using UnityEngine;

public class Coop_Model_PlayerSnatchMoveStart : Coop_Model_ObjectBase
{
	public Vector3 snatchPos = Vector3.get_zero();

	public Coop_Model_PlayerSnatchMoveStart()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.PLAYER_SNATCH_MOVE_START;
	}
}
