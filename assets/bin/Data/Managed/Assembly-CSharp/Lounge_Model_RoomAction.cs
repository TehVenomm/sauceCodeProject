public class Lounge_Model_RoomAction : Coop_Model_Base
{
	public int cid;

	public int aid;

	public Lounge_Model_RoomAction()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_ACTION;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",aid=" + aid;
		return base.ToString() + arg;
	}
}
