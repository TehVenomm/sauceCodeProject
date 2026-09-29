.class public Lcom/helpshift/controllers/SyncController;
.super Ljava/lang/Object;
.source "SyncController.java"

# interfaces
.implements Lcom/helpshift/app/LifecycleListener;
.implements Lcom/helpshift/controllers/DataSyncCompletionListener;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/controllers/SyncController$DataTypes;
    }
.end annotation


# static fields
.field static final COUNT:Ljava/lang/String; = "count"

.field static final FULL_SYNC_TIME:Ljava/lang/String; = "full_sync_time"

.field static final FULL_SYNC_TIME_THRESHOLD:J = 0x5265c00L

.field private static final MAX_RETRY_ATTEMPTS:I = 0xa

.field private static final REALTIME_SYNC_BATCHER_DELAY_MS:J = 0xea60L

.field static final SYNC_TIME:Ljava/lang/String; = "sync_time"

.field private static final TAG:Ljava/lang/String; = "Helpshift_SyncControl"


# instance fields
.field private batcherJob:Ljava/lang/Runnable;

.field dataTypesWithChangedData:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private handler:Landroid/os/Handler;

.field private isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private final keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

.field private retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

.field private final syncListeners:Ljava/util/concurrent/LinkedBlockingQueue;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/LinkedBlockingQueue<",
            "Lcom/helpshift/listeners/SyncListener;",
            ">;"
        }
    .end annotation
.end field

.field private final syncSpecificationMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/helpshift/specifications/SyncSpecification;",
            ">;"
        }
    .end annotation
.end field

.field private final timeUtil:Lcom/helpshift/util/TimeUtil;


# direct methods
.method public varargs constructor <init>(Lcom/helpshift/storage/KeyValueStorage;Lcom/helpshift/util/TimeUtil;[Lcom/helpshift/specifications/SyncSpecification;)V
    .locals 3

    .line 58
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 42
    new-instance v0, Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-direct {v0}, Ljava/util/concurrent/LinkedBlockingQueue;-><init>()V

    iput-object v0, p0, Lcom/helpshift/controllers/SyncController;->syncListeners:Ljava/util/concurrent/LinkedBlockingQueue;

    .line 43
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/controllers/SyncController;->syncSpecificationMap:Ljava/util/Map;

    .line 45
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    iput-object v0, p0, Lcom/helpshift/controllers/SyncController;->isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 59
    iput-object p1, p0, Lcom/helpshift/controllers/SyncController;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    .line 60
    iput-object p2, p0, Lcom/helpshift/controllers/SyncController;->timeUtil:Lcom/helpshift/util/TimeUtil;

    .line 61
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCampaignAppLifeCycleListener()Lcom/helpshift/app/CampaignAppLifeCycleListener;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 63
    invoke-virtual {p1, p0}, Lcom/helpshift/app/CampaignAppLifeCycleListener;->addLifecycleListener(Lcom/helpshift/app/LifecycleListener;)V

    .line 65
    :cond_0
    array-length p1, p3

    :goto_0
    if-ge v1, p1, :cond_1

    aget-object p2, p3, v1

    .line 66
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->syncSpecificationMap:Ljava/util/Map;

    invoke-interface {p2}, Lcom/helpshift/specifications/SyncSpecification;->getDataType()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v2, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method

.method private addDataTypeWithChangedData(Ljava/lang/String;)V
    .locals 1

    .line 370
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    if-nez v0, :cond_0

    .line 371
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    .line 373
    :cond_0
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method private cancelBatcherJob()V
    .locals 2

    .line 409
    invoke-virtual {p0}, Lcom/helpshift/controllers/SyncController;->cleanUpBatcherJob()V

    .line 410
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->handler:Landroid/os/Handler;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->batcherJob:Ljava/lang/Runnable;

    if-eqz v0, :cond_0

    .line 411
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->handler:Landroid/os/Handler;

    invoke-direct {p0}, Lcom/helpshift/controllers/SyncController;->getBatcherJob()Ljava/lang/Runnable;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    :cond_0
    return-void
.end method

.method private dispatchSync(Ljava/lang/String;Z)V
    .locals 3

    const-string v0, "Helpshift_SyncControl"

    .line 139
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Dispatching sync for type :"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", isFullSync : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 140
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncListener(Ljava/lang/String;)Lcom/helpshift/listeners/SyncListener;

    move-result-object p1

    if-eqz p1, :cond_1

    if-eqz p2, :cond_0

    .line 143
    invoke-virtual {p1}, Lcom/helpshift/listeners/SyncListener;->fullSync()V

    goto :goto_0

    .line 146
    :cond_0
    invoke-virtual {p1}, Lcom/helpshift/listeners/SyncListener;->sync()V

    :cond_1
    :goto_0
    return-void
.end method

.method private getBatcherJob()Ljava/lang/Runnable;
    .locals 1

    .line 71
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->batcherJob:Ljava/lang/Runnable;

    if-nez v0, :cond_0

    .line 72
    new-instance v0, Lcom/helpshift/controllers/SyncController$1;

    invoke-direct {v0, p0}, Lcom/helpshift/controllers/SyncController$1;-><init>(Lcom/helpshift/controllers/SyncController;)V

    iput-object v0, p0, Lcom/helpshift/controllers/SyncController;->batcherJob:Ljava/lang/Runnable;

    .line 82
    :cond_0
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->batcherJob:Ljava/lang/Runnable;

    return-object v0
.end method

.method private getDataChangeCount(Ljava/lang/String;)I
    .locals 1

    .line 278
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncInformation(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object p1

    const-string v0, "count"

    invoke-virtual {p1, v0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    return p1
.end method

.method private getElapsedTimeSinceFullSync(Ljava/lang/String;)J
    .locals 4

    .line 299
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->timeUtil:Lcom/helpshift/util/TimeUtil;

    invoke-virtual {v0}, Lcom/helpshift/util/TimeUtil;->elapsedTimeMillis()J

    move-result-wide v0

    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncInformation(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object p1

    const-string v2, "full_sync_time"

    invoke-virtual {p1, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/Long;->valueOf(Ljava/lang/String;)Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    sub-long/2addr v0, v2

    return-wide v0
.end method

.method private getElapsedTimeSinceLastSync(Ljava/lang/String;)J
    .locals 4

    .line 288
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->timeUtil:Lcom/helpshift/util/TimeUtil;

    invoke-virtual {v0}, Lcom/helpshift/util/TimeUtil;->elapsedTimeMillis()J

    move-result-wide v0

    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncInformation(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object p1

    const-string v2, "sync_time"

    invoke-virtual {p1, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/Long;->valueOf(Ljava/lang/String;)Ljava/lang/Long;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    sub-long/2addr v0, v2

    return-wide v0
.end method

.method private getSyncInformation(Ljava/lang/String;)Ljava/util/HashMap;
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 309
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v0, p1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/HashMap;

    const/4 v1, 0x0

    const/4 v2, 0x1

    const-wide/16 v3, 0x0

    if-nez v0, :cond_0

    .line 313
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v5, "count"

    .line 314
    invoke-static {v1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v5, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "sync_time"

    .line 315
    invoke-static {v3, v4}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v1, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "full_sync_time"

    .line 316
    invoke-static {v3, v4}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v1, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :goto_0
    const/4 v1, 0x1

    goto :goto_1

    .line 319
    :cond_0
    iget-object v5, p0, Lcom/helpshift/controllers/SyncController;->timeUtil:Lcom/helpshift/util/TimeUtil;

    invoke-virtual {v5}, Lcom/helpshift/util/TimeUtil;->elapsedTimeMillis()J

    move-result-wide v5

    const-string v7, "sync_time"

    invoke-virtual {v0, v7}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    invoke-static {v7}, Ljava/lang/Long;->valueOf(Ljava/lang/String;)Ljava/lang/Long;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/Long;->longValue()J

    move-result-wide v7

    cmp-long v9, v5, v7

    if-gez v9, :cond_1

    const-string v1, "sync_time"

    .line 321
    invoke-static {v3, v4}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v1, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "full_sync_time"

    .line 322
    invoke-static {v3, v4}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v1, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_1
    :goto_1
    const-string v5, "full_sync_time"

    .line 327
    invoke-virtual {v0, v5}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v5

    if-nez v5, :cond_2

    const-string v1, "full_sync_time"

    .line 328
    invoke-static {v3, v4}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const/4 v1, 0x1

    :cond_2
    if-eqz v1, :cond_3

    .line 333
    iget-object v1, p0, Lcom/helpshift/controllers/SyncController;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v1, p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    :cond_3
    return-object v0
.end method

.method private getSyncListener(Ljava/lang/String;)Lcom/helpshift/listeners/SyncListener;
    .locals 3

    .line 153
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->syncListeners:Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-virtual {v0}, Ljava/util/concurrent/LinkedBlockingQueue;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/listeners/SyncListener;

    .line 154
    invoke-virtual {v1}, Lcom/helpshift/listeners/SyncListener;->getDataType()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    goto :goto_0

    :cond_1
    const/4 v1, 0x0

    :goto_0
    return-object v1
.end method

.method private isDataTypeAllowedForImmediateSync(Ljava/lang/String;)Z
    .locals 1

    const-string v0, "data_type_user"

    .line 416
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_2

    const-string v0, "data_type_analytics_event"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_2

    const-string v0, "data_type_device"

    .line 417
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 418
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->sdkInfoModel:Lcom/helpshift/model/SdkInfoModel;

    invoke-virtual {v0}, Lcom/helpshift/model/SdkInfoModel;->getDevicePropertiesSyncImmediately()Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-nez v0, :cond_2

    :cond_0
    const-string v0, "data_type_switch_user"

    .line 419
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    goto :goto_0

    :cond_1
    const/4 p1, 0x0

    goto :goto_1

    :cond_2
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method

.method private onDataChanged(Ljava/lang/String;)V
    .locals 2

    const-wide/32 v0, 0xea60

    .line 174
    invoke-virtual {p0, p1, v0, v1}, Lcom/helpshift/controllers/SyncController;->scheduleSync(Ljava/lang/String;J)V

    return-void
.end method

.method private retryForDependentDataTypes(Lcom/helpshift/listeners/SyncListener;)V
    .locals 3

    if-eqz p1, :cond_0

    .line 356
    invoke-virtual {p1}, Lcom/helpshift/listeners/SyncListener;->getDependentChildDataTypes()Ljava/util/Set;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 358
    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    const/4 v1, 0x1

    .line 359
    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object v0, v1, v2

    invoke-virtual {p0, v2, v1}, Lcom/helpshift/controllers/SyncController;->triggerSync(Z[Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method private startBatcherThread()V
    .locals 2

    .line 394
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->handler:Landroid/os/Handler;

    if-nez v0, :cond_0

    .line 395
    new-instance v0, Landroid/os/HandlerThread;

    const-string v1, "HS-cm-agg-sync"

    invoke-direct {v0, v1}, Landroid/os/HandlerThread;-><init>(Ljava/lang/String;)V

    .line 396
    invoke-virtual {v0}, Landroid/os/HandlerThread;->start()V

    .line 397
    new-instance v1, Landroid/os/Handler;

    invoke-virtual {v0}, Landroid/os/HandlerThread;->getLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-direct {v1, v0}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v1, p0, Lcom/helpshift/controllers/SyncController;->handler:Landroid/os/Handler;

    :cond_0
    return-void
.end method


# virtual methods
.method public addSpecification(Lcom/helpshift/specifications/SyncSpecification;)V
    .locals 2

    .line 86
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->syncSpecificationMap:Ljava/util/Map;

    invoke-interface {p1}, Lcom/helpshift/specifications/SyncSpecification;->getDataType()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public varargs addSyncListeners([Lcom/helpshift/listeners/SyncListener;)V
    .locals 5

    .line 100
    array-length v0, p1

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_1

    aget-object v2, p1, v1

    .line 101
    iget-object v3, p0, Lcom/helpshift/controllers/SyncController;->syncSpecificationMap:Ljava/util/Map;

    invoke-virtual {v2}, Lcom/helpshift/listeners/SyncListener;->getDataType()Ljava/lang/String;

    move-result-object v4

    invoke-interface {v3, v4}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 102
    iget-object v3, p0, Lcom/helpshift/controllers/SyncController;->syncListeners:Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-virtual {v3, v2}, Ljava/util/concurrent/LinkedBlockingQueue;->add(Ljava/lang/Object;)Z

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method

.method cleanUpBatcherJob()V
    .locals 3

    .line 402
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x1

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    .line 403
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    if-eqz v0, :cond_0

    .line 404
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->clear()V

    :cond_0
    return-void
.end method

.method public dataSyncFailed(Ljava/lang/String;Lcom/helpshift/network/errors/NetworkError;)V
    .locals 4

    const-string v0, "Helpshift_SyncControl"

    .line 242
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Data sync failed : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", Error : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Lcom/helpshift/network/errors/NetworkError;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;)V

    .line 243
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->syncSpecificationMap:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/specifications/SyncSpecification;

    if-eqz v0, :cond_3

    const/4 v1, -0x1

    .line 245
    invoke-virtual {p1}, Ljava/lang/String;->hashCode()I

    move-result v2

    const v3, 0x3ef6b066

    if-eq v2, v3, :cond_1

    const v3, 0x470fdf11

    if-eq v2, v3, :cond_0

    goto :goto_0

    :cond_0
    const-string v2, "data_type_analytics_event"

    invoke-virtual {p1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    const/4 v1, 0x1

    goto :goto_0

    :cond_1
    const-string v2, "data_type_switch_user"

    invoke-virtual {p1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    const/4 v1, 0x0

    :cond_2
    :goto_0
    packed-switch v1, :pswitch_data_0

    goto :goto_1

    .line 248
    :pswitch_0
    instance-of v1, v0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;

    if-eqz v1, :cond_3

    .line 249
    check-cast v0, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;

    invoke-virtual {v0}, Lcom/helpshift/specifications/DecayingIntervalSyncSpecification;->decayElapsedTimeThreshold()V

    .line 254
    :cond_3
    :goto_1
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    if-nez v0, :cond_4

    .line 255
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff$Builder;

    invoke-direct {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;-><init>()V

    const-wide/16 v1, 0x5

    sget-object v3, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    .line 256
    invoke-static {v1, v2, v3}, Lcom/helpshift/common/poller/Delay;->of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setBaseInterval(Lcom/helpshift/common/poller/Delay;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    const/16 v1, 0xa

    .line 257
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setMaxAttempts(I)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    sget-object v1, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;->FAILURE:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    .line 258
    invoke-virtual {v0, v1}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->setRetryPolicy(Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;)Lcom/helpshift/common/poller/HttpBackoff$Builder;

    move-result-object v0

    .line 259
    invoke-virtual {v0}, Lcom/helpshift/common/poller/HttpBackoff$Builder;->build()Lcom/helpshift/common/poller/HttpBackoff;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/controllers/SyncController;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    .line 262
    :cond_4
    invoke-virtual {p2}, Lcom/helpshift/network/errors/NetworkError;->getReason()Ljava/lang/Integer;

    move-result-object p2

    const-wide/16 v0, -0x64

    if-eqz p2, :cond_5

    .line 264
    iget-object v2, p0, Lcom/helpshift/controllers/SyncController;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    invoke-virtual {p2}, Ljava/lang/Integer;->intValue()I

    move-result p2

    invoke-virtual {v2, p2}, Lcom/helpshift/common/poller/HttpBackoff;->nextIntervalMillis(I)J

    move-result-wide v2

    goto :goto_2

    :cond_5
    move-wide v2, v0

    :goto_2
    cmp-long p2, v2, v0

    if-eqz p2, :cond_6

    .line 267
    invoke-virtual {p0, p1, v2, v3}, Lcom/helpshift/controllers/SyncController;->scheduleSync(Ljava/lang/String;J)V

    :cond_6
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method public dataSynced(Ljava/lang/String;Z)V
    .locals 4

    const-string v0, "Helpshift_SyncControl"

    .line 221
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Data sync complete : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", Full sync : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 222
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->timeUtil:Lcom/helpshift/util/TimeUtil;

    invoke-virtual {v0}, Lcom/helpshift/util/TimeUtil;->elapsedTimeMillis()J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v0

    .line 223
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncInformation(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object v1

    const-string v2, "count"

    const/4 v3, 0x0

    .line 224
    invoke-static {v3}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "sync_time"

    .line 225
    invoke-virtual {v1, v2, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz p2, :cond_0

    const-string p2, "full_sync_time"

    .line 227
    invoke-virtual {v1, p2, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 229
    :cond_0
    iget-object p2, p0, Lcom/helpshift/controllers/SyncController;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {p2, p1, v1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    .line 230
    iget-object p1, p0, Lcom/helpshift/controllers/SyncController;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    if-eqz p1, :cond_1

    .line 231
    iget-object p1, p0, Lcom/helpshift/controllers/SyncController;->retryBackoff:Lcom/helpshift/common/poller/HttpBackoff;

    invoke-virtual {p1}, Lcom/helpshift/common/poller/HttpBackoff;->reset()V

    :cond_1
    return-void
.end method

.method public firstDeviceSyncComplete()V
    .locals 1

    const-string v0, "data_type_device"

    .line 351
    invoke-direct {p0, v0}, Lcom/helpshift/controllers/SyncController;->getSyncListener(Ljava/lang/String;)Lcom/helpshift/listeners/SyncListener;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/controllers/SyncController;->retryForDependentDataTypes(Lcom/helpshift/listeners/SyncListener;)V

    return-void
.end method

.method public getDataTypesWithChangedData()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 366
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->dataTypesWithChangedData:Ljava/util/Set;

    return-object v0
.end method

.method public incrementDataChangeCount(Ljava/lang/String;I)V
    .locals 3

    if-gtz p2, :cond_0

    return-void

    .line 188
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncInformation(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "count"

    .line 189
    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    const-string v2, "count"

    add-int/2addr v1, p2

    .line 190
    invoke-static {v1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, v2, p2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 191
    iget-object p2, p0, Lcom/helpshift/controllers/SyncController;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {p2, p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    .line 192
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->onDataChanged(Ljava/lang/String;)V

    return-void
.end method

.method public isFullSyncSatisfied(Ljava/lang/String;)Z
    .locals 4

    .line 339
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncListener(Ljava/lang/String;)Lcom/helpshift/listeners/SyncListener;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 340
    invoke-virtual {v0}, Lcom/helpshift/listeners/SyncListener;->isFullSyncEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 341
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getElapsedTimeSinceFullSync(Ljava/lang/String;)J

    move-result-wide v0

    const-wide/32 v2, 0x5265c00

    cmp-long p1, v0, v2

    if-lez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public onBackground()V
    .locals 5

    .line 388
    invoke-direct {p0}, Lcom/helpshift/controllers/SyncController;->cancelBatcherJob()V

    const-string v0, "data_type_switch_user"

    const-string v1, "data_type_device"

    const-string v2, "data_type_user"

    const-string v3, "data_type_session"

    const-string v4, "data_type_analytics_event"

    .line 389
    filled-new-array {v0, v1, v2, v3, v4}, [Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {p0, v1, v0}, Lcom/helpshift/controllers/SyncController;->triggerSync(Z[Ljava/lang/String;)V

    return-void
.end method

.method public onForeground()V
    .locals 3

    const-string v0, "data_type_switch_user"

    const-string v1, "data_type_user"

    const-string v2, "data_type_analytics_event"

    .line 378
    filled-new-array {v0, v1, v2}, [Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {p0, v1, v0}, Lcom/helpshift/controllers/SyncController;->triggerSync(Z[Ljava/lang/String;)V

    return-void
.end method

.method public scheduleSync(Ljava/lang/String;J)V
    .locals 3

    const-string v0, "Helpshift_SyncControl"

    .line 163
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Scheduling sync : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", Delay : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2, p3}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 164
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->isDataTypeAllowedForImmediateSync(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 165
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->isBatcherScheduled:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 166
    invoke-direct {p0}, Lcom/helpshift/controllers/SyncController;->startBatcherThread()V

    .line 167
    iget-object v0, p0, Lcom/helpshift/controllers/SyncController;->handler:Landroid/os/Handler;

    invoke-direct {p0}, Lcom/helpshift/controllers/SyncController;->getBatcherJob()Ljava/lang/Runnable;

    move-result-object v1

    invoke-virtual {v0, v1, p2, p3}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    .line 169
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->addDataTypeWithChangedData(Ljava/lang/String;)V

    :cond_1
    return-void
.end method

.method public setDataChangeCount(Ljava/lang/String;I)V
    .locals 4

    .line 203
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncInformation(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object v0

    const-string v1, "count"

    .line 204
    invoke-virtual {v0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Integer;->intValue()I

    move-result v1

    const-string v2, "count"

    .line 205
    invoke-static {p2}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 206
    iget-object v2, p0, Lcom/helpshift/controllers/SyncController;->keyValueStorage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {v2, p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    if-eq v1, p2, :cond_0

    if-lez p2, :cond_0

    .line 208
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->onDataChanged(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public switchUserComplete(Ljava/lang/String;)V
    .locals 0

    const-string p1, "data_type_switch_user"

    .line 346
    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->getSyncListener(Ljava/lang/String;)Lcom/helpshift/listeners/SyncListener;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/helpshift/controllers/SyncController;->retryForDependentDataTypes(Lcom/helpshift/listeners/SyncListener;)V

    return-void
.end method

.method varargs triggerSync(Z[Ljava/lang/String;)V
    .locals 8

    .line 114
    array-length v0, p2

    const/4 v1, 0x0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_3

    aget-object v3, p2, v2

    const-string v4, "Helpshift_SyncControl"

    .line 115
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Triggering sync for  type : "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-static {v4, v5}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 116
    invoke-virtual {p0, v3}, Lcom/helpshift/controllers/SyncController;->isFullSyncSatisfied(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_0

    const/4 v4, 0x1

    .line 117
    invoke-direct {p0, v3, v4}, Lcom/helpshift/controllers/SyncController;->dispatchSync(Ljava/lang/String;Z)V

    goto :goto_1

    :cond_0
    if-eqz p1, :cond_1

    .line 120
    iget-object v4, p0, Lcom/helpshift/controllers/SyncController;->syncSpecificationMap:Ljava/util/Map;

    invoke-interface {v4, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/specifications/SyncSpecification;

    if-eqz v4, :cond_2

    .line 122
    invoke-direct {p0, v3}, Lcom/helpshift/controllers/SyncController;->getDataChangeCount(Ljava/lang/String;)I

    move-result v5

    invoke-direct {p0, v3}, Lcom/helpshift/controllers/SyncController;->getElapsedTimeSinceLastSync(Ljava/lang/String;)J

    move-result-wide v6

    invoke-interface {v4, v5, v6, v7}, Lcom/helpshift/specifications/SyncSpecification;->isSatisfied(IJ)Z

    move-result v4

    if-eqz v4, :cond_2

    .line 123
    invoke-direct {p0, v3, v1}, Lcom/helpshift/controllers/SyncController;->dispatchSync(Ljava/lang/String;Z)V

    goto :goto_1

    .line 127
    :cond_1
    invoke-direct {p0, v3, v1}, Lcom/helpshift/controllers/SyncController;->dispatchSync(Ljava/lang/String;Z)V

    :cond_2
    :goto_1
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_3
    return-void
.end method
