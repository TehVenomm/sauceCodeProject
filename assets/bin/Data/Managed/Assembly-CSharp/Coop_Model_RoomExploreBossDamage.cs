public class Coop_Model_RoomExploreBossDamage : Coop_Model_Base
{
	public int dmg;

	public Coop_Model_RoomExploreBossDamage()
	{
		base.packetType = PACKET_TYPE.ROOM_EXPLORE_BOSS_DAMAGE;
	}
}
