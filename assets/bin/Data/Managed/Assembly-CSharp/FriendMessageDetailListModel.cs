using Network;
using System;
using System.Collections.Generic;

public class FriendMessageDetailListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int pageNumMax;

		public List<FriendMessageData> message = new List<FriendMessageData>();
	}

	public class RequestSendForm
	{
		public int userId;

		public int page;
	}

	public static string URL = "ajax/friend/messagedetaillist";

	public Param result = new Param();
}
