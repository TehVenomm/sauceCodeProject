.class public abstract Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;
.super Ljava/lang/Object;
.source "AbstractIntegrationSupport.java"

# interfaces
.implements Lnet/gogame/gowrap/integrations/IntegrationSupport;


# static fields
.field public static final DEFAULT_BANNER_ZONE_ID:Ljava/lang/String; = "defaultBanner"

.field public static final DEFAULT_EVENT_NAME_DELIMITER:Ljava/lang/String; = "."

.field public static final DEFAULT_INTERSTITIAL_ZONE_ID:Ljava/lang/String; = "defaultInterstitial"

.field public static final DEFAULT_PURCHASE_CATEGORY:Ljava/lang/String; = "purchase"

.field public static final DEFAULT_REWARDED_ZONE_ID:Ljava/lang/String; = "defaultRewarded"

.field public static final DEFAULT_REWARD_ID:Ljava/lang/String; = "DEFAULT"

.field public static final DEFAULT_REWARD_QUANTITY:I = -0x1


# instance fields
.field private final id:Ljava/lang/String;

.field private initialized:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 1

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 19
    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->initialized:Z

    .line 24
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->id:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method protected abstract doInit(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
.end method

.method public getId()Ljava/lang/String;
    .locals 1

    .line 46
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->id:Ljava/lang/String;

    return-object v0
.end method

.method public init(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
    .locals 2

    .line 29
    iget-boolean v0, p0, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->initialized:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x1

    .line 33
    :try_start_0
    invoke-virtual {p0, p1, p2, p3}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->doInit(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 37
    :goto_0
    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->initialized:Z

    goto :goto_1

    :catchall_0
    move-exception p1

    goto :goto_2

    :catch_0
    move-exception p1

    :try_start_1
    const-string p2, "goWrap"

    .line 35
    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Error initializing "

    invoke-virtual {p3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->id:Ljava/lang/String;

    invoke-virtual {p3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-static {p2, p3, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :goto_1
    return-void

    .line 37
    :goto_2
    iput-boolean v0, p0, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;->initialized:Z

    .line 38
    throw p1
.end method

.method public onActivityCreated(Landroid/app/Activity;)V
    .locals 0

    return-void
.end method

.method public onActivityDestroyed(Landroid/app/Activity;)V
    .locals 0

    return-void
.end method

.method public onActivityPaused(Landroid/app/Activity;)V
    .locals 0

    return-void
.end method

.method public onActivityResumed(Landroid/app/Activity;)V
    .locals 0

    return-void
.end method

.method public onActivityStarted(Landroid/app/Activity;)V
    .locals 0

    return-void
.end method

.method public onActivityStopped(Landroid/app/Activity;)V
    .locals 0

    return-void
.end method
