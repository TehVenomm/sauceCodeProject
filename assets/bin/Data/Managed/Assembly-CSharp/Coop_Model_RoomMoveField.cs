public class Coop_Model_RoomMoveField : Coop_Model_Base
{
	public int pid;

	public Coop_Model_RoomMoveField()
	{
		base.packetType = PACKET_TYPE.ROOM_MOVE_FIELD;
	}
}
