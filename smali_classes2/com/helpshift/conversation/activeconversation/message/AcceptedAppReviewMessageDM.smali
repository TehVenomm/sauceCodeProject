.class public Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;
.source "AcceptedAppReviewMessageDM.java"


# instance fields
.field public referredMessageId:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V
    .locals 9

    .line 22
    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ACCEPTED_APP_REVIEW:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    const/4 v6, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    move-object v5, p5

    move/from16 v8, p7

    invoke-direct/range {v0 .. v8}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;I)V

    move-object v1, p6

    .line 23
    iput-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->referredMessageId:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public isUISupportedMessage()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 28
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 29
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    if-eqz v0, :cond_0

    .line 30
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    .line 31
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->referredMessageId:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->referredMessageId:Ljava/lang/String;

    :cond_0
    return-void
.end method

.method public send(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/common/exception/RootAPIException;
        }
    .end annotation

    .line 38
    invoke-interface {p2}, Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;->getIssueId()Ljava/lang/String;

    move-result-object v0

    .line 39
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_2

    .line 44
    invoke-static {p1}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "body"

    .line 45
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->body:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "type"

    const-string v2, "ar"

    .line 46
    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "refers"

    .line 47
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->referredMessageId:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 49
    :try_start_0
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->getIssueSendMessageRoute(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p0, p2, v0}, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->makeNetworkRequest(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p2

    .line 52
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object v0

    .line 53
    iget-object p2, p2, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    .line 54
    invoke-interface {v0, p2}, Lcom/helpshift/common/platform/network/ResponseParser;->parseAcceptedAppReviewMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    move-result-object p2

    .line 56
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 57
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->serverId:Ljava/lang/String;

    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->serverId:Ljava/lang/String;

    .line 59
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p2

    invoke-interface {p2, p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p2

    .line 62
    iget-object v0, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq v0, v1, :cond_0

    iget-object v0, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v0, v1, :cond_1

    .line 64
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v0

    iget-object v1, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {v0, p1, v1}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 66
    :cond_1
    throw p2

    .line 40
    :cond_2
    new-instance p1, Ljava/lang/UnsupportedOperationException;

    const-string p2, "AcceptedAppReviewMessageDM send called with conversation in pre issue mode."

    invoke-direct {p1, p2}, Ljava/lang/UnsupportedOperationException;-><init>(Ljava/lang/String;)V

    throw p1
.end method
