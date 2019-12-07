using UnityEngine;

public class Coop_Model_PlayerGather : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public int point_id;

	public Coop_Model_PlayerGather()
	{
		base.packetType = PACKET_TYPE.PLAYER_GATHER;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Character).IsChangeableAction((Character.ACTION_ID)28))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
