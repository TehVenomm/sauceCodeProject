.class public Lcom/helpshift/conversation/loaders/RemoteConversationLoader;
.super Ljava/lang/Object;
.source "RemoteConversationLoader.java"


# instance fields
.field private conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

.field private conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

.field private domain:Lcom/helpshift/common/domain/Domain;

.field private platform:Lcom/helpshift/common/platform/Platform;

.field private remoteDataFetcher:Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;

.field private remoteDataMerger:Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataMerger;

.field private userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V
    .locals 1

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 30
    iput-object p1, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->platform:Lcom/helpshift/common/platform/Platform;

    .line 31
    iput-object p2, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->domain:Lcom/helpshift/common/domain/Domain;

    .line 32
    iput-object p3, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 34
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    .line 35
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationInboxDAO()Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    .line 36
    new-instance v0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;

    invoke-direct {v0, p1, p2, p3}, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;)V

    iput-object v0, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->remoteDataFetcher:Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;

    .line 37
    new-instance v0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataMerger;

    invoke-direct {v0, p1, p2, p3, p4}, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataMerger;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    iput-object v0, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->remoteDataMerger:Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataMerger;

    return-void
.end method


# virtual methods
.method public hasMoreMessage()Z
    .locals 3

    .line 42
    iget-object v0, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getHasOlderMessages(J)Z

    move-result v0

    return v0
.end method

.method public declared-synchronized loadMoreMessages()Z
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/common/exception/RootAPIException;
        }
    .end annotation

    monitor-enter p0

    .line 50
    :try_start_0
    invoke-virtual {p0}, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->hasMoreMessage()Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 51
    monitor-exit p0

    return v1

    .line 54
    :cond_0
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v2, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v0, v2, v3}, Lcom/helpshift/conversation/dao/ConversationDAO;->getOldestMessageCursor(J)Ljava/lang/String;

    move-result-object v0

    .line 56
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v2, :cond_1

    .line 57
    monitor-exit p0

    return v1

    .line 62
    :cond_1
    :try_start_2
    iget-object v1, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->remoteDataFetcher:Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->fetchConversations(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationHistory;

    move-result-object v0
    :try_end_2
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 73
    :try_start_3
    iget-object v1, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v2, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    iget-boolean v4, v0, Lcom/helpshift/conversation/dto/ConversationHistory;->hasOlderMessages:Z

    invoke-interface {v1, v2, v3, v4}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveHasOlderMessages(JZ)V

    .line 75
    iget-object v1, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->remoteDataMerger:Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataMerger;

    iget-object v0, v0, Lcom/helpshift/conversation/dto/ConversationHistory;->conversations:Ljava/util/List;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataMerger;->merge(Ljava/util/List;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    const/4 v0, 0x1

    .line 76
    monitor-exit p0

    return v0

    :catch_0
    move-exception v0

    .line 65
    :try_start_4
    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq v1, v2, :cond_2

    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v1, v2, :cond_3

    .line 67
    :cond_2
    iget-object v1, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v3, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 69
    :cond_3
    throw v0
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    :catchall_0
    move-exception v0

    .line 49
    monitor-exit p0

    throw v0
.end method
