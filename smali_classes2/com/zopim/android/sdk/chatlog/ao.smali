.class Lcom/zopim/android/sdk/chatlog/ao;
.super Lcom/zopim/android/sdk/data/observers/AgentsObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ao;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/AgentsObserver;-><init>()V

    return-void
.end method

.method static synthetic a(Lcom/zopim/android/sdk/chatlog/ao;Ljava/lang/String;Lcom/zopim/android/sdk/model/Agent;)V
    .locals 0

    invoke-direct {p0, p1, p2}, Lcom/zopim/android/sdk/chatlog/ao;->a(Ljava/lang/String;Lcom/zopim/android/sdk/model/Agent;)V

    return-void
.end method

.method private a(Ljava/lang/String;Lcom/zopim/android/sdk/model/Agent;)V
    .locals 3

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->isTyping()Ljava/lang/Boolean;

    move-result-object v0

    if-nez v0, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$500()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Can\'t update agent typing while typing event is null"

    invoke-static {p1, p2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$500()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Agent "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->getDisplayName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " typing "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->isTyping()Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    new-instance v0, Lcom/zopim/android/sdk/chatlog/g;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/g;-><init>()V

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->isTyping()Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    iput-boolean v1, v0, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->getAvatarUri()Ljava/lang/String;

    move-result-object v1

    iput-object v1, v0, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    iput-object v1, v0, Lcom/zopim/android/sdk/chatlog/g;->l:Ljava/lang/Long;

    iput-object p1, v0, Lcom/zopim/android/sdk/chatlog/g;->k:Ljava/lang/String;

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ao;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1500(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/chatlog/i;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/chatlog/i;->getItemCount()I

    move-result v1

    add-int/lit8 v1, v1, -0x1

    invoke-virtual {p1, v1}, Lcom/zopim/android/sdk/chatlog/i;->b(I)Lcom/zopim/android/sdk/chatlog/aa;

    move-result-object v1

    instance-of v2, v1, Lcom/zopim/android/sdk/chatlog/g;

    if-eqz v2, :cond_1

    check-cast v1, Lcom/zopim/android/sdk/chatlog/g;

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->isTyping()Ljava/lang/Boolean;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p2

    iput-boolean p2, v1, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    goto :goto_0

    :cond_1
    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/aa;)V

    :goto_0
    invoke-virtual {p1}, Lcom/zopim/android/sdk/chatlog/i;->getItemCount()I

    move-result p2

    add-int/lit8 p2, p2, -0x1

    invoke-virtual {p1, p2}, Lcom/zopim/android/sdk/chatlog/i;->notifyItemChanged(I)V

    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/ao;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    iget-object p2, p2, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {p2}, Landroidx/recyclerview/widget/RecyclerView;->getLayoutManager()Landroidx/recyclerview/widget/RecyclerView$LayoutManager;

    move-result-object p2

    invoke-virtual {p1}, Lcom/zopim/android/sdk/chatlog/i;->getItemCount()I

    move-result p1

    add-int/lit8 p1, p1, -0x1

    invoke-virtual {p2, p1}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->scrollToPosition(I)V

    return-void
.end method

.method static synthetic b(Lcom/zopim/android/sdk/chatlog/ao;Ljava/lang/String;Lcom/zopim/android/sdk/model/Agent;)V
    .locals 0

    invoke-direct {p0, p1, p2}, Lcom/zopim/android/sdk/chatlog/ao;->b(Ljava/lang/String;Lcom/zopim/android/sdk/model/Agent;)V

    return-void
.end method

.method private b(Ljava/lang/String;Lcom/zopim/android/sdk/model/Agent;)V
    .locals 5

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ao;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1500(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v0

    check-cast v0, Lcom/zopim/android/sdk/chatlog/i;

    const/4 v1, 0x0

    :goto_0
    invoke-virtual {v0}, Lcom/zopim/android/sdk/chatlog/i;->getItemCount()I

    move-result v2

    if-ge v1, v2, :cond_1

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/chatlog/i;->b(I)Lcom/zopim/android/sdk/chatlog/aa;

    move-result-object v2

    instance-of v2, v2, Lcom/zopim/android/sdk/chatlog/a;

    if-eqz v2, :cond_0

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/chatlog/i;->b(I)Lcom/zopim/android/sdk/chatlog/aa;

    move-result-object v2

    check-cast v2, Lcom/zopim/android/sdk/chatlog/a;

    iget-object v3, v2, Lcom/zopim/android/sdk/chatlog/a;->k:Ljava/lang/String;

    invoke-virtual {p1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->getAvatarUri()Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_1

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->getAvatarUri()Ljava/lang/String;

    move-result-object v3

    iget-object v4, v2, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    invoke-virtual {v3, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_0

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Agent;->getAvatarUri()Ljava/lang/String;

    move-result-object v3

    iput-object v3, v2, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method


# virtual methods
.method public update(Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Agent;",
            ">;)V"
        }
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ao;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/chatlog/ap;

    invoke-direct {v1, p0, p1}, Lcom/zopim/android/sdk/chatlog/ap;-><init>(Lcom/zopim/android/sdk/chatlog/ao;Ljava/util/Map;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
