public class Coop_Model_PlayerGatherGimmickState : Coop_Model_ObjectBase
{
	public int state;

	public Coop_Model_PlayerGatherGimmickState()
	{
		base.packetType = PACKET_TYPE.PLAYER_GATHER_GIMMICK_STATE;
	}
}
