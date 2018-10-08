using System;
using System.Collections.Generic;
using System.Linq;

namespace Network
{
	[Serializable]
	public class ProductDataList
	{
		public string checkSum;

		public List<SkuAdsData> skuPopups = new List<SkuAdsData>();

		public bool hasPurchaseBundle;

		public List<string> promotionList = new List<string>();

		public List<ProductData> shopList = new List<ProductData>();

		public unsafe List<ProductData> GetGemList()
		{
			List<ProductData> source = shopList;
			if (_003C_003Ef__am_0024cache5 == null)
			{
				_003C_003Ef__am_0024cache5 = new Func<ProductData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			return source.Where(_003C_003Ef__am_0024cache5).ToList();
		}

		public unsafe List<ProductData> GetBundleList()
		{
			List<ProductData> source = shopList;
			if (_003C_003Ef__am_0024cache6 == null)
			{
				_003C_003Ef__am_0024cache6 = new Func<ProductData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			return source.Where(_003C_003Ef__am_0024cache6).ToList();
		}

		public unsafe bool HasPurchasedBundle()
		{
			List<ProductData> source = shopList;
			if (_003C_003Ef__am_0024cache7 == null)
			{
				_003C_003Ef__am_0024cache7 = new Func<ProductData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			List<ProductData> list = source.Where(_003C_003Ef__am_0024cache7).ToList();
			return list != null && list.Count < MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.TotalPack();
		}
	}
}
