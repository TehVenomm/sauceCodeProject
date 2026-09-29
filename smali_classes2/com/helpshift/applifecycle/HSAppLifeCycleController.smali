.class public Lcom/helpshift/applifecycle/HSAppLifeCycleController;
.super Ljava/lang/Object;
.source "HSAppLifeCycleController.java"

# interfaces
.implements Lcom/helpshift/applifecycle/HSAppLifeCycleListener;


# static fields
.field private static instance:Lcom/helpshift/applifecycle/HSAppLifeCycleController;

.field private static final lock:Ljava/lang/Object;


# instance fields
.field private appLifeCycleListeners:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/applifecycle/HSAppLifeCycleListener;",
            ">;"
        }
    .end annotation
.end field

.field private lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 14
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lock:Ljava/lang/Object;

    .line 16
    new-instance v0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;

    invoke-direct {v0}, Lcom/helpshift/applifecycle/HSAppLifeCycleController;-><init>()V

    sput-object v0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->instance:Lcom/helpshift/applifecycle/HSAppLifeCycleController;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    .line 18
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 19
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->appLifeCycleListeners:Ljava/util/List;

    return-void
.end method

.method static synthetic access$000()Ljava/lang/Object;
    .locals 1

    .line 12
    sget-object v0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lock:Ljava/lang/Object;

    return-object v0
.end method

.method static synthetic access$100(Lcom/helpshift/applifecycle/HSAppLifeCycleController;)Ljava/util/List;
    .locals 0

    .line 12
    iget-object p0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->appLifeCycleListeners:Ljava/util/List;

    return-object p0
.end method

.method public static getInstance()Lcom/helpshift/applifecycle/HSAppLifeCycleController;
    .locals 1

    .line 23
    sget-object v0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->instance:Lcom/helpshift/applifecycle/HSAppLifeCycleController;

    return-object v0
.end method


# virtual methods
.method public declared-synchronized init(Landroid/app/Application;Z)V
    .locals 1

    monitor-enter p0

    .line 31
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v0, :cond_0

    .line 32
    monitor-exit p0

    return-void

    :cond_0
    if-eqz p2, :cond_1

    .line 37
    :try_start_1
    new-instance p2, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;

    invoke-direct {p2, p1}, Lcom/helpshift/applifecycle/ManualAppLifeCycleTracker;-><init>(Landroid/content/Context;)V

    iput-object p2, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    goto :goto_0

    .line 40
    :cond_1
    new-instance p2, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;

    invoke-direct {p2, p1}, Lcom/helpshift/applifecycle/DefaultAppLifeCycleTracker;-><init>(Landroid/app/Application;)V

    iput-object p2, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    .line 43
    :goto_0
    iget-object p1, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    invoke-virtual {p1, p0}, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->registerAppLifeCycleListener(Lcom/helpshift/applifecycle/HSAppLifeCycleListener;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 44
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 30
    monitor-exit p0

    throw p1
.end method

.method public isAppInForeground()Z
    .locals 1

    .line 83
    iget-object v0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    .line 87
    :cond_0
    iget-object v0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    invoke-virtual {v0}, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->isAppInForeground()Z

    move-result v0

    return v0
.end method

.method public onAppBackground(Landroid/content/Context;)V
    .locals 2

    .line 107
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 108
    new-instance v1, Lcom/helpshift/applifecycle/HSAppLifeCycleController$2;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/applifecycle/HSAppLifeCycleController$2;-><init>(Lcom/helpshift/applifecycle/HSAppLifeCycleController;Landroid/content/Context;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public onAppForeground(Landroid/content/Context;)V
    .locals 2

    .line 92
    invoke-static {}, Lcom/helpshift/util/concurrent/ApiExecutorFactory;->getHandlerExecutor()Lcom/helpshift/util/concurrent/ApiExecutor;

    move-result-object v0

    .line 93
    new-instance v1, Lcom/helpshift/applifecycle/HSAppLifeCycleController$1;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/applifecycle/HSAppLifeCycleController$1;-><init>(Lcom/helpshift/applifecycle/HSAppLifeCycleController;Landroid/content/Context;)V

    invoke-interface {v0, v1}, Lcom/helpshift/util/concurrent/ApiExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public onManualAppBackgroundAPI()V
    .locals 1

    .line 71
    iget-object v0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    if-nez v0, :cond_0

    return-void

    .line 75
    :cond_0
    iget-object v0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    invoke-virtual {v0}, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->onManualAppBackgroundAPI()V

    return-void
.end method

.method public onManualAppForegroundAPI()V
    .locals 1

    .line 58
    iget-object v0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    if-nez v0, :cond_0

    return-void

    .line 62
    :cond_0
    iget-object v0, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lifeCycleTracker:Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;

    invoke-virtual {v0}, Lcom/helpshift/applifecycle/BaseAppLifeCycleTracker;->onManualAppForegroundAPI()V

    return-void
.end method

.method public registerAppLifeCycleListener(Lcom/helpshift/applifecycle/HSAppLifeCycleListener;)V
    .locals 2
    .param p1    # Lcom/helpshift/applifecycle/HSAppLifeCycleListener;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 47
    sget-object v0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->lock:Ljava/lang/Object;

    monitor-enter v0

    .line 48
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/applifecycle/HSAppLifeCycleController;->appLifeCycleListeners:Ljava/util/List;

    invoke-interface {v1, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 49
    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method
