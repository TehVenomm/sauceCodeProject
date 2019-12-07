public class Chat_Model_PartyInvite : Chat_Model_Base
{
	public string flag
	{
		get;
		protected set;
	}

	public Chat_Model_PartyInvite()
	{
		m_packetType = CHAT_PACKET_TYPE.PARTY_INVITE;
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
		Chat_Model_PartyInvite chat_Model_PartyInvite = new Chat_Model_PartyInvite();
		chat_Model_PartyInvite.m_packetType = CHAT_PACKET_TYPE.PARTY_INVITE;
		chat_Model_PartyInvite.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
		chat_Model_PartyInvite.flag = str.Substring(40, 1);
		chat_Model_PartyInvite.SetErrorType("0");
		return chat_Model_PartyInvite;
	}

	public static Chat_Model_PartyInvite Create(string flag)
	{
		Chat_Model_PartyInvite obj = new Chat_Model_PartyInvite
		{
			flag = flag
		};
		obj.payload = obj.Serialize();
		return obj;
	}
}
