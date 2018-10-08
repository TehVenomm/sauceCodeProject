public class Coop_Model_StageChat : Coop_Model_Base
{
	public int chara_id;

	public int chat_id;

	public Coop_Model_StageChat()
	{
		base.packetType = PACKET_TYPE.STAGE_CHAT;
	}
}
