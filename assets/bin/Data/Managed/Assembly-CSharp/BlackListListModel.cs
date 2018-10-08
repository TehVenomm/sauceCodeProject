using Network;
using System;
using System.Collections.Generic;

public class BlackListListModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int pageNumMax;

		public List<FriendCharaInfo> black = new List<FriendCharaInfo>();
	}

	public class RequestSendForm
	{
		public int page;
	}

	public static string URL = "ajax/blacklist/list";

	public Param result = new Param();
}
