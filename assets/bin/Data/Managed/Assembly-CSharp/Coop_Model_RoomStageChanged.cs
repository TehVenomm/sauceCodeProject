public class Coop_Model_RoomStageChanged : Coop_Model_Base
{
	public int cid;

	public int sid;

	public int pstgid;

	public int pstghostid;

	public int stgid;

	public int stgidx;

	public int stghostid;

	public Coop_Model_RoomStageChanged()
	{
		base.packetType = PACKET_TYPE.ROOM_STAGE_CHANGED;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",sid=" + sid;
		empty = empty + ",pstgid=" + pstgid;
		empty = empty + ",pstghostid=" + pstghostid;
		empty = empty + ",stgid=" + stgid;
		empty = empty + ",stgidx=" + stgidx;
		empty = empty + ",stghostid=" + stghostid;
		return base.ToString() + empty;
	}
}
