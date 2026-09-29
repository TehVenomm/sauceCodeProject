.class Lcom/zopim/android/sdk/api/f;
.super Lcom/zopim/android/sdk/data/observers/ConnectionObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/ChatService;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/ChatService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/ConnectionObserver;-><init>()V

    return-void
.end method

.method private a()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    iget-object v0, v0, Lcom/zopim/android/sdk/api/ChatService;->mUnsentMessages:Ljava/util/Queue;

    invoke-interface {v0}, Ljava/util/Queue;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Resending cached unsent messages"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    iget-object v0, v0, Lcom/zopim/android/sdk/api/ChatService;->mUnsentMessages:Ljava/util/Queue;

    invoke-interface {v0}, Ljava/util/Queue;->poll()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-eqz v0, :cond_1

    iget-object v1, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/api/ChatService;->send(Ljava/lang/String;)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method private b()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    iget-object v0, v0, Lcom/zopim/android/sdk/api/ChatService;->mUnsentFiles:Ljava/util/Queue;

    invoke-interface {v0}, Ljava/util/Queue;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Resending cached unsent files"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    iget-object v0, v0, Lcom/zopim/android/sdk/api/ChatService;->mUnsentFiles:Ljava/util/Queue;

    invoke-interface {v0}, Ljava/util/Queue;->poll()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/io/File;

    if-eqz v0, :cond_1

    iget-object v1, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-virtual {v1, v0}, Lcom/zopim/android/sdk/api/ChatService;->send(Ljava/io/File;)V

    goto :goto_0

    :cond_1
    return-void
.end method


# virtual methods
.method public update(Lcom/zopim/android/sdk/model/Connection;)V
    .locals 1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Connection;->getStatus()Lcom/zopim/android/sdk/model/Connection$Status;

    move-result-object p1

    sget-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->CONNECTED:Lcom/zopim/android/sdk/model/Connection$Status;

    if-ne p1, v0, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {p1}, Lcom/zopim/android/sdk/api/ChatService;->access$300(Lcom/zopim/android/sdk/api/ChatService;)Z

    move-result p1

    if-nez p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/api/f;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {p1}, Lcom/zopim/android/sdk/api/ChatService;->access$400(Lcom/zopim/android/sdk/api/ChatService;)V

    :cond_0
    invoke-direct {p0}, Lcom/zopim/android/sdk/api/f;->a()V

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/f;->b()V

    :cond_1
    return-void
.end method
