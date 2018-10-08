public class Coop_Model_EnemySyncTarget : Coop_Model_ObjectBase
{
	public int targetId;

	public Coop_Model_EnemySyncTarget()
	{
		base.packetType = PACKET_TYPE.ENEMY_SYNC_TARGET;
	}
}
