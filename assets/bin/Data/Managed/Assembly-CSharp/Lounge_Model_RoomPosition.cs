using UnityEngine;

public class Lounge_Model_RoomPosition : Coop_Model_Base
{
	public int cid;

	public int aid;

	public Vector3 pos;

	public Lounge_Model_RoomPosition()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_POSITION;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",pos=" + pos;
		return base.ToString() + arg;
	}
}
