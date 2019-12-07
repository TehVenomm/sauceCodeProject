public class Chat_Model_SendToClanMessage_Response : Chat_Model_Base
{
	public string Uuid
	{
		get;
		protected set;
	}

	public string Id
	{
		get;
		protected set;
	}

	public string RoomId
	{
		get;
		protected set;
	}

	public string TimeStampClient
	{
		get;
		protected set;
	}

	public string TimeStampServer
	{
		get;
		protected set;
	}

	public string ReceiveId
	{
		get;
		protected set;
	}

	public string SenderId
	{
		get;
		protected set;
	}

	public string SenderName
	{
		get;
		protected set;
	}

	public string Message
	{
		get;
		protected set;
	}

	public Chat_Model_SendToClanMessage_Response()
	{
		m_packetType = CHAT_PACKET_TYPE.CLAN_SENDTO;
	}

	public static Chat_Model_Base Parse(string str)
	{
		string uuid = str.Substring(40, 36);
		string id = str.Substring(76, 32);
		string text = str.Substring(108, 8);
		string roomId = str.Substring(116, 32);
		string timeStampClient = str.Substring(148, 16);
		string timeStampServer = str.Substring(164, 16);
		string senderId = str.Substring(180, 32);
		string receiveId = str.Substring(212, 32);
		string[] array = str.Substring(244).Split(':');
		string senderName = array[0];
		string message = array[1];
		if (int.Parse(text) == 0)
		{
			Chat_Model_SendToClanMessage_Response chat_Model_SendToClanMessage_Response = new Chat_Model_SendToClanMessage_Response();
			chat_Model_SendToClanMessage_Response.Uuid = uuid;
			chat_Model_SendToClanMessage_Response.Id = id;
			chat_Model_SendToClanMessage_Response.RoomId = roomId;
			chat_Model_SendToClanMessage_Response.TimeStampClient = timeStampClient;
			chat_Model_SendToClanMessage_Response.TimeStampServer = timeStampServer;
			chat_Model_SendToClanMessage_Response.ReceiveId = receiveId;
			chat_Model_SendToClanMessage_Response.SenderId = senderId;
			chat_Model_SendToClanMessage_Response.Message = message;
			chat_Model_SendToClanMessage_Response.SenderName = senderName;
			chat_Model_SendToClanMessage_Response.SetErrorType(text);
			return chat_Model_SendToClanMessage_Response;
		}
		return null;
	}
}
