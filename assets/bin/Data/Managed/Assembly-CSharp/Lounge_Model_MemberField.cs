public class Lounge_Model_MemberField : Coop_Model_Base
{
	public int cid;

	public int fid;

	public int fmid;

	public int pid;

	public int qid;

	public bool h;

	public Lounge_Model_MemberField()
	{
		base.packetType = PACKET_TYPE.LOUNGE_MEMBER_FIELD;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",fid=" + fid;
		arg = arg + ",fmid=" + fmid;
		arg = arg + ",pid=" + pid;
		arg = arg + ",qid=" + qid;
		arg = arg + ",host=" + h.ToString();
		return base.ToString() + arg;
	}
}
