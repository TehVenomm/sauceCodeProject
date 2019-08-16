public class Coop_Model_PlayerCarry : Coop_Model_ObjectSyncPositionBase
{
	public InGameProgress.eFieldGimmick type;

	public int pointId;

	public Coop_Model_PlayerCarry()
	{
		base.packetType = PACKET_TYPE.PLAYER_CARRY;
	}
}
