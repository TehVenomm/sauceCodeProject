.class public Lcom/helpshift/android/commons/downloader/runnable/ExternalStorageDownloadRunnable;
.super Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;
.source "ExternalStorageDownloadRunnable.java"


# instance fields
.field private context:Landroid/content/Context;

.field private isNoMediaDir:Z

.field private relativePath:Ljava/lang/String;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Ljava/lang/String;ZLcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V
    .locals 6

    move-object v0, p0

    move-object v1, p2

    move-object v2, p5

    move-object v3, p6

    move-object v4, p7

    move-object v5, p8

    .line 37
    invoke-direct/range {v0 .. v5}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;-><init>(Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V

    .line 39
    iput-object p1, p0, Lcom/helpshift/android/commons/downloader/runnable/ExternalStorageDownloadRunnable;->context:Landroid/content/Context;

    .line 40
    iput-object p3, p0, Lcom/helpshift/android/commons/downloader/runnable/ExternalStorageDownloadRunnable;->relativePath:Ljava/lang/String;

    .line 41
    iput-boolean p4, p0, Lcom/helpshift/android/commons/downloader/runnable/ExternalStorageDownloadRunnable;->isNoMediaDir:Z

    return-void
.end method


# virtual methods
.method public getCacheDir()Ljava/io/File;
    .locals 1

    .line 46
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/ExternalStorageDownloadRunnable;->relativePath:Ljava/lang/String;

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 47
    sget-object v0, Landroid/os/Environment;->DIRECTORY_DOWNLOADS:Ljava/lang/String;

    invoke-static {v0}, Landroid/os/Environment;->getExternalStoragePublicDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    return-object v0

    .line 50
    :cond_0
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/ExternalStorageDownloadRunnable;->relativePath:Ljava/lang/String;

    invoke-static {v0}, Landroid/os/Environment;->getExternalStoragePublicDirectory(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method public isNoMediaDir()Z
    .locals 1

    .line 57
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/ExternalStorageDownloadRunnable;->relativePath:Ljava/lang/String;

    if-eqz v0, :cond_0

    .line 58
    iget-boolean v0, p0, Lcom/helpshift/android/commons/downloader/runnable/ExternalStorageDownloadRunnable;->isNoMediaDir:Z

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method
