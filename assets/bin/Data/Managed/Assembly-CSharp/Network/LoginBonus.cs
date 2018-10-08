using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class LoginBonus
	{
		public class LoginBonusReward
		{
			public string name;

			public int type;

			public int itemId;

			public int itemNum;

			public bool isGet;

			public bool isPickUp;

			public string pickUpText;

			public string day;

			public int frameType;

			public float scale;

			public float GetScale()
			{
				if (scale == 0f)
				{
					return 1f;
				}
				return scale;
			}
		}

		public class NextReward
		{
			public int count;

			public List<LoginBonusReward> reward = new List<LoginBonusReward>();
		}

		public string name;

		public int type;

		public int total;

		public int rotate;

		public int nowCount;

		public List<LoginBonusReward> reward = new List<LoginBonusReward>();

		public List<NextReward> next = new List<NextReward>();

		public bool isBeginner2Pop;

		public int priority;

		public string period_announce;

		public int boardType;

		public int loginBonusId;

		public int usePickUp;
	}
}
