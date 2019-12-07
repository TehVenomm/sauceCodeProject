public class Coop_Model_UpdateBoostComplete : Coop_Model_Base
{
	public bool success;

	public Coop_Model_UpdateBoostComplete()
	{
		base.packetType = PACKET_TYPE.UPDATE_BOOST_COMPLETE;
	}

	public override string ToString()
	{
		return base.ToString() + ",success=" + success.ToString();
	}
}
