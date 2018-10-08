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
			_003CgetProduct_003Ec__AnonStorey56A _003CgetProduct_003Ec__AnonStorey56A;
			return shopList.FirstOrDefault(new Func<StoreData, bool>((object)_003CgetProduct_003Ec__AnonStorey56A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}
}
