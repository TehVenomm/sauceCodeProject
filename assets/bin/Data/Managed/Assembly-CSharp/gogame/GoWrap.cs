// Decompiled with JetBrains decompiler
// Type: gogame.GoWrap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace gogame;

public class GoWrap : IGoWrap
{
  public static GoWrap INSTANCE = new GoWrap();
  private readonly IGoWrap goWrap;
  private IGoWrapDelegate goWrapDelegate;

  public GoWrap() => this.goWrap = (IGoWrap) new GoWrap_Android();

  public void initGoWrap(string objName)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.initGoWrap(objName);
  }

  public void SendMessage(string methodName, object value)
  {
    if (!(this.goWrap is GoWrap_UnityEditor))
      return;
    ((GoWrap_UnityEditor) this.goWrap).SendMessage(methodName, value);
  }

  public IGoWrapDelegate getDelegate() => this.goWrapDelegate;

  public void setDelegate(IGoWrapDelegate goWrapDelegate)
  {
    this.goWrapDelegate = goWrapDelegate;
    if (this.goWrap == null)
      return;
    this.goWrap.setDelegate(goWrapDelegate);
  }

  public void setGuid(string guid)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.setGuid(guid);
  }

  public void setVipStatus(VipStatus vipStatus)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.setVipStatus(vipStatus);
  }

  public void showMenu()
  {
    if (this.goWrap == null)
      return;
    this.goWrap.showMenu();
  }

  public bool hasBannerAds() => this.goWrap != null && this.goWrap.hasBannerAds();

  public bool hasBannerAds(BannerAdSize size)
  {
    return this.goWrap != null && this.goWrap.hasBannerAds(size);
  }

  public void showBannerAd()
  {
    if (this.goWrap == null)
      return;
    this.goWrap.showBannerAd();
  }

  public void showBannerAd(BannerAdSize size)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.showBannerAd(size);
  }

  public void hideBannerAd()
  {
    if (this.goWrap == null)
      return;
    this.goWrap.hideBannerAd();
  }

  public bool hasOffers() => this.goWrap != null && this.goWrap.hasOffers();

  public void showOffers()
  {
    if (this.goWrap == null)
      return;
    this.goWrap.showOffers();
  }

  public bool hasInterstitialAds() => this.goWrap != null && this.goWrap.hasInterstitialAds();

  public void showInterstitialAd()
  {
    if (this.goWrap == null)
      return;
    this.goWrap.showInterstitialAd();
  }

  public bool hasRewardedAds() => this.goWrap != null && this.goWrap.hasRewardedAds();

  public void showRewardedAd()
  {
    if (this.goWrap == null)
      return;
    this.goWrap.showRewardedAd();
  }

  public void trackEvent(string name, string category)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.trackEvent(name, category);
  }

  public void trackEvent(string name, string category, long value)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.trackEvent(name, category, value);
  }

  public void trackEvent(string name, string category, Dictionary<string, object> values)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.trackEvent(name, category, values);
  }

  public void trackPurchase(
    string productId,
    string currency,
    double price,
    string purchaseData,
    string signature)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.trackPurchase(productId, currency, price, purchaseData, signature);
  }

  public void setCustomUrlSchemes(List<string> schemes)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.setCustomUrlSchemes(schemes);
  }

  public void setIOSDeviceToken(byte[] deviceToken)
  {
    if (this.goWrap == null)
      return;
    this.goWrap.setIOSDeviceToken(deviceToken);
  }
}
