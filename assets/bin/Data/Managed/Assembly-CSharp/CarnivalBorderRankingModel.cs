using Network;
using System.Collections.Generic;

public class CarnivalBorderRankingModel : BaseModel
{
	public class RequestSendForm
	{
		public int sa;
	}

	public static string URL = "ajax/carnival/border-ranking";

	public List<CarnivalFriendCharaInfo> result = new List<CarnivalFriendCharaInfo>();
}
