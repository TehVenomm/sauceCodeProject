public class Coop_Model_PlayerPrayerBoost : Coop_Model_ObjectBase
{
	public int sid;

	public bool isBoost;

	public Coop_Model_PlayerPrayerBoost()
	{
		base.packetType = PACKET_TYPE.PLAYER_PRAYER_BOOST;
	}
}
