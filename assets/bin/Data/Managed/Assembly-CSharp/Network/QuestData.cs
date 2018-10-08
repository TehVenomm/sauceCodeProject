using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class QuestData
	{
		public class QuestRewardList
		{
			public List<int> types = new List<int>();

			public List<int> itemIds = new List<int>();

			public List<int> pri = new List<int>();
		}

		public class OrderQuestInfo
		{
			public int num;
		}

		public int questId;

		public int crystalNum;

		public QuestRewardList reward = new QuestRewardList();

		public OrderQuestInfo order;

		public List<float> remainTimes = new List<float>();
	}
}
