public class Coop_Model_PlayerSoulBoost : Coop_Model_ObjectBase
{
	public bool isBoost;

	public Coop_Model_PlayerSoulBoost()
	{
		base.packetType = PACKET_TYPE.PLAYER_SOUL_BOOST;
	}
}
