using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class SellResult
	{
		public int money;

		public List<QuestCompleteReward.Item> exchange = new List<QuestCompleteReward.Item>();

		public List<QuestCompleteReward.SellItem> sell = new List<QuestCompleteReward.SellItem>();
	}
}
