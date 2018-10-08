public class Coop_Model_PlayerDeadStandup : Coop_Model_ObjectBase
{
	public int standupHp;

	public Player.eContinueType cType;

	public Coop_Model_PlayerDeadStandup()
	{
		base.packetType = PACKET_TYPE.PLAYER_DEAD_STANDUP;
	}
}
