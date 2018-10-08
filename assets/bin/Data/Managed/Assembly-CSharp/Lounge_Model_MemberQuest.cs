public class Lounge_Model_MemberQuest : Coop_Model_Base
{
	public int cid;

	public int qid;

	public int pid;

	public bool h;

	public Lounge_Model_MemberQuest()
	{
		base.packetType = PACKET_TYPE.LOUNGE_MEMBER_QUEST;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",cid=" + cid;
		empty = empty + ",qid=" + qid;
		empty = empty + ",pid=" + pid;
		empty = empty + ",host=" + h;
		return base.ToString() + empty;
	}
}
