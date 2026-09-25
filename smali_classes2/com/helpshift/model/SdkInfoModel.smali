.class public Lcom/helpshift/model/SdkInfoModel;
.super Ljava/lang/Object;
.source "SdkInfoModel.java"


# static fields
.field private static final CHANGE_SET_ID_PREFIX:Ljava/lang/String; = "hs__change_set_id:"

.field private static final KEY_DEVICE_ID:Ljava/lang/String; = "hs-device-id"

.field private static final KEY_SYNCED_USER_ID:Ljava/lang/String; = "hs-synced-user-id"

.field public static final SDK_LANGUAGE:Ljava/lang/String; = "sdk-language"

.field public static final SDK_THEME:Ljava/lang/String; = "sdk-theme"


# instance fields
.field private backupStorage:Lcom/helpshift/common/dao/BackupDAO;

.field private etags:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private storage:Lcom/helpshift/storage/KeyValueStorage;


# direct methods
.method protected constructor <init>(Lcom/helpshift/storage/KeyValueStorage;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 30
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 31
    iput-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    .line 32
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getBackupDAO()Lcom/helpshift/common/dao/BackupDAO;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->backupStorage:Lcom/helpshift/common/dao/BackupDAO;

    .line 33
    iget-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string p2, "etags"

    invoke-interface {p1, p2}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/HashMap;

    iput-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    .line 34
    iget-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    if-nez p1, :cond_0

    .line 35
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    iput-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    .line 37
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/model/SdkInfoModel;->updateBackupStorageWithInternalStorage()V

    return-void
.end method

.method private updateBackupStorageWithInternalStorage()V
    .locals 3

    .line 233
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-device-id"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-eqz v0, :cond_0

    .line 235
    iget-object v1, p0, Lcom/helpshift/model/SdkInfoModel;->backupStorage:Lcom/helpshift/common/dao/BackupDAO;

    const-string v2, "hs-device-id"

    invoke-interface {v1, v2, v0}, Lcom/helpshift/common/dao/BackupDAO;->storeValue(Ljava/lang/String;Ljava/io/Serializable;)V

    .line 238
    :cond_0
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-synced-user-id"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-eqz v0, :cond_1

    .line 240
    iget-object v1, p0, Lcom/helpshift/model/SdkInfoModel;->backupStorage:Lcom/helpshift/common/dao/BackupDAO;

    const-string v2, "hs-synced-user-id"

    invoke-interface {v1, v2, v0}, Lcom/helpshift/common/dao/BackupDAO;->storeValue(Ljava/lang/String;Ljava/io/Serializable;)V

    :cond_1
    return-void
.end method


# virtual methods
.method public addDeviceId(Ljava/lang/String;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 126
    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    .line 129
    :cond_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 130
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-device-id"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    .line 131
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->backupStorage:Lcom/helpshift/common/dao/BackupDAO;

    const-string v1, "hs-device-id"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/common/dao/BackupDAO;->storeValue(Ljava/lang/String;Ljava/io/Serializable;)V

    return-void

    .line 134
    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    invoke-direct {p1}, Ljava/lang/IllegalArgumentException;-><init>()V

    throw p1
.end method

.method public addEtag(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 47
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    invoke-virtual {v0, p2, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 48
    iget-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string p2, "etags"

    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    invoke-interface {p1, p2, v0}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public clearEtag(Ljava/lang/String;)V
    .locals 2

    .line 67
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 68
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 69
    iget-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "etags"

    iget-object v1, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    :cond_0
    return-void
.end method

.method public getChangeSetId(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 201
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "hs__change_set_id:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 202
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v0, p1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    return-object p1
.end method

.method public getCurrentLoggedInId()Ljava/lang/String;
    .locals 2

    .line 97
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "current-logged-in-id"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    return-object v0
.end method

.method public getDeviceId()Ljava/lang/String;
    .locals 2

    .line 144
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-device-id"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-nez v0, :cond_0

    .line 146
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->backupStorage:Lcom/helpshift/common/dao/BackupDAO;

    const-string v1, "hs-device-id"

    invoke-interface {v0, v1}, Lcom/helpshift/common/dao/BackupDAO;->getValue(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    :cond_0
    return-object v0
.end method

.method public getDevicePropertiesSyncImmediately()Ljava/lang/Boolean;
    .locals 2

    .line 168
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-device-properties-sync-immediately"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    .line 170
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method public getEtag(Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 58
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->etags:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    return-object p1
.end method

.method public getFirstLaunch()Ljava/lang/Boolean;
    .locals 2

    .line 152
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-first-launch"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    return-object v0
.end method

.method public getOneCampaignFetchSuccessful()Ljava/lang/Boolean;
    .locals 2

    .line 160
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-one-campaign-fetch-successful"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    return-object v0
.end method

.method public getSdkLanguage()Ljava/lang/String;
    .locals 2

    .line 180
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "sdk-language"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    return-object v0
.end method

.method public getServerTimeDelta()Ljava/lang/Float;
    .locals 2

    .line 79
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "server-time-delta"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Float;

    return-object v0
.end method

.method public getTheme()Ljava/lang/Integer;
    .locals 2

    .line 188
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "sdk-theme"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Integer;

    return-object v0
.end method

.method public getUserIdSyncedWithBackend()Ljava/lang/String;
    .locals 2

    .line 211
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-synced-user-id"

    invoke-interface {v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-nez v0, :cond_0

    .line 213
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->backupStorage:Lcom/helpshift/common/dao/BackupDAO;

    const-string v1, "hs-synced-user-id"

    invoke-interface {v0, v1}, Lcom/helpshift/common/dao/BackupDAO;->getValue(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    :cond_0
    return-object v0
.end method

.method public isDuplicateNotification(Ljava/lang/String;Ljava/lang/String;)Z
    .locals 4

    const-string v0, "hs__received_push_campaigns"

    .line 259
    iget-object v1, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    .line 260
    invoke-interface {v1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/HashMap;

    if-nez v1, :cond_0

    .line 262
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 265
    :cond_0
    invoke-virtual {v1, p2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/HashSet;

    if-nez v2, :cond_1

    .line 268
    new-instance v2, Ljava/util/HashSet;

    invoke-direct {v2}, Ljava/util/HashSet;-><init>()V

    .line 271
    :cond_1
    invoke-virtual {v2, p1}, Ljava/util/HashSet;->contains(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    const/4 p1, 0x1

    return p1

    .line 275
    :cond_2
    invoke-virtual {v2, p1}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    .line 276
    invoke-virtual {v1, p2, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 277
    iget-object p1, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    const/4 p1, 0x0

    return p1
.end method

.method public setChangeSetId(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 196
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "hs__change_set_id:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    .line 197
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v0, p2, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setCurrentLoggedInId(Ljava/lang/String;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 107
    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    .line 109
    :cond_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 110
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "current-logged-in-id"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void

    .line 113
    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    invoke-direct {p1}, Ljava/lang/IllegalArgumentException;-><init>()V

    throw p1
.end method

.method public setDevicePropertiesSyncImmediately(Ljava/lang/Boolean;)V
    .locals 2

    .line 176
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-device-properties-sync-immediately"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setFirstLaunch(Ljava/lang/Boolean;)V
    .locals 2

    .line 156
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-first-launch"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setOneCampaignFetchSuccessful(Ljava/lang/Boolean;)V
    .locals 2

    .line 164
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-one-campaign-fetch-successful"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setSdkLanguage(Ljava/lang/String;)V
    .locals 2

    .line 184
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "sdk-language"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setServerTimeDelta(Ljava/lang/Float;)V
    .locals 2

    .line 88
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "server-time-delta"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setTheme(I)V
    .locals 2

    .line 192
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "sdk-theme"

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setUserIdSyncedWithBackend(Ljava/lang/String;)V
    .locals 2

    .line 225
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "hs-synced-user-id"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    .line 226
    iget-object v0, p0, Lcom/helpshift/model/SdkInfoModel;->backupStorage:Lcom/helpshift/common/dao/BackupDAO;

    const-string v1, "hs-synced-user-id"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/common/dao/BackupDAO;->storeValue(Ljava/lang/String;Ljava/io/Serializable;)V

    return-void
.end method
