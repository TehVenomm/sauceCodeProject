using UnityEngine;

public class Coop_Model_CharacterMoveToPosition : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 target_pos = Vector3.get_zero();

	public Coop_Model_CharacterMoveToPosition()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.CHARACTER_MOVE_TO_POSITION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
