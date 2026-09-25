.class public Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;
.super Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;
.source "GoPaySupport.java"

# interfaces
.implements Lnet/gogame/gowrap/integrations/CanSetGuid;
.implements Lnet/gogame/gowrap/integrations/CanTrackPurchaseDetails;
.implements Lnet/gogame/gowrap/integrations/CanTrackSandboxPurchaseDetails;
.implements Lnet/gogame/gowrap/integrations/CanCheckVipStatus;


# static fields
.field public static final CONFIG_APP_ID:Ljava/lang/String; = "appId"

.field public static final CONFIG_GAME_MANAGED_VIP_STATUS:Ljava/lang/String; = "gameManagedVipStatus"

.field public static final CONFIG_SECRET:Ljava/lang/String; = "secret"

.field public static final METADATA_GAME_MANAGED_VIP_STATUS:Ljava/lang/String; = "goWrap.goPay.gameManagedVipStatus"


# instance fields
.field private appId:Ljava/lang/String;

.field private gameManagedVipStatus:Z

.field private final goPayClientListener:Lnet/gogame/gopay/vip/IVipClient$Listener;

.field private integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;


# direct methods
.method public constructor <init>()V
    .locals 1

    const-string v0, "goPay"

    .line 57
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;-><init>(Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 34
    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->gameManagedVipStatus:Z

    .line 36
    new-instance v0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$1;-><init>(Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;)V

    iput-object v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->goPayClientListener:Lnet/gogame/gopay/vip/IVipClient$Listener;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;)Z
    .locals 0

    .line 25
    iget-boolean p0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->gameManagedVipStatus:Z

    return p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;)Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;
    .locals 0

    .line 25
    iget-object p0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    return-object p0
.end method

.method private setExtraData()V
    .locals 5

    .line 124
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getUids()Ljava/util/Map;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 126
    invoke-interface {v0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 127
    sget-object v2, Lnet/gogame/gopay/vip/VipClient;->INSTANCE:Lnet/gogame/gopay/vip/VipClient;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "_uid"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    invoke-virtual {v2, v3, v1}, Lnet/gogame/gopay/vip/VipClient;->setExtraData(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 130
    :cond_0
    sget-object v0, Lnet/gogame/gopay/vip/VipClient;->INSTANCE:Lnet/gogame/gopay/vip/VipClient;

    const-string v1, "gowrap_version"

    const-string v2, "2.6.6"

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gopay/vip/VipClient;->setExtraData(Ljava/lang/String;Ljava/lang/String;)V

    .line 131
    sget-object v0, Lnet/gogame/gopay/vip/VipClient;->INSTANCE:Lnet/gogame/gopay/vip/VipClient;

    const-string v1, "X-goWrap-Version"

    const-string v2, "2.6.6"

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gopay/vip/VipClient;->setExtraHeader(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public checkVipStatus(Ljava/lang/String;Z)V
    .locals 1

    .line 136
    invoke-direct {p0}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->setExtraData()V

    .line 137
    sget-object v0, Lnet/gogame/gopay/vip/VipClient;->INSTANCE:Lnet/gogame/gopay/vip/VipClient;

    invoke-virtual {v0, p1, p2}, Lnet/gogame/gopay/vip/VipClient;->checkVipStatus(Ljava/lang/String;Z)V

    return-void
.end method

.method protected doInit(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
    .locals 5

    .line 83
    iput-object p3, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    const-string p3, "appId"

    .line 85
    invoke-virtual {p2, p3}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    iput-object p3, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->appId:Ljava/lang/String;

    const-string p3, "secret"

    .line 86
    invoke-virtual {p2, p3}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    const/4 v0, 0x0

    .line 90
    :try_start_0
    invoke-virtual {p1}, Landroid/app/Activity;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 91
    invoke-virtual {p1}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    const/16 v3, 0x80

    invoke-virtual {v1, v2, v3}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v1

    .line 92
    iget-object v1, v1, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    const-string v2, "goWrap.goPay.gameManagedVipStatus"

    const/4 v3, -0x1

    .line 93
    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v1

    if-eq v1, v3, :cond_2

    const/4 v2, 0x1

    if-ne v1, v2, :cond_0

    .line 97
    iput-boolean v2, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->gameManagedVipStatus:Z

    goto :goto_0

    :cond_0
    if-nez v1, :cond_1

    .line 100
    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->gameManagedVipStatus:Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :cond_1
    const/4 v2, 0x0

    :goto_0
    if-eqz v2, :cond_3

    :try_start_1
    const-string v1, "goWrap"

    .line 104
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "gameManagedVipStatus="

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v4, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->gameManagedVipStatus:Z

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_1

    :catch_0
    :cond_2
    const/4 v2, 0x0

    :catch_1
    :cond_3
    :goto_1
    if-nez v2, :cond_4

    const-string v1, "gameManagedVipStatus"

    .line 111
    invoke-virtual {p2, v1, v0}, Lnet/gogame/gowrap/integrations/Config;->getBoolean(Ljava/lang/String;Z)Z

    move-result p2

    iput-boolean p2, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->gameManagedVipStatus:Z

    .line 114
    :cond_4
    sget-object p2, Lnet/gogame/gopay/vip/VipClient;->INSTANCE:Lnet/gogame/gopay/vip/VipClient;

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->appId:Ljava/lang/String;

    invoke-virtual {p2, p1, v0, p3}, Lnet/gogame/gopay/vip/VipClient;->init(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 115
    sget-object p1, Lnet/gogame/gopay/vip/VipClient;->INSTANCE:Lnet/gogame/gopay/vip/VipClient;

    iget-object p2, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->goPayClientListener:Lnet/gogame/gopay/vip/IVipClient$Listener;

    invoke-virtual {p1, p2}, Lnet/gogame/gopay/vip/VipClient;->addListener(Lnet/gogame/gopay/vip/IVipClient$Listener;)V

    return-void
.end method

.method public getAppId()Ljava/lang/String;
    .locals 1

    .line 61
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->appId:Ljava/lang/String;

    return-object v0
.end method

.method public getCurrentActivity()Landroid/app/Activity;
    .locals 1

    .line 69
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    return-object v0
.end method

.method public getGuid()Ljava/lang/String;
    .locals 1

    .line 65
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v0}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getGuid()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public isGameManagedVipStatusEnabled()Z
    .locals 1

    .line 73
    iget-boolean v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->gameManagedVipStatus:Z

    return v0
.end method

.method public isIntegrated()Z
    .locals 1

    const-string v0, "net.gogame.gopay.vip.VipClient"

    .line 78
    invoke-static {v0}, Lnet/gogame/gowrap/support/ClassUtils;->hasClass(Ljava/lang/String;)Z

    move-result v0

    return v0
.end method

.method public setGuid(Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    .line 120
    invoke-virtual {p0, p1, v0}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->checkVipStatus(Ljava/lang/String;Z)V

    return-void
.end method

.method public trackPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V
    .locals 3

    .line 142
    invoke-direct {p0}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->setExtraData()V

    .line 143
    new-instance v0, Lnet/gogame/gopay/vip/PurchaseEvent;

    invoke-direct {v0}, Lnet/gogame/gopay/vip/PurchaseEvent;-><init>()V

    .line 144
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getReferenceId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setReferenceId(Ljava/lang/String;)V

    .line 145
    iget-object v1, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v1}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getGuid()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setGuid(Ljava/lang/String;)V

    .line 146
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getProductId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setProductId(Ljava/lang/String;)V

    .line 147
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getCurrencyCode()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setCurrencyCode(Ljava/lang/String;)V

    .line 148
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getPrice()Ljava/lang/Double;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 149
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getPrice()Ljava/lang/Double;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gopay/vip/PurchaseEvent;->setPrice(D)V

    .line 151
    :cond_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getTimestamp()Ljava/util/Date;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 152
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getTimestamp()Ljava/util/Date;

    move-result-object v1

    invoke-virtual {v1}, Ljava/util/Date;->getTime()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gopay/vip/PurchaseEvent;->setTimestamp(J)V

    .line 154
    :cond_1
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getOrderId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setOrderId(Ljava/lang/String;)V

    .line 155
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getVerificationStatus()Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    move-result-object v1

    if-eqz v1, :cond_2

    .line 156
    sget-object v1, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$2;->$SwitchMap$net$gogame$gowrap$integrations$PurchaseDetails$VerificationStatus:[I

    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getVerificationStatus()Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;

    move-result-object v2

    invoke-virtual {v2}, Lnet/gogame/gowrap/integrations/PurchaseDetails$VerificationStatus;->ordinal()I

    move-result v2

    aget v1, v1, v2

    packed-switch v1, :pswitch_data_0

    goto :goto_0

    .line 166
    :pswitch_0
    sget-object v1, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->VERIFICATION_FAILED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setVerificationStatus(Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;)V

    goto :goto_0

    .line 162
    :pswitch_1
    sget-object v1, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->VERIFICATION_SUCCEEDED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setVerificationStatus(Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;)V

    goto :goto_0

    .line 158
    :pswitch_2
    sget-object v1, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setVerificationStatus(Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;)V

    .line 173
    :cond_2
    :goto_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->isSandbox()Z

    move-result p1

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/vip/PurchaseEvent;->setSandbox(Z)V

    .line 174
    sget-object p1, Lnet/gogame/gopay/vip/VipClient;->INSTANCE:Lnet/gogame/gopay/vip/VipClient;

    invoke-virtual {p1, v0}, Lnet/gogame/gopay/vip/VipClient;->trackPurchase(Lnet/gogame/gopay/vip/PurchaseEvent;)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public trackSandboxPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V
    .locals 0

    .line 179
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->trackPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V

    return-void
.end method
