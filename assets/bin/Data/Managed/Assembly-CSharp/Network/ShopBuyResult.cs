using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class ShopBuyResult
	{
		public class ShopReward
		{
			public int itemId;

			public int num;
		}

		public List<ShopReward> reward;
	}
}
