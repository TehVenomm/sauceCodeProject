using UnityEngine;

public class Coop_Model_CharacterMoveHoming : Coop_Model_ObjectSyncPositionBase
{
	public float max_length;

	public Vector3 act_pos = Vector3.get_zero();

	public bool act_pos_f;

	public Coop_Model_CharacterMoveHoming()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
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
