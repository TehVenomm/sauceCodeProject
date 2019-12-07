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
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",pid=" + pid;
		arg = arg + ",aid=" + aid;
		return base.ToString() + arg;
	}
}
