using Network;
using System;
using System.Collections.Generic;
using System.Linq;

public class ShopManager : MonoBehaviourSingleton<ShopManager>
{
	private const int DORAS_PLAINS_EAST_MAP_ID = 10010600;

	public bool offerBundlePack;

	public bool trackPlayerDie;

	public bool HasCheckPromotionItem;

	public bool IsCheckingPromotionItem;

	public ShopList shopData
	{
		get;
		private set;
	}

	public ShopBuyResult buyResult
	{
		get;
		private set;
	}

	public ProductDataList purchaseItemList
	{
		get;
		private set;
	}

	public DarkMarketItemList darkMarketItemList
	{
		get;
		private set;
	}

	public ShopManager()
	{
		shopData = new ShopList();
		SendGetGoldPurchaseItemList(delegate
		{
		});
	}

	private void Start()
	{
		if (MonoBehaviourSingleton<ShopReceiver>.IsValid())
		{
			ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
			i.onPromotionItem = (Action<bool>)Delegate.Combine(i.onPromotionItem, new Action<bool>(OnPromotionItem));
		}
	}

	private void OnDestroy()
	{
		if (MonoBehaviourSingleton<ShopReceiver>.IsValid())
		{
			ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
			i.onPromotionItem = (Action<bool>)Delegate.Remove(i.onPromotionItem, new Action<bool>(OnPromotionItem));
		}
	}

	public bool isNeedShowBundleOffer()
	{
		if (!purchaseItemList.hasPurchaseBundle)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap(10010600))
		{
			return false;
		}
		if (!trackPlayerDie)
		{
			return false;
		}
		return true;
	}

	public unsafe void OnPromotionItem(bool success)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		IsCheckingPromotionItem = false;
		if (success)
		{
			if (_003C_003Ef__am_0024cache9 == null)
			{
				_003C_003Ef__am_0024cache9 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			Protocol.Force(_003C_003Ef__am_0024cache9);
		}
	}

	public void GetPurchaseItem(string productId, ref ProductData data, ref int index)
	{
		int num = 0;
		int count = purchaseItemList.shopList.Count;
		while (true)
		{
			if (num >= count)
			{
				return;
			}
			if (purchaseItemList.shopList[num].productId == productId)
			{
				break;
			}
			num++;
		}
		data = purchaseItemList.shopList[num];
		index = num;
	}

	public void Dirty()
	{
	}

	public ShopList.ShopLineup GetLineup(int Lineup_id)
	{
		return shopData.lineups.Find((ShopList.ShopLineup o) => o.shopLineupId == Lineup_id);
	}

	public void SendGetShop(Action<bool> call_back)
	{
		shopData = null;
		Protocol.Send(ShopListModel.URL, delegate(ShopListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				shopData = ret.result;
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendBuy(int shopLineupId, Action<Error> call_back)
	{
		ShopBuyModel.RequestSendForm requestSendForm = new ShopBuyModel.RequestSendForm();
		requestSendForm.id = shopLineupId;
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		buyResult = null;
		Protocol.Send(ShopBuyModel.URL, requestSendForm, delegate(ShopBuyModel ret)
		{
			if (ret.Error == Error.None)
			{
				buyResult = ret.result;
				Dirty();
				if (buyResult.reward.Count > 0)
				{
					ShopList.ShopLineup lineup = GetLineup(shopLineupId);
					if (lineup != null)
					{
						Dictionary<string, object> values = new Dictionary<string, object>
						{
							{
								"currency_type",
								"gem"
							},
							{
								"currency_value",
								lineup.crystalNum
							},
							{
								"item_id",
								buyResult.reward[0].itemId
							},
							{
								"amount",
								buyResult.reward[0].num
							}
						};
						MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Credit_Spend_purchase_potion", "Credit_Spend", values);
					}
				}
			}
			call_back(ret.Error);
		}, string.Empty);
	}

	public unsafe void SendGetGoldPurchaseItemList(Action<bool> call_back)
	{
		purchaseItemList = null;
		GoldPurchaseItemListModel.SendForm sendForm = new GoldPurchaseItemListModel.SendForm();
		sendForm.checkSum = ((purchaseItemList == null) ? string.Empty : purchaseItemList.checkSum);
		Protocol.Send(GoldPurchaseItemListModel.URL, sendForm, delegate(GoldPurchaseItemListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				if (purchaseItemList == null || !purchaseItemList.checkSum.Equals(ret.result.checkSum))
				{
					purchaseItemList = ret.result;
					List<ProductData> shopList = purchaseItemList.shopList;
					if (_003CSendGetGoldPurchaseItemList_003Ec__AnonStorey6AB._003C_003Ef__am_0024cache2 == null)
					{
						_003CSendGetGoldPurchaseItemList_003Ec__AnonStorey6AB._003C_003Ef__am_0024cache2 = new Func<ProductData, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					string productNameData = string.Join("----", shopList.Select<ProductData, string>(_003CSendGetGoldPurchaseItemList_003Ec__AnonStorey6AB._003C_003Ef__am_0024cache2).ToArray());
					Native.SetProductNameData(productNameData);
					List<ProductData> shopList2 = purchaseItemList.shopList;
					if (_003CSendGetGoldPurchaseItemList_003Ec__AnonStorey6AB._003C_003Ef__am_0024cache3 == null)
					{
						_003CSendGetGoldPurchaseItemList_003Ec__AnonStorey6AB._003C_003Ef__am_0024cache3 = new Func<ProductData, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					productNameData = string.Join("----", shopList2.Select<ProductData, string>(_003CSendGetGoldPurchaseItemList_003Ec__AnonStorey6AB._003C_003Ef__am_0024cache3).ToArray());
					Native.SetProductIdData(productNameData);
					obj = true;
					MonoBehaviourSingleton<AppMain>.I.UpdatePurchaseItemListRequestTime();
					GameSaveData.instance.iAPBundleBought = string.Empty;
				}
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	private void AddTestPack()
	{
		ProductData productData = new ProductData();
		productData.productId = "net.gogame.dragon.sku_conversion3days";
		productData.oldPrice = 0.0;
		productData.price = 1.99;
		productData.crystalNum = 100;
		productData.priceIncludeTax = 1.99;
		productData.productType = 2;
		productData.offerType = 1;
		purchaseItemList.shopList.Add(productData);
		ProductData productData2 = new ProductData();
		productData2.productId = "net.gogame.dragon.sku_conversion7days";
		productData2.oldPrice = 0.0;
		productData2.price = 1.99;
		productData2.crystalNum = 100;
		productData2.priceIncludeTax = 1.99;
		productData2.productType = 2;
		productData2.offerType = 1;
		purchaseItemList.shopList.Add(productData2);
		ProductData productData3 = new ProductData();
		productData3.productId = "net.gogame.dragon.sku_loyalty_fish";
		productData3.oldPrice = 0.0;
		productData3.price = 1.99;
		productData3.crystalNum = 100;
		productData3.priceIncludeTax = 1.99;
		productData3.productType = 2;
		productData3.offerType = 1;
		purchaseItemList.shopList.Add(productData3);
	}

	public void SendGoldCanPurchase(string product_id, string safety_lock_password, Action<Error> call_back)
	{
		GoldCanPurchaseModel.RequestSendForm requestSendForm = new GoldCanPurchaseModel.RequestSendForm();
		requestSendForm.productId = product_id;
		requestSendForm.safetyLockPassword = safety_lock_password;
		Protocol.Send(GoldCanPurchaseModel.URL, requestSendForm, delegate(GoldCanPurchaseModel ret)
		{
			call_back(ret.Error);
		}, string.Empty);
	}

	public void SendDarkMarketCanPurchase(string product_id, int darkMarketId, string safety_lock_password, Action<Error> call_back)
	{
		GoldCanPurchaseModel.RequestSendForm requestSendForm = new GoldCanPurchaseModel.RequestSendForm();
		requestSendForm.productId = product_id;
		requestSendForm.safetyLockPassword = safety_lock_password;
		requestSendForm.marketId = darkMarketId;
		Protocol.Send(GoldCanPurchaseModel.URL, requestSendForm, delegate(GoldCanPurchaseModel ret)
		{
			call_back(ret.Error);
		}, string.Empty);
	}

	public void SendCheckPromotion()
	{
		if (!IsCheckingPromotionItem && !HasCheckPromotionItem)
		{
			if (purchaseItemList == null || purchaseItemList.promotionList.Count == 0)
			{
				Log.Error("Promotion List is null!");
			}
			else
			{
				HasCheckPromotionItem = true;
				IsCheckingPromotionItem = true;
				Native.checkAndGivePromotionItems(string.Join("----", purchaseItemList.promotionList.ToArray()));
			}
		}
	}

	public void SendGetDarkMarketItemList(Action<bool> call_back)
	{
		darkMarketItemList = null;
		Protocol.Send(DarkMarketListModel.URL, delegate(DarkMarketListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				if (!string.IsNullOrEmpty(ret.currentTime) && MonoBehaviourSingleton<GoGameTimeManager>.IsValid())
				{
					GoGameTimeManager.SetServerTime(ret.currentTime);
				}
				darkMarketItemList = ret.result;
				if (!string.IsNullOrEmpty(ret.result.endDate) && !GameSaveData.instance.resetMarketTime.Equals(ret.result.endDate))
				{
					int num = (int)GoGameTimeManager.GetRemainTime(ret.result.endDate).TotalSeconds;
					if (num > 0)
					{
						GameSaveData.instance.canShowNoteDarkMarket = true;
						GameSaveData.instance.resetMarketTime = ret.result.endDate;
					}
				}
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	public DarkMarketItem GetDarkMarketItem(int itemId)
	{
		if (darkMarketItemList != null)
		{
			int count = darkMarketItemList.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (darkMarketItemList.items[i].id == itemId)
				{
					return darkMarketItemList.items[i];
				}
			}
		}
		return null;
	}

	public string GetListProductData()
	{
		if (darkMarketItemList != null)
		{
			List<string> list = new List<string>();
			int count = darkMarketItemList.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (darkMarketItemList.items[i].saleType == 200)
				{
					if (!list.Contains(darkMarketItemList.items[i].saleoffProductId))
					{
						list.Add(darkMarketItemList.items[i].saleoffProductId);
					}
					if (!string.IsNullOrEmpty(darkMarketItemList.items[i].refProductId) && !list.Contains(darkMarketItemList.items[i].refProductId))
					{
						list.Add(darkMarketItemList.items[i].refProductId);
					}
				}
			}
			if (list.Count > 0)
			{
				return string.Join("----", list.ToArray());
			}
		}
		return string.Empty;
	}

	public void SendBuyDarkMarket(int darkMarketId, Action<Error> call_back)
	{
		DarkMarketBuyModel.RequestSendForm requestSendForm = new DarkMarketBuyModel.RequestSendForm();
		requestSendForm.marketId = darkMarketId;
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(DarkMarketBuyModel.URL, requestSendForm, delegate(DarkMarketBuyModel ret)
		{
			call_back(ret.Error);
		}, string.Empty);
	}

	public void UpdateDarkMarketUsedCount(int darkMarketId, int usedCount)
	{
		if (darkMarketItemList != null)
		{
			int count = darkMarketItemList.items.Count;
			for (int i = 0; i < count; i++)
			{
				if (darkMarketItemList.items[i].id == darkMarketId)
				{
					darkMarketItemList.items[i].usedCount = usedCount;
				}
			}
		}
	}
}
