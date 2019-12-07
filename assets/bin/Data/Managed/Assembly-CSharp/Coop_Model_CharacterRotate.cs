public class Coop_Model_CharacterRotate : Coop_Model_ObjectSyncPositionBase
{
	public float target_dir;

	public Coop_Model_CharacterRotate()
	{
		base.packetType = PACKET_TYPE.CHARACTER_ROTATE;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Character).IsChangeableAction(Character.ACTION_ID.ROTATE))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
