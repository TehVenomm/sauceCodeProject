public class Lounge_Model_Register : Coop_Model_Base
{
	public string roomId;

	public int owner;

	public string ownerToken;

	public int uid;

	public string signature;

	public Lounge_Model_Register()
	{
		base.packetType = PACKET_TYPE.LOUNGE_REGISTER;
	}

	public override string ToString()
	{
		return base.ToString() + ",roomId=" + roomId + ",owner=" + owner + ",ownerToken=" + ownerToken + ",uid=" + uid + ",signature=" + signature;
	}
}
