using UnityEngine;

public class Coop_Model_PlayerShotZoneBullet : Coop_Model_ObjectBase
{
	public string bulletName;

	public Vector3 position;

	public Coop_Model_PlayerShotZoneBullet()
	{
		base.packetType = PACKET_TYPE.PLAYER_SHOT_ZONE_BULLET;
	}
}
