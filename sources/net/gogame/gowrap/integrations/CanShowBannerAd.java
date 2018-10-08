package net.gogame.gowrap.integrations;

import net.gogame.gowrap.GoWrap.BannerAdSize;

public interface CanShowBannerAd {
    boolean hasBannerAds(BannerAdSize bannerAdSize);

    void hideBannerAd();

    boolean showBannerAd(BannerAdSize bannerAdSize);
}
