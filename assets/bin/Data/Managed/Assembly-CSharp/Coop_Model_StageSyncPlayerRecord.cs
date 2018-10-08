public class Coop_Model_StageSyncPlayerRecord : Coop_Model_Base
{
	public InGameRecorder.PlayerRecordSyncHost rec;

	public Coop_Model_StageSyncPlayerRecord()
	{
		base.packetType = PACKET_TYPE.STAGE_SYNC_PLAYER_RECORD;
	}
}
