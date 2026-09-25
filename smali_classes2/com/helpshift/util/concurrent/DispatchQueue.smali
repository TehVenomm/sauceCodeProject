.class public Lcom/helpshift/util/concurrent/DispatchQueue;
.super Ljava/lang/Object;
.source "DispatchQueue.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "HS_DispatchQueue"


# instance fields
.field private afterThreads:Ljava/util/concurrent/LinkedBlockingQueue;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/LinkedBlockingQueue<",
            "Ljava/lang/Thread;",
            ">;"
        }
    .end annotation
.end field

.field private tasks:Ljava/util/concurrent/LinkedBlockingQueue;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/LinkedBlockingQueue<",
            "Ljava/util/concurrent/Future;",
            ">;"
        }
    .end annotation
.end field

.field private threadPoolExecutor:Ljava/util/concurrent/ExecutorService;


# direct methods
.method public constructor <init>(Z)V
    .locals 1

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 27
    new-instance v0, Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-direct {v0}, Ljava/util/concurrent/LinkedBlockingQueue;-><init>()V

    iput-object v0, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->tasks:Ljava/util/concurrent/LinkedBlockingQueue;

    .line 28
    new-instance v0, Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-direct {v0}, Ljava/util/concurrent/LinkedBlockingQueue;-><init>()V

    iput-object v0, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->afterThreads:Ljava/util/concurrent/LinkedBlockingQueue;

    if-eqz p1, :cond_0

    .line 30
    new-instance p1, Lcom/helpshift/common/domain/HSThreadFactory;

    const-string v0, "cmdpq-a"

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/HSThreadFactory;-><init>(Ljava/lang/String;)V

    .line 31
    invoke-static {p1}, Ljava/util/concurrent/Executors;->newCachedThreadPool(Ljava/util/concurrent/ThreadFactory;)Ljava/util/concurrent/ExecutorService;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->threadPoolExecutor:Ljava/util/concurrent/ExecutorService;

    goto :goto_0

    .line 34
    :cond_0
    new-instance p1, Lcom/helpshift/common/domain/HSThreadFactory;

    const-string v0, "cmdpq-b"

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/HSThreadFactory;-><init>(Ljava/lang/String;)V

    .line 35
    invoke-static {p1}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor(Ljava/util/concurrent/ThreadFactory;)Ljava/util/concurrent/ExecutorService;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->threadPoolExecutor:Ljava/util/concurrent/ExecutorService;

    :goto_0
    return-void
.end method

.method private trackTask(Ljava/util/concurrent/Future;)V
    .locals 1

    .line 54
    iget-object v0, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->tasks:Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-virtual {v0, p1}, Ljava/util/concurrent/LinkedBlockingQueue;->add(Ljava/lang/Object;)Z

    return-void
.end method


# virtual methods
.method public dispatchAfter(Ljava/lang/Runnable;J)V
    .locals 8

    .line 64
    new-instance v0, Ljava/lang/Thread;

    new-instance v7, Lcom/helpshift/util/concurrent/DispatchQueue$1;

    move-object v1, v7

    move-object v2, p0

    move-wide v3, p2

    move-object v5, p0

    move-object v6, p1

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/util/concurrent/DispatchQueue$1;-><init>(Lcom/helpshift/util/concurrent/DispatchQueue;JLcom/helpshift/util/concurrent/DispatchQueue;Ljava/lang/Runnable;)V

    const-string p1, "HS-cmdpq-trig"

    invoke-direct {v0, v7, p1}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;Ljava/lang/String;)V

    .line 77
    invoke-virtual {v0}, Ljava/lang/Thread;->start()V

    .line 78
    iget-object p1, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->afterThreads:Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-virtual {p1, v0}, Ljava/util/concurrent/LinkedBlockingQueue;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method public dispatchAsync(Ljava/lang/Runnable;)V
    .locals 1

    .line 58
    iget-object v0, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->threadPoolExecutor:Ljava/util/concurrent/ExecutorService;

    invoke-interface {v0, p1}, Ljava/util/concurrent/ExecutorService;->submit(Ljava/lang/Runnable;)Ljava/util/concurrent/Future;

    move-result-object p1

    .line 59
    invoke-direct {p0, p1}, Lcom/helpshift/util/concurrent/DispatchQueue;->trackTask(Ljava/util/concurrent/Future;)V

    return-void
.end method

.method public dispatchSync(Ljava/lang/Runnable;)V
    .locals 2

    .line 40
    iget-object v0, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->threadPoolExecutor:Ljava/util/concurrent/ExecutorService;

    invoke-interface {v0, p1}, Ljava/util/concurrent/ExecutorService;->submit(Ljava/lang/Runnable;)Ljava/util/concurrent/Future;

    move-result-object p1

    .line 42
    :try_start_0
    invoke-interface {p1}, Ljava/util/concurrent/Future;->get()Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/util/concurrent/ExecutionException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "HS_DispatchQueue"

    const-string v1, "Execution exception : "

    .line 49
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    :catch_1
    move-exception p1

    const-string v0, "HS_DispatchQueue"

    const-string v1, "Runnable interrupted : "

    .line 45
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 46
    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Thread;->interrupt()V

    :goto_0
    return-void
.end method

.method public join()V
    .locals 3

    .line 83
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->afterThreads:Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-virtual {v0}, Ljava/util/concurrent/LinkedBlockingQueue;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/Thread;

    .line 84
    invoke-virtual {v1}, Ljava/lang/Thread;->join()V

    goto :goto_0

    .line 86
    :cond_0
    iget-object v0, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->tasks:Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-virtual {v0}, Ljava/util/concurrent/LinkedBlockingQueue;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/concurrent/Future;

    .line 87
    invoke-interface {v1}, Ljava/util/concurrent/Future;->get()Ljava/lang/Object;

    goto :goto_1

    .line 89
    :cond_1
    iget-object v0, p0, Lcom/helpshift/util/concurrent/DispatchQueue;->tasks:Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-virtual {v0}, Ljava/util/concurrent/LinkedBlockingQueue;->clear()V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/util/concurrent/ExecutionException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-exception v0

    const-string v1, "HS_DispatchQueue"

    const-string v2, "Execution exception : "

    .line 96
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_2

    :catch_1
    move-exception v0

    const-string v1, "HS_DispatchQueue"

    const-string v2, "Runnable interrupted : "

    .line 92
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 93
    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Thread;->interrupt()V

    :goto_2
    return-void
.end method
