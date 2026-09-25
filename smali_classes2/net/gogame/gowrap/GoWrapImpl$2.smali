.class Lnet/gogame/gowrap/GoWrapImpl$2;
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
        "Lnet/gogame/gowrap/integrations/CanShowBannerAd;",
        "Lnet/gogame/gowrap/GoWrap$BannerAdSize;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/GoWrapImpl;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/GoWrapImpl;Ljava/lang/String;Ljava/util/List;)V
    .locals 0

    .line 147
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl$2;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    const/4 p1, 0x0

    invoke-direct {p0, p2, p3, p1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;-><init>(Ljava/lang/String;Ljava/util/List;Lnet/gogame/gowrap/GoWrapImpl$1;)V

    return-void
.end method


# virtual methods
.method protected bridge synthetic hasAds(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 0

    .line 147
    check-cast p1, Lnet/gogame/gowrap/integrations/CanShowBannerAd;

    check-cast p2, Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    invoke-virtual {p0, p1, p2}, Lnet/gogame/gowrap/GoWrapImpl$2;->hasAds(Lnet/gogame/gowrap/integrations/CanShowBannerAd;Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z

    move-result p1

    return p1
.end method

.method protected hasAds(Lnet/gogame/gowrap/integrations/CanShowBannerAd;Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z
    .locals 0

    .line 151
    invoke-interface {p1, p2}, Lnet/gogame/gowrap/integrations/CanShowBannerAd;->hasBannerAds(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z

    move-result p1

    return p1
.end method

.method protected bridge synthetic hideAd(Ljava/lang/Object;)V
    .locals 0

    .line 147
    check-cast p1, Lnet/gogame/gowrap/integrations/CanShowBannerAd;

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/GoWrapImpl$2;->hideAd(Lnet/gogame/gowrap/integrations/CanShowBannerAd;)V

    return-void
.end method

.method protected hideAd(Lnet/gogame/gowrap/integrations/CanShowBannerAd;)V
    .locals 0

    .line 161
    invoke-interface {p1}, Lnet/gogame/gowrap/integrations/CanShowBannerAd;->hideBannerAd()V

    return-void
.end method

.method protected bridge synthetic showAd(Ljava/lang/Object;Ljava/lang/Object;)Z
    .locals 0

    .line 147
    check-cast p1, Lnet/gogame/gowrap/integrations/CanShowBannerAd;

    check-cast p2, Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    invoke-virtual {p0, p1, p2}, Lnet/gogame/gowrap/GoWrapImpl$2;->showAd(Lnet/gogame/gowrap/integrations/CanShowBannerAd;Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z

    move-result p1

    return p1
.end method

.method protected showAd(Lnet/gogame/gowrap/integrations/CanShowBannerAd;Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z
    .locals 0

    .line 156
    invoke-interface {p1, p2}, Lnet/gogame/gowrap/integrations/CanShowBannerAd;->showBannerAd(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z

    move-result p1

    return p1
.end method
