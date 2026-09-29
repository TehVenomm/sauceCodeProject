.class public Lcom/helpshift/support/storage/LegacyUserDataMigrator;
.super Ljava/lang/Object;
.source "LegacyUserDataMigrator.java"

# interfaces
.implements Lcom/helpshift/support/storage/SDKMigrator;


# static fields
.field private static final DEFAULT_USER_LOGIN_KEY:Ljava/lang/String; = "default_user_login"

.field private static final DEFAULT_USER_PROFILE:Ljava/lang/String; = "default_user_profile"

.field private static final KEY_DEVICE_ID:Ljava/lang/String; = "key_support_device_id"

.field private static final LOGIN_IDENTIFIER:Ljava/lang/String; = "loginIdentifier"


# instance fields
.field private backupDAO:Lcom/helpshift/common/dao/BackupDAO;

.field private coreApi:Lcom/helpshift/CoreApi;

.field private defaultIdentifier:Ljava/lang/String;

.field private defaultProfileDto:Lcom/helpshift/account/dao/ProfileDTO;

.field private domain:Lcom/helpshift/common/domain/Domain;

.field private fromVersion:Lcom/helpshift/util/VersionName;

.field private hsStorage:Lcom/helpshift/support/HSStorage;

.field private kvStore:Lcom/helpshift/common/platform/KVStore;

.field private legacyAnalyticsEventIDDAO:Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;

.field private legacyProfileDAO:Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;

.field private legacyProfileMigrationDAO:Lcom/helpshift/migration/LegacyProfileMigrationDAO;

.field private loginIdentifier:Ljava/lang/String;

.field private loginProfileDtos:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/account/dao/ProfileDTO;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Lcom/helpshift/CoreApi;Lcom/helpshift/support/HSStorage;Lcom/helpshift/common/platform/KVStore;Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;Lcom/helpshift/common/dao/BackupDAO;Lcom/helpshift/migration/LegacyProfileMigrationDAO;Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;Lcom/helpshift/util/VersionName;)V
    .locals 0

    .line 61
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 62
    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->coreApi:Lcom/helpshift/CoreApi;

    .line 63
    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getDomain()Lcom/helpshift/common/domain/Domain;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->domain:Lcom/helpshift/common/domain/Domain;

    .line 64
    iput-object p2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->hsStorage:Lcom/helpshift/support/HSStorage;

    .line 65
    iput-object p3, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->kvStore:Lcom/helpshift/common/platform/KVStore;

    .line 66
    iput-object p4, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->legacyProfileDAO:Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;

    .line 67
    iput-object p5, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    .line 68
    iput-object p6, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->legacyProfileMigrationDAO:Lcom/helpshift/migration/LegacyProfileMigrationDAO;

    .line 69
    iput-object p7, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->legacyAnalyticsEventIDDAO:Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;

    .line 70
    iput-object p8, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->fromVersion:Lcom/helpshift/util/VersionName;

    return-void
.end method


# virtual methods
.method public backup(Lcom/helpshift/util/VersionName;)V
    .locals 13

    .line 75
    new-instance v0, Lcom/helpshift/util/VersionName;

    const-string v1, "7.0.0"

    invoke-direct {v0, v1}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/util/VersionName;->isGreaterThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 79
    :cond_0
    new-instance v0, Lcom/helpshift/util/VersionName;

    const-string v1, "4.9.1"

    invoke-direct {v0, v1}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/util/VersionName;->isLessThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z

    move-result p1

    if-eqz p1, :cond_2

    .line 82
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->hsStorage:Lcom/helpshift/support/HSStorage;

    const-string v0, "loginIdentifier"

    invoke-virtual {p1, v0}, Lcom/helpshift/support/HSStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginIdentifier:Ljava/lang/String;

    .line 85
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->hsStorage:Lcom/helpshift/support/HSStorage;

    const-string v0, "identity"

    invoke-virtual {p1, v0}, Lcom/helpshift/support/HSStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 87
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->hsStorage:Lcom/helpshift/support/HSStorage;

    const-string v0, "uuid"

    invoke-virtual {p1, v0}, Lcom/helpshift/support/HSStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    .line 88
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 89
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    invoke-virtual {p1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p1

    const-string v0, "android_id"

    invoke-static {p1, v0}, Landroid/provider/Settings$Secure;->getString(Landroid/content/ContentResolver;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    .line 92
    :cond_1
    new-instance p1, Lcom/helpshift/account/dao/ProfileDTO;

    const/4 v2, 0x0

    iget-object v3, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->hsStorage:Lcom/helpshift/support/HSStorage;

    const-string v1, "username"

    .line 95
    invoke-virtual {v0, v1}, Lcom/helpshift/support/HSStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->hsStorage:Lcom/helpshift/support/HSStorage;

    const-string v1, "email"

    .line 96
    invoke-virtual {v0, v1}, Lcom/helpshift/support/HSStorage;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x1

    move-object v1, p1

    invoke-direct/range {v1 .. v10}, Lcom/helpshift/account/dao/ProfileDTO;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultProfileDto:Lcom/helpshift/account/dao/ProfileDTO;

    .line 102
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->legacyProfileDAO:Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;

    invoke-interface {p1}, Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;->fetchProfiles()Ljava/util/List;

    move-result-object p1

    .line 103
    invoke-static {p1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v0

    if-nez v0, :cond_4

    .line 104
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginProfileDtos:Ljava/util/List;

    .line 105
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/account/dao/ProfileDTO;

    .line 114
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v2, v0, Lcom/helpshift/account/dao/ProfileDTO;->identifier:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "_"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, v0, Lcom/helpshift/account/dao/ProfileDTO;->saltedIdentifier:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v9

    .line 115
    new-instance v1, Lcom/helpshift/account/dao/ProfileDTO;

    iget-object v4, v0, Lcom/helpshift/account/dao/ProfileDTO;->localId:Ljava/lang/Long;

    iget-object v5, v0, Lcom/helpshift/account/dao/ProfileDTO;->identifier:Ljava/lang/String;

    iget-object v6, v0, Lcom/helpshift/account/dao/ProfileDTO;->serverId:Ljava/lang/String;

    iget-object v7, v0, Lcom/helpshift/account/dao/ProfileDTO;->name:Ljava/lang/String;

    iget-object v8, v0, Lcom/helpshift/account/dao/ProfileDTO;->email:Ljava/lang/String;

    iget-object v10, v0, Lcom/helpshift/account/dao/ProfileDTO;->uid:Ljava/lang/String;

    iget-object v11, v0, Lcom/helpshift/account/dao/ProfileDTO;->did:Ljava/lang/String;

    iget-boolean v12, v0, Lcom/helpshift/account/dao/ProfileDTO;->isPushTokenSynced:Z

    move-object v3, v1

    invoke-direct/range {v3 .. v12}, Lcom/helpshift/account/dao/ProfileDTO;-><init>(Ljava/lang/Long;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V

    .line 124
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginProfileDtos:Ljava/util/List;

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 132
    :cond_2
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v0, "loginIdentifier"

    invoke-interface {p1, v0}, Lcom/helpshift/common/platform/KVStore;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginIdentifier:Ljava/lang/String;

    .line 135
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v0, "default_user_login"

    invoke-interface {p1, v0}, Lcom/helpshift/common/platform/KVStore;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    .line 136
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_3

    .line 137
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v0, "default_user_profile"

    invoke-interface {p1, v0}, Lcom/helpshift/common/platform/KVStore;->getSerializable(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    .line 138
    instance-of v0, p1, Lcom/helpshift/account/dao/ProfileDTO;

    if-eqz v0, :cond_3

    .line 139
    check-cast p1, Lcom/helpshift/account/dao/ProfileDTO;

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultProfileDto:Lcom/helpshift/account/dao/ProfileDTO;

    .line 144
    :cond_3
    iget-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->legacyProfileDAO:Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;

    invoke-interface {p1}, Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;->fetchProfiles()Ljava/util/List;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginProfileDtos:Ljava/util/List;

    :cond_4
    return-void
.end method

.method public dropProfileDB()V
    .locals 1

    .line 222
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->legacyProfileDAO:Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;

    invoke-interface {v0}, Lcom/helpshift/migration/legacyUser/LegacyProfileDAO;->deleteProfiles()V

    return-void
.end method

.method public restore()V
    .locals 11

    .line 150
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->fromVersion:Lcom/helpshift/util/VersionName;

    new-instance v1, Lcom/helpshift/util/VersionName;

    const-string v2, "7.0.0"

    invoke-direct {v1, v2}, Lcom/helpshift/util/VersionName;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/util/VersionName;->isGreaterThanOrEqualTo(Lcom/helpshift/util/VersionName;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 155
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    if-eqz v0, :cond_1

    .line 156
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "key_support_device_id"

    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/platform/KVStore;->setString(Ljava/lang/String;Ljava/lang/String;)V

    .line 157
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->backupDAO:Lcom/helpshift/common/dao/BackupDAO;

    const-string v1, "key_support_device_id"

    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultIdentifier:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/common/dao/BackupDAO;->storeValue(Ljava/lang/String;Ljava/io/Serializable;)V

    .line 160
    :cond_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 161
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 163
    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultProfileDto:Lcom/helpshift/account/dao/ProfileDTO;

    if-eqz v2, :cond_3

    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultProfileDto:Lcom/helpshift/account/dao/ProfileDTO;

    iget-object v2, v2, Lcom/helpshift/account/dao/ProfileDTO;->serverId:Ljava/lang/String;

    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_3

    .line 165
    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v2

    if-nez v2, :cond_2

    .line 168
    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v2}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v2

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserManagerDM;->createAnonymousUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v2

    .line 171
    :cond_2
    new-instance v9, Lcom/helpshift/migration/legacyUser/LegacyProfile;

    invoke-virtual {v2}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v4

    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultProfileDto:Lcom/helpshift/account/dao/ProfileDTO;

    iget-object v5, v2, Lcom/helpshift/account/dao/ProfileDTO;->email:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultProfileDto:Lcom/helpshift/account/dao/ProfileDTO;

    iget-object v6, v2, Lcom/helpshift/account/dao/ProfileDTO;->name:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->defaultProfileDto:Lcom/helpshift/account/dao/ProfileDTO;

    iget-object v7, v2, Lcom/helpshift/account/dao/ProfileDTO;->serverId:Ljava/lang/String;

    sget-object v8, Lcom/helpshift/migration/MigrationState;->NOT_STARTED:Lcom/helpshift/migration/MigrationState;

    move-object v3, v9

    invoke-direct/range {v3 .. v8}, Lcom/helpshift/migration/legacyUser/LegacyProfile;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/migration/MigrationState;)V

    invoke-interface {v1, v9}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 179
    :cond_3
    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginProfileDtos:Ljava/util/List;

    invoke-static {v2}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v2

    if-nez v2, :cond_5

    .line 180
    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginProfileDtos:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_5

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/helpshift/account/dao/ProfileDTO;

    .line 182
    iget-object v4, v3, Lcom/helpshift/account/dao/ProfileDTO;->serverId:Ljava/lang/String;

    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_4

    .line 183
    new-instance v4, Lcom/helpshift/migration/legacyUser/LegacyProfile;

    iget-object v6, v3, Lcom/helpshift/account/dao/ProfileDTO;->identifier:Ljava/lang/String;

    iget-object v7, v3, Lcom/helpshift/account/dao/ProfileDTO;->email:Ljava/lang/String;

    iget-object v8, v3, Lcom/helpshift/account/dao/ProfileDTO;->name:Ljava/lang/String;

    iget-object v9, v3, Lcom/helpshift/account/dao/ProfileDTO;->serverId:Ljava/lang/String;

    sget-object v10, Lcom/helpshift/migration/MigrationState;->NOT_STARTED:Lcom/helpshift/migration/MigrationState;

    move-object v5, v4

    invoke-direct/range {v5 .. v10}, Lcom/helpshift/migration/legacyUser/LegacyProfile;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/migration/MigrationState;)V

    invoke-interface {v1, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 190
    :cond_4
    new-instance v4, Lcom/helpshift/common/platform/network/KeyValuePair;

    iget-object v5, v3, Lcom/helpshift/account/dao/ProfileDTO;->identifier:Ljava/lang/String;

    iget-object v3, v3, Lcom/helpshift/account/dao/ProfileDTO;->saltedIdentifier:Ljava/lang/String;

    invoke-direct {v4, v5, v3}, Lcom/helpshift/common/platform/network/KeyValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 191
    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 195
    :cond_5
    invoke-static {v1}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v2

    if-nez v2, :cond_6

    .line 196
    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->legacyProfileMigrationDAO:Lcom/helpshift/migration/LegacyProfileMigrationDAO;

    invoke-interface {v2, v1}, Lcom/helpshift/migration/LegacyProfileMigrationDAO;->storeLegacyProfiles(Ljava/util/List;)V

    .line 199
    :cond_6
    invoke-static {v0}, Lcom/helpshift/common/ListUtils;->isEmpty(Ljava/util/List;)Z

    move-result v1

    if-nez v1, :cond_7

    .line 200
    iget-object v1, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->legacyAnalyticsEventIDDAO:Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;

    invoke-interface {v1, v0}, Lcom/helpshift/migration/LegacyAnalyticsEventIDDAO;->storeLegacyAnalyticsEventIDs(Ljava/util/List;)V

    .line 203
    :cond_7
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginIdentifier:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_8

    .line 205
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->coreApi:Lcom/helpshift/CoreApi;

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->logout()Z

    goto :goto_1

    .line 207
    :cond_8
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginProfileDtos:Ljava/util/List;

    if-eqz v0, :cond_a

    .line 209
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginProfileDtos:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_9
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_a

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/account/dao/ProfileDTO;

    .line 211
    iget-object v2, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->loginIdentifier:Ljava/lang/String;

    iget-object v3, v1, Lcom/helpshift/account/dao/ProfileDTO;->identifier:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_9

    .line 212
    iget-object v0, p0, Lcom/helpshift/support/storage/LegacyUserDataMigrator;->coreApi:Lcom/helpshift/CoreApi;

    new-instance v2, Lcom/helpshift/HelpshiftUser$Builder;

    iget-object v3, v1, Lcom/helpshift/account/dao/ProfileDTO;->identifier:Ljava/lang/String;

    iget-object v4, v1, Lcom/helpshift/account/dao/ProfileDTO;->email:Ljava/lang/String;

    invoke-direct {v2, v3, v4}, Lcom/helpshift/HelpshiftUser$Builder;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v1, v1, Lcom/helpshift/account/dao/ProfileDTO;->email:Ljava/lang/String;

    .line 213
    invoke-virtual {v2, v1}, Lcom/helpshift/HelpshiftUser$Builder;->setName(Ljava/lang/String;)Lcom/helpshift/HelpshiftUser$Builder;

    move-result-object v1

    .line 214
    invoke-virtual {v1}, Lcom/helpshift/HelpshiftUser$Builder;->build()Lcom/helpshift/HelpshiftUser;

    move-result-object v1

    .line 212
    invoke-interface {v0, v1}, Lcom/helpshift/CoreApi;->login(Lcom/helpshift/HelpshiftUser;)Z

    :cond_a
    :goto_1
    return-void
.end method
