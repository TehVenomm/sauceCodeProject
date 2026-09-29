.class public Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;
.super Lcom/helpshift/conversation/loaders/ConversationDBLoader;
.source "ConversationHistoryDBLoader.java"


# instance fields
.field private userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/dao/ConversationDAO;)V
    .locals 0

    .line 25
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/loaders/ConversationDBLoader;-><init>(Lcom/helpshift/conversation/dao/ConversationDAO;)V

    .line 26
    iput-object p1, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    return-void
.end method

.method private filterOutConversationCreatedAfterCursor(Ljava/lang/String;Ljava/util/List;)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 223
    invoke-static {p2}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-nez v0, :cond_2

    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_1

    .line 227
    :cond_0
    invoke-static {p1}, Lcom/helpshift/common/util/HSDateFormatSpec;->convertToEpochTime(Ljava/lang/String;)J

    move-result-wide v0

    .line 228
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    .line 229
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 230
    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v3

    .line 232
    invoke-virtual {p0, v3, v4, v0, v1}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->compareEpochTime(JJ)I

    move-result v3

    if-gtz v3, :cond_1

    .line 233
    invoke-interface {p1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    return-object p1

    :cond_2
    :goto_1
    return-object p2
.end method

.method private filterOutConversationsForWhichMessagesLimitExceed(JLjava/util/List;)Ljava/util/List;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 189
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 190
    invoke-interface {p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 191
    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 195
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v1, v0}, Lcom/helpshift/conversation/dao/ConversationDAO;->getMessagesCountForConversations(Ljava/util/List;)Ljava/util/Map;

    move-result-object v0

    const/4 v1, 0x0

    .line 199
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 200
    invoke-interface {p3}, Ljava/util/List;->size()I

    move-result v3

    add-int/lit8 v3, v3, -0x1

    :goto_1
    if-ltz v3, :cond_2

    .line 201
    invoke-interface {p3, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 202
    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v0, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/Integer;

    invoke-virtual {v5}, Ljava/lang/Integer;->intValue()I

    move-result v5

    .line 203
    invoke-interface {v2, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/2addr v1, v5

    int-to-long v4, v1

    cmp-long v6, v4, p1

    if-ltz v6, :cond_1

    goto :goto_2

    :cond_1
    add-int/lit8 v3, v3, -0x1

    goto :goto_1

    .line 209
    :cond_2
    :goto_2
    invoke-static {v2}, Ljava/util/Collections;->reverse(Ljava/util/List;)V

    return-object v2
.end method

.method private filterOutFullPrivacyEnabledConversations(Ljava/util/List;)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 252
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 253
    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    return-object v0

    .line 257
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v1

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_2

    .line 259
    invoke-interface {p1, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 260
    iget-boolean v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->wasFullPrivacyEnabledAtCreation:Z

    if-nez v4, :cond_1

    .line 261
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_2
    return-object v0
.end method

.method private filterOutMultipleOpenConversations(Ljava/util/List;)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 318
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 319
    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    return-object v0

    .line 325
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 327
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v2

    if-nez v2, :cond_1

    .line 328
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_2
    return-object v0
.end method

.method private filterOutRejectedEmptyPreIssues(Ljava/util/List;)Ljava/util/List;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 273
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 274
    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    return-object v0

    .line 279
    :cond_0
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 280
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_1
    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 281
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v5, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v4, v5, :cond_1

    const-string v4, "preissue"

    iget-object v5, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    .line 282
    invoke-virtual {v4, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_1

    .line 283
    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v1, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 287
    :cond_2
    invoke-interface {v1}, Ljava/util/List;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_3

    .line 289
    invoke-interface {v0, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    return-object v0

    .line 293
    :cond_3
    iget-object v2, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    .line 294
    invoke-static {v2, v1}, Lcom/helpshift/conversation/ConversationUtil;->getUserMessageCountForConversationLocalIds(Lcom/helpshift/conversation/dao/ConversationDAO;Ljava/util/List;)Ljava/util/Map;

    move-result-object v1

    .line 297
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_4
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_6

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 298
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v1, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/Integer;

    if-eqz v3, :cond_5

    .line 299
    invoke-virtual {v3}, Ljava/lang/Integer;->intValue()I

    move-result v3

    if-eqz v3, :cond_4

    .line 302
    :cond_5
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    :cond_6
    return-object v0
.end method

.method private getLastOpenConversation(Ljava/util/List;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;"
        }
    .end annotation

    .line 344
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 349
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 352
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v2

    if-eqz v2, :cond_1

    move-object v1, v0

    goto :goto_0

    :cond_2
    return-object v1
.end method


# virtual methods
.method public fetchMessages(Ljava/lang/String;Ljava/lang/String;J)Ljava/util/List;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "J)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 38
    iget-object v0, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 39
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 40
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 42
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    return-object p1

    .line 46
    :cond_0
    invoke-static {v0}, Lcom/helpshift/conversation/ConversationUtil;->sortConversationsBasedOnCreatedAt(Ljava/util/List;)V

    .line 48
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    .line 59
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    const/4 v3, 0x1

    if-eqz v1, :cond_1

    goto :goto_0

    .line 65
    :cond_1
    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->filterOutConversationCreatedAfterCursor(Ljava/lang/String;Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    .line 71
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v4

    if-nez v4, :cond_3

    .line 72
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v4

    sub-int/2addr v4, v3

    invoke-interface {v0, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 73
    invoke-virtual {v4}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getCreatedAt()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_3

    .line 74
    iget-object p1, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v5}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    invoke-interface {p1, v5, v6}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object p1

    .line 75
    invoke-virtual {p0, p2, p3, p4, p1}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->filterMessages(Ljava/lang/String;JLjava/util/List;)Ljava/util/List;

    move-result-object p1

    .line 76
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result p2

    if-nez p2, :cond_2

    .line 77
    invoke-virtual {v4, p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setMessageDMs(Ljava/util/List;)V

    .line 78
    invoke-interface {v2, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 80
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result p1

    int-to-long p1, p1

    sub-long/2addr p3, p1

    .line 82
    :cond_2
    invoke-interface {v0, v4}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    :cond_3
    :goto_0
    const-wide/16 p1, 0x1

    cmp-long v4, p3, p1

    if-gez v4, :cond_4

    return-object v2

    :cond_4
    const/4 p1, 0x0

    if-eqz v1, :cond_5

    .line 96
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result p2

    if-le p2, v3, :cond_6

    sub-int/2addr p2, v3

    .line 98
    invoke-interface {v0, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 99
    invoke-interface {v0, p1, p2}, Ljava/util/List;->subList(II)Ljava/util/List;

    move-result-object p2

    invoke-direct {p0, p2}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->filterOutFullPrivacyEnabledConversations(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    .line 100
    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 104
    :cond_5
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->filterOutFullPrivacyEnabledConversations(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    .line 106
    :cond_6
    :goto_1
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->filterOutRejectedEmptyPreIssues(Ljava/util/List;)Ljava/util/List;

    move-result-object p2

    if-eqz v1, :cond_7

    .line 114
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->getLastOpenConversation(Ljava/util/List;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 116
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->filterOutMultipleOpenConversations(Ljava/util/List;)Ljava/util/List;

    move-result-object p2

    if-eqz v0, :cond_8

    .line 123
    invoke-interface {p2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 127
    :cond_7
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->filterOutMultipleOpenConversations(Ljava/util/List;)Ljava/util/List;

    move-result-object p2

    .line 130
    :cond_8
    :goto_2
    invoke-direct {p0, p3, p4, p2}, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->filterOutConversationsForWhichMessagesLimitExceed(JLjava/util/List;)Ljava/util/List;

    move-result-object p2

    .line 133
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 134
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 135
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_3
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_9

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 136
    iget-object v6, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v0, v6}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 137
    iget-object v6, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v1, v6, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_3

    .line 139
    :cond_9
    iget-object v4, p0, Lcom/helpshift/conversation/loaders/ConversationHistoryDBLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v4, v0}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessagesForConversations(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    .line 140
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_a
    :goto_4
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_b

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 141
    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-interface {v1, v5}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_a

    .line 142
    iget-object v5, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    invoke-interface {v1, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 143
    iget-object v5, v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v5, v4}, Lcom/helpshift/common/util/HSObservableList;->add(Ljava/lang/Object;)Z

    goto :goto_4

    .line 152
    :cond_b
    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result v0

    sub-int/2addr v0, v3

    const/4 v1, 0x0

    :goto_5
    if-ltz v0, :cond_d

    .line 153
    invoke-interface {p2, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 154
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v4}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v4

    add-int/2addr v4, v1

    int-to-long v4, v4

    cmp-long v6, v4, p3

    if-lez v6, :cond_c

    .line 157
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-static {v4}, Lcom/helpshift/conversation/ConversationUtil;->sortMessagesBasedOnCreatedAt(Ljava/util/List;)V

    int-to-long v4, v1

    sub-long v4, p3, v4

    long-to-int v4, v4

    .line 159
    new-instance v5, Ljava/util/ArrayList;

    iget-object v6, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-direct {v5, v6}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 160
    iget-object v6, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v6}, Lcom/helpshift/common/util/HSObservableList;->clear()V

    .line 161
    invoke-interface {v5}, Ljava/util/List;->size()I

    move-result v6

    sub-int/2addr v6, v4

    .line 162
    invoke-interface {v5}, Ljava/util/List;->size()I

    move-result v4

    .line 161
    invoke-interface {v5, v6, v4}, Ljava/util/List;->subList(II)Ljava/util/List;

    move-result-object v4

    .line 163
    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v3, v4}, Lcom/helpshift/common/util/HSObservableList;->addAll(Ljava/util/Collection;)Z

    goto :goto_6

    .line 167
    :cond_c
    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v3}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v3

    add-int/2addr v1, v3

    :goto_6
    add-int/lit8 v0, v0, -0x1

    goto :goto_5

    .line 172
    :cond_d
    invoke-interface {v2, p1, p2}, Ljava/util/List;->addAll(ILjava/util/Collection;)Z

    .line 175
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_7
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_e

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 176
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-static {p2}, Lcom/helpshift/conversation/ConversationUtil;->sortMessagesBasedOnCreatedAt(Ljava/util/List;)V

    goto :goto_7

    :cond_e
    return-object v2
.end method
