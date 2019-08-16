public class Coop_Model_PlayerShotResurrectionHoming : Coop_Model_PlayerShotHealingHoming
{
	public Coop_Model_PlayerShotResurrectionHoming()
	{
		base.packetType = PACKET_TYPE.PLAYER_SHOT_RESURRECTION_HOMING;
	}
}
