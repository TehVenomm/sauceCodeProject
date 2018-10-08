public class Coop_Model_RoomTimeUpdate : Coop_Model_Base
{
	public int elapsedSec;

	public Coop_Model_RoomTimeUpdate()
	{
		base.packetType = PACKET_TYPE.ROOM_TIME_UPDATE;
	}
}
