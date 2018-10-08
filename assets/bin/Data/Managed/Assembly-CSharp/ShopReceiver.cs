using Network;
using System;
using UnityEngine;

public class ShopReceiver : MonoBehaviourSingleton<ShopReceiver>
{
	private class TrackPurchaseData
	{
		public string productId;

		public string purchaseData;

		public string signature;

		public string currency;

		public double price;
	}

	[Serializable]
	public class PaymentPurchaseData
	{
		[Serializable]
		public class PaymentItemData
		{
			public string name;

			public int type;

			public int itemId;

			public int num;
		}

		public string productId;

		public string productName;

		public int crystal;

		public int productType;

		public PaymentItemData[] bundle;
	}

	[Serializable]
	public class OriginalPurchaseData
	{
		public PaymentPurchaseData result;
	}

	public int BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE = 3;

	public Action onBillingUnavailable;

	public Action<string> onBuyItem;

	public Action<string> onBuyGacha;

	public Action<PaymentPurchaseData> onBuySpecialItem;

	public Action<PaymentPurchaseData> onBuyMaterialItem;

	public Action<StoreDataList> onGetProductDatas;

	public Action<bool> onPromotionItem;

	public void buyItem(string json)
	{
		if (json == null)
		{
			onBuyItem(null);
		}
		else
		{
			int result = 0;
			int.TryParse(json, out result);
			if (result != BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE)
			{
				try
				{
					OriginalPurchaseData originalPurchaseData = JsonUtility.FromJson<OriginalPurchaseData>(json);
					PaymentPurchaseData result2 = originalPurchaseData.result;
					if (result2.productType == 1)
					{
						onBuyItem(result2.productId);
					}
					else if (result2.productType == 2)
					{
						onBuySpecialItem(result2);
						GameSaveData.instance.iAPBundleBought = $"{GameSaveData.instance.iAPBundleBought}/{result2.productId}";
					}
					else if (result2.productType == 4)
					{
						onBuyGacha(result2.productId);
					}
					else
					{
						onBuyMaterialItem(result2);
					}
				}
				catch (Exception ex)
				{
					Log.Error(ex.ToString());
					if (onBuyItem != null)
					{
						onBuyItem(null);
					}
				}
			}
			else
			{
				onBillingUnavailable();
			}
		}
	}

	public void promoteItem(string success)
	{
		onPromotionItem(Convert.ToBoolean(success));
	}

	public void promoteCheck(string data)
	{
		MonoBehaviourSingleton<ShopManager>.I.HasCheckPromotionItem = false;
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene")
		{
			MonoBehaviourSingleton<ShopManager>.I.SendCheckPromotion();
		}
	}

	public void paymentsFinished(string itemId)
	{
		if (onBuyItem != null)
		{
			onBuyItem(null);
		}
	}

	public void getProductDatas(string json)
	{
		try
		{
			StoreDataList obj = JSONSerializer.Deserialize<StoreDataList>(json);
			if (onGetProductDatas != null)
			{
				onGetProductDatas(obj);
			}
		}
		catch (Exception ex)
		{
			Log.Error(ex.ToString());
			if (onGetProductDatas != null)
			{
				onGetProductDatas(null);
			}
		}
	}

	public void TrackPurchase(string jsonData)
	{
		try
		{
			TrackPurchaseData trackPurchaseData = JsonUtility.FromJson<TrackPurchaseData>(jsonData);
			MonoBehaviourSingleton<GoWrapManager>.I.trackPurchase(trackPurchaseData.productId, trackPurchaseData.currency, trackPurchaseData.price, trackPurchaseData.purchaseData, trackPurchaseData.signature);
		}
		catch (Exception ex)
		{
			Log.Error(ex.ToString());
		}
	}
}
