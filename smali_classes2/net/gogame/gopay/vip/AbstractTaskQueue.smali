.class public abstract Lnet/gogame/gopay/vip/AbstractTaskQueue;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Ljava/lang/Runnable;
.implements Lnet/gogame/gopay/vip/TaskQueue;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;",
        "Ljava/lang/Runnable;",
        "Lnet/gogame/gopay/vip/TaskQueue<",
        "TT;>;"
    }
.end annotation


# static fields
.field public static final DEFAULT_DELAY:J = 0xea60L


# instance fields
.field private final a:Lnet/gogame/gopay/vip/TaskQueue$Listener;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lnet/gogame/gopay/vip/TaskQueue$Listener<",
            "TT;>;"
        }
    .end annotation
.end field

.field private b:J

.field private c:Ljava/lang/Thread;


# direct methods
.method public constructor <init>(Lnet/gogame/gopay/vip/TaskQueue$Listener;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lnet/gogame/gopay/vip/TaskQueue$Listener<",
            "TT;>;)V"
        }
    .end annotation

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/32 v0, 0xea60

    .line 9
    iput-wide v0, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->b:J

    const/4 v0, 0x0

    .line 10
    iput-object v0, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->c:Ljava/lang/Thread;

    .line 15
    iput-object p1, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->a:Lnet/gogame/gopay/vip/TaskQueue$Listener;

    return-void
.end method


# virtual methods
.method public getDelay()J
    .locals 2

    .line 19
    iget-wide v0, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->b:J

    return-wide v0
.end method

.method protected abstract peek()Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TT;"
        }
    .end annotation
.end method

.method protected abstract remove()V
.end method

.method public run()V
    .locals 3

    .line 44
    :goto_0
    :try_start_0
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/AbstractTaskQueue;->shouldProcess()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 45
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/AbstractTaskQueue;->peek()Ljava/lang/Object;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1

    if-nez v0, :cond_0

    goto :goto_1

    .line 50
    :cond_0
    :try_start_1
    iget-object v1, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->a:Lnet/gogame/gopay/vip/TaskQueue$Listener;

    if-eqz v1, :cond_1

    .line 51
    iget-object v1, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->a:Lnet/gogame/gopay/vip/TaskQueue$Listener;

    invoke-interface {v1, v0}, Lnet/gogame/gopay/vip/TaskQueue$Listener;->onTask(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 53
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/AbstractTaskQueue;->remove()V

    goto :goto_0

    .line 58
    :cond_1
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/AbstractTaskQueue;->remove()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/InterruptedException; {:try_start_1 .. :try_end_1} :catch_2

    goto :goto_0

    :catch_0
    move-exception v0

    :try_start_2
    const-string v1, "goPay"

    const-string v2, "Exception"

    .line 61
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    .line 64
    :cond_2
    :goto_1
    iget-wide v0, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->b:J

    invoke-static {v0, v1}, Ljava/lang/Thread;->sleep(J)V
    :try_end_2
    .catch Ljava/lang/InterruptedException; {:try_start_2 .. :try_end_2} :catch_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_0

    :catch_1
    move-exception v0

    const-string v1, "goPay"

    const-string v2, "Exception"

    .line 68
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :catch_2
    return-void
.end method

.method public setDelay(J)V
    .locals 0

    .line 23
    iput-wide p1, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->b:J

    return-void
.end method

.method protected abstract shouldProcess()Z
.end method

.method public start()V
    .locals 1

    .line 27
    iget-object v0, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->c:Ljava/lang/Thread;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->c:Ljava/lang/Thread;

    invoke-virtual {v0}, Ljava/lang/Thread;->isAlive()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 28
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    invoke-direct {v0}, Ljava/lang/IllegalStateException;-><init>()V

    throw v0

    .line 30
    :cond_1
    :goto_0
    new-instance v0, Ljava/lang/Thread;

    invoke-direct {v0, p0}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    iput-object v0, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->c:Ljava/lang/Thread;

    .line 31
    iget-object v0, p0, Lnet/gogame/gopay/vip/AbstractTaskQueue;->c:Ljava/lang/Thread;

    invoke-virtual {v0}, Ljava/lang/Thread;->start()V

    return-void
.end method
