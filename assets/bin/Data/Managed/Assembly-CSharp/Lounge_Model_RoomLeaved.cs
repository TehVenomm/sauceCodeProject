public class Lounge_Model_RoomLeaved : Coop_Model_Base
{
	public int cid;

	public string token = string.Empty;

	public Lounge_Model_RoomLeaved()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_LEAVED;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",token=" + token;
		return base.ToString() + empty;
	}
}
