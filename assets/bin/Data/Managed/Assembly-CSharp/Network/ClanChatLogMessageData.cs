using System;

namespace Network
{
	[Serializable]
	public class ClanChatLogMessageData
	{
		public int fromUserId;

		public int toUserId;

		public int serverTime;

		public int clientTime;

		public int id;

		public int type;

		public int stampId;

		public string senderName;

		public string message;

		public string uuid;

		public string createdAt;
	}
}
