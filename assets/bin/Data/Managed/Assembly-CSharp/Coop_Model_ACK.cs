public class Coop_Model_ACK : Coop_Model_Base
{
	public int ack;

	public bool positive;

	public Coop_Model_ACK()
	{
		base.packetType = PACKET_TYPE.ACK;
	}
}
