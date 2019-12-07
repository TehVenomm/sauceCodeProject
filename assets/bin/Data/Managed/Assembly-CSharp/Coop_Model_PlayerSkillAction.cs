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
		if (!(owner as Character).IsChangeableAction((Character.ACTION_ID)22))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
