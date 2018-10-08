public class Chat_Model_UpdateDarkMarket : Chat_Model_Base
{
	public string itemMarketId
	{
		get;
		protected set;
	}

	public string soldNum
	{
		get;
		protected set;
	}

	public Chat_Model_UpdateDarkMarket()
	{
		m_packetType = CHAT_PACKET_TYPE.DARK_MARKET_UPDATE;
	}

	public override string Serialize()
	{
		return $"{itemMarketId}--{soldNum}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_Base Parse(string str)
	{
		Chat_Model_UpdateDarkMarket chat_Model_UpdateDarkMarket = new Chat_Model_UpdateDarkMarket();
		chat_Model_UpdateDarkMarket.m_packetType = CHAT_PACKET_TYPE.DARK_MARKET_UPDATE;
		chat_Model_UpdateDarkMarket.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
		chat_Model_UpdateDarkMarket.itemMarketId = str.Substring(40, 10);
		chat_Model_UpdateDarkMarket.soldNum = str.Substring(50, 10);
		Chat_Model_UpdateDarkMarket chat_Model_UpdateDarkMarket2 = chat_Model_UpdateDarkMarket;
		chat_Model_UpdateDarkMarket2.SetErrorType("0");
		return chat_Model_UpdateDarkMarket2;
	}
}
