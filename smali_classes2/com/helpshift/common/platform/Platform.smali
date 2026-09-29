.class public interface abstract Lcom/helpshift/common/platform/Platform;
.super Ljava/lang/Object;
.source "Platform.java"


# virtual methods
.method public abstract canReadFileAtUri(Ljava/lang/String;)Z
.end method

.method public abstract clearNotifications(Ljava/lang/String;)V
.end method

.method public abstract compressAndCopyScreenshot(Lcom/helpshift/conversation/dto/ImagePickerFile;Ljava/lang/String;)V
.end method

.method public abstract compressAndStoreScreenshot(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
.end method

.method public abstract getAPIKey()Ljava/lang/String;
.end method

.method public abstract getAnalyticsEventDAO()Lcom/helpshift/analytics/AnalyticsEventDAO;
.end method

.method public abstract getAppId()Ljava/lang/String;
.end method

.method public abstract getBackupDAO()Lcom/helpshift/common/dao/BackupDAO;
.end method

.method public abstract getCampaignModuleAPIs()Lcom/helpshift/providers/ICampaignsModuleAPIs;
.end method

.method public abstract getClearedUserDAO()Lcom/helpshift/account/dao/ClearedUserDAO;
.end method

.method public abstract getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;
.end method

.method public abstract getConversationInboxDAO()Lcom/helpshift/conversation/dao/ConversationInboxDAO;
.end method

.method public abstract getCustomIssueFieldDAO()Lcom/helpshift/cif/dao/CustomIssueFieldDAO;
.end method

.method public abstract getDevice()Lcom/helpshift/common/platform/Device;
.end method

.method public abstract getDomain()Ljava/lang/String;
.end method

.method public abstract getDownloader()Lcom/helpshift/downloader/SupportDownloader;
.end method

.method public abstract getFAQSearchDM()Lcom/helpshift/faq/domainmodel/FAQSearchDM;
.end method

.method public abstract getFAQSuggestionsDAO()Lcom/helpshift/conversation/dao/FAQSuggestionsDAO;
.end method

.method public abstract getFaqEventDAO()Lcom/helpshift/faq/dao/FaqEventDAO;
.end method

.method public abstract getHTTPTransport()Lcom/helpshift/common/platform/network/HTTPTransport;
.end method

.method public abstract getJsonifier()Lcom/helpshift/common/platform/Jsonifier;
.end method

.method public abstract getKVStore()Lcom/helpshift/common/platform/KVStore;
.end method

.method public abstract getLegacyAnalyticsEventIDDAO()Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;
.end method

.method public abstract getLegacyUserMigrationDataSource()Lcom/helpshift/migration/LegacyProfileMigrationDAO;
.end method

.method public abstract getMetaDataDAO()Lcom/helpshift/meta/dao/MetaDataDAO;
.end method

.method public abstract getMimeTypeForFile(Ljava/lang/String;)Ljava/lang/String;
.end method

.method public abstract getMinimumConversationDescriptionLength()I
.end method

.method public abstract getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;
.end method

.method public abstract getRedactionDAO()Lcom/helpshift/redaction/RedactionDAO;
.end method

.method public abstract getResponseParser()Lcom/helpshift/common/platform/network/ResponseParser;
.end method

.method public abstract getUIThreader()Lcom/helpshift/common/domain/Threader;
.end method

.method public abstract getUserDAO()Lcom/helpshift/account/dao/UserDAO;
.end method

.method public abstract getUserManagerDAO()Lcom/helpshift/account/dao/UserManagerDAO;
.end method

.method public abstract isCurrentThreadUIThread()Z
.end method

.method public abstract isOnline()Z
.end method

.method public abstract isSupportedMimeType(Ljava/lang/String;)Z
.end method

.method public abstract setUIContext(Ljava/lang/Object;)V
.end method

.method public abstract showNotification(Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V
.end method
