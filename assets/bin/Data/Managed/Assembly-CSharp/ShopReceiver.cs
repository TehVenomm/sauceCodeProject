// Decompiled with JetBrains decompiler
// Type: ShopReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class ShopReceiver : MonoBehaviourSingleton<ShopReceiver>
{
  public int BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE = 3;
  public System.Action onBillingUnavailable;
  public Action<string> onBuyItem;
  public Action<string> onBuyGacha;
  public Action<ShopReceiver.PaymentPurchaseData> onBuySpecialItem;
  public Action<ShopReceiver.PaymentPurchaseData> onBuyMaterialItem;
  public Action<StoreDataList> onGetProductDatas;
  public Action<bool> onPromotionItem;

  public void buyItem(string json)
  {
    if (json == null)
    {
      this.onBuyItem((string) null);
    }
    else
    {
      int result1 = 0;
      int.TryParse(json, out result1);
      if (result1 == this.BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE)
      {
        this.onBillingUnavailable();
      }
      else
      {
        try
        {
          ShopReceiver.PaymentPurchaseData result2 = JsonUtility.FromJson<ShopReceiver.OriginalPurchaseData>(json).result;
          if (result2.productType == 1)
            this.onBuyItem(result2.productId);
          else if (result2.productType == 2)
          {
            this.onBuySpecialItem(result2);
            GameSaveData.instance.iAPBundleBought = $"{GameSaveData.instance.iAPBundleBought}/{result2.productId}";
          }
          else if (result2.productType == 4)
            this.onBuyGacha(result2.productId);
          else
            this.onBuyMaterialItem(result2);
        }
        catch (Exception ex)
        {
          Log.Error(ex.ToString());
          if (this.onBuyItem == null)
            return;
          this.onBuyItem((string) null);
        }
      }
    }
  }

  public void promoteItem(string success) => this.onPromotionItem(Convert.ToBoolean(success));

  public void promoteCheck(string data)
  {
    MonoBehaviourSingleton<ShopManager>.I.HasCheckPromotionItem = false;
    if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene"))
      return;
    MonoBehaviourSingleton<ShopManager>.I.SendCheckPromotion();
  }

  public void paymentsFinished(string itemId)
  {
    if (this.onBuyItem == null)
      return;
    this.onBuyItem((string) null);
  }

  public void getProductDatas(string json)
  {
    try
    {
      StoreDataList storeDataList = JSONSerializer.Deserialize<StoreDataList>(json);
      if (this.onGetProductDatas == null)
        return;
      this.onGetProductDatas(storeDataList);
    }
    catch (Exception ex)
    {
      Log.Error(ex.ToString());
      if (this.onGetProductDatas == null)
        return;
      this.onGetProductDatas((StoreDataList) null);
    }
  }

  public void TrackPurchase(string jsonData)
  {
    try
    {
      ShopReceiver.TrackPurchaseData trackPurchaseData = JsonUtility.FromJson<ShopReceiver.TrackPurchaseData>(jsonData);
      MonoBehaviourSingleton<GoWrapManager>.I.trackPurchase(trackPurchaseData.productId, trackPurchaseData.currency, trackPurchaseData.price, trackPurchaseData.purchaseData, trackPurchaseData.signature);
    }
    catch (Exception ex)
    {
      Log.Error(ex.ToString());
    }
  }

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
    public string productId;
    public string productName;
    public int crystal;
    public int productType;
    public ShopReceiver.PaymentPurchaseData.PaymentItemData[] bundle;

    [Serializable]
    public class PaymentItemData
    {
      public string name;
      public int type;
      public int itemId;
      public int num;
    }
  }

  [Serializable]
  public class OriginalPurchaseData
  {
    public ShopReceiver.PaymentPurchaseData result;
  }
}
