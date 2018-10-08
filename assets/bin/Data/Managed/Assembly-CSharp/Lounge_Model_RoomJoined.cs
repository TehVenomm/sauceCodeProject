public class Lounge_Model_RoomJoined : Coop_Model_Base
{
	public int cid;

	public Lounge_Model_RoomJoined()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_JOINED;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		return base.ToString() + empty;
	}
}
