package net.gogame.gowrap;

import java.util.List;
import java.util.Map;

public interface GoWrap {

    public enum BannerAdSize {
        BANNER_SIZE_AUTO,
        BANNER_SIZE_320x50,
        BANNER_SIZE_640x100,
        BANNER_SIZE_480x32,
        BANNER_SIZE_960x64,
        BANNER_SIZE_224x336,
        BANNER_SIZE_448x672,
        BANNER_SIZE_336x224,
        BANNER_SIZE_672x448
    }

    public enum InterstitialAdSize {
        INTERSTITIAL_AD_SIZE_FULLSCREEN
    }

    void didCompleteRewardedAd(String str, int i);

    String getGuid();

    boolean handleCustomUri(String str);

    boolean hasBannerAds();

    boolean hasBannerAds(BannerAdSize bannerAdSize);

    boolean hasInterstitialAds();

    boolean hasOffers();

    boolean hasRewardedAds();

    void hideBannerAd();

    void hideFab();

    void onCustomUrl(String str);

    void onMenuClosed();

    void onMenuOpened();

    void onOffersAvailable();

    void setCustomUrlSchemes(List<String> list);

    void setDelegate(GoWrapDelegate goWrapDelegate);

    void setGuid(String str);

    void setVipStatus(VipStatus vipStatus);

    void showBannerAd();

    void showBannerAd(BannerAdSize bannerAdSize);

    void showFab();

    void showInAppNotifications();

    void showInterstitialAd();

    void showOffers();

    void showRewardedAd();

    void showStartMenu();

    void trackEvent(String str, String str2);

    void trackEvent(String str, String str2, long j);

    void trackEvent(String str, String str2, Map<String, Object> map);

    void trackPurchase(String str, String str2, double d);

    void trackPurchase(String str, String str2, double d, String str3, String str4);
}
