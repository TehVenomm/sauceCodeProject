using Network;
using System;
using System.Collections.Generic;

public class FriendMessageUserListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int pageNumMax;

		public List<MessageUserInfo> messageUser = new List<MessageUserInfo>();
	}

	public class MessageUserInfo : FriendCharaInfo
	{
		public int noReadNum;

		public bool isPermitted;
	}

	public class RequestSendForm
	{
		public int page;
	}

	public static string URL = "ajax/friend/messageuserlist";

	public Param result = new Param();
}
