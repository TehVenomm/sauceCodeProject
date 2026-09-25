.class public final Lnet/gogame/gowrap/ui/ActivityHelper;
.super Ljava/lang/Object;
.source "ActivityHelper.java"

# interfaces
.implements Landroid/app/Application$ActivityLifecycleCallbacks;


# annotations
.annotation build Landroid/annotation/TargetApi;
    value = 0xb
.end annotation


# static fields
.field public static final INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;


# instance fields
.field private currentActivity:Landroid/app/Activity;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 16
    new-instance v0, Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/ActivityHelper;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    .line 20
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 17
    iput-object v0, p0, Lnet/gogame/gowrap/ui/ActivityHelper;->currentActivity:Landroid/app/Activity;

    return-void
.end method


# virtual methods
.method public getCurrentActivity()Landroid/app/Activity;
    .locals 1

    .line 24
    iget-object v0, p0, Lnet/gogame/gowrap/ui/ActivityHelper;->currentActivity:Landroid/app/Activity;

    return-object v0
.end method

.method public onActivityCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 3

    .line 33
    sget-object p2, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {p2}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object p2

    if-eqz p2, :cond_0

    .line 34
    sget-object p2, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {p2}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object p2

    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    .line 36
    :try_start_0
    invoke-interface {v0, p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->onActivityCreated(Landroid/app/Activity;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 38
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public onActivityDestroyed(Landroid/app/Activity;)V
    .locals 4

    .line 104
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 105
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    .line 107
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->onActivityDestroyed(Landroid/app/Activity;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 109
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public onActivityPaused(Landroid/app/Activity;)V
    .locals 4

    .line 73
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 74
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    .line 76
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->onActivityPaused(Landroid/app/Activity;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 78
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public onActivityResumed(Landroid/app/Activity;)V
    .locals 4

    .line 59
    iput-object p1, p0, Lnet/gogame/gowrap/ui/ActivityHelper;->currentActivity:Landroid/app/Activity;

    .line 60
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 61
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    .line 63
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->onActivityResumed(Landroid/app/Activity;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 65
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public onActivitySaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    return-void
.end method

.method public onActivityStarted(Landroid/app/Activity;)V
    .locals 4

    .line 46
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 47
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    .line 49
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->onActivityStarted(Landroid/app/Activity;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 51
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public onActivityStopped(Landroid/app/Activity;)V
    .locals 4

    .line 86
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 87
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getIntegrationSupportList()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    .line 89
    :try_start_0
    invoke-interface {v1, p1}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->onActivityStopped(Landroid/app/Activity;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 91
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method public setCurrentActivity(Landroid/app/Activity;)V
    .locals 0

    .line 28
    iput-object p1, p0, Lnet/gogame/gowrap/ui/ActivityHelper;->currentActivity:Landroid/app/Activity;

    return-void
.end method
