using UnityEngine;

public class Coop_Model_PlayerSpecialActionContinue : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public bool isHitSpAttack;

	public Coop_Model_PlayerSpecialActionContinue()
	{
		base.packetType = PACKET_TYPE.PLAYER_SPECIAL_ACTION_CONTINUE;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Player).IsChangeableAction(Character.ACTION_ID.ATTACK))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
