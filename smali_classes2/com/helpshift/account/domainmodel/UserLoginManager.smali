.class public Lcom/helpshift/account/domainmodel/UserLoginManager;
.super Ljava/lang/Object;
.source "UserLoginManager.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_ULoginM"


# instance fields
.field private coreApi:Lcom/helpshift/CoreApi;

.field private domain:Lcom/helpshift/common/domain/Domain;

.field private platform:Lcom/helpshift/common/platform/Platform;


# direct methods
.method public constructor <init>(Lcom/helpshift/CoreApi;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    .line 24
    iput-object p2, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->domain:Lcom/helpshift/common/domain/Domain;

    .line 25
    iput-object p3, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->platform:Lcom/helpshift/common/platform/Platform;

    return-void
.end method

.method private cleanUpActiveUser()V
    .locals 1

    .line 96
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v0

    .line 97
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getActiveConversationInboxDM()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    .line 99
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->clearPushNotifications()V

    .line 101
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/ConversationInboxPoller;->stop()V

    return-void
.end method

.method private clearConfigRouteETag()V
    .locals 2

    .line 215
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/domain/network/NetworkConstants;->SUPPORT_CONFIG_ROUTE:Ljava/lang/String;

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->removeETag(Ljava/lang/String;)V

    return-void
.end method

.method private deleteUser(Lcom/helpshift/account/domainmodel/UserDM;)Z
    .locals 4

    .line 122
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    .line 123
    invoke-virtual {v0, p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->deleteUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 126
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getRedactionDAO()Lcom/helpshift/redaction/RedactionDAO;

    move-result-object v1

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v1, v2, v3}, Lcom/helpshift/redaction/RedactionDAO;->deleteRedactionDetail(J)V

    .line 128
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v1

    invoke-virtual {v1, p1}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->deleteConversations(Lcom/helpshift/account/domainmodel/UserDM;)V

    :cond_0
    return v0
.end method

.method private setUpActiveUser()V
    .locals 4

    .line 105
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v0

    .line 106
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getActiveConversationInboxDM()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    .line 108
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->showPushNotifications()V

    .line 110
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;

    move-result-object v1

    .line 112
    sget-object v2, Lcom/helpshift/account/domainmodel/UserSetupState;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->getState()Lcom/helpshift/account/domainmodel/UserSetupState;

    move-result-object v3

    if-ne v2, v3, :cond_0

    .line 113
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/ConversationInboxPoller;->startAppPoller(Z)V

    goto :goto_0

    .line 117
    :cond_0
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserSetupDM;->startSetup()V

    :goto_0
    return-void
.end method


# virtual methods
.method public clearAnonymousUser()Z
    .locals 3

    .line 134
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    .line 135
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 138
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->isActiveUser()Z

    move-result v2

    if-eqz v2, :cond_0

    const-string v0, "Helpshift_ULoginM"

    const-string v1, "clearAnonymousUser should be called when a logged-in user is active"

    .line 139
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    return v0

    .line 143
    :cond_0
    invoke-direct {p0, v1}, Lcom/helpshift/account/domainmodel/UserLoginManager;->deleteUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 145
    invoke-virtual {v0, v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->clearAnonymousUser(Lcom/helpshift/account/domainmodel/UserDM;)V

    :cond_1
    const/4 v0, 0x1

    return v0
.end method

.method public clearPersonallyIdentifiableInformation()V
    .locals 3

    .line 183
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->isSDKSessionActive()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "Helpshift_ULoginM"

    const-string v1, "clear PII should not be called after starting a Helpshift session"

    .line 184
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 191
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    .line 192
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    .line 193
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 194
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->logout()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 197
    invoke-direct {p0, v1}, Lcom/helpshift/account/domainmodel/UserLoginManager;->deleteUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    .line 199
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getCampaignModuleAPIs()Lcom/helpshift/providers/ICampaignsModuleAPIs;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/providers/ICampaignsModuleAPIs;->logout()V

    goto :goto_0

    .line 205
    :cond_1
    invoke-virtual {v0, v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->resetNameAndEmail(Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 206
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveName(Ljava/lang/String;)V

    .line 207
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveEmail(Ljava/lang/String;)V

    :cond_2
    :goto_0
    return-void
.end method

.method public login(Lcom/helpshift/HelpshiftUser;)Z
    .locals 6

    .line 29
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    .line 37
    invoke-virtual {v0, p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->isActiveUser(Lcom/helpshift/HelpshiftUser;)Z

    move-result v1

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v1, :cond_5

    .line 42
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    .line 43
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object v4

    if-nez v4, :cond_0

    .line 44
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getAuthToken()Ljava/lang/String;

    move-result-object v5

    if-nez v5, :cond_0

    goto :goto_2

    :cond_0
    if-eqz v4, :cond_2

    .line 48
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getAuthToken()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v4, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_1

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v2, 0x1

    goto :goto_2

    .line 50
    :cond_2
    :goto_1
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getAuthToken()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateAuthToken(Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/String;)V

    goto :goto_0

    .line 55
    :goto_2
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object v4

    .line 56
    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_3

    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getName()Ljava/lang/String;

    move-result-object v5

    invoke-static {v5}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v5

    if-nez v5, :cond_8

    .line 57
    :cond_3
    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v5

    if-nez v5, :cond_4

    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_8

    .line 58
    :cond_4
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateUserName(Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/String;)V

    goto :goto_4

    .line 63
    :cond_5
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->isSDKSessionActive()Z

    move-result v1

    if-eqz v1, :cond_6

    const-string p1, "Helpshift_ULoginM"

    const-string v0, "Login should be called before starting a Helpshift session"

    .line 64
    invoke-static {p1, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    return v2

    .line 72
    :cond_6
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->cleanUpActiveUser()V

    .line 74
    invoke-virtual {v0, p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->login(Lcom/helpshift/HelpshiftUser;)V

    .line 75
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getInactiveLoggedInUsers()Ljava/util/List;

    move-result-object p1

    .line 76
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_3
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_7

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/account/domainmodel/UserDM;

    .line 77
    invoke-direct {p0, v0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->deleteUser(Lcom/helpshift/account/domainmodel/UserDM;)Z

    goto :goto_3

    .line 80
    :cond_7
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->setUpActiveUser()V

    const/4 v2, 0x1

    .line 86
    :cond_8
    :goto_4
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->clearConfigRouteETag()V

    if-eqz v2, :cond_9

    .line 89
    iget-object p1, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->onUserAuthenticationUpdated()V

    :cond_9
    return v3
.end method

.method public logout()Z
    .locals 2

    .line 152
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->isSDKSessionActive()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "Helpshift_ULoginM"

    const-string v1, "Logout should be called before starting a Helpshift session"

    .line 153
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    return v0

    .line 157
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    .line 158
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 159
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->isAnonymousUser()Z

    move-result v1

    if-eqz v1, :cond_1

    const/4 v0, 0x1

    return v0

    .line 164
    :cond_1
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->cleanUpActiveUser()V

    .line 166
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->loginWithAnonymousUser()Z

    move-result v0

    .line 168
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->setUpActiveUser()V

    if-eqz v0, :cond_2

    .line 172
    invoke-direct {p0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->clearConfigRouteETag()V

    .line 174
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserLoginManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->onUserAuthenticationUpdated()V

    :cond_2
    return v0
.end method
