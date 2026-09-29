// Decompiled with JetBrains decompiler
// Type: TradingPostManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TradingPostManager : MonoBehaviourSingleton<TradingPostManager>
{
  public bool tradingEnable;
  public int tradingDay;
  public int tradingStatus;
  public int tradingAccept;
  public int tradingConditionDay;
  public int tradingSellMinGem;
  public int tradingSellMaxGem;
  public string tradingLastSold;
  public Dictionary<int, List<TradingPostInfo>> InfoDic = new Dictionary<int, List<TradingPostInfo>>();
  public TradingPostManager.TradingPostSellItemData tradingPostSellItemData = new TradingPostManager.TradingPostSellItemData();

  public TradingPostInfo Viewinfo { get; set; }

  public List<int> itemValidList { get; set; }

  public string startSectionName { get; set; }

  public int tradingPostFindItemId { get; set; }

  public bool isCheckUserAgreementSuccess { get; set; }

  public bool isRefreshTradingPost { get; set; }

  public int tradingPostSoldNum { get; private set; }

  public string tradingPostLastSoldTime { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.SetTradingPostLastSoldTime();
  }

  public void SetTradingPostInfo(HomeInfoModel.Param result)
  {
    this.tradingEnable = result.tradingEnable;
    this.tradingDay = result.tradingDay;
    this.tradingStatus = result.tradingStatus;
    this.tradingAccept = result.tradingAccept;
    this.tradingConditionDay = result.tradingConditionDay;
    this.tradingSellMinGem = result.tradingSellMinGem;
    this.tradingSellMaxGem = result.tradingSellMaxGem;
    this.tradingLastSold = result.tradingLastSold;
  }

  public void SetTradingPostFindData(int itemId) => this.tradingPostFindItemId = itemId;

  public void RemoveTradingPostFindData() => this.tradingPostFindItemId = 0;

  public void SetTradingPostSellItemData(uint itemId, ulong uniqID, int quantity)
  {
    this.tradingPostSellItemData.itemId = itemId;
    this.tradingPostSellItemData.uniqID = uniqID;
    this.tradingPostSellItemData.itemQuantity = quantity;
  }

  public void UpdateTradingPostSoldCount(int soldNum) => this.tradingPostSoldNum = soldNum;

  public void RemoveTradingPostSoldCount() => this.tradingPostSoldNum = 0;

  public static bool IsItemValid(uint itemId)
  {
    return Singleton<TradingPostTable>.I.IsExistItemData(itemId) && !Singleton<TradingPostTable>.I.GetItemData(itemId).cantSell;
  }

  public void SetTradingPostLastSold(string val) => this.tradingLastSold = val;

  public void SetTradingPostLastSoldTime()
  {
    MonoBehaviourSingleton<TradingPostManager>.I.tradingPostLastSoldTime = PlayerPrefs.GetString("TradingPost.LastSoldTime", "");
  }

  public void SaveTradingPostLastSoldTime()
  {
    PlayerPrefs.SetString("TradingPost.LastSoldTime", this.tradingLastSold);
    MonoBehaviourSingleton<TradingPostManager>.I.tradingPostLastSoldTime = this.tradingLastSold;
  }

  public void SendRequestInfo(int page, Action<bool, List<TradingPostInfo>> callback)
  {
    Protocol.Send<TradingPostInfoModel.RequestSendForm, TradingPostInfoModel>(TradingPostInfoModel.URL, new TradingPostInfoModel.RequestSendForm()
    {
      page = page
    }, (Action<TradingPostInfoModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        if (ret.result != null)
        {
          List<TradingPostInfo> result = ret.result;
          foreach (TradingPostInfo tradingPostInfo in result)
            tradingPostInfo.pageId = page;
          if (this.InfoDic.ContainsKey(page))
            this.InfoDic[page] = result;
          else
            this.InfoDic.Add(page, result);
        }
      }
      if (callback == null)
        return;
      callback(flag, ret.result);
    }));
  }

  public void SendRequestItemDetail(
    int itemId,
    int page,
    Action<bool, List<TradingPostDetail>> callback)
  {
    Protocol.Send<TradingPostItemDetailModel.RequestSendForm, TradingPostItemDetailModel>(TradingPostItemDetailModel.URL, new TradingPostItemDetailModel.RequestSendForm()
    {
      page = page,
      itemId = itemId
    }, (Action<TradingPostItemDetailModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      if (callback == null)
        return;
      callback(flag, ret.result);
    }));
  }

  public void SendRequestFindItem(int itemId, Action<bool, TradingPostInfo> callback)
  {
    Protocol.Send<TradingPostInfoModel.RequestSendForm, TradingPostInfoModel>(TradingPostInfoModel.URL, new TradingPostInfoModel.RequestSendForm()
    {
      itemId = itemId
    }, (Action<TradingPostInfoModel>) (ret =>
    {
      TradingPostInfo tradingPostInfo = ret.result == null || ret.result.Count <= 0 ? (TradingPostInfo) null : ret.result[0];
      if (callback == null)
        return;
      callback(tradingPostInfo != null, tradingPostInfo);
    }));
  }

  public void SendRequestUserAgreement(Action<bool> callback)
  {
    Protocol.Send<TradingPostUserAgreementModel>(TradingPostUserAgreementModel.URL, (Action<TradingPostUserAgreementModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        MonoBehaviourSingleton<TradingPostManager>.I.tradingDay = ret.result.tradingDay;
        MonoBehaviourSingleton<TradingPostManager>.I.tradingStatus = ret.result.tradingStatus;
        MonoBehaviourSingleton<TradingPostManager>.I.tradingAccept = ret.result.tradingAccept;
        flag = true;
      }
      if (callback == null)
        return;
      callback(flag);
    }));
  }

  public void SendRequestSellItem(int uid, int quantity, int price, Action<bool> callback)
  {
    Protocol.Send<TradingPostSellItemModel.RequestSendForm, TradingPostSellItemModel>(TradingPostSellItemModel.URL, new TradingPostSellItemModel.RequestSendForm()
    {
      uid = uid,
      quantity = quantity,
      price = price
    }, (Action<TradingPostSellItemModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      callback(flag);
    }));
  }

  public void SendRequestItemStartingAtPrice(
    int itemId,
    Action<bool, TradingPostItemStartingAtPriceModel> callback)
  {
    Protocol.Send<TradingPostItemStartingAtPriceModel.RequestSendForm, TradingPostItemStartingAtPriceModel>(TradingPostItemStartingAtPriceModel.URL, new TradingPostItemStartingAtPriceModel.RequestSendForm()
    {
      itemId = itemId
    }, (Action<TradingPostItemStartingAtPriceModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      callback(flag, ret);
    }));
  }

  public void SendRequestBuyItem(int transactionId, Action<bool, Error> callback)
  {
    Protocol.Send<TradingPostBuyItemModel.RequestSendForm, TradingPostBuyItemModel>(TradingPostBuyItemModel.URL, new TradingPostBuyItemModel.RequestSendForm()
    {
      transactionId = transactionId,
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<TradingPostBuyItemModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      callback(flag, ret.Error);
    }));
  }

  public void SendRequestRemoveTransaction(int transactionId, Action<bool, Error> callback)
  {
    Protocol.Send<TradingPostRemoveTransactionModel.RequestSendForm, TradingPostRemoveTransactionModel>(TradingPostRemoveTransactionModel.URL, new TradingPostRemoveTransactionModel.RequestSendForm()
    {
      transactionId = transactionId
    }, (Action<TradingPostRemoveTransactionModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      callback(flag, ret.Error);
    }));
  }

  public void SendRequestLogInfo(Action<bool, TradingPostTransactionLog> callback)
  {
    Protocol.Send<TradingPostHistoryLogModel>(TradingPostHistoryLogModel.URL, (WWWForm) null, (Action<TradingPostHistoryLogModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
        flag = true;
      callback(flag, ret.result);
    }));
  }

  public static void ShowUnavailableDialog()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "This feature is not available now. Please come back later", StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U)), (Action<string>) (s => { }));
  }

  public static bool IsTradingEnable()
  {
    return MonoBehaviourSingleton<TradingPostManager>.I.tradingEnable;
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
    return TradingPostManager.IsPurchasedLicense() || TradingPostManager.IsLoginRequireFinish();
  }

  public static bool IsFinishTradingPostTutorial()
  {
    return GameSaveData.instance.isFinishTradingPostTutorial;
  }

  public static bool IsNewTradingPostSold()
  {
    return MonoBehaviourSingleton<TradingPostManager>.IsValid() && !string.Equals(MonoBehaviourSingleton<TradingPostManager>.I.tradingPostLastSoldTime, MonoBehaviourSingleton<TradingPostManager>.I.tradingLastSold);
  }

  public class TradingPostSellItemData
  {
    public uint itemId;
    public ulong uniqID;
    public int itemQuantity;
  }
}
