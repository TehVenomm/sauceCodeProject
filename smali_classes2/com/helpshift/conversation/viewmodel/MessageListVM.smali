.class public Lcom/helpshift/conversation/viewmodel/MessageListVM;
.super Ljava/lang/Object;
.source "MessageListVM.java"


# instance fields
.field protected final domain:Lcom/helpshift/common/domain/Domain;

.field messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

.field protected final platform:Lcom/helpshift/common/platform/Platform;

.field private timeZoneOffSet:J

.field private uiConversationLocalIdMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Lcom/helpshift/conversation/activeconversation/UIConversation;",
            ">;"
        }
    .end annotation
.end field

.field uiMessageDMs:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;)V
    .locals 1

    .line 46
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 42
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentHashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiConversationLocalIdMap:Ljava/util/Map;

    .line 47
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 48
    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 49
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getTimeZoneOffSet()J

    move-result-wide p1

    iput-wide p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->timeZoneOffSet:J

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/conversation/viewmodel/MessageListVM;I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 0

    .line 36
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V
    .locals 0

    .line 36
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->appendUIMessages(Ljava/util/List;)V

    return-void
.end method

.method static synthetic access$200(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V
    .locals 0

    .line 36
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertUIMessages(Ljava/util/List;)V

    return-void
.end method

.method static synthetic access$300(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V
    .locals 0

    .line 36
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyNewUIMessagesAdded(Ljava/util/List;)V

    return-void
.end method

.method static synthetic access$400(Lcom/helpshift/conversation/viewmodel/MessageListVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 0

    .line 36
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isSystemMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p0

    return p0
.end method

.method static synthetic access$500(Lcom/helpshift/conversation/viewmodel/MessageListVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Ljava/util/List;
    .locals 0

    .line 36
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->buildSystemMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Ljava/util/List;

    move-result-object p0

    return-object p0
.end method

.method private appendUIMessages(Ljava/util/List;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 527
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v1, v0, -0x1

    .line 528
    invoke-direct {p0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v2

    const/4 v3, 0x1

    .line 529
    invoke-direct {p0, p1, v2, v3}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertSystemMessageDMs(Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)Ljava/util/List;

    move-result-object p1

    .line 531
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v2, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 532
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    iget-object v4, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    .line 533
    invoke-interface {v4}, Ljava/util/List;->size()I

    move-result v4

    sub-int/2addr v4, v3

    invoke-virtual {p0, v2, v1, v4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByTime(Ljava/util/List;II)Lcom/helpshift/util/ValuePair;

    move-result-object v1

    .line 534
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    if-eqz v2, :cond_0

    .line 535
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result p1

    invoke-interface {v2, v0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->appendMessages(II)V

    if-eqz v1, :cond_0

    .line 537
    iget-object p1, v1, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    if-ge p1, v0, :cond_0

    .line 539
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    sub-int/2addr v0, p1

    invoke-interface {v1, p1, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->updateMessages(II)V

    :cond_0
    return-void
.end method

.method private buildSystemDividerMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;
    .locals 3

    const/4 v0, 0x0

    if-eqz p1, :cond_4

    if-nez p2, :cond_0

    goto :goto_2

    .line 245
    :cond_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isDifferentConversationMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v1

    if-nez v1, :cond_1

    return-object v0

    .line 259
    :cond_1
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    invoke-virtual {p0, v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getUIConversation(J)Lcom/helpshift/conversation/activeconversation/UIConversation;

    move-result-object v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-eqz v0, :cond_2

    .line 260
    iget-boolean v0, v0, Lcom/helpshift/conversation/activeconversation/UIConversation;->isRedacted:Z

    if-eqz v0, :cond_2

    const/4 v0, 0x1

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    :goto_0
    if-nez v0, :cond_3

    .line 268
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getIssueStateForMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Lcom/helpshift/conversation/dto/IssueState;

    move-result-object p1

    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p1, v0, :cond_3

    goto :goto_1

    :cond_3
    const/4 v1, 0x0

    .line 270
    :goto_1
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getDateFromMessageDM(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/util/Date;

    move-result-object p1

    invoke-direct {p0, p1, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->generateConversationDividerMessage(Ljava/util/Date;Z)Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;

    move-result-object p1

    .line 271
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;->conversationLocalId:Ljava/lang/Long;

    return-object p1

    :cond_4
    :goto_2
    return-object v0
.end method

.method private buildSystemMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Ljava/util/List;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            "ZZ)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;",
            ">;"
        }
    .end annotation

    if-nez p2, :cond_0

    .line 292
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    return-object p1

    .line 294
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 296
    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->buildSystemDividerMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 298
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 301
    :cond_1
    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isDifferentConversationMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v1

    .line 302
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getDateFromMessageDM(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/util/Date;

    move-result-object v2

    .line 303
    iget-object v3, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-virtual {p0, v3, v4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getUIConversation(J)Lcom/helpshift/conversation/activeconversation/UIConversation;

    move-result-object v3

    const/4 v4, 0x1

    const/4 v5, 0x0

    if-eqz v3, :cond_2

    .line 304
    iget-boolean v6, v3, Lcom/helpshift/conversation/activeconversation/UIConversation;->isRedacted:Z

    if-eqz v6, :cond_2

    const/4 v6, 0x1

    goto :goto_0

    :cond_2
    const/4 v6, 0x0

    :goto_0
    if-nez v6, :cond_7

    if-nez v1, :cond_4

    if-eqz p3, :cond_3

    goto :goto_1

    .line 342
    :cond_3
    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isMessagesDayDifferent(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    if-eqz p1, :cond_7

    .line 343
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isSystemDateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    if-nez p1, :cond_7

    .line 344
    iget-object p1, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-direct {p0, v2, p3, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->generateSystemDateMessageDM(Ljava/util/Date;ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    move-result-object p1

    .line 347
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 348
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_3

    :cond_4
    :goto_1
    if-eqz v3, :cond_6

    if-eqz p4, :cond_5

    .line 318
    iget-boolean p1, v3, Lcom/helpshift/conversation/activeconversation/UIConversation;->isInPreIssueMode:Z

    if-nez p1, :cond_5

    iget-object p1, v3, Lcom/helpshift/conversation/activeconversation/UIConversation;->publishId:Ljava/lang/String;

    .line 320
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_5

    goto :goto_2

    :cond_5
    const/4 v4, 0x0

    :goto_2
    if-eqz v4, :cond_6

    .line 323
    iget-object p1, v3, Lcom/helpshift/conversation/activeconversation/UIConversation;->publishId:Ljava/lang/String;

    iget-object p4, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 324
    invoke-direct {p0, p1, v2, p3, p4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->generateSystemPublishIdMessageDM(Ljava/lang/String;Ljava/util/Date;ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;

    move-result-object p1

    .line 328
    iget-object p3, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    iput-object p3, p1, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 329
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const/4 p3, 0x0

    .line 336
    :cond_6
    iget-object p1, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-direct {p0, v2, p3, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->generateSystemDateMessageDM(Ljava/util/Date;ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    move-result-object p1

    .line 339
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    iput-object p2, p1, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 340
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_7
    :goto_3
    return-object v0
.end method

.method private canGroupMessagesByTime(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 5

    const/4 v0, 0x0

    if-eqz p1, :cond_a

    if-nez p2, :cond_0

    goto :goto_1

    .line 791
    :cond_0
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    return v0

    .line 796
    :cond_1
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isUserMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-direct {p0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isUserMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v1

    if-nez v1, :cond_3

    .line 797
    :cond_2
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isAdminMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v1

    if-eqz v1, :cond_9

    invoke-direct {p0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isAdminMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v1

    if-nez v1, :cond_3

    goto :goto_0

    .line 802
    :cond_3
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v1

    .line 803
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v3

    .line 802
    invoke-direct {p0, v1, v2, v3, v4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isMinuteDifferent(JJ)Z

    move-result v1

    if-eqz v1, :cond_4

    return v0

    .line 808
    :cond_4
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isUserMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v1

    const/4 v2, 0x1

    if-eqz v1, :cond_6

    .line 809
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isUserMessageSent(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    if-eqz p1, :cond_5

    invoke-direct {p0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isUserMessageSent(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    if-eqz p1, :cond_5

    const/4 v0, 0x1

    :cond_5
    return v0

    .line 813
    :cond_6
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getDisplayedAuthorName()Ljava/lang/String;

    move-result-object p1

    .line 814
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getDisplayedAuthorName()Ljava/lang/String;

    move-result-object p2

    if-nez p1, :cond_8

    if-nez p2, :cond_7

    const/4 v0, 0x1

    :cond_7
    return v0

    .line 820
    :cond_8
    invoke-virtual {p1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1

    :cond_9
    :goto_0
    return v0

    :cond_a
    :goto_1
    return v0
.end method

.method private containsAdminMessages(Ljava/util/List;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)Z"
        }
    .end annotation

    .line 572
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 573
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isAdminMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x1

    return p1

    :cond_1
    const/4 p1, 0x0

    return p1
.end method

.method private declared-synchronized filterUIMessages(Ljava/util/Collection;)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 157
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez p1, :cond_0

    .line 159
    monitor-exit p0

    return-object v0

    .line 162
    :cond_0
    :try_start_1
    invoke-interface {p1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 163
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isUISupportedMessage()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 164
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    .line 167
    :cond_2
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    .line 156
    monitor-exit p0

    throw p1
.end method

.method private findBinaryIndex(JII)I
    .locals 5

    sub-int v0, p4, p3

    .line 726
    div-int/lit8 v0, v0, 0x2

    add-int/2addr v0, p3

    if-ne p3, v0, :cond_2

    .line 728
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0, p3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v0

    .line 729
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v2, p4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v2

    cmp-long v4, p1, v0

    if-gez v4, :cond_0

    return p3

    :cond_0
    cmp-long p3, p1, v2

    if-ltz p3, :cond_1

    add-int/lit8 p4, p4, 0x1

    return p4

    :cond_1
    return p4

    .line 741
    :cond_2
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v1

    cmp-long v3, v1, p1

    if-gtz v3, :cond_3

    .line 743
    invoke-direct {p0, p1, p2, v0, p4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->findBinaryIndex(JII)I

    move-result p1

    return p1

    .line 746
    :cond_3
    invoke-direct {p0, p1, p2, p3, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->findBinaryIndex(JII)I

    move-result p1

    return p1
.end method

.method private findBinaryIndexIntoUIList(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)I
    .locals 4

    .line 709
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 715
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v2

    add-int/lit8 p1, v0, -0x1

    invoke-direct {p0, v2, v3, v1, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->findBinaryIndex(JII)I

    move-result p1

    if-gez p1, :cond_1

    const/4 p1, 0x0

    goto :goto_0

    :cond_1
    if-le p1, v0, :cond_2

    move p1, v0

    :cond_2
    :goto_0
    return p1
.end method

.method private generateConversationDividerMessage(Ljava/util/Date;Z)Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;
    .locals 3

    .line 371
    new-instance v0, Ljava/util/Date;

    invoke-virtual {p1}, Ljava/util/Date;->getTime()J

    move-result-wide v1

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    .line 372
    sget-object p1, Lcom/helpshift/common/util/HSDateFormatSpec;->STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

    invoke-virtual {p1, v0}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object p1

    .line 373
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;

    .line 374
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getEpochTimeForSystemMessage(Ljava/lang/String;)J

    move-result-wide v1

    invoke-direct {v0, p1, v1, v2, p2}, Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;-><init>(Ljava/lang/String;JZ)V

    .line 375
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    return-object v0
.end method

.method private generateSystemDateMessageDM(Ljava/util/Date;ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;
    .locals 3

    .line 359
    new-instance v0, Ljava/util/Date;

    invoke-virtual {p1}, Ljava/util/Date;->getTime()J

    move-result-wide v1

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    .line 360
    sget-object p1, Lcom/helpshift/common/util/HSDateFormatSpec;->STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

    invoke-virtual {p1, v0}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object p1

    .line 361
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    .line 362
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getEpochTimeForSystemMessage(Ljava/lang/String;)J

    move-result-wide v1

    invoke-direct {v0, p1, v1, v2, p2}, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;-><init>(Ljava/lang/String;JZ)V

    .line 364
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 365
    iput-object p3, v0, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;->conversationLocalId:Ljava/lang/Long;

    return-object v0
.end method

.method private generateSystemPublishIdMessageDM(Ljava/lang/String;Ljava/util/Date;ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;
    .locals 7

    .line 382
    new-instance v0, Ljava/util/Date;

    invoke-virtual {p2}, Ljava/util/Date;->getTime()J

    move-result-wide v1

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    .line 383
    sget-object p2, Lcom/helpshift/common/util/HSDateFormatSpec;->STORAGE_TIME_FORMAT:Lcom/helpshift/common/util/HSSimpleDateFormat;

    invoke-virtual {p2, v0}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v3

    .line 384
    new-instance p2, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;

    .line 385
    invoke-direct {p0, v3}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getEpochTimeForSystemMessage(Ljava/lang/String;)J

    move-result-wide v4

    move-object v1, p2

    move-object v2, p1

    move v6, p3

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;JZ)V

    .line 387
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object p3, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p2, p1, p3}, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 388
    iput-object p4, p2, Lcom/helpshift/conversation/activeconversation/message/SystemPublishIdMessageDM;->conversationLocalId:Ljava/lang/Long;

    return-object p2
.end method

.method private getDateFromMessageDM(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Ljava/util/Date;
    .locals 3

    .line 398
    new-instance v0, Ljava/util/Date;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v1

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    return-object v0
.end method

.method private getEpochTimeForSystemMessage(Ljava/lang/String;)J
    .locals 4

    .line 394
    invoke-static {p1}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v0

    const-wide/16 v2, 0x1

    sub-long/2addr v0, v2

    return-wide v0
.end method

.method private getIssueStateForMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Lcom/helpshift/conversation/dto/IssueState;
    .locals 2

    if-nez p1, :cond_0

    .line 195
    sget-object p1, Lcom/helpshift/conversation/dto/IssueState;->UNKNOWN:Lcom/helpshift/conversation/dto/IssueState;

    return-object p1

    .line 197
    :cond_0
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    invoke-virtual {p0, v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getUIConversation(J)Lcom/helpshift/conversation/activeconversation/UIConversation;

    move-result-object p1

    if-nez p1, :cond_1

    .line 199
    sget-object p1, Lcom/helpshift/conversation/dto/IssueState;->UNKNOWN:Lcom/helpshift/conversation/dto/IssueState;

    return-object p1

    .line 201
    :cond_1
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/UIConversation;->issueState:Lcom/helpshift/conversation/dto/IssueState;

    return-object p1
.end method

.method private getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 1

    if-ltz p1, :cond_1

    .line 702
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-lt p1, v0, :cond_0

    goto :goto_0

    .line 705
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    return-object p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return-object p1
.end method

.method private getSortMessagesComparator()Ljava/util/Comparator;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Comparator<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 437
    new-instance v0, Lcom/helpshift/conversation/viewmodel/MessageListVM$1;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM$1;-><init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;)V

    return-object v0
.end method

.method private declared-synchronized groupConversationRedactedMessages(Ljava/util/List;)Ljava/util/List;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 118
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 119
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v1, :cond_0

    .line 120
    monitor-exit p0

    return-object v0

    .line 125
    :cond_0
    :try_start_1
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 v1, 0x0

    const/4 v2, 0x0

    move-object v3, v1

    const/4 v4, 0x0

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_3

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 126
    instance-of v6, v5, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;

    if-eqz v6, :cond_1

    add-int/lit8 v4, v4, 0x1

    .line 128
    check-cast v5, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;

    move-object v3, v5

    goto :goto_0

    :cond_1
    if-eqz v3, :cond_2

    .line 132
    iput v4, v3, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;->contiguousRedactedConversationsCount:I

    .line 133
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    move-object v3, v1

    const/4 v4, 0x0

    .line 139
    :cond_2
    invoke-interface {v0, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_3
    if-eqz v3, :cond_4

    .line 144
    iput v4, v3, Lcom/helpshift/conversation/activeconversation/message/SystemRedactedConversationMessageDM;->contiguousRedactedConversationsCount:I

    .line 145
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 147
    :cond_4
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    .line 117
    monitor-exit p0

    throw p1
.end method

.method private declared-synchronized insertSystemMessageDMs(Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            "Z)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 209
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 211
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 212
    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v1

    const-string v2, "showConversationInfoScreen"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    const/4 v2, 0x0

    if-nez p3, :cond_0

    if-nez p2, :cond_0

    const/4 p3, 0x1

    goto :goto_0

    :cond_0
    const/4 p3, 0x0

    .line 216
    :goto_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 218
    invoke-direct {p0, p2, v3, p3, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->buildSystemMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Ljava/util/List;

    move-result-object p2

    .line 222
    invoke-interface {v0, p2}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 225
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-object p2, v3

    const/4 p3, 0x0

    goto :goto_1

    .line 229
    :cond_1
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    .line 208
    monitor-exit p0

    throw p1
.end method

.method private insertSystemMessageDMsForLatestEmptyConversation(Ljava/util/List;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/UIConversation;",
            ">;)V"
        }
    .end annotation

    .line 78
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 82
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    const/4 v2, 0x1

    sub-int/2addr v1, v2

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 83
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 84
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v3

    sub-int/2addr v3, v2

    invoke-interface {p1, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/UIConversation;

    .line 85
    iget-wide v3, p1, Lcom/helpshift/conversation/activeconversation/UIConversation;->localID:J

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    .line 89
    invoke-virtual {v3, v1}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_3

    .line 90
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-virtual {p0, v3, v4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getUIConversation(J)Lcom/helpshift/conversation/activeconversation/UIConversation;

    move-result-object v1

    const/4 v3, 0x0

    if-eqz v1, :cond_1

    .line 91
    iget-boolean v1, v1, Lcom/helpshift/conversation/activeconversation/UIConversation;->isRedacted:Z

    if-eqz v1, :cond_1

    const/4 v1, 0x1

    goto :goto_0

    :cond_1
    const/4 v1, 0x0

    :goto_0
    if-nez v1, :cond_2

    .line 97
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getIssueStateForMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Lcom/helpshift/conversation/dto/IssueState;

    move-result-object v0

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v1, :cond_2

    goto :goto_1

    :cond_2
    const/4 v2, 0x0

    .line 99
    :goto_1
    new-instance v0, Ljava/util/Date;

    iget-wide v4, p1, Lcom/helpshift/conversation/activeconversation/UIConversation;->epochCreateTime:J

    invoke-direct {v0, v4, v5}, Ljava/util/Date;-><init>(J)V

    .line 100
    invoke-direct {p0, v0, v2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->generateConversationDividerMessage(Ljava/util/Date;Z)Lcom/helpshift/conversation/activeconversation/message/SystemDividerMessageDM;

    move-result-object v1

    .line 101
    iget-wide v4, p1, Lcom/helpshift/conversation/activeconversation/UIConversation;->localID:J

    invoke-static {v4, v5}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    invoke-direct {p0, v0, v3, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->generateSystemDateMessageDM(Ljava/util/Date;ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    move-result-object p1

    .line 102
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 103
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_3
    return-void
.end method

.method private insertUIMessages(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 551
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 552
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertUIMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto :goto_0

    .line 556
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyMessageListVMRefreshAll()V

    return-void
.end method

.method private isAdminMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 0

    .line 756
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isAdminMessage:Z

    return p1
.end method

.method private isDayDifferent(JJ)Z
    .locals 2

    .line 418
    iget-wide v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->timeZoneOffSet:J

    add-long/2addr p1, v0

    .line 419
    iget-wide v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->timeZoneOffSet:J

    add-long/2addr p3, v0

    const-wide/32 v0, 0x5265c00

    .line 421
    div-long/2addr p1, v0

    .line 422
    div-long/2addr p3, v0

    cmp-long v0, p1, p3

    if-eqz v0, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method private isDifferentConversationMessages(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 0

    if-eqz p1, :cond_1

    if-nez p2, :cond_0

    goto :goto_0

    .line 183
    :cond_0
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {p1, p2}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result p1

    xor-int/lit8 p1, p1, 0x1

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method

.method private isMessagesDayDifferent(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 2

    if-eqz p1, :cond_1

    if-nez p2, :cond_0

    goto :goto_0

    .line 414
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v0

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide p1

    invoke-direct {p0, v0, v1, p1, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isDayDifferent(JJ)Z

    move-result p1

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return p1
.end method

.method private isMinuteDifferent(JJ)Z
    .locals 2

    .line 427
    iget-wide v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->timeZoneOffSet:J

    add-long/2addr p1, v0

    .line 428
    iget-wide v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->timeZoneOffSet:J

    add-long/2addr p3, v0

    const-wide/32 v0, 0xea60

    .line 431
    div-long/2addr p1, v0

    .line 432
    div-long/2addr p3, v0

    cmp-long v0, p1, p3

    if-eqz v0, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method private isSystemDateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 0

    .line 406
    instance-of p1, p1, Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    return p1
.end method

.method private isSystemMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 0

    .line 402
    instance-of p1, p1, Lcom/helpshift/conversation/activeconversation/message/SystemMessageDM;

    return p1
.end method

.method private isUserMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 1

    .line 752
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isAdminMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v0

    if-nez v0, :cond_0

    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isSystemMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    if-nez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method private isUserMessageSent(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 4

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 766
    :cond_0
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v2, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_TEXT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    const/4 v3, 0x1

    if-eq v1, v2, :cond_4

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v2, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_TEXT_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-eq v1, v2, :cond_4

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v2, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v1, v2, :cond_1

    goto :goto_0

    .line 771
    :cond_1
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    sget-object v2, Lcom/helpshift/conversation/activeconversation/message/MessageType;->SCREENSHOT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    if-ne v1, v2, :cond_3

    .line 772
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->state:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    if-ne p1, v1, :cond_2

    const/4 v0, 0x1

    :cond_2
    return v0

    :cond_3
    return v0

    .line 769
    :cond_4
    :goto_0
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;->getState()Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    move-result-object p1

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageState;->SENT:Lcom/helpshift/conversation/activeconversation/message/UserMessageState;

    if-ne p1, v1, :cond_5

    const/4 v0, 0x1

    :cond_5
    return v0
.end method

.method private notifyNewUIMessagesAdded(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 515
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->containsAdminMessages(Ljava/util/List;)Z

    move-result p1

    .line 516
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    if-eqz v0, :cond_1

    if-eqz p1, :cond_0

    .line 518
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    invoke-interface {p1}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->newAdminMessagesAdded()V

    goto :goto_0

    .line 521
    :cond_0
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    invoke-interface {p1}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->newUserMessagesAdded()V

    :cond_1
    :goto_0
    return-void
.end method

.method private updateMessageGroupViewState(Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Z
    .locals 3

    .line 836
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    move-result-object v0

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz p2, :cond_1

    if-eqz p3, :cond_0

    .line 845
    new-instance p1, Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    invoke-direct {p1, v2, v1}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;-><init>(ZZ)V

    goto :goto_1

    .line 854
    :cond_0
    new-instance p2, Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isUserMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    invoke-direct {p2, v1, p1}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;-><init>(ZZ)V

    :goto_0
    move-object p1, p2

    goto :goto_1

    :cond_1
    if-eqz p3, :cond_2

    .line 865
    new-instance p2, Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isAdminMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    invoke-direct {p2, v2, p1}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;-><init>(ZZ)V

    goto :goto_0

    .line 872
    :cond_2
    new-instance p1, Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    invoke-direct {p1, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;-><init>(ZZ)V

    .line 877
    :goto_1
    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_3

    return v1

    .line 881
    :cond_3
    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->updateViewState(Lcom/helpshift/conversation/activeconversation/message/UIViewState;)V

    return v2
.end method


# virtual methods
.method public addMessages(Ljava/util/Collection;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 493
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->processAddedMessages(Ljava/util/Collection;)Ljava/util/List;

    move-result-object p1

    .line 495
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    if-lez v0, :cond_0

    .line 496
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM$2;-><init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method checkAndPrependSystemMessagesBeforeFirstMessage()V
    .locals 2

    .line 1060
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM$4;-><init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method checkAndUpdateGroupByDate(I)Z
    .locals 9

    .line 987
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v0

    add-int/lit8 v1, p1, 0x1

    .line 988
    invoke-direct {p0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v1

    .line 990
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isSystemDateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v0

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v0, :cond_1

    if-eqz v1, :cond_0

    .line 991
    invoke-direct {p0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isSystemDateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 992
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    add-int/lit8 p1, p1, -0x1

    const/4 v0, 0x1

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    .line 997
    :goto_0
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v1

    add-int/lit8 v4, p1, -0x1

    .line 998
    invoke-direct {p0, v4}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v4

    if-eqz v4, :cond_5

    if-eqz v1, :cond_5

    .line 1000
    invoke-direct {p0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isSystemDateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v5

    if-eqz v5, :cond_2

    goto :goto_1

    .line 1004
    :cond_2
    invoke-direct {p0, v4, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isMessagesDayDifferent(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v4

    if-eqz v4, :cond_4

    .line 1005
    new-instance v0, Ljava/util/Date;

    .line 1006
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v4

    invoke-direct {v0, v4, v5}, Ljava/util/Date;-><init>(J)V

    .line 1007
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v4

    const-wide/16 v6, -0x1

    cmp-long v8, v4, v6

    if-nez v8, :cond_3

    const/4 v2, 0x1

    :cond_3
    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 1006
    invoke-direct {p0, v0, v2, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->generateSystemDateMessageDM(Ljava/util/Date;ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/SystemDateMessageDM;

    move-result-object v0

    .line 1009
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v1, p1, v0}, Ljava/util/List;->add(ILjava/lang/Object;)V

    const/4 v0, 0x1

    :cond_4
    return v0

    :cond_5
    :goto_1
    return v0
.end method

.method declared-synchronized checkAndUpdateGroupByTime(Ljava/util/List;II)Lcom/helpshift/util/ValuePair;
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;II)",
            "Lcom/helpshift/util/ValuePair<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 914
    :try_start_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    const/4 v1, 0x0

    .line 915
    invoke-static {p2, v1}, Ljava/lang/Math;->max(II)I

    move-result p2

    const/4 v2, 0x1

    sub-int/2addr v0, v2

    .line 916
    invoke-static {p3, v0}, Ljava/lang/Math;->min(II)I

    move-result p3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v3, 0x0

    if-ge p3, p2, :cond_0

    .line 919
    monitor-exit p0

    return-object v3

    :cond_0
    const/4 v4, -0x1

    if-lez p2, :cond_2

    add-int/lit8 v5, p2, -0x1

    .line 927
    :try_start_1
    invoke-interface {p1, v5}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 930
    invoke-virtual {p0, v5}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isGroupFirstMessageAtIndex(I)Z

    move-result v7

    .line 931
    invoke-interface {p1, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-direct {p0, v6, v8}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->canGroupMessagesByTime(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v8

    xor-int/lit8 v9, v8, 0x1

    .line 932
    invoke-direct {p0, v6, v7, v9}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->updateMessageGroupViewState(Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Z

    move-result v6

    if-eqz v6, :cond_1

    move v6, v5

    goto :goto_0

    :cond_1
    const/4 v5, -0x1

    const/4 v6, -0x1

    :goto_0
    xor-int/lit8 v7, v8, 0x1

    goto :goto_1

    :cond_2
    const/4 v5, -0x1

    const/4 v6, -0x1

    const/4 v7, 0x1

    :goto_1
    if-gt p2, p3, :cond_7

    .line 942
    invoke-interface {p1, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    if-ne p2, v0, :cond_3

    .line 945
    invoke-direct {p0, v8, v7, v2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->updateMessageGroupViewState(Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Z

    move-result v8

    goto :goto_2

    :cond_3
    add-int/lit8 v9, p2, 0x1

    .line 950
    invoke-interface {p1, v9}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 951
    invoke-direct {p0, v8, v9}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->canGroupMessagesByTime(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result v9

    if-eqz v9, :cond_4

    .line 952
    invoke-direct {p0, v8, v7, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->updateMessageGroupViewState(Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Z

    move-result v8

    const/4 v7, 0x0

    goto :goto_2

    .line 959
    :cond_4
    invoke-direct {p0, v8, v7, v2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->updateMessageGroupViewState(Lcom/helpshift/conversation/activeconversation/message/MessageDM;ZZ)Z

    move-result v8

    const/4 v7, 0x1

    :goto_2
    if-eqz v8, :cond_6

    if-ne v5, v4, :cond_5

    move v5, p2

    :cond_5
    move v6, p2

    :cond_6
    add-int/lit8 p2, p2, 0x1

    goto :goto_1

    :cond_7
    if-eq v5, v4, :cond_8

    .line 974
    new-instance v3, Lcom/helpshift/util/ValuePair;

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-direct {v3, p1, p2}, Lcom/helpshift/util/ValuePair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 976
    :cond_8
    monitor-exit p0

    return-object v3

    :catchall_0
    move-exception p1

    .line 913
    monitor-exit p0

    throw p1
.end method

.method public copyOfUIMessageDMs()Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 1168
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    if-eqz v0, :cond_0

    .line 1169
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object v0

    .line 1172
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    return-object v0
.end method

.method public getLastUIMessage()Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 2

    .line 1176
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-lez v0, :cond_0

    .line 1178
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    add-int/lit8 v0, v0, -0x1

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    return-object v0

    :cond_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public declared-synchronized getUIConversation(J)Lcom/helpshift/conversation/activeconversation/UIConversation;
    .locals 1

    monitor-enter p0

    .line 1149
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiConversationLocalIdMap:Ljava/util/Map;

    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/UIConversation;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public getUiMessageDMs()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 1028
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    return-object v0
.end method

.method public initializeMessageList(Ljava/util/List;Ljava/util/List;ZLcom/helpshift/conversation/viewmodel/MessageListVMCallback;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/UIConversation;",
            ">;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;Z",
            "Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;",
            ")V"
        }
    .end annotation

    .line 59
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->updateUIConversationOrder(Ljava/util/List;)V

    .line 61
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->processAddedMessages(Ljava/util/Collection;)Ljava/util/List;

    move-result-object p2

    .line 63
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->groupConversationRedactedMessages(Ljava/util/List;)Ljava/util/List;

    move-result-object p2

    const/4 v0, 0x0

    .line 64
    invoke-direct {p0, p2, v0, p3}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertSystemMessageDMs(Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)Ljava/util/List;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    .line 65
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertSystemMessageDMsForLatestEmptyConversation(Ljava/util/List;)V

    .line 67
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    iget-object p2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result p2

    add-int/lit8 p2, p2, -0x1

    const/4 p3, 0x0

    invoke-virtual {p0, p1, p3, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByTime(Ljava/util/List;II)Lcom/helpshift/util/ValuePair;

    .line 70
    iput-object p4, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    return-void
.end method

.method public insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2

    if-eqz p1, :cond_1

    .line 587
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isUISupportedMessage()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 591
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM$3;-><init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void

    :cond_1
    :goto_0
    return-void
.end method

.method insertUIMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2

    .line 565
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->findBinaryIndexIntoUIList(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)I

    move-result v0

    .line 566
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v1, v0, p1}, Ljava/util/List;->add(ILjava/lang/Object;)V

    .line 567
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByDate(I)Z

    .line 568
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    add-int/lit8 v1, v0, -0x1

    add-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, p1, v1, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByTime(Ljava/util/List;II)Lcom/helpshift/util/ValuePair;

    return-void
.end method

.method isGroupFirstMessageAtIndex(I)Z
    .locals 3

    if-gez p1, :cond_0

    const/4 p1, 0x0

    return p1

    :cond_0
    const/4 v0, 0x1

    if-nez p1, :cond_1

    return v0

    .line 899
    :cond_1
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v1, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 900
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    sub-int/2addr p1, v0

    invoke-interface {v2, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 901
    invoke-direct {p0, p1, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->canGroupMessagesByTime(Lcom/helpshift/conversation/activeconversation/message/MessageDM;Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    xor-int/2addr p1, v0

    return p1
.end method

.method isMessageAtCorrectPosition(I)Z
    .locals 8

    .line 683
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v0

    const/4 v1, 0x1

    if-nez v0, :cond_0

    return v1

    :cond_0
    add-int/lit8 v2, p1, -0x1

    .line 688
    invoke-direct {p0, v2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v2

    const/4 v3, 0x0

    if-eqz v2, :cond_1

    .line 689
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v4

    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v6

    cmp-long v2, v4, v6

    if-gez v2, :cond_1

    return v3

    :cond_1
    add-int/2addr p1, v1

    .line 693
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getItemAtPosition(I)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 694
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v4

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v6

    cmp-long p1, v4, v6

    if-lez p1, :cond_2

    return v3

    :cond_2
    return v1
.end method

.method notifyMessageListUpdate(Lcom/helpshift/util/ValuePair;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/util/ValuePair<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Integer;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    return-void

    .line 672
    :cond_0
    iget-object v0, p1, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    check-cast v0, Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    .line 673
    iget-object v1, p1, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v1, Ljava/lang/Integer;

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    sub-int/2addr v1, v0

    add-int/lit8 v1, v1, 0x1

    .line 674
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    if-eqz v2, :cond_1

    if-lez v0, :cond_1

    if-lez v1, :cond_1

    iget-object p1, p1, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast p1, Ljava/lang/Integer;

    .line 677
    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->size()I

    move-result v2

    if-ge p1, v2, :cond_1

    .line 678
    iget-object p1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->updateMessages(II)V

    :cond_1
    return-void
.end method

.method notifyMessageListVMRefreshAll()V
    .locals 1

    .line 660
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    if-eqz v0, :cond_0

    .line 661
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->refreshAll()V

    :cond_0
    return-void
.end method

.method notifyUIMessageListUpdated()V
    .locals 1

    .line 1224
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    if-eqz v0, :cond_0

    .line 1225
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->onUIMessageListUpdated()V

    :cond_0
    return-void
.end method

.method public prependMessages(Ljava/util/List;Z)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;Z)V"
        }
    .end annotation

    .line 1032
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_1

    if-nez p2, :cond_0

    .line 1034
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndPrependSystemMessagesBeforeFirstMessage()V

    :cond_0
    return-void

    .line 1040
    :cond_1
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->filterUIMessages(Ljava/util/Collection;)Ljava/util/List;

    move-result-object p1

    .line 1042
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getSortMessagesComparator()Ljava/util/Comparator;

    move-result-object v0

    invoke-static {p1, v0}, Ljava/util/Collections;->sort(Ljava/util/List;Ljava/util/Comparator;)V

    .line 1043
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->groupConversationRedactedMessages(Ljava/util/List;)Ljava/util/List;

    move-result-object p1

    const/4 v0, 0x0

    .line 1045
    invoke-direct {p0, p1, v0, p2}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertSystemMessageDMs(Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/MessageDM;Z)Ljava/util/List;

    move-result-object p1

    const/4 p2, 0x0

    .line 1046
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    invoke-virtual {p0, p1, p2, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByTime(Ljava/util/List;II)Lcom/helpshift/util/ValuePair;

    .line 1048
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->prependMessagesInternal(Ljava/util/List;)V

    return-void
.end method

.method prependMessagesInternal(Ljava/util/List;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 1098
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 1101
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/MessageListVM$5;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM$5;-><init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method protected processAddedMessages(Ljava/util/Collection;)Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "+",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 484
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->filterUIMessages(Ljava/util/Collection;)Ljava/util/List;

    move-result-object p1

    .line 485
    invoke-direct {p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->getSortMessagesComparator()Ljava/util/Comparator;

    move-result-object v0

    invoke-static {p1, v0}, Ljava/util/Collections;->sort(Ljava/util/List;Ljava/util/Comparator;)V

    return-object p1
.end method

.method public remove(Ljava/util/List;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 1187
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->filterUIMessages(Ljava/util/Collection;)Ljava/util/List;

    move-result-object p1

    .line 1189
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 1193
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM$6;-><init>(Lcom/helpshift/conversation/viewmodel/MessageListVM;Ljava/util/List;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public unregisterMessageListVMCallback()V
    .locals 1

    const/4 v0, 0x0

    .line 1019
    iput-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    return-void
.end method

.method public declared-synchronized updateUIConversationOrder(Ljava/util/List;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/UIConversation;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    .line 1157
    :try_start_0
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v0, :cond_0

    .line 1158
    monitor-exit p0

    return-void

    .line 1161
    :cond_0
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiConversationLocalIdMap:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->clear()V

    .line 1162
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/UIConversation;

    .line 1163
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiConversationLocalIdMap:Ljava/util/Map;

    iget-wide v2, v0, Lcom/helpshift/conversation/activeconversation/UIConversation;->localID:J

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-interface {v1, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    .line 1165
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 1156
    monitor-exit p0

    throw p1
.end method

.method updateUIMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 4

    .line 619
    iget-object v0, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->indexOf(Ljava/lang/Object;)I

    move-result v0

    const/4 v1, -0x1

    if-ne v0, v1, :cond_0

    return-void

    .line 626
    :cond_0
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->isMessageAtCorrectPosition(I)Z

    move-result v1

    if-eqz v1, :cond_3

    .line 627
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByDate(I)Z

    move-result p1

    .line 628
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    add-int/lit8 v2, v0, -0x1

    add-int/lit8 v3, v0, 0x1

    invoke-virtual {p0, v1, v2, v3}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByTime(Ljava/util/List;II)Lcom/helpshift/util/ValuePair;

    move-result-object v1

    if-eqz p1, :cond_1

    .line 630
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyMessageListVMRefreshAll()V

    goto :goto_1

    :cond_1
    if-eqz v1, :cond_2

    .line 636
    iget-object p1, v1, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    invoke-static {v0, p1}, Ljava/lang/Math;->min(II)I

    move-result p1

    .line 637
    iget-object v1, v1, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v1, Ljava/lang/Integer;

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    invoke-static {v0, v1}, Ljava/lang/Math;->max(II)I

    move-result v0

    goto :goto_0

    :cond_2
    move p1, v0

    .line 640
    :goto_0
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    if-eqz v1, :cond_4

    if-gt p1, v0, :cond_4

    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    .line 641
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    if-ge v0, v1, :cond_4

    .line 642
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->messageListVMCallback:Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;

    sub-int/2addr v0, p1

    add-int/lit8 v0, v0, 0x1

    invoke-interface {v1, p1, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVMCallback;->updateMessages(II)V

    goto :goto_1

    .line 648
    :cond_3
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    add-int/lit8 v1, v0, -0x1

    .line 649
    invoke-virtual {p0, v1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByDate(I)Z

    .line 650
    iget-object v2, p0, Lcom/helpshift/conversation/viewmodel/MessageListVM;->uiMessageDMs:Ljava/util/List;

    add-int/lit8 v0, v0, 0x1

    invoke-virtual {p0, v2, v1, v0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->checkAndUpdateGroupByTime(Ljava/util/List;II)Lcom/helpshift/util/ValuePair;

    .line 653
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->insertUIMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 655
    invoke-virtual {p0}, Lcom/helpshift/conversation/viewmodel/MessageListVM;->notifyMessageListVMRefreshAll()V

    :cond_4
    :goto_1
    return-void
.end method
