.class Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;
.super Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;
.source "DefaultAppLifeCycleTracker.java"

# interfaces
.implements Landroid/app/Application$ActivityLifecycleCallbacks;


# static fields
.field private static TAG:Ljava/lang/String; = "DALCTracker"


# instance fields
.field private isAppForeground:Z

.field private isConfigurationChanged:Z

.field private started:I

.field private stopped:I


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>(Landroid/app/Application;)V
    .locals 1

    .line 22
    invoke-direct {p0, p1}, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;-><init>(Landroid/content/Context;)V

    const/4 v0, 0x0

    .line 17
    iput-boolean v0, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isConfigurationChanged:Z

    .line 25
    invoke-virtual {p1, p0}, Landroid/app/Application;->unregisterActivityLifecycleCallbacks(Landroid/app/Application$ActivityLifecycleCallbacks;)V

    .line 26
    invoke-virtual {p1, p0}, Landroid/app/Application;->registerActivityLifecycleCallbacks(Landroid/app/Application$ActivityLifecycleCallbacks;)V

    .line 31
    invoke-static {}, Lcom/helpshift/PluginEventBridge;->shouldCallFirstForegroundEvent()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 32
    iget p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->started:I

    const/4 v0, 0x1

    add-int/2addr p1, v0

    iput p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->started:I

    .line 33
    iput-boolean v0, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isAppForeground:Z

    :cond_0
    return-void
.end method


# virtual methods
.method public isAppInForeground()Z
    .locals 2

    .line 39
    iget v0, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->started:I

    iget v1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->stopped:I

    if-le v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public onActivityCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
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

.method public onActivitySaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    return-void
.end method

.method public onActivityStarted(Landroid/app/Activity;)V
    .locals 1

    .line 60
    iget p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->started:I

    const/4 v0, 0x1

    add-int/2addr p1, v0

    iput p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->started:I

    .line 62
    iget-boolean p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isConfigurationChanged:Z

    if-nez p1, :cond_1

    .line 63
    iget-boolean p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isAppForeground:Z

    if-nez p1, :cond_0

    .line 64
    invoke-virtual {p0}, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->notifyAppForeground()V

    .line 66
    :cond_0
    iput-boolean v0, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isAppForeground:Z

    :cond_1
    const/4 p1, 0x0

    .line 68
    iput-boolean p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isConfigurationChanged:Z

    return-void
.end method

.method public onActivityStopped(Landroid/app/Activity;)V
    .locals 2

    .line 84
    iget v0, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->stopped:I

    const/4 v1, 0x1

    add-int/2addr v0, v1

    iput v0, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->stopped:I

    const/4 v0, 0x0

    if-eqz p1, :cond_0

    .line 86
    invoke-virtual {p1}, Landroid/app/Activity;->isChangingConfigurations()Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    iput-boolean v1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isConfigurationChanged:Z

    .line 87
    iget-boolean p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isConfigurationChanged:Z

    if-nez p1, :cond_1

    .line 88
    iget p1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->started:I

    iget v1, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->stopped:I

    if-ne p1, v1, :cond_1

    .line 89
    iput-boolean v0, p0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->isAppForeground:Z

    .line 90
    invoke-virtual {p0}, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->notifyAppBackground()V

    :cond_1
    return-void
.end method

.method public onManualAppBackgroundAPI()V
    .locals 2

    .line 49
    sget-object v0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->TAG:Ljava/lang/String;

    const-string v1, "Install API is called with manualLifeCycleTracking config as false, Ignore this event"

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public onManualAppForegroundAPI()V
    .locals 2

    .line 44
    sget-object v0, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;->TAG:Ljava/lang/String;

    const-string v1, "Install API is called with manualLifeCycleTracking config as false, Ignore this event"

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
