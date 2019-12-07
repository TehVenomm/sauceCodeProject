public class Party_Model_RoomLeaved : Coop_Model_Base
{
	public int cid;

	public string token = "";

	public Party_Model_RoomLeaved()
	{
		base.packetType = PACKET_TYPE.PARTY_ROOM_LEAVED;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",token=" + token;
		return base.ToString() + arg;
	}
}
