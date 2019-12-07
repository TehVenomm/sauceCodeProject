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
		string arg = $"{0:D32}".Substring(RoomId.Length) + RoomId;
		return $"{arg}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_LeaveRoom_Request Create(string roomId)
	{
		Chat_Model_LeaveRoom_Request obj = new Chat_Model_LeaveRoom_Request
		{
			RoomId = roomId
		};
		obj.payload = obj.Serialize();
		return obj;
	}
}
