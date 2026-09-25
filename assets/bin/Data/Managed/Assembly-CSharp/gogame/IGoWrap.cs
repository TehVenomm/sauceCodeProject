// Decompiled with JetBrains decompiler
// Type: gogame.IGoWrap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace gogame;

public interface IGoWrap
{
  void initGoWrap(string objName);

  IGoWrapDelegate getDelegate();

  void setDelegate(IGoWrapDelegate goWrapDelegate);

  void setGuid(string guid);

  void setVipStatus(VipStatus vipStatus);

  bool hasBannerAds();

  bool hasBannerAds(BannerAdSize size);

  void showBannerAd();

  void showBannerAd(BannerAdSize size);

  void hideBannerAd();

  bool hasOffers();

  void showOffers();

  bool hasInterstitialAds();

  void showInterstitialAd();

  bool hasRewardedAds();

  void showRewardedAd();

  void showMenu();

  void trackEvent(string name, string category);

  void trackEvent(string name, string category, long value);

  void trackEvent(string name, string category, Dictionary<string, object> values);

  void trackPurchase(
    string productId,
    string currencyCode,
    double price,
    string purchaseData,
    string signature);

  void setCustomUrlSchemes(List<string> schemes);

  void setIOSDeviceToken(byte[] deviceToken);
}
