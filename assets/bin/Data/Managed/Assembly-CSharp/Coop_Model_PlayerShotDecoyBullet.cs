using UnityEngine;

public class Coop_Model_PlayerShotDecoyBullet : Coop_Model_ObjectBase
{
	public int skIndex;

	public int decoyId;

	public string bulletName;

	public Vector3 position;

	public Coop_Model_PlayerShotDecoyBullet()
	{
		base.packetType = PACKET_TYPE.PLAYER_SHOT_DECOY_BULLET;
	}
}
