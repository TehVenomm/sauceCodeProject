using System;
using System.Collections.Generic;
using System.Linq;

namespace Network
{
	[Serializable]
	public class StoreDataList
	{
		public List<StoreData> shopList = new List<StoreData>();

		public unsafe StoreData getProduct(string id)
		{
			_003CgetProduct_003Ec__AnonStorey54E _003CgetProduct_003Ec__AnonStorey54E;
			return shopList.FirstOrDefault(new Func<StoreData, bool>((object)_003CgetProduct_003Ec__AnonStorey54E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}
