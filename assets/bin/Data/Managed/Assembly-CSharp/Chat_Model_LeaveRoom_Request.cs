public class Chat_Model_LeaveRoom_Request : Chat_Model_Base
{
	public string RoomId
	{
		get;
		protected set;
	}

	public Chat_Model_LeaveRoom_Request()
	{
		m_packetType = CHAT_PACKET_TYPE.LEAVE_ROOM;
	}

	public override string Serialize()
	{
		string text = $"{0:D32}";
		string arg = text.Substring(RoomId.Length) + RoomId;
		return $"{arg}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_LeaveRoom_Request Create(string roomId)
	{
		Chat_Model_LeaveRoom_Request chat_Model_LeaveRoom_Request = new Chat_Model_LeaveRoom_Request();
		chat_Model_LeaveRoom_Request.RoomId = roomId;
		Chat_Model_LeaveRoom_Request chat_Model_LeaveRoom_Request2 = chat_Model_LeaveRoom_Request;
		chat_Model_LeaveRoom_Request2.payload = chat_Model_LeaveRoom_Request2.Serialize();
		return chat_Model_LeaveRoom_Request2;
	}
}
