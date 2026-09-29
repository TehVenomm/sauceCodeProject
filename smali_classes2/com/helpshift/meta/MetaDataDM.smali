.class public Lcom/helpshift/meta/MetaDataDM;
.super Ljava/lang/Object;
.source "MetaDataDM.java"


# instance fields
.field private customMetaDataCallable:Lcom/helpshift/meta/RootMetaDataCallable;

.field private debugLogDTOs:Ljava/util/LinkedList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/LinkedList<",
            "Lcom/helpshift/meta/dto/DebugLogDTO;",
            ">;"
        }
    .end annotation
.end field

.field private final device:Lcom/helpshift/common/platform/Device;

.field private domain:Lcom/helpshift/common/domain/Domain;

.field private final jsonifier:Lcom/helpshift/common/platform/Jsonifier;

.field private metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

.field private final sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;)V
    .locals 1

    .line 45
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 43
    new-instance v0, Ljava/util/LinkedList;

    invoke-direct {v0}, Ljava/util/LinkedList;-><init>()V

    iput-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->debugLogDTOs:Ljava/util/LinkedList;

    .line 46
    iput-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 47
    iput-object p3, p0, Lcom/helpshift/meta/MetaDataDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    .line 48
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getMetaDataDAO()Lcom/helpshift/meta/dao/MetaDataDAO;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    .line 49
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getJsonifier()Lcom/helpshift/common/platform/Jsonifier;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    .line 50
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    return-void
.end method

.method private cleanMetaTags(Ljava/util/Map;Ljava/lang/String;)Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    .line 283
    new-array v0, v0, [Ljava/lang/String;

    .line 285
    invoke-interface {p1, p2}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    .line 286
    instance-of v2, v1, [Ljava/lang/String;

    if-eqz v2, :cond_0

    .line 287
    check-cast v1, [Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/meta/MetaDataDM;->cleanTags([Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v0

    .line 290
    :cond_0
    array-length v1, v0

    if-lez v1, :cond_1

    .line 291
    invoke-interface {p1, p2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    return-object p1
.end method

.method private static cleanTags([Ljava/lang/String;)[Ljava/lang/String;
    .locals 6

    .line 54
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    const/4 v1, 0x0

    if-eqz p0, :cond_1

    .line 56
    array-length v2, p0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_1

    aget-object v4, p0, v3

    .line 57
    invoke-static {v4}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v5

    if-nez v5, :cond_0

    invoke-virtual {v4}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v5

    if-lez v5, :cond_0

    .line 58
    invoke-virtual {v4}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v4

    invoke-interface {v0, v4}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 62
    :cond_1
    new-array p0, v1, [Ljava/lang/String;

    invoke-interface {v0, p0}, Ljava/util/Set;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p0

    check-cast p0, [Ljava/lang/String;

    return-object p0
.end method

.method private getBreadCrumbs()Ljava/lang/Object;
    .locals 2

    .line 91
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    invoke-interface {v1}, Lcom/helpshift/meta/dao/MetaDataDAO;->getBreadCrumbs()Ljava/util/ArrayList;

    move-result-object v1

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyBreadCrumbDTOList(Ljava/util/List;)Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method private getCustomMetaDataFromCallable()Ljava/util/Map;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation

    .line 259
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->customMetaDataCallable:Lcom/helpshift/meta/RootMetaDataCallable;

    if-eqz v0, :cond_0

    .line 260
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->customMetaDataCallable:Lcom/helpshift/meta/RootMetaDataCallable;

    invoke-interface {v0}, Lcom/helpshift/meta/RootMetaDataCallable;->call()Ljava/util/Map;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 262
    invoke-direct {p0, v0}, Lcom/helpshift/meta/MetaDataDM;->removeEmptyKeyOrValue(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v0

    const-string v1, "hs-tags"

    .line 263
    invoke-direct {p0, v0, v1}, Lcom/helpshift/meta/MetaDataDM;->cleanMetaTags(Ljava/util/Map;Ljava/lang/String;)Ljava/util/Map;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :cond_1
    :goto_0
    return-object v0
.end method

.method private declared-synchronized getDebugLogs()Ljava/lang/Object;
    .locals 5

    monitor-enter p0

    .line 186
    :try_start_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 187
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->debugLogDTOs:Ljava/util/LinkedList;

    invoke-virtual {v1}, Ljava/util/LinkedList;->size()I

    move-result v1

    .line 188
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v3, "debugLogLimit"

    invoke-virtual {v2, v3}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getInt(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Integer;->intValue()I

    move-result v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v1, :cond_0

    if-ge v3, v2, :cond_0

    .line 191
    :try_start_1
    iget-object v4, p0, Lcom/helpshift/meta/MetaDataDM;->debugLogDTOs:Ljava/util/LinkedList;

    invoke-virtual {v4}, Ljava/util/LinkedList;->removeFirst()Ljava/lang/Object;

    move-result-object v4

    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_1
    .catch Ljava/util/NoSuchElementException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :catch_0
    move-exception v0

    .line 194
    :try_start_2
    invoke-static {v0}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object v0

    throw v0

    .line 197
    :cond_0
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->debugLogDTOs:Ljava/util/LinkedList;

    invoke-virtual {v1}, Ljava/util/LinkedList;->clear()V

    .line 198
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    invoke-interface {v1, v0}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyDebugLogDTOList(Ljava/util/List;)Ljava/lang/Object;

    move-result-object v0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 185
    monitor-exit p0

    throw v0
.end method

.method private getDeviceInfo()Ljava/lang/Object;
    .locals 3

    .line 105
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "platform"

    .line 106
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getPlatformName()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "library-version"

    .line 107
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getSDKVersion()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "device-model"

    .line 108
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getDeviceModel()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "os-version"

    .line 109
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getOSVersion()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 112
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "sdkLanguage"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 113
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 114
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Device;->getLanguage()Ljava/lang/String;

    move-result-object v1

    .line 116
    :cond_0
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_1

    const-string v2, "language-code"

    .line 117
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/util/MissingResourceException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :cond_1
    const-string v1, "timestamp"

    .line 123
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getTimeStamp()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "application-identifier"

    .line 126
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getAppIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 127
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Device;->getAppName()Ljava/lang/String;

    move-result-object v1

    .line 128
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_2

    const-string v1, "(unknown)"

    :cond_2
    const-string v2, "application-name"

    .line 131
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "application-version"

    .line 132
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getAppVersion()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "disk-space"

    .line 134
    invoke-direct {p0}, Lcom/helpshift/meta/MetaDataDM;->getDiskSpace()Ljava/lang/Object;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 136
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "fullPrivacy"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_3

    const-string v1, "country-code"

    .line 137
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getSimCountryIso()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "carrier-name"

    .line 138
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getCarrierName()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_3
    const-string v1, "network-type"

    .line 140
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getNetworkType()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "battery-level"

    .line 141
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getBatteryLevel()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "battery-status"

    .line 142
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getBatteryStatus()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 143
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    invoke-interface {v1, v0}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyToObject(Ljava/util/Map;)Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method private getDiskSpace()Ljava/lang/Object;
    .locals 4

    .line 95
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Device;->getDiskSpace()Lcom/helpshift/meta/dto/DeviceDiskSpaceDTO;

    move-result-object v0

    .line 96
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    if-eqz v0, :cond_0

    const-string v2, "total-space-phone"

    .line 98
    iget-object v3, v0, Lcom/helpshift/meta/dto/DeviceDiskSpaceDTO;->phoneTotalSpace:Ljava/lang/String;

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "free-space-phone"

    .line 99
    iget-object v0, v0, Lcom/helpshift/meta/dto/DeviceDiskSpaceDTO;->phoneFreeSpace:Ljava/lang/String;

    invoke-interface {v1, v2, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 101
    :cond_0
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyToObject(Ljava/util/Map;)Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method private getExtra(Ljava/lang/String;)Ljava/lang/Object;
    .locals 3

    .line 147
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "api-version"

    .line 148
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getApiVersion()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "library-version"

    .line 149
    iget-object v2, p0, Lcom/helpshift/meta/MetaDataDM;->device:Lcom/helpshift/common/platform/Device;

    invoke-interface {v2}, Lcom/helpshift/common/platform/Device;->getSDKVersion()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 150
    invoke-static {p1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    const-string v1, "user-id"

    .line 151
    invoke-interface {v0, v1, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 153
    :cond_0
    iget-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    invoke-interface {p1, v0}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyToObject(Ljava/util/Map;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method private removeEmptyKeyOrValue(Ljava/util/Map;)Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;"
        }
    .end annotation

    .line 270
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 271
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 272
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 273
    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/io/Serializable;

    .line 275
    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_1

    instance-of v3, v1, Ljava/lang/String;

    if-eqz v3, :cond_0

    check-cast v1, Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 276
    :cond_1
    invoke-virtual {v0, v2}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_2
    return-object v0
.end method

.method private saveCustomMetaData(Ljava/util/Map;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/io/Serializable;",
            ">;)V"
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 208
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 210
    :goto_0
    iget-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    invoke-interface {p1, v0}, Lcom/helpshift/meta/dao/MetaDataDAO;->saveCustomMetaData(Ljava/util/HashMap;)V

    return-void
.end method


# virtual methods
.method public declared-synchronized addDebugLog(Lcom/helpshift/meta/dto/DebugLogDTO;)V
    .locals 4

    monitor-enter p0

    const/16 v0, 0x1388

    .line 164
    :try_start_0
    iget-object v1, p1, Lcom/helpshift/meta/dto/DebugLogDTO;->msg:Ljava/lang/String;

    if-eqz v1, :cond_0

    iget-object v1, p1, Lcom/helpshift/meta/dto/DebugLogDTO;->msg:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    if-le v1, v0, :cond_0

    .line 165
    iget-object v1, p1, Lcom/helpshift/meta/dto/DebugLogDTO;->msg:Ljava/lang/String;

    const/4 v2, 0x0

    invoke-virtual {v1, v2, v0}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v0

    .line 166
    new-instance v1, Lcom/helpshift/meta/dto/DebugLogDTO;

    iget-object v2, p1, Lcom/helpshift/meta/dto/DebugLogDTO;->level:Ljava/lang/Integer;

    iget-object v3, p1, Lcom/helpshift/meta/dto/DebugLogDTO;->tag:Ljava/lang/String;

    iget-object p1, p1, Lcom/helpshift/meta/dto/DebugLogDTO;->throwable:Ljava/lang/String;

    invoke-direct {v1, v2, v3, v0, p1}, Lcom/helpshift/meta/dto/DebugLogDTO;-><init>(Ljava/lang/Integer;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    move-object p1, v1

    .line 169
    :cond_0
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->debugLogDTOs:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->size()I

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/16 v1, 0x64

    if-le v0, v1, :cond_1

    .line 171
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->debugLogDTOs:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->removeLast()Ljava/lang/Object;
    :try_end_1
    .catch Ljava/util/NoSuchElementException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 174
    :try_start_2
    invoke-static {p1}, Lcom/helpshift/common/exception/RootAPIException;->wrap(Ljava/lang/Exception;)Lcom/helpshift/common/exception/RootAPIException;

    move-result-object p1

    throw p1

    .line 177
    :cond_1
    :goto_0
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v1, "debugLogLimit"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getInt(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    if-eqz v0, :cond_2

    .line 178
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->debugLogDTOs:Ljava/util/LinkedList;

    invoke-virtual {v0, p1}, Ljava/util/LinkedList;->addFirst(Ljava/lang/Object;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 180
    :cond_2
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 162
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized clearBreadCrumbs()V
    .locals 2

    monitor-enter p0

    .line 87
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Lcom/helpshift/meta/dao/MetaDataDAO;->setBreadCrumbs(Ljava/util/ArrayList;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 88
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 86
    monitor-exit p0

    throw v0
.end method

.method public clearCustomMetaData()V
    .locals 2

    .line 235
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Lcom/helpshift/meta/dao/MetaDataDAO;->saveCustomMetaData(Ljava/util/HashMap;)V

    return-void
.end method

.method public getCustomMetaData()Ljava/lang/Object;
    .locals 3

    .line 215
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->customMetaDataCallable:Lcom/helpshift/meta/RootMetaDataCallable;

    if-eqz v0, :cond_0

    .line 216
    invoke-direct {p0}, Lcom/helpshift/meta/MetaDataDM;->getCustomMetaDataFromCallable()Ljava/util/Map;

    move-result-object v0

    .line 217
    invoke-direct {p0, v0}, Lcom/helpshift/meta/MetaDataDM;->saveCustomMetaData(Ljava/util/Map;)V

    goto :goto_0

    .line 220
    :cond_0
    iget-object v0, p0, Lcom/helpshift/meta/MetaDataDM;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    invoke-interface {v0}, Lcom/helpshift/meta/dao/MetaDataDAO;->getCustomMetaData()Ljava/util/HashMap;

    move-result-object v0

    :goto_0
    const/4 v1, 0x0

    if-eqz v0, :cond_2

    .line 226
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v2, "fullPrivacy"

    invoke-virtual {v1, v2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_1

    const-string v1, "private-data"

    .line 227
    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 229
    :cond_1
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    invoke-interface {v1, v0}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyCustomMetaMap(Ljava/util/Map;)Ljava/lang/Object;

    move-result-object v1

    :cond_2
    return-object v1
.end method

.method public getMetaInfo()Ljava/lang/Object;
    .locals 5

    .line 239
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "breadcrumbs"

    .line 240
    invoke-direct {p0}, Lcom/helpshift/meta/MetaDataDM;->getBreadCrumbs()Ljava/lang/Object;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "device_info"

    .line 241
    invoke-direct {p0}, Lcom/helpshift/meta/MetaDataDM;->getDeviceInfo()Ljava/lang/Object;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "logs"

    .line 242
    invoke-direct {p0}, Lcom/helpshift/meta/MetaDataDM;->getDebugLogs()Ljava/lang/Object;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 243
    invoke-virtual {p0}, Lcom/helpshift/meta/MetaDataDM;->getCustomMetaData()Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_0

    const-string v2, "custom_meta"

    .line 245
    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 247
    :cond_0
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getUserManagerDM()Lcom/helpshift/account/domainmodel/UserManagerDM;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getUserMetaIdentifier()Ljava/lang/String;

    move-result-object v1

    const-string v2, "extra"

    .line 248
    invoke-direct {p0, v1}, Lcom/helpshift/meta/MetaDataDM;->getExtra(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 251
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "fp_status"

    .line 252
    iget-object v3, p0, Lcom/helpshift/meta/MetaDataDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v4, "fullPrivacy"

    invoke-virtual {v3, v4}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getBoolean(Ljava/lang/String;)Z

    move-result v3

    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "user_info"

    .line 253
    iget-object v3, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    invoke-interface {v3, v1}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyToObject(Ljava/util/Map;)Ljava/lang/Object;

    move-result-object v1

    invoke-interface {v0, v2, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 254
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->jsonifier:Lcom/helpshift/common/platform/Jsonifier;

    invoke-interface {v1, v0}, Lcom/helpshift/common/platform/Jsonifier;->jsonifyToObject(Ljava/util/Map;)Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method public declared-synchronized pushBreadCrumb(Ljava/lang/String;)V
    .locals 3

    monitor-enter p0

    .line 66
    :try_start_0
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v1, "yyyy-MM-dd\'T\'HH:mm:ss.SSS\'Z\'"

    sget-object v2, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    invoke-direct {v0, v1, v2}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    new-instance v1, Ljava/util/Date;

    invoke-direct {v1}, Ljava/util/Date;-><init>()V

    .line 67
    invoke-virtual {v0, v1}, Ljava/text/SimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    .line 68
    iget-object v1, p0, Lcom/helpshift/meta/MetaDataDM;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    invoke-interface {v1}, Lcom/helpshift/meta/dao/MetaDataDAO;->getBreadCrumbs()Ljava/util/ArrayList;

    move-result-object v1

    if-nez v1, :cond_0

    .line 70
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 72
    :cond_0
    new-instance v2, Lcom/helpshift/meta/dto/BreadCrumbDTO;

    invoke-direct {v2, p1, v0}, Lcom/helpshift/meta/dto/BreadCrumbDTO;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v1, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 74
    iget-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->sdkConfigurationDM:Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    const-string v0, "breadcrumbLimit"

    invoke-virtual {p1, v0}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getInt(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    .line 75
    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v0

    if-lez p1, :cond_2

    if-le v0, p1, :cond_1

    sub-int p1, v0, p1

    .line 80
    new-instance v2, Ljava/util/ArrayList;

    invoke-virtual {v1, p1, v0}, Ljava/util/ArrayList;->subList(II)Ljava/util/List;

    move-result-object p1

    invoke-direct {v2, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    move-object v1, v2

    .line 82
    :cond_1
    iget-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->metaDataDAO:Lcom/helpshift/meta/dao/MetaDataDAO;

    invoke-interface {p1, v1}, Lcom/helpshift/meta/dao/MetaDataDAO;->setBreadCrumbs(Ljava/util/ArrayList;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 84
    :cond_2
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 65
    monitor-exit p0

    throw p1
.end method

.method public setCustomMetaDataCallable(Lcom/helpshift/meta/RootMetaDataCallable;)V
    .locals 0

    .line 202
    iput-object p1, p0, Lcom/helpshift/meta/MetaDataDM;->customMetaDataCallable:Lcom/helpshift/meta/RootMetaDataCallable;

    return-void
.end method
