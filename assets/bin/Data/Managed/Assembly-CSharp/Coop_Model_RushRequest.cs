public class Coop_Model_RushRequest : Coop_Model_Base
{
	public int requestRushIndex;

	public Coop_Model_RushRequest()
	{
		base.packetType = PACKET_TYPE.ROOM_RUSH_REQUEST;
	}
}
