public class Lounge_Model_MemberArena : Coop_Model_Base
{
	public int cid;

	public int pid;

	public int aid;

	public Lounge_Model_MemberArena()
	{
		base.packetType = PACKET_TYPE.LOUNGE_MEMBER_ARENA;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",pid=" + pid;
		empty = empty + ",aid=" + aid;
		return base.ToString() + empty;
	}
}
