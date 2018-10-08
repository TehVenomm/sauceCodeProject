public class Coop_Model_PlayerSpecialActionGaugeSync : Coop_Model_ObjectBase
{
	public int weaponIndex;

	public float currentSpActionGauge;

	public int comboLv;

	public Coop_Model_PlayerSpecialActionGaugeSync()
	{
		base.packetType = PACKET_TYPE.PLAYER_SPECIAL_ACTION_GAUGE_SYNC;
	}
}
