.class public Lcom/helpshift/support/SupportAppLifeCycleListener;
.super Ljava/lang/Object;
.source "SupportAppLifeCycleListener.java"

# interfaces
.implements Lcom/helpshift/applifecycle/HSAppLifeCycleListener;


# instance fields
.field data:Lcom/helpshift/support/HSApiData;

.field storage:Lcom/helpshift/support/HSStorage;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 27
    iput-object v0, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->data:Lcom/helpshift/support/HSApiData;

    .line 28
    iput-object v0, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->storage:Lcom/helpshift/support/HSStorage;

    return-void
.end method

.method private tryFetchingServerConfig(Landroid/content/Context;)V
    .locals 5

    .line 88
    invoke-static {p1}, Lcom/helpshift/util/ApplicationUtil;->isApplicationDebuggable(Landroid/content/Context;)Z

    move-result p1

    .line 89
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getDomain()Lcom/helpshift/common/domain/Domain;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v0

    .line 90
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    const-wide/16 v3, 0x3e8

    div-long/2addr v1, v3

    if-nez p1, :cond_0

    .line 92
    invoke-virtual {v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getLastSuccessfulConfigFetchTime()Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    sub-long/2addr v1, v3

    .line 91
    invoke-static {v1, v2}, Ljava/lang/Math;->abs(J)J

    move-result-wide v1

    .line 93
    invoke-virtual {v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getPeriodicFetchInterval()J

    move-result-wide v3

    cmp-long p1, v1, v3

    if-ltz p1, :cond_1

    .line 94
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->fetchServerConfig()V

    .line 96
    :cond_1
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->refreshPoller()V

    return-void
.end method


# virtual methods
.method public onAppBackground(Landroid/content/Context;)V
    .locals 0

    const/4 p1, 0x0

    .line 82
    invoke-static {p1}, Lcom/helpshift/app/AppLifeCycleStateHolder;->setAppInForeground(Z)V

    .line 83
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/conversation/ConversationInboxPoller;->stop()V

    .line 84
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->sendRequestIdsForSuccessfulApiCalls()V

    return-void
.end method

.method public onAppForeground(Landroid/content/Context;)V
    .locals 7

    const/4 v0, 0x1

    .line 36
    invoke-static {v0}, Lcom/helpshift/app/AppLifeCycleStateHolder;->setAppInForeground(Z)V

    .line 37
    iget-object v1, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->data:Lcom/helpshift/support/HSApiData;

    if-nez v1, :cond_0

    .line 38
    new-instance v1, Lcom/helpshift/support/HSApiData;

    invoke-direct {v1, p1}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    iput-object v1, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->data:Lcom/helpshift/support/HSApiData;

    .line 39
    iget-object v1, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->data:Lcom/helpshift/support/HSApiData;

    iget-object v1, v1, Lcom/helpshift/support/HSApiData;->storage:Lcom/helpshift/support/HSStorage;

    iput-object v1, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->storage:Lcom/helpshift/support/HSStorage;

    .line 42
    :cond_0
    iget-object v1, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->data:Lcom/helpshift/support/HSApiData;

    invoke-virtual {v1}, Lcom/helpshift/support/HSApiData;->updateReviewCounter()V

    .line 43
    iget-object v1, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->data:Lcom/helpshift/support/HSApiData;

    invoke-virtual {v1}, Lcom/helpshift/support/HSApiData;->shouldShowReviewPopup()Z

    move-result v1

    if-eqz v1, :cond_1

    .line 44
    new-instance v1, Landroid/content/Intent;

    const-class v2, Lcom/helpshift/support/HSReview;

    invoke-direct {v1, p1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const/high16 v2, 0x10000000

    .line 45
    invoke-virtual {v1, v2}, Landroid/content/Intent;->setFlags(I)Landroid/content/Intent;

    .line 46
    invoke-virtual {p1, v1}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    .line 48
    :cond_1
    invoke-direct {p0, p1}, Lcom/helpshift/support/SupportAppLifeCycleListener;->tryFetchingServerConfig(Landroid/content/Context;)V

    .line 49
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->sendFailedApiCalls()V

    .line 50
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->sendAppStartEvent()V

    .line 53
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->resetPreIssueConversations()V

    .line 55
    invoke-static {p1}, Lcom/helpshift/util/HelpshiftConnectionUtil;->isOnline(Landroid/content/Context;)Z

    move-result p1

    .line 57
    monitor-enter p0

    if-eqz p1, :cond_3

    .line 59
    :try_start_0
    invoke-static {}, Lcom/helpshift/static_classes/ErrorReporting;->isEnabled()Z

    move-result p1

    if-eqz p1, :cond_3

    .line 60
    iget-object p1, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->storage:Lcom/helpshift/support/HSStorage;

    invoke-virtual {p1}, Lcom/helpshift/support/HSStorage;->getLastErrorReportedTime()J

    move-result-wide v1

    .line 61
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getServerTimeDelta()F

    move-result p1

    .line 62
    invoke-static {p1}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/util/TimeUtil;->getAdjustedTimeInMillis(Ljava/lang/Float;)J

    move-result-wide v3

    const/4 p1, 0x0

    sub-long v1, v3, v1

    const-wide/32 v5, 0x5265c00

    cmp-long p1, v1, v5

    if-lez p1, :cond_2

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_3

    .line 65
    invoke-static {}, Lcom/helpshift/util/HSLogger;->getFatalLogsCount()I

    move-result p1

    if-lez p1, :cond_3

    .line 66
    invoke-static {}, Lcom/helpshift/util/HSLogger;->getAll()Ljava/util/List;

    move-result-object p1

    if-eqz p1, :cond_3

    .line 67
    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_3

    .line 68
    iget-object v0, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->storage:Lcom/helpshift/support/HSStorage;

    invoke-virtual {v0, v3, v4}, Lcom/helpshift/support/HSStorage;->setLastErrorReportedTime(J)V

    .line 69
    iget-object v0, p0, Lcom/helpshift/support/SupportAppLifeCycleListener;->data:Lcom/helpshift/support/HSApiData;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/HSApiData;->sendErrorReports(Ljava/util/List;)V

    goto :goto_1

    :catchall_0
    move-exception p1

    goto :goto_2

    .line 73
    :cond_3
    :goto_1
    monitor-exit p0

    return-void

    :goto_2
    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1
.end method
