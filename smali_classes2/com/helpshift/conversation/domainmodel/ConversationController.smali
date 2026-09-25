.class public Lcom/helpshift/conversation/domainmodel/ConversationController;
.super Ljava/lang/Object;
.source "ConversationController.java"

# interfaces
.implements Lcom/helpshift/common/AutoRetriableDM;
.implements Lcom/helpshift/account/domainmodel/IUserSyncExecutor;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;,
        Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;
    }
.end annotation


# static fields
.field private static final ACTIVE_ISSUE_NOTIFICATION_COUNT_TIMEOUT:J = 0xea60L

.field private static final CREATE_ISSUE_ROUTE:Ljava/lang/String; = "/issues/"

.field private static final CREATE_ISSUE_UNIQUE_MAPPING_KEY:Ljava/lang/String; = "issue_default_unique_key"

.field private static final CREATE_PRE_ISSUE_ROUTE:Ljava/lang/String; = "/preissues/"

.field private static final CREATE_PRE_ISSUE_UNIQUE_MAPPING_KEY:Ljava/lang/String; = "preissue_default_unique_key"

.field private static final INACTIVE_ISSUES_NOTIFICATION_COUNT_TIMEOUT:J = 0x493e0L

.field public static final MESSAGES_PAGE_SIZE:J = 0x64L

.field private static final TAG:Ljava/lang/String; = "Helpshift_ConvInboxDM"

.field static final fetchConversationUpdatesLock:Ljava/lang/Object;


# instance fields
.field private aliveViewableConversation:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/conversation/activeconversation/ViewableConversation;",
            ">;"
        }
    .end annotation
.end field

.field final conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

.field private final conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

.field private final conversationInboxPoller:Lcom/helpshift/conversation/ConversationInboxPoller;

.field public final conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field private conversationViewState:I

.field final domain:Lcom/helpshift/common/domain/Domain;

.field private final faqSearchDM:Lcom/helpshift/faq/domainmodel/FAQSearchDM;

.field public fetchConversationUpdatesListenerReference:Ljava/util/concurrent/atomic/AtomicReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/atomic/AtomicReference<",
            "Lcom/helpshift/common/FetchDataFromThread<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Integer;",
            ">;>;"
        }
    .end annotation
.end field

.field private inAppNotificationMessageCountMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation
.end field

.field inProgressPreIssueCreators:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/Long;",
            "Lcom/helpshift/common/domain/One;",
            ">;"
        }
    .end annotation
.end field

.field private isCreateConversationInProgress:Z

.field private lastNotifCountFetchTime:J

.field private final liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

.field final platform:Lcom/helpshift/common/platform/Platform;

.field private remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

.field private final sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

.field private shouldDropCustomMetadata:Z

.field private startNewConversationListenerRef:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;",
            ">;"
        }
    .end annotation
.end field

.field private userCanReadMessages:Z

.field final userDM:Lcom/helpshift/account/domainmodel/UserDM;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 114
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesLock:Ljava/lang/Object;

    return-void
.end method

.method public constructor <init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 4

    .line 145
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/16 v0, 0x0

    .line 111
    iput-wide v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->lastNotifCountFetchTime:J

    const/4 v0, 0x0

    .line 130
    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesListenerReference:Ljava/util/concurrent/atomic/AtomicReference;

    .line 134
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inProgressPreIssueCreators:Ljava/util/HashMap;

    const/4 v0, -0x1

    .line 139
    iput v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationViewState:I

    .line 143
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentHashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inAppNotificationMessageCountMap:Ljava/util/Map;

    .line 146
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    .line 147
    iput-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    .line 148
    iput-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 149
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationInboxDAO()Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    .line 150
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    .line 151
    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getFAQSearchDM()Lcom/helpshift/faq/domainmodel/FAQSearchDM;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->faqSearchDM:Lcom/helpshift/faq/domainmodel/FAQSearchDM;

    .line 152
    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 153
    new-instance v0, Lcom/helpshift/conversation/ConversationInboxPoller;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getPoller()Lcom/helpshift/common/domain/Poller;

    move-result-object v2

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-direct {v0, p3, v1, v2, v3}, Lcom/helpshift/conversation/ConversationInboxPoller;-><init>(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;Lcom/helpshift/common/domain/Poller;Lcom/helpshift/conversation/dao/ConversationDAO;)V

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxPoller:Lcom/helpshift/conversation/ConversationInboxPoller;

    .line 154
    new-instance v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-direct {v0, p2, p1}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    .line 155
    new-instance v0, Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-direct {v0, p1, p2, p3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;)V

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    .line 156
    new-instance v0, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-direct {v0, p1, p2, p3, v1}, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/conversation/domainmodel/ConversationController;Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;
    .locals 0

    .line 95
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object p0

    return-object p0
.end method

.method private buildForwardPollerNetwork()Lcom/helpshift/common/domain/network/Network;
    .locals 4

    .line 703
    new-instance v0, Lcom/helpshift/common/domain/network/POSTNetwork;

    const-string v1, "/conversations/updates/"

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v0, v1, v2, v3}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 704
    new-instance v1, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {v1, v0}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 705
    new-instance v0, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/network/FailedAPICallNetworkDecorator;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 706
    new-instance v1, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v1, v0, v2}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 707
    new-instance v0, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {v0, v1}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    return-object v0
.end method

.method private buildForwardPollerRequestData(Ljava/lang/String;)Lcom/helpshift/common/platform/network/RequestData;
    .locals 2

    .line 712
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-static {v0}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    .line 714
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    const-string v1, "cursor"

    .line 715
    invoke-virtual {v0, v1, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 718
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getLastViewableSyncedConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 721
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_1

    const-string v1, "issue_id"

    .line 722
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v0, v1, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 724
    :cond_1
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_2

    const-string v1, "preissue_id"

    .line 725
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-virtual {v0, v1, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    :goto_0
    const-string p1, "ucrm"

    .line 728
    iget-boolean v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userCanReadMessages:Z

    invoke-static {v1}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p1, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 729
    new-instance p1, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {p1, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    return-object p1
.end method

.method private canShowNotificationForConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 6

    const/4 v0, 0x0

    if-eqz p1, :cond_4

    .line 963
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 964
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    iget-wide v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    cmp-long v5, v1, v3

    if-nez v5, :cond_4

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    .line 965
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_1

    .line 969
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 970
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isVisibleOnUI()Z

    move-result v2

    if-eqz v2, :cond_1

    return v0

    :cond_1
    const/4 v0, 0x1

    if-nez v1, :cond_2

    .line 980
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    goto :goto_0

    .line 983
    :cond_2
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    :goto_0
    if-eqz v1, :cond_3

    .line 989
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    iget-object v0, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    :cond_3
    return v0

    :cond_4
    :goto_1
    return v0
.end method

.method private checkAndGenerateNotification(Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    .line 890
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 891
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->shouldShowInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 892
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    iput-wide v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 893
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getMessageCountForShowingInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)I

    move-result v1

    .line 894
    invoke-direct {p0, v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->showInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;I)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method private checkAndTryToUploadImage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 2

    if-eqz p2, :cond_0

    .line 399
    iget-object v0, p2, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    .line 401
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, p1, p2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendScreenshot(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 408
    :catch_0
    invoke-virtual {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveImageAttachmentDraft(Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    :cond_0
    return-void
.end method

.method private checkForReOpen(Ljava/util/List;)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    .line 1013
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromUIOrStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    const/4 v1, 0x0

    const/4 v2, 0x0

    if-eqz v0, :cond_1

    .line 1017
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v3

    if-nez v3, :cond_0

    .line 1018
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const/4 v2, 0x1

    .line 1025
    :cond_1
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v3

    .line 1026
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_2
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1027
    iget-object v5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v5}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    iput-wide v5, v4, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    if-eqz v3, :cond_3

    .line 1030
    invoke-virtual {v3, v4}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isActiveConversationEqual(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v5

    if-eqz v5, :cond_3

    .line 1031
    iget v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationViewState:I

    invoke-virtual {v3, v4, v1, v2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->checkForReopen(ILjava/lang/String;Z)Z

    move-result v4

    goto :goto_2

    .line 1036
    :cond_3
    iget-object v5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget v6, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationViewState:I

    .line 1037
    invoke-virtual {v5, v4, v6, v1, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->checkForReOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;ILjava/lang/String;Z)Z

    move-result v4

    :goto_2
    if-eqz v4, :cond_2

    .line 1043
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->shouldShowInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v4

    if-eqz v4, :cond_2

    .line 1044
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getMessageCountForShowingInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)I

    move-result v4

    .line 1045
    invoke-direct {p0, v0, v4}, Lcom/helpshift/conversation/domainmodel/ConversationController;->showInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;I)V

    goto :goto_1

    :cond_4
    return-void
.end method

.method private clearInAppNotificationCountCache()V
    .locals 1

    .line 926
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inAppNotificationMessageCountMap:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->clear()V

    return-void
.end method

.method private clearRequestIdForPendingCreateConversationCall(Ljava/util/List;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    .line 854
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v0

    const-string v1, "/issues/"

    const-string v2, "issue_default_unique_key"

    .line 855
    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getPendingRequestId(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 857
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v1

    const-string v2, "/preissues/"

    const-string v3, "preissue_default_unique_key"

    .line 858
    invoke-interface {v1, v2, v3}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getPendingRequestId(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    if-nez v0, :cond_0

    if-eqz v1, :cond_3

    .line 861
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_3

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 862
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdRequestId:Ljava/lang/String;

    if-eqz v3, :cond_1

    .line 863
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdRequestId:Ljava/lang/String;

    invoke-virtual {v3, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 864
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v2

    const-string v3, "/issues/"

    const-string v4, "issue_default_unique_key"

    invoke-interface {v2, v3, v4}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->deletePendingRequestId(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 867
    :cond_2
    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdRequestId:Ljava/lang/String;

    invoke-virtual {v2, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 868
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v2

    const-string v3, "/preissues/"

    const-string v4, "preissue_default_unique_key"

    invoke-interface {v2, v3, v4}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->deletePendingRequestId(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_3
    return-void
.end method

.method private deleteConversationsAndMessages()V
    .locals 6

    .line 180
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    .line 182
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v2, v0, v1}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v2

    .line 183
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 184
    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v4}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/Long;->longValue()J

    move-result-wide v4

    iput-wide v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 185
    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v4, v3}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->deleteCachedScreenshotFiles(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    goto :goto_0

    .line 188
    :cond_0
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v2, v0, v1}, Lcom/helpshift/conversation/dao/ConversationDAO;->deleteConversations(J)V

    return-void
.end method

.method private fetchConversationHistory()V
    .locals 2

    .line 2006
    sget-object v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesLock:Ljava/lang/Object;

    monitor-enter v0

    .line 2007
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    invoke-virtual {v1}, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->loadMoreMessages()Z

    .line 2008
    monitor-exit v0

    return-void

    :catchall_0
    move-exception v1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method

.method private fetchConversationUpdatesInternal(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationInbox;
    .locals 4

    .line 734
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->buildForwardPollerNetwork()Lcom/helpshift/common/domain/network/Network;

    move-result-object v0

    .line 735
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->buildForwardPollerRequestData(Ljava/lang/String;)Lcom/helpshift/common/platform/network/RequestData;

    move-result-object p1

    .line 737
    :try_start_0
    invoke-interface {v0, p1}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object v0

    .line 738
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object v1

    .line 739
    iget-object v0, v0, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {v1, v0}, Lcom/helpshift/common/platform/network/ResponseParser;->parseConversationInbox(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationInbox;

    move-result-object v0
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    .line 755
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-boolean v3, v0, Lcom/helpshift/conversation/dto/ConversationInbox;->issueExists:Z

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateIssueExists(Lcom/helpshift/account/domainmodel/UserDM;Z)V

    .line 758
    iget-object p1, p1, Lcom/helpshift/common/platform/network/RequestData;->body:Ljava/util/Map;

    const-string v1, "cursor"

    invoke-interface {p1, v1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_0

    iget-object p1, v0, Lcom/helpshift/conversation/dto/ConversationInbox;->hasOlderMessages:Ljava/lang/Boolean;

    if-eqz p1, :cond_0

    .line 760
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    iget-object v3, v0, Lcom/helpshift/conversation/dto/ConversationInbox;->hasOlderMessages:Ljava/lang/Boolean;

    invoke-virtual {v3}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v3

    invoke-interface {p1, v1, v2, v3}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveHasOlderMessages(JZ)V

    .line 764
    :cond_0
    iget-object p1, v0, Lcom/helpshift/conversation/dto/ConversationInbox;->conversations:Ljava/util/List;

    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->merge(Ljava/util/List;)V

    .line 769
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    iget-object v3, v0, Lcom/helpshift/conversation/dto/ConversationInbox;->cursor:Ljava/lang/String;

    invoke-interface {p1, v1, v2, v3}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveConversationInboxTimestamp(JLjava/lang/String;)V

    return-object v0

    :catch_0
    move-exception p1

    .line 742
    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq v0, v1, :cond_1

    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v1, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-eq v0, v1, :cond_1

    .line 746
    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    instance-of v0, v0, Lcom/helpshift/common/exception/NetworkException;

    if-eqz v0, :cond_2

    .line 747
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 748
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isVisibleOnUI()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 749
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getConversationVMCallback()Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->onConversationInboxPollFailure()V

    goto :goto_0

    .line 744
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 752
    :cond_2
    :goto_0
    throw p1
.end method

.method private findMatchingIssueTypeConversation(Lcom/helpshift/conversation/activeconversation/ViewableConversation;Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 2
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 1938
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1939
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {p2, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    return-object v0

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method private findMatchingPreIssueTypeConversation(Lcom/helpshift/conversation/activeconversation/ViewableConversation;Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 2
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 1927
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1928
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-virtual {p2, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    return-object v0

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method private getActiveConversationFromUIOrStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 3

    .line 1681
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v0

    if-nez v0, :cond_1

    .line 1683
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 1687
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    iput-wide v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    goto :goto_0

    :cond_0
    move-object v0, v1

    :goto_0
    return-object v0

    .line 1691
    :cond_1
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    return-object v0
.end method

.method private getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;
    .locals 1

    .line 882
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 886
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    return-object v0

    :cond_1
    :goto_0
    const/4 v0, 0x0

    return-object v0
.end method

.method private getAliveViewableConversation(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;
    .locals 3

    .line 1332
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;

    const/4 v1, 0x0

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 1336
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    .line 1337
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v2

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {p1, v2}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    return-object v0

    :cond_1
    return-object v1

    :cond_2
    :goto_0
    return-object v1
.end method

.method private getCampaignDID()Ljava/lang/String;
    .locals 1

    .line 677
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getCampaignModuleAPIs()Lcom/helpshift/providers/ICampaignsModuleAPIs;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 682
    :cond_0
    invoke-interface {v0}, Lcom/helpshift/providers/ICampaignsModuleAPIs;->getDeviceIdentifier()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method private getCampaignUID()Ljava/lang/String;
    .locals 1

    .line 668
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getCampaignModuleAPIs()Lcom/helpshift/providers/ICampaignsModuleAPIs;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 673
    :cond_0
    invoke-interface {v0}, Lcom/helpshift/providers/ICampaignsModuleAPIs;->getUserIdentifier()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method private getInAppNotificationCountCache(Ljava/lang/String;)I
    .locals 1

    .line 914
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inAppNotificationMessageCountMap:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    if-nez p1, :cond_0

    const/4 p1, -0x1

    return p1

    .line 918
    :cond_0
    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    return p1
.end method

.method private getLastViewableSyncedConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 2

    .line 1703
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 1710
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1711
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_0

    .line 1716
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getLastViewableSyncedConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    goto :goto_0

    .line 1720
    :cond_1
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getLastViewableSyncedConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    :goto_0
    return-object v0
.end method

.method private getLastViewableSyncedConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 4

    .line 1738
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1739
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v1

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    return-object v2

    .line 1744
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-static {v1}, Lcom/helpshift/conversation/util/predicate/ConversationPredicates;->newSyncedConversationPredicate(Lcom/helpshift/conversation/activeconversation/ConversationManager;)Lcom/helpshift/util/Predicate;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/Filters;->filter(Ljava/util/List;Lcom/helpshift/util/Predicate;)Ljava/util/List;

    move-result-object v0

    .line 1745
    invoke-static {}, Lcom/helpshift/conversation/util/predicate/ConversationPredicates;->newInProgressConversationPredicate()Lcom/helpshift/util/Predicate;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/Filters;->filter(Ljava/util/List;Lcom/helpshift/util/Predicate;)Ljava/util/List;

    move-result-object v1

    .line 1747
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v3

    if-eqz v3, :cond_1

    return-object v2

    .line 1750
    :cond_1
    invoke-interface {v1}, Ljava/util/List;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_2

    .line 1751
    invoke-static {v0}, Lcom/helpshift/conversation/ConversationUtil;->getLastConversationBasedOnCreatedAt(Ljava/util/Collection;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    goto :goto_0

    .line 1754
    :cond_2
    invoke-static {v1}, Lcom/helpshift/conversation/ConversationUtil;->getLastConversationBasedOnCreatedAt(Ljava/util/Collection;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    :goto_0
    return-object v0
.end method

.method private getMessageCountForShowingInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)I
    .locals 2

    .line 956
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getInAppNotificationCountCache(Ljava/lang/String;)I

    move-result v0

    .line 957
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getUnSeenMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;)I

    move-result p1

    const/4 v1, 0x0

    if-lez p1, :cond_0

    if-eq p1, v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_1

    goto :goto_1

    :cond_1
    const/4 p1, 0x0

    :goto_1
    return p1
.end method

.method private getPoller()Lcom/helpshift/common/domain/Poller;
    .locals 3

    .line 192
    new-instance v0, Lcom/helpshift/common/domain/Poller;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/conversation/domainmodel/ConversationController$1;

    invoke-direct {v2, p0}, Lcom/helpshift/conversation/domainmodel/ConversationController$1;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;)V

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/domain/Poller;-><init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/domain/F;)V

    return-object v0
.end method

.method private isAtLeastOneConversationNonActionable(Ljava/util/List;)Z
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)Z"
        }
    .end annotation

    .line 2049
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 2053
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 2054
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    iput-wide v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 2055
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v0

    if-nez v0, :cond_1

    const/4 p1, 0x1

    return p1

    :cond_2
    return v1
.end method

.method private markStatusAsResolutionAcceptedIfResolved(Ljava/util/Collection;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Collection<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    .line 843
    invoke-interface {p1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 844
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 845
    sget-object v2, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v1, v2, :cond_0

    .line 846
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v1

    if-nez v1, :cond_0

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 847
    invoke-virtual {v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldShowConversationResolutionQuestion()Z

    move-result v1

    if-nez v1, :cond_0

    .line 848
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    const/4 v2, 0x1

    invoke-virtual {v1, v0, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->markConversationResolutionStatus(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method private merge(Ljava/util/List;)V
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    .line 776
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 780
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v4

    .line 782
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    .line 783
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 784
    new-instance v2, Ljava/util/HashSet;

    invoke-direct {v2}, Ljava/util/HashSet;-><init>()V

    .line 786
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v3

    const/4 v5, 0x1

    if-le v3, v5, :cond_1

    .line 793
    invoke-static {p1}, Lcom/helpshift/conversation/ConversationUtil;->sortConversationsBasedOnCreatedAt(Ljava/util/List;)V

    .line 803
    :cond_1
    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-static {v3}, Lcom/helpshift/conversation/util/predicate/ConversationPredicates;->allMessagesAfterLastMessageInDbPredicate(Lcom/helpshift/conversation/activeconversation/ConversationManager;)Lcom/helpshift/util/Predicate;

    move-result-object v3

    invoke-static {p1, v3}, Lcom/helpshift/util/Filters;->filter(Ljava/util/List;Lcom/helpshift/util/Predicate;)Ljava/util/List;

    move-result-object v5

    move-object v3, p0

    move-object v6, v0

    move-object v7, v2

    move-object v8, v1

    .line 805
    invoke-direct/range {v3 .. v8}, Lcom/helpshift/conversation/domainmodel/ConversationController;->merge(Ljava/util/List;Ljava/util/List;Ljava/util/Set;Ljava/util/Set;Ljava/util/Map;)V

    .line 808
    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 809
    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v5, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v1, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;

    invoke-virtual {v4, v3, v5}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->clearMessageUpdates(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    goto :goto_0

    .line 812
    :cond_2
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    .line 813
    invoke-interface {p1, v0}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 814
    invoke-interface {p1, v2}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 816
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->checkForReOpen(Ljava/util/List;)V

    .line 817
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->markStatusAsResolutionAcceptedIfResolved(Ljava/util/Collection;)V

    .line 819
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->isPushTokenSynced()Z

    move-result v0

    if-nez v0, :cond_3

    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "enableInAppNotification"

    .line 820
    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 821
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->checkAndGenerateNotification(Ljava/util/List;)V

    .line 824
    :cond_3
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->sendUnreadCountUpdate()V

    return-void
.end method

.method private merge(Ljava/util/List;Ljava/util/List;Ljava/util/Set;Ljava/util/Set;Ljava/util/Map;)V
    .locals 18
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Ljava/util/Set<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Ljava/util/Set<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Lcom/helpshift/conversation/activeconversation/ConversationUpdate;",
            ">;)V"
        }
    .end annotation

    move-object/from16 v0, p0

    move-object/from16 v1, p2

    move-object/from16 v2, p3

    move-object/from16 v3, p4

    move-object/from16 v4, p5

    .line 1082
    new-instance v5, Ljava/util/ArrayList;

    invoke-direct {v5}, Ljava/util/ArrayList;-><init>()V

    .line 1083
    new-instance v6, Ljava/util/HashMap;

    invoke-direct {v6}, Ljava/util/HashMap;-><init>()V

    .line 1084
    new-instance v7, Ljava/util/HashMap;

    invoke-direct {v7}, Ljava/util/HashMap;-><init>()V

    .line 1085
    new-instance v8, Ljava/util/HashMap;

    invoke-direct {v8}, Ljava/util/HashMap;-><init>()V

    .line 1087
    invoke-interface/range {p1 .. p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v9

    :cond_0
    :goto_0
    invoke-interface {v9}, Ljava/util/Iterator;->hasNext()Z

    move-result v10

    if-eqz v10, :cond_3

    invoke-interface {v9}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1089
    iget-object v11, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v11}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v11

    invoke-virtual {v11}, Ljava/lang/Long;->longValue()J

    move-result-wide v11

    iput-wide v11, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 1091
    iget-object v11, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v11}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v11

    if-nez v11, :cond_1

    .line 1092
    iget-object v11, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-interface {v6, v11, v10}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 1094
    :cond_1
    iget-object v11, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v11}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v11

    if-nez v11, :cond_2

    .line 1095
    iget-object v11, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-interface {v7, v11, v10}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 1097
    :cond_2
    invoke-virtual {v10}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v11

    if-eqz v11, :cond_0

    .line 1102
    iget-object v11, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v11}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object v11

    const-string v12, "/preissues/"

    const-string v13, "preissue_default_unique_key"

    .line 1103
    invoke-interface {v11, v12, v13}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getPendingRequestId(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    if-eqz v11, :cond_0

    .line 1106
    invoke-interface {v8, v11, v10}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_3
    const/4 v9, 0x0

    const/4 v10, 0x0

    .line 1110
    :goto_1
    invoke-interface/range {p2 .. p2}, Ljava/util/List;->size()I

    move-result v11

    const/4 v12, 0x1

    if-ge v9, v11, :cond_10

    .line 1111
    invoke-interface {v1, v9}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v11

    check-cast v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1112
    iget-object v13, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v13}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v13

    invoke-virtual {v13}, Ljava/lang/Long;->longValue()J

    move-result-wide v13

    iput-wide v13, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 1113
    iget-object v13, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 1114
    iget-object v14, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 1115
    iget-object v15, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->createdRequestId:Ljava/lang/String;

    const/16 v16, 0x0

    .line 1120
    invoke-interface {v6, v13}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v17

    if-eqz v17, :cond_5

    .line 1121
    invoke-interface {v6, v13}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v13

    move-object/from16 v16, v13

    check-cast v16, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    :cond_4
    :goto_2
    move v13, v10

    move-object/from16 v10, v16

    goto :goto_3

    .line 1123
    :cond_5
    invoke-interface {v7, v14}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v13

    if-eqz v13, :cond_6

    .line 1124
    invoke-interface {v7, v14}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v13

    move-object/from16 v16, v13

    check-cast v16, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    goto :goto_2

    .line 1126
    :cond_6
    invoke-static {v15}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v13

    if-nez v13, :cond_4

    .line 1127
    invoke-interface {v8, v15}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v13

    if-eqz v13, :cond_4

    .line 1131
    invoke-interface {v8, v15}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    if-eqz v10, :cond_7

    .line 1140
    iget-object v13, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v14, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v14}, Ljava/lang/Long;->longValue()J

    move-result-wide v14

    invoke-interface {v13, v14, v15}, Lcom/helpshift/conversation/dao/ConversationDAO;->deleteMessagesForConversation(J)Z

    :cond_7
    const-string v13, "issue"

    .line 1143
    iget-object v14, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    invoke-virtual {v13, v14}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v13

    if-eqz v13, :cond_8

    const-string v13, "Helpshift_ConvInboxDM"

    const-string v14, "Preissue creation was skipped, issue created directly. Handling idempotent case for it."

    .line 1144
    invoke-static {v13, v14}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1147
    iget-object v13, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v13, v11}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendConversationPostedEvent(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    :cond_8
    const/4 v13, 0x1

    :goto_3
    if-eqz v10, :cond_b

    .line 1156
    iget-object v12, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v12}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v12

    invoke-virtual {v12}, Ljava/lang/Long;->longValue()J

    move-result-wide v14

    iput-wide v14, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 1161
    iget-object v12, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v4, v12}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v12

    if-eqz v12, :cond_9

    .line 1162
    iget-object v12, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v4, v12}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v12

    check-cast v12, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;

    goto :goto_4

    .line 1165
    :cond_9
    new-instance v12, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;

    invoke-direct {v12}, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;-><init>()V

    .line 1168
    :goto_4
    invoke-virtual {v11}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v14

    if-eqz v14, :cond_a

    .line 1169
    invoke-direct {v0, v10, v11, v2, v12}, Lcom/helpshift/conversation/domainmodel/ConversationController;->mergePreIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Set;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    goto :goto_5

    .line 1172
    :cond_a
    invoke-direct {v0, v10, v11, v2, v12}, Lcom/helpshift/conversation/domainmodel/ConversationController;->mergeIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Set;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    .line 1174
    :goto_5
    iget-object v10, v10, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {v4, v10, v12}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_6

    .line 1178
    :cond_b
    invoke-virtual {v11}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v10

    if-eqz v10, :cond_c

    .line 1182
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v14

    iput-wide v14, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->lastUserActivityTime:J

    .line 1186
    iget-object v10, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v14, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v10, v14, :cond_c

    .line 1187
    sget-object v10, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    iput-object v10, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 1192
    :cond_c
    iget-object v10, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    if-eqz v10, :cond_e

    .line 1194
    sget-object v14, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v10, v14, :cond_d

    sget-object v14, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v10, v14, :cond_d

    sget-object v14, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-eq v10, v14, :cond_d

    sget-object v14, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v10, v14, :cond_e

    .line 1198
    :cond_d
    invoke-interface/range {p2 .. p2}, Ljava/util/List;->size()I

    move-result v14

    sub-int/2addr v14, v12

    if-eq v9, v14, :cond_e

    .line 1199
    iput-boolean v12, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    :cond_e
    if-eqz v10, :cond_f

    .line 1204
    iget-boolean v10, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isRedacted:Z

    if-eqz v10, :cond_f

    iget-object v10, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v14, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v10, v14, :cond_f

    .line 1206
    iput-boolean v12, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    .line 1207
    sget-object v10, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    iput-object v10, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 1211
    :cond_f
    invoke-interface {v5, v11}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :goto_6
    add-int/lit8 v9, v9, 0x1

    move v10, v13

    goto/16 :goto_1

    .line 1217
    :cond_10
    invoke-interface {v5}, Ljava/util/List;->size()I

    move-result v6

    if-gt v6, v12, :cond_11

    .line 1218
    invoke-interface {v3, v5}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    goto :goto_a

    .line 1225
    :cond_11
    new-instance v6, Ljava/util/ArrayList;

    invoke-direct {v6, v5}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 1228
    invoke-interface {v6}, Ljava/util/List;->size()I

    move-result v7

    sub-int/2addr v7, v12

    :goto_7
    if-ltz v7, :cond_14

    .line 1229
    invoke-interface {v6, v7}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1233
    invoke-virtual {v8}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v9

    if-nez v9, :cond_13

    add-int/lit8 v9, v7, -0x1

    :goto_8
    if-ltz v9, :cond_13

    .line 1235
    invoke-interface {v6, v9}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v11

    check-cast v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1236
    iget-object v12, v8, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v12}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v12

    if-nez v12, :cond_12

    iget-object v12, v8, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    iget-object v13, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 1237
    invoke-virtual {v12, v13}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v12

    if-eqz v12, :cond_12

    iget-object v12, v8, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    iget-object v13, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 1238
    invoke-virtual {v12, v13}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v12

    if-eqz v12, :cond_12

    .line 1239
    iget-object v8, v8, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    iget-object v9, v11, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v8, v9}, Lcom/helpshift/common/util/HSObservableList;->addAll(Ljava/util/Collection;)Z

    .line 1241
    invoke-interface {v5, v11}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    goto :goto_9

    :cond_12
    add-int/lit8 v9, v9, -0x1

    goto :goto_8

    :cond_13
    :goto_9
    add-int/lit8 v7, v7, -0x1

    goto :goto_7

    .line 1249
    :cond_14
    invoke-interface {v3, v5}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    .line 1252
    :goto_a
    invoke-direct {v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->clearRequestIdForPendingCreateConversationCall(Ljava/util/List;)V

    .line 1254
    invoke-virtual {v0, v2, v3, v4}, Lcom/helpshift/conversation/domainmodel/ConversationController;->putConversations(Ljava/util/Set;Ljava/util/Set;Ljava/util/Map;)V

    if-eqz v10, :cond_15

    .line 1261
    invoke-direct/range {p0 .. p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v1

    if-eqz v1, :cond_15

    .line 1263
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->handleIdempotentPreIssueCreationSuccess()V

    :cond_15
    return-void
.end method

.method private mergeIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Set;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Ljava/util/Set<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Lcom/helpshift/conversation/activeconversation/ConversationUpdate;",
            ")V"
        }
    .end annotation

    .line 1952
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    .line 1958
    iget-object v2, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-direct {p0, v0, v2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->findMatchingIssueTypeConversation(Lcom/helpshift/conversation/activeconversation/ViewableConversation;Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v2

    if-eqz v2, :cond_0

    move-object p1, v2

    .line 1963
    :cond_0
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v2

    .line 1964
    iget-object v3, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-virtual {v3, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    .line 1965
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isVisibleOnUI()Z

    move-result v3

    goto :goto_0

    :cond_1
    const/4 v2, 0x0

    const/4 v3, 0x0

    .line 1968
    :goto_0
    iget-object v4, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    if-eqz v2, :cond_2

    .line 1970
    invoke-virtual {v0, p2, p4}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->mergeIssueForActiveConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    goto :goto_1

    .line 1974
    :cond_2
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v2, p1, p2, v1, p4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->mergeIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    :goto_1
    if-eqz v0, :cond_3

    .line 1979
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isVisibleOnUI()Z

    move-result p2

    if-nez p2, :cond_4

    :cond_3
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object p4, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne p2, p4, :cond_4

    .line 1982
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p2

    if-eqz p2, :cond_4

    .line 1986
    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object p4, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    .line 1987
    invoke-virtual {p2, p4}, Ljava/lang/Long;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_4

    .line 1988
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->handleConversationEnded(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    :cond_4
    if-nez v3, :cond_5

    .line 1997
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, p1, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->checkAndIncrementMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    .line 1999
    :cond_5
    invoke-interface {p3, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method private mergePreIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/util/Set;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            "Ljava/util/Set<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Lcom/helpshift/conversation/activeconversation/ConversationUpdate;",
            ")V"
        }
    .end annotation

    .line 1856
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 1858
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v1

    const/4 v2, 0x0

    if-eqz v1, :cond_1

    .line 1865
    invoke-direct {p0, v1, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->findMatchingPreIssueTypeConversation(Lcom/helpshift/conversation/activeconversation/ViewableConversation;Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v3

    if-eqz v3, :cond_0

    move-object p1, v3

    .line 1871
    :cond_0
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v3

    .line 1872
    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 1873
    invoke-virtual {v0, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    .line 1874
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->isVisibleOnUI()Z

    move-result v4

    goto :goto_0

    :cond_1
    const/4 v3, 0x0

    const/4 v4, 0x0

    .line 1880
    :goto_0
    iget-object v5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v5, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendConversationPostedEventOnPreIssueUpdate(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 1882
    iget-object v5, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v5}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_3

    .line 1883
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v5

    if-eqz v5, :cond_3

    .line 1884
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_3

    if-eqz v3, :cond_2

    .line 1886
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->handlePreIssueCreationSuccess()V

    goto :goto_1

    .line 1889
    :cond_2
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->handlePreIssueCreationSuccess(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 1893
    :cond_3
    :goto_1
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 1898
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v5

    if-eqz v5, :cond_5

    if-eqz v3, :cond_4

    .line 1900
    invoke-virtual {v1, p2, p4}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->mergePreIssueForActiveConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    goto :goto_2

    .line 1904
    :cond_4
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, p1, p2, v2, p4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->mergePreIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    goto :goto_2

    .line 1907
    :cond_5
    iget-object v1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-static {v1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v1

    if-nez v1, :cond_6

    .line 1908
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object p2, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 1909
    invoke-virtual {v1, p1, v3, p2, p4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->updateMessageDMs(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZLjava/util/List;Lcom/helpshift/conversation/activeconversation/ConversationUpdate;)V

    :cond_6
    :goto_2
    if-nez v4, :cond_7

    .line 1918
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, p1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->checkAndIncrementMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/IssueState;)V

    .line 1920
    :cond_7
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendConversationEndedDelegateForPreIssue(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 1921
    invoke-interface {p3, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method private declared-synchronized removeInMemoryConversation()V
    .locals 1

    monitor-enter p0

    const/4 v0, 0x0

    .line 360
    :try_start_0
    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 361
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 359
    monitor-exit p0

    throw v0
.end method

.method private retryCallsForConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V
    .locals 2

    .line 219
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    iput-wide v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 220
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->retryMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    .line 221
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->csatState:Lcom/helpshift/conversation/states/ConversationCSATState;

    sget-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_NOT_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    if-ne p2, v0, :cond_1

    .line 223
    :try_start_0
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendCSATSurveyInternal(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 227
    iget-object p2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object v0, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-ne p2, v0, :cond_0

    goto :goto_0

    .line 228
    :cond_0
    throw p1

    :cond_1
    :goto_0
    return-void
.end method

.method private sendUnreadCountUpdate()V
    .locals 3

    .line 828
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesListenerReference:Ljava/util/concurrent/atomic/AtomicReference;

    if-eqz v0, :cond_0

    .line 829
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesListenerReference:Ljava/util/concurrent/atomic/AtomicReference;

    .line 830
    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/common/FetchDataFromThread;

    if-eqz v0, :cond_0

    .line 832
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v2, Lcom/helpshift/conversation/domainmodel/ConversationController$3;

    invoke-direct {v2, p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController$3;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/common/FetchDataFromThread;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method private declared-synchronized setAliveConversation(Lcom/helpshift/conversation/activeconversation/ViewableConversation;)V
    .locals 1

    monitor-enter p0

    .line 356
    :try_start_0
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 357
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 355
    monitor-exit p0

    throw p1
.end method

.method private shouldShowInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 2

    .line 940
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "enableInAppNotification"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 942
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->canShowNotificationForConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result p1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method private showInAppNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;I)V
    .locals 6

    if-lez p2, :cond_0

    .line 901
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    .line 902
    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/Device;->getAppName()Ljava/lang/String;

    move-result-object v4

    const/4 v5, 0x1

    move-object v0, p0

    move v3, p2

    .line 901
    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/domainmodel/ConversationController;->showNotificationOnUI(Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V

    .line 903
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    invoke-direct {p0, p1, p2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->updateInAppNotificationCountCache(Ljava/lang/String;I)V

    :cond_0
    return-void
.end method

.method private showNotificationOnUI(Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V
    .locals 9

    if-lez p3, :cond_0

    .line 1000
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v8, Lcom/helpshift/conversation/domainmodel/ConversationController$4;

    move-object v1, v8

    move-object v2, p0

    move-object v3, p1

    move-object v4, p2

    move v5, p3

    move-object v6, p4

    move v7, p5

    invoke-direct/range {v1 .. v7}, Lcom/helpshift/conversation/domainmodel/ConversationController$4;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V

    invoke-virtual {v0, v8}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    :cond_0
    return-void
.end method

.method private updateInAppNotificationCountCache(Ljava/lang/String;I)V
    .locals 1

    .line 922
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inAppNotificationMessageCountMap:Ljava/util/Map;

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method


# virtual methods
.method public checkAndDropCustomMeta(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 0

    .line 393
    iget-boolean p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->shouldDropCustomMetadata:Z

    if-eqz p1, :cond_0

    .line 394
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->dropCustomMetaData()V

    :cond_0
    return-void
.end method

.method public clearNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 1607
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/domainmodel/ConversationController$5;

    invoke-direct {v1, p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController$5;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runOnUI(Lcom/helpshift/common/domain/F;)V

    .line 1613
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->clearInAppNotificationCountCache()V

    return-void
.end method

.method public clearPushNotifications()V
    .locals 3

    .line 1582
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 1583
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1584
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1585
    invoke-virtual {p0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->clearNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public createConversation(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 7

    .line 413
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0, v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->registerUserWithServer(Lcom/helpshift/account/domainmodel/UserDM;)V

    .line 415
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-static {v0}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    .line 416
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object v1

    invoke-static {p3}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object p3

    invoke-interface {v1, p3}, Lcom/helpshift/common/platform/Jsonifier;->jsonify(Ljava/util/Collection;)Ljava/lang/String;

    move-result-object p3

    const-string v1, "user_provided_emails"

    .line 417
    invoke-virtual {p3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-interface {v0, v1, p3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p3, "user_provided_name"

    .line 418
    invoke-interface {v0, p3, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "body"

    .line 419
    invoke-interface {v0, p2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p1, "cuid"

    .line 420
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getCampaignUID()Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p1, "cdid"

    .line 421
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getCampaignDID()Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p1, "device_language"

    .line 422
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getDefaultLanguage()Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 423
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getSDKLanguage()Ljava/lang/String;

    move-result-object p1

    .line 424
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p2

    if-nez p2, :cond_0

    const-string p2, "developer_set_language"

    .line 425
    invoke-interface {v0, p2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    const-string p1, "meta"

    .line 427
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/meta/MetaDataDM;->getMetaInfo()Ljava/lang/Object;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 428
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string p2, "fullPrivacy"

    invoke-virtual {p1, p2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result p1

    .line 431
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getCustomIssueFieldDM()Lcom/helpshift/cif/CustomIssueFieldDM;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/cif/CustomIssueFieldDM;->getCustomIssueFieldData()Ljava/lang/Object;

    move-result-object p2

    if-eqz p2, :cond_1

    const-string p3, "custom_fields"

    .line 433
    invoke-virtual {p2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, p3, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 437
    :cond_1
    new-instance v2, Lcom/helpshift/common/domain/network/POSTNetwork;

    const-string p2, "/issues/"

    iget-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v2, p2, p3, v1}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 438
    new-instance v4, Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;

    invoke-direct {v4}, Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;-><init>()V

    .line 439
    new-instance p2, Lcom/helpshift/common/domain/network/IdempotentNetwork;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    const-string v5, "/issues/"

    const-string v6, "issue_default_unique_key"

    move-object v1, p2

    invoke-direct/range {v1 .. v6}, Lcom/helpshift/common/domain/network/IdempotentNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;Ljava/lang/String;Ljava/lang/String;)V

    .line 443
    new-instance p3, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {p3, p2}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 444
    new-instance p2, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p2, p3, v1}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 445
    new-instance p3, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p3, p2, v1}, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 446
    new-instance p2, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {p2, p3}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 450
    :try_start_0
    new-instance p3, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {p3, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 451
    invoke-interface {p2, p3}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p2

    .line 452
    iget-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    .line 453
    invoke-interface {p3}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object p3

    iget-object p2, p2, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {p3, p2}, Lcom/helpshift/common/platform/network/ResponseParser;->parseReadableConversation(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p2

    .line 454
    iput-boolean p1, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->wasFullPrivacyEnabledAtCreation:Z

    .line 455
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    iput-wide v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 457
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object p3, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-interface {p1, p3}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationWithoutMessages(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    if-nez p1, :cond_2

    .line 460
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p1, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 463
    :cond_2
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object p1

    iget-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    const/4 v0, 0x1

    invoke-virtual {p1, p3, v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateIssueExists(Lcom/helpshift/account/domainmodel/UserDM;Z)V

    .line 465
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->sendPushToken()V

    .line 466
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxPoller:Lcom/helpshift/conversation/ConversationInboxPoller;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/ConversationInboxPoller;->startAppPoller(Z)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p2

    :catch_0
    move-exception p1

    .line 470
    iget-object p2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object p3, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq p2, p3, :cond_3

    iget-object p2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object p3, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne p2, p3, :cond_4

    .line 472
    :cond_3
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object p2

    iget-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {p2, p3, v0}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 474
    :cond_4
    throw p1
.end method

.method public createLocalPreIssueConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 15

    .line 1422
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-static {v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getCurrentAdjustedTimeForStorage(Lcom/helpshift/common/platform/Platform;)Lcom/helpshift/util/ValuePair;

    move-result-object v0

    .line 1423
    iget-object v1, v0, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    check-cast v1, Ljava/lang/String;

    .line 1424
    iget-object v0, v0, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast v0, Ljava/lang/Long;

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v13

    .line 1425
    new-instance v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    const-string v3, "Pre Issue Conversation"

    sget-object v4, Lcom/helpshift/conversation/dto/IssueState;->NEW:Lcom/helpshift/conversation/dto/IssueState;

    const-string v12, "preissue"

    const/4 v9, 0x0

    const/4 v10, 0x0

    const/4 v11, 0x0

    move-object v2, v0

    move-object v5, v1

    move-wide v6, v13

    move-object v8, v1

    invoke-direct/range {v2 .. v12}, Lcom/helpshift/conversation/activeconversation/model/Conversation;-><init>(Ljava/lang/String;Lcom/helpshift/conversation/dto/IssueState;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLjava/lang/String;)V

    .line 1434
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    iput-wide v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 1435
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v2

    iput-wide v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->lastUserActivityTime:J

    .line 1436
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v2, v0}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertPreIssueConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 1438
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v3, "conversationGreetingMessage"

    invoke-virtual {v2, v3}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 1439
    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 1440
    new-instance v9, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;

    const/4 v3, 0x0

    const-string v8, ""

    move-object v2, v9

    move-object v5, v1

    move-wide v6, v13

    invoke-direct/range {v2 .. v8}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;)V

    .line 1445
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v1, v9, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->conversationLocalId:Ljava/lang/Long;

    const/4 v1, 0x1

    .line 1446
    iput v1, v9, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->deliveryState:I

    .line 1447
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-virtual {v9, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->setDependencies(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 1449
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v1, v9}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 1451
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v1, v9}, Lcom/helpshift/common/util/HSObservableList;->add(Ljava/lang/Object;)Z

    :cond_0
    return-object v0
.end method

.method public createPreIssue(Lcom/helpshift/conversation/activeconversation/ViewableConversation;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;)V
    .locals 9

    .line 490
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 491
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inProgressPreIssueCreators:Ljava/util/HashMap;

    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v1, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/common/domain/One;

    if-eqz v1, :cond_0

    const-string p1, "Helpshift_ConvInboxDM"

    .line 495
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string p3, "Pre issue creation already in progress: "

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object p3, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 496
    invoke-virtual {v1}, Lcom/helpshift/common/domain/One;->getF()Lcom/helpshift/common/domain/F;

    move-result-object p1

    check-cast p1, Lcom/helpshift/conversation/CreatePreIssueDM;

    .line 497
    invoke-virtual {p1, p4}, Lcom/helpshift/conversation/CreatePreIssueDM;->setListener(Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;)V

    return-void

    .line 502
    :cond_0
    new-instance v8, Lcom/helpshift/conversation/CreatePreIssueDM;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    move-object v1, v8

    move-object v2, p0

    move-object v4, p1

    move-object v5, p4

    move-object v6, p2

    move-object v7, p3

    invoke-direct/range {v1 .. v7}, Lcom/helpshift/conversation/CreatePreIssueDM;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/conversation/activeconversation/ViewableConversation;Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;Ljava/lang/String;Ljava/lang/String;)V

    .line 508
    new-instance p1, Lcom/helpshift/common/domain/One;

    invoke-direct {p1, v8}, Lcom/helpshift/common/domain/One;-><init>(Lcom/helpshift/common/domain/F;)V

    .line 509
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inProgressPreIssueCreators:Ljava/util/HashMap;

    iget-object p3, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {p2, p3, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 511
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance p3, Lcom/helpshift/conversation/domainmodel/ConversationController$2;

    invoke-direct {p3, p0, p1, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController$2;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/common/domain/One;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    invoke-virtual {p2, p3}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public createPreIssueNetwork(Lcom/helpshift/conversation/activeconversation/model/Conversation;Ljava/lang/String;Ljava/lang/String;)V
    .locals 9

    .line 543
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-static {v0}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object v0

    .line 545
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object v1

    .line 546
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v2

    .line 547
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_0

    const-string v3, "name"

    .line 548
    invoke-interface {v0, v3, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 551
    :cond_0
    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_1

    const-string v1, "email"

    .line 552
    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const-string v1, "cuid"

    .line 555
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getCampaignUID()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "cdid"

    .line 556
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getCampaignDID()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "device_language"

    .line 557
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getDefaultLanguage()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 558
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getSDKLanguage()Ljava/lang/String;

    move-result-object v1

    .line 559
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_2

    const-string v2, "developer_set_language"

    .line 560
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 563
    :cond_2
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/meta/MetaDataDM;->getMetaInfo()Ljava/lang/Object;

    move-result-object v1

    const-string v2, "meta"

    .line 564
    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 565
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "fullPrivacy"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    .line 568
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getCustomIssueFieldDM()Lcom/helpshift/cif/CustomIssueFieldDM;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/cif/CustomIssueFieldDM;->getCustomIssueFieldData()Ljava/lang/Object;

    move-result-object v2

    if-eqz v2, :cond_3

    const-string v3, "custom_fields"

    .line 570
    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 573
    :cond_3
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_4

    const-string v2, "greeting"

    .line 574
    invoke-interface {v0, v2, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 577
    :cond_4
    invoke-static {p3}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p2

    if-nez p2, :cond_5

    const-string p2, "user_message"

    .line 578
    invoke-interface {v0, p2, p3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_5
    const-string p2, "is_prefilled"

    .line 581
    iget-boolean v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isAutoFilledPreIssue:Z

    invoke-static {v2}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, p2, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 584
    new-instance v4, Lcom/helpshift/common/domain/network/POSTNetwork;

    const-string p2, "/preissues/"

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v4, p2, v2, v3}, Lcom/helpshift/common/domain/network/POSTNetwork;-><init>(Ljava/lang/String;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V

    .line 585
    new-instance v6, Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;

    invoke-direct {v6}, Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;-><init>()V

    .line 586
    new-instance p2, Lcom/helpshift/common/domain/network/IdempotentNetwork;

    iget-object v5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    const-string v7, "/preissues/"

    const-string v8, "preissue_default_unique_key"

    move-object v3, p2

    invoke-direct/range {v3 .. v8}, Lcom/helpshift/common/domain/network/IdempotentNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;Ljava/lang/String;Ljava/lang/String;)V

    .line 590
    new-instance v2, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;

    invoke-direct {v2, p2}, Lcom/helpshift/common/domain/network/AuthenticationFailureNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 591
    new-instance p2, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p2, v2, v3}, Lcom/helpshift/common/domain/network/TSCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 592
    new-instance v2, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {v2, p2, v3}, Lcom/helpshift/common/domain/network/MetaCorrectedNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;Lcom/helpshift/common/platform/Platform;)V

    .line 593
    new-instance p2, Lcom/helpshift/common/domain/network/GuardOKNetwork;

    invoke-direct {p2, v2}, Lcom/helpshift/common/domain/network/GuardOKNetwork;-><init>(Lcom/helpshift/common/domain/network/Network;)V

    .line 596
    new-instance v2, Lcom/helpshift/common/platform/network/RequestData;

    invoke-direct {v2, v0}, Lcom/helpshift/common/platform/network/RequestData;-><init>(Ljava/util/Map;)V

    .line 599
    :try_start_0
    invoke-interface {p2, v2}, Lcom/helpshift/common/domain/network/Network;->makeRequest(Lcom/helpshift/common/platform/network/RequestData;)Lcom/helpshift/common/platform/network/Response;

    move-result-object p2

    .line 600
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;

    move-result-object v0

    iget-object p2, p2, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    invoke-interface {v0, p2}, Lcom/helpshift/common/platform/network/ResponseParser;->parseReadableConversation(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p2

    .line 603
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    if-nez v0, :cond_6

    .line 604
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 608
    :cond_6
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    .line 609
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->title:Ljava/lang/String;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->title:Ljava/lang/String;

    .line 610
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getCreatedAt()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setCreatedAt(Ljava/lang/String;)V

    .line 611
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->getEpochCreatedAtTime()J

    move-result-wide v2

    invoke-virtual {p1, v2, v3}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setEpochCreatedAtTime(J)V

    .line 612
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updatedAt:Ljava/lang/String;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->updatedAt:Ljava/lang/String;

    .line 613
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->publishId:Ljava/lang/String;

    .line 614
    iget-boolean v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    iput-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->showAgentName:Z

    .line 615
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    .line 616
    iput-boolean v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->wasFullPrivacyEnabledAtCreation:Z

    .line 617
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    iput-wide v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 621
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->deleteMessagesForConversation(J)Z

    .line 622
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    .line 627
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {v0}, Lcom/helpshift/common/util/HSObservableList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_7
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    const/4 v2, 0x1

    if-eqz v1, :cond_9

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 628
    iget-object v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iput-object v3, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->conversationLocalId:Ljava/lang/Long;

    .line 629
    instance-of v3, v1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;

    if-eqz v3, :cond_8

    .line 630
    iput v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->deliveryState:I

    goto :goto_0

    .line 632
    :cond_8
    instance-of v2, v1, Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;

    if-eqz v2, :cond_7

    const/4 v2, 0x2

    .line 633
    iput v2, v1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->deliveryState:I

    goto :goto_0

    .line 637
    :cond_9
    iget-object v0, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 640
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0, v1, v2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->updateIssueExists(Lcom/helpshift/account/domainmodel/UserDM;Z)V

    .line 642
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->sendPushToken()V

    .line 644
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {v0, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 647
    invoke-static {p3}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_a

    const-string p3, ""

    .line 648
    :cond_a
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p1

    invoke-virtual {p1, p3}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->newConversationStarted(Ljava/lang/String;)V

    const-string p1, "issue"

    .line 650
    iget-object p3, p2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->issueType:Ljava/lang/String;

    invoke-virtual {p1, p3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_b

    const-string p1, "Helpshift_ConvInboxDM"

    const-string p3, "Preissue creation skipped, issue created directly."

    .line 651
    invoke-static {p1, p3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 654
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendConversationPostedEvent(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_b
    return-void

    :catch_0
    move-exception p1

    .line 659
    iget-object p2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object p3, Lcom/helpshift/common/exception/NetworkException;->INVALID_AUTH_TOKEN:Lcom/helpshift/common/exception/NetworkException;

    if-eq p2, p3, :cond_c

    iget-object p2, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    sget-object p3, Lcom/helpshift/common/exception/NetworkException;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/common/exception/NetworkException;

    if-ne p2, p3, :cond_d

    .line 661
    :cond_c
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getAuthenticationFailureDM()Lcom/helpshift/account/AuthenticationFailureDM;

    move-result-object p2

    iget-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v0, p1, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    invoke-virtual {p2, p3, v0}, Lcom/helpshift/account/AuthenticationFailureDM;->notifyAuthenticationFailure(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/common/exception/ExceptionType;)V

    .line 663
    :cond_d
    throw p1
.end method

.method deleteAllConversationsData()V
    .locals 3

    .line 173
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->deleteConversationsAndMessages()V

    .line 174
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    .line 176
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    invoke-interface {v2, v0, v1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->deleteUserData(J)V

    return-void
.end method

.method public deleteCachedFilesForResolvedConversations()V
    .locals 2

    .line 1761
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/domainmodel/ConversationController$6;

    invoke-direct {v1, p0}, Lcom/helpshift/conversation/domainmodel/ConversationController$6;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method public executeUserSync()V
    .locals 5

    .line 2019
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchInitialConversationUpdates()Lcom/helpshift/conversation/dto/ConversationInbox;

    .line 2022
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 2024
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->isAtLeastOneConversationNonActionable(Ljava/util/List;)Z

    move-result v1

    if-eqz v1, :cond_0

    return-void

    :cond_0
    const/4 v1, 0x3

    const/4 v2, 0x0

    .line 2034
    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    invoke-virtual {v3}, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->hasMoreMessage()Z

    move-result v3

    .line 2035
    :goto_0
    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->isAtLeastOneConversationNonActionable(Ljava/util/List;)Z

    move-result v0

    if-nez v0, :cond_1

    if-eqz v3, :cond_1

    if-ge v2, v1, :cond_1

    .line 2039
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationHistory()V

    .line 2041
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v3}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-interface {v0, v3, v4}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 2042
    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    invoke-virtual {v3}, Lcom/helpshift/conversation/loaders/RemoteConversationLoader;->hasMoreMessage()Z

    move-result v3

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method

.method public fetchConversationUpdates()Lcom/helpshift/conversation/dto/ConversationInbox;
    .locals 4

    .line 690
    sget-object v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesLock:Ljava/lang/Object;

    monitor-enter v0

    .line 691
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v1, v2, v3}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getConversationInboxTimestamp(J)Ljava/lang/String;

    move-result-object v1

    .line 692
    invoke-direct {p0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesInternal(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationInbox;

    move-result-object v1

    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    .line 693
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method

.method public fetchConversationsAndGetNotificationCount()Lcom/helpshift/util/ValuePair;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/helpshift/util/ValuePair<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Boolean;",
            ">;"
        }
    .end annotation

    .line 1641
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    const/4 v1, 0x1

    if-eqz v0, :cond_6

    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->issueExists()Z

    move-result v0

    if-nez v0, :cond_0

    goto/16 :goto_2

    .line 1645
    :cond_0
    iget-boolean v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userCanReadMessages:Z

    const/4 v2, 0x0

    if-eqz v0, :cond_1

    .line 1646
    new-instance v0, Lcom/helpshift/util/ValuePair;

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-direct {v0, v2, v1}, Lcom/helpshift/util/ValuePair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    return-object v0

    .line 1649
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v3}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    invoke-interface {v0, v3, v4}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1651
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 1652
    new-instance v0, Lcom/helpshift/util/ValuePair;

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-direct {v0, v2, v1}, Lcom/helpshift/util/ValuePair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    return-object v0

    .line 1656
    :cond_2
    invoke-static {v0}, Lcom/helpshift/conversation/ConversationUtil;->shouldPollActivelyForConversations(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_3

    const-wide/32 v3, 0xea60

    goto :goto_0

    :cond_3
    const-wide/32 v3, 0x493e0

    .line 1659
    :goto_0
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v5

    iget-wide v7, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->lastNotifCountFetchTime:J

    sub-long/2addr v5, v7

    cmp-long v0, v5, v3

    if-gez v0, :cond_4

    .line 1661
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getNotificationCountSync()I

    move-result v0

    .line 1662
    new-instance v2, Lcom/helpshift/util/ValuePair;

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-direct {v2, v0, v1}, Lcom/helpshift/util/ValuePair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    return-object v2

    .line 1666
    :cond_4
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->lastNotifCountFetchTime:J

    .line 1667
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdates()Lcom/helpshift/conversation/dto/ConversationInbox;

    .line 1669
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromUIOrStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    if-eqz v0, :cond_5

    .line 1671
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v1, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getUnSeenMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;)I

    move-result v0

    goto :goto_1

    :cond_5
    const/4 v0, 0x0

    .line 1673
    :goto_1
    new-instance v1, Lcom/helpshift/util/ValuePair;

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-direct {v1, v0, v2}, Lcom/helpshift/util/ValuePair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    return-object v1

    .line 1642
    :cond_6
    :goto_2
    new-instance v0, Lcom/helpshift/util/ValuePair;

    const/4 v2, -0x1

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-direct {v0, v2, v1}, Lcom/helpshift/util/ValuePair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    return-object v0
.end method

.method public fetchInitialConversationUpdates()Lcom/helpshift/conversation/dto/ConversationInbox;
    .locals 2

    .line 697
    sget-object v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesLock:Ljava/lang/Object;

    monitor-enter v0

    const/4 v1, 0x0

    .line 698
    :try_start_0
    invoke-direct {p0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesInternal(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationInbox;

    move-result-object v1

    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    .line 699
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method

.method public getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 5

    .line 1373
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "disableInAppConversation"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_2

    .line 1374
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1375
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 1376
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1377
    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v3}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    iput-wide v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 1378
    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v3, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 1379
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 1382
    :cond_1
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v0

    if-lez v0, :cond_2

    .line 1383
    invoke-static {v1}, Lcom/helpshift/conversation/ConversationUtil;->getLastConversationBasedOnCreatedAt(Ljava/util/Collection;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    goto :goto_1

    :cond_2
    const/4 v0, 0x0

    :goto_1
    return-object v0
.end method

.method public getActiveConversationOrPreIssue()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 3

    .line 1364
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    if-nez v0, :cond_0

    .line 1365
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "conversationalIssueFiling"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 1366
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->createLocalPreIssueConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method public getConversationArchivalPrefillText()Ljava/lang/String;
    .locals 3

    .line 243
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getConversationArchivalPrefillText(J)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getConversationDetail()Lcom/helpshift/conversation/dto/ConversationDetailDTO;
    .locals 3

    .line 239
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getDescriptionDetail(J)Lcom/helpshift/conversation/dto/ConversationDetailDTO;

    move-result-object v0

    return-object v0
.end method

.method public getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;
    .locals 1

    .line 169
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxPoller:Lcom/helpshift/conversation/ConversationInboxPoller;

    return-object v0
.end method

.method public getEmail()Ljava/lang/String;
    .locals 3

    .line 270
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getEmail(J)Ljava/lang/String;

    move-result-object v0

    .line 271
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 272
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method public getFAQSearchResults(Ljava/lang/String;)Ljava/util/ArrayList;
    .locals 1

    .line 302
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->faqSearchDM:Lcom/helpshift/faq/domainmodel/FAQSearchDM;

    invoke-interface {v0, p1}, Lcom/helpshift/faq/domainmodel/FAQSearchDM;->getSearchResults(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object p1

    return-object p1
.end method

.method public getImageAttachmentDraft()Lcom/helpshift/conversation/dto/ImagePickerFile;
    .locals 3

    .line 290
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getImageAttachment(J)Lcom/helpshift/conversation/dto/ImagePickerFile;

    move-result-object v0

    return-object v0
.end method

.method public getLastConversationsRedactionTime()Ljava/lang/Long;
    .locals 3

    .line 286
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getLastConversationsRedactionTime(J)Ljava/lang/Long;

    move-result-object v0

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 3

    .line 262
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getName(J)Ljava/lang/String;

    move-result-object v0

    .line 263
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 264
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getName()Ljava/lang/String;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method public getNotificationCountSync()I
    .locals 4

    .line 1618
    iget-boolean v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userCanReadMessages:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    .line 1622
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromUIOrStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    if-nez v0, :cond_1

    return v1

    .line 1628
    :cond_1
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v2, v0}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->getUnSeenMessageCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;)I

    move-result v2

    .line 1631
    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    invoke-interface {v3, v0}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getPushNotificationData(Ljava/lang/String;)Lcom/helpshift/conversation/dao/PushNotificationData;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 1633
    iget v1, v0, Lcom/helpshift/conversation/dao/PushNotificationData;->count:I

    .line 1636
    :cond_2
    invoke-static {v2, v1}, Ljava/lang/Math;->max(II)I

    move-result v0

    return v0
.end method

.method public getOldestConversationCreatedAtTime()Ljava/lang/Long;
    .locals 3

    .line 2070
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->getOldestConversationCreatedAtTime(J)Ljava/lang/Long;

    move-result-object v0

    return-object v0
.end method

.method public getOpenConversationWithMessages()Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 6

    .line 1395
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1396
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 1397
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v2

    const/4 v3, 0x0

    if-eqz v2, :cond_0

    return-object v3

    .line 1400
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1401
    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v4}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/Long;->longValue()J

    move-result-wide v4

    iput-wide v4, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 1402
    invoke-virtual {v2}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v4

    if-eqz v4, :cond_1

    .line 1403
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 1406
    :cond_2
    invoke-interface {v1}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_3

    return-object v3

    .line 1410
    :cond_3
    invoke-static {v1}, Lcom/helpshift/conversation/ConversationUtil;->getLastConversationBasedOnCreatedAt(Ljava/util/Collection;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    .line 1411
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v2, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v1, v2, v3}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setMessageDMs(Ljava/util/List;)V

    return-object v0
.end method

.method public getUserReplyText()Ljava/lang/String;
    .locals 3

    .line 298
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getUserReplyDraft(J)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getViewableConversation(ZLjava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;
    .locals 7

    const/4 v0, 0x0

    if-eqz p1, :cond_1

    .line 1281
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 1285
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getType()Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    move-result-object p2

    sget-object v1, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->SINGLE:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    if-ne p2, v1, :cond_0

    .line 1286
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->removeInMemoryConversation()V

    move-object p1, v0

    :cond_0
    if-nez p1, :cond_4

    .line 1291
    new-instance p1, Lcom/helpshift/conversation/loaders/ConversationHistoryLoader;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    const-wide/16 v4, 0x64

    move-object v0, p1

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/loaders/ConversationHistoryLoader;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/RemoteConversationLoader;J)V

    .line 1293
    new-instance p2, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    move-object v0, p2

    move-object v4, p1

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/activeconversation/ViewableConversationHistory;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/ConversationHistoryLoader;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    .line 1294
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->init()V

    .line 1295
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getAllConversations()Ljava/util/List;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result p1

    if-eqz p1, :cond_3

    .line 1297
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->createLocalPreIssueConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    .line 1298
    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->onNewConversationStarted(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    goto :goto_0

    .line 1303
    :cond_1
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 1307
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getType()Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    move-result-object v1

    sget-object v2, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->HISTORY:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    if-ne v1, v2, :cond_2

    .line 1308
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->removeInMemoryConversation()V

    move-object p1, v0

    :cond_2
    if-nez p1, :cond_4

    .line 1313
    new-instance p1, Lcom/helpshift/conversation/loaders/SingleConversationLoader;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    const-wide/16 v5, 0x64

    move-object v0, p1

    move-object v3, p2

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/loaders/SingleConversationLoader;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/Long;Lcom/helpshift/conversation/loaders/RemoteConversationLoader;J)V

    .line 1316
    new-instance p2, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    move-object v0, p2

    move-object v4, p1

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/SingleConversationLoader;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    .line 1317
    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->init()V

    :cond_3
    :goto_0
    move-object p1, p2

    .line 1320
    :cond_4
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->setLiveUpdateDM(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;)V

    .line 1321
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setAliveConversation(Lcom/helpshift/conversation/activeconversation/ViewableConversation;)V

    return-object p1
.end method

.method public handlePushNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 6

    const-string v0, "issue"

    .line 1524
    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1525
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p1, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationWithoutMessages(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    goto :goto_0

    :cond_0
    const-string v0, "preissue"

    .line 1527
    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_5

    .line 1528
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-interface {p1, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readPreConversationWithoutMessages(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    :goto_0
    if-nez p1, :cond_1

    return-void

    .line 1542
    :cond_1
    invoke-static {p3}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_2

    .line 1543
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object p2

    invoke-interface {p2}, Lcom/helpshift/common/platform/Device;->getAppName()Ljava/lang/String;

    move-result-object p3

    :cond_2
    move-object v4, p3

    .line 1546
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object p3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    .line 1547
    invoke-interface {p2, p3}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getPushNotificationData(Ljava/lang/String;)Lcom/helpshift/conversation/dao/PushNotificationData;

    move-result-object p2

    const/4 p3, 0x1

    if-nez p2, :cond_3

    move-object p2, v4

    const/4 v3, 0x1

    goto :goto_1

    .line 1555
    :cond_3
    iget v0, p2, Lcom/helpshift/conversation/dao/PushNotificationData;->count:I

    add-int/2addr v0, p3

    .line 1556
    iget-object p2, p2, Lcom/helpshift/conversation/dao/PushNotificationData;->title:Ljava/lang/String;

    move v3, v0

    .line 1560
    :goto_1
    iget-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    new-instance v1, Lcom/helpshift/conversation/dao/PushNotificationData;

    invoke-direct {v1, v3, p2}, Lcom/helpshift/conversation/dao/PushNotificationData;-><init>(ILjava/lang/String;)V

    invoke-interface {p3, v0, v1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->setPushNotificationData(Ljava/lang/String;Lcom/helpshift/conversation/dao/PushNotificationData;)V

    if-lez v3, :cond_4

    .line 1564
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->canShowNotificationForConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result p2

    if-eqz p2, :cond_4

    .line 1565
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    const/4 v5, 0x0

    move-object v0, p0

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/domainmodel/ConversationController;->showNotificationOnUI(Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V

    .line 1572
    :cond_4
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->sendUnreadCountUpdate()V

    return-void

    :cond_5
    const-string p2, "Helpshift_ConvInboxDM"

    .line 1531
    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Cannot handle push for unknown issue type. "

    invoke-virtual {p3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public initialize()V
    .locals 2

    .line 160
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CONVERSATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0, v1, p0}, Lcom/helpshift/common/AutoRetryFailedEventDM;->register(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;Lcom/helpshift/common/AutoRetriableDM;)V

    .line 162
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getSyncState()Lcom/helpshift/account/domainmodel/UserSyncStatus;

    move-result-object v0

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSyncStatus;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    if-ne v0, v1, :cond_0

    .line 163
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/account/domainmodel/UserDM;->addObserver(Ljava/util/Observer;)V

    :cond_0
    return-void
.end method

.method public isActiveConversationActionable()Z
    .locals 6

    .line 1459
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation()Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 1462
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    if-nez v1, :cond_1

    .line 1466
    invoke-virtual {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    :cond_1
    const/4 v2, 0x1

    const/4 v3, 0x0

    if-eqz v1, :cond_7

    .line 1475
    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v4, v1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result v4

    if-nez v4, :cond_2

    return v3

    .line 1479
    :cond_2
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v4

    if-eqz v4, :cond_3

    iget-object v4, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 1480
    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_3

    .line 1481
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v4

    if-eqz v4, :cond_3

    goto :goto_2

    .line 1485
    :cond_3
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v4

    if-nez v4, :cond_8

    iget-object v4, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v5, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v4, v5, :cond_4

    goto :goto_2

    .line 1488
    :cond_4
    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v4, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v1, v4, :cond_7

    if-eqz v0, :cond_5

    .line 1491
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getConversationVMCallback()Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;

    move-result-object v0

    if-eqz v0, :cond_5

    .line 1494
    invoke-interface {v0}, Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;->isMessageBoxVisible()Z

    move-result v0

    move v1, v0

    const/4 v0, 0x1

    goto :goto_1

    :cond_5
    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_1
    if-nez v0, :cond_6

    .line 1498
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v4

    invoke-interface {v0, v4, v5}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getPersistMessageBox(J)Z

    move-result v0

    .line 1499
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v4}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/Long;->longValue()J

    move-result-wide v4

    invoke-interface {v1, v4, v5}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getUserReplyDraft(J)Ljava/lang/String;

    move-result-object v1

    if-nez v0, :cond_8

    .line 1500
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_7

    goto :goto_2

    :cond_6
    move v2, v1

    goto :goto_2

    :cond_7
    const/4 v2, 0x0

    :cond_8
    :goto_2
    return v2
.end method

.method public isCreateConversationInProgress()Z
    .locals 1

    .line 686
    iget-boolean v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->isCreateConversationInProgress:Z

    return v0
.end method

.method public isPreissueCreationInProgress(J)Z
    .locals 1

    .line 531
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inProgressPreIssueCreators:Ljava/util/HashMap;

    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method

.method putConversations(Ljava/util/Set;Ljava/util/Set;Ljava/util/Map;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Set<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Ljava/util/Set<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Lcom/helpshift/conversation/activeconversation/ConversationUpdate;",
            ">;)V"
        }
    .end annotation

    .line 1055
    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1057
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    iput-wide v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    goto :goto_0

    .line 1061
    :cond_0
    invoke-interface {p2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1063
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    iput-wide v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    goto :goto_1

    .line 1067
    :cond_1
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    invoke-interface {v0, v1, p3}, Lcom/helpshift/conversation/dao/ConversationDAO;->updateConversations(Ljava/util/List;Ljava/util/Map;)V

    .line 1069
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    new-instance p3, Ljava/util/ArrayList;

    invoke-direct {p3, p2}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    invoke-interface {p1, p3}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertConversations(Ljava/util/List;)V

    return-void
.end method

.method public redactConversations()V
    .locals 4

    .line 2078
    sget-object v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesLock:Ljava/lang/Object;

    monitor-enter v0

    .line 2080
    :try_start_0
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->deleteConversationsAndMessages()V

    .line 2082
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;

    if-eqz v1, :cond_0

    .line 2083
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->aliveViewableConversation:Ljava/lang/ref/WeakReference;

    invoke-virtual {v1}, Ljava/lang/ref/WeakReference;->clear()V

    .line 2086
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {v1, v2, v3}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->resetDataAfterConversationsDeletion(J)V

    .line 2087
    monitor-exit v0

    return-void

    :catchall_0
    move-exception v1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1
.end method

.method public registerStartNewConversationListener(Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;)V
    .locals 1

    .line 314
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    return-void
.end method

.method public resetLastNotificationCountFetchTime()V
    .locals 2

    const-wide/16 v0, 0x0

    .line 1677
    iput-wide v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->lastNotifCountFetchTime:J

    return-void
.end method

.method resetPreIssueConversationsForUser(Lcom/helpshift/account/domainmodel/UserDM;)V
    .locals 8

    const-string v0, "Helpshift_ConvInboxDM"

    const-string v1, "Starting preissues reset."

    .line 1777
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1778
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-virtual {p1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_6

    .line 1780
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v1

    if-nez v1, :cond_0

    goto/16 :goto_1

    .line 1785
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getPreissueResetInterval()J

    move-result-wide v1

    const-wide/16 v3, 0x3e8

    mul-long v1, v1, v3

    .line 1786
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_5

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1789
    invoke-virtual {v3}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isInPreIssueMode()Z

    move-result v4

    if-nez v4, :cond_2

    goto :goto_0

    .line 1794
    :cond_2
    iget-wide v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->lastUserActivityTime:J

    .line 1795
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v6

    sub-long/2addr v6, v4

    cmp-long v4, v6, v1

    if-ltz v4, :cond_1

    .line 1797
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_3

    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_3

    const-string v4, "Helpshift_ConvInboxDM"

    .line 1799
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Deleting offline preissue : "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v6, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-static {v4, v5}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 1800
    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v5

    invoke-interface {v4, v5, v6}, Lcom/helpshift/conversation/dao/ConversationDAO;->deleteConversation(J)V

    .line 1801
    invoke-direct {p0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->removeInMemoryConversation()V

    goto :goto_0

    .line 1805
    :cond_3
    invoke-virtual {v3}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result v4

    if-nez v4, :cond_4

    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v5, Lcom/helpshift/conversation/dto/IssueState;->UNKNOWN:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v4, v5, :cond_1

    .line 1807
    :cond_4
    invoke-virtual {p0, v3}, Lcom/helpshift/conversation/domainmodel/ConversationController;->clearNotification(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 1808
    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v5, Lcom/helpshift/conversation/domainmodel/ConversationController$7;

    invoke-direct {v5, p0, v3, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController$7;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/account/domainmodel/UserDM;)V

    invoke-virtual {v4, v5}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    goto :goto_0

    :cond_5
    return-void

    :cond_6
    :goto_1
    return-void
.end method

.method public resetPushNotificationCount(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 1577
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    const/4 v1, 0x0

    invoke-interface {v0, p1, v1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->setPushNotificationData(Ljava/lang/String;Lcom/helpshift/conversation/dao/PushNotificationData;)V

    .line 1578
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p1

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->didReceiveNotification(I)V

    return-void
.end method

.method public saveDescriptionDetail(Ljava/lang/String;I)V
    .locals 6

    .line 247
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    new-instance v3, Lcom/helpshift/conversation/dto/ConversationDetailDTO;

    .line 249
    invoke-static {}, Ljava/lang/System;->nanoTime()J

    move-result-wide v4

    invoke-direct {v3, p1, v4, v5, p2}, Lcom/helpshift/conversation/dto/ConversationDetailDTO;-><init>(Ljava/lang/String;JI)V

    .line 247
    invoke-interface {v0, v1, v2, v3}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveDescriptionDetail(JLcom/helpshift/conversation/dto/ConversationDetailDTO;)V

    return-void
.end method

.method public saveEmail(Ljava/lang/String;)V
    .locals 3

    .line 258
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2, p1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveEmail(JLjava/lang/String;)V

    return-void
.end method

.method public saveImageAttachmentDraft(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 3

    .line 278
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2, p1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveImageAttachment(JLcom/helpshift/conversation/dto/ImagePickerFile;)V

    return-void
.end method

.method public saveLastConversationsRedactionTime(J)V
    .locals 3

    .line 282
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2, p1, p2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveLastConversationsRedactionTime(JJ)V

    return-void
.end method

.method public saveName(Ljava/lang/String;)V
    .locals 3

    .line 254
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2, p1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveName(JLjava/lang/String;)V

    return-void
.end method

.method public saveUserReplyText(Ljava/lang/String;)V
    .locals 3

    .line 294
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2, p1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveUserReplyDraft(JLjava/lang/String;)V

    return-void
.end method

.method public sendFailedApiCalls(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;)V
    .locals 2

    .line 202
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 203
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    invoke-interface {p1, v0, v1}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object p1

    .line 204
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 206
    iget-object v1, v0, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-direct {p0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 209
    invoke-virtual {v1}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0

    const/4 v1, 0x1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->retryCallsForConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    .line 213
    invoke-direct {p0, v0, v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->retryCallsForConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;Z)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method public setConversationViewState(I)V
    .locals 0

    .line 1517
    iput p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationViewState:I

    return-void
.end method

.method public setPersistMessageBox(Z)V
    .locals 3

    .line 1513
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2, p1}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->savePersistMessageBox(JZ)V

    return-void
.end method

.method public setShouldDropCustomMetadata(Z)V
    .locals 0

    .line 310
    iput-boolean p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->shouldDropCustomMetadata:Z

    return-void
.end method

.method public setUserCanReadMessages(Z)V
    .locals 0

    .line 235
    iput-boolean p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userCanReadMessages:Z

    return-void
.end method

.method public shouldOpenConversationFromNotification(J)Z
    .locals 2

    .line 1344
    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getAliveViewableConversation(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/ViewableConversation;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 1345
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v1

    if-nez v1, :cond_1

    .line 1346
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    invoke-interface {v1, p1}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationWithoutMessages(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 1349
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    iput-wide v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 1350
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->shouldOpen(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result p1

    return p1

    :cond_1
    if-eqz v0, :cond_2

    .line 1354
    invoke-virtual {v0}, Lcom/helpshift/conversation/activeconversation/ViewableConversation;->shouldOpen()Z

    move-result p1

    if-eqz p1, :cond_2

    const/4 p1, 0x1

    goto :goto_0

    :cond_2
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public shouldPersistMessageBox()Z
    .locals 3

    .line 1509
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getPersistMessageBox(J)Z

    move-result v0

    return v0
.end method

.method public showPushNotifications()V
    .locals 10

    .line 1590
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    .line 1591
    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v0

    .line 1592
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 1593
    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object v3, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    .line 1594
    invoke-interface {v2, v3}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->getPushNotificationData(Ljava/lang/String;)Lcom/helpshift/conversation/dao/PushNotificationData;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 1595
    iget v3, v2, Lcom/helpshift/conversation/dao/PushNotificationData;->count:I

    if-lez v3, :cond_0

    .line 1596
    iget-object v5, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    .line 1597
    iget-object v6, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    iget v7, v2, Lcom/helpshift/conversation/dao/PushNotificationData;->count:I

    iget-object v8, v2, Lcom/helpshift/conversation/dao/PushNotificationData;->title:Ljava/lang/String;

    const/4 v9, 0x0

    move-object v4, p0

    invoke-direct/range {v4 .. v9}, Lcom/helpshift/conversation/domainmodel/ConversationController;->showNotificationOnUI(Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method public startNewConversation(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 7

    .line 326
    new-instance v6, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;

    move-object v0, v6

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v4, p3

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;-><init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    .line 330
    iget-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v6}, Lcom/helpshift/conversation/domainmodel/ConversationController$CreateConversationStateHolder;->getStartNewConversationInternalF()Lcom/helpshift/common/domain/F;

    move-result-object p2

    invoke-virtual {p1, p2}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method startNewConversationInternal(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 7

    const/4 v0, 0x1

    .line 335
    iput-boolean v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->isCreateConversationInProgress:Z

    .line 336
    invoke-virtual {p0, p1, p2, p3}, Lcom/helpshift/conversation/domainmodel/ConversationController;->tryToStartNewConversation(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    .line 337
    new-instance p2, Lcom/helpshift/conversation/loaders/SingleConversationLoader;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v3, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    iget-object v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->remoteConversationLoader:Lcom/helpshift/conversation/loaders/RemoteConversationLoader;

    const-wide/16 v5, 0x64

    move-object v0, p2

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/loaders/SingleConversationLoader;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/account/domainmodel/UserDM;Ljava/lang/Long;Lcom/helpshift/conversation/loaders/RemoteConversationLoader;J)V

    .line 342
    new-instance p3, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    move-object v0, p3

    move-object v4, p2

    invoke-direct/range {v0 .. v5}, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;-><init>(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/conversation/loaders/SingleConversationLoader;Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    .line 344
    invoke-virtual {p3}, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->init()V

    .line 345
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->liveUpdateDM:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-virtual {p3, p2}, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->setLiveUpdateDM(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;)V

    .line 346
    invoke-direct {p0, p3}, Lcom/helpshift/conversation/domainmodel/ConversationController;->setAliveConversation(Lcom/helpshift/conversation/activeconversation/ViewableConversation;)V

    .line 348
    invoke-virtual {p3}, Lcom/helpshift/conversation/activeconversation/ViewableSingleConversation;->getActiveConversation()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p2

    invoke-direct {p0, p2, p4}, Lcom/helpshift/conversation/domainmodel/ConversationController;->checkAndTryToUploadImage(Lcom/helpshift/conversation/activeconversation/model/Conversation;Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    const/4 p2, 0x0

    .line 349
    iput-boolean p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->isCreateConversationInProgress:Z

    .line 350
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    if-eqz p2, :cond_0

    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {p2}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object p2

    if-eqz p2, :cond_0

    .line 351
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {p2}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide p3

    invoke-interface {p2, p3, p4}, Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;->onCreateConversationSuccess(J)V

    :cond_0
    return-void
.end method

.method public triggerFAQSearchIndexing()V
    .locals 1

    .line 306
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->faqSearchDM:Lcom/helpshift/faq/domainmodel/FAQSearchDM;

    invoke-interface {v0}, Lcom/helpshift/faq/domainmodel/FAQSearchDM;->startFAQSearchIndexing()V

    return-void
.end method

.method public tryToStartNewConversation(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 5

    const/4 v0, 0x0

    .line 367
    :try_start_0
    sget-object v1, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesLock:Ljava/lang/Object;

    monitor-enter v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 370
    :try_start_1
    invoke-virtual {p0, p1, p2, p3}, Lcom/helpshift/conversation/domainmodel/ConversationController;->createConversation(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v2

    .line 371
    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :try_start_2
    const-string v1, ""

    .line 372
    invoke-virtual {p0, v1, v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveDescriptionDetail(Ljava/lang/String;I)V

    .line 373
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    invoke-virtual {v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->shouldCreateConversationAnonymously()Z

    move-result v1

    if-nez v1, :cond_0

    .line 374
    invoke-virtual {p0, p2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveName(Ljava/lang/String;)V

    .line 375
    invoke-virtual {p0, p3}, Lcom/helpshift/conversation/domainmodel/ConversationController;->saveEmail(Ljava/lang/String;)V

    .line 377
    :cond_0
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    iget-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    invoke-virtual {p3}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object p3

    invoke-virtual {p3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    const/4 p3, 0x0

    invoke-interface {p2, v3, v4, p3}, Lcom/helpshift/conversation/dao/ConversationInboxDAO;->saveConversationArchivalPrefillText(JLjava/lang/String;)V

    .line 378
    invoke-virtual {p0, v2}, Lcom/helpshift/conversation/domainmodel/ConversationController;->checkAndDropCustomMeta(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 379
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {p2, v2}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendConversationPostedEvent(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 380
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {p2}, Lcom/helpshift/common/domain/Domain;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p2

    invoke-virtual {p2, p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->newConversationStarted(Ljava/lang/String;)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0

    return-object v2

    :catchall_0
    move-exception p1

    .line 371
    :try_start_3
    monitor-exit v1
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    :try_start_4
    throw p1
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_0

    :catch_0
    move-exception p1

    .line 384
    iput-boolean v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->isCreateConversationInProgress:Z

    .line 385
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {p2}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object p2

    if-eqz p2, :cond_1

    .line 386
    iget-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {p2}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;

    invoke-interface {p2, p1}, Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;->onCreateConversationFailure(Ljava/lang/Exception;)V

    .line 388
    :cond_1
    throw p1
.end method

.method public unregisterStartNewConversationListener(Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;)V
    .locals 1

    .line 318
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    .line 319
    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-ne v0, p1, :cond_0

    .line 320
    new-instance p1, Ljava/lang/ref/WeakReference;

    const/4 v0, 0x0

    invoke-direct {p1, v0}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController;->startNewConversationListenerRef:Ljava/lang/ref/WeakReference;

    :cond_0
    return-void
.end method
