.class public abstract Lnet/gogame/gopay/vip/TapeTaskQueue;
.super Lnet/gogame/gopay/vip/AbstractTaskQueue;
.source "SourceFile"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Lnet/gogame/gopay/vip/AbstractTaskQueue<",
        "TT;>;"
    }
.end annotation


# instance fields
.field private final a:Lnet/gogame/gopay/vip/tape2/ObjectQueue;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lnet/gogame/gopay/vip/tape2/ObjectQueue<",
            "TT;>;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Ljava/io/File;Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;Lnet/gogame/gopay/vip/TaskQueue$Listener;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/io/File;",
            "Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter<",
            "TT;>;",
            "Lnet/gogame/gopay/vip/TaskQueue$Listener;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 16
    invoke-direct {p0, p3}, Lnet/gogame/gopay/vip/AbstractTaskQueue;-><init>(Lnet/gogame/gopay/vip/TaskQueue$Listener;)V

    .line 18
    invoke-static {p1, p2}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->create(Ljava/io/File;Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;)Lnet/gogame/gopay/vip/tape2/ObjectQueue;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gopay/vip/TapeTaskQueue;->a:Lnet/gogame/gopay/vip/tape2/ObjectQueue;

    return-void
.end method


# virtual methods
.method public declared-synchronized add(Ljava/lang/Object;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)V"
        }
    .end annotation

    monitor-enter p0

    .line 24
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/vip/TapeTaskQueue;->a:Lnet/gogame/gopay/vip/tape2/ObjectQueue;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->add(Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string v0, "goPay"

    const-string v1, "Exception"

    .line 26
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 28
    :goto_0
    monitor-exit p0

    return-void

    .line 23
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized peek()Ljava/lang/Object;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TT;"
        }
    .end annotation

    monitor-enter p0

    .line 33
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/vip/TapeTaskQueue;->a:Lnet/gogame/gopay/vip/tape2/ObjectQueue;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->peek()Ljava/lang/Object;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    goto :goto_0

    :catch_0
    move-exception v0

    :try_start_1
    const-string v1, "goPay"

    const-string v2, "Exception"

    .line 35
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    const/4 v0, 0x0

    .line 36
    monitor-exit p0

    return-object v0

    .line 32
    :goto_0
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized remove()V
    .locals 3

    monitor-enter p0

    .line 43
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/vip/TapeTaskQueue;->a:Lnet/gogame/gopay/vip/tape2/ObjectQueue;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->remove()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception v0

    goto :goto_1

    :catch_0
    move-exception v0

    :try_start_1
    const-string v1, "goPay"

    const-string v2, "Exception"

    .line 45
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 47
    :goto_0
    monitor-exit p0

    return-void

    .line 42
    :goto_1
    monitor-exit p0

    throw v0
.end method
