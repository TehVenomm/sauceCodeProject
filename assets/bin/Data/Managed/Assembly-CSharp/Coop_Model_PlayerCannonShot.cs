using UnityEngine;

public class Coop_Model_PlayerCannonShot : Coop_Model_ObjectSyncPositionBase
{
	public int cannonId;

	public Vector3 cannonVec;

	public Coop_Model_PlayerCannonShot()
	{
		base.packetType = PACKET_TYPE.PLAYER_CANNON_SHOT;
	}
}
