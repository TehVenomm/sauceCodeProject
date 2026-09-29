.class public Lcom/helpshift/common/domain/One;
.super Lcom/helpshift/common/domain/F;
.source "One.java"


# instance fields
.field private final f:Lcom/helpshift/common/domain/F;

.field private final running:Ljava/util/concurrent/atomic/AtomicBoolean;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/F;)V
    .locals 2

    .line 17
    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    .line 15
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    iput-object v0, p0, Lcom/helpshift/common/domain/One;->running:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 18
    iput-object p1, p0, Lcom/helpshift/common/domain/One;->f:Lcom/helpshift/common/domain/F;

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 23
    iget-object v0, p0, Lcom/helpshift/common/domain/One;->running:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 25
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/One;->f:Lcom/helpshift/common/domain/F;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/F;->f()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 28
    iget-object v0, p0, Lcom/helpshift/common/domain/One;->running:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V

    goto :goto_0

    :catchall_0
    move-exception v0

    iget-object v2, p0, Lcom/helpshift/common/domain/One;->running:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v2, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V

    .line 29
    throw v0

    :cond_0
    :goto_0
    return-void
.end method

.method public getF()Lcom/helpshift/common/domain/F;
    .locals 1

    .line 34
    iget-object v0, p0, Lcom/helpshift/common/domain/One;->f:Lcom/helpshift/common/domain/F;

    return-object v0
.end method
