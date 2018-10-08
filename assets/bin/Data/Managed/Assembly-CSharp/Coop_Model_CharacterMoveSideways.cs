using UnityEngine;

public class Coop_Model_CharacterMoveSideways : Coop_Model_ObjectSyncPositionBase
{
	public int moveAngleSign;

	public Vector3 actionPos = Vector3.get_zero();

	public bool actionPosFlag;

	public Coop_Model_CharacterMoveSideways()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.CHARACTER_MOVE_SIDEWAYS;
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
