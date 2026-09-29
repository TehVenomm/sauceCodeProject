.class public Lcom/github/droidfu/DroidFuApplication;
.super Landroid/app/Application;
.source "DroidFuApplication.java"


# instance fields
.field private contextObjects:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;>;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 24
    invoke-direct {p0}, Landroid/app/Application;-><init>()V

    .line 26
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/github/droidfu/DroidFuApplication;->contextObjects:Ljava/util/HashMap;

    return-void
.end method


# virtual methods
.method public declared-synchronized getActiveContext(Ljava/lang/String;)Landroid/content/Context;
    .locals 2

    monitor-enter p0

    .line 29
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/DroidFuApplication;->contextObjects:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/ref/WeakReference;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    const/4 p1, 0x0

    .line 31
    monitor-exit p0

    return-object p1

    .line 34
    :cond_0
    :try_start_1
    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    if-nez v0, :cond_1

    .line 36
    iget-object v1, p0, Lcom/github/droidfu/DroidFuApplication;->contextObjects:Ljava/util/HashMap;

    invoke-virtual {v1, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 38
    :cond_1
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    .line 28
    monitor-exit p0

    throw p1
.end method

.method public onClose()V
    .locals 0

    return-void
.end method

.method public declared-synchronized resetActiveContext(Ljava/lang/String;)V
    .locals 1

    monitor-enter p0

    .line 47
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/DroidFuApplication;->contextObjects:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 48
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 46
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized setActiveContext(Ljava/lang/String;Landroid/content/Context;)V
    .locals 1

    monitor-enter p0

    .line 42
    :try_start_0
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    .line 43
    iget-object p2, p0, Lcom/github/droidfu/DroidFuApplication;->contextObjects:Ljava/util/HashMap;

    invoke-virtual {p2, p1, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 44
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 41
    monitor-exit p0

    throw p1
.end method
