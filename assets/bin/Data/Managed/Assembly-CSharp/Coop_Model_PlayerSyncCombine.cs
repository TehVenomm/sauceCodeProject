public class Coop_Model_PlayerSyncCombine : Coop_Model_ObjectBase
{
	public bool isCombine;

	public Coop_Model_PlayerSyncCombine()
	{
		base.packetType = PACKET_TYPE.PLAYER_SYNC_COMBINE;
	}
}
