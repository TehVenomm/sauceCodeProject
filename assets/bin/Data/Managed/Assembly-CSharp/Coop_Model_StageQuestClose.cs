public class Coop_Model_StageQuestClose : Coop_Model_Base
{
	public bool is_succeed;

	public Coop_Model_StageQuestClose()
	{
		base.packetType = PACKET_TYPE.STAGE_QUEST_CLOSE;
	}
}
