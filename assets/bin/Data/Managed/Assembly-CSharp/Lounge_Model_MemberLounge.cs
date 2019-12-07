public class Lounge_Model_MemberLounge : Coop_Model_Base
{
	public int cid;

	public Lounge_Model_MemberLounge()
	{
		base.packetType = PACKET_TYPE.LOUNGE_MEMBER_LOUNGE;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",cid=" + cid;
		return base.ToString() + arg;
	}
}
