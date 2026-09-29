.class public Lcom/helpshift/support/SupportMigrator;
.super Ljava/lang/Object;
.source "SupportMigrator.java"


# static fields
.field public static final TAG:Ljava/lang/String; = "Helpshift_SupportMigr"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static deleteDBLockFilesOnSDKMigration(Landroid/content/Context;)V
    .locals 3

    .line 227
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    sget-object v1, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "__hs_supportkvdb_lock"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 228
    new-instance v1, Ljava/io/File;

    invoke-direct {v1, v0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 229
    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 230
    invoke-virtual {v1}, Ljava/io/File;->delete()Z

    .line 232
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    sget-object v1, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "__hs_kvdb_lock"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 233
    new-instance v1, Ljava/io/File;

    invoke-direct {v1, v0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 234
    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 235
    invoke-virtual {v1}, Ljava/io/File;->delete()Z

    .line 239
    :cond_1
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, ".backups/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 240
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "/helpshift/databases/"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    .line 242
    invoke-static {p0}, Landroid/os/Environment;->getExternalStoragePublicDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object p0

    if-eqz p0, :cond_3

    .line 243
    invoke-virtual {p0}, Ljava/io/File;->canWrite()Z

    move-result v0

    if-eqz v0, :cond_3

    .line 244
    new-instance v0, Ljava/io/File;

    const-string v1, "__hs__db_profiles"

    invoke-direct {v0, p0, v1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 245
    invoke-virtual {v0}, Ljava/io/File;->canWrite()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 246
    invoke-virtual {v0}, Ljava/io/File;->delete()Z

    .line 249
    :cond_2
    new-instance v0, Ljava/io/File;

    const-string v1, "__hs__kv_backup"

    invoke-direct {v0, p0, v1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 250
    invoke-virtual {v0}, Ljava/io/File;->canWrite()Z

    move-result p0

    if-eqz p0, :cond_3

    .line 251
    invoke-virtual {v0}, Ljava/io/File;->delete()Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    const-string v0, "Helpshift_SupportMigr"

    .line 256
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Error on deleting lock file: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    :cond_3
    :goto_0
    return-void
.end method

.method private static fixDuplicateConversations(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/util/VersionName;)V
    .locals 8

    .line 167
    new-instance v0, Lcom/helpshift/util/VersionName;

    const-string v1, "7.0.0"

    invoke-direct {v0, v1}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    invoke-virtual {p2, v0}, Lcom/helpshift/util/VersionName;->isGreaterThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z

    move-result p2

    if-eqz p2, :cond_9

    .line 170
    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getAllUsers()Ljava/util/List;

    move-result-object p2

    .line 171
    invoke-interface {p0}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p0

    .line 174
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    .line 175
    new-instance v1, Ljava/util/HashSet;

    invoke-direct {v1}, Ljava/util/HashSet;-><init>()V

    .line 177
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :cond_0
    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_9

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/account/domainmodel/UserDM;

    .line 178
    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v3

    invoke-virtual {v3, v2}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getConversationInboxDM(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v3

    .line 179
    invoke-virtual {v3}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getActiveConversationFromStorage()Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v3

    if-nez v3, :cond_1

    goto :goto_0

    .line 187
    :cond_1
    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-interface {p0, v2, v3}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v2

    .line 188
    invoke-static {v2}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v3

    if-eqz v3, :cond_2

    goto :goto_0

    .line 192
    :cond_2
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_3
    :goto_1
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 194
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v4

    const/4 v5, 0x1

    const/4 v6, 0x0

    if-nez v4, :cond_4

    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    .line 195
    invoke-interface {v1, v4}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_4

    const/4 v4, 0x1

    goto :goto_2

    :cond_4
    const/4 v4, 0x0

    .line 197
    :goto_2
    iget-object v7, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v7}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v7

    if-nez v7, :cond_5

    iget-object v7, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 198
    invoke-interface {v0, v7}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v7

    if-eqz v7, :cond_5

    goto :goto_3

    :cond_5
    const/4 v5, 0x0

    :goto_3
    if-nez v4, :cond_8

    if-eqz v5, :cond_6

    goto :goto_4

    .line 210
    :cond_6
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_7

    .line 212
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    invoke-interface {v1, v4}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 215
    :cond_7
    iget-object v4, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_3

    .line 217
    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    invoke-interface {v0, v3}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 205
    :cond_8
    :goto_4
    invoke-interface {p0}, Lcom/helpshift/conversation/dao/ConversationDAO;->dropAndCreateDatabase()V

    .line 206
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p0

    invoke-interface {p0}, Lcom/helpshift/CoreApi;->resetUsersSyncStatusAndStartSetupForActiveUser()V

    return-void

    :cond_9
    return-void
.end method

.method public static migrate(Landroid/content/Context;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/support/HSApiData;Lcom/helpshift/support/HSStorage;)V
    .locals 15

    move-object/from16 v1, p1

    move-object/from16 v2, p2

    move-object/from16 v12, p4

    .line 37
    invoke-virtual/range {p4 .. p4}, Lcom/helpshift/support/HSStorage;->getLibraryVersion()Ljava/lang/String;

    move-result-object v13

    .line 40
    invoke-virtual {v13}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_1

    const-string v0, "7.6.3"

    .line 41
    invoke-virtual {v13, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 42
    new-instance v3, Lcom/helpshift/util/VersionName;

    const-string v0, "0"

    invoke-direct {v3, v0}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    .line 44
    :try_start_0
    new-instance v0, Lcom/helpshift/util/VersionName;

    invoke-direct {v0, v13}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v4, "Helpshift_SupportMigr"

    .line 47
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Error in creating SemVer: "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v4, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    move-object v0, v3

    .line 53
    :goto_0
    new-instance v3, Lcom/helpshift/util/VersionName;

    const-string v4, "7.0.0"

    invoke-direct {v3, v4}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v3}, Lcom/helpshift/util/VersionName;->isGreaterThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z

    move-result v3

    xor-int/lit8 v3, v3, 0x1

    if-eqz v3, :cond_0

    .line 58
    new-instance v14, Lcom/helpshift/support/storage/LegacyUserDataMigrator;

    .line 59
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v4

    .line 61
    invoke-interface/range {p1 .. p1}, Lcom/helpshift/common/platform/Platform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object v6

    .line 62
    invoke-static {p0}, Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;->getInstance(Landroid/content/Context;)Lcom/helpshift/account/dao/legacy/AndroidLegacyProfileDAO;

    move-result-object v7

    .line 63
    invoke-interface/range {p1 .. p1}, Lcom/helpshift/common/platform/Platform;->getBackupDAO()Lcom/helpshift/common/dao/BackupDAO;

    move-result-object v8

    .line 64
    invoke-interface/range {p1 .. p1}, Lcom/helpshift/common/platform/Platform;->getLegacyUserMigrationDataSource()Lcom/helpshift/migration/LegacyProfileMigrationDAO;

    move-result-object v9

    .line 65
    invoke-interface/range {p1 .. p1}, Lcom/helpshift/common/platform/Platform;->getLegacyAnalyticsEventIDDAO()Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;

    move-result-object v10

    move-object v3, v14

    move-object/from16 v5, p4

    move-object v11, v0

    invoke-direct/range {v3 .. v11}, Lcom/helpshift/support/storage/LegacyUserDataMigrator;-><init>(Lcom/helpshift/CoreApi;Lcom/helpshift/support/HSStorage;Lcom/helpshift/common/platform/KVStore;Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;Lcom/helpshift/common/dao/BackupDAO;Lcom/helpshift/migration/LegacyProfileMigrationDAO;Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;Lcom/helpshift/util/VersionName;)V

    .line 67
    new-instance v3, Lcom/helpshift/support/storage/SupportKVStoreMigrator;

    invoke-direct {v3, v12}, Lcom/helpshift/support/storage/SupportKVStoreMigrator;-><init>(Lcom/helpshift/support/HSStorage;)V

    .line 70
    invoke-virtual {v14, v0}, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->backup(Lcom/helpshift/util/VersionName;)V

    .line 71
    invoke-virtual {v3, v0}, Lcom/helpshift/support/storage/SupportKVStoreMigrator;->backup(Lcom/helpshift/util/VersionName;)V

    .line 74
    invoke-virtual/range {p3 .. p3}, Lcom/helpshift/support/HSApiData;->clearETagsForFaqs()V

    .line 75
    invoke-virtual/range {p4 .. p4}, Lcom/helpshift/support/HSStorage;->clearDatabase()V

    .line 76
    invoke-virtual {v14}, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->dropProfileDB()V

    .line 78
    invoke-interface/range {p1 .. p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/conversation/dao/ConversationDAO;->dropAndCreateDatabase()V

    .line 82
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->resetUsersSyncStatusAndStartSetupForActiveUser()V

    .line 84
    invoke-interface/range {p1 .. p1}, Lcom/helpshift/common/platform/Platform;->getKVStore()Lcom/helpshift/common/platform/KVStore;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/KVStore;->removeAllKeys()V

    .line 87
    invoke-virtual {v14}, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->restore()V

    .line 88
    invoke-virtual {v3}, Lcom/helpshift/support/storage/SupportKVStoreMigrator;->restore()V

    .line 91
    invoke-virtual/range {p2 .. p2}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUserSetupDM()Lcom/helpshift/account/domainmodel/UserSetupDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserSetupDM;->startSetup()V

    goto :goto_1

    .line 97
    :cond_0
    invoke-static {v1, v2, v0}, Lcom/helpshift/support/SupportMigrator;->fixDuplicateConversations(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/util/VersionName;)V

    .line 104
    invoke-static {v1, v2, v0}, Lcom/helpshift/support/SupportMigrator;->updateRejectConversations(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/util/VersionName;)V

    .line 110
    invoke-static {v1, v0}, Lcom/helpshift/support/SupportMigrator;->removeConfigApiEtag(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/util/VersionName;)V

    .line 114
    :cond_1
    :goto_1
    invoke-virtual/range {p4 .. p4}, Lcom/helpshift/support/HSStorage;->clearLegacySearchIndexFile()V

    .line 115
    invoke-static {p0}, Lcom/helpshift/support/SupportMigrator;->deleteDBLockFilesOnSDKMigration(Landroid/content/Context;)V

    const-string v0, "7.6.3"

    .line 118
    invoke-virtual {v0, v13}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_2

    const-string v0, "7.6.3"

    .line 119
    invoke-virtual {v12, v0}, Lcom/helpshift/support/HSStorage;->setLibraryVersion(Ljava/lang/String;)V

    :cond_2
    return-void
.end method

.method private static removeConfigApiEtag(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/util/VersionName;)V
    .locals 2

    .line 124
    new-instance v0, Lcom/helpshift/util/VersionName;

    const-string v1, "7.5.0"

    invoke-direct {v0, v1}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/util/VersionName;->isLessThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 125
    invoke-interface {p0}, Lcom/helpshift/common/platform/Platform;->getNetworkRequestDAO()Lcom/helpshift/common/platform/network/NetworkRequestDAO;

    move-result-object p0

    sget-object p1, Lcom/helpshift/common/domain/network/NetworkConstants;->SUPPORT_CONFIG_ROUTE:Ljava/lang/String;

    invoke-interface {p0, p1}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->removeETag(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method private static updateRejectConversations(Lcom/helpshift/common/platform/Platform;Lcom/helpshift/common/domain/Domain;Lcom/helpshift/util/VersionName;)V
    .locals 5

    .line 137
    new-instance v0, Lcom/helpshift/util/VersionName;

    const-string v1, "7.0.0"

    invoke-direct {v0, v1}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    invoke-virtual {p2, v0}, Lcom/helpshift/util/VersionName;->isGreaterThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z

    move-result v0

    if-eqz v0, :cond_4

    new-instance v0, Lcom/helpshift/util/VersionName;

    const-string v1, "7.1.0"

    invoke-direct {v0, v1}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    .line 138
    invoke-virtual {p2, v0}, Lcom/helpshift/util/VersionName;->isLessThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z

    move-result p2

    if-eqz p2, :cond_4

    .line 139
    invoke-interface {p0}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p0

    .line 140
    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getAllUsers()Ljava/util/List;

    move-result-object p2

    .line 142
    invoke-static {p2}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 145
    :cond_0
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :cond_1
    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_4

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/account/domainmodel/UserDM;

    .line 147
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {p0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationsWithoutMessages(J)Ljava/util/List;

    move-result-object v1

    .line 149
    invoke-static {v1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v2

    if-eqz v2, :cond_2

    goto :goto_0

    .line 152
    :cond_2
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_3
    :goto_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 154
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->state:Lcom/helpshift/conversation/dto/IssueState;

    sget-object v4, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    if-ne v3, v4, :cond_3

    iget-boolean v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isStartNewConversationClicked:Z

    if-nez v3, :cond_3

    .line 156
    invoke-virtual {v0}, Lcom/helpshift/account/domainmodel/UserDM;->getLocalId()Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    iput-wide v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->userLocalId:J

    .line 157
    invoke-virtual {p1}, Lcom/helpshift/common/domain/Domain;->getConversationInboxManagerDM()Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;

    move-result-object v3

    invoke-virtual {v3, v0}, Lcom/helpshift/conversation/domainmodel/ConversationInboxManagerDM;->getConversationInboxDM(Lcom/helpshift/account/domainmodel/UserDM;)Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v3

    iget-object v3, v3, Lcom/helpshift/conversation/domainmodel/ConversationController;->conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    const/4 v4, 0x1

    .line 158
    invoke-virtual {v3, v2, v4, v4}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->setStartNewConversationButtonClicked(Lcom/helpshift/conversation/activeconversation/model/Conversation;ZZ)V

    goto :goto_1

    :cond_4
    return-void
.end method
