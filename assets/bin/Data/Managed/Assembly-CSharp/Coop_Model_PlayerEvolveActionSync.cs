public class Coop_Model_PlayerEvolveActionSync : Coop_Model_ObjectBase
{
	public bool isAction;

	public Coop_Model_PlayerEvolveActionSync()
	{
		base.packetType = PACKET_TYPE.PLAYER_EVOLVE_ACTION_SYNC;
	}
}
