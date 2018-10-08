using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class ShopList
	{
		public class ShopLineup
		{
			public int shopLineupId;

			public string name;

			public string description;

			public int crystalNum;

			public List<int> itemIds = new List<int>();
		}

		public List<ShopLineup> lineups = new List<ShopLineup>();
	}
}
