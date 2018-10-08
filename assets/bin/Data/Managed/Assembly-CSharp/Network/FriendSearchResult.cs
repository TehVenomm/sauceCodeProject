using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class FriendSearchResult
	{
		public int pageNumMax;

		public List<FriendCharaInfo> search = new List<FriendCharaInfo>();
	}
}
