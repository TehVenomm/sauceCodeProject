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
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",sid=" + sid;
		arg = arg + ",pstgid=" + pstgid;
		arg = arg + ",pstghostid=" + pstghostid;
		arg = arg + ",stgid=" + stgid;
		arg = arg + ",stgidx=" + stgidx;
		arg = arg + ",stghostid=" + stghostid;
		return base.ToString() + arg;
	}
}
