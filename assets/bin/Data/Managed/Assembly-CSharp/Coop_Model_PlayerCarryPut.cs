public class Coop_Model_PlayerCarryPut : Coop_Model_ObjectSyncPositionBase
{
	public int pointId;

	public Coop_Model_PlayerCarryPut()
	{
		base.packetType = PACKET_TYPE.PLAYER_CARRY_PUT;
	}
}
