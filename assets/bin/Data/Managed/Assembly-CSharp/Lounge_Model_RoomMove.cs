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
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",pos=" + pos;
		return base.ToString() + empty;
	}
}
