.class public Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;
.super Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;
.source "FAQListMessageDM.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;
    }
.end annotation


# instance fields
.field public faqs:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;",
            ">;"
        }
    .end annotation
.end field

.field public isSuggestionsReadEventSent:Z

.field public suggestionsReadFAQPublishId:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;)V
    .locals 8

    .line 54
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->serverId:Ljava/lang/String;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->body:Ljava/lang/String;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v3

    .line 55
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->getEpochCreatedAtTime()J

    move-result-wide v4

    iget-object v6, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->authorName:Ljava/lang/String;

    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->FAQ_LIST:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    .line 54
    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    const/4 v0, 0x0

    .line 30
    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventSent:Z

    const-string v0, ""

    .line 31
    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    .line 56
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->faqs:Ljava/util/List;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    .line 57
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->shouldShowAgentNameForConversation:Z

    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->shouldShowAgentNameForConversation:Z

    .line 58
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageWithOptionInputDM;->conversationLocalId:Ljava/lang/Long;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->conversationLocalId:Ljava/lang/Long;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/util/List;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "J",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;",
            ">;)V"
        }
    .end annotation

    .line 41
    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->FAQ_LIST:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-wide v4, p4

    move-object v6, p6

    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    const/4 p1, 0x0

    .line 30
    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventSent:Z

    const-string p1, ""

    .line 31
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    .line 42
    iput-object p7, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    return-void
.end method

.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "J",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;",
            ">;",
            "Lcom/helpshift/conversation/activeconversation/message/MessageType;",
            ")V"
        }
    .end annotation

    move-object v8, p0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-wide v4, p4

    move-object v6, p6

    move-object/from16 v7, p8

    .line 35
    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    const/4 v0, 0x0

    .line 30
    iput-boolean v0, v8, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventSent:Z

    const-string v0, ""

    .line 31
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    move-object/from16 v0, p7

    .line 36
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/util/List;ZLjava/lang/String;)V
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "J",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM$FAQ;",
            ">;Z",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    move-object v8, p0

    .line 47
    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->FAQ_LIST:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-wide v4, p4

    move-object v6, p6

    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    const/4 v0, 0x0

    .line 30
    iput-boolean v0, v8, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventSent:Z

    const-string v0, ""

    .line 31
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    move-object/from16 v0, p7

    .line 48
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    move/from16 v0, p8

    .line 49
    iput-boolean v0, v8, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventSent:Z

    move-object/from16 v0, p9

    .line 50
    iput-object v0, v8, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    return-void
.end method

.method private markSuggestionsReadEventAsSent()V
    .locals 1

    const/4 v0, 0x1

    .line 139
    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventSent:Z

    .line 140
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object v0

    invoke-interface {v0, p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method


# virtual methods
.method public handleSuggestionClick(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 78
    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    invoke-static {p3}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p3

    if-nez p3, :cond_0

    return-void

    .line 82
    :cond_0
    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    invoke-static {p3}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p3

    if-eqz p3, :cond_1

    .line 83
    iput-object p4, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    .line 84
    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p3

    invoke-interface {p3, p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 87
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventPending()Z

    move-result p3

    if-eqz p3, :cond_2

    .line 88
    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->sendSuggestionReadEvent(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;Lcom/helpshift/account/domainmodel/UserDM;)V

    :cond_2
    return-void
.end method

.method public isSuggestionsReadEventPending()Z
    .locals 1

    .line 135
    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventSent:Z

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 63
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 64
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    if-eqz v0, :cond_0

    .line 65
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    .line 66
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->faqs:Ljava/util/List;

    :cond_0
    return-void
.end method

.method public sendSuggestionReadEvent(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 7

    .line 98
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 102
    :cond_0
    invoke-static {p2}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object p2

    .line 103
    invoke-interface {p1}, Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;->isInPreIssueMode()Z

    move-result v0

    if-eqz v0, :cond_1

    const-string v0, "preissue_id"

    .line 104
    invoke-interface {p1}, Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;->getPreIssueId()Ljava/lang/String;

    move-result-object p1

    invoke-interface {p2, v0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_1
    const-string v0, "issue_id"

    .line 107
    invoke-interface {p1}, Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;->getIssueId()Ljava/lang/String;

    move-result-object p1

    invoke-interface {p2, v0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :goto_0
    const-string p1, "message_id"

    .line 109
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->serverId:Ljava/lang/String;

    invoke-interface {p2, p1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p1, "faq_publish_id"

    .line 110
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->suggestionsReadFAQPublishId:Ljava/lang/String;

    invoke-interface {p2, p1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v5, "/faqs_suggestion_read/"

    .line 113
    new-instance v2, Lcom/helpshift/common/domain/network/POSTNetwork;

    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v2, v5, p1, v0}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 114
    new-instance p1, Lcom/helpshift/common/domain/network/IdempotentNetwork;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->getIdempotentPolicy()Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;

    move-result-object v4

    iget-object v6, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->serverId:Ljava/lang/String;

    move-object v1, p1

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/common/domain/network/IdempotentNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;Ljava/lang/String;Ljava/lang/String;)V

    .line 115
    new-instance v0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p1, v1}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 116
    new-instance p1, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 117
    new-instance v0, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v0, p1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 120
    :try_start_0
    new-instance p1, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {p1, p2}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    .line 121
    invoke-direct {p0}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->markSuggestionsReadEventAsSent()V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    .line 125
    iget-object p2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne p2, v0, :cond_2

    .line 126
    invoke-direct {p0}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->markSuggestionsReadEventAsSent()V

    :goto_1
    return-void

    .line 129
    :cond_2
    throw p1
.end method
