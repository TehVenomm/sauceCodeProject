public class Coop_Model_RoomNotifyTraceBoss : Coop_Model_Base
{
	public int mid;

	public int lc;

	public Coop_Model_RoomNotifyTraceBoss()
	{
		base.packetType = PACKET_TYPE.ROOM_NOTIFY_TRACE_BOSS;
	}
}
