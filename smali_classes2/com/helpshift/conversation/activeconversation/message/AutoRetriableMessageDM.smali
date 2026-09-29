.class public abstract Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/MessageDM;
.source "AutoRetriableMessageDM.java"


# instance fields
.field private syncStatus:I


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;I)V
    .locals 0

    .line 33
    invoke-direct/range {p0 .. p7}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 34
    iput p8, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->syncStatus:I

    return-void
.end method

.method private updateSyncStatusIntoMemoryAndDB(I)V
    .locals 1

    .line 39
    iget v0, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->syncStatus:I

    if-ne v0, p1, :cond_0

    return-void

    .line 44
    :cond_0
    iput p1, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->syncStatus:I

    .line 45
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p1

    invoke-interface {p1, p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method


# virtual methods
.method public getSyncStatus()I
    .locals 1

    .line 57
    iget v0, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->syncStatus:I

    return v0
.end method

.method public isRetriable()Z
    .locals 2

    .line 89
    iget v0, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->syncStatus:I

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method makeNetworkRequest(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/common/platform/network/Response;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Lcom/helpshift/common/platform/network/Response;"
        }
    .end annotation

    .line 62
    new-instance v1, Lcom/helpshift/common/domain/network/POSTNetwork;

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v1, p1, v0, v2}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 63
    new-instance v6, Lcom/helpshift/common/domain/network/IdempotentNetwork;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->getIdempotentPolicy()Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;

    move-result-object v3

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->localId:Ljava/lang/Long;

    .line 64
    invoke-static {v0}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    move-object v0, v6

    move-object v4, p1

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/common/domain/network/IdempotentNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;Ljava/lang/String;Ljava/lang/String;)V

    .line 65
    new-instance p1, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p1, v6, v0}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 66
    new-instance v0, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {v0, p1}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 67
    new-instance p1, Lcom/helpshift/common/domain/network/UserPreConditionsFailedNetwork;

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/network/UserPreConditionsFailedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 68
    new-instance v0, Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;

    invoke-direct {v0, p1}, Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 69
    new-instance p1, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 70
    new-instance v0, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v0, p1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 73
    :try_start_0
    new-instance p1, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {p1, p2}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    .line 77
    iget-object p2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-eq p2, v0, :cond_1

    iget-object p2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->USER_PRE_CONDITION_FAILED:Lcom/helpshift/common/exception/NetworkException;

    if-ne p2, v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p2, 0x1

    .line 82
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->updateSyncStatusIntoMemoryAndDB(I)V

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p2, 0x3

    .line 79
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->updateSyncStatusIntoMemoryAndDB(I)V

    .line 84
    :goto_1
    throw p1
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 50
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 51
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;

    if-eqz v0, :cond_0

    .line 52
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;

    iget p1, p1, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->syncStatus:I

    iput p1, p0, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->syncStatus:I

    :cond_0
    return-void
.end method

.method public abstract send(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)V
.end method
