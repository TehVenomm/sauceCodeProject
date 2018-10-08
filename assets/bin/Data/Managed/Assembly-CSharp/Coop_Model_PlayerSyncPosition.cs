public class Coop_Model_PlayerSyncPosition : Coop_Model_ObjectSyncPositionBase
{
	public Coop_Model_PlayerSyncPosition()
	{
		base.packetType = PACKET_TYPE.PLAYER_SYNC_POSITION;
	}
}
