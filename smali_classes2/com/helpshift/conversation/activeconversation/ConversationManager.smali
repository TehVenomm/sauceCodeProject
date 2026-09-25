.class public Lcom/helpshift/conversation/activeconversation/ConversationManager;
.super Ljava/lang/Object;
.source "ConversationManager.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_ConvManager"


# instance fields
.field private conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

.field domain:Lcom/helpshift/common/domain/Domain;

.field platform:Lcom/helpshift/common/platform/Platform;

.field private sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

.field userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 0

    .line 110
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 111
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    .line 112
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    .line 113
    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 114
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    .line 115
    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 0

    .line 100
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->deleteOptionsForAdminMessageWithOptionsInput(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    return-void
.end method

.method private addMessageToDBAndGlobalList(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2

    .line 231
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v0, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 232
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p2, v0, v1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 233
    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->addObserver(Ljava/util/Observer;)V

    .line 234
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p1, p2}, Lcom/helpshift/common/util/HSObservableList;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method private addMessageToDbAndUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 492
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v0, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 493
    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method

.method private clearRedactedAttachmentsResources(Ljava/util/List;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;",
            ">;)V"
        }
    .end annotation

    .line 632
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 636
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/activeconversation/ConversationManager$6;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$6;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Ljava/util/List;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private deleteOptionsForAdminMessageWithOptionsInput(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V
    .locals 2

    .line 218
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->referredMessageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v0, v1, :cond_0

    .line 219
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->serverId:Ljava/lang/String;

    .line 220
    invoke-interface {v0, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;

    .line 221
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->options:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->clear()V

    .line 222
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v0, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    :cond_0
    return-void
.end method

.method private deleteOptionsForAdminMessageWithOptionsInput(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 3

    .line 925
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    .line 926
    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    sget-object p1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-interface {v0, v1, v2, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(JLcom/helpshift/conversation/activeconversation/message/MessageType;)Ljava/util/List;

    move-result-object p1

    .line 927
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 928
    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->options:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->clear()V

    goto :goto_0

    .line 930
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v0, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessages(Ljava/util/List;)V

    return-void
.end method

.method private evaluateBotControlMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Collection;)V
    .locals 17
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Ljava/util/Collection<",
            "+",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    .line 663
    invoke-interface/range {p2 .. p2}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 664
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 666
    sget-object v5, Lcom/helpshift/conversation/activeconversation/ConversationManager$12;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    invoke-virtual {v4}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v4

    aget v4, v5, v4

    const/4 v5, 0x1

    if-eq v4, v5, :cond_0

    goto :goto_0

    .line 671
    :cond_0
    iget-object v4, v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v4}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v4

    .line 672
    iget-object v5, v4, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v8, v5

    check-cast v8, Ljava/lang/String;

    .line 673
    iget-object v4, v4, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v4, Ljava/lang/Long;

    invoke-virtual {v4}, Ljava/lang/Long;->longValue()J

    move-result-wide v9

    .line 674
    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/UnsupportedAdminMessageWithInputDM;

    .line 676
    new-instance v4, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;

    const-string v7, "Unsupported bot input"

    const-string v11, "mobile"

    const-string v12, "bot_cancelled"

    const-string v13, "unsupported_bot_input"

    iget-object v14, v3, Lcom/helpshift/conversation/activeconversation/message/UnsupportedAdminMessageWithInputDM;->botInfo:Ljava/lang/String;

    iget-object v15, v3, Lcom/helpshift/conversation/activeconversation/message/UnsupportedAdminMessageWithInputDM;->serverId:Ljava/lang/String;

    const/16 v16, 0x1

    move-object v6, v4

    invoke-direct/range {v6 .. v16}, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    .line 683
    iget-object v3, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v3, v4, Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 684
    invoke-direct {v0, v1, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDbAndUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 687
    new-instance v3, Lcom/helpshift/conversation/activeconversation/ConversationManager$7;

    invoke-direct {v3, v0, v4, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$7;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/UserBotControlMessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-direct {v0, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method private getLocalIdToPendingRequestIdMessageMap(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 781
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v0

    invoke-direct {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getRouteForSendingMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, p1}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getPendingRequestIdMapForRoute(Ljava/lang/String;)Ljava/util/Map;

    move-result-object p1

    return-object p1
.end method

.method private getMessageDMForUpdate(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/util/Map;Ljava/util/Map;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;",
            "Lcom/helpshift/conversation/activeconversation/ConversationUpdate;",
            ")",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;"
        }
    .end annotation

    .line 812
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-interface {p2, v0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 813
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-interface {p2, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    goto :goto_0

    .line 815
    :cond_0
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->createdRequestId:Ljava/lang/String;

    invoke-interface {p3, p2}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_1

    .line 816
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->createdRequestId:Ljava/lang/String;

    invoke-interface {p3, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 817
    iget-object p2, p4, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->localIdsForResolvedRequestIds:Ljava/util/List;

    iget-object p3, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    invoke-static {p3}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p3

    invoke-interface {p2, p3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    :goto_0
    return-object p1
.end method

.method private getRouteForSendingMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/lang/String;
    .locals 2

    .line 787
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 788
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/preissues/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getPreIssueId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/messages/"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    .line 791
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/issues/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getIssueId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/messages/"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    :goto_0
    return-object p1
.end method

.method private markMessagesAsSeen(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/List;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 1229
    invoke-static {p2}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 1233
    invoke-interface {p2, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->readAt:Ljava/lang/String;

    .line 1234
    invoke-interface {p2, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->seenAtMessageCursor:Ljava/lang/String;

    .line 1237
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-static {v2}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v2

    const-string v3, "read_at"

    .line 1238
    invoke-interface {v2, v3, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "mc"

    .line 1239
    invoke-interface {v2, v1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "md_state"

    const-string v1, "read"

    .line 1240
    invoke-interface {v2, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1242
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getRouteForSendingMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/lang/String;

    move-result-object p1

    .line 1244
    :try_start_0
    new-instance v0, Lcom/helpshift/common/domain/network/PUTNetwork;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p1, v1, v3}, Lcom/helpshift/common/domain/network/PUTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 1245
    new-instance p1, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 1246
    new-instance v0, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p1, v1}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 1247
    new-instance p1, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 1248
    new-instance v0, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v0, p1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 1249
    new-instance p1, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {p1, v2}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    .line 1252
    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq v0, v1, :cond_3

    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v0, v1, :cond_1

    goto :goto_0

    .line 1256
    :cond_1
    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v0, v1, :cond_2

    goto :goto_1

    .line 1257
    :cond_2
    throw p1

    .line 1254
    :cond_3
    :goto_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object p1, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 1262
    :goto_1
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_2
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    const/4 v1, 0x1

    .line 1263
    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isMessageSeenSynced:Z

    goto :goto_2

    .line 1265
    :cond_4
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p1, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessages(Ljava/util/List;)V

    return-void
.end method

.method private markSeenMessagesAsRead(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Set;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Ljava/util/Set<",
            "Ljava/lang/Long;",
            ">;)V"
        }
    .end annotation

    .line 1198
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v0

    .line 1199
    iget-object v0, v0, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    check-cast v0, Ljava/lang/String;

    .line 1201
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 1202
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 1204
    iget-object v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v3}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_0
    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_1

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1205
    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    if-eqz v5, :cond_0

    .line 1206
    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    invoke-interface {v1, v5, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 1209
    :cond_1
    invoke-interface {p2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :cond_2
    :goto_1
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_3

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/Long;

    .line 1210
    invoke-interface {v1, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    if-eqz v3, :cond_2

    .line 1212
    iput-object v0, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->readAt:Ljava/lang/String;

    const/4 v4, 0x1

    .line 1213
    iput v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->deliveryState:I

    .line 1214
    iget-object v4, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageCursor:Ljava/lang/String;

    iput-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->seenAtMessageCursor:Ljava/lang/String;

    .line 1215
    invoke-interface {v2, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 1219
    :cond_3
    invoke-static {v2}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result p2

    if-eqz p2, :cond_4

    return-void

    .line 1223
    :cond_4
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p2, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessages(Ljava/util/List;)V

    .line 1224
    invoke-direct {p0, p1, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->markMessagesAsSeen(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/List;)V

    return-void
.end method

.method private populateMessageDMLookup(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Map;Ljava/util/Map;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 742
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 743
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v1, v2, v3}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object v1

    .line 744
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 745
    iget-object v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v3}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_0
    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_1

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 747
    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    if-eqz v5, :cond_0

    .line 748
    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    invoke-interface {v2, v5, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 751
    :cond_1
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_3

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 752
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    invoke-interface {v2, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    if-nez v4, :cond_2

    .line 754
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 757
    :cond_2
    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 760
    :cond_3
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    .line 762
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getLocalIdToPendingRequestIdMessageMap(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/util/Map;

    move-result-object p1

    .line 764
    :cond_4
    :goto_2
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_6

    .line 765
    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 766
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_5

    .line 767
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-interface {p2, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 770
    :cond_5
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    if-eqz v2, :cond_4

    .line 771
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    invoke-static {v2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    if-eqz p1, :cond_4

    .line 773
    invoke-interface {p1, v2}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_4

    .line 774
    invoke-interface {p1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    invoke-interface {p3, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_2

    :cond_6
    return-void
.end method

.method private sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V
    .locals 2

    .line 238
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/activeconversation/ConversationManager$3;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$3;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/common/domain/F;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private sendReOpenRejectedMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Ljava/lang/String;)V
    .locals 10

    .line 196
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v0

    .line 197
    iget-object v1, v0, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v4, v1

    check-cast v4, Ljava/lang/String;

    .line 198
    iget-object v0, v0, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v0, Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    .line 199
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;

    const-string v7, "mobile"

    const/4 v3, 0x0

    const/4 v9, 0x1

    move-object v2, v0

    move-object v8, p4

    invoke-direct/range {v2 .. v9}, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V

    .line 202
    iput p2, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->reason:I

    .line 203
    iput-object p3, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->openConversationId:Ljava/lang/String;

    .line 204
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object p2, v0, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 205
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object p3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, p2, p3}, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 207
    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDBAndGlobalList(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 209
    new-instance p2, Lcom/helpshift/conversation/activeconversation/ConversationManager$2;

    invoke-direct {p2, p0, v0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$2;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-direct {p0, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private sendScreenshotMessageInternal(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;Z)V
    .locals 1

    .line 1045
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {p2, v0, p1, p3}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->uploadImage(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;Z)V

    .line 1051
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object p3, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p2, p3, :cond_0

    .line 1052
    sget-object p2, Lcom/helpshift/conversation/dto/IssueState;->WAITING_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p2

    .line 1056
    iget-object p3, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->CONVERSATION_ARCHIVED:Lcom/helpshift/common/exception/NetworkException;

    if-ne p3, v0, :cond_1

    .line 1057
    sget-object p2, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    :cond_0
    :goto_0
    return-void

    .line 1060
    :cond_1
    throw p2
.end method

.method private sendTextMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;)V
    .locals 2

    .line 996
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {p2, v0, p1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->send(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)V

    .line 1000
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p2, v0, :cond_1

    .line 1001
    sget-object p2, Lcom/helpshift/conversation/dto/IssueState;->WAITING_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p2

    .line 1005
    iget-object v0, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->CONVERSATION_ARCHIVED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v0, v1, :cond_0

    .line 1006
    sget-object p2, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    goto :goto_0

    .line 1008
    :cond_0
    iget-object v0, p2, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->USER_PRE_CONDITION_FAILED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v0, v1, :cond_2

    .line 1009
    sget-object p2, Lcom/helpshift/conversation/dto/IssueState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    :cond_1
    :goto_0
    return-void

    .line 1012
    :cond_2
    throw p2
.end method

.method private setCSATState(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/states/ConversationCSATState;)V
    .locals 3

    .line 1400
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    if-eq v0, p2, :cond_0

    const-string v0, "Helpshift_ConvManager"

    .line 1401
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Update CSAT state : Conversation : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", state : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Lcom/helpshift/conversation/states/ConversationCSATState;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1403
    :cond_0
    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    .line 1406
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p2, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    return-void
.end method

.method private updateMessageClickableState(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)V
    .locals 1

    .line 309
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v0, :cond_0

    .line 310
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->updateState(Z)V

    goto :goto_0

    .line 312
    :cond_0
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    if-eqz v0, :cond_1

    .line 313
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->setAttachmentButtonClickable(Z)V

    goto :goto_0

    .line 315
    :cond_1
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    if-eqz v0, :cond_2

    .line 316
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->updateState(Z)V

    :cond_2
    :goto_0
    return-void
.end method


# virtual methods
.method addMessageToUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2

    .line 497
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p2, v0, v1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 498
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isUISupportedMessage()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 499
    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->addObserver(Ljava/util/Observer;)V

    .line 500
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0, p2}, Lcom/helpshift/common/util/HSObservableList;->add(Ljava/lang/Object;)Z

    .line 501
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-static {p1}, Lcom/helpshift/conversation/ConversationUtil;->sortMessagesBasedOnCreatedAt(Ljava/util/List;)V

    :cond_0
    return-void
.end method

.method public addPreissueFirstUserMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;)V
    .locals 8

    const-string v0, "Helpshift_ConvManager"

    const-string v1, "Adding first user message to DB and UI."

    .line 962
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 963
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v0

    .line 964
    iget-object v1, v0, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v4, v1

    check-cast v4, Ljava/lang/String;

    .line 965
    iget-object v0, v0, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v0, Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    .line 966
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    const-string v7, "mobile"

    move-object v2, v0

    move-object v3, p2

    invoke-direct/range {v2 .. v7}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    .line 967
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, p2, v1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 968
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object p2, v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 972
    sget-object p2, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENDING:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v0, p2}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    .line 975
    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDbAndUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    return-void
.end method

.method public checkAndIncrementMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V
    .locals 2

    .line 1835
    invoke-static {p2}, Lcom/helpshift/conversation/ConversationUtil;->isInProgressState(Lcom/helpshift/conversation/dto/IssueState;)Z

    move-result p2

    const/4 v0, 0x1

    if-eqz p2, :cond_1

    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p2, v1, :cond_0

    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p2, v1, :cond_0

    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p2, v1, :cond_1

    .line 1839
    :cond_0
    invoke-virtual {p0, p1, v0, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setShouldIncrementMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZZ)V

    goto :goto_0

    .line 1841
    :cond_1
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result p2

    if-eqz p2, :cond_2

    const/4 p2, 0x0

    .line 1842
    invoke-virtual {p0, p1, p2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setShouldIncrementMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZZ)V

    :cond_2
    :goto_0
    return-void
.end method

.method public checkForReOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Z)Z
    .locals 16

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move/from16 v2, p2

    move-object/from16 v3, p3

    .line 122
    iget-object v4, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    const/4 v5, 0x0

    const/4 v6, 0x1

    if-eqz v4, :cond_6

    iget-object v4, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v4}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v4

    if-lez v4, :cond_6

    .line 123
    iget-object v4, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    iget-object v7, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v7}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v7

    sub-int/2addr v7, v6

    invoke-virtual {v4, v7}, Lcom/helpshift/common/util/HSObservableList;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 125
    instance-of v7, v4, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    if-nez v7, :cond_0

    return v5

    .line 130
    :cond_0
    move-object v7, v4

    check-cast v7, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    invoke-virtual {v7}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->isAnswered()Z

    move-result v8

    if-eqz v8, :cond_1

    return v5

    :cond_1
    const/4 v8, 0x0

    if-ne v2, v6, :cond_2

    .line 136
    iget-object v2, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-direct {v0, v1, v6, v8, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendReOpenRejectedMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_2
    if-eqz p4, :cond_3

    const/4 v2, 0x4

    .line 141
    iget-object v3, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-direct {v0, v1, v2, v8, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendReOpenRejectedMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_3
    const/4 v9, 0x2

    if-ne v2, v9, :cond_4

    const/4 v2, 0x3

    .line 146
    iget-object v3, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-direct {v0, v1, v2, v8, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendReOpenRejectedMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_4
    if-eqz v3, :cond_5

    .line 150
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 151
    invoke-virtual {v3, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_5

    .line 152
    iget-object v2, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-direct {v0, v1, v9, v3, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendReOpenRejectedMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 158
    :cond_5
    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->WAITING_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    iput-object v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 159
    iput-boolean v5, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isConversationEndedDelegateSent:Z

    .line 160
    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v2, v1}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 163
    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v2}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v2

    .line 164
    iget-object v3, v2, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v10, v3

    check-cast v10, Ljava/lang/String;

    .line 165
    iget-object v2, v2, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v2, Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v11

    .line 166
    new-instance v2, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    const/4 v9, 0x0

    const-string v13, "mobile"

    iget-object v14, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    const/4 v15, 0x1

    move-object v8, v2

    invoke-direct/range {v8 .. v15}, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;I)V

    .line 169
    iget-object v3, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v3, v2, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 170
    iget-object v3, v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v4, v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v2, v3, v4}, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 172
    invoke-direct {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDBAndGlobalList(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 176
    invoke-virtual {v7, v6}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->setAnsweredAndNotify(Z)V

    .line 177
    iget-object v3, v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v3, v7}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 179
    new-instance v3, Lcom/helpshift/conversation/activeconversation/ConversationManager$1;

    invoke-direct {v3, v0, v2, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$1;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-direct {v0, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V

    const/4 v5, 0x1

    :cond_6
    :goto_0
    return v5
.end method

.method public clearMessageUpdates(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    .locals 4

    const/4 v0, 0x0

    .line 1751
    :goto_0
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->localIdsForResolvedRequestIds:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    if-ge v0, v1, :cond_0

    .line 1752
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v1

    .line 1753
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getRouteForSendingMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Ljava/lang/String;

    move-result-object v2

    iget-object v3, p2, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->localIdsForResolvedRequestIds:Ljava/util/List;

    .line 1754
    invoke-interface {v3, v0}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 1753
    invoke-interface {v1, v2, v3}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->deletePendingRequestId(Ljava/lang/String;Ljava/lang/String;)V

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 1756
    :cond_0
    iget-object p1, p2, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->updatedMessageDMs:Ljava/util/List;

    invoke-interface {p1}, Ljava/util/List;->clear()V

    .line 1757
    iget-object p1, p2, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->newMessageDMs:Ljava/util/List;

    invoke-interface {p1}, Ljava/util/List;->clear()V

    return-void
.end method

.method public containsAtleastOneUserMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 4

    .line 1729
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_3

    .line 1730
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 1733
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p1}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1734
    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isUISupportedMessage()Z

    move-result v3

    if-eqz v3, :cond_0

    .line 1735
    instance-of v3, v2, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v3, :cond_1

    return v1

    .line 1738
    :cond_1
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1740
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v2

    const/4 v3, 0x3

    if-le v2, v3, :cond_0

    return v1

    :cond_2
    const/4 p1, 0x0

    return p1

    :cond_3
    return v1
.end method

.method public deleteCachedScreenshotFiles(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 4

    .line 1771
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object p1

    .line 1772
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 1773
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1774
    instance-of v2, v1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    if-eqz v2, :cond_0

    .line 1775
    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    .line 1777
    :try_start_0
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->getFilePath()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/common/util/FileUtil;->deleteFile(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    const/4 v2, 0x0

    .line 1778
    iput-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->filePath:Ljava/lang/String;

    .line 1779
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "Helpshift_ConvManager"

    const-string v3, "Exception while deleting ScreenshotMessageDM file"

    .line 1783
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    .line 1789
    :cond_1
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p1, v0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessages(Ljava/util/List;)V

    return-void
.end method

.method public dropCustomMetaData()V
    .locals 2

    .line 1721
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/meta/MetaDataDM;->setCustomMetaDataCallable(Lcom/helpshift/meta/RootMetaDataCallable;)V

    .line 1722
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/meta/MetaDataDM;->clearCustomMetaData()V

    return-void
.end method

.method public evaluateBotExecutionState(Ljava/util/List;Z)Z
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;Z)Z"
        }
    .end annotation

    if-eqz p1, :cond_4

    .line 831
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_1

    .line 837
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    const/4 v1, 0x1

    sub-int/2addr v0, v1

    :goto_0
    if-ltz v0, :cond_3

    .line 838
    invoke-interface {p1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 839
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 841
    sget-object v4, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_BOT_CONTROL:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v4, v3, :cond_2

    .line 842
    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;

    .line 843
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->actionType:Ljava/lang/String;

    const-string v4, "bot_started"

    .line 844
    invoke-virtual {v4, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_1

    return v1

    :cond_1
    const-string v4, "bot_ended"

    .line 847
    invoke-virtual {v4, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 848
    iget-boolean p1, v2, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->hasNextBot:Z

    return p1

    :cond_2
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    :cond_3
    return p2

    :cond_4
    :goto_1
    return p2
.end method

.method public filterMessagesOlderThanLastMessageInDb(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 5

    .line 431
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->getOldestMessageCursor(J)Ljava/lang/String;

    move-result-object v0

    .line 434
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    const/4 v2, 0x0

    if-nez v1, :cond_1

    .line 435
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 436
    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v3

    invoke-static {v3, v4}, Lcom/helpshift/conversation/util/predicate/MessagePredicates;->olderThanLastDbMessagePredicate(J)Lcom/helpshift/util/Predicate;

    move-result-object v0

    invoke-static {v1, v0}, Lcom/helpshift/util/Filters;->filter(Ljava/util/List;Lcom/helpshift/util/Predicate;)Ljava/util/List;

    move-result-object v0

    .line 437
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v1}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v1

    .line 438
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v3

    if-eqz v1, :cond_0

    if-nez v3, :cond_0

    const/4 v2, 0x1

    :cond_0
    if-eq v1, v3, :cond_1

    .line 441
    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setMessageDMs(Ljava/util/List;)V

    :cond_1
    return v2
.end method

.method public getLatestUnansweredBotMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 8

    .line 1582
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v0

    const/4 v1, 0x1

    sub-int/2addr v0, v1

    :goto_0
    const/4 v2, 0x0

    if-ltz v0, :cond_7

    .line 1583
    iget-object v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v3, v0}, Lcom/helpshift/common/util/HSObservableList;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1586
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_BOT_CONTROL:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v4, v5, :cond_0

    return-object v2

    .line 1590
    :cond_0
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_TEXT_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-eq v4, v5, :cond_2

    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-eq v4, v5, :cond_2

    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->FAQ_LIST_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-eq v4, v5, :cond_2

    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v5, Lcom/helpshift/conversation/activeconversation/message/MessageType;->OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v4, v5, :cond_1

    goto :goto_1

    :cond_1
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    :cond_2
    :goto_1
    const/4 v4, 0x0

    add-int/2addr v0, v1

    .line 1601
    :goto_2
    iget-object v5, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v5}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v5

    if-ge v0, v5, :cond_5

    .line 1602
    iget-object v5, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v5, v0}, Lcom/helpshift/common/util/HSObservableList;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1603
    iget-object v6, v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-eq v6, v7, :cond_3

    iget-object v6, v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_TEXT_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v6, v7, :cond_4

    .line 1606
    :cond_3
    check-cast v5, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    .line 1607
    iget-object v6, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-virtual {v5}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->getReferredMessageId()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v6, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_4

    goto :goto_3

    :cond_4
    add-int/lit8 v0, v0, 0x1

    goto :goto_2

    :cond_5
    const/4 v1, 0x0

    :goto_3
    if-eqz v1, :cond_6

    goto :goto_4

    :cond_6
    move-object v2, v3

    :goto_4
    return-object v2

    :cond_7
    return-object v2
.end method

.method public getUnSeenMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;)I
    .locals 5

    .line 1793
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 1797
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v0, v2, v3}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 1800
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1801
    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isUISupportedMessage()Z

    move-result v3

    if-eqz v3, :cond_1

    iget v3, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->deliveryState:I

    const/4 v4, 0x1

    if-eq v3, v4, :cond_1

    .line 1803
    sget-object v3, Lcom/helpshift/conversation/activeconversation/ConversationManager$12;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    iget-object v4, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-virtual {v4}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v4

    aget v3, v3, v4

    packed-switch v3, :pswitch_data_0

    goto :goto_0

    .line 1805
    :pswitch_0
    instance-of v3, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    if-eqz v3, :cond_1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;

    iget-boolean v2, v2, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;->isMessageEmpty:Z

    if-nez v2, :cond_1

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :pswitch_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1826
    :cond_2
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->shouldIncrementMessageCount:Z

    if-eqz p1, :cond_3

    add-int/lit8 v1, v1, 0x1

    :cond_3
    return v1

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_1
        :pswitch_0
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_1
    .end packed-switch
.end method

.method public handleAdminSuggestedQuestionRead(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 8

    .line 1705
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    move-object v4, v1

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1706
    instance-of v1, v4, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    if-eqz v1, :cond_0

    iget-object v1, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-virtual {p2, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 1707
    new-instance p2, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;

    move-object v2, p2

    move-object v3, p0

    move-object v5, p1

    move-object v6, p3

    move-object v7, p4

    invoke-direct/range {v2 .. v7}, Lcom/helpshift/conversation/activeconversation/ConversationManager$11;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Ljava/lang/String;)V

    invoke-direct {p0, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V

    :cond_1
    return-void
.end method

.method public handleAppReviewRequestClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V
    .locals 2

    .line 1270
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    .line 1271
    invoke-virtual {p2, v0, v1}, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->handleRequestReviewClick(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 1273
    new-instance v1, Lcom/helpshift/conversation/activeconversation/ConversationManager$9;

    invoke-direct {v1, p0, v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager$9;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;)V

    invoke-direct {p0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method public handleConversationEnded(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 914
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/activeconversation/ConversationManager$8;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$8;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public handlePreIssueCreationSuccess(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 1847
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iput-wide v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->lastUserActivityTime:J

    return-void
.end method

.method public hasBotSwitchedToAnotherBotInPollerResponse(Ljava/util/Collection;)Z
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)Z"
        }
    .end annotation

    const/4 v0, 0x0

    if-eqz p1, :cond_4

    .line 1629
    invoke-interface {p1}, Ljava/util/Collection;->size()I

    move-result v1

    if-nez v1, :cond_0

    goto :goto_1

    .line 1633
    :cond_0
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 1638
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result p1

    const/4 v2, 0x1

    sub-int/2addr p1, v2

    const/4 v3, 0x0

    :goto_0
    if-ltz p1, :cond_3

    .line 1639
    invoke-interface {v1, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1640
    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 1642
    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_BOT_CONTROL:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v6, v5, :cond_2

    .line 1643
    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;

    .line 1644
    iget-object v4, v4, Lcom/helpshift/conversation/activeconversation/message/AdminBotControlMessageDM;->actionType:Ljava/lang/String;

    const-string v5, "bot_ended"

    .line 1645
    invoke-virtual {v5, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_1

    return v3

    :cond_1
    const-string v5, "bot_started"

    .line 1650
    invoke-virtual {v5, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_2

    const/4 v3, 0x1

    :cond_2
    add-int/lit8 p1, p1, -0x1

    goto :goto_0

    :cond_3
    return v0

    :cond_4
    :goto_1
    return v0
.end method

.method public initializeIssueStatusForUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 949
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v1, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 950
    invoke-virtual {v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldShowConversationResolutionQuestion()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    .line 951
    invoke-virtual {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->markConversationResolutionStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    :cond_0
    return-void
.end method

.method public initializeMessageListForUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/List;Z)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;Z)V"
        }
    .end annotation

    .line 1486
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1487
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 1488
    iget-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->shouldShowAgentNameForConversation:Z

    .line 1489
    invoke-virtual {p0, v0, p3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageOnConversationUpdate(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)V

    .line 1490
    invoke-virtual {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateAcceptedRequestForReopenMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public initializeMessagesForUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
    .locals 4

    .line 1440
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-static {v0}, Lcom/helpshift/conversation/ConversationUtil;->sortMessagesBasedOnCreatedAt(Ljava/util/List;)V

    const/4 v0, 0x0

    if-eqz p2, :cond_3

    .line 1444
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p0, p2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->evaluateBotExecutionState(Ljava/util/List;Z)Z

    move-result p2

    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInBetweenBotExecution:Z

    .line 1445
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p2}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1446
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 1447
    iget-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->shouldShowAgentNameForConversation:Z

    .line 1448
    instance-of v1, v0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    if-eqz v1, :cond_0

    .line 1449
    move-object v1, v0

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v1, v2}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->downloadThumbnailImage(Lcom/helpshift/common/platform/Platform;)V

    .line 1451
    :cond_0
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldEnableMessagesClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    invoke-virtual {p0, v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageOnConversationUpdate(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)V

    .line 1452
    invoke-virtual {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateAcceptedRequestForReopenMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto :goto_0

    .line 1454
    :cond_1
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p2}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result p2

    if-lez p2, :cond_5

    .line 1456
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v0

    const/4 v1, 0x1

    sub-int/2addr v0, v1

    invoke-virtual {p2, v0}, Lcom/helpshift/common/util/HSObservableList;->get(I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1457
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v2, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-eq v0, v2, :cond_2

    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v2, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_TEXT_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v0, v2, :cond_5

    .line 1458
    :cond_2
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getLatestUnansweredBotMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v0

    .line 1464
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInBetweenBotExecution:Z

    if-eqz p1, :cond_5

    if-nez v0, :cond_5

    .line 1465
    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    invoke-virtual {p2, v1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->updateState(Z)V

    goto :goto_2

    .line 1471
    :cond_3
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p2}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_1
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_5

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1472
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 1473
    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    iput-boolean v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->shouldShowAgentNameForConversation:Z

    .line 1474
    instance-of v2, v1, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    if-eqz v2, :cond_4

    .line 1475
    move-object v2, v1

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v2, v3}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->downloadThumbnailImage(Lcom/helpshift/common/platform/Platform;)V

    .line 1477
    :cond_4
    invoke-virtual {p0, v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageOnConversationUpdate(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)V

    goto :goto_1

    :cond_5
    :goto_2
    return-void
.end method

.method public isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 1

    .line 1501
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method

.method public markConversationResolutionStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
    .locals 9

    .line 322
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v0

    .line 323
    iget-object v1, v0, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v4, v1

    check-cast v4, Ljava/lang/String;

    .line 324
    iget-object v0, v0, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v0, Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    if-eqz p2, :cond_0

    const-string v3, "Accepted the solution"

    .line 327
    new-instance p2, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    const-string v7, "mobile"

    const/4 v8, 0x1

    move-object v2, p2

    invoke-direct/range {v2 .. v8}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;I)V

    .line 329
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p2, v0, v1}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 330
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 332
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v0, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 335
    new-instance v0, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;

    invoke-direct {v0, p0, p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$4;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-direct {p0, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V

    .line 352
    sget-object p2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    .line 355
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object p2

    sget-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->RESOLUTION_ACCEPTED:Lcom/helpshift/analytics/AnalyticsEventType;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {p2, v0, p1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/lang/String;)V

    .line 358
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p1

    const-string p2, "User accepted the solution"

    invoke-virtual {p1, p2}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->userRepliedToConversation(Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    const-string v3, "Did not accept the solution"

    .line 362
    new-instance p2, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    const-string v7, "mobile"

    const/4 v8, 0x1

    move-object v2, p2

    invoke-direct/range {v2 .. v8}, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;I)V

    .line 364
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 366
    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDbAndUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 369
    new-instance v0, Lcom/helpshift/conversation/activeconversation/ConversationManager$5;

    invoke-direct {v0, p0, p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$5;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-direct {p0, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V

    .line 386
    sget-object p2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    .line 389
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object p2

    sget-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->RESOLUTION_REJECTED:Lcom/helpshift/analytics/AnalyticsEventType;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {p2, v0, p1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/lang/String;)V

    .line 392
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p1

    const-string p2, "User rejected the solution"

    invoke-virtual {p1, p2}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->userRepliedToConversation(Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public markMessagesAsSeen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 5

    .line 1169
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1170
    new-instance v1, Ljava/util/HashSet;

    invoke-direct {v1}, Ljava/util/HashSet;-><init>()V

    .line 1171
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1172
    iget v3, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->deliveryState:I

    const/4 v4, 0x1

    if-eq v3, v4, :cond_0

    .line 1173
    sget-object v3, Lcom/helpshift/conversation/activeconversation/ConversationManager$12;->$SwitchMap$com$helpshift$conversation$activeconversation$message$MessageType:[I

    iget-object v4, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-virtual {v4}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ordinal()I

    move-result v4

    aget v3, v3, v4

    packed-switch v3, :pswitch_data_0

    goto :goto_0

    .line 1184
    :pswitch_0
    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    invoke-interface {v1, v2}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 1190
    :cond_1
    invoke-interface {v1}, Ljava/util/Set;->size()I

    move-result v0

    if-nez v0, :cond_2

    return-void

    .line 1194
    :cond_2
    invoke-direct {p0, p1, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->markSeenMessagesAsRead(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Set;)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method public mergeIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    .locals 4

    .line 454
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 455
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 456
    sget-object v2, Lcom/helpshift/conversation/activeconversation/ConversationManager$12;->$SwitchMap$com$helpshift$conversation$dto$IssueState:[I

    invoke-virtual {v0}, Lcom/helpshift/conversation/dto/IssueState;->ordinal()I

    move-result v3

    aget v2, v2, v3

    const/4 v3, 0x4

    if-eq v2, v3, :cond_0

    goto :goto_0

    .line 461
    :cond_0
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v3, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v2, v3, :cond_1

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v3, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v2, v3, :cond_1

    goto :goto_0

    :cond_1
    move-object v0, v1

    .line 469
    :goto_0
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageCursor:Ljava/lang/String;

    if-eqz v1, :cond_2

    .line 471
    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageCursor:Ljava/lang/String;

    .line 473
    :cond_2
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 474
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 475
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    .line 476
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->title:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->title:Ljava/lang/String;

    .line 477
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    iput-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    .line 478
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    .line 479
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdAt:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdAt:Ljava/lang/String;

    .line 480
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v1

    iput-wide v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->epochCreatedAtTime:J

    .line 481
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    iput-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    .line 482
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updatedAt:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updatedAt:Ljava/lang/String;

    .line 484
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    sget-object v2, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    if-ne v1, v2, :cond_3

    .line 485
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    .line 487
    :cond_3
    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 488
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p0, p1, p3, p2, p4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLjava/util/List;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    return-void
.end method

.method public mergePreIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    .locals 3

    .line 858
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 860
    sget-object v1, Lcom/helpshift/conversation/activeconversation/ConversationManager$12;->$SwitchMap$com$helpshift$conversation$dto$IssueState:[I

    invoke-virtual {v0}, Lcom/helpshift/conversation/dto/IssueState;->ordinal()I

    move-result v2

    aget v1, v1, v2

    packed-switch v1, :pswitch_data_0

    goto :goto_0

    .line 862
    :pswitch_0
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->COMPLETED_ISSUE_CREATED:Lcom/helpshift/conversation/dto/IssueState;

    .line 864
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    goto :goto_0

    .line 870
    :pswitch_1
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    .line 877
    :goto_0
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageCursor:Ljava/lang/String;

    if-eqz v1, :cond_0

    .line 879
    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageCursor:Ljava/lang/String;

    .line 881
    :cond_0
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 882
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 883
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    .line 884
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->title:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->title:Ljava/lang/String;

    .line 885
    iget-boolean v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    iput-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    .line 886
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    .line 887
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdAt:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdAt:Ljava/lang/String;

    .line 888
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v1

    iput-wide v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->epochCreatedAtTime:J

    .line 889
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updatedAt:Ljava/lang/String;

    iput-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updatedAt:Ljava/lang/String;

    .line 890
    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 891
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p0, p1, p3, p2, p4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLjava/util/List;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x4
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public refreshConversationOnIssueStateUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 4

    .line 263
    sget-object v0, Lcom/helpshift/conversation/activeconversation/ConversationManager$12;->$SwitchMap$com$helpshift$conversation$dto$IssueState:[I

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {v1}, Lcom/helpshift/conversation/dto/IssueState;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    goto :goto_2

    .line 284
    :pswitch_0
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->handleConversationEnded(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    goto :goto_2

    .line 266
    :pswitch_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 267
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v1, v2, v3}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object v1

    .line 268
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 269
    instance-of v3, v2, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v3, :cond_0

    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    if-nez v3, :cond_0

    .line 270
    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 273
    :cond_1
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    .line 274
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    .line 275
    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->body:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "\n"

    .line 276
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_1

    .line 278
    :cond_2
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getConversationInboxDAO()Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    move-result-object v0

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    .line 279
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 278
    invoke-interface {v0, v2, v3, v1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveConversationArchivalPrefillText(JLjava/lang/String;)V

    .line 280
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->handleConversationEnded(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 287
    :goto_2
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessagesOnIssueStatusUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method public retryMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 1034
    instance-of v0, p2, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v0, :cond_0

    .line 1035
    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendTextMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;)V

    goto :goto_0

    .line 1037
    :cond_0
    instance-of v0, p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    if-eqz v0, :cond_1

    .line 1038
    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendScreenshotMessageInternal(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;Z)V

    :cond_1
    :goto_0
    return-void
.end method

.method public retryMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
    .locals 9

    .line 1066
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1067
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 1068
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 1069
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3}, Ljava/util/HashMap;-><init>()V

    .line 1070
    new-instance v4, Ljava/util/ArrayList;

    invoke-direct {v4}, Ljava/util/ArrayList;-><init>()V

    .line 1071
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_4

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1072
    iget-object v6, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v7, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v5, v6, v7}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 1073
    instance-of v6, v5, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;

    if-eqz v6, :cond_1

    move-object v6, v5

    check-cast v6, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;

    .line 1074
    invoke-virtual {v6}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->isRetriable()Z

    move-result v7

    if-eqz v7, :cond_1

    .line 1075
    invoke-interface {v1, v6}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1077
    :cond_1
    iget-object v6, v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->readAt:Ljava/lang/String;

    invoke-static {v6}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v6

    if-nez v6, :cond_2

    iget-boolean v6, v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isMessageSeenSynced:Z

    if-nez v6, :cond_2

    .line 1078
    invoke-interface {v2, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1080
    :cond_2
    instance-of v6, v5, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    if-eqz v6, :cond_3

    .line 1081
    iget-object v6, v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    move-object v7, v5

    check-cast v7, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    invoke-interface {v3, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1083
    :cond_3
    instance-of v6, v5, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    if-eqz v6, :cond_0

    .line 1084
    check-cast v5, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    .line 1085
    invoke-virtual {v5}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->isSuggestionsReadEventPending()Z

    move-result v6

    if-eqz v6, :cond_0

    .line 1086
    invoke-interface {v4, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 1092
    :cond_4
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_5
    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_c

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;

    .line 1094
    iget-object v5, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v6, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v5, v6, :cond_b

    iget-object v5, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v6, Lcom/helpshift/conversation/dto/IssueState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v5, v6, :cond_6

    goto :goto_2

    .line 1099
    :cond_6
    :try_start_0
    iget-object v5, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1, v5, p1}, Lcom/helpshift/conversation/activeconversation/message/AutoRetriableMessageDM;->send(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)V

    .line 1100
    instance-of v5, v1, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    if-eqz v5, :cond_5

    .line 1101
    new-instance v5, Ljava/util/ArrayList;

    invoke-direct {v5}, Ljava/util/ArrayList;-><init>()V

    .line 1104
    move-object v6, v1

    check-cast v6, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;

    .line 1105
    iget-object v7, v6, Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;->referredMessageId:Ljava/lang/String;

    .line 1106
    invoke-interface {v3, v7}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_7

    .line 1107
    invoke-interface {v3, v7}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;

    .line 1108
    iget-object v8, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v7, v8}, Lcom/helpshift/conversation/activeconversation/message/RequestAppReviewMessageDM;->handleAcceptedReviewSuccess(Lcom/helpshift/common/platform/Platform;)V

    .line 1111
    invoke-interface {v5, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_7
    if-eqz p2, :cond_5

    .line 1117
    invoke-interface {v5, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1118
    invoke-virtual {p0, p1, v6}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    const/4 v1, 0x1

    const/4 v6, 0x0

    .line 1119
    invoke-virtual {p0, p1, v1, v5, v6}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLjava/util/List;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v1

    .line 1124
    iget-object v5, v1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v6, Lcom/helpshift/common/exception/NetworkException;->CONVERSATION_ARCHIVED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v5, v6, :cond_8

    .line 1125
    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    goto :goto_1

    .line 1127
    :cond_8
    iget-object v5, v1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v6, Lcom/helpshift/common/exception/NetworkException;->USER_PRE_CONDITION_FAILED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v5, v6, :cond_9

    .line 1128
    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {p0, p1, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    goto :goto_1

    .line 1130
    :cond_9
    iget-object v5, v1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v6, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v5, v6, :cond_a

    goto :goto_1

    .line 1131
    :cond_a
    throw v1

    :cond_b
    :goto_2
    return-void

    .line 1138
    :cond_c
    new-instance p2, Ljava/util/HashMap;

    invoke-direct {p2}, Ljava/util/HashMap;-><init>()V

    .line 1139
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_3
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_e

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1140
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->readAt:Ljava/lang/String;

    .line 1141
    invoke-interface {p2, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/List;

    if-nez v3, :cond_d

    .line 1143
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    .line 1145
    :cond_d
    invoke-interface {v3, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1146
    invoke-interface {p2, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_3

    .line 1150
    :cond_e
    invoke-interface {p2}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_4
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_10

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 1152
    :try_start_1
    invoke-interface {p2, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/List;

    invoke-direct {p0, p1, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->markMessagesAsSeen(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/List;)V
    :try_end_1
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_4

    :catch_1
    move-exception v1

    .line 1155
    iget-object v2, v1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v3, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v2, v3, :cond_f

    goto :goto_4

    .line 1156
    :cond_f
    throw v1

    .line 1162
    :cond_10
    invoke-interface {v4}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_5
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_11

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;

    .line 1163
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0, p1, v1}, Lcom/helpshift/conversation/activeconversation/message/FAQListMessageDM;->sendSuggestionReadEvent(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;Lcom/helpshift/account/domainmodel/UserDM;)V

    goto :goto_5

    :cond_11
    return-void
.end method

.method public sendCSATSurvey(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;)V
    .locals 1

    const/4 v0, 0x5

    if-le p2, v0, :cond_0

    const/4 p2, 0x5

    goto :goto_0

    :cond_0
    if-gez p2, :cond_1

    const/4 p2, 0x0

    .line 1336
    :cond_1
    :goto_0
    iput p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatRating:I

    if-eqz p3, :cond_2

    .line 1338
    invoke-virtual {p3}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p3

    .line 1340
    :cond_2
    iput-object p3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatFeedback:Ljava/lang/String;

    .line 1341
    sget-object p2, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_NOT_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setCSATState(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/states/ConversationCSATState;)V

    .line 1344
    new-instance p2, Lcom/helpshift/conversation/activeconversation/ConversationManager$10;

    invoke-direct {p2, p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager$10;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-direct {p0, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V

    .line 1352
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p2

    iget p3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatRating:I

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatFeedback:Ljava/lang/String;

    invoke-virtual {p2, p3, p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->userCompletedCustomerSatisfactionSurvey(ILjava/lang/String;)V

    return-void
.end method

.method public sendCSATSurveyInternal(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 8

    .line 1356
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/issues/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "/customer-survey/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    .line 1358
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-static {v0}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "rating"

    .line 1359
    iget v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatRating:I

    invoke-static {v2}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "feedback"

    .line 1360
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatFeedback:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1365
    new-instance v3, Lcom/helpshift/common/domain/network/POSTNetwork;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v3, v6, v1, v2}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 1366
    new-instance v5, Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;

    invoke-direct {v5}, Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;-><init>()V

    .line 1367
    new-instance v1, Lcom/helpshift/common/domain/network/IdempotentNetwork;

    iget-object v4, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v7, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    move-object v2, v1

    invoke-direct/range {v2 .. v7}, Lcom/helpshift/common/domain/network/IdempotentNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;Ljava/lang/String;Ljava/lang/String;)V

    .line 1368
    new-instance v2, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v2, v1, v3}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 1369
    new-instance v1, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {v1, v2}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 1370
    new-instance v2, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v2, v1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    const/4 v1, 0x0

    .line 1374
    :try_start_0
    new-instance v3, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v3, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 1375
    invoke-interface {v2, v3}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    .line 1376
    sget-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v0, :cond_0

    .line 1393
    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setCSATState(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/states/ConversationCSATState;)V

    :cond_0
    return-void

    :catchall_0
    move-exception v0

    goto :goto_1

    :catch_0
    move-exception v0

    .line 1381
    :try_start_1
    iget-object v2, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v3, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq v2, v3, :cond_1

    iget-object v2, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v3, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-eq v2, v3, :cond_1

    .line 1385
    iget-object v2, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v3, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne v2, v3, :cond_2

    .line 1386
    sget-object v2, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    move-object v1, v2

    goto :goto_0

    .line 1383
    :cond_1
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v2

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v4, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {v2, v3, v4}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 1388
    :cond_2
    :goto_0
    throw v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :goto_1
    if-eqz v1, :cond_3

    .line 1393
    invoke-direct {p0, p1, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setCSATState(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/states/ConversationCSATState;)V

    .line 1395
    :cond_3
    throw v0
.end method

.method public sendConversationEndedDelegate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 1

    .line 934
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isConversationEndedDelegateSent:Z

    if-nez v0, :cond_0

    .line 936
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->conversationEnded()V

    const/4 v0, 0x1

    .line 938
    iput-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isConversationEndedDelegateSent:Z

    .line 939
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v0, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    :cond_0
    return-void
.end method

.method public sendConversationEndedDelegateForPreIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 1851
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v1, :cond_0

    .line 1852
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->handleConversationEnded(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    :cond_0
    return-void
.end method

.method public sendConversationPostedEvent(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 1767
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/analytics/AnalyticsEventType;->CONVERSATION_POSTED:Lcom/helpshift/analytics/AnalyticsEventType;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/lang/String;)V

    return-void
.end method

.method public sendConversationPostedEventOnPreIssueUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 1761
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->COMPLETED_ISSUE_CREATED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v1, :cond_0

    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v0, p1, :cond_0

    .line 1762
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendConversationPostedEvent(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    :cond_0
    return-void
.end method

.method public sendOptionInputMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;Z)V
    .locals 10

    .line 1662
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v0

    .line 1663
    iget-object v1, v0, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v4, v1

    check-cast v4, Ljava/lang/String;

    .line 1664
    iget-object v0, v0, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v0, Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    if-eqz p4, :cond_0

    .line 1668
    iget-object p3, p2, Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iget-object p3, p3, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->skipLabel:Ljava/lang/String;

    :goto_0
    move-object v3, p3

    goto :goto_1

    .line 1672
    :cond_0
    iget-object p3, p3, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->title:Ljava/lang/String;

    goto :goto_0

    .line 1675
    :goto_1
    new-instance p3, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;

    const-string v7, "mobile"

    move-object v2, p3

    move-object v8, p2

    move v9, p4

    invoke-direct/range {v2 .. v9}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;Z)V

    .line 1678
    iget-object p4, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object p4, p3, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->conversationLocalId:Ljava/lang/Long;

    const/4 p4, 0x1

    .line 1682
    invoke-virtual {p3, p4}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;->updateState(Z)V

    .line 1683
    invoke-direct {p0, p1, p3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDbAndUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 1684
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->deleteOptionsForAdminMessageWithOptionsInput(Lcom/helpshift/conversation/activeconversation/message/OptionInputMessageDM;)V

    .line 1685
    invoke-direct {p0, p1, p3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendTextMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;)V

    return-void
.end method

.method public sendScreenshot(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
    .locals 18

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move-object/from16 v2, p2

    move-object/from16 v3, p3

    .line 1296
    iget-object v4, v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v4}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v4

    .line 1297
    iget-object v5, v4, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v8, v5

    check-cast v8, Ljava/lang/String;

    .line 1298
    iget-object v4, v4, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v4, Ljava/lang/Long;

    invoke-virtual {v4}, Ljava/lang/Long;->longValue()J

    move-result-wide v9

    .line 1299
    new-instance v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    const-string v11, "mobile"

    const/4 v7, 0x0

    const/4 v12, 0x0

    const/4 v13, 0x0

    const/4 v14, 0x0

    const/4 v15, 0x0

    const/16 v16, 0x0

    const/16 v17, 0x0

    move-object v6, v4

    invoke-direct/range {v6 .. v17}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;IZ)V

    .line 1308
    iget-object v5, v2, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileName:Ljava/lang/String;

    iput-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->fileName:Ljava/lang/String;

    .line 1309
    iget-object v5, v2, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    iput-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->filePath:Ljava/lang/String;

    .line 1310
    invoke-virtual {v4, v3}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setRefersMessageId(Ljava/lang/String;)V

    .line 1311
    invoke-virtual/range {p0 .. p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldEnableMessagesClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v5

    invoke-virtual {v4, v5}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->updateState(Z)V

    .line 1312
    iget-object v5, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 1313
    invoke-direct {v0, v1, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDbAndUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    const/4 v5, 0x1

    if-eqz v3, :cond_1

    .line 1315
    iget-object v6, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v6}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :cond_0
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_1

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1316
    iget-object v8, v7, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    if-eqz v8, :cond_0

    iget-object v8, v7, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    .line 1317
    invoke-virtual {v8, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_0

    iget-object v8, v7, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v9, Lcom/helpshift/conversation/activeconversation/message/MessageType;->REQUESTED_SCREENSHOT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v8, v9, :cond_0

    .line 1319
    check-cast v7, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;

    .line 1320
    iget-object v3, v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v7, v3, v5}, Lcom/helpshift/conversation/activeconversation/message/RequestScreenshotMessageDM;->setIsAnswered(Lcom/helpshift/common/platform/Platform;Z)V

    .line 1325
    :cond_1
    iget-boolean v2, v2, Lcom/helpshift/conversation/dto/ImagePickerFile;->isFileCompressionAndCopyingDone:Z

    xor-int/2addr v2, v5

    invoke-direct {v0, v1, v4, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendScreenshotMessageInternal(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;Z)V

    return-void
.end method

.method public sendTextMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;)V
    .locals 8

    .line 979
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v0

    .line 980
    iget-object v1, v0, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v4, v1

    check-cast v4, Ljava/lang/String;

    .line 981
    iget-object v0, v0, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v0, Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    .line 982
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    const-string v7, "mobile"

    move-object v2, v0

    move-object v3, p2

    invoke-direct/range {v2 .. v7}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    .line 983
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object p2, v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 984
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldEnableMessagesClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result p2

    invoke-virtual {v0, p2}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->updateState(Z)V

    .line 987
    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDbAndUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 990
    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendTextMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;)V

    return-void
.end method

.method public sendTextMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;Z)V
    .locals 10

    .line 1019
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v0

    .line 1020
    iget-object v1, v0, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    move-object v4, v1

    check-cast v4, Ljava/lang/String;

    .line 1021
    iget-object v0, v0, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v0, Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    .line 1022
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;

    const-string v7, "mobile"

    move-object v2, v0

    move-object v3, p2

    move-object v8, p3

    move v9, p4

    invoke-direct/range {v2 .. v9}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;-><init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithTextInputDM;Z)V

    .line 1024
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object p2, v0, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->conversationLocalId:Ljava/lang/Long;

    const/4 p2, 0x1

    .line 1028
    invoke-virtual {v0, p2}, Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;->updateState(Z)V

    .line 1029
    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->addMessageToDbAndUI(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 1030
    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendTextMessage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;)V

    return-void
.end method

.method public setEnableMessageClickOnResolutionRejected(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
    .locals 1

    .line 895
    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->enableMessageClickOnResolutionRejected:Z

    .line 896
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p2, v0, :cond_0

    .line 897
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessagesOnIssueStatusUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    :cond_0
    return-void
.end method

.method public setShouldIncrementMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZZ)V
    .locals 1

    .line 1419
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->shouldIncrementMessageCount:Z

    if-eq v0, p2, :cond_0

    .line 1420
    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->shouldIncrementMessageCount:Z

    if-eqz p3, :cond_0

    .line 1422
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p2, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    :cond_0
    return-void
.end method

.method public setStartNewConversationButtonClicked(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZZ)V
    .locals 0

    .line 1411
    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    if-eqz p3, :cond_0

    .line 1413
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p2, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    :cond_0
    return-void
.end method

.method public shouldEnableMessagesClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 3

    .line 1858
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInBetweenBotExecution:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 1864
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v1, 0x1

    goto :goto_0

    .line 1867
    :cond_1
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v0, v2, :cond_3

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v0, v2, :cond_3

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v0, v2, :cond_3

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v2, :cond_2

    goto :goto_0

    .line 1873
    :cond_2
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v2, :cond_3

    .line 1874
    iget-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->enableMessageClickOnResolutionRejected:Z

    :cond_3
    :goto_0
    return v1
.end method

.method public shouldOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 4

    .line 1518
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "conversationalIssueFiling"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 1519
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 1520
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    return v1

    .line 1527
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    const/4 v2, 0x1

    if-eqz v0, :cond_1

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v0

    if-eqz v0, :cond_1

    return v2

    .line 1532
    :cond_1
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 1534
    iget-boolean v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    if-eqz v3, :cond_2

    goto :goto_2

    .line 1538
    :cond_2
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v3

    if-nez v3, :cond_8

    sget-object v3, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v3, :cond_3

    goto :goto_1

    .line 1541
    :cond_3
    sget-object v3, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v0, v3, :cond_7

    sget-object v3, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v0, v3, :cond_7

    sget-object v3, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v3, :cond_4

    goto :goto_0

    .line 1546
    :cond_4
    sget-object v3, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v3, :cond_9

    .line 1551
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    if-eqz v0, :cond_5

    goto :goto_2

    .line 1554
    :cond_5
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    if-nez v0, :cond_6

    goto :goto_1

    .line 1564
    :cond_6
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-static {v0, p1}, Lcom/helpshift/conversation/ConversationUtil;->getUserMessageCountForConversationLocalId(Lcom/helpshift/conversation/dao/ConversationDAO;Ljava/lang/Long;)I

    move-result p1

    if-lez p1, :cond_9

    goto :goto_1

    .line 1544
    :cond_7
    :goto_0
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    xor-int/lit8 v1, p1, 0x1

    goto :goto_2

    :cond_8
    :goto_1
    const/4 v1, 0x1

    :cond_9
    :goto_2
    return v1
.end method

.method public shouldShowCSATInFooter(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 2

    .line 902
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 905
    :cond_0
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    sget-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->NONE:Lcom/helpshift/conversation/states/ConversationCSATState;

    if-ne p1, v0, :cond_1

    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v0, "customerSatisfactionSurvey"

    .line 906
    invoke-virtual {p1, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 v1, 0x1

    :cond_1
    return v1
.end method

.method updateAcceptedRequestForReopenMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2

    .line 705
    instance-of v0, p2, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    if-eqz v0, :cond_0

    .line 706
    move-object v0, p2

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    .line 708
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->isAnswered()Z

    move-result v1

    if-nez v1, :cond_1

    .line 709
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->unansweredRequestForReopenMessageDMs:Ljava/util/Map;

    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-interface {p1, p2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 712
    :cond_0
    instance-of v0, p2, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    if-eqz v0, :cond_1

    .line 713
    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;

    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;->referredMessageId:Ljava/lang/String;

    .line 714
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->unansweredRequestForReopenMessageDMs:Ljava/util/Map;

    invoke-interface {v0, p2}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 715
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->unansweredRequestForReopenMessageDMs:Ljava/util/Map;

    .line 716
    invoke-interface {v0, p2}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    .line 717
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p2, v0, v1}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 718
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    iput-boolean p1, p2, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->shouldShowAgentNameForConversation:Z

    const/4 p1, 0x1

    .line 719
    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;->setAnsweredAndNotify(Z)V

    .line 720
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p1, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    :cond_1
    :goto_0
    return-void
.end method

.method public updateIsAutoFilledPreissueFlag(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
    .locals 0

    .line 1900
    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isAutoFilledPreIssue:Z

    .line 1901
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p2, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    return-void
.end method

.method public updateIssueStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V
    .locals 3

    .line 405
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, p2, :cond_0

    return-void

    :cond_0
    const-string v0, "Helpshift_ConvManager"

    .line 408
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Changing conversation status from: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", new status: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", for: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 410
    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 412
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->refreshConversationOnIssueStateUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 415
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p2, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 418
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->conversationDMListener:Lcom/helpshift/conversation/activeconversation/ConversationDMListener;

    if-eqz p2, :cond_1

    .line 419
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->conversationDMListener:Lcom/helpshift/conversation/activeconversation/ConversationDMListener;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    invoke-interface {p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationDMListener;->onIssueStatusChange(Lcom/helpshift/conversation/dto/IssueState;)V

    :cond_1
    return-void
.end method

.method public updateLastUserActivityTime(Lcom/helpshift/conversation/activeconversation/model/Conversation;J)V
    .locals 1

    .line 1428
    iput-wide p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->lastUserActivityTime:J

    .line 1429
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v0, p1, p2, p3}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateLastUserActivityTimeInConversation(Ljava/lang/Long;J)V

    return-void
.end method

.method public updateMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLjava/util/List;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Z",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;",
            "Lcom/helpshift/conversation/activeconversation/ConversationUpdate;",
            ")V"
        }
    .end annotation

    if-nez p4, :cond_0

    .line 521
    new-instance p4, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;

    invoke-direct {p4}, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;-><init>()V

    .line 525
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 526
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 527
    invoke-direct {p0, p1, v0, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->populateMessageDMLookup(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Map;Ljava/util/Map;)V

    .line 530
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 531
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    .line 533
    invoke-interface {p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p3

    :goto_0
    invoke-interface {p3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_6

    invoke-interface {p3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 534
    invoke-direct {p0, v4, v0, v1, p4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getMessageDMForUpdate(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Ljava/util/Map;Ljava/util/Map;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v5

    if-eqz v5, :cond_5

    .line 538
    instance-of v6, v5, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v6, :cond_1

    .line 539
    invoke-virtual {v5, v4}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 540
    move-object v4, v5

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v4, v6}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    goto :goto_1

    .line 542
    :cond_1
    instance-of v6, v5, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    if-eqz v6, :cond_2

    .line 543
    invoke-virtual {v5, v4}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 544
    move-object v4, v5

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    sget-object v6, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v4, v6}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    .line 546
    iget-boolean v4, v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isRedacted:Z

    if-eqz v4, :cond_4

    .line 547
    move-object v4, v5

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 550
    :cond_2
    instance-of v6, v5, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    if-eqz v6, :cond_3

    .line 551
    invoke-virtual {v5, v4}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->mergeAndNotify(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 553
    iget-boolean v4, v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isRedacted:Z

    if-eqz v4, :cond_4

    .line 554
    move-object v4, v5

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 558
    :cond_3
    invoke-virtual {v5, v4}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->mergeAndNotify(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 560
    :cond_4
    :goto_1
    iget-object v4, p4, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->updatedMessageDMs:Ljava/util/List;

    invoke-interface {v4, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 565
    :cond_5
    invoke-interface {v2, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 570
    :cond_6
    invoke-direct {p0, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->clearRedactedAttachmentsResources(Ljava/util/List;)V

    .line 573
    invoke-static {v2}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result p3

    if-eqz p3, :cond_7

    return-void

    .line 578
    :cond_7
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p3

    :goto_2
    invoke-interface {p3}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_a

    invoke-interface {p3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 579
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, v1, v3}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 580
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 581
    iget-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->shouldShowAgentNameForConversation:Z

    .line 592
    instance-of v1, v0, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v1, :cond_8

    .line 593
    move-object v1, v0

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    sget-object v3, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v1, v3}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    goto :goto_3

    .line 595
    :cond_8
    instance-of v1, v0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    if-eqz v1, :cond_9

    .line 596
    move-object v1, v0

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    sget-object v3, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    invoke-virtual {v1, v3}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->setState(Lcom/helpshift/conversation/activeconversation/message/UserMessageState;)V

    .line 599
    :cond_9
    :goto_3
    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->addObserver(Ljava/util/Observer;)V

    goto :goto_2

    :cond_a
    if-eqz p2, :cond_c

    .line 604
    invoke-static {v2}, Lcom/helpshift/conversation/ConversationUtil;->sortMessagesBasedOnCreatedAt(Ljava/util/List;)V

    .line 605
    iget-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInBetweenBotExecution:Z

    .line 606
    invoke-virtual {p0, v2, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->evaluateBotExecutionState(Ljava/util/List;Z)Z

    move-result p2

    iput-boolean p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInBetweenBotExecution:Z

    .line 609
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p2, v2}, Lcom/helpshift/common/util/HSObservableList;->addAll(Ljava/util/Collection;)Z

    .line 613
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_4
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result p3

    if-eqz p3, :cond_d

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 615
    instance-of v0, p3, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    if-eqz v0, :cond_b

    .line 616
    move-object v0, p3

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/message/AdminImageAttachmentMessageDM;->downloadThumbnailImage(Lcom/helpshift/common/platform/Platform;)V

    .line 618
    :cond_b
    invoke-virtual {p0, p1, p3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateAcceptedRequestForReopenMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto :goto_4

    .line 622
    :cond_c
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p2, v2}, Lcom/helpshift/common/util/HSObservableList;->addAll(Ljava/util/Collection;)Z

    .line 626
    :cond_d
    iget-object p2, p4, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->newMessageDMs:Ljava/util/List;

    invoke-interface {p2, v2}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 627
    invoke-direct {p0, p1, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->evaluateBotControlMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Collection;)V

    return-void
.end method

.method updateMessageOnConversationUpdate(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)V
    .locals 0

    .line 300
    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageClickableState(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)V

    .line 301
    instance-of p2, p1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    if-eqz p2, :cond_0

    .line 302
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    .line 303
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->checkAndReDownloadImageIfNotExist(Lcom/helpshift/common/platform/Platform;)V

    :cond_0
    return-void
.end method

.method public updateMessagesClickOnBotSwitch(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
    .locals 1

    .line 1696
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p1}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 1697
    invoke-direct {p0, v0, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageClickableState(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method updateMessagesOnIssueStatusUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 291
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldEnableMessagesClick(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v0

    .line 292
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p1}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 293
    invoke-virtual {p0, v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageOnConversationUpdate(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public updateStateBasedOnMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 3

    .line 1881
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v1, :cond_3

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    if-eqz v0, :cond_3

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 1882
    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v0

    if-lez v0, :cond_3

    .line 1883
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    const/4 v1, 0x0

    :goto_0
    if-ltz v0, :cond_1

    .line 1885
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 1886
    invoke-virtual {v1, v0}, Lcom/helpshift/common/util/HSObservableList;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    instance-of v2, v1, Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;

    if-nez v2, :cond_0

    instance-of v2, v1, Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;

    if-eqz v2, :cond_1

    :cond_0
    add-int/lit8 v0, v0, -0x1

    goto :goto_0

    .line 1890
    :cond_1
    instance-of v0, v1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    if-eqz v0, :cond_2

    .line 1891
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    goto :goto_1

    .line 1893
    :cond_2
    instance-of v0, v1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    if-eqz v0, :cond_3

    .line 1894
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    :cond_3
    :goto_1
    return-void
.end method
