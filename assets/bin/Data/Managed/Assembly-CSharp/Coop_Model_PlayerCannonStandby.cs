public class Coop_Model_PlayerCannonStandby : Coop_Model_ObjectSyncPositionBase
{
	public int cannonId;

	public Coop_Model_PlayerCannonStandby()
	{
		base.packetType = PACKET_TYPE.PLAYER_CANNON_STANDBY;
	}
}
