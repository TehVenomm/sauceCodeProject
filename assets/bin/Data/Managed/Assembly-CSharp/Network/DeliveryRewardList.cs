using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class DeliveryRewardList
	{
		public QuestCompleteReward delivery = new QuestCompleteReward();

		public List<QuestCompleteReward.SellItem> sell = new List<QuestCompleteReward.SellItem>();
	}
}
