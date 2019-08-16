using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class DarkMarketItem
	{
		public int id;

		public int limit;

		public float baseNum;

		public float saleNum;

		public string saleoffProductId;

		public string refProductId;

		public int saleType;

		public int usedCount;

		public int feature;

		public string imgId;

		public string name;

		public int remain = -1;

		public List<DarkMarketReward> rewards;
	}
}
