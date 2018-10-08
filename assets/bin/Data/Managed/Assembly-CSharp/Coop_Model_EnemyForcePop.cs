public class Coop_Model_EnemyForcePop : Coop_Model_Base
{
	public string psig;

	public int keyId;

	public int eid;

	public int lv;

	public int popType;

	public float x;

	public float z;

	public Coop_Model_EnemyForcePop()
	{
		base.packetType = PACKET_TYPE.ENEMY_FORCE_POP;
	}
}
