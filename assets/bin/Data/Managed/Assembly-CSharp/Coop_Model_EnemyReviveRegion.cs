public class Coop_Model_EnemyReviveRegion : Coop_Model_ObjectBase
{
	public int region_id = -1;

	public Coop_Model_EnemyReviveRegion()
	{
		base.packetType = PACKET_TYPE.ENEMY_REVIVE_REGION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Enemy enemy = owner as Enemy;
		if (enemy.actionID == Character.ACTION_ID.ATTACK && !enemy.reviveRegionWaitSync)
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
