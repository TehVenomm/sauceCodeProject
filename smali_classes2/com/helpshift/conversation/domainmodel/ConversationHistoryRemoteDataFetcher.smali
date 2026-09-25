.class public Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;
.super Ljava/lang/Object;
.source "ConversationHistoryRemoteDataFetcher.java"


# instance fields
.field private domain:Lcom/helpshift/common/domain/Domain;

.field private platform:Lcom/helpshift/common/platform/Platform;

.field private userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 0

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 30
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->platform:Lcom/helpshift/common/platform/Platform;

    .line 31
    iput-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->domain:Lcom/helpshift/common/domain/Domain;

    .line 32
    iput-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    return-void
.end method

.method private buildFetchNetwork()Lcom/helpshift/common/domain/network/Network;
    .locals 4

    .line 36
    new-instance v0, Lcom/helpshift/common/domain/network/POSTNetwork;

    const-string v1, "/conversations/history/"

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, v2, v3}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 37
    new-instance v1, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 38
    new-instance v0, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 39
    new-instance v1, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v1, v0, v2}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 40
    new-instance v0, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    return-object v0
.end method

.method private buildRequestData(Ljava/lang/String;)Lcom/helpshift/common/platform/network/RequestData;
    .locals 2

    .line 45
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-static {v0}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "cursor"

    .line 46
    invoke-virtual {v0, v1, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 47
    new-instance p1, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {p1, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    return-object p1
.end method


# virtual methods
.method public fetchConversations(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationHistory;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/common/exception/RootAPIException;
        }
    .end annotation

    .line 51
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->buildFetchNetwork()Lcom/helpshift/common/domain/network/Network;

    move-result-object v0

    .line 52
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->buildRequestData(Ljava/lang/String;)Lcom/helpshift/common/platform/network/RequestData;

    move-result-object p1

    .line 53
    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p1

    .line 54
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationHistoryRemoteDataFetcher;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object v0

    iget-object p1, p1, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {v0, p1}, Lcom/helpshift/common/platform/network/ResponseParser;->parseConversationHistory(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationHistory;

    move-result-object p1

    return-object p1
.end method
