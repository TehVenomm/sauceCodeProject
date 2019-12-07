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
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",stgid=" + stgid;
		arg = arg + ",stgidx=" + stgidx;
		arg = arg + ",stghostid=" + stghostid;
		return base.ToString() + arg;
	}
}
