public class Coop_Model_EnemyUpdateBombArrow : Coop_Model_ObjectBase
{
	public int regionId;

	public Coop_Model_EnemyUpdateBombArrow()
	{
		base.packetType = PACKET_TYPE.ENEMY_UPDATE_BOMBARROW;
	}
}
