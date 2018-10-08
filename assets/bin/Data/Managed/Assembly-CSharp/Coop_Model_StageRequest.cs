public class Coop_Model_StageRequest : Coop_Model_Base
{
	public int series_index;

	public Coop_Model_StageRequest()
	{
		base.packetType = PACKET_TYPE.STAGE_REQUEST;
	}
}
