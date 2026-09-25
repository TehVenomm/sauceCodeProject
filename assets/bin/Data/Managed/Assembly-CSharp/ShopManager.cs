// Decompiled with JetBrains decompiler
// Type: ShopManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class ShopManager : MonoBehaviourSingleton<ShopManager>
{
  private const int DORAS_PLAINS_EAST_MAP_ID = 10010600;
  public bool offerBundlePack;
  public bool trackPlayerDie;
  public bool HasCheckPromotionItem;
  public bool IsCheckingPromotionItem;

  public ShopList shopData { get; private set; }

  public ShopBuyResult buyResult { get; private set; }

  public ProductDataList purchaseItemList { get; private set; }

  public DarkMarketItemList darkMarketItemList { get; private set; }

  public ShopManager() => this.shopData = new ShopList();

  private void Start()
  {
    if (!MonoBehaviourSingleton<ShopReceiver>.IsValid())
      return;
    MonoBehaviourSingleton<ShopReceiver>.I.onPromotionItem += new Action<bool>(this.OnPromotionItem);
  }

  private void OnDestroy()
  {
    if (!MonoBehaviourSingleton<ShopReceiver>.IsValid())
      return;
    MonoBehaviourSingleton<ShopReceiver>.I.onPromotionItem -= new Action<bool>(this.OnPromotionItem);
  }

  public bool isNeedShowBundleOffer()
  {
    return this.purchaseItemList.hasPurchaseBundle && MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap(10010600) && this.trackPlayerDie;
  }

  public void OnPromotionItem(bool success)
  {
    this.IsCheckingPromotionItem = false;
    if (!success)
      return;
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, (Action<bool>) (is_success => MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST)))));
  }

  public void GetPurchaseItem(string productId, ref Network.ProductData data, ref int index)
  {
    int index1 = 0;
    for (int count = this.purchaseItemList.shopList.Count; index1 < count; ++index1)
    {
      if (this.purchaseItemList.shopList[index1].productId == productId)
      {
        data = this.purchaseItemList.shopList[index1];
        index = index1;
        break;
      }
    }
  }

  public void Dirty()
  {
  }

  public ShopList.ShopLineup GetLineup(int Lineup_id)
  {
    return this.shopData.lineups.Find((Predicate<ShopList.ShopLineup>) (o => o.shopLineupId == Lineup_id));
  }

  public void SendGetShop(Action<bool> call_back)
  {
    this.shopData = (ShopList) null;
    Protocol.Send<ShopListModel>(ShopListModel.URL, (Action<ShopListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.shopData = ret.result;
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public void SendBuy(int shopLineupId, Action<Error> call_back)
  {
    ShopBuyModel.RequestSendForm postData = new ShopBuyModel.RequestSendForm();
    postData.id = shopLineupId;
    postData.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
    this.buyResult = (ShopBuyResult) null;
    Protocol.Send<ShopBuyModel.RequestSendForm, ShopBuyModel>(ShopBuyModel.URL, postData, (Action<ShopBuyModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        this.buyResult = ret.result;
        this.Dirty();
        if (this.buyResult.reward.Count > 0)
        {
          ShopList.ShopLineup lineup = this.GetLineup(shopLineupId);
          if (lineup != null)
            MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Credit_Spend_purchase_potion", "Credit_Spend", new Dictionary<string, object>()
            {
              {
                "currency_type",
                (object) "gem"
              },
              {
                "currency_value",
                (object) lineup.crystalNum
              },
              {
                "item_id",
                (object) this.buyResult.reward[0].itemId
              },
              {
                "amount",
                (object) this.buyResult.reward[0].num
              }
            });
        }
      }
      call_back(ret.Error);
    }));
  }

  public void SendGetGoldPurchaseItemList(Action<bool> call_back)
  {
    this.purchaseItemList = (ProductDataList) null;
    Protocol.Send<GoldPurchaseItemListModel.SendForm, GoldPurchaseItemListModel>(GoldPurchaseItemListModel.URL, new GoldPurchaseItemListModel.SendForm()
    {
      checkSum = this.purchaseItemList != null ? this.purchaseItemList.checkSum : string.Empty
    }, (Action<GoldPurchaseItemListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        if (this.purchaseItemList == null || !this.purchaseItemList.checkSum.Equals(ret.result.checkSum))
        {
          this.purchaseItemList = ret.result;
          Native.SetProductNameData(string.Join("----", this.purchaseItemList.shopList.Select<Network.ProductData, string>((Func<Network.ProductData, string>) (x => x.name)).ToArray<string>()));
          Native.SetProductIdData(string.Join("----", this.purchaseItemList.shopList.Select<Network.ProductData, string>((Func<Network.ProductData, string>) (x => x.productId)).ToArray<string>()));
          flag = true;
          MonoBehaviourSingleton<AppMain>.I.UpdatePurchaseItemListRequestTime();
          GameSaveData.instance.iAPBundleBought = string.Empty;
        }
        this.Dirty();
      }
      if (call_back == null)
        return;
      call_back(flag);
    }));
  }

  private void AddTestPack()
  {
    this.purchaseItemList.shopList.Add(new Network.ProductData()
    {
      productId = "net.gogame.dragon.sku_conversion3days",
      oldPrice = 0.0,
      price = 1.99,
      crystalNum = 100,
      priceIncludeTax = 1.99,
      productType = 2,
      offerType = 1
    });
    this.purchaseItemList.shopList.Add(new Network.ProductData()
    {
      productId = "net.gogame.dragon.sku_conversion7days",
      oldPrice = 0.0,
      price = 1.99,
      crystalNum = 100,
      priceIncludeTax = 1.99,
      productType = 2,
      offerType = 1
    });
    this.purchaseItemList.shopList.Add(new Network.ProductData()
    {
      productId = "net.gogame.dragon.sku_loyalty_fish",
      oldPrice = 0.0,
      price = 1.99,
      crystalNum = 100,
      priceIncludeTax = 1.99,
      productType = 2,
      offerType = 1
    });
  }

  public void SendGoldCanPurchase(
    string product_id,
    string safety_lock_password,
    Action<Error> call_back)
  {
    Protocol.Send<GoldCanPurchaseModel.RequestSendForm, GoldCanPurchaseModel>(GoldCanPurchaseModel.URL, new GoldCanPurchaseModel.RequestSendForm()
    {
      productId = product_id,
      safetyLockPassword = safety_lock_password
    }, (Action<GoldCanPurchaseModel>) (ret => call_back(ret.Error)));
  }

  public void SendDarkMarketCanPurchase(
    string product_id,
    int darkMarketId,
    string safety_lock_password,
    Action<Error> call_back)
  {
    Protocol.Send<GoldCanPurchaseModel.RequestSendForm, GoldCanPurchaseModel>(GoldCanPurchaseModel.URL, new GoldCanPurchaseModel.RequestSendForm()
    {
      productId = product_id,
      safetyLockPassword = safety_lock_password,
      marketId = darkMarketId
    }, (Action<GoldCanPurchaseModel>) (ret => call_back(ret.Error)));
  }

  public void SendCheckPromotion()
  {
    if (this.IsCheckingPromotionItem || this.HasCheckPromotionItem)
      return;
    if (this.purchaseItemList == null || this.purchaseItemList.promotionList.Count == 0)
    {
      Log.Error("Promotion List is null!");
    }
    else
    {
      this.HasCheckPromotionItem = true;
      this.IsCheckingPromotionItem = true;
      Native.checkAndGivePromotionItems(string.Join("----", this.purchaseItemList.promotionList.ToArray()));
    }
  }

  public void SendGetDarkMarketItemList(Action<bool> call_back)
  {
    this.darkMarketItemList = (DarkMarketItemList) null;
    Protocol.Send<DarkMarketListModel>(DarkMarketListModel.URL, (Action<DarkMarketListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        if (!string.IsNullOrEmpty(ret.currentTime) && MonoBehaviourSingleton<GoGameTimeManager>.IsValid())
          GoGameTimeManager.SetServerTime(ret.currentTime);
        this.darkMarketItemList = ret.result;
        if (!string.IsNullOrEmpty(ret.result.endDate) && !GameSaveData.instance.resetMarketTime.Equals(ret.result.endDate) && (int) GoGameTimeManager.GetRemainTime(ret.result.endDate).TotalSeconds > 0)
        {
          GameSaveData.instance.canShowNoteDarkMarket = true;
          GameSaveData.instance.resetMarketTime = ret.result.endDate;
        }
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public DarkMarketItem GetDarkMarketItem(int itemId)
  {
    if (this.darkMarketItemList != null)
    {
      int count = this.darkMarketItemList.items.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.darkMarketItemList.items[index].id == itemId)
          return this.darkMarketItemList.items[index];
      }
    }
    return (DarkMarketItem) null;
  }

  public string GetListProductData()
  {
    if (this.darkMarketItemList != null)
    {
      List<string> stringList = new List<string>();
      int count = this.darkMarketItemList.items.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.darkMarketItemList.items[index].saleType == 200)
        {
          if (!stringList.Contains(this.darkMarketItemList.items[index].saleoffProductId))
            stringList.Add(this.darkMarketItemList.items[index].saleoffProductId);
          if (!string.IsNullOrEmpty(this.darkMarketItemList.items[index].refProductId) && !stringList.Contains(this.darkMarketItemList.items[index].refProductId))
            stringList.Add(this.darkMarketItemList.items[index].refProductId);
        }
      }
      if (stringList.Count > 0)
        return string.Join("----", stringList.ToArray());
    }
    return string.Empty;
  }

  public void SendBuyDarkMarket(int darkMarketId, Action<Error> call_back)
  {
    Protocol.Send<DarkMarketBuyModel.RequestSendForm, DarkMarketBuyModel>(DarkMarketBuyModel.URL, new DarkMarketBuyModel.RequestSendForm()
    {
      marketId = darkMarketId,
      crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal
    }, (Action<DarkMarketBuyModel>) (ret => call_back(ret.Error)));
  }

  public void UpdateDarkMarketUsedCount(int darkMarketId, int usedCount)
  {
    if (this.darkMarketItemList == null)
      return;
    int count = this.darkMarketItemList.items.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.darkMarketItemList.items[index].id == darkMarketId)
        this.darkMarketItemList.items[index].usedCount = usedCount;
    }
  }

  public void SendTradingPostInfo(Action<Error> call_back)
  {
  }
}
