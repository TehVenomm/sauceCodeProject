package net.gogame.gowrap.integrations;

import net.gogame.gowrap.GoWrap.InterstitialAdSize;

public interface CanShowInterstitialAd {
    boolean hasInterstitialAds(InterstitialAdSize interstitialAdSize);

    boolean showInterstitialAd(InterstitialAdSize interstitialAdSize);
}
