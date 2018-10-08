using System;

namespace Network
{
	[Serializable]
	public class FriendCharaInfo : CharaInfo
	{
		public bool following;

		public bool follower;

		public int requestId;
	}
}
