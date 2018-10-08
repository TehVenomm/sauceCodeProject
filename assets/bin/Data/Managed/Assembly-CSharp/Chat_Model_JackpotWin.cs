public class Chat_Model_JackpotWin : Chat_Model_Base
{
	public string jacpotData
	{
		get;
		protected set;
	}

	public Chat_Model_JackpotWin()
	{
		m_packetType = CHAT_PACKET_TYPE.JACKPOT_WIN_UPDATE;
	}

	public override string Serialize()
	{
		return $"{jacpotData}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_Base Parse(string str)
	{
		Chat_Model_JackpotWin chat_Model_JackpotWin = new Chat_Model_JackpotWin();
		chat_Model_JackpotWin.m_packetType = CHAT_PACKET_TYPE.JACKPOT_WIN_UPDATE;
		chat_Model_JackpotWin.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
		chat_Model_JackpotWin.jacpotData = str.Substring(40);
		Chat_Model_JackpotWin chat_Model_JackpotWin2 = chat_Model_JackpotWin;
		chat_Model_JackpotWin2.SetErrorType("0");
		return chat_Model_JackpotWin2;
	}

	public static Chat_Model_JackpotWin Create(string flag)
	{
		Chat_Model_JackpotWin chat_Model_JackpotWin = new Chat_Model_JackpotWin();
		chat_Model_JackpotWin.jacpotData = flag;
		Chat_Model_JackpotWin chat_Model_JackpotWin2 = chat_Model_JackpotWin;
		chat_Model_JackpotWin2.payload = chat_Model_JackpotWin2.Serialize();
		return chat_Model_JackpotWin2;
	}
}
