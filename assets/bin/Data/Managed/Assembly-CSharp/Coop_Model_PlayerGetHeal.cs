public class Coop_Model_PlayerGetHeal : Coop_Model_ObjectBase
{
	public int heal_hp;

	public int heal_type;

	public bool receive;

	public int effect_type;

	public bool isApplyAbility;

	public Coop_Model_PlayerGetHeal()
	{
		base.packetType = PACKET_TYPE.PLAYER_GET_HEAL;
	}
}
