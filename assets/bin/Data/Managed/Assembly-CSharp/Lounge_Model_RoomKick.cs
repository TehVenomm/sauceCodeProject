public class Lounge_Model_RoomKick : Coop_Model_Base
{
	public int cid;

	public string token = string.Empty;

	public Lounge_Model_RoomKick()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_KICK;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",token=" + token;
		return base.ToString() + empty;
	}
}
