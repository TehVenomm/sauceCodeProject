.class public Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;
.super Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;
.source "DownloadInProgressCacheDbStorage.java"


# static fields
.field private static final DOWNLOAD_MANAGER_DB_KEY:Ljava/lang/String; = "kDownloadManagerCachedFiles"


# instance fields
.field private requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;


# direct methods
.method public constructor <init>(Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;)V
    .locals 0

    .line 13
    invoke-direct {p0, p2}, Lcom/helpshift/android/commons/downloader/storage/BaseCacheDbStorage;-><init>(Lcom/helpshift/android/commons/downloader/contracts/DownloaderKeyValueStorage;)V

    .line 14
    iput-object p1, p0, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    return-void
.end method


# virtual methods
.method public getFilePath()Ljava/lang/String;
    .locals 1

    .line 18
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v0, v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->getFilePath(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method getStorageKey()Ljava/lang/String;
    .locals 1

    const-string v0, "kDownloadManagerCachedFiles"

    return-object v0
.end method

.method public insertFilePath(Ljava/lang/String;)V
    .locals 1

    .line 22
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v0, v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-virtual {p0, v0, p1}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->insertFilePath(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public removeFilePath()V
    .locals 1

    .line 26
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v0, v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->removeFilePath(Ljava/lang/String;)V

    return-void
.end method
