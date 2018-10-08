using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class GatherRewardList
	{
		public QuestCompleteReward gather = new QuestCompleteReward();

		public List<QuestCompleteReward.SellItem> sell = new List<QuestCompleteReward.SellItem>();
	}
}
