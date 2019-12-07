public class Coop_Model_RoomLeaved : Coop_Model_Base
{
	public int cid;

	public string token = "";

	public int stgid;

	public int stghostid;

	public Coop_Model_RoomLeaved()
	{
		base.packetType = PACKET_TYPE.ROOM_LEAVED;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",token=" + token;
		arg = arg + ",stgid=" + stgid;
		arg = arg + ",stghostid=" + stghostid;
		return base.ToString() + arg;
	}
}
