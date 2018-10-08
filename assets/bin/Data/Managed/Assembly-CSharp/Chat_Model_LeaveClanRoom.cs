public class Chat_Model_LeaveClanRoom : Chat_Model_Base
{
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

	public int Owner
	{
		get;
		protected set;
	}

	public string Result
	{
		get;
		protected set;
	}

	public string UserId
	{
		get;
		protected set;
	}

	public Chat_Model_LeaveClanRoom()
	{
		m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM;
	}

	public override string Serialize()
	{
		string text = $"{0:D32}";
		string arg = text.Substring(RoomId.Length) + RoomId;
		return $"{arg}{NickName}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_Base Parse(string str)
	{
		Chat_Model_LeaveClanRoom chat_Model_LeaveClanRoom = null;
		string s = str.Substring(72, 8);
		int num = int.Parse(s);
		if (num == 1)
		{
			Chat_Model_LeaveClanRoom chat_Model_LeaveClanRoom2 = new Chat_Model_LeaveClanRoom();
			chat_Model_LeaveClanRoom2.m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM;
			chat_Model_LeaveClanRoom2.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
			chat_Model_LeaveClanRoom2.RoomId = str.Substring(40, 32);
			chat_Model_LeaveClanRoom2.Owner = int.Parse(s);
			chat_Model_LeaveClanRoom = chat_Model_LeaveClanRoom2;
			string errorType = str.Substring(80);
			chat_Model_LeaveClanRoom.SetErrorType(errorType);
		}
		else
		{
			Chat_Model_LeaveClanRoom chat_Model_LeaveClanRoom2 = new Chat_Model_LeaveClanRoom();
			chat_Model_LeaveClanRoom2.m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM;
			chat_Model_LeaveClanRoom2.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
			chat_Model_LeaveClanRoom2.RoomId = str.Substring(40, 32);
			chat_Model_LeaveClanRoom2.UserId = str.Substring(80, 32);
			chat_Model_LeaveClanRoom2.Owner = int.Parse(s);
			chat_Model_LeaveClanRoom = chat_Model_LeaveClanRoom2;
			chat_Model_LeaveClanRoom.SetErrorType("00000000");
		}
		return chat_Model_LeaveClanRoom;
	}

	public static Chat_Model_LeaveClanRoom Create(string roomId, string nickName)
	{
		Chat_Model_LeaveClanRoom chat_Model_LeaveClanRoom = new Chat_Model_LeaveClanRoom();
		chat_Model_LeaveClanRoom.RoomId = roomId;
		chat_Model_LeaveClanRoom.NickName = nickName;
		Chat_Model_LeaveClanRoom chat_Model_LeaveClanRoom2 = chat_Model_LeaveClanRoom;
		chat_Model_LeaveClanRoom2.payload = chat_Model_LeaveClanRoom2.Serialize();
		return chat_Model_LeaveClanRoom2;
	}
}
