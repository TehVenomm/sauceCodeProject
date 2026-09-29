.class public Lcom/helpshift/common/domain/Domain;
.super Ljava/lang/Object;
.source "Domain.java"


# instance fields
.field private analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

.field private attachmentFileManagerDM:Lcom/helpshift/common/domain/AttachmentFileManagerDM;

.field private authenticationFailureDM:Lcom/helpshift/account/AuthenticationFailureDM;

.field private autoRetryFailedEventDM:Lcom/helpshift/common/AutoRetryFailedEventDM;

.field private conversationInboxManagerDM:Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

.field private cryptoDM:Lcom/helpshift/crypto/CryptoDM;

.field private customIssueFieldDM:Lcom/helpshift/cif/CustomIssueFieldDM;

.field private delayedThreader:Lcom/helpshift/common/domain/DelayedThreader;

.field private errorReportsDM:Lcom/helpshift/logger/ErrorReportsDM;

.field private faqsDM:Lcom/helpshift/faq/FaqsDM;

.field private localeProviderDM:Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

.field private metaDataDM:Lcom/helpshift/meta/MetaDataDM;

.field private parallelThreader:Lcom/helpshift/common/domain/Threader;

.field private final platform:Lcom/helpshift/common/platform/Platform;

.field private sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

.field private serialThreader:Lcom/helpshift/common/domain/Threader;

.field private uiThreadDelegateDecorator:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

.field private userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;

.field private webSocketAuthDM:Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;)V
    .locals 4

    .line 56
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 57
    iput-object p1, p0, Lcom/helpshift/common/domain/Domain;->platform:Lcom/helpshift/common/platform/Platform;

    .line 58
    new-instance v0, Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    invoke-direct {v0, p0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;-><init>(Lcom/helpshift/common/domain/Domain;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->uiThreadDelegateDecorator:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    .line 60
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff$Builder;

    invoke-direct {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;-><init>()V

    sget-object v1, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v2, 0x5

    .line 61
    invoke-static {v2, v3, v1}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setBaseInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    sget-object v1, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v2, 0x3c

    .line 62
    invoke-static {v2, v3, v1}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMaxInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const/16 v1, 0xa

    .line 63
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMaxAttempts(I)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const v1, 0x3dcccccd    # 0.1f

    .line 64
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRandomness(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const/high16 v1, 0x40000000    # 2.0f

    .line 65
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMultiplier(F)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;->FAILURE:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    .line 66
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRetryPolicy(Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 67
    invoke-virtual {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->build()Lcom/helpshift/common/poller/HttpBackoff;

    move-result-object v0

    .line 69
    new-instance v1, Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-direct {v1, p0, p1, v0}, Lcom/helpshift/common/AutoRetryFailedEventDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/poller/HttpBackoff;)V

    iput-object v1, p0, Lcom/helpshift/common/domain/Domain;->autoRetryFailedEventDM:Lcom/helpshift/common/AutoRetryFailedEventDM;

    .line 70
    new-instance v0, Lcom/helpshift/account/domainmodel/UserManagerDM;

    invoke-direct {v0, p1, p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;

    .line 71
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->init()V

    .line 73
    new-instance v0, Lcom/helpshift/common/domain/HSThreadFactory;

    const-string v1, "core-s"

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/HSThreadFactory;-><init>(Ljava/lang/String;)V

    invoke-static {v0}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor(Ljava/util/concurrent/ThreadFactory;)Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    .line 74
    new-instance v1, Lcom/helpshift/common/domain/BackgroundThreader;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/BackgroundThreader;-><init>(Ljava/util/concurrent/ExecutorService;)V

    iput-object v1, p0, Lcom/helpshift/common/domain/Domain;->serialThreader:Lcom/helpshift/common/domain/Threader;

    .line 76
    new-instance v0, Lcom/helpshift/common/domain/HSThreadFactory;

    const-string v1, "core-p"

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/HSThreadFactory;-><init>(Ljava/lang/String;)V

    invoke-static {v0}, Ljava/util/concurrent/Executors;->newCachedThreadPool(Ljava/util/concurrent/ThreadFactory;)Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    .line 77
    new-instance v1, Lcom/helpshift/common/domain/BackgroundThreader;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/BackgroundThreader;-><init>(Ljava/util/concurrent/ExecutorService;)V

    iput-object v1, p0, Lcom/helpshift/common/domain/Domain;->parallelThreader:Lcom/helpshift/common/domain/Threader;

    .line 79
    new-instance v0, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 81
    new-instance v0, Lcom/helpshift/meta/MetaDataDM;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-direct {v0, p0, p1, v1}, Lcom/helpshift/meta/MetaDataDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->metaDataDM:Lcom/helpshift/meta/MetaDataDM;

    .line 82
    new-instance v0, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    .line 83
    new-instance v0, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain;->userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;

    invoke-direct {v0, p1, p0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserManagerDM;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->conversationInboxManagerDM:Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    .line 84
    new-instance v0, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-direct {v0, v1, p1}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;-><init>(Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->localeProviderDM:Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    .line 85
    new-instance p1, Lcom/helpshift/account/AuthenticationFailureDM;

    invoke-direct {p1, p0}, Lcom/helpshift/account/AuthenticationFailureDM;-><init>(Lcom/helpshift/common/domain/Domain;)V

    iput-object p1, p0, Lcom/helpshift/common/domain/Domain;->authenticationFailureDM:Lcom/helpshift/account/AuthenticationFailureDM;

    return-void
.end method

.method private declared-synchronized getDelayedThreader()Lcom/helpshift/common/domain/DelayedThreader;
    .locals 3

    monitor-enter p0

    .line 97
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->delayedThreader:Lcom/helpshift/common/domain/DelayedThreader;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    .line 98
    new-instance v1, Lcom/helpshift/common/domain/HSThreadFactory;

    const-string v2, "core-d"

    invoke-direct {v1, v2}, Lcom/helpshift/common/domain/HSThreadFactory;-><init>(Ljava/lang/String;)V

    invoke-static {v0, v1}, Ljava/util/concurrent/Executors;->newScheduledThreadPool(ILjava/util/concurrent/ThreadFactory;)Ljava/util/concurrent/ScheduledExecutorService;

    move-result-object v0

    .line 99
    new-instance v1, Lcom/helpshift/common/domain/BackgroundDelayedThreader;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/BackgroundDelayedThreader;-><init>(Ljava/util/concurrent/ScheduledExecutorService;)V

    iput-object v1, p0, Lcom/helpshift/common/domain/Domain;->delayedThreader:Lcom/helpshift/common/domain/DelayedThreader;

    .line 101
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->delayedThreader:Lcom/helpshift/common/domain/DelayedThreader;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 96
    monitor-exit p0

    throw v0
.end method


# virtual methods
.method public getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;
    .locals 1

    .line 117
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    return-object v0
.end method

.method public declared-synchronized getAttachmentFileManagerDM()Lcom/helpshift/common/domain/AttachmentFileManagerDM;
    .locals 2

    monitor-enter p0

    .line 172
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->attachmentFileManagerDM:Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    if-nez v0, :cond_0

    .line 173
    new-instance v0, Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p0, v1}, Lcom/helpshift/common/domain/AttachmentFileManagerDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->attachmentFileManagerDM:Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    .line 175
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->attachmentFileManagerDM:Lcom/helpshift/common/domain/AttachmentFileManagerDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 171
    monitor-exit p0

    throw v0
.end method

.method public getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;
    .locals 1

    .line 213
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->authenticationFailureDM:Lcom/helpshift/account/AuthenticationFailureDM;

    return-object v0
.end method

.method public getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;
    .locals 1

    .line 209
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->autoRetryFailedEventDM:Lcom/helpshift/common/AutoRetryFailedEventDM;

    return-object v0
.end method

.method public getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;
    .locals 1

    .line 109
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->conversationInboxManagerDM:Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    return-object v0
.end method

.method public declared-synchronized getCryptoDM()Lcom/helpshift/crypto/CryptoDM;
    .locals 1

    monitor-enter p0

    .line 144
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->cryptoDM:Lcom/helpshift/crypto/CryptoDM;

    if-nez v0, :cond_0

    .line 145
    new-instance v0, Lcom/helpshift/crypto/CryptoDM;

    invoke-direct {v0}, Lcom/helpshift/crypto/CryptoDM;-><init>()V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->cryptoDM:Lcom/helpshift/crypto/CryptoDM;

    .line 147
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->cryptoDM:Lcom/helpshift/crypto/CryptoDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 143
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getCustomIssueFieldDM()Lcom/helpshift/cif/CustomIssueFieldDM;
    .locals 2

    monitor-enter p0

    .line 136
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->customIssueFieldDM:Lcom/helpshift/cif/CustomIssueFieldDM;

    if-nez v0, :cond_0

    .line 137
    new-instance v0, Lcom/helpshift/cif/CustomIssueFieldDM;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p0, v1}, Lcom/helpshift/cif/CustomIssueFieldDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->customIssueFieldDM:Lcom/helpshift/cif/CustomIssueFieldDM;

    .line 139
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->customIssueFieldDM:Lcom/helpshift/cif/CustomIssueFieldDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 135
    monitor-exit p0

    throw v0
.end method

.method public getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;
    .locals 1

    .line 121
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->uiThreadDelegateDecorator:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    return-object v0
.end method

.method public declared-synchronized getErrorReportsDM()Lcom/helpshift/logger/ErrorReportsDM;
    .locals 2

    monitor-enter p0

    .line 218
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->errorReportsDM:Lcom/helpshift/logger/ErrorReportsDM;

    if-nez v0, :cond_0

    .line 219
    new-instance v0, Lcom/helpshift/logger/ErrorReportsDM;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, p0}, Lcom/helpshift/logger/ErrorReportsDM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->errorReportsDM:Lcom/helpshift/logger/ErrorReportsDM;

    .line 221
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->errorReportsDM:Lcom/helpshift/logger/ErrorReportsDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 217
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getFaqsDM()Lcom/helpshift/faq/FaqsDM;
    .locals 2

    monitor-enter p0

    .line 152
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->faqsDM:Lcom/helpshift/faq/FaqsDM;

    if-nez v0, :cond_0

    .line 153
    new-instance v0, Lcom/helpshift/faq/FaqsDM;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p0, v1}, Lcom/helpshift/faq/FaqsDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->faqsDM:Lcom/helpshift/faq/FaqsDM;

    .line 155
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->faqsDM:Lcom/helpshift/faq/FaqsDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 151
    monitor-exit p0

    throw v0
.end method

.method public getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;
    .locals 1

    .line 167
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->localeProviderDM:Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    return-object v0
.end method

.method public getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;
    .locals 1

    .line 131
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->metaDataDM:Lcom/helpshift/meta/MetaDataDM;

    return-object v0
.end method

.method public getParallelThreader()Lcom/helpshift/common/domain/Threader;
    .locals 1

    .line 93
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->parallelThreader:Lcom/helpshift/common/domain/Threader;

    return-object v0
.end method

.method public getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;
    .locals 1

    .line 113
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    return-object v0
.end method

.method public getSerialThreader()Lcom/helpshift/common/domain/Threader;
    .locals 1

    .line 89
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->serialThreader:Lcom/helpshift/common/domain/Threader;

    return-object v0
.end method

.method public getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;
    .locals 1

    .line 105
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;

    return-object v0
.end method

.method public declared-synchronized getWebSocketAuthDM()Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;
    .locals 2

    monitor-enter p0

    .line 160
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->webSocketAuthDM:Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;

    if-nez v0, :cond_0

    .line 161
    new-instance v0, Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;

    iget-object v1, p0, Lcom/helpshift/common/domain/Domain;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p0, v1}, Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/common/domain/Domain;->webSocketAuthDM:Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;

    .line 163
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->webSocketAuthDM:Lcom/helpshift/auth/domainmodel/WebSocketAuthDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 159
    monitor-exit p0

    throw v0
.end method

.method public runDelayed(Lcom/helpshift/common/domain/F;J)V
    .locals 1

    .line 196
    invoke-direct {p0}, Lcom/helpshift/common/domain/Domain;->getDelayedThreader()Lcom/helpshift/common/domain/DelayedThreader;

    move-result-object v0

    invoke-interface {v0, p1, p2, p3}, Lcom/helpshift/common/domain/DelayedThreader;->thread(Lcom/helpshift/common/domain/F;J)Lcom/helpshift/common/domain/F;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/common/domain/F;->f()V

    return-void
.end method

.method public runDelayedInParallel(Lcom/helpshift/common/domain/F;J)V
    .locals 1

    .line 200
    new-instance v0, Lcom/helpshift/common/domain/Domain$1;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/common/domain/Domain$1;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/domain/F;)V

    invoke-virtual {p0, v0, p2, p3}, Lcom/helpshift/common/domain/Domain;->runDelayed(Lcom/helpshift/common/domain/F;J)V

    return-void
.end method

.method public runOnUI(Lcom/helpshift/common/domain/F;)V
    .locals 1

    .line 187
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->isCurrentThreadUIThread()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 188
    invoke-virtual {p1}, Lcom/helpshift/common/domain/F;->f()V

    goto :goto_0

    .line 191
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getUIThreader()Lcom/helpshift/common/domain/Threader;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/Threader;->thread(Lcom/helpshift/common/domain/F;)Lcom/helpshift/common/domain/F;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/common/domain/F;->f()V

    :goto_0
    return-void
.end method

.method public runParallel(Lcom/helpshift/common/domain/F;)V
    .locals 1

    .line 183
    invoke-virtual {p0}, Lcom/helpshift/common/domain/Domain;->getParallelThreader()Lcom/helpshift/common/domain/Threader;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/Threader;->thread(Lcom/helpshift/common/domain/F;)Lcom/helpshift/common/domain/F;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/common/domain/F;->f()V

    return-void
.end method

.method public runSerial(Lcom/helpshift/common/domain/F;)V
    .locals 1

    .line 179
    invoke-virtual {p0}, Lcom/helpshift/common/domain/Domain;->getSerialThreader()Lcom/helpshift/common/domain/Threader;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/Threader;->thread(Lcom/helpshift/common/domain/F;)Lcom/helpshift/common/domain/F;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/common/domain/F;->f()V

    return-void
.end method

.method public setDelegate(Lcom/helpshift/delegate/RootDelegate;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 126
    iget-object v0, p0, Lcom/helpshift/common/domain/Domain;->uiThreadDelegateDecorator:Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    invoke-virtual {v0, p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->setDelegate(Lcom/helpshift/delegate/RootDelegate;)V

    :cond_0
    return-void
.end method
