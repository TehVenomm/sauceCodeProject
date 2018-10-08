using UnityEngine;

public class Coop_Model_PlayerSetPresentBullet : Coop_Model_ObjectBase
{
	public int presentBulletId;

	public int type;

	public Vector3 position;

	public string bulletName;

	public Coop_Model_PlayerSetPresentBullet()
	{
		base.packetType = PACKET_TYPE.PLAYER_SET_PRESENT_BULLET;
	}
}
