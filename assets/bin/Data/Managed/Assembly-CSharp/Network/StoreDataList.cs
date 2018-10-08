using System;
using System.Collections.Generic;
using System.Linq;

namespace Network
{
	[Serializable]
	public class StoreDataList
	{
		public List<StoreData> shopList = new List<StoreData>();

		public StoreData getProduct(string id)
		{
			return shopList.FirstOrDefault((StoreData o) => o.productId == id);
		}
	}
}
