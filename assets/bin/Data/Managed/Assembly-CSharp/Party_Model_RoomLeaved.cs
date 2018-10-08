public class Party_Model_RoomLeaved : Coop_Model_Base
{
	public int cid;

	public string token = string.Empty;

	public Party_Model_RoomLeaved()
	{
		base.packetType = PACKET_TYPE.PARTY_ROOM_LEAVED;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",token=" + token;
		return base.ToString() + empty;
	}
}
