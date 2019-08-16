using Network;
using System.Collections.Generic;

public class ClanDetailModel : BaseModel
{
	public class Param
	{
		public ClanData clan = new ClanData();

		public List<FriendCharaInfo> memberList = new List<FriendCharaInfo>();

		public bool isRequested;

		public bool isInvited;
	}

	public class RequestSendForm
	{
		public string cId;
	}

	public static string URL = "ajax/clan/detail";

	public Param result = new Param();
}
