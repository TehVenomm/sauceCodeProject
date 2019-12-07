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

		public List<ProductData> GetGemList()
		{
			return shopList.Where((ProductData o) => !o.isSpecial).ToList();
		}

		public List<ProductData> GetBundleList()
		{
			return shopList.Where((ProductData o) => Singleton<ProductDataTable>.I.HasPack(o.productId)).ToList();
		}

		public bool HasPurchasedBundle()
		{
			List<ProductData> list = shopList.Where((ProductData o) => Singleton<ProductDataTable>.I.HasPack(o.productId)).ToList();
			if (list != null)
			{
				return list.Count < Singleton<ProductDataTable>.I.TotalPack();
			}
			return false;
		}
	}
}
