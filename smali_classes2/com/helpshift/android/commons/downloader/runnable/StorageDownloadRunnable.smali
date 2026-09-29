.class public abstract Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;
.super Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;
.source "StorageDownloadRunnable.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_InterDownRun"


# instance fields
.field private downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;


# direct methods
.method constructor <init>(Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V
    .locals 0

    .line 29
    invoke-direct {p0, p1, p3, p4, p5}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;-><init>(Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V

    .line 30
    iput-object p2, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    return-void
.end method

.method private checkAndCreateNoMediaFile(Ljava/io/File;)V
    .locals 2

    :try_start_0
    const-string v0, ".nomedia"

    .line 157
    new-instance v1, Ljava/io/File;

    invoke-direct {v1, p1, v0}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 158
    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result p1

    if-nez p1, :cond_0

    .line 159
    invoke-virtual {v1}, Ljava/io/File;->createNewFile()Z
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "Helpshift_InterDownRun"

    const-string v1, "Exception while creating no media file"

    .line 163
    invoke-static {v0, v1, p1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method private deleteFile(Ljava/io/File;)V
    .locals 2

    if-nez p1, :cond_0

    return-void

    .line 126
    :cond_0
    :try_start_0
    invoke-virtual {p1}, Ljava/io/File;->delete()Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "Helpshift_InterDownRun"

    const-string v1, "Exception in deleting file "

    .line 129
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-void
.end method

.method private generateFileName()Ljava/lang/String;
    .locals 4

    .line 168
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Support_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v1, v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v2, v2, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    const-string v3, "/"

    invoke-virtual {v2, v3}, Ljava/lang/String;->lastIndexOf(Ljava/lang/String;)I

    move-result v2

    add-int/lit8 v2, v2, 0x1

    invoke-virtual {v1, v2}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method private getFileToWriteResponseData()Ljava/io/File;
    .locals 3

    .line 95
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->getCachedFile()Ljava/io/File;

    move-result-object v0

    if-eqz v0, :cond_0

    return-object v0

    .line 100
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->getCacheDir()Ljava/io/File;

    move-result-object v0

    .line 102
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v1

    if-nez v1, :cond_1

    .line 103
    invoke-virtual {v0}, Ljava/io/File;->mkdirs()Z

    .line 106
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->isNoMediaDir()Z

    move-result v1

    if-eqz v1, :cond_2

    .line 107
    invoke-direct {p0, v0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->checkAndCreateNoMediaFile(Ljava/io/File;)V

    .line 110
    :cond_2
    invoke-direct {p0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->generateFileName()Ljava/lang/String;

    move-result-object v1

    .line 111
    new-instance v2, Ljava/io/File;

    invoke-direct {v2, v0, v1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 112
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {v2}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->insertFilePath(Ljava/lang/String;)V

    return-object v2
.end method


# virtual methods
.method protected clearCache()V
    .locals 2

    .line 46
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->getCachedFile()Ljava/io/File;

    move-result-object v0

    .line 47
    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {v1}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->removeFilePath()V

    .line 48
    invoke-direct {p0, v0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->deleteFile(Ljava/io/File;)V

    return-void
.end method

.method protected getAlreadyDownloadedBytes()J
    .locals 2

    .line 35
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->getCachedFile()Ljava/io/File;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 40
    invoke-virtual {v0}, Ljava/io/File;->length()J

    move-result-wide v0

    goto :goto_0

    :cond_0
    const-wide/16 v0, 0x0

    :goto_0
    return-wide v0
.end method

.method public abstract getCacheDir()Ljava/io/File;
.end method

.method public getCachedFile()Ljava/io/File;
    .locals 3

    .line 134
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {v0}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->getFilePath()Ljava/lang/String;

    move-result-object v0

    .line 135
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    return-object v2

    .line 139
    :cond_0
    new-instance v1, Ljava/io/File;

    invoke-direct {v1, v0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 140
    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-virtual {v1}, Ljava/io/File;->canWrite()Z

    move-result v0

    if-eqz v0, :cond_1

    return-object v1

    .line 148
    :cond_1
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {v0}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->removeFilePath()V

    return-object v2
.end method

.method protected isGzipSupported()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public abstract isNoMediaDir()Z
.end method

.method protected processHttpResponse(Ljava/io/InputStream;I)V
    .locals 12
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 60
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->getAlreadyDownloadedBytes()J

    move-result-wide v0

    .line 61
    invoke-direct {p0}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->getFileToWriteResponseData()Ljava/io/File;

    move-result-object v2

    const/4 v3, 0x0

    .line 64
    :try_start_0
    new-instance v4, Ljava/io/FileOutputStream;

    const/4 v5, 0x1

    invoke-direct {v4, v2, v5}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;Z)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    const/16 v3, 0x2000

    .line 66
    :try_start_1
    new-array v6, v3, [B

    const-wide/16 v7, 0x0

    :cond_0
    :goto_0
    const/4 v9, 0x0

    .line 68
    invoke-virtual {p1, v6, v9, v3}, Ljava/io/InputStream;->read([BII)I

    move-result v10

    const/4 v11, -0x1

    if-eq v10, v11, :cond_2

    if-ltz v10, :cond_1

    .line 73
    invoke-virtual {v4, v6, v9, v10}, Ljava/io/FileOutputStream;->write([BII)V

    .line 74
    invoke-virtual {v2}, Ljava/io/File;->length()J

    move-result-wide v9

    long-to-float v9, v9

    int-to-long v10, p2

    add-long/2addr v10, v0

    long-to-float v10, v10

    div-float/2addr v9, v10

    const/high16 v10, 0x42c80000    # 100.0f

    mul-float v9, v9, v10

    float-to-long v9, v9

    cmp-long v11, v9, v7

    if-eqz v11, :cond_0

    long-to-int v7, v9

    .line 78
    invoke-virtual {p0, v7}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->notifyProgressChange(I)V

    move-wide v7, v9

    goto :goto_0

    .line 70
    :cond_1
    new-instance p1, Ljava/io/EOFException;

    invoke-direct {p1}, Ljava/io/EOFException;-><init>()V

    throw p1

    .line 83
    :cond_2
    iget-object p1, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {p1}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->removeFilePath()V

    .line 85
    invoke-virtual {v2}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Helpshift_InterDownRun"

    .line 86
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Download finished : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v1, v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p2, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 87
    invoke-virtual {p0, v5, p1}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 90
    invoke-virtual {p0, v4}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->closeFileStream(Ljava/io/Closeable;)V

    return-void

    :catchall_0
    move-exception p1

    goto :goto_1

    :catchall_1
    move-exception p1

    move-object v4, v3

    :goto_1
    invoke-virtual {p0, v4}, Lcom/helpshift/android/commons/downloader/runnable/StorageDownloadRunnable;->closeFileStream(Ljava/io/Closeable;)V

    .line 91
    throw p1
.end method
