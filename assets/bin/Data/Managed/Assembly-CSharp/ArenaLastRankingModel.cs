using Network;
using System;
using System.Collections.Generic;

public class ArenaLastRankingModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public Network.EventData eventData;

		public List<ArenaRankingData> rankingDataList = new List<ArenaRankingData>();

		public int myRank;
	}

	public class RequestSendForm
	{
		public int groupId;

		public int isContainSelf;
	}

	public static string URL = "ajax/arena/last-ranking";

	public Param result = new Param();
}
