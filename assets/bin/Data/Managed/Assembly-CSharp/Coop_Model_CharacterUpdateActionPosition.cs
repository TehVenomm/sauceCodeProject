using UnityEngine;

public class Coop_Model_CharacterUpdateActionPosition : Coop_Model_ObjectBase
{
	public string trigger;

	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public Coop_Model_CharacterUpdateActionPosition()
	{
		base.packetType = PACKET_TYPE.CHARACTER_UPDATE_ACTION_POSITION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if ((character.actionID == Character.ACTION_ID.ATTACK || character.actionID == (Character.ACTION_ID)21) && (!character.actionPositionWaitSync || character.actionPositionWaitTrigger != trigger))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
