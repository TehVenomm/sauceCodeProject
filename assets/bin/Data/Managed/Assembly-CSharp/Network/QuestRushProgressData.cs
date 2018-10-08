using System.Collections.Generic;

namespace Network
{
	public class QuestRushProgressData
	{
		public class RushTimeBonus
		{
			public string bonusName = string.Empty;

			public int plusSec;
		}

		public QuestCompleteRewardList reward = new QuestCompleteRewardList();

		public int remainSec;

		public List<RushTimeBonus> plusSec = new List<RushTimeBonus>();

		public List<PointEventCurrentData> pointEvent;

		public List<PointShopResultData> pointShop = new List<PointShopResultData>();
	}
}
