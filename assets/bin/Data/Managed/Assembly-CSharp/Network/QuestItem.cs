using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class QuestItem
	{
		public class SellItem
		{
			public int type;

			public int itemId;

			public int param0;

			public int num;

			public int pri;
		}

		public string uniqId;

		public int questId;

		public int num;

		public List<SellItem> sellItems = new List<SellItem>();

		public QuestData.QuestRewardList reward = new QuestData.QuestRewardList();

		public List<float> remainTimes = new List<float>();
	}
}
