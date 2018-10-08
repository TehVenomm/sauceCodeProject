using System;

namespace Network
{
	[Serializable]
	public class FriendMessageData
	{
		public string id;

		public int fromUserId;

		public int toUserId;

		public string message;

		public string createdAt;

		public long lid => long.Parse(id);
	}
}
