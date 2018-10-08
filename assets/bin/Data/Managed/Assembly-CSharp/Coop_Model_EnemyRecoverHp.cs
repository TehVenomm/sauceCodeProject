public class Coop_Model_EnemyRecoverHp : Coop_Model_ObjectBase
{
	public int value;

	public Coop_Model_EnemyRecoverHp()
	{
		base.packetType = PACKET_TYPE.ENEMY_RECOVER_HP;
	}
}
