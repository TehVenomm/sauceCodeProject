public class Coop_Model_StageRequestPop : Coop_Model_Base
{
	public bool isPlayer;

	public bool isSelf;

	public Coop_Model_StageRequestPop()
	{
		base.packetType = PACKET_TYPE.STAGE_REQUEST_POP;
	}
}
