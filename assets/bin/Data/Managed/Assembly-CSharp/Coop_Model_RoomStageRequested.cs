public class Coop_Model_RoomStageRequested : Coop_Model_Base
{
	public int cid;

	public Coop_Model_RoomStageRequested()
	{
		base.packetType = PACKET_TYPE.ROOM_STAGE_REQUESTED;
	}

	public override string ToString()
	{
		return base.ToString() + " ,cid=" + cid;
	}
}
