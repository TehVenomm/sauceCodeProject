// Decompiled with JetBrains decompiler
// Type: gogame.GoWrap_UnityEditor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace gogame;

public class GoWrap_UnityEditor : IGoWrap
{
  private List<string> customUrlSchemes;
  private IGoWrapDelegate goWrapDelegate;
  private string guid;
  private VipStatus vipStatus;
  private string objName;

  public void initGoWrap(string objName) => this.objName = objName;

  public void SendMessage(string methodName, object value)
  {
    if (this.objName == null)
      return;
    GameObject gameObject = GameObject.Find(this.objName);
    if (Object.op_Equality((Object) gameObject, (Object) null))
      return;
    gameObject.SendMessage(methodName, value);
  }

  public IGoWrapDelegate getDelegate() => this.goWrapDelegate;

  public void setDelegate(IGoWrapDelegate goWrapDelegate) => this.goWrapDelegate = goWrapDelegate;

  public void showMenu() => UnityEditorGoWrapMenu.ShowGoWrapMenu();

  public void setCustomUrlSchemes(List<string> schemes)
  {
    this.customUrlSchemes = schemes;
    if (this.customUrlSchemes == null)
      Debug.Log((object) "[goWrap] Custom URL schemes cleared");
    else
      Debug.Log((object) ("[goWrap] Custom URL schemes: " + string.Join(", ", this.customUrlSchemes.ToArray())));
  }

  public void setIOSDeviceToken(byte[] deviceToken)
  {
    Debug.LogWarning((object) "[goWrap] setIOSDeviceToken() not supported in the Unity editor");
  }

  public void setGuid(string guid)
  {
    this.guid = guid;
    Debug.Log((object) $"[goWrap] guid={this.guid}");
  }

  public void setVipStatus(VipStatus vipStatus)
  {
    this.vipStatus = vipStatus;
    if (this.vipStatus != null)
      Debug.Log((object) $"[goWrap] vipStatus vip={this.vipStatus.vip}, suspended={this.vipStatus.suspended}, suspensionMessage={this.vipStatus.suspensionMessage}");
    else
      Debug.Log((object) "[goWrap] vipStatus=null");
  }

  public void trackEvent(string name, string category)
  {
    Debug.Log((object) $"[goWrap] trackEvent(name={name}, category={category})");
  }

  public void trackEvent(string name, string category, long value)
  {
    Debug.Log((object) $"[goWrap] trackEvent(name={name}, category={category}, value={value})");
  }

  public void trackEvent(string name, string category, Dictionary<string, object> values)
  {
    string str = (string) null;
    if (values != null)
    {
      JSONObject jsonObject = new JSONObject();
      foreach (KeyValuePair<string, object> keyValuePair in values)
      {
        string key = keyValuePair.Key;
        object val1 = keyValuePair.Value;
        switch (val1)
        {
          case null:
            continue;
          case bool val2:
            jsonObject.AddField(key, val2);
            continue;
          case Decimal _:
            jsonObject.AddField(key, Convert.ToSingle(val1));
            continue;
          case double _:
            jsonObject.AddField(key, Convert.ToSingle(val1));
            continue;
          case float val3:
            jsonObject.AddField(key, val3);
            continue;
          case int val4:
            jsonObject.AddField(key, val4);
            continue;
          case uint _:
            jsonObject.AddField(key, Convert.ToInt64(val1));
            continue;
          case long val5:
            jsonObject.AddField(key, val5);
            continue;
          case ulong _:
            jsonObject.AddField(key, Convert.ToInt64(val1));
            continue;
          case short val6:
            jsonObject.AddField(key, (int) val6);
            continue;
          case ushort _:
            jsonObject.AddField(key, Convert.ToInt32(val1));
            continue;
          case byte _:
            jsonObject.AddField(key, (int) Convert.ToInt16(val1));
            continue;
          case sbyte _:
            jsonObject.AddField(key, (int) Convert.ToInt16(val1));
            continue;
          case string _:
            jsonObject.AddField(key, (string) val1);
            continue;
          default:
            jsonObject.AddField(key, $"[object {val1.GetType().Name}]");
            continue;
        }
      }
      str = jsonObject.Print();
    }
    Debug.Log((object) $"[goWrap] trackEvent(name={name}, category={category}, values={str})");
  }

  public void trackPurchase(string productId, string currencyCode, double price)
  {
    Debug.Log((object) $"[goWrap] trackPurchase(productId={productId}, currencyCode={currencyCode}, price={price})");
  }

  public void trackPurchase(
    string productId,
    string currencyCode,
    double price,
    string purchaseData,
    string signature)
  {
    Debug.Log((object) $"[goWrap] trackPurchase(productId={productId}, currencyCode={currencyCode}, price={price}, purchaseData={purchaseData}, signature={signature})");
  }

  public bool hasOffers()
  {
    Debug.LogWarning((object) "[goWrap] hasOffers() not supported in the Unity editor");
    return false;
  }

  public void showOffers()
  {
    Debug.LogWarning((object) "[goWrap] showOffers() not supported in the Unity editor");
  }

  public bool hasBannerAds()
  {
    Debug.LogWarning((object) "[goWrap] hasBannerAds() not supported in the Unity editor");
    return false;
  }

  public bool hasBannerAds(BannerAdSize size)
  {
    Debug.LogWarning((object) "[goWrap] hasBannerAds() not supported in the Unity editor");
    return false;
  }

  public void showBannerAd()
  {
    Debug.LogWarning((object) "[goWrap] showBannerAd() not supported in the Unity editor");
  }

  public void showBannerAd(BannerAdSize size)
  {
    Debug.LogWarning((object) "[goWrap] showBannerAd() not supported in the Unity editor");
  }

  public void hideBannerAd()
  {
    Debug.LogWarning((object) "[goWrap] hideBannerAd() not supported in the Unity editor");
  }

  public bool hasInterstitialAds()
  {
    Debug.LogWarning((object) "[goWrap] hasInterstitialAds() not supported in the Unity editor");
    return false;
  }

  public void showInterstitialAd()
  {
    Debug.LogWarning((object) "[goWrap] showInterstitialAd() not supported in the Unity editor");
  }

  public bool hasRewardedAds()
  {
    Debug.LogWarning((object) "[goWrap] hasRewardedAds() not supported in the Unity editor");
    return false;
  }

  public void showRewardedAd()
  {
    Debug.LogWarning((object) "[goWrap] showRewardedAd() not supported in the Unity editor");
  }
}
