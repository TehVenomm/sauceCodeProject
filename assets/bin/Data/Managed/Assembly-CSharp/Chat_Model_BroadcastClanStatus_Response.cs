public class Chat_Model_BroadcastClanStatus_Response : Chat_Model_Base
{
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

	public string Result
	{
		get;
		protected set;
	}

	public string Type
	{
		get;
		protected set;
	}

	public string Status
	{
		get;
		protected set;
	}

	public Chat_Model_BroadcastClanStatus_Response()
	{
		m_packetType = CHAT_PACKET_TYPE.CLAN_BROADCAST_STATUS;
	}

	public static Chat_Model_Base Parse(string str)
	{
		string roomId = str.Substring(40, 32);
		string result = str.Substring(72, 8);
		string type = str.Substring(80, 8);
		string status = str.Substring(88, 8);
		string id = str.Substring(96, 32);
		Chat_Model_BroadcastClanStatus_Response chat_Model_BroadcastClanStatus_Response = new Chat_Model_BroadcastClanStatus_Response();
		chat_Model_BroadcastClanStatus_Response.Id = id;
		chat_Model_BroadcastClanStatus_Response.RoomId = roomId;
		chat_Model_BroadcastClanStatus_Response.Result = result;
		chat_Model_BroadcastClanStatus_Response.Type = type;
		chat_Model_BroadcastClanStatus_Response.Status = status;
		Chat_Model_BroadcastClanStatus_Response chat_Model_BroadcastClanStatus_Response2 = chat_Model_BroadcastClanStatus_Response;
		chat_Model_BroadcastClanStatus_Response2.SetErrorType("0");
		return chat_Model_BroadcastClanStatus_Response2;
	}
}
