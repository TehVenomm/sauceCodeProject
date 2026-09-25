.class public abstract Lcom/helpshift/conversation/activeconversation/message/MessageDM;
.super Ljava/util/Observable;
.source "MessageDM.java"


# static fields
.field public static final DELIVERY_STATE_READ:I = 0x1

.field public static final DELIVERY_STATE_SENT:I = 0x2

.field public static final DELIVERY_STATE_UNREAD:I = 0x0

.field private static final TAG:Ljava/lang/String; = "Helpshift_MessageDM"


# instance fields
.field public authorId:Ljava/lang/String;

.field public authorName:Ljava/lang/String;

.field public body:Ljava/lang/String;

.field public conversationLocalId:Ljava/lang/Long;

.field private createdAt:Ljava/lang/String;

.field public createdRequestId:Ljava/lang/String;

.field public deliveryState:I

.field protected domain:Lcom/helpshift/common/domain/Domain;

.field private epochCreatedAtTime:J

.field public final isAdminMessage:Z

.field public isMessageSeenSynced:Z

.field public isRedacted:Z

.field public localId:Ljava/lang/Long;

.field public final messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

.field protected platform:Lcom/helpshift/common/platform/Platform;

.field public readAt:Ljava/lang/String;

.field public seenAtMessageCursor:Ljava/lang/String;

.field public serverId:Ljava/lang/String;

.field public shouldShowAgentNameForConversation:Z

.field private final uiViewState:Lcom/helpshift/conversation/activeconversation/message/UIViewState;


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;ZLcom/helpshift/conversation/activeconversation/message/MessageType;)V
    .locals 0

    .line 61
    invoke-direct {p0}, Ljava/util/Observable;-><init>()V

    .line 62
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    .line 63
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->createdAt:Ljava/lang/String;

    .line 64
    iput-wide p3, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->epochCreatedAtTime:J

    .line 65
    iput-object p5, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->authorName:Ljava/lang/String;

    .line 66
    iput-boolean p6, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isAdminMessage:Z

    .line 67
    iput-object p7, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->messageType:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    .line 70
    new-instance p1, Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    invoke-direct {p1}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;-><init>()V

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->uiViewState:Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    return-void
.end method


# virtual methods
.method public getAccessbilityMessageTime()Ljava/lang/String;
    .locals 4

    .line 106
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getCurrentLocale()Ljava/util/Locale;

    move-result-object v0

    .line 107
    new-instance v1, Ljava/util/Date;

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v2

    invoke-direct {v1, v2, v3}, Ljava/util/Date;-><init>(J)V

    .line 108
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 109
    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v2

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->is24HourFormat()Z

    move-result v2

    if-eqz v2, :cond_0

    const-string v2, "H:mm"

    goto :goto_0

    :cond_0
    const-string v2, "h:mm a"

    .line 110
    :goto_0
    invoke-static {v2, v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getDateFormatter(Ljava/lang/String;Ljava/util/Locale;)Lcom/helpshift/common/util/HSSimpleDateFormat;

    move-result-object v2

    invoke-virtual {v2, v1}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "EEEE, MMMM dd, yyyy"

    .line 111
    invoke-static {v3, v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getDateFormatter(Ljava/lang/String;Ljava/util/Locale;)Lcom/helpshift/common/util/HSSimpleDateFormat;

    move-result-object v0

    invoke-virtual {v0, v1}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    .line 112
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getCreatedAt()Ljava/lang/String;
    .locals 1

    .line 179
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->createdAt:Ljava/lang/String;

    return-object v0
.end method

.method public getDisplayedAuthorName()Ljava/lang/String;
    .locals 3

    .line 122
    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isAdminMessage:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->shouldShowAgentNameForConversation:Z

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 123
    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v0

    const-string v2, "showAgentName"

    invoke-virtual {v0, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 125
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->authorName:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->authorName:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    move-object v1, v0

    :cond_1
    :goto_0
    return-object v1
.end method

.method public getEpochCreatedAtTime()J
    .locals 2

    .line 189
    iget-wide v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->epochCreatedAtTime:J

    return-wide v0
.end method

.method protected getIdempotentPolicy()Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;
    .locals 1

    .line 198
    new-instance v0, Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;

    invoke-direct {v0}, Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;-><init>()V

    return-object v0
.end method

.method getIssueSendMessageRoute(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)Ljava/lang/String;
    .locals 2

    .line 171
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/issues/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {p1}, Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;->getIssueId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/messages/"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method getPreIssueSendMessageRoute(Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;)Ljava/lang/String;
    .locals 2

    .line 166
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "/preissues/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {p1}, Lcom/helpshift/conversation/activeconversation/ConversationServerInfo;->getPreIssueId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/messages/"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method getSendMessageNetwork(Ljava/lang/String;)Lcom/helpshift/common/domain/network/Network;
    .locals 7

    .line 155
    new-instance v1, Lcom/helpshift/common/domain/network/POSTNetwork;

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v1, p1, v0, v2}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 156
    new-instance v6, Lcom/helpshift/common/domain/network/IdempotentNetwork;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getIdempotentPolicy()Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;

    move-result-object v3

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    .line 157
    invoke-static {v0}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    move-object v0, v6

    move-object v4, p1

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/common/domain/network/IdempotentNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;Ljava/lang/String;Ljava/lang/String;)V

    .line 158
    new-instance p1, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p1, v6, v0}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 159
    new-instance v0, Lcom/helpshift/common/domain/network/UserPreConditionsFailedNetwork;

    invoke-direct {v0, p1}, Lcom/helpshift/common/domain/network/UserPreConditionsFailedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 160
    new-instance p1, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 161
    new-instance v0, Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;

    invoke-direct {v0, p1}, Lcom/helpshift/common/domain/network/GuardAgainstConversationArchivalNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 162
    new-instance p1, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {p1, v0}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    return-object p1
.end method

.method public getSubText()Ljava/lang/String;
    .locals 5

    .line 79
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v0

    .line 80
    invoke-virtual {v0}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getCurrentLocale()Ljava/util/Locale;

    move-result-object v0

    :try_start_0
    const-string v1, "yyyy-MM-dd\'T\'HH:mm:ss.SSS\'Z\'"

    const-string v2, "GMT"

    .line 83
    invoke-static {v1, v0, v2}, Lcom/helpshift/common/util/HSDateFormatSpec;->getDateFormatter(Ljava/lang/String;Ljava/util/Locale;Ljava/lang/String;)Lcom/helpshift/common/util/HSSimpleDateFormat;

    move-result-object v1

    .line 84
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/helpshift/common/util/HSSimpleDateFormat;->parse(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v1
    :try_end_0
    .catch Ljava/text/ParseException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    .line 87
    new-instance v2, Ljava/util/Date;

    invoke-direct {v2}, Ljava/util/Date;-><init>()V

    const-string v3, "Helpshift_MessageDM"

    const-string v4, "getSubText : ParseException"

    .line 88
    invoke-static {v3, v4, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object v1, v2

    .line 90
    :goto_0
    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 91
    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v2

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->is24HourFormat()Z

    move-result v2

    if-eqz v2, :cond_0

    const-string v2, "H:mm"

    goto :goto_1

    :cond_0
    const-string v2, "h:mm a"

    .line 92
    :goto_1
    invoke-static {v2, v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getDateFormatter(Ljava/lang/String;Ljava/util/Locale;)Lcom/helpshift/common/util/HSSimpleDateFormat;

    move-result-object v0

    invoke-virtual {v0, v1}, Lcom/helpshift/common/util/HSSimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    .line 95
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getDisplayedAuthorName()Ljava/lang/String;

    move-result-object v1

    .line 96
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_1

    .line 97
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    :cond_1
    return-object v0
.end method

.method public getUiViewState()Lcom/helpshift/conversation/activeconversation/message/UIViewState;
    .locals 1

    .line 175
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->uiViewState:Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    return-object v0
.end method

.method public abstract isUISupportedMessage()Z
.end method

.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 2

    .line 142
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->body:Ljava/lang/String;

    .line 143
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getCreatedAt()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->createdAt:Ljava/lang/String;

    .line 144
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->epochCreatedAtTime:J

    .line 145
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->authorName:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->authorName:Ljava/lang/String;

    .line 146
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 147
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    .line 149
    :cond_0
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isRedacted:Z

    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->isRedacted:Z

    return-void
.end method

.method public mergeAndNotify(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 0

    .line 137
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 138
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->notifyUpdated()V

    return-void
.end method

.method notifyUpdated()V
    .locals 0

    .line 132
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->setChanged()V

    .line 133
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->notifyObservers()V

    return-void
.end method

.method public setCreatedAt(Ljava/lang/String;)V
    .locals 1

    .line 183
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 184
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->createdAt:Ljava/lang/String;

    :cond_0
    return-void
.end method

.method public setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 74
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 75
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->platform:Lcom/helpshift/common/platform/Platform;

    return-void
.end method

.method public setEpochCreatedAtTime(J)V
    .locals 0

    .line 193
    iput-wide p1, p0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->epochCreatedAtTime:J

    return-void
.end method
