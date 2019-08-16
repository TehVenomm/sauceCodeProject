public class Coop_Model_PlayerStoneEnd : Coop_Model_ObjectBase
{
	public float countTime;

	public Coop_Model_PlayerStoneEnd()
	{
		base.packetType = PACKET_TYPE.PLAYER_STONE_END;
	}
}
