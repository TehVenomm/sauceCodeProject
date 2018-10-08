public class Coop_Model_RoomStageChange : Coop_Model_Base
{
	public int qId;

	public int idx;

	public Coop_Model_RoomStageChange()
	{
		base.packetType = PACKET_TYPE.ROOM_STAGE_CHANGE;
	}

	public override string ToString()
	{
		return base.ToString() + ",qId=" + qId + ",idx=" + idx;
	}
}
