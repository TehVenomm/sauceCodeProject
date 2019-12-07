using UnityEngine;

public class Coop_Model_CharacterMoveVelocityEnd : Coop_Model_ObjectBase
{
	public float time;

	public Vector3 pos = Vector3.zero;

	public float direction;

	public float sync_speed;

	public int motion_id;

	public Coop_Model_CharacterMoveVelocityEnd()
	{
		base.packetType = PACKET_TYPE.CHARACTER_MOVE_VELOCITY_END;
	}

	public override Vector3 GetObjectPosition()
	{
		return pos;
	}

	public override bool IsHaveObjectPosition()
	{
		return true;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		bool flag = false;
		if (character.actionID == Character.ACTION_ID.MOVE && character.moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
		{
			flag = true;
		}
		if (!character.IsChangeableAction(Character.ACTION_ID.MOVE) && !flag)
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
