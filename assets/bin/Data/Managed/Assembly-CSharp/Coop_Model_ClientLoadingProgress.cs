public class Coop_Model_ClientLoadingProgress : Coop_Model_ObjectBase
{
	public int per;

	public Coop_Model_ClientLoadingProgress()
	{
		base.packetType = PACKET_TYPE.CLIENT_LOADING_PROGRESS;
	}
}
