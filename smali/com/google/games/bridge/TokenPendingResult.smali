.class public Lcom/google/games/bridge/TokenPendingResult;
.super Lcom/google/android/gms/common/api/PendingResult;
.source "TokenPendingResult.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/google/android/gms/common/api/PendingResult<",
        "Lcom/google/games/bridge/TokenResult;",
        ">;"
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String; = "TokenPendingResult"


# instance fields
.field private latch:Ljava/util/concurrent/CountDownLatch;

.field result:Lcom/google/games/bridge/TokenResult;

.field private resultCallback:Lcom/google/android/gms/common/api/ResultCallback;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/google/android/gms/common/api/ResultCallback<",
            "-",
            "Lcom/google/games/bridge/TokenResult;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 44
    invoke-direct {p0}, Lcom/google/android/gms/common/api/PendingResult;-><init>()V

    .line 39
    new-instance v0, Ljava/util/concurrent/CountDownLatch;

    const/4 v1, 0x1

    invoke-direct {v0, v1}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    iput-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->latch:Ljava/util/concurrent/CountDownLatch;

    .line 45
    new-instance v0, Lcom/google/games/bridge/TokenResult;

    invoke-direct {v0}, Lcom/google/games/bridge/TokenResult;-><init>()V

    iput-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    return-void
.end method

.method private declared-synchronized getCallback()Lcom/google/android/gms/common/api/ResultCallback;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/google/android/gms/common/api/ResultCallback<",
            "-",
            "Lcom/google/games/bridge/TokenResult;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 119
    :try_start_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->resultCallback:Lcom/google/android/gms/common/api/ResultCallback;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method private declared-synchronized getResult()Lcom/google/games/bridge/TokenResult;
    .locals 1

    monitor-enter p0

    .line 142
    :try_start_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method private declared-synchronized setCallback(Lcom/google/android/gms/common/api/ResultCallback;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/ResultCallback<",
            "-",
            "Lcom/google/games/bridge/TokenResult;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    .line 115
    :try_start_0
    iput-object p1, p0, Lcom/google/games/bridge/TokenPendingResult;->resultCallback:Lcom/google/android/gms/common/api/ResultCallback;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 116
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 114
    monitor-exit p0

    throw p1
.end method

.method private declared-synchronized setResult(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    .locals 1

    monitor-enter p0

    .line 133
    :try_start_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    if-eqz v0, :cond_0

    if-nez p1, :cond_0

    iget-object p1, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {p1}, Lcom/google/games/bridge/TokenResult;->getAuthCode()Ljava/lang/String;

    move-result-object p1

    .line 135
    :cond_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    if-eqz v0, :cond_1

    if-nez p3, :cond_1

    iget-object p3, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {p3}, Lcom/google/games/bridge/TokenResult;->getIdToken()Ljava/lang/String;

    move-result-object p3

    .line 136
    :cond_1
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    if-eqz v0, :cond_2

    if-nez p2, :cond_2

    iget-object p2, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {p2}, Lcom/google/games/bridge/TokenResult;->getEmail()Ljava/lang/String;

    move-result-object p2

    .line 138
    :cond_2
    new-instance v0, Lcom/google/games/bridge/TokenResult;

    invoke-direct {v0, p1, p2, p3, p4}, Lcom/google/games/bridge/TokenResult;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    iput-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 139
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 132
    monitor-exit p0

    throw p1
.end method


# virtual methods
.method public bridge synthetic await()Lcom/google/android/gms/common/api/Result;
    .locals 1
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 35
    invoke-virtual {p0}, Lcom/google/games/bridge/TokenPendingResult;->await()Lcom/google/games/bridge/TokenResult;

    move-result-object v0

    return-object v0
.end method

.method public bridge synthetic await(JLjava/util/concurrent/TimeUnit;)Lcom/google/android/gms/common/api/Result;
    .locals 0
    .param p3    # Ljava/util/concurrent/TimeUnit;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 35
    invoke-virtual {p0, p1, p2, p3}, Lcom/google/games/bridge/TokenPendingResult;->await(JLjava/util/concurrent/TimeUnit;)Lcom/google/games/bridge/TokenResult;

    move-result-object p1

    return-object p1
.end method

.method public await()Lcom/google/games/bridge/TokenResult;
    .locals 2
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 53
    :try_start_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v0}, Ljava/util/concurrent/CountDownLatch;->await()V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const/16 v0, 0xe

    const/4 v1, 0x0

    .line 55
    invoke-direct {p0, v1, v1, v1, v0}, Lcom/google/games/bridge/TokenPendingResult;->setResult(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    .line 58
    :goto_0
    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getResult()Lcom/google/games/bridge/TokenResult;

    move-result-object v0

    return-object v0
.end method

.method public await(JLjava/util/concurrent/TimeUnit;)Lcom/google/games/bridge/TokenResult;
    .locals 2
    .param p3    # Ljava/util/concurrent/TimeUnit;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    const/4 v0, 0x0

    .line 65
    :try_start_0
    iget-object v1, p0, Lcom/google/games/bridge/TokenPendingResult;->latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v1, p1, p2, p3}, Ljava/util/concurrent/CountDownLatch;->await(JLjava/util/concurrent/TimeUnit;)Z

    move-result p1

    if-nez p1, :cond_0

    const/16 p1, 0xf

    .line 66
    invoke-direct {p0, v0, v0, v0, p1}, Lcom/google/games/bridge/TokenPendingResult;->setResult(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const/16 p1, 0xe

    .line 69
    invoke-direct {p0, v0, v0, v0, p1}, Lcom/google/games/bridge/TokenPendingResult;->setResult(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    .line 71
    :cond_0
    :goto_0
    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getResult()Lcom/google/games/bridge/TokenResult;

    move-result-object p1

    return-object p1
.end method

.method public cancel()V
    .locals 2

    const/4 v0, 0x0

    const/16 v1, 0x10

    .line 76
    invoke-direct {p0, v0, v0, v0, v1}, Lcom/google/games/bridge/TokenPendingResult;->setResult(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    .line 77
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v0}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    return-void
.end method

.method public isCanceled()Z
    .locals 1

    .line 82
    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getResult()Lcom/google/games/bridge/TokenResult;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getResult()Lcom/google/games/bridge/TokenResult;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/games/bridge/TokenResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/common/api/Status;->isCanceled()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public setAuthCode(Ljava/lang/String;)V
    .locals 1

    .line 167
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {v0, p1}, Lcom/google/games/bridge/TokenResult;->setAuthCode(Ljava/lang/String;)V

    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 1

    .line 163
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {v0, p1}, Lcom/google/games/bridge/TokenResult;->setEmail(Ljava/lang/String;)V

    return-void
.end method

.method public setIdToken(Ljava/lang/String;)V
    .locals 1

    .line 171
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {v0, p1}, Lcom/google/games/bridge/TokenResult;->setIdToken(Ljava/lang/String;)V

    return-void
.end method

.method public setResultCallback(Lcom/google/android/gms/common/api/ResultCallback;)V
    .locals 5
    .param p1    # Lcom/google/android/gms/common/api/ResultCallback;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/ResultCallback<",
            "-",
            "Lcom/google/games/bridge/TokenResult;",
            ">;)V"
        }
    .end annotation

    .line 91
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v0}, Ljava/util/concurrent/CountDownLatch;->getCount()J

    move-result-wide v0

    const-wide/16 v2, 0x0

    cmp-long v4, v0, v2

    if-nez v4, :cond_0

    .line 92
    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getResult()Lcom/google/games/bridge/TokenResult;

    move-result-object v0

    invoke-interface {p1, v0}, Lcom/google/android/gms/common/api/ResultCallback;->onResult(Lcom/google/android/gms/common/api/Result;)V

    goto :goto_0

    .line 94
    :cond_0
    invoke-direct {p0, p1}, Lcom/google/games/bridge/TokenPendingResult;->setCallback(Lcom/google/android/gms/common/api/ResultCallback;)V

    :goto_0
    return-void
.end method

.method public setResultCallback(Lcom/google/android/gms/common/api/ResultCallback;JLjava/util/concurrent/TimeUnit;)V
    .locals 2
    .param p1    # Lcom/google/android/gms/common/api/ResultCallback;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p4    # Ljava/util/concurrent/TimeUnit;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/google/android/gms/common/api/ResultCallback<",
            "-",
            "Lcom/google/games/bridge/TokenResult;",
            ">;J",
            "Ljava/util/concurrent/TimeUnit;",
            ")V"
        }
    .end annotation

    const/4 v0, 0x0

    .line 103
    :try_start_0
    iget-object v1, p0, Lcom/google/games/bridge/TokenPendingResult;->latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v1, p2, p3, p4}, Ljava/util/concurrent/CountDownLatch;->await(JLjava/util/concurrent/TimeUnit;)Z

    move-result p2

    if-nez p2, :cond_0

    const/16 p2, 0xf

    .line 104
    invoke-direct {p0, v0, v0, v0, p2}, Lcom/google/games/bridge/TokenPendingResult;->setResult(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const/16 p2, 0xe

    .line 107
    invoke-direct {p0, v0, v0, v0, p2}, Lcom/google/games/bridge/TokenPendingResult;->setResult(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    .line 110
    :cond_0
    :goto_0
    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getResult()Lcom/google/games/bridge/TokenResult;

    move-result-object p2

    invoke-interface {p1, p2}, Lcom/google/android/gms/common/api/ResultCallback;->onResult(Lcom/google/android/gms/common/api/Result;)V

    return-void
.end method

.method public setStatus(I)V
    .locals 4

    .line 151
    iget-object v0, p0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {v0, p1}, Lcom/google/games/bridge/TokenResult;->setStatus(I)V

    .line 152
    iget-object p1, p0, Lcom/google/games/bridge/TokenPendingResult;->latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    .line 153
    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getCallback()Lcom/google/android/gms/common/api/ResultCallback;

    move-result-object p1

    .line 154
    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getResult()Lcom/google/games/bridge/TokenResult;

    move-result-object v0

    if-eqz p1, :cond_0

    const-string v1, "TokenPendingResult"

    .line 156
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, " Calling onResult for callback: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p1, " result: "

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 157
    invoke-direct {p0}, Lcom/google/games/bridge/TokenPendingResult;->getCallback()Lcom/google/android/gms/common/api/ResultCallback;

    move-result-object p1

    invoke-interface {p1, v0}, Lcom/google/android/gms/common/api/ResultCallback;->onResult(Lcom/google/android/gms/common/api/Result;)V

    :cond_0
    return-void
.end method
