public class Lounge_Model_AFK_Kick : Lounge_Model_RoomKick
{
	public Lounge_Model_AFK_Kick()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_AFK_KICK;
	}
}
