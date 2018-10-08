using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class PointShop
	{
		public int pointShopId;

		public int userPoint;

		public bool isEvent;

		public string expire;

		public List<PointShopItem> items;
	}
}
