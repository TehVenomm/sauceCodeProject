using Network;
using System.Collections.Generic;

public class CarnivalTopRankingModel : BaseModel
{
	public class RequestSendForm
	{
		public int num;

		public int sa;
	}

	public static string URL = "ajax/carnival/top-ranking";

	public List<CarnivalFriendCharaInfo> result = new List<CarnivalFriendCharaInfo>();
}
