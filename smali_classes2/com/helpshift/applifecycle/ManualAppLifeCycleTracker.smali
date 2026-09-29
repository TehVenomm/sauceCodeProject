.class Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;
.super Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;
.source "ManualAppLifeCycleTracker.java"


# static fields
.field private static TAG:Ljava/lang/String; = "MALCTracker"


# instance fields
.field private isAppInForeground:Z


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 16
    invoke-direct {p0, p1}, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;-><init>(Landroid/content/Context;)V

    const/4 p1, 0x0

    .line 13
    iput-boolean p1, p0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->isAppInForeground:Z

    return-void
.end method


# virtual methods
.method public isAppInForeground()Z
    .locals 1

    .line 21
    iget-boolean v0, p0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->isAppInForeground:Z

    return v0
.end method

.method public onManualAppBackgroundAPI()V
    .locals 2

    .line 42
    iget-boolean v0, p0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->isAppInForeground:Z

    if-nez v0, :cond_0

    .line 43
    sget-object v0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->TAG:Ljava/lang/String;

    const-string v1, "Application is already in background, so ignore this event"

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 47
    :cond_0
    sget-object v0, Lcom/helpshift/util/HelpshiftContext;->installCallSuccessful:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v0, 0x0

    .line 48
    iput-boolean v0, p0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->isAppInForeground:Z

    .line 49
    invoke-virtual {p0}, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->notifyAppBackground()V

    goto :goto_0

    .line 52
    :cond_1
    sget-object v0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->TAG:Ljava/lang/String;

    const-string v1, "onManualAppBackgroundAPI is called without calling install API"

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public onManualAppForegroundAPI()V
    .locals 2

    .line 26
    iget-boolean v0, p0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->isAppInForeground:Z

    if-eqz v0, :cond_0

    .line 27
    sget-object v0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->TAG:Ljava/lang/String;

    const-string v1, "Application is already in foreground, so ignore this event"

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 31
    :cond_0
    sget-object v0, Lcom/helpshift/util/HelpshiftContext;->installCallSuccessful:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v0, 0x1

    .line 32
    iput-boolean v0, p0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->isAppInForeground:Z

    .line 33
    invoke-virtual {p0}, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->notifyAppForeground()V

    goto :goto_0

    .line 36
    :cond_1
    sget-object v0, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;->TAG:Ljava/lang/String;

    const-string v1, "onManualAppForegroundAPI is called without calling install API"

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method
