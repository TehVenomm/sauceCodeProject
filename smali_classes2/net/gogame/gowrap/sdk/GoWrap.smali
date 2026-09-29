.class public final Lnet/gogame/gowrap/sdk/GoWrap;
.super Ljava/lang/Object;
.source "GoWrap.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/sdk/GoWrap$BannerAdSize;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "goWrap"

.field private static goWrap:Lnet/gogame/gowrap/GoWrap;

.field private static goWrapExists:Ljava/lang/Boolean;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 18
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static hasBannerAds()Z
    .locals 2

    .line 266
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 267
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->hasBannerAds()Z

    move-result v0

    return v0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "hasBannerAds()=false"

    .line 269
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const/4 v0, 0x0

    return v0
.end method

.method public static hasBannerAds(Lnet/gogame/gowrap/sdk/GoWrap$BannerAdSize;)Z
    .locals 3

    .line 281
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 282
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-static {p0}, Lnet/gogame/gowrap/sdk/GoWrap;->toBannerAdSize(Lnet/gogame/gowrap/sdk/GoWrap$BannerAdSize;)Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    move-result-object p0

    invoke-interface {v0, p0}, Lnet/gogame/gowrap/GoWrap;->hasBannerAds(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z

    move-result p0

    return p0

    :cond_0
    const-string v0, "goWrap"

    .line 284
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "hasBannerAds("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p0, ")=false"

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const/4 p0, 0x0

    return p0
.end method

.method private static hasClass(Ljava/lang/String;)Z
    .locals 0

    .line 424
    :try_start_0
    invoke-static {p0}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;
    :try_end_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    const/4 p0, 0x1

    return p0

    :catch_0
    const/4 p0, 0x0

    return p0
.end method

.method private static hasGoWrap()Z
    .locals 2

    .line 409
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrapExists:Ljava/lang/Boolean;

    if-nez v0, :cond_0

    const-string v0, "net.gogame.gowrap.GoWrap"

    .line 410
    invoke-static {v0}, Lnet/gogame/gowrap/sdk/GoWrap;->hasClass(Ljava/lang/String;)Z

    move-result v0

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    sput-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrapExists:Ljava/lang/Boolean;

    .line 411
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrapExists:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 412
    invoke-static {}, Lnet/gogame/gowrap/GoWrapFactory;->getInstance()Lnet/gogame/gowrap/GoWrap;

    move-result-object v0

    sput-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    .line 415
    :cond_0
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrapExists:Ljava/lang/Boolean;

    if-nez v0, :cond_1

    const-string v0, "goWrap"

    const-string v1, "Cannot detect goWrap"

    .line 416
    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const/4 v0, 0x0

    return v0

    .line 419
    :cond_1
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrapExists:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0
.end method

.method public static hasInterstitialAds()Z
    .locals 2

    .line 328
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 329
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->hasInterstitialAds()Z

    move-result v0

    return v0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "hasInterstitialAds()=false"

    .line 331
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const/4 v0, 0x0

    return v0
.end method

.method public static hasOffers()Z
    .locals 2

    .line 378
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 379
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->hasOffers()Z

    move-result v0

    return v0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "hasOffers()=false"

    .line 381
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const/4 v0, 0x0

    return v0
.end method

.method public static hasRewardedAds()Z
    .locals 2

    .line 353
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 354
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->hasRewardedAds()Z

    move-result v0

    return v0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "hasRewardedAds()=false"

    .line 356
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const/4 v0, 0x0

    return v0
.end method

.method public static hideBannerAd()V
    .locals 2

    .line 315
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 316
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->hideBannerAd()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "hideBannerAd()"

    .line 318
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static hideFab()V
    .locals 2

    .line 47
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 48
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->hideFab()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "hideFab()"

    .line 50
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static setCustomUrlSchemes(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 135
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 136
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0, p0}, Lnet/gogame/gowrap/GoWrap;->setCustomUrlSchemes(Ljava/util/List;)V

    goto :goto_0

    :cond_0
    const-string p0, "goWrap"

    const-string v0, "setCustomUrlSchemes()"

    .line 138
    invoke-static {p0, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static setDelegate(Lnet/gogame/gowrap/sdk/GoWrapDelegate;)V
    .locals 2

    .line 60
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_1

    if-nez p0, :cond_0

    .line 62
    sget-object p0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    const/4 v0, 0x0

    invoke-interface {p0, v0}, Lnet/gogame/gowrap/GoWrap;->setDelegate(Lnet/gogame/gowrap/GoWrapDelegate;)V

    return-void

    .line 65
    :cond_0
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    new-instance v1, Lnet/gogame/gowrap/sdk/GoWrap$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/sdk/GoWrap$1;-><init>(Lnet/gogame/gowrap/sdk/GoWrapDelegate;)V

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/GoWrap;->setDelegate(Lnet/gogame/gowrap/GoWrapDelegate;)V

    goto :goto_0

    :cond_1
    const-string p0, "goWrap"

    const-string v0, "setDelegate()"

    .line 125
    invoke-static {p0, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static setGuid(Ljava/lang/String;)V
    .locals 1

    .line 150
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 151
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0, p0}, Lnet/gogame/gowrap/GoWrap;->setGuid(Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    const-string p0, "goWrap"

    const-string v0, "setGuid()"

    .line 153
    invoke-static {p0, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static setVipStatus(Lnet/gogame/gowrap/sdk/VipStatus;)V
    .locals 2

    .line 165
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v0, 0x0

    if-eqz p0, :cond_0

    .line 168
    new-instance v0, Lnet/gogame/gowrap/VipStatus;

    invoke-direct {v0}, Lnet/gogame/gowrap/VipStatus;-><init>()V

    .line 169
    invoke-virtual {p0}, Lnet/gogame/gowrap/sdk/VipStatus;->isVip()Z

    move-result v1

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/VipStatus;->setVip(Z)V

    .line 170
    invoke-virtual {p0}, Lnet/gogame/gowrap/sdk/VipStatus;->isSuspended()Z

    move-result v1

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/VipStatus;->setSuspended(Z)V

    .line 171
    invoke-virtual {p0}, Lnet/gogame/gowrap/sdk/VipStatus;->getSuspensionMessage()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Lnet/gogame/gowrap/VipStatus;->setSuspensionMessage(Ljava/lang/String;)V

    .line 173
    :cond_0
    sget-object p0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {p0, v0}, Lnet/gogame/gowrap/GoWrap;->setVipStatus(Lnet/gogame/gowrap/VipStatus;)V

    goto :goto_0

    :cond_1
    const-string p0, "goWrap"

    const-string v0, "setVipStatus()"

    .line 175
    invoke-static {p0, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static showBannerAd()V
    .locals 2

    .line 293
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 294
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->showBannerAd()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "showBannerAd()"

    .line 296
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static showBannerAd(Lnet/gogame/gowrap/sdk/GoWrap$BannerAdSize;)V
    .locals 3

    .line 304
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 305
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-static {p0}, Lnet/gogame/gowrap/sdk/GoWrap;->toBannerAdSize(Lnet/gogame/gowrap/sdk/GoWrap$BannerAdSize;)Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    move-result-object p0

    invoke-interface {v0, p0}, Lnet/gogame/gowrap/GoWrap;->showBannerAd(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    .line 307
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "showBannerAd("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p0, ")"

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static showFab()V
    .locals 2

    .line 36
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 37
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->showFab()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "showFab()"

    .line 39
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static showInAppNotifications()V
    .locals 2

    .line 401
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 402
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->showInAppNotifications()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "showInAppNotifications()"

    .line 404
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static showInterstitialAd()V
    .locals 2

    .line 340
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 341
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->showInterstitialAd()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "showInterstitialAd()"

    .line 343
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static showMenu()V
    .locals 2

    .line 25
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 26
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->showStartMenu()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "showMenu()"

    .line 28
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static showOffers()V
    .locals 2

    .line 390
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 391
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->showOffers()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "showOffers()"

    .line 393
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static showRewardedAd()V
    .locals 2

    .line 365
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 366
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrap;->showRewardedAd()V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "showRewardedAd()"

    .line 368
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method private static toBannerAdSize(Lnet/gogame/gowrap/sdk/GoWrap$BannerAdSize;)Lnet/gogame/gowrap/GoWrap$BannerAdSize;
    .locals 1

    .line 432
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap$2;->$SwitchMap$net$gogame$gowrap$sdk$GoWrap$BannerAdSize:[I

    invoke-virtual {p0}, Lnet/gogame/gowrap/sdk/GoWrap$BannerAdSize;->ordinal()I

    move-result p0

    aget p0, v0, p0

    packed-switch p0, :pswitch_data_0

    const/4 p0, 0x0

    return-object p0

    .line 450
    :pswitch_0
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_672x448:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    .line 448
    :pswitch_1
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_336x224:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    .line 446
    :pswitch_2
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_448x672:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    .line 444
    :pswitch_3
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_224x336:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    .line 442
    :pswitch_4
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_960x64:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    .line 440
    :pswitch_5
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_480x32:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    .line 438
    :pswitch_6
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_640x100:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    .line 436
    :pswitch_7
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_320x50:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    .line 434
    :pswitch_8
    sget-object p0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_AUTO:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    return-object p0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_8
        :pswitch_7
        :pswitch_6
        :pswitch_5
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public static trackEvent(Ljava/lang/String;Ljava/lang/String;)V
    .locals 4

    .line 223
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 224
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0, p0, p1}, Lnet/gogame/gowrap/GoWrap;->trackEvent(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "trackEvent(\'%s\', \'%s\')"

    const/4 v2, 0x2

    .line 226
    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p0, v2, v3

    const/4 p0, 0x1

    aput-object p1, v2, p0

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static trackEvent(Ljava/lang/String;Ljava/lang/String;J)V
    .locals 4

    .line 238
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 239
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0, p0, p1, p2, p3}, Lnet/gogame/gowrap/GoWrap;->trackEvent(Ljava/lang/String;Ljava/lang/String;J)V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "trackEvent(\'%s\', \'%s\', %d)"

    const/4 v2, 0x3

    .line 241
    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p0, v2, v3

    const/4 p0, 0x1

    aput-object p1, v2, p0

    const/4 p0, 0x2

    invoke-static {p2, p3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    aput-object p1, v2, p0

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 4
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

    .line 253
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 254
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0, p0, p1, p2}, Lnet/gogame/gowrap/GoWrap;->trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "trackEvent(\'%s\', \'%s\', %s)"

    const/4 v2, 0x3

    .line 256
    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p0, v2, v3

    const/4 p0, 0x1

    aput-object p1, v2, p0

    const/4 p0, 0x2

    aput-object p2, v2, p0

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static trackPurchase(Ljava/lang/String;Ljava/lang/String;D)V
    .locals 4

    .line 187
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 188
    sget-object v0, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    invoke-interface {v0, p0, p1, p2, p3}, Lnet/gogame/gowrap/GoWrap;->trackPurchase(Ljava/lang/String;Ljava/lang/String;D)V

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "trackPurchase(\'%s\', \'%s\', %f)"

    const/4 v2, 0x3

    .line 190
    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p0, v2, v3

    const/4 p0, 0x1

    aput-object p1, v2, p0

    const/4 p0, 0x2

    .line 191
    invoke-static {p2, p3}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p1

    aput-object p1, v2, p0

    .line 190
    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static trackPurchase(Ljava/lang/String;Ljava/lang/String;DLjava/lang/String;Ljava/lang/String;)V
    .locals 8

    .line 206
    invoke-static {}, Lnet/gogame/gowrap/sdk/GoWrap;->hasGoWrap()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 207
    sget-object v1, Lnet/gogame/gowrap/sdk/GoWrap;->goWrap:Lnet/gogame/gowrap/GoWrap;

    move-object v2, p0

    move-object v3, p1

    move-wide v4, p2

    move-object v6, p4

    move-object v7, p5

    invoke-interface/range {v1 .. v7}, Lnet/gogame/gowrap/GoWrap;->trackPurchase(Ljava/lang/String;Ljava/lang/String;DLjava/lang/String;Ljava/lang/String;)V

    goto :goto_1

    :cond_0
    const-string v0, "goWrap"

    const-string v1, "trackPurchase(\'%s\', \'%s\', %f, %s, %s)"

    const/4 v2, 0x5

    .line 209
    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p0, v2, v3

    const/4 p0, 0x1

    aput-object p1, v2, p0

    const/4 p0, 0x2

    .line 210
    invoke-static {p2, p3}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p1

    aput-object p1, v2, p0

    const/4 p0, 0x3

    const/4 p1, 0x0

    if-eqz p4, :cond_1

    const-string p2, "(purchaseData)"

    goto :goto_0

    :cond_1
    move-object p2, p1

    :goto_0
    aput-object p2, v2, p0

    const/4 p0, 0x4

    if-eqz p5, :cond_2

    const-string p1, "(signature)"

    :cond_2
    aput-object p1, v2, p0

    .line 209
    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_1
    return-void
.end method
