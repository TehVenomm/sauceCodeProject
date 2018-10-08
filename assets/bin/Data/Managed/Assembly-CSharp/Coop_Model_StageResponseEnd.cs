public class Coop_Model_StageResponseEnd : Coop_Model_Base
{
	public int error_id;

	public Coop_Model_StageResponseEnd()
	{
		base.packetType = PACKET_TYPE.STAGE_RESPONSE_END;
	}
}
