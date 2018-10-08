public class Coop_Model_RoomTimeCheck : Coop_Model_Base
{
	public int elapsedSec;

	public Coop_Model_RoomTimeCheck()
	{
		base.packetType = PACKET_TYPE.ROOM_TIME_CHECK;
	}
}
