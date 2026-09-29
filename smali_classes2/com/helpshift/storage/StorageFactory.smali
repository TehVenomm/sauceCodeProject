.class public Lcom/helpshift/storage/StorageFactory;
.super Ljava/lang/Object;
.source "StorageFactory.java"


# static fields
.field private static storageFactoryInstance:Lcom/helpshift/storage/StorageFactory;


# instance fields
.field public final keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;


# direct methods
.method constructor <init>()V
    .locals 3

    .line 21
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 22
    new-instance v0, Lcom/helpshift/storage/RetryKeyValueDbStorage;

    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/helpshift/storage/RetryKeyValueDbStorage;-><init>(Landroid/content/Context;)V

    .line 23
    new-instance v1, Lcom/helpshift/storage/CachedKeyValueStorage;

    .line 24
    invoke-direct {p0}, Lcom/helpshift/storage/StorageFactory;->getCacheWhitelistKeys()Ljava/util/Set;

    move-result-object v2

    invoke-direct {v1, v0, v2}, Lcom/helpshift/storage/CachedKeyValueStorage;-><init>(Lcom/helpshift/storage/KeyValueStorage;Ljava/util/Set;)V

    iput-object v1, p0, Lcom/helpshift/storage/StorageFactory;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    return-void
.end method

.method private getCacheWhitelistKeys()Ljava/util/Set;
    .locals 14
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 36
    new-instance v0, Ljava/util/HashSet;

    const-string v1, "firstDeviceSyncComplete"

    const-string v2, "hs__campaigns_icon_image_retry_counts"

    const-string v3, "sdk-language"

    const-string v4, "sdk-theme"

    const-string v5, "disableHelpshiftBranding"

    const-string v6, "screenOrientation"

    const-string v7, "data_type_device"

    const-string v8, "data_type_user"

    const-string v9, "data_type_session"

    const-string v10, "data_type_switch_user"

    const-string v11, "data_type_analytics_event"

    const-string v12, "__hs_switch_current_user"

    const-string v13, "__hs_switch_prev_user"

    filled-new-array/range {v1 .. v13}, [Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    return-object v0
.end method

.method public static declared-synchronized getInstance()Lcom/helpshift/storage/StorageFactory;
    .locals 2

    const-class v0, Lcom/helpshift/storage/StorageFactory;

    monitor-enter v0

    .line 28
    :try_start_0
    sget-object v1, Lcom/helpshift/storage/StorageFactory;->storageFactoryInstance:Lcom/helpshift/storage/StorageFactory;

    if-nez v1, :cond_0

    .line 29
    new-instance v1, Lcom/helpshift/storage/StorageFactory;

    invoke-direct {v1}, Lcom/helpshift/storage/StorageFactory;-><init>()V

    sput-object v1, Lcom/helpshift/storage/StorageFactory;->storageFactoryInstance:Lcom/helpshift/storage/StorageFactory;

    .line 31
    :cond_0
    sget-object v1, Lcom/helpshift/storage/StorageFactory;->storageFactoryInstance:Lcom/helpshift/storage/StorageFactory;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    .line 27
    monitor-exit v0

    throw v1
.end method
