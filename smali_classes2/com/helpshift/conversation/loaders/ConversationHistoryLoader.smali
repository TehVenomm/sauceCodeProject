.class public Lcom/helpshift/conversation/loaders/ConversationHistoryLoader;
.super Lcom/helpshift/conversation/loaders/ConversationsLoader;
.source "ConversationHistoryLoader.java"


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/RemoteConversationLoader;J)V
    .locals 6

    .line 12
    new-instance v2, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;

    .line 13
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object v0

    invoke-direct {v2, p2, v0}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;-><init>(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/dao/ConversationDAO;)V

    move-object v0, p0

    move-object v1, p1

    move-object v3, p3

    move-wide v4, p4

    .line 12
    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/loaders/ConversationsLoader;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/conversation/loaders/ConversationDBLoader;Lcom/helpshift/conversation/loaders/RemoteConversationLoader;J)V

    return-void
.end method


# virtual methods
.method public hasMoreMessages()Z
    .locals 1

    .line 20
    iget-object v0, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryLoader;->conversationDBLoader:Lcom/helpshift/conversation/loaders/ConversationDBLoader;

    invoke-virtual {v0}, Lcom/helpshift/conversation/loaders/ConversationDBLoader;->hasMoreMessages()Z

    move-result v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryLoader;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    invoke-virtual {v0}, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->hasMoreMessage()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method
