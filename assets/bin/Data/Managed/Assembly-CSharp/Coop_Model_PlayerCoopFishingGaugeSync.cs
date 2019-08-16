public class Coop_Model_PlayerCoopFishingGaugeSync : Coop_Model_Base
{
	public int ownerUserId;

	public float gaugeValue;

	public Coop_Model_PlayerCoopFishingGaugeSync()
	{
		base.packetType = PACKET_TYPE.PLAYER_COOP_FISHING_GAUGE_SYNC;
	}
}
