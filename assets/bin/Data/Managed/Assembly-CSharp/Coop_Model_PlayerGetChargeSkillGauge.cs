public class Coop_Model_PlayerGetChargeSkillGauge : Coop_Model_ObjectBase
{
	public int buffType;

	public int buffValue;

	public int useSkillIndex;

	public bool receive;

	public Coop_Model_PlayerGetChargeSkillGauge()
	{
		base.packetType = PACKET_TYPE.PLAYER_GET_CHARGE_SKILLGAUGE;
	}
}
