.class public Lcom/helpshift/android/commons/downloader/runnable/FilteredViewExternalStorageDownloadRunnable;
.super Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;
.source "FilteredViewExternalStorageDownloadRunnable.java"


# instance fields
.field private context:Landroid/content/Context;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V
    .locals 6

    move-object v0, p0

    move-object v1, p2

    move-object v2, p3

    move-object v3, p4

    move-object v4, p5

    move-object v5, p6

    .line 22
    invoke-direct/range {v0 .. v5}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;-><init>(Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V

    .line 24
    iput-object p1, p0, Lcom/helpshift/android/commons/downloader/runnable/FilteredViewExternalStorageDownloadRunnable;->context:Landroid/content/Context;

    return-void
.end method


# virtual methods
.method public getCacheDir()Ljava/io/File;
    .locals 2

    .line 29
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/FilteredViewExternalStorageDownloadRunnable;->context:Landroid/content/Context;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/content/Context;->getExternalFilesDir(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method public isNoMediaDir()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method
