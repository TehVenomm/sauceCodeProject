public class Coop_Model_PlayerSacrificedHp : Coop_Model_ObjectBase
{
	public int sacrificedHp;

	public Coop_Model_PlayerSacrificedHp()
	{
		base.packetType = PACKET_TYPE.PLAYER_SACRIFICED_HP;
	}
}
