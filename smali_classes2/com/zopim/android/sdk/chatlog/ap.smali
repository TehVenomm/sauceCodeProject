.class Lcom/zopim/android/sdk/chatlog/ap;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Ljava/util/Map;

.field final synthetic b:Lcom/zopim/android/sdk/chatlog/ao;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ao;Ljava/util/Map;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ap;->b:Lcom/zopim/android/sdk/chatlog/ao;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ap;->a:Ljava/util/Map;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ap;->b:Lcom/zopim/android/sdk/chatlog/ao;

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/ao;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1500(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v0

    instance-of v0, v0, Lcom/zopim/android/sdk/chatlog/i;

    if-nez v0, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$500()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Aborting update. Adapter must be of type "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-class v2, Lcom/zopim/android/sdk/chatlog/i;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ap;->a:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/zopim/android/sdk/model/Agent;

    iget-object v3, p0, Lcom/zopim/android/sdk/chatlog/ap;->b:Lcom/zopim/android/sdk/chatlog/ao;

    invoke-static {v3, v2, v1}, Lcom/zopim/android/sdk/chatlog/ao;->a(Lcom/zopim/android/sdk/chatlog/ao;Ljava/lang/String;Lcom/zopim/android/sdk/model/Agent;)V

    iget-object v3, p0, Lcom/zopim/android/sdk/chatlog/ap;->b:Lcom/zopim/android/sdk/chatlog/ao;

    invoke-static {v3, v2, v1}, Lcom/zopim/android/sdk/chatlog/ao;->b(Lcom/zopim/android/sdk/chatlog/ao;Ljava/lang/String;Lcom/zopim/android/sdk/model/Agent;)V

    goto :goto_0

    :cond_1
    invoke-static {}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$500()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Agents updated"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
