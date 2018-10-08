public class Party_Model_Register : Coop_Model_Base
{
	public string roomId;

	public int owner;

	public string ownerToken;

	public int uid;

	public string signature;

	public int pid;

	public int qid;

	public Party_Model_Register()
	{
		base.packetType = PACKET_TYPE.PARTY_REGISTER;
	}

	public override string ToString()
	{
		return base.ToString() + ",roomId=" + roomId + ",owner=" + owner + ",ownerToken=" + ownerToken + ",uid=" + uid + ",signature=" + signature + ",pid=" + pid + ",qid=" + qid;
	}
}
