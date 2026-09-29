.class public Lcom/helpshift/android/commons/downloader/storage/DownloadManagerCacheDbStorage;
.super Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;
.source "DownloadManagerCacheDbStorage.java"


# static fields
.field private static final KEY_CACHED_DOWNLOADS:Ljava/lang/String; = "hs-cached-downloads"


# direct methods
.method public constructor <init>(Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;)V
    .locals 0

    .line 10
    invoke-direct {p0, p1}, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;-><init>(Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;)V

    return-void
.end method


# virtual methods
.method getStorageKey()Ljava/lang/String;
    .locals 1

    const-string v0, "hs-cached-downloads"

    return-object v0
.end method
