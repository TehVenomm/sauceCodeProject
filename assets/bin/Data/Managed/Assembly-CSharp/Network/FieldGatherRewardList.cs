using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class FieldGatherRewardList
	{
		public QuestCompleteReward fieldGather = new QuestCompleteReward();

		public List<QuestCompleteReward.SellItem> sell = new List<QuestCompleteReward.SellItem>();
	}
}
