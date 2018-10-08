public class Coop_Model_PlayerPairSwordsLaserEnd : Coop_Model_ObjectBase
{
	public int weaponIndex;

	public float currentSpActionGauge;

	public Coop_Model_PlayerPairSwordsLaserEnd()
	{
		base.packetType = PACKET_TYPE.PLAYER_PAIR_SWORDS_LASER_END;
	}
}
