public class Coop_Model_StageChatMessage : Coop_Model_Base
{
	public int user_id;

	public int chara_id;

	public string text;

	public Coop_Model_StageChatMessage()
	{
		base.packetType = PACKET_TYPE.CHAT_MESSAGE;
	}
}
