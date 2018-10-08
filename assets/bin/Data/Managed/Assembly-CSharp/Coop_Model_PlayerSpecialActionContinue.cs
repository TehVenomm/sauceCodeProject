using UnityEngine;

public class Coop_Model_PlayerSpecialActionContinue : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.get_zero();

	public bool act_pos_f;

	public bool isHitSpAttack;

	public Coop_Model_PlayerSpecialActionContinue()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.PLAYER_SPECIAL_ACTION_CONTINUE;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Player player = owner as Player;
		if (!player.IsChangeableAction(Character.ACTION_ID.ATTACK))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
