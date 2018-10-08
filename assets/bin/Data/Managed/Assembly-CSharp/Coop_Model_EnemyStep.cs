public class Coop_Model_EnemyStep : Coop_Model_ObjectSyncPositionBase
{
	public int motion_id;

	public Coop_Model_EnemyStep()
	{
		base.packetType = PACKET_TYPE.ENEMY_STEP;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction(Character.ACTION_ID.MAX))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
