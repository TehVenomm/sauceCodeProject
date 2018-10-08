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
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",fid=" + fid;
		empty = empty + ",fmid=" + fmid;
		empty = empty + ",pid=" + pid;
		empty = empty + ",qid=" + qid;
		empty = empty + ",host=" + h;
		return base.ToString() + empty;
	}
}
