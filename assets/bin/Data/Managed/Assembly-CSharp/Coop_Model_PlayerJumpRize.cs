using UnityEngine;

public class Coop_Model_PlayerJumpRize : Coop_Model_ObjectBase
{
	public Vector3 dir;

	public int level;

	public Coop_Model_PlayerJumpRize()
	{
		base.packetType = PACKET_TYPE.PLAYER_JUMP_RIZE;
	}
}
