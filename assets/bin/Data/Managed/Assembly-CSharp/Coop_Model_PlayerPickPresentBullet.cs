public class Coop_Model_PlayerPickPresentBullet : Coop_Model_ObjectBase
{
	public int presentBulletId;

	public Coop_Model_PlayerPickPresentBullet()
	{
		base.packetType = PACKET_TYPE.PLAYER_PICK_PRESENT_BULLET;
	}
}
