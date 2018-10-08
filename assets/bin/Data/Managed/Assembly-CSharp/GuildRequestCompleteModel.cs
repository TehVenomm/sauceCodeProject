using Network;
using System;
using System.Collections.Generic;

public class GuildRequestCompleteModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public QuestCompleteRewardList reward = new QuestCompleteRewardList();

		public List<PointShopResultData> bonusPointShop = new List<PointShopResultData>();

		public List<PointEventCurrentData> pointEvent;
	}

	public class RequestSendForm
	{
		public int slotNo;
	}

	public static string URL = "ajax/guild-request/complete";

	public Param result = new Param();
}
