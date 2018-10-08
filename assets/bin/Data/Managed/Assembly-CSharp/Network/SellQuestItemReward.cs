using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class SellQuestItemReward
	{
		public QuestCompleteReward sellOrder = new QuestCompleteReward();

		public List<QuestCompleteReward.SellItem> sell = new List<QuestCompleteReward.SellItem>();
	}
}
