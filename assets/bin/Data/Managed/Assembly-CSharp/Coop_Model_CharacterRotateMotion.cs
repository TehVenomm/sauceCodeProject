public class Coop_Model_CharacterRotateMotion : Coop_Model_ObjectSyncPositionBase
{
	public float target_dir;

	public Coop_Model_CharacterRotateMotion()
	{
		base.packetType = PACKET_TYPE.CHARACTER_ROTATE_MOTION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction(Character.ACTION_ID.ROTATE))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
