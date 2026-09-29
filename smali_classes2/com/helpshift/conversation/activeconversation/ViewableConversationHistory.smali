.class public Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;
.super Lcom/helpshift/conversation/activeconversation/ViewableConversation;
.source "ViewableConversationHistory.java"


# instance fields
.field private conversations:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/ConversationHistoryLoader;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V
    .locals 0

    .line 30
    invoke-direct/range {p0 .. p5}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/ConversationsLoader;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    .line 23
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    return-void
.end method


# virtual methods
.method public declared-synchronized getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 2

    monitor-enter p0

    .line 64
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    add-int/lit8 v1, v1, -0x1

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getAllConversations()Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 91
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getPaginationCursor()Lcom/helpshift/conversation/activeconversation/PaginationCursor;
    .locals 2

    monitor-enter p0

    .line 141
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 142
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 143
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->buildPaginationCursor(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/PaginationCursor;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :cond_0
    const/4 v0, 0x0

    .line 145
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 140
    monitor-exit p0

    throw v0
.end method

.method public getType()Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;
    .locals 1

    .line 35
    sget-object v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->HISTORY:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    return-object v0
.end method

.method public declared-synchronized init()V
    .locals 5

    monitor-enter p0

    .line 40
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversationLoader:Lcom/helpshift/conversation/loaders/ConversationsLoader;

    invoke-virtual {v0}, Lcom/helpshift/conversation/loaders/ConversationsLoader;->fetchInitialConversations()Ljava/util/List;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    .line 41
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 42
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    iput-wide v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 43
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v2, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateStateBasedOnMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 45
    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v1}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 46
    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v4, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v2, v3, v4}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    .line 49
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 39
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized initializeConversationsForUI()V
    .locals 6

    monitor-enter p0

    .line 69
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    .line 70
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 71
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v4

    .line 73
    iget-object v5, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v5, v3, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->initializeMessagesForUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    .line 75
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 68
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized onNewConversationStarted(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 1

    monitor-enter p0

    .line 85
    :try_start_0
    invoke-virtual {p1, p0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setListener(Lcom/helpshift/conversation/activeconversation/ConversationDMListener;)V

    .line 86
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 87
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 84
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized prependConversations(Ljava/util/List;)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    .line 109
    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 111
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 112
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v0, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 120
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v1

    .line 121
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    const/4 v3, 0x0

    const/4 v4, 0x0

    :goto_1
    if-ge v4, v1, :cond_2

    .line 123
    invoke-interface {p1, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 124
    iget-object v6, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v0, v6}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    if-eqz v6, :cond_1

    .line 126
    iget-object v6, v6, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    iget-object v5, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v6, v5}, Lcom/helpshift/common/util/HSObservableList;->prependItems(Ljava/util/Collection;)V

    goto :goto_2

    .line 129
    :cond_1
    invoke-interface {v2, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :goto_2
    add-int/lit8 v4, v4, 0x1

    goto :goto_1

    .line 134
    :cond_2
    invoke-static {v2}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result p1

    if-nez p1, :cond_3

    .line 135
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-interface {p1, v3, v2}, Ljava/util/List;->addAll(ILjava/util/Collection;)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 137
    :cond_3
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 108
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized registerMessagesObserver(Lcom/helpshift/common/util/HSListObserver;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/util/HSListObserver<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    .line 78
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;->conversations:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 79
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v2, p1}, Lcom/helpshift/common/util/HSObservableList;->setObserver(Lcom/helpshift/common/util/HSListObserver;)V

    .line 80
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->registerMessagesObserver()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    .line 82
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 77
    monitor-exit p0

    throw p1
.end method

.method public shouldOpen()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
