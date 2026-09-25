.class Lnet/gogame/chat/zopim/ZopimChatContext$4;
.super Ljava/lang/Object;
.source "ZopimChatContext.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/zopim/ZopimChatContext;->doNotifyDataSetChanged()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/zopim/ZopimChatContext;


# direct methods
.method constructor <init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V
    .locals 0

    .line 92
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$4;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 7

    .line 97
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 98
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 99
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v2

    invoke-interface {v2}, Lcom/zopim/android/sdk/data/DataSource;->getChatLog()Ljava/util/LinkedHashMap;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/LinkedHashMap;->keySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v2

    const/4 v3, 0x1

    :cond_0
    const/4 v4, 0x1

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_2

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    .line 100
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v6

    invoke-interface {v6}, Lcom/zopim/android/sdk/data/DataSource;->getChatLog()Ljava/util/LinkedHashMap;

    move-result-object v6

    invoke-virtual {v6, v5}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/zopim/android/sdk/model/ChatLog;

    .line 101
    invoke-interface {v0, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const/4 v6, 0x0

    if-eqz v4, :cond_1

    .line 103
    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v4

    invoke-interface {v1, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 105
    :cond_1
    invoke-static {v6}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v4

    invoke-interface {v1, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 107
    :goto_1
    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v4

    sget-object v5, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    if-ne v4, v5, :cond_0

    const/4 v4, 0x0

    goto :goto_0

    .line 113
    :cond_2
    iget-object v2, p0, Lnet/gogame/chat/zopim/ZopimChatContext$4;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-static {v2, v0}, Lnet/gogame/chat/zopim/ZopimChatContext;->access$102(Lnet/gogame/chat/zopim/ZopimChatContext;Ljava/util/List;)Ljava/util/List;

    .line 114
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext$4;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-static {v0, v1}, Lnet/gogame/chat/zopim/ZopimChatContext;->access$202(Lnet/gogame/chat/zopim/ZopimChatContext;Ljava/util/List;)Ljava/util/List;

    .line 115
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext$4;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v1

    invoke-interface {v1}, Lcom/zopim/android/sdk/data/DataSource;->getAgents()Ljava/util/LinkedHashMap;

    move-result-object v1

    invoke-static {v0, v1}, Lnet/gogame/chat/zopim/ZopimChatContext;->access$302(Lnet/gogame/chat/zopim/ZopimChatContext;Ljava/util/Map;)Ljava/util/Map;

    .line 116
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext$4;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-virtual {v0}, Lnet/gogame/chat/zopim/ZopimChatContext;->notifyDataSetChanged()V

    return-void
.end method
