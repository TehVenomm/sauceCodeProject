.class public Lcom/helpshift/util/concurrent/HandlerThreadExecutor;
.super Ljava/lang/Object;
.source "HandlerThreadExecutor.java"

# interfaces
.implements Lcom/helpshift/util/concurrent/ApiExecutor;


# instance fields
.field private handler:Landroid/os/Handler;

.field private final syncLock:Ljava/lang/Object;

.field uiHandler:Landroid/os/Handler;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 1

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 9
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->syncLock:Ljava/lang/Object;

    .line 14
    new-instance v0, Landroid/os/HandlerThread;

    invoke-direct {v0, p1}, Landroid/os/HandlerThread;-><init>(Ljava/lang/String;)V

    .line 15
    invoke-virtual {v0}, Landroid/os/HandlerThread;->start()V

    .line 16
    new-instance p1, Landroid/os/Handler;

    invoke-virtual {v0}, Landroid/os/HandlerThread;->getLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-direct {p1, v0}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object p1, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->handler:Landroid/os/Handler;

    .line 17
    new-instance p1, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-direct {p1, v0}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object p1, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->uiHandler:Landroid/os/Handler;

    return-void
.end method


# virtual methods
.method public awaitForSyncExecution()V
    .locals 1

    .line 47
    new-instance v0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor$2;

    invoke-direct {v0, p0}, Lcom/helpshift/util/concurrent/HandlerThreadExecutor$2;-><init>(Lcom/helpshift/util/concurrent/HandlerThreadExecutor;)V

    invoke-virtual {p0, v0}, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->runSync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public runAsync(Ljava/lang/Runnable;)V
    .locals 1

    .line 22
    iget-object v0, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->handler:Landroid/os/Handler;

    invoke-virtual {v0, p1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public runOnUiThread(Ljava/lang/Runnable;)V
    .locals 1

    .line 37
    new-instance v0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor$1;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/util/concurrent/HandlerThreadExecutor$1;-><init>(Lcom/helpshift/util/concurrent/HandlerThreadExecutor;Ljava/lang/Runnable;)V

    invoke-virtual {p0, v0}, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->runAsync(Ljava/lang/Runnable;)V

    return-void
.end method

.method public runSync(Ljava/lang/Runnable;)V
    .locals 2

    .line 28
    new-instance v0, Lcom/helpshift/util/concurrent/NotifyingRunnable;

    invoke-direct {v0, p1}, Lcom/helpshift/util/concurrent/NotifyingRunnable;-><init>(Ljava/lang/Runnable;)V

    .line 29
    iget-object p1, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->syncLock:Ljava/lang/Object;

    monitor-enter p1

    .line 30
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->handler:Landroid/os/Handler;

    invoke-virtual {v1, v0}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    .line 31
    invoke-virtual {v0}, Lcom/helpshift/util/concurrent/NotifyingRunnable;->waitForCompletion()V

    .line 32
    monitor-exit p1

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method
