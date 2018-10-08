public class Coop_Model_RoomStageHostChanged : Coop_Model_Base
{
	public int stgid;

	public int stghostid;

	public Coop_Model_RoomStageHostChanged()
	{
		base.packetType = PACKET_TYPE.ROOM_STAGE_HOST_CHANGED;
	}

	public override string ToString()
	{
		return base.ToString() + " ,stgid=" + stgid + ",stghostid=" + stghostid;
	}
}
