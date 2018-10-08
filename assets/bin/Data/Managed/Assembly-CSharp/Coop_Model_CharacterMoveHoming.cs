using UnityEngine;

public class Coop_Model_CharacterMoveHoming : Coop_Model_ObjectSyncPositionBase
{
	public float max_length;

	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public Coop_Model_CharacterMoveHoming()
	{
		base.packetType = PACKET_TYPE.CHARACTER_MOVE_HOMING;
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
