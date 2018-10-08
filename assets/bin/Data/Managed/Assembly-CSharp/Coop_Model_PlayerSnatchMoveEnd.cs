using UnityEngine;

public class Coop_Model_PlayerSnatchMoveEnd : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public int triggerIndex;

	public Coop_Model_PlayerSnatchMoveEnd()
	{
		base.packetType = PACKET_TYPE.PLAYER_SNATCH_MOVE_END;
	}
}
