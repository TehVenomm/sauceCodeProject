using UnityEngine;

public class Lounge_Model_RoomMove : Coop_Model_Base
{
	public int cid;

	public Vector3 pos;

	public Lounge_Model_RoomMove()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_MOVE;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",pos=" + pos;
		return base.ToString() + arg;
	}
}
