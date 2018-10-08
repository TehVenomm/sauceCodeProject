public class Coop_Model_EnemyUpdateBleedDamage : Coop_Model_ObjectBase
{
	public Enemy.BleedSyncData sync_data = new Enemy.BleedSyncData();

	public Coop_Model_EnemyUpdateBleedDamage()
	{
		base.packetType = PACKET_TYPE.ENEMY_UPDATE_BLEED_DAMAGE;
	}
}
