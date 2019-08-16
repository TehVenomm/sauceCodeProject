public class Coop_Model_PlayerSyncSubstitute : Coop_Model_ObjectBase
{
	public int num;

	public Coop_Model_PlayerSyncSubstitute()
	{
		base.packetType = PACKET_TYPE.PLAYER_SYNC_SUBSTITUTE;
	}
}
