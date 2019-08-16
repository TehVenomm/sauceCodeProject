using Network;
using System.Collections.Generic;

public class CarnivalFriendRankingModel : BaseModel
{
	public class RequestSendForm
	{
		public int sa;
	}

	public static string URL = "ajax/carnival/friend-ranking";

	public List<CarnivalFriendCharaInfo> result = new List<CarnivalFriendCharaInfo>();
}
