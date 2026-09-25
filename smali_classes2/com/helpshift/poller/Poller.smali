.class public abstract Lcom/helpshift/poller/Poller;
.super Ljava/lang/Object;
.source "Poller.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<V:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;",
        "Ljava/lang/Runnable;"
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_Poller"


# instance fields
.field private final callable:Ljava/util/concurrent/Callable;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/Callable<",
            "TV;>;"
        }
    .end annotation
.end field

.field private final executorService:Ljava/util/concurrent/ExecutorService;

.field private final scheduledExecutorService:Ljava/util/concurrent/ScheduledExecutorService;

.field private started:Z


# direct methods
.method public constructor <init>(Ljava/util/concurrent/Callable;Ljava/util/concurrent/ExecutorService;Ljava/util/concurrent/ScheduledExecutorService;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/concurrent/Callable<",
            "TV;>;",
            "Ljava/util/concurrent/ExecutorService;",
            "Ljava/util/concurrent/ScheduledExecutorService;",
            ")V"
        }
    .end annotation

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 27
    iput-object p1, p0, Lcom/helpshift/poller/Poller;->callable:Ljava/util/concurrent/Callable;

    .line 28
    iput-object p2, p0, Lcom/helpshift/poller/Poller;->executorService:Ljava/util/concurrent/ExecutorService;

    .line 29
    iput-object p3, p0, Lcom/helpshift/poller/Poller;->scheduledExecutorService:Ljava/util/concurrent/ScheduledExecutorService;

    return-void
.end method


# virtual methods
.method public abstract getFailDelay(Ljava/lang/Exception;)Lcom/helpshift/common/poller/Delay;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end method

.method public abstract getSuccessDelay(Ljava/lang/Object;)Lcom/helpshift/common/poller/Delay;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TV;)",
            "Lcom/helpshift/common/poller/Delay;"
        }
    .end annotation
.end method

.method public run()V
    .locals 3

    .line 57
    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0x0

    invoke-virtual {p0, v1, v2, v0}, Lcom/helpshift/poller/Poller;->runDelayed(JLjava/util/concurrent/TimeUnit;)V

    return-void
.end method

.method runDelayed(JLjava/util/concurrent/TimeUnit;)V
    .locals 2

    .line 68
    :try_start_0
    iget-boolean v0, p0, Lcom/helpshift/poller/Poller;->started:Z

    if-eqz v0, :cond_3

    iget-object v0, p0, Lcom/helpshift/poller/Poller;->scheduledExecutorService:Ljava/util/concurrent/ScheduledExecutorService;

    invoke-interface {v0}, Ljava/util/concurrent/ScheduledExecutorService;->isShutdown()Z

    move-result v0

    if-nez v0, :cond_3

    .line 69
    iget-object v0, p0, Lcom/helpshift/poller/Poller;->scheduledExecutorService:Ljava/util/concurrent/ScheduledExecutorService;

    iget-object v1, p0, Lcom/helpshift/poller/Poller;->callable:Ljava/util/concurrent/Callable;

    invoke-interface {v0, v1, p1, p2, p3}, Ljava/util/concurrent/ScheduledExecutorService;->schedule(Ljava/util/concurrent/Callable;JLjava/util/concurrent/TimeUnit;)Ljava/util/concurrent/ScheduledFuture;

    move-result-object p1
    :try_end_0
    .catch Ljava/util/concurrent/RejectedExecutionException; {:try_start_0 .. :try_end_0} :catch_1

    .line 72
    :try_start_1
    invoke-interface {p1}, Ljava/util/concurrent/Future;->get()Ljava/lang/Object;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/helpshift/poller/Poller;->getSuccessDelay(Ljava/lang/Object;)Lcom/helpshift/common/poller/Delay;

    move-result-object p1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/util/concurrent/RejectedExecutionException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_0

    :catch_0
    move-exception p1

    .line 75
    :try_start_2
    invoke-virtual {p1}, Ljava/lang/Exception;->getCause()Ljava/lang/Throwable;

    move-result-object p2

    instance-of p2, p2, Lcom/helpshift/network/errors/NetworkError;

    if-eqz p2, :cond_0

    .line 76
    invoke-virtual {p1}, Ljava/lang/Exception;->getCause()Ljava/lang/Throwable;

    move-result-object p1

    check-cast p1, Lcom/helpshift/network/errors/NetworkError;

    invoke-virtual {p0, p1}, Lcom/helpshift/poller/Poller;->getFailDelay(Ljava/lang/Exception;)Lcom/helpshift/common/poller/Delay;

    move-result-object p1

    goto :goto_0

    .line 79
    :cond_0
    invoke-virtual {p0, p1}, Lcom/helpshift/poller/Poller;->getFailDelay(Ljava/lang/Exception;)Lcom/helpshift/common/poller/Delay;

    move-result-object p1

    :goto_0
    if-eqz p1, :cond_2

    .line 83
    iget-object p2, p0, Lcom/helpshift/poller/Poller;->executorService:Ljava/util/concurrent/ExecutorService;

    invoke-interface {p2}, Ljava/util/concurrent/ExecutorService;->isShutdown()Z

    move-result p2

    if-eqz p2, :cond_1

    goto :goto_1

    .line 89
    :cond_1
    iget-object p2, p0, Lcom/helpshift/poller/Poller;->executorService:Ljava/util/concurrent/ExecutorService;

    new-instance p3, Lcom/helpshift/poller/Poller$1;

    invoke-direct {p3, p0, p1}, Lcom/helpshift/poller/Poller$1;-><init>(Lcom/helpshift/poller/Poller;Lcom/helpshift/common/poller/Delay;)V

    invoke-interface {p2, p3}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    goto :goto_2

    :cond_2
    :goto_1
    const/4 p1, 0x0

    .line 84
    iput-boolean p1, p0, Lcom/helpshift/poller/Poller;->started:Z
    :try_end_2
    .catch Ljava/util/concurrent/RejectedExecutionException; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_2

    :catch_1
    move-exception p1

    const-string p2, "Helpshift_Poller"

    const-string p3, "Rejected execution of run delayed : "

    .line 101
    invoke-static {p2, p3, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_3
    :goto_2
    return-void
.end method

.method public shutdown()V
    .locals 1

    const/4 v0, 0x0

    .line 33
    iput-boolean v0, p0, Lcom/helpshift/poller/Poller;->started:Z

    .line 34
    iget-object v0, p0, Lcom/helpshift/poller/Poller;->scheduledExecutorService:Ljava/util/concurrent/ScheduledExecutorService;

    invoke-interface {v0}, Ljava/util/concurrent/ScheduledExecutorService;->shutdownNow()Ljava/util/List;

    .line 35
    iget-object v0, p0, Lcom/helpshift/poller/Poller;->executorService:Ljava/util/concurrent/ExecutorService;

    invoke-interface {v0}, Ljava/util/concurrent/ExecutorService;->shutdownNow()Ljava/util/List;

    return-void
.end method

.method public start()V
    .locals 3

    .line 39
    iget-boolean v0, p0, Lcom/helpshift/poller/Poller;->started:Z

    if-nez v0, :cond_0

    const/4 v0, 0x1

    .line 40
    iput-boolean v0, p0, Lcom/helpshift/poller/Poller;->started:Z

    .line 46
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/poller/Poller;->executorService:Ljava/util/concurrent/ExecutorService;

    invoke-interface {v0, p0}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V
    :try_end_0
    .catch Ljava/util/concurrent/RejectedExecutionException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_Poller"

    const-string v2, "Rejected execution : "

    .line 50
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    :goto_0
    return-void
.end method
