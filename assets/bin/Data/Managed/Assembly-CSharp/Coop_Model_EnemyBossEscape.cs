public class Coop_Model_EnemyBossEscape : Coop_Model_Base
{
	public int sid;

	public Coop_Model_EnemyBossEscape()
	{
		base.packetType = PACKET_TYPE.ENEMY_BOSS_ESCAPE;
	}

	public override string ToString()
	{
		return base.ToString() + ",sid=" + sid;
	}
}
