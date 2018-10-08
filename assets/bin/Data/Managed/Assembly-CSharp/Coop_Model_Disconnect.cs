public class Coop_Model_Disconnect : Coop_Model_Base
{
	public int code;

	public Coop_Model_Disconnect()
	{
		base.packetType = PACKET_TYPE.DISCONNECT;
	}
}
