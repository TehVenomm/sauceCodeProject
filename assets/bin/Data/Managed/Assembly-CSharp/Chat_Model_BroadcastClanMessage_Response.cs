public class Chat_Model_BroadcastClanMessage_Response : Chat_Model_Base
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

	public string NickName
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

	public Chat_Model_BroadcastClanMessage_Response()
	{
		m_packetType = CHAT_PACKET_TYPE.CLAN_BROADCAST_ROOM;
	}

	public static Chat_Model_Base Parse(string str)
	{
		string uuid = str.Substring(40, 36);
		string id = str.Substring(76, 32);
		string roomId = str.Substring(108, 32);
		string text = str.Substring(140, 8);
		string timeStampClient = str.Substring(148, 16);
		string timeStampServer = str.Substring(164, 16);
		string senderId = str.Substring(180, 32);
		string[] array = str.Substring(212).Split(':');
		string text2 = (array.Length == 2) ? array[0] : string.Empty;
		string message = (array.Length == 2) ? array[1] : string.Empty;
		if (int.Parse(text) == 0)
		{
			Chat_Model_BroadcastClanMessage_Response chat_Model_BroadcastClanMessage_Response = new Chat_Model_BroadcastClanMessage_Response();
			chat_Model_BroadcastClanMessage_Response.Uuid = uuid;
			chat_Model_BroadcastClanMessage_Response.Id = id;
			chat_Model_BroadcastClanMessage_Response.NickName = text2;
			chat_Model_BroadcastClanMessage_Response.RoomId = roomId;
			chat_Model_BroadcastClanMessage_Response.TimeStampClient = timeStampClient;
			chat_Model_BroadcastClanMessage_Response.TimeStampServer = timeStampServer;
			chat_Model_BroadcastClanMessage_Response.SenderId = senderId;
			chat_Model_BroadcastClanMessage_Response.SenderName = text2;
			chat_Model_BroadcastClanMessage_Response.Message = message;
			chat_Model_BroadcastClanMessage_Response.SetErrorType(text);
			return chat_Model_BroadcastClanMessage_Response;
		}
		return null;
	}
}
