package net.gogame.gowrap.integrations;

import net.gogame.gowrap.GoWrap.InterstitialAdSize;

public interface CanShowRewardedAd {
    boolean hasRewardedAds(InterstitialAdSize interstitialAdSize);

    boolean showRewardedAd(InterstitialAdSize interstitialAdSize);
}
