public class Coop_Model_PlayerExplodeDecoyBullet : Coop_Model_ObjectBase
{
	public int decoyId;

	public Coop_Model_PlayerExplodeDecoyBullet()
	{
		base.packetType = PACKET_TYPE.PLAYER_EXPLODE_DECOY_BULLET;
	}
}
