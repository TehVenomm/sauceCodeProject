.class public Lcom/helpshift/JavaCore;
.super Ljava/lang/Object;
.source "JavaCore.java"

# interfaces
.implements Lcom/helpshift/CoreApi;


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_JavaCore"


# instance fields
.field final analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

.field private domain:Lcom/helpshift/common/domain/Domain;

.field private isSDKSessionActive:Z

.field private final metaDataDM:Lcom/helpshift/meta/MetaDataDM;

.field private final parallelThreader:Lcom/helpshift/common/domain/Threader;

.field final platform:Lcom/helpshift/common/platform/Platform;

.field final sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

.field private userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/Platform;)V
    .locals 1

    .line 64
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 62
    iput-boolean v0, p0, Lcom/helpshift/JavaCore;->isSDKSessionActive:Z

    .line 65
    iput-object p1, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    .line 66
    new-instance v0, Lcom/helpshift/common/domain/Domain;

    invoke-direct {v0, p1}, Lcom/helpshift/common/domain/Domain;-><init>(Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    .line 67
    iget-object p1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/JavaCore;->userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;

    .line 68
    iget-object p1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getParallelThreader()Lcom/helpshift/common/domain/Threader;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/JavaCore;->parallelThreader:Lcom/helpshift/common/domain/Threader;

    .line 69
    iget-object p1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/JavaCore;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 70
    iget-object p1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/JavaCore;->analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    .line 71
    iget-object p1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/JavaCore;->metaDataDM:Lcom/helpshift/meta/MetaDataDM;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/JavaCore;)Lcom/helpshift/account/domainmodel/UserManagerDM;
    .locals 0

    .line 52
    iget-object p0, p0, Lcom/helpshift/JavaCore;->userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/JavaCore;)Lcom/helpshift/common/domain/Domain;
    .locals 0

    .line 52
    iget-object p0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    return-object p0
.end method

.method private runParallel(Lcom/helpshift/common/domain/F;)V
    .locals 1

    .line 408
    iget-object v0, p0, Lcom/helpshift/JavaCore;->parallelThreader:Lcom/helpshift/common/domain/Threader;

    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/Threader;->thread(Lcom/helpshift/common/domain/F;)Lcom/helpshift/common/domain/F;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/common/domain/F;->f()V

    return-void
.end method


# virtual methods
.method public declared-synchronized clearAnonymousUser()Z
    .locals 3

    monitor-enter p0

    .line 142
    :try_start_0
    new-instance v0, Lcom/helpshift/account/domainmodel/UserLoginManager;

    iget-object v1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p0, v1, v2}, Lcom/helpshift/account/domainmodel/UserLoginManager;-><init>(Lcom/helpshift/CoreApi;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->clearAnonymousUser()Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public fetchServerConfig()V
    .locals 1

    .line 196
    new-instance v0, Lcom/helpshift/JavaCore$1;

    invoke-direct {v0, p0}, Lcom/helpshift/JavaCore$1;-><init>(Lcom/helpshift/JavaCore;)V

    invoke-direct {p0, v0}, Lcom/helpshift/JavaCore;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 1

    .line 111
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    .line 112
    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    return-object v0
.end method

.method public getActiveConversationOrPreIssue()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 1

    .line 117
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationOrPreIssue()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    return-object v0
.end method

.method public getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;
    .locals 1

    .line 248
    iget-object v0, p0, Lcom/helpshift/JavaCore;->analyticsEventDM:Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    return-object v0
.end method

.method public getAttachmentFileManagerDM()Lcom/helpshift/common/domain/AttachmentFileManagerDM;
    .locals 1

    .line 362
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAttachmentFileManagerDM()Lcom/helpshift/common/domain/AttachmentFileManagerDM;

    move-result-object v0

    return-object v0
.end method

.method public getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;
    .locals 1

    .line 367
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    return-object v0
.end method

.method public getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;
    .locals 1

    .line 342
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getActiveConversationInboxDM()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    return-object v0
.end method

.method getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;
    .locals 1

    .line 399
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v0

    return-object v0
.end method

.method public getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;
    .locals 1

    .line 303
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v0

    return-object v0
.end method

.method public getConversationInfoViewModel(Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;)Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;
    .locals 2

    .line 101
    new-instance v0, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;

    iget-object v1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-direct {v0, v1, p1}, Lcom/helpshift/conversation/viewmodel/ConversationInfoVM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/conversation/activeconversation/ConversationInfoRenderer;)V

    return-object v0
.end method

.method public getConversationalViewModel(ZLjava/lang/Long;Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;Z)Lcom/helpshift/conversation/viewmodel/ConversationalVM;
    .locals 9

    .line 88
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    .line 89
    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getViewableConversation(ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v5

    .line 90
    new-instance p2, Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    iget-object v2, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v3, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    .line 92
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v4

    move-object v1, p2

    move-object v6, p3

    move v7, p1

    move v8, p4

    invoke-direct/range {v1 .. v8}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/activeconversation/ViewableConversation;Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;ZZ)V

    return-object p2
.end method

.method public getCryptoDM()Lcom/helpshift/crypto/CryptoDM;
    .locals 1

    .line 352
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getCryptoDM()Lcom/helpshift/crypto/CryptoDM;

    move-result-object v0

    return-object v0
.end method

.method public getCustomIssueFieldDM()Lcom/helpshift/cif/CustomIssueFieldDM;
    .locals 1

    .line 293
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getCustomIssueFieldDM()Lcom/helpshift/cif/CustomIssueFieldDM;

    move-result-object v0

    return-object v0
.end method

.method public getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;
    .locals 1

    .line 264
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object v0

    return-object v0
.end method

.method public getDomain()Lcom/helpshift/common/domain/Domain;
    .locals 1

    .line 76
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    return-object v0
.end method

.method public getErrorReportsDM()Lcom/helpshift/logger/ErrorReportsDM;
    .locals 1

    .line 404
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getErrorReportsDM()Lcom/helpshift/logger/ErrorReportsDM;

    move-result-object v0

    return-object v0
.end method

.method public getFaqDM()Lcom/helpshift/faq/FaqsDM;
    .locals 1

    .line 347
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getFaqsDM()Lcom/helpshift/faq/FaqsDM;

    move-result-object v0

    return-object v0
.end method

.method public getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;
    .locals 1

    .line 357
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v0

    return-object v0
.end method

.method public getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;
    .locals 1

    .line 288
    iget-object v0, p0, Lcom/helpshift/JavaCore;->metaDataDM:Lcom/helpshift/meta/MetaDataDM;

    return-object v0
.end method

.method public getNewConversationViewModel(Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;)Lcom/helpshift/conversation/viewmodel/NewConversationVM;
    .locals 4

    .line 81
    new-instance v0, Lcom/helpshift/conversation/viewmodel/NewConversationVM;

    iget-object v1, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v3

    invoke-direct {v0, v1, v2, v3, p1}, Lcom/helpshift/conversation/viewmodel/NewConversationVM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;)V

    return-object v0
.end method

.method public getNotificationCountAsync(Lcom/helpshift/common/FetchDataFromThread;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/FetchDataFromThread<",
            "Lcom/helpshift/util/ValuePair<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Boolean;",
            ">;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 319
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/JavaCore$4;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/JavaCore$4;-><init>(Lcom/helpshift/JavaCore;Lcom/helpshift/common/FetchDataFromThread;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public getNotificationCountSync()I
    .locals 1

    .line 313
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getNotificationCountSync()I

    move-result v0

    return v0
.end method

.method public getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;
    .locals 1

    .line 298
    iget-object v0, p0, Lcom/helpshift/JavaCore;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    return-object v0
.end method

.method public getScreenshotPreviewModel(Lcom/helpshift/conversation/activeconversation/ScreenshotPreviewRenderer;)Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;
    .locals 2

    .line 106
    new-instance v0, Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;

    iget-object v1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-direct {v0, v1, p1}, Lcom/helpshift/conversation/viewmodel/ScreenshotPreviewVM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/conversation/activeconversation/ScreenshotPreviewRenderer;)V

    return-object v0
.end method

.method public getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;
    .locals 1

    .line 283
    iget-object v0, p0, Lcom/helpshift/JavaCore;->userManagerDM:Lcom/helpshift/account/domainmodel/UserManagerDM;

    return-object v0
.end method

.method public getUserSetupVM(Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;)Lcom/helpshift/conversation/usersetup/UserSetupVM;
    .locals 4

    .line 122
    new-instance v0, Lcom/helpshift/conversation/usersetup/UserSetupVM;

    iget-object v1, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v3

    invoke-virtual {v3}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;

    move-result-object v3

    invoke-direct {v0, v1, v2, v3, p1}, Lcom/helpshift/conversation/usersetup/UserSetupVM;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserSetupDM;Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;)V

    return-object v0
.end method

.method public handlePushNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 308
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    invoke-virtual {v0, p1, p2, p3}, Lcom/helpshift/conversation/domainmodel/ConversationController;->handlePushNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public isActiveConversationActionable()Z
    .locals 1

    .line 127
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->isActiveConversationActionable()Z

    move-result v0

    return v0
.end method

.method public isSDKSessionActive()Z
    .locals 1

    .line 132
    iget-boolean v0, p0, Lcom/helpshift/JavaCore;->isSDKSessionActive:Z

    return v0
.end method

.method public declared-synchronized login(Lcom/helpshift/HelpshiftUser;)Z
    .locals 3

    monitor-enter p0

    .line 146
    :try_start_0
    new-instance v0, Lcom/helpshift/account/domainmodel/UserLoginManager;

    iget-object v1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p0, v1, v2}, Lcom/helpshift/account/domainmodel/UserLoginManager;-><init>(Lcom/helpshift/CoreApi;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    invoke-virtual {v0, p1}, Lcom/helpshift/account/domainmodel/UserLoginManager;->login(Lcom/helpshift/HelpshiftUser;)Z

    move-result p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized logout()Z
    .locals 3

    monitor-enter p0

    .line 159
    :try_start_0
    new-instance v0, Lcom/helpshift/account/domainmodel/UserLoginManager;

    iget-object v1, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, p0, v1, v2}, Lcom/helpshift/account/domainmodel/UserLoginManager;-><init>(Lcom/helpshift/CoreApi;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserLoginManager;->logout()Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public onSDKSessionEnded()V
    .locals 1

    const/4 v0, 0x0

    .line 190
    iput-boolean v0, p0, Lcom/helpshift/JavaCore;->isSDKSessionActive:Z

    .line 191
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->sessionEnded()V

    return-void
.end method

.method public onSDKSessionStarted()V
    .locals 1

    const/4 v0, 0x1

    .line 184
    iput-boolean v0, p0, Lcom/helpshift/JavaCore;->isSDKSessionActive:Z

    .line 185
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->sessionBegan()V

    return-void
.end method

.method public refreshPoller()V
    .locals 2

    .line 214
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/ConversationInboxPoller;->refreshPoller(Z)V

    return-void
.end method

.method public resetPreIssueConversations()V
    .locals 1

    .line 395
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->resetPreIssueConversations()V

    return-void
.end method

.method public resetUsersSyncStatusAndStartSetupForActiveUser()V
    .locals 1

    .line 413
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->resetSyncStateForAllUsers()V

    .line 414
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->startSetup()V

    return-void
.end method

.method public sendAnalyticsEvent()V
    .locals 1

    .line 233
    new-instance v0, Lcom/helpshift/JavaCore$2;

    invoke-direct {v0, p0}, Lcom/helpshift/JavaCore$2;-><init>(Lcom/helpshift/JavaCore;)V

    invoke-direct {p0, v0}, Lcom/helpshift/JavaCore;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public sendAppStartEvent()V
    .locals 1

    .line 253
    new-instance v0, Lcom/helpshift/JavaCore$3;

    invoke-direct {v0, p0}, Lcom/helpshift/JavaCore$3;-><init>(Lcom/helpshift/JavaCore;)V

    invoke-direct {p0, v0}, Lcom/helpshift/JavaCore;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public sendFailedApiCalls()V
    .locals 1

    .line 271
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getFaqDM()Lcom/helpshift/faq/FaqsDM;

    .line 272
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    .line 273
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    .line 276
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;

    .line 277
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    .line 278
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->sendAllEvents()V

    return-void
.end method

.method public sendRequestIdsForSuccessfulApiCalls()V
    .locals 2

    .line 372
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/JavaCore$5;

    invoke-direct {v1, p0}, Lcom/helpshift/JavaCore$5;-><init>(Lcom/helpshift/JavaCore;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public setDelegateListener(Lcom/helpshift/delegate/RootDelegate;)V
    .locals 1

    .line 137
    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/domain/Domain;->setDelegate(Lcom/helpshift/delegate/RootDelegate;)V

    return-void
.end method

.method public setNameAndEmail(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 153
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveName(Ljava/lang/String;)V

    .line 154
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveEmail(Ljava/lang/String;)V

    return-void
.end method

.method public setPushToken(Ljava/lang/String;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 169
    :cond_0
    iget-object v0, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/Device;->getPushToken()Ljava/lang/String;

    move-result-object v0

    .line 170
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    return-void

    .line 174
    :cond_1
    iget-object v0, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/helpshift/common/platform/Device;->setPushToken(Ljava/lang/String;)V

    .line 177
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->resetPushTokenSyncStatusForUsers()V

    .line 179
    invoke-virtual {p0}, Lcom/helpshift/JavaCore;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->sendPushToken()V

    return-void
.end method

.method public updateApiConfig(Lcom/helpshift/configuration/dto/RootApiConfig;)V
    .locals 2

    .line 224
    iget-object v0, p0, Lcom/helpshift/JavaCore;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v0, p1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->updateApiConfig(Lcom/helpshift/configuration/dto/RootApiConfig;)V

    .line 225
    iget-object v0, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->enableFullPrivacy:Ljava/lang/Boolean;

    if-eqz v0, :cond_0

    iget-object p1, p1, Lcom/helpshift/configuration/dto/RootApiConfig;->enableFullPrivacy:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 227
    new-instance p1, Lcom/helpshift/account/domainmodel/UserLoginManager;

    iget-object v0, p0, Lcom/helpshift/JavaCore;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/JavaCore;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p1, p0, v0, v1}, Lcom/helpshift/account/domainmodel/UserLoginManager;-><init>(Lcom/helpshift/CoreApi;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserLoginManager;->clearPersonallyIdentifiableInformation()V

    :cond_0
    return-void
.end method

.method public updateInstallConfig(Lcom/helpshift/configuration/dto/RootInstallConfig;)V
    .locals 1

    .line 219
    iget-object v0, p0, Lcom/helpshift/JavaCore;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v0, p1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->updateInstallConfig(Lcom/helpshift/configuration/dto/RootInstallConfig;)V

    return-void
.end method
