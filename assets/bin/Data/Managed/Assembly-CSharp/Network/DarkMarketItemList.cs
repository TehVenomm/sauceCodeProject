using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class DarkMarketItemList
	{
		public List<DarkMarketItem> items = new List<DarkMarketItem>();

		public string startDate;

		public string endDate;
	}
}
