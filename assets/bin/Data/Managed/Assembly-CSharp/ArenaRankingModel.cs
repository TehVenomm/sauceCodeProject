using Network;
using System.Collections.Generic;

public class ArenaRankingModel : BaseModel
{
	public class RequestSendForm
	{
		public int groupId;

		public int isContainSelf;
	}

	public static string URL = "ajax/arena/ranking";

	public List<ArenaRankingData> result = new List<ArenaRankingData>();
}
