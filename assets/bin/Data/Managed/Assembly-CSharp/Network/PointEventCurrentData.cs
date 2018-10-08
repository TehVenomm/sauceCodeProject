using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class PointEventCurrentData
	{
		public class PointResultData
		{
			public List<PointRewardData> getReward = new List<PointRewardData>();

			public PointRewardData nextReward;

			public int userPoint;

			public int getPoint;

			public List<BonusPointData> bonusPoint = new List<BonusPointData>();

			public int beforeRank;

			public int afterRank;

			public bool isStartedBoost;
		}

		public class PointRewardData
		{
			public int point;

			public List<Reward> reward = new List<Reward>();
		}

		public class Reward
		{
			public int type;

			public int itemId;

			public int num;

			public string description;
		}

		public class BonusPointData
		{
			public string name;

			public int point;

			public float boostRate;
		}

		public int eventId;

		public PointResultData pointRankingData;

		public string rewardTitle;
	}
}
