.class public Lcom/helpshift/common/platform/AndroidPlatform;
.super Ljava/lang/Object;
.source "AndroidPlatform.java"

# interfaces
.implements Lcom/helpshift/common/platform/Platform;


# static fields
.field private static final TAG:Ljava/lang/String; = "AndroidPlatform"


# instance fields
.field private analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

.field private apiKey:Ljava/lang/String;

.field private appId:Ljava/lang/String;

.field private backupDAO:Lcom/helpshift/common/dao/BackupDAO;

.field private clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

.field private final context:Landroid/content/Context;

.field private conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

.field private conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

.field private customIssueFieldDAO:Lcom/helpshift/cif/dao/CustomIssueFieldDAO;

.field private data:Lcom/helpshift/support/HSApiData;

.field private device:Lcom/helpshift/common/platform/Device;

.field private domain:Ljava/lang/String;

.field private downloader:Lcom/helpshift/downloader/SupportDownloader;

.field private faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

.field private faqSearchDM:Lcom/helpshift/faq/domainmodel/FAQSearchDM;

.field private jsonifier:Lcom/helpshift/common/platform/Jsonifier;

.field private legacyAnalyticsEventIDDAO:Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;

.field private legacyProfileMigrationDAO:Lcom/helpshift/migration/LegacyProfileMigrationDAO;

.field private metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

.field private networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

.field private redactionDAO:Lcom/helpshift/redaction/RedactionDAO;

.field private storage:Lcom/helpshift/common/platform/KVStore;

.field private uiContext:Landroid/content/Context;

.field private uiThreader:Lcom/helpshift/common/domain/Threader;

.field private userDAO:Lcom/helpshift/account/dao/UserDAO;

.field private userManagerDAO:Lcom/helpshift/account/dao/AndroidUserManagerDAO;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 88
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 89
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    .line 90
    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->apiKey:Ljava/lang/String;

    .line 91
    iput-object p3, p0, Lcom/helpshift/common/platform/AndroidPlatform;->domain:Ljava/lang/String;

    .line 92
    iput-object p4, p0, Lcom/helpshift/common/platform/AndroidPlatform;->appId:Ljava/lang/String;

    .line 94
    new-instance p2, Lcom/helpshift/support/storage/SupportKeyValueDBStorage;

    invoke-direct {p2, p1}, Lcom/helpshift/support/storage/SupportKeyValueDBStorage;-><init>(Landroid/content/Context;)V

    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->storage:Lcom/helpshift/common/platform/KVStore;

    .line 95
    new-instance p2, Lcom/helpshift/common/platform/AndroidBackupDAO;

    invoke-direct {p2}, Lcom/helpshift/common/platform/AndroidBackupDAO;-><init>()V

    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    .line 96
    new-instance p2, Lcom/helpshift/common/platform/AndroidDevice;

    iget-object p3, p0, Lcom/helpshift/common/platform/AndroidPlatform;->storage:Lcom/helpshift/common/platform/KVStore;

    iget-object p4, p0, Lcom/helpshift/common/platform/AndroidPlatform;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    invoke-direct {p2, p1, p3, p4}, Lcom/helpshift/common/platform/AndroidDevice;-><init>(Landroid/content/Context;Lcom/helpshift/common/platform/KVStore;Lcom/helpshift/common/dao/BackupDAO;)V

    .line 97
    invoke-virtual {p2}, Lcom/helpshift/common/platform/AndroidDevice;->updateDeviceIdInBackupDAO()V

    .line 98
    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->device:Lcom/helpshift/common/platform/Device;

    .line 100
    new-instance p2, Lcom/helpshift/account/dao/AndroidUserDAO;

    invoke-static {p1}, Lcom/helpshift/account/dao/UserDB;->getInstance(Landroid/content/Context;)Lcom/helpshift/account/dao/UserDB;

    move-result-object p3

    invoke-direct {p2, p3}, Lcom/helpshift/account/dao/AndroidUserDAO;-><init>(Lcom/helpshift/account/dao/UserDB;)V

    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    .line 101
    new-instance p2, Lcom/helpshift/account/dao/AndroidUserManagerDAO;

    iget-object p3, p0, Lcom/helpshift/common/platform/AndroidPlatform;->storage:Lcom/helpshift/common/platform/KVStore;

    invoke-direct {p2, p3}, Lcom/helpshift/account/dao/AndroidUserManagerDAO;-><init>(Lcom/helpshift/common/platform/KVStore;)V

    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->userManagerDAO:Lcom/helpshift/account/dao/AndroidUserManagerDAO;

    .line 102
    new-instance p2, Lcom/helpshift/account/dao/AndroidClearedUserDAO;

    invoke-static {p1}, Lcom/helpshift/account/dao/UserDB;->getInstance(Landroid/content/Context;)Lcom/helpshift/account/dao/UserDB;

    move-result-object p1

    invoke-direct {p2, p1}, Lcom/helpshift/account/dao/AndroidClearedUserDAO;-><init>(Lcom/helpshift/account/dao/UserDB;)V

    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    .line 103
    new-instance p1, Lcom/helpshift/common/platform/AndroidJsonifier;

    invoke-direct {p1}, Lcom/helpshift/common/platform/AndroidJsonifier;-><init>()V

    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    .line 104
    new-instance p1, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;

    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->storage:Lcom/helpshift/common/platform/KVStore;

    invoke-direct {p1, p2}, Lcom/helpshift/support/storage/AndroidAnalyticsEventDAO;-><init>(Lcom/helpshift/common/platform/KVStore;)V

    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

    .line 105
    new-instance p1, Lcom/helpshift/common/platform/AndroidMetadataDAO;

    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidPlatform;->storage:Lcom/helpshift/common/platform/KVStore;

    invoke-direct {p1, p2}, Lcom/helpshift/common/platform/AndroidMetadataDAO;-><init>(Lcom/helpshift/common/platform/KVStore;)V

    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    return-void
.end method

.method private declared-synchronized getData()Lcom/helpshift/support/HSApiData;
    .locals 2

    monitor-enter p0

    .line 426
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->data:Lcom/helpshift/support/HSApiData;

    if-nez v0, :cond_0

    .line 427
    new-instance v0, Lcom/helpshift/support/HSApiData;

    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-direct {v0, v1}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->data:Lcom/helpshift/support/HSApiData;

    .line 429
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->data:Lcom/helpshift/support/HSApiData;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 425
    monitor-exit p0

    throw v0
.end method


# virtual methods
.method public canReadFileAtUri(Ljava/lang/String;)Z
    .locals 1

    .line 421
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-static {v0, p1}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->canReadFileAtUri(Landroid/content/Context;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public clearNotifications(Ljava/lang/String;)V
    .locals 2

    .line 370
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    const/4 v1, 0x1

    invoke-static {v0, p1, v1}, Lcom/helpshift/util/ApplicationUtil;->cancelNotification(Landroid/content/Context;Ljava/lang/String;I)V

    return-void
.end method

.method public compressAndCopyScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/helpshift/common/exception/RootAPIException;
        }
    .end annotation

    .line 314
    :try_start_0
    invoke-static {p1, p2}, Lcom/helpshift/support/util/AttachmentUtil;->copyAttachment(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    .line 317
    invoke-static {p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1
.end method

.method public compressAndStoreScreenshot(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 295
    :try_start_0
    invoke-static {p1, p2}, Lcom/helpshift/support/util/AttachmentUtil;->copyAttachment(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez p2, :cond_0

    goto :goto_0

    :cond_0
    move-object p1, p2

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p2

    :try_start_1
    const-string v0, "AndroidPlatform"

    const-string v1, "Saving attachment"

    .line 299
    invoke-static {v0, v1, p2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :goto_0
    return-object p1

    .line 306
    :goto_1
    throw p1
.end method

.method public getAPIKey()Ljava/lang/String;
    .locals 1

    .line 110
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->apiKey:Ljava/lang/String;

    return-object v0
.end method

.method public getAnalyticsEventDAO()Lcom/helpshift/analytics/AnalyticsEventDAO;
    .locals 1

    .line 162
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->analyticsEventDAO:Lcom/helpshift/analytics/AnalyticsEventDAO;

    return-object v0
.end method

.method public getAppId()Ljava/lang/String;
    .locals 1

    .line 120
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->appId:Ljava/lang/String;

    return-object v0
.end method

.method public getBackupDAO()Lcom/helpshift/common/dao/BackupDAO;
    .locals 1

    .line 176
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    return-object v0
.end method

.method public getCampaignModuleAPIs()Lcom/helpshift/providers/ICampaignsModuleAPIs;
    .locals 1

    .line 375
    invoke-static {}, Lcom/helpshift/providers/CrossModuleDataProvider;->getCampaignModuleAPIs()Lcom/helpshift/providers/ICampaignsModuleAPIs;

    move-result-object v0

    return-object v0
.end method

.method public getClearedUserDAO()Lcom/helpshift/account/dao/ClearedUserDAO;
    .locals 1

    .line 220
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->clearedUserDAO:Lcom/helpshift/account/dao/ClearedUserDAO;

    return-object v0
.end method

.method public declared-synchronized getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;
    .locals 2

    monitor-enter p0

    .line 140
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    if-nez v0, :cond_0

    .line 141
    new-instance v0, Lcom/helpshift/common/platform/AndroidConversationDAO;

    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-direct {v0, v1}, Lcom/helpshift/common/platform/AndroidConversationDAO;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    .line 143
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 139
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getConversationInboxDAO()Lcom/helpshift/conversation/dao/ConversationInboxDAO;
    .locals 3

    monitor-enter p0

    .line 131
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    if-nez v0, :cond_0

    .line 132
    new-instance v0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;

    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-virtual {p0}, Lcom/helpshift/common/platform/AndroidPlatform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;-><init>(Landroid/content/Context;Lcom/helpshift/common/platform/KVStore;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;

    .line 134
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationInboxDAO:Lcom/helpshift/conversation/dao/ConversationInboxDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 130
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getCustomIssueFieldDAO()Lcom/helpshift/cif/dao/CustomIssueFieldDAO;
    .locals 2

    monitor-enter p0

    .line 168
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->customIssueFieldDAO:Lcom/helpshift/cif/dao/CustomIssueFieldDAO;

    if-nez v0, :cond_0

    .line 169
    new-instance v0, Lcom/helpshift/common/platform/AndroidCustomIssueFieldDAO;

    invoke-virtual {p0}, Lcom/helpshift/common/platform/AndroidPlatform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/common/platform/AndroidCustomIssueFieldDAO;-><init>(Lcom/helpshift/common/platform/KVStore;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->customIssueFieldDAO:Lcom/helpshift/cif/dao/CustomIssueFieldDAO;

    .line 171
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->customIssueFieldDAO:Lcom/helpshift/cif/dao/CustomIssueFieldDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 167
    monitor-exit p0

    throw v0
.end method

.method public getDevice()Lcom/helpshift/common/platform/Device;
    .locals 1

    .line 125
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->device:Lcom/helpshift/common/platform/Device;

    return-object v0
.end method

.method public getDomain()Ljava/lang/String;
    .locals 1

    .line 115
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->domain:Ljava/lang/String;

    return-object v0
.end method

.method public declared-synchronized getDownloader()Lcom/helpshift/downloader/SupportDownloader;
    .locals 3

    monitor-enter p0

    .line 274
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->downloader:Lcom/helpshift/downloader/SupportDownloader;

    if-nez v0, :cond_0

    .line 275
    new-instance v0, Lcom/helpshift/common/platform/AndroidSupportDownloader;

    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    .line 276
    invoke-virtual {p0}, Lcom/helpshift/common/platform/AndroidPlatform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Lcom/helpshift/common/platform/AndroidSupportDownloader;-><init>(Landroid/content/Context;Lcom/helpshift/common/platform/KVStore;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->downloader:Lcom/helpshift/downloader/SupportDownloader;

    .line 278
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->downloader:Lcom/helpshift/downloader/SupportDownloader;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 273
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getFAQSearchDM()Lcom/helpshift/faq/domainmodel/FAQSearchDM;
    .locals 2

    monitor-enter p0

    .line 192
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->faqSearchDM:Lcom/helpshift/faq/domainmodel/FAQSearchDM;

    if-nez v0, :cond_0

    .line 193
    new-instance v0, Lcom/helpshift/common/platform/AndroidFAQSearchDM;

    invoke-direct {p0}, Lcom/helpshift/common/platform/AndroidPlatform;->getData()Lcom/helpshift/support/HSApiData;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/common/platform/AndroidFAQSearchDM;-><init>(Lcom/helpshift/support/HSApiData;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->faqSearchDM:Lcom/helpshift/faq/domainmodel/FAQSearchDM;

    .line 195
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->faqSearchDM:Lcom/helpshift/faq/domainmodel/FAQSearchDM;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 191
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getFAQSuggestionsDAO()Lcom/helpshift/conversation/dao/FAQSuggestionsDAO;
    .locals 2

    monitor-enter p0

    .line 149
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    if-nez v0, :cond_0

    .line 150
    new-instance v0, Lcom/helpshift/common/platform/AndroidConversationDAO;

    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-direct {v0, v1}, Lcom/helpshift/common/platform/AndroidConversationDAO;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    .line 152
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    check-cast v0, Lcom/helpshift/conversation/dao/FAQSuggestionsDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 148
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getFaqEventDAO()Lcom/helpshift/faq/dao/FaqEventDAO;
    .locals 2

    monitor-enter p0

    .line 235
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

    if-nez v0, :cond_0

    .line 236
    new-instance v0, Lcom/helpshift/common/platform/AndroidFaqEventDAO;

    invoke-virtual {p0}, Lcom/helpshift/common/platform/AndroidPlatform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/common/platform/AndroidFaqEventDAO;-><init>(Lcom/helpshift/common/platform/KVStore;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;

    .line 238
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->faqEventDAO:Lcom/helpshift/faq/dao/FaqEventDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 234
    monitor-exit p0

    throw v0
.end method

.method public getHTTPTransport()Lcom/helpshift/common/platform/network/HTTPTransport;
    .locals 1

    .line 186
    new-instance v0, Lcom/helpshift/common/platform/AndroidHTTPTransport;

    invoke-direct {v0}, Lcom/helpshift/common/platform/AndroidHTTPTransport;-><init>()V

    return-object v0
.end method

.method public getJsonifier()Lcom/helpshift/common/platform/Jsonifier;
    .locals 1

    .line 205
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    return-object v0
.end method

.method public getKVStore()Lcom/helpshift/common/platform/KVStore;
    .locals 1

    .line 200
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->storage:Lcom/helpshift/common/platform/KVStore;

    return-object v0
.end method

.method public declared-synchronized getLegacyAnalyticsEventIDDAO()Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;
    .locals 2

    monitor-enter p0

    .line 404
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->legacyAnalyticsEventIDDAO:Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;

    if-nez v0, :cond_0

    .line 405
    new-instance v0, Lcom/helpshift/account/dao/AndroidLegacyAnalyticsEventIDDAO;

    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-static {v1}, Lcom/helpshift/account/dao/UserDB;->getInstance(Landroid/content/Context;)Lcom/helpshift/account/dao/UserDB;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/account/dao/AndroidLegacyAnalyticsEventIDDAO;-><init>(Lcom/helpshift/account/dao/UserDB;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->legacyAnalyticsEventIDDAO:Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;

    .line 407
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->legacyAnalyticsEventIDDAO:Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 403
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getLegacyUserMigrationDataSource()Lcom/helpshift/migration/LegacyProfileMigrationDAO;
    .locals 2

    monitor-enter p0

    .line 395
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->legacyProfileMigrationDAO:Lcom/helpshift/migration/LegacyProfileMigrationDAO;

    if-nez v0, :cond_0

    .line 396
    new-instance v0, Lcom/helpshift/account/dao/AndroidLegacyProfileMigrationDAO;

    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-static {v1}, Lcom/helpshift/account/dao/UserDB;->getInstance(Landroid/content/Context;)Lcom/helpshift/account/dao/UserDB;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/account/dao/AndroidLegacyProfileMigrationDAO;-><init>(Lcom/helpshift/account/dao/UserDB;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->legacyProfileMigrationDAO:Lcom/helpshift/migration/LegacyProfileMigrationDAO;

    .line 398
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->legacyProfileMigrationDAO:Lcom/helpshift/migration/LegacyProfileMigrationDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 394
    monitor-exit p0

    throw v0
.end method

.method public getMetaDataDAO()Lcom/helpshift/meta/dao/MetaDataDAO;
    .locals 1

    .line 157
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    return-object v0
.end method

.method public getMimeTypeForFile(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 288
    invoke-static {p1}, Lcom/helpshift/util/FileUtil;->getMimeType(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getMinimumConversationDescriptionLength()I
    .locals 2

    .line 324
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiContext:Landroid/content/Context;

    if-eqz v0, :cond_0

    .line 325
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiContext:Landroid/content/Context;

    goto :goto_0

    .line 328
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    .line 330
    :goto_0
    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/helpshift/R$integer;->hs__issue_description_min_chars:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getInteger(I)I

    move-result v0

    return v0
.end method

.method public declared-synchronized getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;
    .locals 2

    monitor-enter p0

    .line 226
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    if-nez v0, :cond_0

    .line 227
    new-instance v0, Lcom/helpshift/common/platform/AndroidNetworkRequestDAO;

    invoke-virtual {p0}, Lcom/helpshift/common/platform/AndroidPlatform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/common/platform/AndroidNetworkRequestDAO;-><init>(Lcom/helpshift/common/platform/KVStore;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    .line 229
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->networkRequestDAO:Lcom/helpshift/common/platform/network/NetworkRequestDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 225
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized getRedactionDAO()Lcom/helpshift/redaction/RedactionDAO;
    .locals 2

    monitor-enter p0

    .line 413
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->redactionDAO:Lcom/helpshift/redaction/RedactionDAO;

    if-nez v0, :cond_0

    .line 414
    new-instance v0, Lcom/helpshift/account/dao/AndroidRedactionDAO;

    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-static {v1}, Lcom/helpshift/account/dao/UserDB;->getInstance(Landroid/content/Context;)Lcom/helpshift/account/dao/UserDB;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/account/dao/AndroidRedactionDAO;-><init>(Lcom/helpshift/account/dao/UserDB;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->redactionDAO:Lcom/helpshift/redaction/RedactionDAO;

    .line 416
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->redactionDAO:Lcom/helpshift/redaction/RedactionDAO;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 412
    monitor-exit p0

    throw v0
.end method

.method public getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;
    .locals 1

    .line 181
    new-instance v0, Lcom/helpshift/common/platform/AndroidResponseParser;

    invoke-direct {v0}, Lcom/helpshift/common/platform/AndroidResponseParser;-><init>()V

    return-object v0
.end method

.method public declared-synchronized getUIThreader()Lcom/helpshift/common/domain/Threader;
    .locals 1

    monitor-enter p0

    .line 249
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiThreader:Lcom/helpshift/common/domain/Threader;

    if-nez v0, :cond_0

    .line 250
    new-instance v0, Lcom/helpshift/common/platform/AndroidPlatform$1;

    invoke-direct {v0, p0}, Lcom/helpshift/common/platform/AndroidPlatform$1;-><init>(Lcom/helpshift/common/platform/AndroidPlatform;)V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiThreader:Lcom/helpshift/common/domain/Threader;

    .line 268
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiThreader:Lcom/helpshift/common/domain/Threader;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 248
    monitor-exit p0

    throw v0
.end method

.method public getUserDAO()Lcom/helpshift/account/dao/UserDAO;
    .locals 1

    .line 215
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->userDAO:Lcom/helpshift/account/dao/UserDAO;

    return-object v0
.end method

.method public getUserManagerDAO()Lcom/helpshift/account/dao/UserManagerDAO;
    .locals 1

    .line 210
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->userManagerDAO:Lcom/helpshift/account/dao/AndroidUserManagerDAO;

    return-object v0
.end method

.method public isCurrentThreadUIThread()Z
    .locals 2

    .line 243
    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isOnline()Z
    .locals 1

    .line 380
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-static {v0}, Lcom/helpshift/util/HelpshiftConnectionUtil;->isOnline(Landroid/content/Context;)Z

    move-result v0

    return v0
.end method

.method public isSupportedMimeType(Ljava/lang/String;)Z
    .locals 0

    .line 283
    invoke-static {p1}, Lcom/helpshift/util/FileUtil;->isSupportedMimeType(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public setUIContext(Ljava/lang/Object;)V
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    .line 386
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiContext:Landroid/content/Context;

    goto :goto_0

    .line 388
    :cond_0
    instance-of v0, p1, Landroid/content/Context;

    if-eqz v0, :cond_1

    .line 389
    check-cast p1, Landroid/content/Context;

    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiContext:Landroid/content/Context;

    :cond_1
    :goto_0
    return-void
.end method

.method public showNotification(Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V
    .locals 1

    .line 340
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiContext:Landroid/content/Context;

    if-eqz v0, :cond_0

    .line 341
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->uiContext:Landroid/content/Context;

    goto :goto_0

    .line 344
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-static {v0}, Lcom/helpshift/util/ApplicationUtil;->getContextWithUpdatedLocale(Landroid/content/Context;)Landroid/content/Context;

    move-result-object v0

    .line 348
    :goto_0
    invoke-static {v0, p1, p2, p3, p4}, Lcom/helpshift/support/util/SupportNotification;->createNotification(Landroid/content/Context;Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;)Landroidx/core/app/NotificationCompat$Builder;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 355
    invoke-virtual {p1}, Landroidx/core/app/NotificationCompat$Builder;->build()Landroid/app/Notification;

    move-result-object p1

    .line 358
    new-instance p4, Lcom/helpshift/notifications/NotificationChannelsManager;

    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-direct {p4, v0}, Lcom/helpshift/notifications/NotificationChannelsManager;-><init>(Landroid/content/Context;)V

    .line 359
    sget-object v0, Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;->SUPPORT:Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;

    invoke-virtual {p4, p1, v0}, Lcom/helpshift/notifications/NotificationChannelsManager;->attachChannelId(Landroid/app/Notification;Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;)Landroid/app/Notification;

    move-result-object p1

    .line 360
    iget-object p4, p0, Lcom/helpshift/common/platform/AndroidPlatform;->context:Landroid/content/Context;

    invoke-static {p4, p2, p1}, Lcom/helpshift/util/ApplicationUtil;->showNotification(Landroid/content/Context;Ljava/lang/String;Landroid/app/Notification;)V

    if-eqz p5, :cond_1

    const-string p1, "didReceiveInAppNotificationCount"

    .line 363
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string p4, ""

    invoke-virtual {p2, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/helpshift/PluginEventBridge;->sendMessage(Ljava/lang/String;Ljava/lang/String;)V

    :cond_1
    return-void
.end method
