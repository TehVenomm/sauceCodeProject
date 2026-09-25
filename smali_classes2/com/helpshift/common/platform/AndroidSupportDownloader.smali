.class public Lcom/helpshift/common/platform/AndroidSupportDownloader;
.super Ljava/lang/Object;
.source "AndroidSupportDownloader.java"

# interfaces
.implements Lcom/helpshift/downloader/SupportDownloader;


# static fields
.field private static final CORE_POOL_SIZE:I = 0x5

.field private static final KEEP_ALIVE_TIME:I = 0x1

.field private static final KEEP_ALIVE_TIME_UNIT:Ljava/util/concurrent/TimeUnit;

.field private static final MAXIMUM_POOL_SIZE:I = 0x5


# instance fields
.field private callbackManager:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/util/Set<",
            "Lcom/helpshift/downloader/SupportDownloadStateChangeListener;",
            ">;>;"
        }
    .end annotation
.end field

.field private context:Landroid/content/Context;

.field private final downloadManager:Lcom/helpshift/android/commons/downloader/DownloadManager;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 32
    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    sput-object v0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->KEEP_ALIVE_TIME_UNIT:Ljava/util/concurrent/TimeUnit;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Lcom/helpshift/common/platform/KVStore;)V
    .locals 9

    .line 41
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 42
    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->context:Landroid/content/Context;

    .line 43
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->callbackManager:Ljava/util/Map;

    .line 44
    new-instance v0, Lcom/helpshift/common/platform/SupportDownloaderKVStorage;

    invoke-direct {v0, p2}, Lcom/helpshift/common/platform/SupportDownloaderKVStorage;-><init>(Lcom/helpshift/common/platform/KVStore;)V

    .line 45
    new-instance v7, Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-direct {v7}, Ljava/util/concurrent/LinkedBlockingQueue;-><init>()V

    .line 46
    new-instance p2, Ljava/util/concurrent/ThreadPoolExecutor;

    sget-object v6, Lcom/helpshift/common/platform/AndroidSupportDownloader;->KEEP_ALIVE_TIME_UNIT:Ljava/util/concurrent/TimeUnit;

    new-instance v8, Lcom/helpshift/common/domain/HSThreadFactory;

    const-string v1, "sp-dwnld"

    invoke-direct {v8, v1}, Lcom/helpshift/common/domain/HSThreadFactory;-><init>(Ljava/lang/String;)V

    const/4 v2, 0x5

    const/4 v3, 0x5

    const-wide/16 v4, 0x1

    move-object v1, p2

    invoke-direct/range {v1 .. v8}, Ljava/util/concurrent/ThreadPoolExecutor;-><init>(IIJLjava/util/concurrent/TimeUnit;Ljava/util/concurrent/BlockingQueue;Ljava/util/concurrent/ThreadFactory;)V

    .line 52
    new-instance v1, Lcom/helpshift/android/commons/downloader/DownloadManager;

    invoke-direct {v1, p1, v0, p2}, Lcom/helpshift/android/commons/downloader/DownloadManager;-><init>(Landroid/content/Context;Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;Ljava/util/concurrent/ThreadPoolExecutor;)V

    iput-object v1, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->downloadManager:Lcom/helpshift/android/commons/downloader/DownloadManager;

    return-void
.end method

.method private declared-synchronized addCallback(Ljava/lang/String;Lcom/helpshift/downloader/SupportDownloadStateChangeListener;)V
    .locals 1

    monitor-enter p0

    if-nez p2, :cond_0

    .line 145
    monitor-exit p0

    return-void

    .line 147
    :cond_0
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->callbackManager:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Set;

    if-nez v0, :cond_1

    .line 149
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    .line 151
    :cond_1
    invoke-interface {v0, p2}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 152
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->callbackManager:Ljava/util/Map;

    invoke-interface {p2, p1, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 153
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 143
    monitor-exit p0

    throw p1
.end method

.method private buildDownloadConfig(Lcom/helpshift/downloader/SupportDownloader$StorageDirType;)Lcom/helpshift/android/commons/downloader/DownloadConfig;
    .locals 3

    .line 61
    sget-object v0, Lcom/helpshift/common/platform/AndroidSupportDownloader$4;->$SwitchMap$com$helpshift$downloader$SupportDownloader$StorageDirType:[I

    invoke-virtual {p1}, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->ordinal()I

    move-result p1

    aget p1, v0, p1

    const/4 v0, 0x1

    const/4 v1, 0x0

    packed-switch p1, :pswitch_data_0

    .line 73
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Unsupported download Dir type"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 70
    :pswitch_0
    sget-object p1, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->EXTERNAL_OR_INTERNAL:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    goto :goto_0

    .line 67
    :pswitch_1
    sget-object p1, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->EXTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    goto :goto_0

    .line 64
    :pswitch_2
    sget-object p1, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->INTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    const/4 v1, 0x1

    .line 75
    :goto_0
    new-instance v2, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;

    invoke-direct {v2}, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;-><init>()V

    .line 76
    invoke-virtual {v2, v0}, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->setUseCache(Z)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;

    move-result-object v2

    .line 77
    invoke-virtual {v2, v1}, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->setIsNoMedia(Z)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;

    move-result-object v1

    .line 78
    invoke-virtual {v1, v0}, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->setWriteToFile(Z)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;

    move-result-object v0

    .line 79
    invoke-virtual {v0, p1}, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->setDownloadDirType(Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;)Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;

    move-result-object p1

    .line 80
    invoke-virtual {p1}, Lcom/helpshift/android/commons/downloader/DownloadConfig$Builder;->create()Lcom/helpshift/android/commons/downloader/DownloadConfig;

    move-result-object p1

    return-object p1

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private declared-synchronized getCallbacks(Ljava/lang/String;)Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Set<",
            "Lcom/helpshift/downloader/SupportDownloadStateChangeListener;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 160
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->callbackManager:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/Set;

    if-nez p1, :cond_0

    .line 163
    new-instance p1, Ljava/util/HashSet;

    invoke-direct {p1}, Ljava/util/HashSet;-><init>()V

    goto :goto_0

    .line 166
    :cond_0
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0, p1}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-object p1, v0

    .line 168
    :goto_0
    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 159
    monitor-exit p0

    throw p1
.end method

.method private declared-synchronized removeCallbacks(Ljava/lang/String;)V
    .locals 1

    monitor-enter p0

    .line 156
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->callbackManager:Ljava/util/Map;

    invoke-interface {v0, p1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 157
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 155
    monitor-exit p0

    throw p1
.end method


# virtual methods
.method handleDownloadFailure(Ljava/lang/String;)V
    .locals 2

    .line 137
    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->getCallbacks(Ljava/lang/String;)Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/downloader/SupportDownloadStateChangeListener;

    .line 138
    invoke-interface {v1, p1}, Lcom/helpshift/downloader/SupportDownloadStateChangeListener;->onFailure(Ljava/lang/String;)V

    goto :goto_0

    .line 140
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->removeCallbacks(Ljava/lang/String;)V

    return-void
.end method

.method handleDownloadSuccess(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 124
    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->getCallbacks(Ljava/lang/String;)Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/downloader/SupportDownloadStateChangeListener;

    .line 125
    invoke-interface {v1, p1, p2}, Lcom/helpshift/downloader/SupportDownloadStateChangeListener;->onSuccess(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 127
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->removeCallbacks(Ljava/lang/String;)V

    return-void
.end method

.method handleProgressChange(Ljava/lang/String;I)V
    .locals 2

    .line 131
    invoke-direct {p0, p1}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->getCallbacks(Ljava/lang/String;)Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/downloader/SupportDownloadStateChangeListener;

    .line 132
    invoke-interface {v1, p1, p2}, Lcom/helpshift/downloader/SupportDownloadStateChangeListener;->onProgressChange(Ljava/lang/String;I)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method public startDownload(Lcom/helpshift/downloader/AdminFileInfo;Lcom/helpshift/downloader/SupportDownloader$StorageDirType;Lcom/helpshift/common/domain/network/AuthDataProvider;Lcom/helpshift/downloader/SupportDownloadStateChangeListener;)V
    .locals 7

    .line 87
    iget-object v0, p1, Lcom/helpshift/downloader/AdminFileInfo;->url:Ljava/lang/String;

    invoke-direct {p0, v0, p4}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->addCallback(Ljava/lang/String;Lcom/helpshift/downloader/SupportDownloadStateChangeListener;)V

    .line 89
    new-instance v2, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object p4, p1, Lcom/helpshift/downloader/AdminFileInfo;->url:Ljava/lang/String;

    iget-boolean v0, p1, Lcom/helpshift/downloader/AdminFileInfo;->isSecureAttachment:Z

    iget-object p1, p1, Lcom/helpshift/downloader/AdminFileInfo;->contentType:Ljava/lang/String;

    invoke-direct {v2, p4, v0, p1}, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;-><init>(Ljava/lang/String;ZLjava/lang/String;)V

    .line 94
    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidSupportDownloader;->downloadManager:Lcom/helpshift/android/commons/downloader/DownloadManager;

    .line 95
    invoke-direct {p0, p2}, Lcom/helpshift/common/platform/AndroidSupportDownloader;->buildDownloadConfig(Lcom/helpshift/downloader/SupportDownloader$StorageDirType;)Lcom/helpshift/android/commons/downloader/DownloadConfig;

    move-result-object v3

    new-instance v4, Lcom/helpshift/common/platform/AndroidSupportDownloader$1;

    invoke-direct {v4, p0, p3}, Lcom/helpshift/common/platform/AndroidSupportDownloader$1;-><init>(Lcom/helpshift/common/platform/AndroidSupportDownloader;Lcom/helpshift/common/domain/network/AuthDataProvider;)V

    new-instance v5, Lcom/helpshift/common/platform/AndroidSupportDownloader$2;

    invoke-direct {v5, p0}, Lcom/helpshift/common/platform/AndroidSupportDownloader$2;-><init>(Lcom/helpshift/common/platform/AndroidSupportDownloader;)V

    new-instance v6, Lcom/helpshift/common/platform/AndroidSupportDownloader$3;

    invoke-direct {v6, p0}, Lcom/helpshift/common/platform/AndroidSupportDownloader$3;-><init>(Lcom/helpshift/common/platform/AndroidSupportDownloader;)V

    .line 94
    invoke-virtual/range {v1 .. v6}, Lcom/helpshift/android/commons/downloader/DownloadManager;->startDownload(Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/DownloadConfig;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V

    return-void
.end method
