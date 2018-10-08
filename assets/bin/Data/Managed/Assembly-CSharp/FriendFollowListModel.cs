using Network;
using System;
using System.Collections.Generic;

public class FriendFollowListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int pageNumMax;

		public List<FriendCharaInfo> follow = new List<FriendCharaInfo>();
	}

	public class RequestSendForm
	{
		public int page;
	}

	public static string URL = "ajax/friend/followlist";

	public Param result = new Param();
}
