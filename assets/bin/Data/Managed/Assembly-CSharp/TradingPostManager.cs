using Network;
using System;
using System.Collections.Generic;

public class TradingPostManager : MonoBehaviourSingleton<TradingPostManager>
{
	public class TradingPostSellItemData
	{
		public uint itemId;

		public ulong uniqID;

		public int itemQuantity;
	}

	public int tradingDay;

	public int tradingStatus;

	public int tradingAccept;

	public int tradingConditionDay;

	public int tradingSellMinGem;

	public int tradingSellMaxGem;

	public Dictionary<int, List<TradingPostInfo>> InfoDic = new Dictionary<int, List<TradingPostInfo>>();

	public TradingPostSellItemData tradingPostSellItemData = new TradingPostSellItemData();

	public TradingPostInfo Viewinfo
	{
		get;
		set;
	}

	public List<int> itemValidList
	{
		get;
		set;
	}

	public string startSectionName
	{
		get;
		set;
	}

	public int tradingPostFindItemId
	{
		get;
		set;
	}

	public bool isCheckUserAgreementSuccess
	{
		get;
		set;
	}

	public bool isRefreshTradingPost
	{
		get;
		set;
	}

	public void SetTradingPostInfo(HomeInfoModel.Param result)
	{
		tradingDay = result.tradingDay;
		tradingStatus = result.tradingStatus;
		tradingAccept = result.tradingAccept;
		tradingConditionDay = result.tradingConditionDay;
		tradingSellMinGem = result.tradingSellMinGem;
		tradingSellMaxGem = result.tradingSellMaxGem;
	}

	public void SetTradingPostFindData(int itemId)
	{
		tradingPostFindItemId = itemId;
	}

	public void RemoveTradingPostFindData()
	{
		tradingPostFindItemId = 0;
	}

	public void SetTradingPostSellItemData(uint itemId, ulong uniqID, int quantity)
	{
		tradingPostSellItemData.itemId = itemId;
		tradingPostSellItemData.uniqID = uniqID;
		tradingPostSellItemData.itemQuantity = quantity;
	}

	public static bool IsItemValid(uint itemId)
	{
		return Singleton<TradingPostTable>.I.IsExistItemData(itemId) && !Singleton<TradingPostTable>.I.GetItemData(itemId).cantSell;
	}

	public void SendRequestInfo(int page, Action<bool, List<TradingPostInfo>> callback)
	{
		TradingPostInfoModel.RequestSendForm requestSendForm = new TradingPostInfoModel.RequestSendForm();
		requestSendForm.page = page;
		Protocol.Send(TradingPostInfoModel.URL, requestSendForm, delegate(TradingPostInfoModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
				if (ret.result != null)
				{
					List<TradingPostInfo> result = ret.result;
					foreach (TradingPostInfo item in result)
					{
						item.pageId = page;
					}
					if (InfoDic.ContainsKey(page))
					{
						InfoDic[page] = result;
					}
					else
					{
						InfoDic.Add(page, result);
					}
				}
			}
			if (callback != null)
			{
				callback(arg, ret.result);
			}
		}, string.Empty);
	}

	public void SendRequestItemDetail(int itemId, int page, Action<bool, List<TradingPostDetail>> callback)
	{
		TradingPostItemDetailModel.RequestSendForm requestSendForm = new TradingPostItemDetailModel.RequestSendForm();
		requestSendForm.page = page;
		requestSendForm.itemId = itemId;
		Protocol.Send(TradingPostItemDetailModel.URL, requestSendForm, delegate(TradingPostItemDetailModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
			}
			if (callback != null)
			{
				callback(arg, ret.result);
			}
		}, string.Empty);
	}

	public void SendRequestFindItem(int itemId, Action<bool, TradingPostInfo> callback)
	{
		TradingPostInfoModel.RequestSendForm requestSendForm = new TradingPostInfoModel.RequestSendForm();
		requestSendForm.itemId = itemId;
		Protocol.Send(TradingPostInfoModel.URL, requestSendForm, delegate(TradingPostInfoModel ret)
		{
			TradingPostInfo tradingPostInfo = (ret.result == null || ret.result.Count <= 0) ? null : ret.result[0];
			if (callback != null)
			{
				callback(tradingPostInfo != null, tradingPostInfo);
			}
		}, string.Empty);
	}

	public void SendRequestUserAgreement(Action<bool> callback)
	{
		Protocol.Send(TradingPostUserAgreementModel.URL, delegate(TradingPostUserAgreementModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				MonoBehaviourSingleton<TradingPostManager>.I.tradingDay = ret.result.tradingDay;
				MonoBehaviourSingleton<TradingPostManager>.I.tradingStatus = ret.result.tradingStatus;
				MonoBehaviourSingleton<TradingPostManager>.I.tradingAccept = ret.result.tradingAccept;
				obj = true;
			}
			if (callback != null)
			{
				callback(obj);
			}
		}, string.Empty);
	}

	public void SendRequestSellItem(int uid, int quantity, int price, Action<bool> callback)
	{
		TradingPostSellItemModel.RequestSendForm requestSendForm = new TradingPostSellItemModel.RequestSendForm();
		requestSendForm.uid = uid;
		requestSendForm.quantity = quantity;
		requestSendForm.price = price;
		Protocol.Send(TradingPostSellItemModel.URL, requestSendForm, delegate(TradingPostSellItemModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
			}
			callback(obj);
		}, string.Empty);
	}

	public void SendRequestItemStartingAtPrice(int itemId, Action<bool, TradingPostItemStartingAtPriceModel> callback)
	{
		TradingPostItemStartingAtPriceModel.RequestSendForm requestSendForm = new TradingPostItemStartingAtPriceModel.RequestSendForm();
		requestSendForm.itemId = itemId;
		Protocol.Send(TradingPostItemStartingAtPriceModel.URL, requestSendForm, delegate(TradingPostItemStartingAtPriceModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
			}
			callback(arg, ret);
		}, string.Empty);
	}

	public void SendRequestBuyItem(int transactionId, Action<bool, Error> callback)
	{
		TradingPostBuyItemModel.RequestSendForm requestSendForm = new TradingPostBuyItemModel.RequestSendForm();
		requestSendForm.transactionId = transactionId;
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		Protocol.Send(TradingPostBuyItemModel.URL, requestSendForm, delegate(TradingPostBuyItemModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
			}
			callback(arg, ret.Error);
		}, string.Empty);
	}

	public void SendRequestRemoveTransaction(int transactionId, Action<bool, Error> callback)
	{
		TradingPostRemoveTransactionModel.RequestSendForm requestSendForm = new TradingPostRemoveTransactionModel.RequestSendForm();
		requestSendForm.transactionId = transactionId;
		Protocol.Send(TradingPostRemoveTransactionModel.URL, requestSendForm, delegate(TradingPostRemoveTransactionModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
			}
			callback(arg, ret.Error);
		}, string.Empty);
	}

	public void SendRequestLogInfo(Action<bool, TradingPostTransactionLog> callback)
	{
		Protocol.Send(TradingPostHistoryLogModel.URL, null, delegate(TradingPostHistoryLogModel ret)
		{
			bool arg = false;
			if (ret.Error == Error.None)
			{
				arg = true;
			}
			callback(arg, ret.result);
		}, string.Empty);
	}

	public static bool IsPurchasedLicense()
	{
		return MonoBehaviourSingleton<TradingPostManager>.I.tradingStatus >= 2;
	}

	public static bool IsLoginRequireFinish()
	{
		return MonoBehaviourSingleton<TradingPostManager>.I.tradingStatus >= 1;
	}

	public static bool IsAcceptUserAgreement()
	{
		return MonoBehaviourSingleton<TradingPostManager>.I.tradingAccept > 0;
	}

	public static bool IsFulfillRequirement()
	{
		return IsPurchasedLicense() || IsLoginRequireFinish();
	}

	public static bool IsFinishTradingPostTutorial()
	{
		return GameSaveData.instance.isFinishTradingPostTutorial;
	}
}
