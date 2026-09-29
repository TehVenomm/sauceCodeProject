.class public Lnet/gogame/gowrap/GoWrapImpl;
.super Ljava/lang/Object;
.source "GoWrapImpl.java"

# interfaces
.implements Lnet/gogame/gowrap/GoWrap;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/GoWrapImpl$AdManager;,
        Lnet/gogame/gowrap/GoWrapImpl$Listener;
    }
.end annotation


# static fields
.field public static final INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;


# instance fields
.field private final bannerAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lnet/gogame/gowrap/GoWrapImpl$AdManager<",
            "Lnet/gogame/gowrap/integrations/CanShowBannerAd;",
            "Lnet/gogame/gowrap/GoWrap$BannerAdSize;",
            ">;"
        }
    .end annotation
.end field

.field private canChat:Lnet/gogame/gowrap/integrations/CanChat;

.field private canCheckVipStatus:Lnet/gogame/gowrap/integrations/CanCheckVipStatus;

.field private final canGetUidList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanGetUid;",
            ">;"
        }
    .end annotation
.end field

.field private final canSetGuidList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanSetGuid;",
            ">;"
        }
    .end annotation
.end field

.field private final canShowBannerAdList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanShowBannerAd;",
            ">;"
        }
    .end annotation
.end field

.field private final canShowInAppNotificationsList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanShowInAppNotifications;",
            ">;"
        }
    .end annotation
.end field

.field private final canShowInterstitialAdList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;",
            ">;"
        }
    .end annotation
.end field

.field private canShowOffers:Lnet/gogame/gowrap/integrations/CanShowOffers;

.field private final canShowRewardedAdList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanShowRewardedAd;",
            ">;"
        }
    .end annotation
.end field

.field private final canTrackEventList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanTrackEvent;",
            ">;"
        }
    .end annotation
.end field

.field private final canTrackPurchaseDetailsList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanTrackPurchaseDetails;",
            ">;"
        }
    .end annotation
.end field

.field private final canTrackPurchaseList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanTrackPurchase;",
            ">;"
        }
    .end annotation
.end field

.field private final canTrackSandboxPurchaseDetailsList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/CanTrackSandboxPurchaseDetails;",
            ">;"
        }
    .end annotation
.end field

.field private customUrlSchemes:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private delegate:Lnet/gogame/gowrap/GoWrapDelegate;

.field private guid:Ljava/lang/String;

.field private final integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

.field private final integrationSupportList:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/IntegrationSupport;",
            ">;"
        }
    .end annotation
.end field

.field private final interstitialAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lnet/gogame/gowrap/GoWrapImpl$AdManager<",
            "Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;",
            "Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;",
            ">;"
        }
    .end annotation
.end field

.field private final listeners:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lnet/gogame/gowrap/GoWrapImpl$Listener;",
            ">;"
        }
    .end annotation
.end field

.field private mainActivity:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "+",
            "Landroid/app/Activity;",
            ">;"
        }
    .end annotation
.end field

.field private final rewardedAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lnet/gogame/gowrap/GoWrapImpl$AdManager<",
            "Lnet/gogame/gowrap/integrations/CanShowRewardedAd;",
            "Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;",
            ">;"
        }
    .end annotation
.end field

.field private vipStatus:Lnet/gogame/gowrap/VipStatus;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 45
    new-instance v0, Lnet/gogame/gowrap/GoWrapImpl;

    invoke-direct {v0}, Lnet/gogame/gowrap/GoWrapImpl;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    return-void
.end method

.method private constructor <init>()V
    .locals 3

    .line 144
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 46
    const-class v0, Lnet/gogame/gowrap/ui/MainActivity;

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->mainActivity:Ljava/lang/Class;

    .line 47
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->integrationSupportList:Ljava/util/List;

    .line 48
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canGetUidList:Ljava/util/List;

    .line 49
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canSetGuidList:Ljava/util/List;

    .line 50
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackPurchaseList:Ljava/util/List;

    .line 51
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackPurchaseDetailsList:Ljava/util/List;

    .line 52
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackSandboxPurchaseDetailsList:Ljava/util/List;

    .line 53
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackEventList:Ljava/util/List;

    .line 54
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowBannerAdList:Ljava/util/List;

    .line 55
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowInterstitialAdList:Ljava/util/List;

    .line 56
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowRewardedAdList:Ljava/util/List;

    .line 57
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowInAppNotificationsList:Ljava/util/List;

    .line 61
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->listeners:Ljava/util/Set;

    const/4 v0, 0x0

    .line 62
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canChat:Lnet/gogame/gowrap/integrations/CanChat;

    .line 63
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowOffers:Lnet/gogame/gowrap/integrations/CanShowOffers;

    .line 64
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canCheckVipStatus:Lnet/gogame/gowrap/integrations/CanCheckVipStatus;

    .line 65
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->guid:Ljava/lang/String;

    .line 66
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    .line 67
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->customUrlSchemes:Ljava/util/List;

    .line 68
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->vipStatus:Lnet/gogame/gowrap/VipStatus;

    .line 70
    new-instance v0, Lnet/gogame/gowrap/GoWrapImpl$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/GoWrapImpl$1;-><init>(Lnet/gogame/gowrap/GoWrapImpl;)V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    .line 146
    new-instance v0, Lnet/gogame/gowrap/GoWrapImpl$2;

    const-string v1, "banner"

    iget-object v2, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowBannerAdList:Ljava/util/List;

    invoke-direct {v0, p0, v1, v2}, Lnet/gogame/gowrap/GoWrapImpl$2;-><init>(Lnet/gogame/gowrap/GoWrapImpl;Ljava/lang/String;Ljava/util/List;)V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->bannerAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    .line 164
    new-instance v0, Lnet/gogame/gowrap/GoWrapImpl$3;

    const-string v1, "interstitial"

    iget-object v2, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowInterstitialAdList:Ljava/util/List;

    invoke-direct {v0, p0, v1, v2}, Lnet/gogame/gowrap/GoWrapImpl$3;-><init>(Lnet/gogame/gowrap/GoWrapImpl;Ljava/lang/String;Ljava/util/List;)V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->interstitialAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    .line 182
    new-instance v0, Lnet/gogame/gowrap/GoWrapImpl$4;

    const-string v1, "rewarded"

    iget-object v2, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowRewardedAdList:Ljava/util/List;

    invoke-direct {v0, p0, v1, v2}, Lnet/gogame/gowrap/GoWrapImpl$4;-><init>(Lnet/gogame/gowrap/GoWrapImpl;Ljava/lang/String;Ljava/util/List;)V

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->rewardedAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/GoWrapImpl;)Ljava/lang/String;
    .locals 0

    .line 43
    iget-object p0, p0, Lnet/gogame/gowrap/GoWrapImpl;->guid:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/GoWrapImpl;)Ljava/util/List;
    .locals 0

    .line 43
    iget-object p0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canGetUidList:Ljava/util/List;

    return-object p0
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/GoWrapImpl;)Lnet/gogame/gowrap/VipStatus;
    .locals 0

    .line 43
    iget-object p0, p0, Lnet/gogame/gowrap/GoWrapImpl;->vipStatus:Lnet/gogame/gowrap/VipStatus;

    return-object p0
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/GoWrapImpl;)V
    .locals 0

    .line 43
    invoke-direct {p0}, Lnet/gogame/gowrap/GoWrapImpl;->fireOnOffersAvailableEvent()V

    return-void
.end method

.method static synthetic access$500(Ljava/lang/Object;)Ljava/lang/String;
    .locals 0

    .line 43
    invoke-static {p0}, Lnet/gogame/gowrap/GoWrapImpl;->getId(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private fireOnOffersAvailableEvent()V
    .locals 4

    .line 365
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->listeners:Ljava/util/Set;

    if-eqz v0, :cond_1

    .line 366
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->listeners:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/GoWrapImpl$Listener;

    if-eqz v1, :cond_0

    .line 369
    :try_start_0
    invoke-interface {v1}, Lnet/gogame/gowrap/GoWrapImpl$Listener;->onOffersAvailable()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 371
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_1
    return-void
.end method

.method private fireOnVipStatusUpdatedEvent(Lnet/gogame/gowrap/VipStatus;)V
    .locals 4

    .line 351
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->listeners:Ljava/util/Set;

    if-eqz v0, :cond_1

    .line 352
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->listeners:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/GoWrapImpl$Listener;

    if-eqz v1, :cond_0

    .line 355
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/GoWrapImpl$Listener;->onVipStatusUpdated(Lnet/gogame/gowrap/VipStatus;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 357
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_1
    return-void
.end method

.method private generateReferenceId()Ljava/lang/String;
    .locals 3

    .line 438
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 439
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v1

    invoke-virtual {v1}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "_"

    .line 440
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 441
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    .line 442
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method private static getId(Ljava/lang/Object;)Ljava/lang/String;
    .locals 1

    if-nez p0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 214
    :cond_0
    instance-of v0, p0, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    if-eqz v0, :cond_1

    .line 215
    check-cast p0, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    .line 216
    invoke-interface {p0}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->getId()Ljava/lang/String;

    move-result-object p0

    return-object p0

    .line 218
    :cond_1
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private trackPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V
    .locals 4

    .line 548
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackPurchaseDetailsList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackPurchaseDetails;

    .line 550
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/integrations/CanTrackPurchaseDetails;->trackPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 552
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method private trackSandboxPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V
    .locals 4

    .line 558
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackSandboxPurchaseDetailsList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackSandboxPurchaseDetails;

    .line 560
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/integrations/CanTrackSandboxPurchaseDetails;->trackSandboxPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 562
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method


# virtual methods
.method public addListener(Lnet/gogame/gowrap/GoWrapImpl$Listener;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 340
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->listeners:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    :cond_0
    return-void
.end method

.method public checkVipStatus(Z)V
    .locals 2

    .line 421
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canCheckVipStatus:Lnet/gogame/gowrap/integrations/CanCheckVipStatus;

    if-nez v0, :cond_0

    return-void

    .line 424
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canCheckVipStatus:Lnet/gogame/gowrap/integrations/CanCheckVipStatus;

    iget-object v1, p0, Lnet/gogame/gowrap/GoWrapImpl;->guid:Ljava/lang/String;

    invoke-interface {v0, v1, p1}, Lnet/gogame/gowrap/integrations/CanCheckVipStatus;->checkVipStatus(Ljava/lang/String;Z)V

    return-void
.end method

.method public didCompleteRewardedAd(Ljava/lang/String;I)V
    .locals 1

    .line 730
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    if-eqz v0, :cond_0

    .line 732
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/GoWrapDelegate;->didCompleteRewardedAd(Ljava/lang/String;I)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "goWrap"

    const-string v0, "Exception"

    .line 734
    invoke-static {p2, v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public getCustomUrlSchemes()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 307
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->customUrlSchemes:Ljava/util/List;

    return-object v0
.end method

.method public getGuid()Ljava/lang/String;
    .locals 1

    .line 401
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->guid:Ljava/lang/String;

    return-object v0
.end method

.method public getIntegrationSupportList()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/integrations/IntegrationSupport;",
            ">;"
        }
    .end annotation

    .line 298
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->integrationSupportList:Ljava/util/List;

    return-object v0
.end method

.method public getMainActivity()Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "+",
            "Landroid/app/Activity;",
            ">;"
        }
    .end annotation

    .line 203
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->mainActivity:Ljava/lang/Class;

    return-object v0
.end method

.method public getVipStatus()Lnet/gogame/gowrap/VipStatus;
    .locals 1

    .line 428
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->vipStatus:Lnet/gogame/gowrap/VipStatus;

    return-object v0
.end method

.method public handleCustomUri(Landroid/net/Uri;)Z
    .locals 2

    if-eqz p1, :cond_2

    .line 324
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->customUrlSchemes:Ljava/util/List;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->customUrlSchemes:Ljava/util/List;

    .line 325
    invoke-virtual {p1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 328
    :cond_0
    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->onCustomUrl(Ljava/lang/String;)V

    .line 329
    sget-object p1, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 330
    sget-object p1, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    const-string v0, "net.gogame.gowrap."

    .line 331
    invoke-virtual {p1, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 332
    sget-object p1, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object p1

    invoke-virtual {p1}, Landroid/app/Activity;->finish()V

    :cond_1
    const/4 p1, 0x1

    return p1

    :cond_2
    :goto_0
    const/4 p1, 0x0

    return p1
.end method

.method public handleCustomUri(Ljava/lang/String;)Z
    .locals 1

    if-eqz p1, :cond_1

    .line 317
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->customUrlSchemes:Ljava/util/List;

    if-nez v0, :cond_0

    goto :goto_0

    .line 320
    :cond_0
    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->handleCustomUri(Landroid/net/Uri;)Z

    move-result p1

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method

.method public hasBannerAds()Z
    .locals 1

    .line 602
    sget-object v0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_AUTO:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/GoWrapImpl;->hasBannerAds(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z

    move-result v0

    return v0
.end method

.method public hasBannerAds(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z
    .locals 2

    .line 608
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->bannerAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->hasAds(Ljava/lang/Object;)Z

    move-result p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 610
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    const/4 p1, 0x0

    return p1
.end method

.method public hasChat()Z
    .locals 1

    .line 288
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canChat:Lnet/gogame/gowrap/integrations/CanChat;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public hasInterstitialAds()Z
    .locals 3

    .line 643
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->interstitialAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    sget-object v1, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->INTERSTITIAL_AD_SIZE_FULLSCREEN:Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->hasAds(Ljava/lang/Object;)Z

    move-result v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 646
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    const/4 v0, 0x0

    return v0
.end method

.method public hasOffers()Z
    .locals 1

    .line 770
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowOffers:Lnet/gogame/gowrap/integrations/CanShowOffers;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowOffers:Lnet/gogame/gowrap/integrations/CanShowOffers;

    invoke-interface {v0}, Lnet/gogame/gowrap/integrations/CanShowOffers;->hasOffers()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public hasRewardedAds()Z
    .locals 3

    .line 665
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->rewardedAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    sget-object v1, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->INTERSTITIAL_AD_SIZE_FULLSCREEN:Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->hasAds(Ljava/lang/Object;)Z

    move-result v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 667
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    const/4 v0, 0x0

    return v0
.end method

.method public hideBannerAd()V
    .locals 3

    .line 634
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->bannerAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->hideAd()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 636
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public hideFab()V
    .locals 1

    .line 394
    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 395
    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/FabManager;->hideFab(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method

.method public onCustomUrl(Ljava/lang/String;)V
    .locals 4

    const-string v0, "goWrap"

    const-string v1, "onCustomUrl(%s)"

    const/4 v2, 0x1

    .line 715
    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 716
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    if-eqz v0, :cond_0

    .line 718
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    instance-of v0, v0, Lnet/gogame/gowrap/GoWrapDelegateV2;

    if-eqz v0, :cond_0

    .line 719
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    check-cast v0, Lnet/gogame/gowrap/GoWrapDelegateV2;

    .line 720
    invoke-interface {v0, p1}, Lnet/gogame/gowrap/GoWrapDelegateV2;->onCustomUrl(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 723
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public onMenuClosed()V
    .locals 3

    const-string v0, "goWrap"

    const-string v1, "onMenuClosed()"

    .line 700
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 701
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    if-eqz v0, :cond_0

    .line 703
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    instance-of v0, v0, Lnet/gogame/gowrap/GoWrapDelegateV2;

    if-eqz v0, :cond_0

    .line 704
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    check-cast v0, Lnet/gogame/gowrap/GoWrapDelegateV2;

    .line 705
    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrapDelegateV2;->onMenuClosed()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 708
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public onMenuOpened()V
    .locals 3

    const-string v0, "goWrap"

    const-string v1, "onMenuOpened()"

    .line 685
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 686
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    if-eqz v0, :cond_0

    .line 688
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    instance-of v0, v0, Lnet/gogame/gowrap/GoWrapDelegateV2;

    if-eqz v0, :cond_0

    .line 689
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    check-cast v0, Lnet/gogame/gowrap/GoWrapDelegateV2;

    .line 690
    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrapDelegateV2;->onMenuOpened()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 693
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public onOffersAvailable()V
    .locals 3

    const-string v0, "goWrap"

    const-string v1, "onOffersAvailable()"

    .line 741
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 742
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    if-eqz v0, :cond_0

    .line 744
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    instance-of v0, v0, Lnet/gogame/gowrap/GoWrapDelegateV2;

    if-eqz v0, :cond_0

    .line 745
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    check-cast v0, Lnet/gogame/gowrap/GoWrapDelegateV2;

    .line 746
    invoke-interface {v0}, Lnet/gogame/gowrap/GoWrapDelegateV2;->onOffersAvailable()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 749
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public register(Lnet/gogame/gowrap/integrations/IntegrationSupport;Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;)V
    .locals 3

    .line 222
    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {v0, p2}, Lnet/gogame/gowrap/ui/ActivityHelper;->setCurrentActivity(Landroid/app/Activity;)V

    .line 223
    invoke-interface {p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->isIntegrated()Z

    move-result v0

    if-eqz v0, :cond_10

    .line 224
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->integrationSupportList:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 225
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanChat;

    if-eqz v0, :cond_1

    .line 226
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canChat:Lnet/gogame/gowrap/integrations/CanChat;

    if-nez v0, :cond_0

    .line 227
    move-object v0, p1

    check-cast v0, Lnet/gogame/gowrap/integrations/CanChat;

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canChat:Lnet/gogame/gowrap/integrations/CanChat;

    goto :goto_0

    :cond_0
    const-string v0, "goWrap"

    .line 229
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Chat service already registered, skipping "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 230
    invoke-interface {p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->getId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 229
    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 233
    :cond_1
    :goto_0
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanShowOffers;

    if-eqz v0, :cond_3

    .line 234
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowOffers:Lnet/gogame/gowrap/integrations/CanShowOffers;

    if-nez v0, :cond_2

    .line 235
    move-object v0, p1

    check-cast v0, Lnet/gogame/gowrap/integrations/CanShowOffers;

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowOffers:Lnet/gogame/gowrap/integrations/CanShowOffers;

    goto :goto_1

    :cond_2
    const-string v0, "goWrap"

    .line 237
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Offers service already registered, skipping "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 238
    invoke-interface {p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->getId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 237
    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 241
    :cond_3
    :goto_1
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanCheckVipStatus;

    if-eqz v0, :cond_5

    .line 242
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canCheckVipStatus:Lnet/gogame/gowrap/integrations/CanCheckVipStatus;

    if-nez v0, :cond_4

    .line 243
    move-object v0, p1

    check-cast v0, Lnet/gogame/gowrap/integrations/CanCheckVipStatus;

    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canCheckVipStatus:Lnet/gogame/gowrap/integrations/CanCheckVipStatus;

    goto :goto_2

    :cond_4
    const-string v0, "goWrap"

    .line 245
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "VIP status check service already registered, skipping "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 246
    invoke-interface {p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->getId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 245
    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 249
    :cond_5
    :goto_2
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanGetUid;

    if-eqz v0, :cond_6

    .line 250
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canGetUidList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanGetUid;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 252
    :cond_6
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanSetGuid;

    if-eqz v0, :cond_7

    .line 253
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canSetGuidList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanSetGuid;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 255
    :cond_7
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanTrackPurchase;

    if-eqz v0, :cond_8

    .line 256
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackPurchaseList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackPurchase;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 258
    :cond_8
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanTrackPurchaseDetails;

    if-eqz v0, :cond_9

    .line 259
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackPurchaseDetailsList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackPurchaseDetails;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 261
    :cond_9
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanTrackSandboxPurchaseDetails;

    if-eqz v0, :cond_a

    .line 262
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackSandboxPurchaseDetailsList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackSandboxPurchaseDetails;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 264
    :cond_a
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanTrackEvent;

    if-eqz v0, :cond_b

    .line 265
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackEventList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackEvent;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 267
    :cond_b
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanShowBannerAd;

    if-eqz v0, :cond_c

    .line 268
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowBannerAdList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanShowBannerAd;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 270
    :cond_c
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;

    if-eqz v0, :cond_d

    .line 271
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowInterstitialAdList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanShowInterstitialAd;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 273
    :cond_d
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanShowRewardedAd;

    if-eqz v0, :cond_e

    .line 274
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowRewardedAdList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanShowRewardedAd;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 276
    :cond_e
    instance-of v0, p1, Lnet/gogame/gowrap/integrations/CanShowInAppNotifications;

    if-eqz v0, :cond_f

    .line 277
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowInAppNotificationsList:Ljava/util/List;

    move-object v1, p1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanShowInAppNotifications;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 280
    :cond_f
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {p1, p2, p3, v0}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->init(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_3

    :catch_0
    move-exception p1

    const-string p2, "goWrap"

    const-string p3, "Exception"

    .line 282
    invoke-static {p2, p3, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_10
    :goto_3
    return-void
.end method

.method public removeListener(Lnet/gogame/gowrap/GoWrapImpl$Listener;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 346
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->listeners:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z

    :cond_0
    return-void
.end method

.method public setCustomUrlSchemes(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 312
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl;->customUrlSchemes:Ljava/util/List;

    return-void
.end method

.method public setDelegate(Lnet/gogame/gowrap/GoWrapDelegate;)V
    .locals 0

    .line 303
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl;->delegate:Lnet/gogame/gowrap/GoWrapDelegate;

    return-void
.end method

.method public setGuid(Ljava/lang/String;)V
    .locals 4

    .line 406
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->guid:Ljava/lang/String;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    .line 407
    iput-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->vipStatus:Lnet/gogame/gowrap/VipStatus;

    .line 408
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/GoWrapImpl;->fireOnVipStatusUpdatedEvent(Lnet/gogame/gowrap/VipStatus;)V

    .line 410
    :cond_0
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl;->guid:Ljava/lang/String;

    .line 411
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canSetGuidList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanSetGuid;

    .line 413
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/integrations/CanSetGuid;->setGuid(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 415
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_1
    return-void
.end method

.method public setMainActivity(Ljava/lang/Class;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Landroid/app/Activity;",
            ">;)V"
        }
    .end annotation

    .line 207
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl;->mainActivity:Ljava/lang/Class;

    return-void
.end method

.method public setVipStatus(Lnet/gogame/gowrap/VipStatus;)V
    .locals 0

    .line 433
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl;->vipStatus:Lnet/gogame/gowrap/VipStatus;

    .line 434
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->fireOnVipStatusUpdatedEvent(Lnet/gogame/gowrap/VipStatus;)V

    return-void
.end method

.method public showBannerAd()V
    .locals 1

    .line 617
    sget-object v0, Lnet/gogame/gowrap/GoWrap$BannerAdSize;->BANNER_SIZE_AUTO:Lnet/gogame/gowrap/GoWrap$BannerAdSize;

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/GoWrapImpl;->showBannerAd(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)V

    return-void
.end method

.method public showBannerAd(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)V
    .locals 2

    .line 623
    :try_start_0
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->hasBannerAds(Lnet/gogame/gowrap/GoWrap$BannerAdSize;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 624
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->bannerAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->showAd(Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 627
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public showFab()V
    .locals 1

    .line 387
    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 388
    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/FabManager;->showFab(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method

.method public showInAppNotifications()V
    .locals 4

    const-string v0, "goWrap"

    const-string v1, "showInAppNotifications()"

    .line 756
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 757
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowInAppNotificationsList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanShowInAppNotifications;

    .line 760
    :try_start_0
    invoke-interface {v1}, Lnet/gogame/gowrap/integrations/CanShowInAppNotifications;->showInAppNotifications()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 763
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    :goto_1
    return-void
.end method

.method public showInterstitialAd()V
    .locals 3

    .line 654
    :try_start_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/GoWrapImpl;->hasInterstitialAds()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 655
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->interstitialAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    sget-object v1, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->INTERSTITIAL_AD_SIZE_FULLSCREEN:Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->showAd(Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 658
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public showOffers()V
    .locals 1

    .line 775
    invoke-virtual {p0}, Lnet/gogame/gowrap/GoWrapImpl;->hasOffers()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 778
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canShowOffers:Lnet/gogame/gowrap/integrations/CanShowOffers;

    invoke-interface {v0}, Lnet/gogame/gowrap/integrations/CanShowOffers;->showOffers()V

    return-void
.end method

.method public showRewardedAd()V
    .locals 3

    .line 675
    :try_start_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/GoWrapImpl;->hasRewardedAds()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 676
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->rewardedAdManager:Lnet/gogame/gowrap/GoWrapImpl$AdManager;

    sget-object v1, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->INTERSTITIAL_AD_SIZE_FULLSCREEN:Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/GoWrapImpl$AdManager;->showAd(Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 679
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public showStartMenu()V
    .locals 1

    .line 380
    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 381
    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/FabManager;->showMenu(Landroid/app/Activity;)V

    :cond_0
    return-void
.end method

.method public startChat()V
    .locals 1

    .line 292
    invoke-virtual {p0}, Lnet/gogame/gowrap/GoWrapImpl;->hasChat()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 293
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canChat:Lnet/gogame/gowrap/integrations/CanChat;

    invoke-interface {v0}, Lnet/gogame/gowrap/integrations/CanChat;->startChat()V

    :cond_0
    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;)V
    .locals 4

    .line 569
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackEventList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackEvent;

    .line 571
    :try_start_0
    invoke-interface {v1, p1, p2}, Lnet/gogame/gowrap/integrations/CanTrackEvent;->trackEvent(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 573
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;J)V
    .locals 4

    .line 580
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackEventList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackEvent;

    .line 582
    :try_start_0
    invoke-interface {v1, p1, p2, p3, p4}, Lnet/gogame/gowrap/integrations/CanTrackEvent;->trackEvent(Ljava/lang/String;Ljava/lang/String;J)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 584
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
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

    .line 591
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackEventList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackEvent;

    .line 593
    :try_start_0
    invoke-interface {v1, p1, p2, p3}, Lnet/gogame/gowrap/integrations/CanTrackEvent;->trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 595
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public trackPurchase(Ljava/lang/String;Ljava/lang/String;D)V
    .locals 4

    .line 457
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl;->canTrackPurchaseList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/CanTrackPurchase;

    .line 459
    :try_start_0
    invoke-interface {v1, p1, p2, p3, p4}, Lnet/gogame/gowrap/integrations/CanTrackPurchase;->trackPurchase(Ljava/lang/String;Ljava/lang/String;D)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 461
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    .line 465
    :cond_0
    new-instance v0, Lnet/gogame/gowrap/integrations/PurchaseDetails;

    invoke-direct {v0}, Lnet/gogame/gowrap/integrations/PurchaseDetails;-><init>()V

    .line 466
    invoke-direct {p0}, Lnet/gogame/gowrap/GoWrapImpl;->generateReferenceId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setReferenceId(Ljava/lang/String;)V

    .line 467
    new-instance v1, Ljava/util/Date;

    invoke-direct {v1}, Ljava/util/Date;-><init>()V

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setTimestamp(Ljava/util/Date;)V

    .line 468
    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setProductId(Ljava/lang/String;)V

    .line 469
    invoke-virtual {v0, p2}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setCurrencyCode(Ljava/lang/String;)V

    .line 470
    invoke-static {p3, p4}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object p1

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setPrice(Ljava/lang/Double;)V

    const/4 p1, 0x0

    .line 471
    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setOrderId(Ljava/lang/String;)V

    .line 472
    sget-object p1, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setVerificationStatus(Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;)V

    const/4 p1, 0x0

    .line 473
    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setSandbox(Z)V

    const-string p1, "Legacy/deprecated"

    .line 474
    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setComment(Ljava/lang/String;)V

    .line 475
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/GoWrapImpl;->trackPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V

    return-void
.end method

.method public trackPurchase(Ljava/lang/String;Ljava/lang/String;DLjava/lang/String;Ljava/lang/String;)V
    .locals 16

    move-object/from16 v1, p0

    move-object/from16 v9, p1

    move-object/from16 v10, p2

    move-object/from16 v11, p5

    move-object/from16 v12, p6

    .line 482
    new-instance v13, Lnet/gogame/gowrap/integrations/PurchaseDetails;

    invoke-direct {v13}, Lnet/gogame/gowrap/integrations/PurchaseDetails;-><init>()V

    .line 483
    invoke-direct/range {p0 .. p0}, Lnet/gogame/gowrap/GoWrapImpl;->generateReferenceId()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v13, v0}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setReferenceId(Ljava/lang/String;)V

    .line 484
    invoke-virtual {v13, v9}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setProductId(Ljava/lang/String;)V

    .line 485
    invoke-virtual {v13, v10}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setCurrencyCode(Ljava/lang/String;)V

    .line 486
    invoke-static/range {p3 .. p4}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v0

    invoke-virtual {v13, v0}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setPrice(Ljava/lang/Double;)V

    .line 487
    invoke-virtual {v13, v11}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setPurchaseData(Ljava/lang/String;)V

    .line 488
    invoke-virtual {v13, v12}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setSignature(Ljava/lang/String;)V

    .line 490
    new-instance v0, Ljava/util/Date;

    invoke-direct {v0}, Ljava/util/Date;-><init>()V

    const/4 v2, 0x0

    const/4 v3, 0x1

    const/4 v4, 0x0

    if-eqz v11, :cond_4

    if-eqz v12, :cond_4

    .line 495
    :try_start_0
    new-instance v5, Lorg/json/JSONObject;

    invoke-direct {v5, v11}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 496
    new-instance v6, Lnet/gogame/gowrap/support/InAppPurchaseData;

    invoke-direct {v6, v5}, Lnet/gogame/gowrap/support/InAppPurchaseData;-><init>(Lorg/json/JSONObject;)V

    .line 497
    invoke-virtual {v6}, Lnet/gogame/gowrap/support/InAppPurchaseData;->getOrderId()Ljava/lang/String;

    move-result-object v7
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    .line 498
    :try_start_1
    invoke-virtual {v6}, Lnet/gogame/gowrap/support/InAppPurchaseData;->getPurchaseTime()Ljava/lang/Long;

    move-result-object v4

    if-eqz v4, :cond_0

    .line 499
    new-instance v4, Ljava/util/Date;

    invoke-virtual {v6}, Lnet/gogame/gowrap/support/InAppPurchaseData;->getPurchaseTime()Ljava/lang/Long;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/Long;->longValue()J

    move-result-wide v14

    invoke-direct {v4, v14, v15}, Ljava/util/Date;-><init>(J)V

    move-object v0, v4

    :cond_0
    const-string v4, "goPay"

    .line 503
    invoke-virtual {v5, v4}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    if-eqz v4, :cond_2

    const-string v5, "sandbox"

    .line 505
    invoke-virtual {v4, v5}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_1

    const-string v5, "sandbox"

    .line 506
    invoke-virtual {v4, v5, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v4

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v4, 0x1

    goto :goto_1

    :cond_2
    if-eqz v7, :cond_1

    .line 509
    invoke-virtual {v7}, Ljava/lang/String;->length()I

    move-result v4

    if-eqz v4, :cond_1

    const-string v4, "goPay-sandbox-"

    .line 510
    invoke-virtual {v7, v4}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v4
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    if-eqz v4, :cond_3

    goto :goto_0

    :cond_3
    const/4 v4, 0x0

    :goto_1
    move v5, v4

    move-object v4, v7

    goto :goto_2

    :catch_0
    move-object v4, v7

    :catch_1
    :cond_4
    const/4 v5, 0x1

    .line 517
    :goto_2
    invoke-virtual {v13, v0}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setTimestamp(Ljava/util/Date;)V

    .line 518
    invoke-virtual {v13, v4}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setOrderId(Ljava/lang/String;)V

    .line 519
    sget-object v0, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->VERIFICATION_SUCCEEDED:Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    invoke-virtual {v13, v0}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setVerificationStatus(Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;)V

    .line 521
    invoke-virtual {v13, v5}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->setSandbox(Z)V

    if-eqz v5, :cond_5

    const-string v0, "goWrap"

    const-string v4, "Sandbox purchase detected (%s, %s, %f, %s, %s)"

    const/4 v5, 0x5

    .line 524
    new-array v5, v5, [Ljava/lang/Object;

    aput-object v9, v5, v2

    aput-object v10, v5, v3

    const/4 v2, 0x2

    .line 525
    invoke-static/range {p3 .. p4}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v3

    aput-object v3, v5, v2

    const/4 v2, 0x3

    aput-object v11, v5, v2

    const/4 v2, 0x4

    aput-object v12, v5, v2

    .line 524
    invoke-static {v4, v5}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v0, v2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 526
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v2, "product_id"

    .line 527
    invoke-interface {v0, v2, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "currency_code"

    .line 528
    invoke-interface {v0, v2, v10}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "price"

    .line 529
    invoke-static/range {p3 .. p4}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "sandbox"

    const-string v3, "purchase"

    .line 530
    invoke-virtual {v1, v2, v3, v0}, Lnet/gogame/gowrap/GoWrapImpl;->trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    .line 532
    invoke-direct {v1, v13}, Lnet/gogame/gowrap/GoWrapImpl;->trackSandboxPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V

    return-void

    .line 536
    :cond_5
    iget-object v0, v1, Lnet/gogame/gowrap/GoWrapImpl;->canTrackPurchaseList:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v14

    :goto_3
    invoke-interface {v14}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_6

    invoke-interface {v14}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    move-object v2, v0

    check-cast v2, Lnet/gogame/gowrap/integrations/CanTrackPurchase;

    move-object/from16 v3, p1

    move-object/from16 v4, p2

    move-wide/from16 v5, p3

    move-object/from16 v7, p5

    move-object/from16 v8, p6

    .line 538
    :try_start_2
    invoke-interface/range {v2 .. v8}, Lnet/gogame/gowrap/integrations/CanTrackPurchase;->trackPurchase(Ljava/lang/String;Ljava/lang/String;DLjava/lang/String;Ljava/lang/String;)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2

    goto :goto_3

    :catch_2
    move-exception v0

    move-object v2, v0

    const-string v0, "goWrap"

    const-string v3, "Exception"

    .line 540
    invoke-static {v0, v3, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_3

    .line 544
    :cond_6
    invoke-direct {v1, v13}, Lnet/gogame/gowrap/GoWrapImpl;->trackPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V

    return-void
.end method
