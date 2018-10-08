using UnityEngine;

public class Coop_Model_PlayerJumpEnd : Coop_Model_ObjectBase
{
	public bool isSuccess;

	public Vector3 pos;

	public float y;

	public Coop_Model_PlayerJumpEnd()
	{
		base.packetType = PACKET_TYPE.PLAYER_JUMP_END;
	}
}
