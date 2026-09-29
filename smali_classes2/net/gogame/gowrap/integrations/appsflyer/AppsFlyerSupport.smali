.class public Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;
.super Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;
.source "AppsFlyerSupport.java"

# interfaces
.implements Lnet/gogame/gowrap/integrations/CanGetUid;
.implements Lnet/gogame/gowrap/integrations/CanSetGuid;
.implements Lnet/gogame/gowrap/integrations/CanTrackPurchaseDetails;
.implements Lnet/gogame/gowrap/integrations/CanTrackEvent;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport$PushTokenAsyncTask;
    }
.end annotation


# static fields
.field public static final CONFIG_DEV_KEY:Ljava/lang/String; = "devKey"

.field public static final CONFIG_EVENT_NAME_DELIMITER:Ljava/lang/String; = "eventNameDelimiter"

.field public static final CONFIG_SENDER_ID:Ljava/lang/String; = "senderId"


# instance fields
.field private eventNameDelimiter:Ljava/lang/String;

.field private integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;


# direct methods
.method public constructor <init>()V
    .locals 1

    const-string v0, "appsflyer"

    .line 35
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;-><init>(Ljava/lang/String;)V

    const-string v0, "."

    .line 32
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->eventNameDelimiter:Ljava/lang/String;

    return-void
.end method

.method private toEventName(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    if-nez p1, :cond_0

    return-object p2

    :cond_0
    const-string v0, "%s%s%s"

    const/4 v1, 0x3

    .line 112
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    aput-object p1, v1, v2

    const/4 p1, 0x1

    iget-object v2, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->eventNameDelimiter:Ljava/lang/String;

    aput-object v2, v1, p1

    const/4 p1, 0x2

    aput-object p2, v1, p1

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method


# virtual methods
.method protected doInit(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
    .locals 3

    .line 45
    iput-object p3, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    const-string p3, "devKey"

    .line 47
    invoke-virtual {p2, p3}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    const-string v0, "senderId"

    .line 48
    invoke-virtual {p2, v0}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "eventNameDelimiter"

    const-string v2, "."

    .line 49
    invoke-virtual {p2, v1, v2}, Lnet/gogame/gowrap/integrations/Config;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->eventNameDelimiter:Ljava/lang/String;

    .line 52
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object p2

    const/4 v1, 0x0

    invoke-virtual {p2, v1}, Lcom/appsflyer/AppsFlyerLib;->setCollectIMEI(Z)V

    if-eqz v0, :cond_0

    .line 54
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object p2

    invoke-virtual {p2, v0}, Lcom/appsflyer/AppsFlyerLib;->enableUninstallTracking(Ljava/lang/String;)V

    .line 56
    :cond_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object p2

    invoke-virtual {p1}, Landroid/app/Activity;->getApplication()Landroid/app/Application;

    move-result-object v0

    invoke-virtual {p2, v0, p3}, Lcom/appsflyer/AppsFlyerLib;->startTracking(Landroid/app/Application;Ljava/lang/String;)V

    const-string p2, "DEFAULT"

    const-string p3, "APP_LAUNCH"

    .line 57
    invoke-virtual {p0, p2, p3}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->trackEvent(Ljava/lang/String;Ljava/lang/String;)V

    .line 59
    new-instance p2, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport$PushTokenAsyncTask;

    const/4 p3, 0x0

    invoke-direct {p2, p3}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport$PushTokenAsyncTask;-><init>(Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport$1;)V

    const/4 p3, 0x1

    new-array p3, p3, [Landroid/content/Context;

    aput-object p1, p3, v1

    invoke-virtual {p2, p3}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport$PushTokenAsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method

.method public getUid()Ljava/lang/String;
    .locals 2

    .line 64
    invoke-virtual {p0}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->isIntegrated()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 67
    :cond_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v1}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerLib;->getAppsFlyerUID(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public isIntegrated()Z
    .locals 1

    const-string v0, "com.appsflyer.AppsFlyerLib"

    .line 40
    invoke-static {v0}, Lnet/gogame/gowrap/support/ClassUtils;->hasClass(Ljava/lang/String;)Z

    move-result v0

    return v0
.end method

.method public setGuid(Ljava/lang/String;)V
    .locals 1

    .line 72
    invoke-virtual {p0}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->isIntegrated()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 75
    :cond_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/appsflyer/AppsFlyerLib;->setCustomerUserId(Ljava/lang/String;)V

    if-eqz p1, :cond_1

    const-string p1, "DEFAULT"

    const-string v0, "LOGIN"

    .line 77
    invoke-virtual {p0, p1, v0}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->trackEvent(Ljava/lang/String;Ljava/lang/String;)V

    :cond_1
    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    .line 117
    invoke-virtual {p0}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->isIntegrated()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 120
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 121
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v1

    iget-object v2, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v2}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v2

    .line 122
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->toEventName(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 121
    invoke-virtual {v1, v2, p1, v0}, Lcom/appsflyer/AppsFlyerLib;->trackEvent(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;J)V
    .locals 2

    .line 127
    invoke-virtual {p0}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->isIntegrated()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 130
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "af_param_1"

    .line 131
    invoke-static {p3, p4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p3

    invoke-interface {v0, v1, p3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 132
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object p3

    iget-object p4, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {p4}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getCurrentActivity()Landroid/app/Activity;

    move-result-object p4

    .line 133
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->toEventName(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 132
    invoke-virtual {p3, p4, p1, v0}, Lcom/appsflyer/AppsFlyerLib;->trackEvent(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 2
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

    .line 138
    invoke-virtual {p0}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->isIntegrated()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 141
    :cond_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v1}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v1

    .line 142
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->toEventName(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 141
    invoke-virtual {v0, v1, p1, p3}, Lcom/appsflyer/AppsFlyerLib;->trackEvent(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public trackPurchase(Lnet/gogame/gowrap/integrations/PurchaseDetails;)V
    .locals 3

    .line 83
    invoke-virtual {p0}, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->isIntegrated()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 86
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 87
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getProductId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    const-string v1, "af_content_id"

    .line 88
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getProductId()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 90
    :cond_1
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getCurrencyCode()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_2

    const-string v1, "af_currency"

    .line 91
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getCurrencyCode()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 93
    :cond_2
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getOrderId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_3

    const-string v1, "af_order_id"

    .line 94
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getOrderId()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 96
    :cond_3
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getPrice()Ljava/lang/Double;

    move-result-object v1

    if-eqz v1, :cond_4

    const-string v1, "af_revenue"

    .line 97
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getPrice()Ljava/lang/Double;

    move-result-object v2

    .line 98
    invoke-virtual {v2}, Ljava/lang/Double;->floatValue()F

    move-result v2

    .line 97
    invoke-static {v2}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 100
    :cond_4
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getComment()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_5

    const-string v1, "gowrap_comment"

    .line 101
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getComment()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "af_content_type"

    .line 102
    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/PurchaseDetails;->getComment()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, v1, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 104
    :cond_5
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object p1

    iget-object v1, p0, Lnet/gogame/gowrap/integrations/appsflyer/AppsFlyerSupport;->integrationContext:Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    invoke-interface {v1}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v1

    const-string v2, "af_purchase"

    invoke-virtual {p1, v1, v2, v0}, Lcom/appsflyer/AppsFlyerLib;->trackEvent(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method
