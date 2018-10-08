public class Coop_Model_EnemyWarp : Coop_Model_ObjectSyncPositionBase
{
	public Coop_Model_EnemyWarp()
	{
		base.packetType = PACKET_TYPE.ENEMY_WARP;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Enemy enemy = owner as Enemy;
		if (enemy.actionID == Character.ACTION_ID.ATTACK && !enemy.warpWaitSync)
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
