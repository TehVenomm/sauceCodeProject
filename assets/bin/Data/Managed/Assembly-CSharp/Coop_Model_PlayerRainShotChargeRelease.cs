using UnityEngine;

public class Coop_Model_PlayerRainShotChargeRelease : Coop_Model_ObjectBase
{
	public Vector3 fallPos;

	public float fallRotY;

	public Coop_Model_PlayerRainShotChargeRelease()
	{
		base.packetType = PACKET_TYPE.PLAYER_RAIN_SHOT_CHARGE_RELEASE;
	}
}
