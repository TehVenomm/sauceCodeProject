public class Lounge_Model_RoomHostChanged : Coop_Model_Base
{
	public int hostid;

	public Lounge_Model_RoomHostChanged()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_HOST_CHANGED;
	}

	public override string ToString()
	{
		return base.ToString() + " ,hostid=" + hostid;
	}
}
