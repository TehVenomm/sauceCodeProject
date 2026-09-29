.class public Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;
.super Lcom/helpshift/conversation/activeconversation/ViewableConversation;
.source "ViewableSingleConversation.java"


# instance fields
.field private conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/SingleConversationLoader;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V
    .locals 0

    .line 26
    invoke-direct/range {p0 .. p5}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/ConversationsLoader;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    return-void
.end method


# virtual methods
.method public getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 1

    .line 51
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    return-object v0
.end method

.method public getAllConversations()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 77
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-static {v0}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public getPaginationCursor()Lcom/helpshift/conversation/activeconversation/PaginationCursor;
    .locals 1

    .line 98
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->buildPaginationCursor(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/PaginationCursor;

    move-result-object v0

    return-object v0
.end method

.method public getType()Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;
    .locals 1

    .line 31
    sget-object v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->SINGLE:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    return-object v0
.end method

.method public declared-synchronized init()V
    .locals 4

    monitor-enter p0

    .line 36
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversationLoader:Lcom/helpshift/conversation/loaders/ConversationsLoader;

    invoke-virtual {v0}, Lcom/helpshift/conversation/loaders/ConversationsLoader;->fetchInitialConversations()Ljava/util/List;

    move-result-object v0

    const/4 v1, 0x0

    .line 37
    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 38
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    iput-wide v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 39
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 40
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    .line 42
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 35
    monitor-exit p0

    throw v0
.end method

.method public initializeConversationsForUI()V
    .locals 3

    .line 56
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->initializeMessagesForUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    return-void
.end method

.method public onNewConversationStarted(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 0

    return-void
.end method

.method public prependConversations(Ljava/util/List;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    .line 87
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_1

    .line 89
    invoke-interface {p1, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 90
    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object v4, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v3, v4}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 91
    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v3, v2}, Lcom/helpshift/common/util/HSObservableList;->prependItems(Ljava/util/Collection;)V

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method

.method public registerMessagesObserver(Lcom/helpshift/common/util/HSListObserver;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/util/HSListObserver<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 61
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/util/HSObservableList;->setObserver(Lcom/helpshift/common/util/HSListObserver;)V

    .line 62
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->registerMessagesObserver()V

    return-void
.end method

.method public shouldOpen()Z
    .locals 2

    .line 46
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->conversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    return v0
.end method
