using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class QuestStartData
	{
		public class EnemyReward
		{
			public List<RegionDropItem> reward = new List<RegionDropItem>();

			public DropHpRate drop = new DropHpRate();
		}

		public class RegionDropItem
		{
			public int regionId;

			public List<BreakItem> breakReward = new List<BreakItem>();
		}

		public class BreakItem
		{
			public int rarity;

			public int type;

			public int num;
		}

		public class DropHpRate
		{
			public List<float> hpRate = new List<float>();

			public List<int> rarity = new List<int>();
		}

		public string qt;

		public List<EnemyReward> enemy = new List<EnemyReward>();
	}
}
