using Network;
using System;
using System.Collections.Generic;

public class PointShopManager
{
	public List<PointShop> pointShopList
	{
		get;
		private set;
	}

	public void SendGetPointShops(Action<bool, List<PointShop>> call_back)
	{
		Protocol.Send("ajax/pointshop/list", null, delegate(PointShopModel ret)
		{
			bool flag = false;
			if (ret.Error == Error.None)
			{
				pointShopList = ret.result;
				flag = true;
			}
			call_back.Invoke(flag, pointShopList);
		}, string.Empty);
	}

	public void SendPointShopBuy(PointShopItem pointShopItem, PointShop pointShop, int num, Action<bool> call_back)
	{
		PointShopBuyModel.SendForm sendForm = new PointShopBuyModel.SendForm();
		sendForm.uid = pointShopItem.pointShopItemId;
		sendForm.num = num;
		Protocol.Send(PointShopBuyModel.URL, sendForm, delegate(PointShopBuyModel result)
		{
			bool obj = false;
			if (result != null && result.Error == Error.None)
			{
				pointShopItem.buyCount += num;
				pointShop.userPoint -= pointShopItem.needPoint * num;
				obj = true;
			}
			call_back(obj);
		}, string.Empty);
	}

	public static string GetBoughtMessage(PointShopItem item, int num)
	{
		string empty = string.Empty;
		if (item.itemId != 1200000)
		{
			return string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 7u), item.name, num);
		}
		return string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 8u), item.name, num);
	}
}
