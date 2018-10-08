public class Coop_Model_PlayerSetStatus : Coop_Model_ObjectBase
{
	public int level;

	public int atk;

	public int def;

	public int hp;

	public Coop_Model_PlayerSetStatus()
	{
		base.packetType = PACKET_TYPE.PLAYER_SETSTATUS;
	}
}
