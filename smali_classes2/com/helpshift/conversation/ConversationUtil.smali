.class public Lcom/helpshift/conversation/ConversationUtil;
.super Ljava/lang/Object;
.source "ConversationUtil.java"


# static fields
.field private static conversationComparator:Ljava/util/Comparator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Comparator<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation
.end field

.field private static messageDMComparator:Ljava/util/Comparator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Comparator<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 19
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static synthetic access$000(JJ)I
    .locals 0

    .line 19
    invoke-static {p0, p1, p2, p3}, Lcom/helpshift/conversation/ConversationUtil;->compareEpochTime(JJ)I

    move-result p0

    return p0
.end method

.method private static checkAndUpdateCachedConversationComparator()V
    .locals 1

    .line 24
    sget-object v0, Lcom/helpshift/conversation/ConversationUtil;->conversationComparator:Ljava/util/Comparator;

    if-nez v0, :cond_0

    .line 25
    new-instance v0, Lcom/helpshift/conversation/ConversationUtil$1;

    invoke-direct {v0}, Lcom/helpshift/conversation/ConversationUtil$1;-><init>()V

    sput-object v0, Lcom/helpshift/conversation/ConversationUtil;->conversationComparator:Ljava/util/Comparator;

    :cond_0
    return-void
.end method

.method private static compareEpochTime(JJ)I
    .locals 1

    cmp-long v0, p0, p2

    if-lez v0, :cond_0

    const/4 p0, 0x1

    return p0

    :cond_0
    cmp-long v0, p0, p2

    if-gez v0, :cond_1

    const/4 p0, -0x1

    return p0

    :cond_1
    const/4 p0, 0x0

    return p0
.end method

.method public static getLastConversationBasedOnCreatedAt(Ljava/util/Collection;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;"
        }
    .end annotation

    .line 53
    invoke-static {}, Lcom/helpshift/conversation/ConversationUtil;->checkAndUpdateCachedConversationComparator()V

    .line 54
    sget-object v0, Lcom/helpshift/conversation/ConversationUtil;->conversationComparator:Ljava/util/Comparator;

    invoke-static {p0, v0}, Ljava/util/Collections;->max(Ljava/util/Collection;Ljava/util/Comparator;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    return-object p0
.end method

.method public static getUserMessageCountForConversationLocalId(Lcom/helpshift/conversation/dao/ConversationDAO;Ljava/lang/Long;)I
    .locals 2

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 105
    :cond_0
    invoke-static {p1}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    .line 104
    invoke-static {p0, v1}, Lcom/helpshift/conversation/ConversationUtil;->getUserMessageCountForConversationLocalIds(Lcom/helpshift/conversation/dao/ConversationDAO;Ljava/util/List;)Ljava/util/Map;

    move-result-object p0

    .line 106
    invoke-interface {p0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/Integer;

    if-eqz p0, :cond_1

    .line 108
    invoke-virtual {p0}, Ljava/lang/Integer;->intValue()I

    move-result p0

    return p0

    :cond_1
    return v0
.end method

.method public static getUserMessageCountForConversationLocalIds(Lcom/helpshift/conversation/dao/ConversationDAO;Ljava/util/List;)Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/dao/ConversationDAO;",
            "Ljava/util/List<",
            "Ljava/lang/Long;",
            ">;)",
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x5

    .line 125
    new-array v0, v0, [Ljava/lang/String;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_TEXT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->getValue()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ACCEPTED_APP_REVIEW:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 126
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->getValue()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->SCREENSHOT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 127
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->getValue()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x2

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_TEXT_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 128
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->getValue()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x3

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/MessageType;->USER_RESP_FOR_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 129
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/message/MessageType;->getValue()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x4

    aput-object v1, v0, v2

    .line 130
    invoke-interface {p0, p1, v0}, Lcom/helpshift/conversation/dao/ConversationDAO;->getMessagesCountForConversations(Ljava/util/List;[Ljava/lang/String;)Ljava/util/Map;

    move-result-object p0

    return-object p0
.end method

.method public static isInProgressState(Lcom/helpshift/conversation/dto/IssueState;)Z
    .locals 1

    .line 135
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->NEW:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p0, v0, :cond_1

    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->NEW_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p0, v0, :cond_1

    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->AGENT_REPLIED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p0, v0, :cond_1

    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->WAITING_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p0, v0, :cond_1

    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->PENDING_REASSIGNMENT:Lcom/helpshift/conversation/dto/IssueState;

    if-eq p0, v0, :cond_1

    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->COMPLETED_ISSUE_CREATED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p0, v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p0, 0x1

    :goto_1
    return p0
.end method

.method public static shouldPollActivelyForConversations(Ljava/util/List;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)Z"
        }
    .end annotation

    .line 151
    invoke-static {p0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 155
    :cond_0
    invoke-interface {p0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :cond_1
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 157
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 p0, 0x1

    return p0

    :cond_2
    return v1
.end method

.method public static sortConversationsBasedOnCreatedAt(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    .line 40
    invoke-static {}, Lcom/helpshift/conversation/ConversationUtil;->checkAndUpdateCachedConversationComparator()V

    .line 41
    sget-object v0, Lcom/helpshift/conversation/ConversationUtil;->conversationComparator:Ljava/util/Comparator;

    invoke-static {p0, v0}, Ljava/util/Collections;->sort(Ljava/util/List;Ljava/util/Comparator;)V

    return-void
.end method

.method public static sortMessagesBasedOnCreatedAt(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 61
    sget-object v0, Lcom/helpshift/conversation/ConversationUtil;->messageDMComparator:Ljava/util/Comparator;

    if-nez v0, :cond_0

    .line 62
    new-instance v0, Lcom/helpshift/conversation/ConversationUtil$2;

    invoke-direct {v0}, Lcom/helpshift/conversation/ConversationUtil$2;-><init>()V

    sput-object v0, Lcom/helpshift/conversation/ConversationUtil;->messageDMComparator:Ljava/util/Comparator;

    .line 71
    :cond_0
    sget-object v0, Lcom/helpshift/conversation/ConversationUtil;->messageDMComparator:Ljava/util/Comparator;

    invoke-static {p0, v0}, Ljava/util/Collections;->sort(Ljava/util/List;Ljava/util/Comparator;)V

    return-void
.end method
