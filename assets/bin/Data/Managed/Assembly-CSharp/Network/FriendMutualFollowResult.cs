using System;

namespace Network
{
	[Serializable]
	public class FriendMutualFollowResult
	{
		public int targetUserId;

		public string targetUserName;

		public bool promotionSuccess;
	}
}
