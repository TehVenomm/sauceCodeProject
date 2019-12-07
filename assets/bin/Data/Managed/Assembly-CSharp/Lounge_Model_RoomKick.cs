public class Lounge_Model_RoomKick : Coop_Model_Base
{
	public int cid;

	public string token = "";

	public Lounge_Model_RoomKick()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_KICK;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",token=" + token;
		return base.ToString() + arg;
	}
}
