using System;

namespace Network
{
	[Serializable]
	public class FriendCharaInfo : CharaInfo
	{
		[Serializable]
		public class JoinInfo
		{
			public int joinType;

			public int targetParam;

			public string conditionParam = string.Empty;
		}

		public bool following;

		public bool follower;

		public int requestId;

		public int playedCount;

		public int following_id;

		public int follower_id;

		public JoinInfo joinStatus;
	}
}
