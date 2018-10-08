using Network;
using System.Collections.Generic;

public class ArenaFriendRankingModel : BaseModel
{
	public class RequestSendForm
	{
		public int groupId;

		public int isContainSelf;
	}

	public static string URL = "ajax/arena/friend-ranking";

	public List<ArenaRankingData> result = new List<ArenaRankingData>();
}
