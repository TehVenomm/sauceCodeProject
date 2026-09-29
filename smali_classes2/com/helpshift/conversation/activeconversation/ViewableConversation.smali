.class public abstract Lcom/helpshift/conversation/activeconversation/ViewableConversation;
.super Ljava/lang/Object;
.source "ViewableConversation.java"

# interfaces
.implements Lcom/helpshift/conversation/activeconversation/ConversationDMListener;
.implements Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;
.implements Lcom/helpshift/conversation/loaders/ConversationsLoader$LoadMoreConversationsCallback;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;
    }
.end annotation


# instance fields
.field protected conversationLoader:Lcom/helpshift/conversation/loaders/ConversationsLoader;

.field protected conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field private conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

.field protected domain:Lcom/helpshift/common/domain/Domain;

.field private isLoadMoreInProgress:Ljava/util/concurrent/atomic/AtomicBoolean;

.field protected liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

.field protected platform:Lcom/helpshift/common/platform/Platform;

.field private sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

.field protected userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/ConversationsLoader;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V
    .locals 2

    .line 60
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 46
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isLoadMoreInProgress:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 61
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->platform:Lcom/helpshift/common/platform/Platform;

    .line 62
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->domain:Lcom/helpshift/common/domain/Domain;

    .line 63
    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 64
    iput-object p4, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationLoader:Lcom/helpshift/conversation/loaders/ConversationsLoader;

    .line 65
    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 66
    iput-object p5, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    return-void
.end method


# virtual methods
.method protected buildPaginationCursor(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/PaginationCursor;
    .locals 2

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 414
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getCreatedAt()Ljava/lang/String;

    move-result-object v0

    .line 422
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-static {v1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v1

    if-eqz v1, :cond_1

    move-object p1, v0

    goto :goto_0

    .line 427
    :cond_1
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    const/4 v1, 0x0

    invoke-virtual {p1, v1}, Lcom/helpshift/common/util/HSObservableList;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getCreatedAt()Ljava/lang/String;

    move-result-object p1

    .line 429
    :goto_0
    new-instance v1, Lcom/helpshift/conversation/activeconversation/PaginationCursor;

    invoke-direct {v1, v0, p1}, Lcom/helpshift/conversation/activeconversation/PaginationCursor;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    return-object v1
.end method

.method public checkForReopen(ILjava/lang/String;Z)Z
    .locals 2

    .line 186
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 187
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 188
    invoke-virtual {v1, v0, p1, p2, p3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->checkForReOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Z)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 194
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->refreshConversationOnIssueStateUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 196
    iget-object p2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->onIssueStatusChange(Lcom/helpshift/conversation/dto/IssueState;)V

    :cond_0
    return p1
.end method

.method public abstract getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;
.end method

.method public abstract getAllConversations()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation
.end method

.method public getConversationVMCallback()Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;
    .locals 1

    .line 277
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    return-object v0
.end method

.method public abstract getPaginationCursor()Lcom/helpshift/conversation/activeconversation/PaginationCursor;
.end method

.method public abstract getType()Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;
.end method

.method public getUIConversations()Ljava/util/List;
    .locals 16
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/UIConversation;",
            ">;"
        }
    .end annotation

    .line 326
    invoke-virtual/range {p0 .. p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object v0

    .line 327
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 328
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v2

    if-eqz v2, :cond_0

    return-object v1

    .line 331
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v2

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_1

    .line 333
    invoke-interface {v0, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 334
    new-instance v15, Lcom/helpshift/conversation/activeconversation/UIConversation;

    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v5}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    invoke-virtual {v4}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getCreatedAt()Ljava/lang/String;

    move-result-object v8

    .line 335
    invoke-virtual {v4}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v9

    iget-object v11, v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    .line 336
    invoke-virtual {v4}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v12

    iget-object v13, v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    iget-boolean v14, v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    move-object v4, v15

    move v7, v3

    invoke-direct/range {v4 .. v14}, Lcom/helpshift/conversation/activeconversation/UIConversation;-><init>(JILjava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/dto/IssueState;Z)V

    .line 338
    invoke-interface {v1, v15}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_1
    return-object v1
.end method

.method public handleIdempotentPreIssueCreationSuccess()V
    .locals 1

    .line 237
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    .line 238
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->init()V

    .line 239
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->handleIdempotentPreIssueCreationSuccess()V

    :cond_0
    return-void
.end method

.method public handlePreIssueCreationSuccess()V
    .locals 1

    .line 227
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    .line 228
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->handlePreIssueCreationSuccess()V

    :cond_0
    return-void
.end method

.method public hasMoreMessages()Z
    .locals 1

    .line 319
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationLoader:Lcom/helpshift/conversation/loaders/ConversationsLoader;

    invoke-virtual {v0}, Lcom/helpshift/conversation/loaders/ConversationsLoader;->hasMoreMessages()Z

    move-result v0

    return v0
.end method

.method public abstract init()V
.end method

.method public abstract initializeConversationsForUI()V
.end method

.method public isActiveConversationEqual(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 3

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 209
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    if-nez v1, :cond_1

    return v0

    .line 215
    :cond_1
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_2

    .line 216
    iget-object v0, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1

    .line 218
    :cond_2
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_3

    .line 219
    iget-object v0, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1

    :cond_3
    return v0
.end method

.method public isAgentTyping()Z
    .locals 1

    .line 266
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isAgentTyping()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldEnableTypingIndicator()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isConversationVMAttached()Z
    .locals 1

    .line 98
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isVisibleOnUI()Z
    .locals 1

    .line 87
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->isVisibleOnUI()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public loadMoreMessages()V
    .locals 3

    .line 345
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isLoadMoreInProgress:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 347
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationLoader:Lcom/helpshift/conversation/loaders/ConversationsLoader;

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getPaginationCursor()Lcom/helpshift/conversation/activeconversation/PaginationCursor;

    move-result-object v1

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/conversation/loaders/ConversationsLoader;->loadMoreConversations(Lcom/helpshift/conversation/activeconversation/PaginationCursor;Lcom/helpshift/conversation/loaders/ConversationsLoader$LoadMoreConversationsCallback;)V

    :cond_0
    return-void
.end method

.method public loading()V
    .locals 2

    .line 400
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isLoadMoreInProgress:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V

    .line 401
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    .line 402
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->onHistoryLoadingStarted()V

    :cond_0
    return-void
.end method

.method public mergeIssueForActiveConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    .locals 5

    .line 142
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 143
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 144
    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    .line 147
    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    const/4 v4, 0x1

    .line 148
    invoke-virtual {v3, v0, p1, v4, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->mergeIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    .line 151
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz p1, :cond_0

    .line 152
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {p1}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->onConversationInboxPollSuccess()V

    :cond_0
    const-string p1, "preissue"

    .line 156
    invoke-virtual {p1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    const-string p1, "issue"

    iget-object p2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    invoke-virtual {p1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 157
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->startLiveUpdates()V

    .line 161
    :cond_1
    iget-object p1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p1, v1, :cond_5

    .line 164
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->refreshConversationOnIssueStateUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 170
    invoke-static {p1}, Lcom/helpshift/conversation/ConversationUtil;->isInProgressState(Lcom/helpshift/conversation/dto/IssueState;)Z

    move-result p2

    const/4 v0, 0x0

    if-eqz p2, :cond_2

    invoke-static {v1}, Lcom/helpshift/conversation/ConversationUtil;->isInProgressState(Lcom/helpshift/conversation/dto/IssueState;)Z

    move-result p2

    if-eqz p2, :cond_2

    const/4 p2, 0x1

    goto :goto_0

    :cond_2
    const/4 p2, 0x0

    .line 172
    :goto_0
    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->COMPLETED_ISSUE_CREATED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v1, v2, :cond_3

    const/4 v0, 0x1

    :cond_3
    if-nez v0, :cond_4

    if-nez p2, :cond_5

    .line 176
    :cond_4
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->onIssueStatusChange(Lcom/helpshift/conversation/dto/IssueState;)V

    :cond_5
    return-void
.end method

.method public mergePreIssueForActiveConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    .locals 4

    .line 119
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 120
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 123
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    const/4 v3, 0x1

    .line 124
    invoke-virtual {v2, v0, p1, v3, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->mergePreIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    .line 127
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz p1, :cond_0

    .line 128
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {p1}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->onConversationInboxPollSuccess()V

    .line 132
    :cond_0
    iget-object p1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p1, v1, :cond_1

    .line 135
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->refreshConversationOnIssueStateUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 137
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->onIssueStatusChange(Lcom/helpshift/conversation/dto/IssueState;)V

    :cond_1
    return-void
.end method

.method public onAdminAttachmentMessageClicked(Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;)V
    .locals 2

    .line 106
    sget-object v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    goto :goto_0

    .line 112
    :pswitch_0
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;

    .line 113
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/message/AdminAttachmentMessageDM;->handleClick(Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;)V

    goto :goto_0

    .line 108
    :pswitch_1
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    .line 109
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->handleClick(Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;)V

    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public onAgentTypingUpdate(Z)V
    .locals 1

    .line 271
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    .line 272
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {v0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->onAgentTypingUpdate(Z)V

    :cond_0
    return-void
.end method

.method public onError()V
    .locals 2

    .line 392
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isLoadMoreInProgress:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V

    .line 393
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    .line 394
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->onHistoryLoadingError()V

    :cond_0
    return-void
.end method

.method public onIssueStatusChange(Lcom/helpshift/conversation/dto/IssueState;)V
    .locals 1

    .line 245
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    .line 246
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {v0, p1}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->onIssueStatusChange(Lcom/helpshift/conversation/dto/IssueState;)V

    :cond_0
    return-void
.end method

.method public abstract onNewConversationStarted(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
.end method

.method public onScreenshotMessageClicked(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V
    .locals 1

    .line 102
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->handleClick(Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;)V

    return-void
.end method

.method public onSuccess(Ljava/util/List;Z)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;Z)V"
        }
    .end annotation

    .line 355
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz v0, :cond_0

    .line 356
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->onHistoryLoadingSuccess()V

    .line 359
    :cond_0
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_2

    .line 360
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isLoadMoreInProgress:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {p1, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V

    .line 363
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz p1, :cond_1

    .line 364
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-interface {p1, v0, p2}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->prependConversations(Ljava/util/List;Z)V

    :cond_1
    return-void

    .line 369
    :cond_2
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 370
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 371
    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v3}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    iput-wide v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 372
    invoke-virtual {p0, v2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isActiveConversationEqual(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v3

    if-eqz v3, :cond_3

    .line 374
    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 375
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v4

    invoke-virtual {v3, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldEnableMessagesClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v3

    if-eqz v3, :cond_3

    const/4 v3, 0x1

    goto :goto_1

    :cond_3
    const/4 v3, 0x0

    .line 376
    :goto_1
    iget-object v4, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v5, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 377
    invoke-virtual {v4, v2, v5, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->initializeMessageListForUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/List;Z)V

    .line 378
    invoke-virtual {v0, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 381
    :cond_4
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->prependConversations(Ljava/util/List;)V

    .line 383
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    if-eqz p1, :cond_5

    .line 384
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    invoke-interface {p1, v0, p2}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->prependConversations(Ljava/util/List;Z)V

    .line 387
    :cond_5
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isLoadMoreInProgress:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {p1, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V

    return-void
.end method

.method public abstract prependConversations(Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract registerMessagesObserver(Lcom/helpshift/common/util/HSListObserver;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/util/HSListObserver<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation
.end method

.method public setConversationVMCallback(Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;)V
    .locals 0

    .line 281
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    .line 282
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    invoke-virtual {p1, p0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setListener(Lcom/helpshift/conversation/activeconversation/ConversationDMListener;)V

    return-void
.end method

.method public setLiveUpdateDM(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;)V
    .locals 0

    .line 77
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    return-void
.end method

.method public abstract shouldOpen()Z
.end method

.method public startLiveUpdates()V
    .locals 2

    .line 252
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 253
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    if-eqz v1, :cond_0

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v1

    if-nez v1, :cond_0

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 254
    invoke-virtual {v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldEnableTypingIndicator()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 255
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v1, p0, v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->registerListener(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public stopLiveUpdates()V
    .locals 1

    .line 260
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    if-eqz v0, :cond_0

    .line 261
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->unregisterListener()V

    :cond_0
    return-void
.end method

.method public unregisterConversationVMCallback()V
    .locals 2

    const/4 v0, 0x0

    .line 286
    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->conversationVMCallback:Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    .line 287
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setListener(Lcom/helpshift/conversation/activeconversation/ConversationDMListener;)V

    return-void
.end method
