using UnityEngine;

public class Coop_Model_CharacterMoveLookAt : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 moveLookAtPos = Vector3.get_zero();

	public Coop_Model_CharacterMoveLookAt()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.CHARACTER_MOVE_LOOKAT;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction(Character.ACTION_ID.MOVE_POINT))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
