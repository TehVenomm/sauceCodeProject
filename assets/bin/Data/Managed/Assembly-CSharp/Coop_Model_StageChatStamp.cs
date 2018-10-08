public class Coop_Model_StageChatStamp : Coop_Model_Base
{
	public int user_id;

	public int chara_id;

	public int stamp_id;

	public Coop_Model_StageChatStamp()
	{
		base.packetType = PACKET_TYPE.STAGE_CHAT_STAMP;
	}
}
