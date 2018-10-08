public class Coop_Model_RoomLeaved : Coop_Model_Base
{
	public int cid;

	public string token = string.Empty;

	public int stgid;

	public int stghostid;

	public Coop_Model_RoomLeaved()
	{
		base.packetType = PACKET_TYPE.ROOM_LEAVED;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",token=" + token;
		empty = empty + ",stgid=" + stgid;
		empty = empty + ",stghostid=" + stghostid;
		return base.ToString() + empty;
	}
}
