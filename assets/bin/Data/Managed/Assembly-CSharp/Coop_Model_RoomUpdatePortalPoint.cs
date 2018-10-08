public class Coop_Model_RoomUpdatePortalPoint : Coop_Model_Base
{
	public int pid;

	public int pt;

	public int x;

	public int z;

	public Coop_Model_RoomUpdatePortalPoint()
	{
		base.packetType = PACKET_TYPE.ROOM_UPDATE_PORTAL_POINT;
	}
}
