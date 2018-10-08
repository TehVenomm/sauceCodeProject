using System;
using System.Collections.Generic;

public class FriendMessagedMutualFollowerListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public List<FriendMessageUserListModel.MessageUserInfo> messageFollowList = new List<FriendMessageUserListModel.MessageUserInfo>();
	}

	public class RequestSendForm
	{
	}

	public static string URL = "ajax/friend/followmessagelist";

	public Param result = new Param();
}
