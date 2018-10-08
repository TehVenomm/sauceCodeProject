using UnityEngine;

public class Coop_Model_PlayerCannonRotate : Coop_Model_ObjectBase
{
	public Vector3 cannonVec;

	public Coop_Model_PlayerCannonRotate()
	{
		base.packetType = PACKET_TYPE.PLAYER_CANNON_ROTATE;
	}
}
