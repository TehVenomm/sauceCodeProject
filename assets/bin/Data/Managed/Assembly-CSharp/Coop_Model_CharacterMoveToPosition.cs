using UnityEngine;

public class Coop_Model_CharacterMoveToPosition : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 target_pos = Vector3.zero;

	public Coop_Model_CharacterMoveToPosition()
	{
		base.packetType = PACKET_TYPE.CHARACTER_MOVE_TO_POSITION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Character).IsChangeableAction(Character.ACTION_ID.MOVE))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
