public class Chat_Model_BroadcastMessage_Response : Chat_Model_Base
{
	public string NickName
	{
		get;
		protected set;
	}

	public int RoomNumber
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

	public Chat_Model_BroadcastMessage_Response()
	{
		m_packetType = CHAT_PACKET_TYPE.BROADCAST_ROOM;
	}

	public static Chat_Model_Base Parse(string str)
	{
		string roomId = str.Substring(40, 32);
		string text = str.Substring(72, 8);
		string timeStampClient = str.Substring(80, 16);
		string timeStampServer = str.Substring(96, 16);
		string senderId = str.Substring(112, 32);
		string[] array = str.Substring(144).Split(':');
		string text2 = (array.Length != 2) ? string.Empty : array[0];
		string message = (array.Length != 2) ? string.Empty : array[1];
		if (int.Parse(text) == 0)
		{
			Chat_Model_BroadcastMessage_Response chat_Model_BroadcastMessage_Response = new Chat_Model_BroadcastMessage_Response();
			chat_Model_BroadcastMessage_Response.NickName = text2;
			chat_Model_BroadcastMessage_Response.RoomId = roomId;
			chat_Model_BroadcastMessage_Response.TimeStampClient = timeStampClient;
			chat_Model_BroadcastMessage_Response.TimeStampServer = timeStampServer;
			chat_Model_BroadcastMessage_Response.SenderId = senderId;
			chat_Model_BroadcastMessage_Response.SenderName = text2;
			chat_Model_BroadcastMessage_Response.Message = message;
			Chat_Model_BroadcastMessage_Response chat_Model_BroadcastMessage_Response2 = chat_Model_BroadcastMessage_Response;
			chat_Model_BroadcastMessage_Response2.SetErrorType(text);
			return chat_Model_BroadcastMessage_Response2;
		}
		return null;
	}
}
