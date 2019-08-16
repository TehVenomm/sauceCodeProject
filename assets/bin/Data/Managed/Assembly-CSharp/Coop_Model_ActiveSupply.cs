public class Coop_Model_ActiveSupply : Coop_Model_Base
{
	public int pointId;

	public Coop_Model_ActiveSupply()
	{
		base.packetType = PACKET_TYPE.ACTIVE_SUPPLY;
	}
}
