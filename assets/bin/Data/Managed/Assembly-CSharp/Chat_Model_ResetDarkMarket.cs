public class Chat_Model_ResetDarkMarket : Chat_Model_Base
{
	public string endDate
	{
		get;
		protected set;
	}

	public Chat_Model_ResetDarkMarket()
	{
		m_packetType = CHAT_PACKET_TYPE.DARK_MARKET_RESET;
	}

	public override string Serialize()
	{
		return $"{endDate}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_Base Parse(string str)
	{
		Chat_Model_ResetDarkMarket chat_Model_ResetDarkMarket = new Chat_Model_ResetDarkMarket();
		chat_Model_ResetDarkMarket.m_packetType = CHAT_PACKET_TYPE.DARK_MARKET_RESET;
		chat_Model_ResetDarkMarket.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
		chat_Model_ResetDarkMarket.endDate = str.Substring(40, 14);
		chat_Model_ResetDarkMarket.SetErrorType("0");
		return chat_Model_ResetDarkMarket;
	}
}
