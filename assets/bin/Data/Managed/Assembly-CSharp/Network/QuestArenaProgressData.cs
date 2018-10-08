using System.Collections.Generic;

namespace Network
{
	public class QuestArenaProgressData
	{
		public class ArenaTimeBonus
		{
			public string bonusName = string.Empty;

			public int plusSec;
		}

		public XorInt remainMilliSec = 0;

		public List<ArenaTimeBonus> plusSec = new List<ArenaTimeBonus>();

		public QuestCompleteRewardList reward = new QuestCompleteRewardList();

		public List<PointShopResultData> pointShop = new List<PointShopResultData>();
	}
}
