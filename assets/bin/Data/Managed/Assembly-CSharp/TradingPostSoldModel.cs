public class TradingPostSoldModel : Chat_Model_Base
{
	public string soldNum
	{
		get;
		protected set;
	}

	public TradingPostSoldModel()
	{
		m_packetType = CHAT_PACKET_TYPE.TRADING_POST_SOLD;
	}

	public override string Serialize()
	{
		return $"{soldNum}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_Base Parse(string str)
	{
		TradingPostSoldModel tradingPostSoldModel = new TradingPostSoldModel();
		tradingPostSoldModel.m_packetType = CHAT_PACKET_TYPE.TRADING_POST_SOLD;
		tradingPostSoldModel.soldNum = str.Substring(41);
		tradingPostSoldModel.SetErrorType("0");
		return tradingPostSoldModel;
	}
}
