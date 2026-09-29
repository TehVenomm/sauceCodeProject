.class Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/data/LivechatChatLogPath;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "b"
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

.field private b:Landroid/os/Handler;

.field private c:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/data/LivechatChatLogPath;)V
    .locals 1

    iput-object p1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->a:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance p1, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-direct {p1, v0}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object p1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->b:Landroid/os/Handler;

    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    iput-object p1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->c:Ljava/util/Map;

    return-void
.end method


# virtual methods
.method declared-synchronized a(Ljava/lang/String;)V
    .locals 2

    monitor-enter p0

    :try_start_0
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Removing timeout runnable"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->b:Landroid/os/Handler;

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->c:Ljava/util/Map;

    invoke-interface {v1, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Runnable;

    invoke-virtual {v0, p1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method declared-synchronized a(Ljava/lang/String;Lcom/zopim/android/sdk/model/ChatLog;)V
    .locals 3

    monitor-enter p0

    if-nez p1, :cond_0

    :try_start_0
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->access$000()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Can not add chat log without an id"

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    goto :goto_0

    :cond_0
    if-nez p2, :cond_1

    :try_start_1
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->access$000()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Can not add chat log that is null"

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    monitor-exit p0

    return-void

    :cond_1
    :try_start_2
    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->c:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;

    if-eqz v0, :cond_2

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->access$000()Ljava/lang/String;

    move-result-object v1

    const-string v2, "Removing previous timeout"

    invoke-static {v1, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->b:Landroid/os/Handler;

    invoke-virtual {v1, v0}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    :cond_2
    new-instance v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->a:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-direct {v0, v1, p1, p2}, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;-><init>(Lcom/zopim/android/sdk/data/LivechatChatLogPath;Ljava/lang/String;Lcom/zopim/android/sdk/model/ChatLog;)V

    iget-object p2, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->c:Ljava/util/Map;

    invoke-interface {p2, p1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->access$000()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Scheduling timeout runnable"

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->b:Landroid/os/Handler;

    const-wide/16 v1, 0x1388

    invoke-virtual {p1, v0, v1, v2}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    monitor-exit p0

    return-void

    :goto_0
    monitor-exit p0

    throw p1
.end method
