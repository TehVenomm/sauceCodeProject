using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class HomeCharaInfoList
	{
		public CharaInfo own = new CharaInfo();

		public List<FriendCharaInfo> chara = new List<FriendCharaInfo>();
	}
}
