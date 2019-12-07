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
		string arg = "";
		arg = arg + ",cid=" + cid;
		arg = arg + ",qid=" + qid;
		arg = arg + ",pid=" + pid;
		arg = arg + ",host=" + h.ToString();
		return base.ToString() + arg;
	}
}
