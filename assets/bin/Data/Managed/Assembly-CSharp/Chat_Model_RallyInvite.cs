public class Chat_Model_RallyInvite : Chat_Model_Base
{
	public string flag
	{
		get;
		protected set;
	}

	public Chat_Model_RallyInvite()
	{
		m_packetType = CHAT_PACKET_TYPE.RALLY_INVITE;
	}

	public override string Serialize()
	{
		return $"{flag}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_Base Parse(string str)
	{
		Chat_Model_RallyInvite chat_Model_RallyInvite = new Chat_Model_RallyInvite();
		chat_Model_RallyInvite.m_packetType = CHAT_PACKET_TYPE.RALLY_INVITE;
		chat_Model_RallyInvite.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
		chat_Model_RallyInvite.flag = str.Substring(40, 1);
		chat_Model_RallyInvite.SetErrorType("0");
		return chat_Model_RallyInvite;
	}

	public static Chat_Model_RallyInvite Create(string flag)
	{
		Chat_Model_RallyInvite obj = new Chat_Model_RallyInvite
		{
			flag = flag
		};
		obj.payload = obj.Serialize();
		return obj;
	}
}
