public class Coop_Model_RoomJoined : Coop_Model_Base
{
	public int cid;

	public int stgid;

	public int stgidx;

	public int stghostid;

	public Coop_Model_RoomJoined()
	{
		base.packetType = PACKET_TYPE.ROOM_JOINED;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",stgid=" + stgid;
		empty = empty + ",stgidx=" + stgidx;
		empty = empty + ",stghostid=" + stghostid;
		return base.ToString() + empty;
	}
}
