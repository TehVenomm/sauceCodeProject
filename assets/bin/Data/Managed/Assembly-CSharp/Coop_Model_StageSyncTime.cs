public class Coop_Model_StageSyncTime : Coop_Model_Base
{
	public float elapsedTime;

	public Coop_Model_StageSyncTime()
	{
		base.packetType = PACKET_TYPE.STAGE_SYNC_TIME;
	}
}
