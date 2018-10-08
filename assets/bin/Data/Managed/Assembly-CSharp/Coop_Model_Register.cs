public class Coop_Model_Register : Coop_Model_Base
{
	public string roomId;

	public string token;

	public Coop_Model_Register()
	{
		base.packetType = PACKET_TYPE.REGISTER;
	}
}
