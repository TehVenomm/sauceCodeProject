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
			return (from o in shopList
			where !o.isSpecial
			select o).ToList();
		}

		public List<ProductData> GetBundleList()
		{
			return (from o in shopList
			where MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.HasPack(o.productId)
			select o).ToList();
		}

		public bool HasPurchasedBundle()
		{
			List<ProductData> list = (from o in shopList
			where MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.HasPack(o.productId)
			select o).ToList();
			return list != null && list.Count < MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.TotalPack();
		}
	}
}
