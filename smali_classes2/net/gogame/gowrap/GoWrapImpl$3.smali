.class Lnet/gogame/gowrap/GoWrapImpl$3;
.super Lnet/gogame/gowrap/GoWrapImpl$AdManager;
.source "GoWrapImpl.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/GoWrapImpl;-><init>()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lnet/gogame/gowrap/GoWrapImpl$AdManager<",
        "Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;",
        "Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/GoWrapImpl;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/GoWrapImpl;Ljava/lang/String;Ljava/util/List;)V
    .locals 0

    .line 165
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl$3;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    const/4 p1, 0x0

    invoke-direct {p0, p2, p3, p1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;-><init>(Ljava/lang/String;Ljava/util/List;Lnet/gogame/gowrap/GoWrapImpl$1;)V

    return-void
.end method


# virtual methods
.method protected bridge synthetic hasAds(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 0

    .line 165
    check-cast p1, Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;

    check-cast p2, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    invoke-virtual {p0, p1, p2}, Lnet/gogame/gowrap/GoWrapImpl$3;->hasAds(Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;)Z

    move-result p1

    return p1
.end method

.method protected hasAds(Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;)Z
    .locals 0

    .line 169
    invoke-interface {p1, p2}, Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;->hasInterstitialAds(Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;)Z

    move-result p1

    return p1
.end method

.method protected bridge synthetic hideAd(Ljava/lang/Object;)V
    .locals 0

    .line 165
    check-cast p1, Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/GoWrapImpl$3;->hideAd(Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;)V

    return-void
.end method

.method protected hideAd(Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;)V
    .locals 0

    return-void
.end method

.method protected bridge synthetic showAd(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 0

    .line 165
    check-cast p1, Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;

    check-cast p2, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    invoke-virtual {p0, p1, p2}, Lnet/gogame/gowrap/GoWrapImpl$3;->showAd(Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;)Z

    move-result p1

    return p1
.end method

.method protected showAd(Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;)Z
    .locals 0

    .line 174
    invoke-interface {p1, p2}, Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;->showInterstitialAd(Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;)Z

    move-result p1

    return p1
.end method
