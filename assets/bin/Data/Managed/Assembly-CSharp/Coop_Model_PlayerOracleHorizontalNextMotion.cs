public class Coop_Model_PlayerOracleHorizontalNextMotion : Coop_Model_ObjectSyncPositionBase
{
	public bool isFinish;

	public Coop_Model_PlayerOracleHorizontalNextMotion()
	{
		base.packetType = PACKET_TYPE.PLAYER_ORACLE_HORIZONTAL_NEXT_MOTION;
	}
}
