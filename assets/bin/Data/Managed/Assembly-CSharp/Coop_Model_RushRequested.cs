public class Coop_Model_RushRequested : Coop_Model_Base
{
	public int currentWaveIndex;

	public InGameManager.RushWaveSyncData syncData;

	public Coop_Model_RushRequested()
	{
		base.packetType = PACKET_TYPE.ROOM_RUSH_REQUESTED;
	}
}
