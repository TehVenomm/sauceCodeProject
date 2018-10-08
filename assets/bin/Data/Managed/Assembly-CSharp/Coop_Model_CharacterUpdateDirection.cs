public class Coop_Model_CharacterUpdateDirection : Coop_Model_ObjectBase
{
	public string trigger;

	public float dir;

	public float lerp_dir;

	public Coop_Model_CharacterUpdateDirection()
	{
		base.packetType = PACKET_TYPE.CHARACTER_UPDATE_DIRECTION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if ((character.actionID == Character.ACTION_ID.ATTACK || character.actionID == (Character.ACTION_ID)21) && (!character.directionWaitSync || character.directionWaitTrigger != trigger))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
