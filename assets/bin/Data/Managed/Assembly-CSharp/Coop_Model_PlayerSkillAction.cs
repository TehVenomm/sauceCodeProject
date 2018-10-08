using UnityEngine;

public class Coop_Model_PlayerSkillAction : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.get_zero();

	public bool act_pos_f;

	public int skill_index;

	public Coop_Model_PlayerSkillAction()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.PLAYER_SKILL_ACTION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction((Character.ACTION_ID)20))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
