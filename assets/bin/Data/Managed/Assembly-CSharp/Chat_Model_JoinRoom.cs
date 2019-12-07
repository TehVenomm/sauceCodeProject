public class Chat_Model_JoinRoom : Chat_Model_Base
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

	public Chat_Model_JoinRoom()
	{
		m_packetType = CHAT_PACKET_TYPE.JOIN_ROOM;
	}

	public override string Serialize()
	{
		string arg = $"{0:D32}".Substring(RoomId.Length) + RoomId;
		return $"{arg}{NickName}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_Base Parse(string str)
	{
		Chat_Model_JoinRoom chat_Model_JoinRoom = new Chat_Model_JoinRoom();
		chat_Model_JoinRoom.m_packetType = CHAT_PACKET_TYPE.JOIN_ROOM;
		chat_Model_JoinRoom.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
		chat_Model_JoinRoom.RoomId = str.Substring(40, 32);
		string errorType = str.Substring(72);
		chat_Model_JoinRoom.SetErrorType(errorType);
		return chat_Model_JoinRoom;
	}

	public static Chat_Model_JoinRoom Create(string roomId, string nickName)
	{
		Chat_Model_JoinRoom obj = new Chat_Model_JoinRoom
		{
			RoomId = roomId,
			NickName = nickName
		};
		obj.payload = obj.Serialize();
		return obj;
	}
}
