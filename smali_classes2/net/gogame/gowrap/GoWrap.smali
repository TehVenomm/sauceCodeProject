.class public interface abstract Lnet/gogame/gowrap/GoWrap;
.super Ljava/lang/Object;
.source "GoWrap.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/GoWrap$BannerAdSize;,
        Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;
    }
.end annotation


# virtual methods
.method public abstract didCompleteRewardedAd(Ljava/lang/String;I)V
.end method

.method public abstract getGuid()Ljava/lang/String;
.end method

.method public abstract handleCustomUri(Ljava/lang/String;)Z
.end method

.method public abstract hasBannerAds()Z
.end method

.method public abstract hasBannerAds(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z
.end method

.method public abstract hasInterstitialAds()Z
.end method

.method public abstract hasOffers()Z
.end method

.method public abstract hasRewardedAds()Z
.end method

.method public abstract hideBannerAd()V
.end method

.method public abstract hideFab()V
.end method

.method public abstract onCustomUrl(Ljava/lang/String;)V
.end method

.method public abstract onMenuClosed()V
.end method

.method public abstract onMenuOpened()V
.end method

.method public abstract onOffersAvailable()V
.end method

.method public abstract setCustomUrlSchemes(Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract setDelegate(Lnet/gogame/gowrap/GoWrapDelegate;)V
.end method

.method public abstract setGuid(Ljava/lang/String;)V
.end method

.method public abstract setVipStatus(Lnet/gogame/gowrap/VipStatus;)V
.end method

.method public abstract showBannerAd()V
.end method

.method public abstract showBannerAd(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)V
.end method

.method public abstract showFab()V
.end method

.method public abstract showInAppNotifications()V
.end method

.method public abstract showInterstitialAd()V
.end method

.method public abstract showOffers()V
.end method

.method public abstract showRewardedAd()V
.end method

.method public abstract showStartMenu()V
.end method

.method public abstract trackEvent(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract trackEvent(Ljava/lang/String;Ljava/lang/String;J)V
.end method

.method public abstract trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract trackPurchase(Ljava/lang/String;Ljava/lang/String;D)V
.end method

.method public abstract trackPurchase(Ljava/lang/String;Ljava/lang/String;DLjava/lang/String;Ljava/lang/String;)V
.end method
