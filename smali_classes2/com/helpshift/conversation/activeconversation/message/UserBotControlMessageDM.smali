.class public Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;
.source "UserBotControlMessageDM.java"


# instance fields
.field public actionType:Ljava/lang/String;

.field public botInfo:Ljava/lang/String;

.field public reason:Ljava/lang/String;

.field public refersMessageId:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    .locals 10

    move-object v9, p0

    .line 38
    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_BOT_CONTROL:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    const/4 v6, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-wide v3, p3

    move-object v5, p5

    move/from16 v8, p10

    invoke-direct/range {v0 .. v8}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;I)V

    move-object/from16 v0, p6

    .line 39
    iput-object v0, v9, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->actionType:Ljava/lang/String;

    move-object/from16 v0, p7

    .line 40
    iput-object v0, v9, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->reason:Ljava/lang/String;

    move-object/from16 v0, p8

    .line 41
    iput-object v0, v9, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->botInfo:Ljava/lang/String;

    move-object/from16 v0, p9

    .line 42
    iput-object v0, v9, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->refersMessageId:Ljava/lang/String;

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

    .line 47
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 48
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;

    if-eqz v0, :cond_0

    .line 49
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;

    .line 50
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->actionType:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->actionType:Ljava/lang/String;

    .line 51
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->reason:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->reason:Ljava/lang/String;

    .line 52
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->botInfo:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->botInfo:Ljava/lang/String;

    .line 53
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->refersMessageId:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->refersMessageId:Ljava/lang/String;

    :cond_0
    return-void
.end method

.method public send(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)V
    .locals 3

    .line 59
    invoke-static {p1}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "origin"

    const-string v2, "mobile"

    .line 60
    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "type"

    .line 61
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->actionType:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "chatbot_cancelled_reason"

    .line 62
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->reason:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "body"

    .line 63
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->body:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "chatbot_info"

    .line 64
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->botInfo:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "refers"

    .line 65
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->refersMessageId:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 68
    invoke-interface {p2}, Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;->isInPreIssueMode()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 69
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->getPreIssueSendMessageRoute(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)Ljava/lang/String;

    move-result-object p2

    goto :goto_0

    .line 72
    :cond_0
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->getIssueSendMessageRoute(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)Ljava/lang/String;

    move-result-object p2

    .line 77
    :goto_0
    :try_start_0
    invoke-virtual {p0, p2, v0}, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->makeNetworkRequest(Ljava/lang/String;Ljava/util/Map;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p2
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    .line 87
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object p1

    .line 88
    iget-object p2, p2, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    const/4 v0, 0x0

    .line 89
    invoke-interface {p1, p2, v0}, Lcom/helpshift/common/platform/network/ResponseParser;->parseBotControlMessage(Ljava/lang/String;Z)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;

    .line 91
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 92
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->serverId:Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->serverId:Ljava/lang/String;

    .line 95
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p1

    invoke-interface {p1, p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void

    :catch_0
    move-exception p2

    .line 80
    iget-object v0, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-eq v0, v1, :cond_1

    iget-object v0, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-ne v0, v1, :cond_2

    .line 82
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v0

    iget-object v1, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {v0, p1, v1}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 84
    :cond_2
    throw p2
.end method
