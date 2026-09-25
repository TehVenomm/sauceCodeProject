.class public abstract Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;
.super Ljava/lang/Object;
.source "BaseAppLifeCycleTracker.java"


# instance fields
.field private context:Landroid/content/Context;

.field private lifeCycleListener:Lcom/helpshift/applifecycle/HSAppLifeCycleListener;


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 15
    iput-object p1, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->context:Landroid/content/Context;

    return-void
.end method


# virtual methods
.method public abstract isAppInForeground()Z
.end method

.method notifyAppBackground()V
    .locals 2

    .line 37
    iget-object v0, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->lifeCycleListener:Lcom/helpshift/applifecycle/HSAppLifeCycleListener;

    if-nez v0, :cond_0

    return-void

    .line 41
    :cond_0
    iget-object v0, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->lifeCycleListener:Lcom/helpshift/applifecycle/HSAppLifeCycleListener;

    iget-object v1, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->context:Landroid/content/Context;

    invoke-interface {v0, v1}, Lcom/helpshift/applifecycle/HSAppLifeCycleListener;->onAppBackground(Landroid/content/Context;)V

    return-void
.end method

.method notifyAppForeground()V
    .locals 2

    .line 28
    iget-object v0, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->lifeCycleListener:Lcom/helpshift/applifecycle/HSAppLifeCycleListener;

    if-nez v0, :cond_0

    return-void

    .line 32
    :cond_0
    iget-object v0, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->lifeCycleListener:Lcom/helpshift/applifecycle/HSAppLifeCycleListener;

    iget-object v1, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->context:Landroid/content/Context;

    invoke-interface {v0, v1}, Lcom/helpshift/applifecycle/HSAppLifeCycleListener;->onAppForeground(Landroid/content/Context;)V

    return-void
.end method

.method public abstract onManualAppBackgroundAPI()V
.end method

.method public abstract onManualAppForegroundAPI()V
.end method

.method registerAppLifeCycleListener(Lcom/helpshift/applifecycle/HSAppLifeCycleListener;)V
    .locals 0
    .param p1    # Lcom/helpshift/applifecycle/HSAppLifeCycleListener;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 19
    iput-object p1, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->lifeCycleListener:Lcom/helpshift/applifecycle/HSAppLifeCycleListener;

    return-void
.end method

.method unregisterAppLifeCycleListener()V
    .locals 1

    const/4 v0, 0x0

    .line 23
    iput-object v0, p0, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->lifeCycleListener:Lcom/helpshift/applifecycle/HSAppLifeCycleListener;

    return-void
.end method
