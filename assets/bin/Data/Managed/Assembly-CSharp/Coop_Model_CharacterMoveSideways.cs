using UnityEngine;

public class Coop_Model_CharacterMoveSideways : Coop_Model_ObjectSyncPositionBase
{
	public int moveAngleSign;

	public Vector3 actionPos = Vector3.zero;

	public bool actionPosFlag;

	public Coop_Model_CharacterMoveSideways()
	{
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
