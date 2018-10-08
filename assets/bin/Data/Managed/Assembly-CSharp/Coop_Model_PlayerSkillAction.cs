using UnityEngine;

public class Coop_Model_PlayerSkillAction : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public int skill_index;

	public bool isUsingSecondGrade;

	public Coop_Model_PlayerSkillAction()
	{
		base.packetType = PACKET_TYPE.PLAYER_SKILL_ACTION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction((Character.ACTION_ID)21))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
