using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class QuestCompleteRewardList
	{
		public QuestCompleteReward breakReward = new QuestCompleteReward();

		public QuestCompleteReward breakPartsReward = new QuestCompleteReward();

		public QuestCompleteReward drop = new QuestCompleteReward();

		public QuestCompleteReward mission = new QuestCompleteReward();

		public QuestCompleteReward followReward = new QuestCompleteReward();

		public QuestCompleteReward missionComplete = new QuestCompleteReward();

		public QuestCompleteReward first = new QuestCompleteReward();

		public QuestCompleteReward order = new QuestCompleteReward();

		public QuestCompleteReward boost = new QuestCompleteReward();

		public List<QuestCompleteReward.SellItem> sell = new List<QuestCompleteReward.SellItem>();
	}
}
