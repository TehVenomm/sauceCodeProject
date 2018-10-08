using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class MysteryGift
	{
		public class MysteryGiftReward
		{
			public string name;

			public int type;

			public int itemId;

			public int num;
		}

		public List<MysteryGiftReward> present;

		public int received;

		public int next;
	}
}
