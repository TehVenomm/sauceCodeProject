package net.gogame.gowrap.sdk;

import android.util.Log;
import java.util.List;
import java.util.Map;
import net.gogame.gowrap.GoWrapDelegateV2;
import net.gogame.gowrap.GoWrapFactory;
import net.gogame.gowrap.VipStatus;

public final class GoWrap {
    private static final String TAG = "goWrap";
    private static net.gogame.gowrap.GoWrap goWrap = null;
    private static Boolean goWrapExists = null;

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

    private GoWrap() {
    }

    public static boolean hasBannerAds() {
        if (hasGoWrap()) {
            return goWrap.hasBannerAds();
        }
        Log.d("goWrap", "hasBannerAds()=false");
        return false;
    }

    public static boolean hasBannerAds(BannerAdSize bannerAdSize) {
        if (hasGoWrap()) {
            return goWrap.hasBannerAds(toBannerAdSize(bannerAdSize));
        }
        Log.d("goWrap", "hasBannerAds(" + bannerAdSize + ")=false");
        return false;
    }

    private static boolean hasClass(String str) {
        try {
            Class.forName(str);
            return true;
        } catch (ClassNotFoundException e) {
            return false;
        }
    }

    private static boolean hasGoWrap() {
        if (goWrapExists == null) {
            goWrapExists = Boolean.valueOf(hasClass("net.gogame.gowrap.GoWrap"));
            if (goWrapExists.booleanValue()) {
                goWrap = GoWrapFactory.getInstance();
            }
        }
        if (goWrapExists != null) {
            return goWrapExists.booleanValue();
        }
        Log.w("goWrap", "Cannot detect goWrap");
        return false;
    }

    public static boolean hasInterstitialAds() {
        if (hasGoWrap()) {
            return goWrap.hasInterstitialAds();
        }
        Log.d("goWrap", "hasInterstitialAds()=false");
        return false;
    }

    public static boolean hasOffers() {
        if (hasGoWrap()) {
            return goWrap.hasOffers();
        }
        Log.d("goWrap", "hasOffers()=false");
        return false;
    }

    public static boolean hasRewardedAds() {
        if (hasGoWrap()) {
            return goWrap.hasRewardedAds();
        }
        Log.d("goWrap", "hasRewardedAds()=false");
        return false;
    }

    public static void hideBannerAd() {
        if (hasGoWrap()) {
            goWrap.hideBannerAd();
        } else {
            Log.d("goWrap", "hideBannerAd()");
        }
    }

    public static void hideFab() {
        if (hasGoWrap()) {
            goWrap.hideFab();
        } else {
            Log.d("goWrap", "hideFab()");
        }
    }

    public static void setCustomUrlSchemes(List<String> list) {
        if (hasGoWrap()) {
            goWrap.setCustomUrlSchemes(list);
        } else {
            Log.d("goWrap", "setCustomUrlSchemes()");
        }
    }

    public static void setDelegate(final GoWrapDelegate goWrapDelegate) {
        if (!hasGoWrap()) {
            Log.d("goWrap", "setDelegate()");
        } else if (goWrapDelegate == null) {
            goWrap.setDelegate(null);
        } else {
            goWrap.setDelegate(new GoWrapDelegateV2() {
                public void didCompleteRewardedAd(String str, int i) {
                    try {
                        goWrapDelegate.didCompleteRewardedAd(str, i);
                    } catch (Exception e) {
                        Log.e("goWrap", "Exception", e);
                    }
                }

                public void onCustomUrl(String str) {
                    try {
                        if (goWrapDelegate instanceof GoWrapDelegateV2) {
                            ((GoWrapDelegateV2) goWrapDelegate).onCustomUrl(str);
                        }
                    } catch (Exception e) {
                        Log.e("goWrap", "Exception", e);
                    }
                }

                public void onMenuClosed() {
                    try {
                        if (goWrapDelegate instanceof GoWrapDelegateV2) {
                            ((GoWrapDelegateV2) goWrapDelegate).onMenuClosed();
                        }
                    } catch (Exception e) {
                        Log.e("goWrap", "Exception", e);
                    }
                }

                public void onMenuOpened() {
                    try {
                        if (goWrapDelegate instanceof GoWrapDelegateV2) {
                            ((GoWrapDelegateV2) goWrapDelegate).onMenuOpened();
                        }
                    } catch (Exception e) {
                        Log.e("goWrap", "Exception", e);
                    }
                }

                public void onOffersAvailable() {
                    try {
                        if (goWrapDelegate instanceof GoWrapDelegateV2) {
                            ((GoWrapDelegateV2) goWrapDelegate).onOffersAvailable();
                        }
                    } catch (Exception e) {
                        Log.e("goWrap", "Exception", e);
                    }
                }
            });
        }
    }

    public static void setGuid(String str) {
        if (hasGoWrap()) {
            goWrap.setGuid(str);
        } else {
            Log.d("goWrap", "setGuid()");
        }
    }

    public static void setVipStatus(VipStatus vipStatus) {
        if (hasGoWrap()) {
            VipStatus vipStatus2 = null;
            if (vipStatus != null) {
                vipStatus2 = new VipStatus();
                vipStatus2.setVip(vipStatus.isVip());
                vipStatus2.setSuspended(vipStatus.isSuspended());
                vipStatus2.setSuspensionMessage(vipStatus.getSuspensionMessage());
            }
            goWrap.setVipStatus(vipStatus2);
            return;
        }
        Log.d("goWrap", "setVipStatus()");
    }

    public static void showBannerAd() {
        if (hasGoWrap()) {
            goWrap.showBannerAd();
        } else {
            Log.d("goWrap", "showBannerAd()");
        }
    }

    public static void showBannerAd(BannerAdSize bannerAdSize) {
        if (hasGoWrap()) {
            goWrap.showBannerAd(toBannerAdSize(bannerAdSize));
        } else {
            Log.d("goWrap", "showBannerAd(" + bannerAdSize + ")");
        }
    }

    public static void showFab() {
        if (hasGoWrap()) {
            goWrap.showFab();
        } else {
            Log.d("goWrap", "showFab()");
        }
    }

    public static void showInterstitialAd() {
        if (hasGoWrap()) {
            goWrap.showInterstitialAd();
        } else {
            Log.d("goWrap", "showInterstitialAd()");
        }
    }

    public static void showMenu() {
        if (hasGoWrap()) {
            goWrap.showStartMenu();
        } else {
            Log.d("goWrap", "showMenu()");
        }
    }

    public static void showOffers() {
        if (hasGoWrap()) {
            goWrap.showOffers();
        } else {
            Log.d("goWrap", "showOffers()");
        }
    }

    public static void showRewardedAd() {
        if (hasGoWrap()) {
            goWrap.showRewardedAd();
        } else {
            Log.d("goWrap", "showRewardedAd()");
        }
    }

    private static net.gogame.gowrap.GoWrap.BannerAdSize toBannerAdSize(BannerAdSize bannerAdSize) {
        switch (bannerAdSize) {
            case BANNER_SIZE_AUTO:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_AUTO;
            case BANNER_SIZE_320x50:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_320x50;
            case BANNER_SIZE_640x100:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_640x100;
            case BANNER_SIZE_480x32:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_480x32;
            case BANNER_SIZE_960x64:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_960x64;
            case BANNER_SIZE_224x336:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_224x336;
            case BANNER_SIZE_448x672:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_448x672;
            case BANNER_SIZE_336x224:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_336x224;
            case BANNER_SIZE_672x448:
                return net.gogame.gowrap.GoWrap.BannerAdSize.BANNER_SIZE_672x448;
            default:
                return null;
        }
    }

    public static void trackEvent(String str, String str2) {
        if (hasGoWrap()) {
            goWrap.trackEvent(str, str2);
            return;
        }
        Log.d("goWrap", String.format("trackEvent('%s', '%s')", new Object[]{str, str2}));
    }

    public static void trackEvent(String str, String str2, long j) {
        if (hasGoWrap()) {
            goWrap.trackEvent(str, str2, j);
            return;
        }
        Log.d("goWrap", String.format("trackEvent('%s', '%s', %d)", new Object[]{str, str2, Long.valueOf(j)}));
    }

    public static void trackEvent(String str, String str2, Map<String, Object> map) {
        if (hasGoWrap()) {
            goWrap.trackEvent(str, str2, map);
            return;
        }
        Log.d("goWrap", String.format("trackEvent('%s', '%s', %s)", new Object[]{str, str2, map}));
    }

    public static void trackPurchase(String str, String str2, double d) {
        if (hasGoWrap()) {
            goWrap.trackPurchase(str, str2, d);
            return;
        }
        Log.d("goWrap", String.format("trackPurchase('%s', '%s', %f)", new Object[]{str, str2, Double.valueOf(d)}));
    }

    public static void trackPurchase(String str, String str2, double d, String str3, String str4) {
        String str5 = null;
        if (hasGoWrap()) {
            goWrap.trackPurchase(str, str2, d, str3, str4);
            return;
        }
        String str6 = str3 != null ? "(purchaseData)" : null;
        if (str4 != null) {
            str5 = "(signature)";
        }
        Log.d("goWrap", String.format("trackPurchase('%s', '%s', %f, %s, %s)", new Object[]{str, str2, Double.valueOf(d), str6, str5}));
    }
}
