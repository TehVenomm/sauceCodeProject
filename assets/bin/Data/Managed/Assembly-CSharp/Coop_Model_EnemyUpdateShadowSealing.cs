public class Coop_Model_EnemyUpdateShadowSealing : Coop_Model_ObjectBase
{
	public Enemy.ShadowSealingSyncData sync_data = new Enemy.ShadowSealingSyncData();

	public Coop_Model_EnemyUpdateShadowSealing()
	{
		base.packetType = PACKET_TYPE.ENEMY_UPDATE_SHADOWSEALING;
	}
}
