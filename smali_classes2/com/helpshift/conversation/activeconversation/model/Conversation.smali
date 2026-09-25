.class public Lcom/helpshift/conversation/activeconversation/model/Conversation;
.super Ljava/lang/Object;
.source "Conversation.java"

# interfaces
.implements Ljava/util/Observer;
.implements Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;


# instance fields
.field public conversationDMListener:Lcom/helpshift/conversation/activeconversation/ConversationDMListener;

.field public createdAt:Ljava/lang/String;

.field public createdRequestId:Ljava/lang/String;

.field public csatFeedback:Ljava/lang/String;

.field public csatRating:I

.field public csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

.field public enableMessageClickOnResolutionRejected:Z

.field public epochCreatedAtTime:J

.field public isAutoFilledPreIssue:Z

.field public isConversationEndedDelegateSent:Z

.field public isInBetweenBotExecution:Z

.field public isRedacted:Z

.field public isStartNewConversationClicked:Z

.field public issueType:Ljava/lang/String;

.field public lastUserActivityTime:J

.field public localId:Ljava/lang/Long;

.field public localUUID:Ljava/lang/String;

.field public messageCursor:Ljava/lang/String;

.field public messageDMs:Lcom/helpshift/common/util/HSObservableList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/helpshift/common/util/HSObservableList<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation
.end field

.field public preConversationServerId:Ljava/lang/String;

.field public publishId:Ljava/lang/String;

.field public serverId:Ljava/lang/String;

.field public shouldIncrementMessageCount:Z

.field public showAgentName:Z

.field public state:Lcom/helpshift/conversation/dto/IssueState;

.field public title:Ljava/lang/String;

.field public final unansweredRequestForReopenMessageDMs:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/helpshift/conversation/activeconversation/message/RequestForReopenMessageDM;",
            ">;"
        }
    .end annotation
.end field

.field public updatedAt:Ljava/lang/String;

.field public userLocalId:J

.field public wasFullPrivacyEnabledAtCreation:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;Lcom/helpshift/conversation/dto/IssueState;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLjava/lang/String;)V
    .locals 1

    .line 68
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 32
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->unansweredRequestForReopenMessageDMs:Ljava/util/Map;

    .line 42
    new-instance v0, Lcom/helpshift/common/util/HSObservableList;

    invoke-direct {v0}, Lcom/helpshift/common/util/HSObservableList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 48
    sget-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->NONE:Lcom/helpshift/conversation/states/ConversationCSATState;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    .line 69
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->title:Ljava/lang/String;

    .line 70
    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdAt:Ljava/lang/String;

    .line 71
    iput-wide p4, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->epochCreatedAtTime:J

    .line 72
    iput-object p6, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updatedAt:Ljava/lang/String;

    .line 73
    iput-object p7, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    .line 74
    iput-object p8, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageCursor:Ljava/lang/String;

    .line 75
    iput-boolean p9, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    .line 76
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 77
    iput-object p10, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    return-void
.end method

.method private updateStateBasedOnMessages()V
    .locals 3

    .line 136
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v0, v1, :cond_3

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    if-eqz v0, :cond_3

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 137
    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v0

    if-lez v0, :cond_3

    .line 138
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->size()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    const/4 v1, 0x0

    :goto_0
    if-ltz v0, :cond_1

    .line 140
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 141
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

    .line 145
    :cond_1
    instance-of v0, v1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;

    if-eqz v0, :cond_2

    .line 146
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    goto :goto_1

    .line 148
    :cond_2
    instance-of v0, v1, Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;

    if-eqz v0, :cond_3

    .line 149
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    :cond_3
    :goto_1
    return-void
.end method


# virtual methods
.method public getCreatedAt()Ljava/lang/String;
    .locals 1

    .line 96
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdAt:Ljava/lang/String;

    return-object v0
.end method

.method public getEpochCreatedAtTime()J
    .locals 2

    .line 88
    iget-wide v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->epochCreatedAtTime:J

    return-wide v0
.end method

.method public getIssueId()Ljava/lang/String;
    .locals 1

    .line 112
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    return-object v0
.end method

.method public getPreIssueId()Ljava/lang/String;
    .locals 1

    .line 117
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    return-object v0
.end method

.method public isInPreIssueMode()Z
    .locals 2

    const-string v0, "preissue"

    .line 107
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    return v0
.end method

.method public isIssueInProgress()Z
    .locals 1

    .line 155
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    invoke-static {v0}, Lcom/helpshift/conversation/ConversationUtil;->isInProgressState(Lcom/helpshift/conversation/dto/IssueState;)Z

    move-result v0

    return v0
.end method

.method public isLocalPreIssue()Z
    .locals 1

    .line 174
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public registerMessagesObserver()V
    .locals 2

    .line 163
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 164
    invoke-virtual {v1, p0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->addObserver(Ljava/util/Observer;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public setCreatedAt(Ljava/lang/String;)V
    .locals 1

    .line 100
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 101
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdAt:Ljava/lang/String;

    :cond_0
    return-void
.end method

.method public setEpochCreatedAtTime(J)V
    .locals 0

    .line 92
    iput-wide p1, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->epochCreatedAtTime:J

    return-void
.end method

.method public setListener(Lcom/helpshift/conversation/activeconversation/ConversationDMListener;)V
    .locals 0

    .line 159
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->conversationDMListener:Lcom/helpshift/conversation/activeconversation/ConversationDMListener;

    return-void
.end method

.method public setLocalId(J)V
    .locals 1

    .line 81
    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    .line 82
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p1}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 83
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v0, p2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    goto :goto_0

    :cond_0
    return-void
.end method

.method public setMessageDMs(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 122
    new-instance v0, Lcom/helpshift/common/util/HSObservableList;

    invoke-direct {v0, p1}, Lcom/helpshift/common/util/HSObservableList;-><init>(Ljava/util/List;)V

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 123
    invoke-direct {p0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updateStateBasedOnMessages()V

    return-void
.end method

.method public update(Ljava/util/Observable;Ljava/lang/Object;)V
    .locals 1

    .line 128
    instance-of p2, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    if-eqz p2, :cond_0

    .line 129
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 130
    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p2, p1}, Lcom/helpshift/common/util/HSObservableList;->indexOf(Ljava/lang/Object;)I

    move-result p2

    .line 131
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0, p2, p1}, Lcom/helpshift/common/util/HSObservableList;->setAndNotifyObserver(ILjava/lang/Object;)Ljava/lang/Object;

    :cond_0
    return-void
.end method
