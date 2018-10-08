public class Coop_Model_ClientStatus : Coop_Model_ObjectBase
{
	public int status;

	public int joinType;

	public Coop_Model_ClientStatus()
	{
		base.packetType = PACKET_TYPE.CLIENT_STATUS;
	}

	public override string ToString()
	{
		return base.ToString() + ",status=" + status;
	}
}
