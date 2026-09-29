.class public Lcom/helpshift/conversation/ConversationInboxPoller;
.super Ljava/lang/Object;
.source "ConversationInboxPoller.java"

# interfaces
.implements Ljava/util/Observer;


# static fields
.field private static final INITIAL_DELAY:J = 0xbb8L

.field private static final TAG:Ljava/lang/String; = "Helpshift_ConvPoller"


# instance fields
.field private final conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

.field private currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

.field private pollFunctionListener:Lcom/helpshift/common/domain/PollFunction$PollFunctionListener;

.field public final poller:Lcom/helpshift/common/domain/Poller;

.field private final sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

.field private final userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;Lcom/helpshift/common/domain/Poller;Lcom/helpshift/conversation/dao/ConversationDAO;)V
    .locals 1

    .line 51
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 38
    new-instance v0, Lcom/helpshift/conversation/ConversationInboxPoller$1;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/ConversationInboxPoller$1;-><init>(Lcom/helpshift/conversation/ConversationInboxPoller;)V

    iput-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->pollFunctionListener:Lcom/helpshift/common/domain/PollFunction$PollFunctionListener;

    .line 52
    iput-object p1, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 53
    iput-object p2, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 54
    iput-object p3, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->poller:Lcom/helpshift/common/domain/Poller;

    .line 55
    iput-object p4, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    return-void
.end method

.method private shouldStartSDKPoller()Z
    .locals 2

    .line 60
    invoke-static {}, Lcom/helpshift/app/AppLifeCycleStateHolder;->isAppInForeground()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 61
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->issueExists()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 62
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced()Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "disableInAppConversation"

    .line 63
    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method


# virtual methods
.method public refreshPoller(Z)V
    .locals 2

    .line 147
    invoke-static {}, Lcom/helpshift/app/AppLifeCycleStateHolder;->isAppInForeground()Z

    move-result v0

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 150
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

    sget-object v1, Lcom/helpshift/common/domain/PollingInterval;->AGGRESSIVE:Lcom/helpshift/common/domain/PollingInterval;

    if-ne v0, v1, :cond_1

    .line 152
    invoke-virtual {p0}, Lcom/helpshift/conversation/ConversationInboxPoller;->startChatPoller()V

    goto :goto_1

    .line 156
    :cond_1
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/ConversationInboxPoller;->startAppPoller(Z)V

    goto :goto_1

    .line 148
    :cond_2
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/conversation/ConversationInboxPoller;->stop()V

    :goto_1
    return-void
.end method

.method public startAppPoller(Z)V
    .locals 4

    .line 67
    invoke-direct {p0}, Lcom/helpshift/conversation/ConversationInboxPoller;->shouldStartSDKPoller()Z

    move-result v0

    if-nez v0, :cond_0

    .line 68
    invoke-virtual {p0}, Lcom/helpshift/conversation/ConversationInboxPoller;->stop()V

    return-void

    .line 72
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 76
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v1

    if-nez v1, :cond_1

    .line 77
    invoke-static {v0}, Lcom/helpshift/conversation/ConversationUtil;->shouldPollActivelyForConversations(Ljava/util/List;)Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_2

    .line 84
    sget-object v0, Lcom/helpshift/common/domain/PollingInterval;->PASSIVE:Lcom/helpshift/common/domain/PollingInterval;

    goto :goto_1

    :cond_2
    sget-object v0, Lcom/helpshift/common/domain/PollingInterval;->CONSERVATIVE:Lcom/helpshift/common/domain/PollingInterval;

    .line 87
    :goto_1
    iget-object v1, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

    if-ne v1, v0, :cond_3

    return-void

    .line 91
    :cond_3
    invoke-virtual {p0}, Lcom/helpshift/conversation/ConversationInboxPoller;->stop()V

    .line 92
    iput-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

    const-string v1, "Helpshift_ConvPoller"

    .line 94
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Listening for conversation updates : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz p1, :cond_4

    const-wide/16 v1, 0xbb8

    goto :goto_2

    :cond_4
    const-wide/16 v1, 0x0

    .line 99
    :goto_2
    iget-object p1, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->poller:Lcom/helpshift/common/domain/Poller;

    iget-object v3, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->pollFunctionListener:Lcom/helpshift/common/domain/PollFunction$PollFunctionListener;

    invoke-virtual {p1, v0, v1, v2, v3}, Lcom/helpshift/common/domain/Poller;->start(Lcom/helpshift/common/domain/PollingInterval;JLcom/helpshift/common/domain/PollFunction$PollFunctionListener;)V

    return-void
.end method

.method public startChatPoller()V
    .locals 5

    .line 105
    invoke-static {}, Lcom/helpshift/app/AppLifeCycleStateHolder;->isAppInForeground()Z

    move-result v0

    if-nez v0, :cond_0

    .line 106
    invoke-virtual {p0}, Lcom/helpshift/conversation/ConversationInboxPoller;->stop()V

    return-void

    .line 111
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

    sget-object v1, Lcom/helpshift/common/domain/PollingInterval;->AGGRESSIVE:Lcom/helpshift/common/domain/PollingInterval;

    if-ne v0, v1, :cond_1

    return-void

    .line 115
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/conversation/ConversationInboxPoller;->stop()V

    .line 116
    sget-object v0, Lcom/helpshift/common/domain/PollingInterval;->AGGRESSIVE:Lcom/helpshift/common/domain/PollingInterval;

    iput-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

    const-string v0, "Helpshift_ConvPoller"

    const-string v1, "Listening for in-chat conversation updates"

    .line 118
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 120
    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->poller:Lcom/helpshift/common/domain/Poller;

    sget-object v1, Lcom/helpshift/common/domain/PollingInterval;->AGGRESSIVE:Lcom/helpshift/common/domain/PollingInterval;

    const-wide/16 v2, 0x0

    iget-object v4, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->pollFunctionListener:Lcom/helpshift/common/domain/PollFunction$PollFunctionListener;

    invoke-virtual {v0, v1, v2, v3, v4}, Lcom/helpshift/common/domain/Poller;->start(Lcom/helpshift/common/domain/PollingInterval;JLcom/helpshift/common/domain/PollFunction$PollFunctionListener;)V

    return-void
.end method

.method public stop()V
    .locals 3

    const-string v0, "Helpshift_ConvPoller"

    .line 124
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Stopped listening for conversation updates : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 125
    iget-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->poller:Lcom/helpshift/common/domain/Poller;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Poller;->stop()V

    const/4 v0, 0x0

    .line 129
    iput-object v0, p0, Lcom/helpshift/conversation/ConversationInboxPoller;->currentPollingInterval:Lcom/helpshift/common/domain/PollingInterval;

    return-void
.end method

.method public update(Ljava/util/Observable;Ljava/lang/Object;)V
    .locals 0

    const/4 p1, 0x1

    .line 134
    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/ConversationInboxPoller;->refreshPoller(Z)V

    return-void
.end method
