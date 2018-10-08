using UnityEngine;

public class Coop_Model_PlayerSnatchMoveEnd : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.get_zero();

	public bool act_pos_f;

	public int triggerIndex;

	public Coop_Model_PlayerSnatchMoveEnd()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.PLAYER_SNATCH_MOVE_END;
	}
}
