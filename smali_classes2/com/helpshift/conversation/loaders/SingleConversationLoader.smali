.class public Lcom/helpshift/conversation/loaders/SingleConversationLoader;
.super Lcom/helpshift/conversation/loaders/ConversationsLoader;
.source "SingleConversationLoader.java"


# instance fields
.field private activeConversationId:Ljava/lang/Long;

.field private isActiveConversationFullyFetched:Z

.field private platform:Lcom/helpshift/common/platform/Platform;

.field private userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/Long;Lcom/helpshift/conversation/loaders/RemoteConversationLoader;J)V
    .locals 6

    .line 22
    new-instance v2, Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;

    .line 23
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object v0

    invoke-direct {v2, v0, p3}, Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;-><init>(Lcom/helpshift/conversation/dao/ConversationDAO;Ljava/lang/Long;)V

    move-object v0, p0

    move-object v1, p1

    move-object v3, p4

    move-wide v4, p5

    .line 22
    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/loaders/ConversationsLoader;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/conversation/loaders/ConversationDBLoader;Lcom/helpshift/conversation/loaders/RemoteConversationLoader;J)V

    const/4 p4, 0x0

    .line 15
    iput-boolean p4, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->isActiveConversationFullyFetched:Z

    .line 25
    iput-object p1, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->platform:Lcom/helpshift/common/platform/Platform;

    .line 26
    iput-object p2, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 27
    iput-object p3, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->activeConversationId:Ljava/lang/Long;

    return-void
.end method


# virtual methods
.method public hasMoreMessages()Z
    .locals 9

    .line 38
    iget-boolean v0, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->isActiveConversationFullyFetched:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 42
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->conversationDBLoader:Lcom/helpshift/conversation/loaders/ConversationDBLoader;

    invoke-virtual {v0}, Lcom/helpshift/conversation/loaders/ConversationDBLoader;->hasMoreMessages()Z

    move-result v0

    const/4 v2, 0x1

    if-eqz v0, :cond_1

    return v2

    .line 54
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->platform:Lcom/helpshift/common/platform/Platform;

    .line 55
    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object v0

    iget-object v3, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v3}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-interface {v0, v3, v4}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 56
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v3

    if-nez v3, :cond_5

    const-wide/16 v3, 0x0

    .line 59
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v5

    :cond_2
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_3

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 60
    iget-object v7, v6, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object v8, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->activeConversationId:Ljava/lang/Long;

    invoke-virtual {v7, v8}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v7

    if-eqz v7, :cond_2

    .line 61
    invoke-virtual {v6}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v3

    .line 67
    :cond_3
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_4
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_5

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 68
    iget-object v6, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object v7, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->activeConversationId:Ljava/lang/Long;

    invoke-virtual {v6, v7}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-nez v6, :cond_4

    .line 69
    invoke-virtual {v5}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v5

    cmp-long v7, v3, v5

    if-lez v7, :cond_4

    .line 75
    iput-boolean v2, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->isActiveConversationFullyFetched:Z

    return v1

    .line 83
    :cond_5
    iget-object v0, p0, Lcom/helpshift/conversation/loaders/SingleConversationLoader;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    invoke-virtual {v0}, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->hasMoreMessage()Z

    move-result v0

    return v0
.end method
