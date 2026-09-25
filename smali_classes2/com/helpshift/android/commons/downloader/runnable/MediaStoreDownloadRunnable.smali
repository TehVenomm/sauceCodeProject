.class public Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;
.super Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;
.source "MediaStoreDownloadRunnable.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_mediaRun"


# instance fields
.field private context:Landroid/content/Context;

.field private downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V
    .locals 0

    .line 37
    invoke-direct {p0, p2, p4, p5, p6}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;-><init>(Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V

    .line 38
    iput-object p1, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->context:Landroid/content/Context;

    .line 39
    iput-object p3, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    return-void
.end method

.method private buildUri(Ljava/lang/String;)Landroid/net/Uri;
    .locals 3

    .line 201
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->context:Landroid/content/Context;

    invoke-static {v0, p1}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->canReadFileAtUri(Landroid/content/Context;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    .line 207
    :cond_0
    :try_start_0
    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "Helpshift_mediaRun"

    const-string v2, "Error while converting filePath to uri"

    .line 210
    invoke-static {v0, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object p1, v1

    :goto_0
    return-object p1
.end method

.method private createFile(Ljava/lang/String;Ljava/lang/String;)Landroid/net/Uri;
    .locals 4

    .line 139
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x1d

    if-ge v0, v1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 143
    :cond_0
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    .line 144
    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->context:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v1

    .line 146
    invoke-direct {p0, p2}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->isImageType(Ljava/lang/String;)Z

    move-result v2

    const/4 v3, 0x1

    if-eqz v2, :cond_1

    const-string v2, "_display_name"

    .line 147
    invoke-virtual {v0, v2, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "mime_type"

    .line 148
    invoke-virtual {v0, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "is_pending"

    .line 149
    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {v0, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string p1, "external_primary"

    .line 150
    invoke-static {p1}, Landroid/provider/MediaStore$Images$Media;->getContentUri(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    goto :goto_0

    :cond_1
    const-string v2, "_display_name"

    .line 153
    invoke-virtual {v0, v2, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "mime_type"

    .line 154
    invoke-virtual {v0, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "is_pending"

    .line 155
    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {v0, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string p1, "external_primary"

    .line 156
    invoke-static {p1}, Landroid/provider/MediaStore$Downloads;->getContentUri(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    .line 159
    :goto_0
    invoke-virtual {v1, p1, v0}, Landroid/content/ContentResolver;->insert(Landroid/net/Uri;Landroid/content/ContentValues;)Landroid/net/Uri;

    move-result-object p1

    return-object p1
.end method

.method private deleteUri(Landroid/net/Uri;)V
    .locals 2

    if-nez p1, :cond_0

    return-void

    .line 240
    :cond_0
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v0

    const/4 v1, 0x0

    .line 241
    invoke-virtual {v0, p1, v1, v1}, Landroid/content/ContentResolver;->delete(Landroid/net/Uri;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "Helpshift_mediaRun"

    const-string v1, "Error when deleting a file via uri"

    .line 244
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-void
.end method

.method private generateFileName()Ljava/lang/String;
    .locals 4

    .line 178
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Support_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v1, v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

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

.method private getCachedFileUri()Landroid/net/Uri;
    .locals 3

    .line 182
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {v0}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->getFilePath()Ljava/lang/String;

    move-result-object v0

    .line 183
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    return-object v2

    .line 187
    :cond_0
    invoke-direct {p0, v0}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->buildUri(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v0

    if-eqz v0, :cond_1

    return-object v0

    .line 194
    :cond_1
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {v0}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->removeFilePath()V

    return-object v2
.end method

.method private getFileUriToWriteResponseData()Landroid/net/Uri;
    .locals 2

    .line 128
    invoke-direct {p0}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->getCachedFileUri()Landroid/net/Uri;

    move-result-object v0

    if-eqz v0, :cond_0

    return-object v0

    .line 133
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->generateFileName()Ljava/lang/String;

    move-result-object v0

    .line 134
    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v1, v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->contentType:Ljava/lang/String;

    invoke-direct {p0, v0, v1}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->createFile(Ljava/lang/String;Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v0

    return-object v0
.end method

.method private isImageType(Ljava/lang/String;)Z
    .locals 3

    .line 217
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return v1

    :cond_0
    const-string v0, "image/.*"

    .line 224
    :try_start_0
    invoke-static {v0}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v0

    .line 225
    invoke-virtual {v0, p1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p1

    .line 226
    invoke-virtual {p1}, Ljava/util/regex/Matcher;->matches()Z

    move-result p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "Helpshift_mediaRun"

    const-string v2, "Error when check image mime type"

    .line 229
    invoke-static {v0, v2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method private updateIsPendingFlag(Landroid/net/Uri;Ljava/lang/String;)V
    .locals 2

    .line 163
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x1d

    if-ge v0, v1, :cond_0

    return-void

    .line 167
    :cond_0
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    .line 168
    invoke-direct {p0, p2}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->isImageType(Ljava/lang/String;)Z

    move-result p2

    const/4 v1, 0x0

    if-eqz p2, :cond_1

    const-string p2, "is_pending"

    .line 169
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    goto :goto_0

    :cond_1
    const-string p2, "is_pending"

    .line 172
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-virtual {v0, p2, v1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    .line 174
    :goto_0
    iget-object p2, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->context:Landroid/content/Context;

    invoke-virtual {p2}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p2

    const/4 v1, 0x0

    invoke-virtual {p2, p1, v0, v1, v1}, Landroid/content/ContentResolver;->update(Landroid/net/Uri;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    return-void
.end method


# virtual methods
.method protected clearCache()V
    .locals 2

    .line 63
    invoke-direct {p0}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->getCachedFileUri()Landroid/net/Uri;

    move-result-object v0

    .line 64
    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {v1}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->removeFilePath()V

    .line 65
    invoke-direct {p0, v0}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->deleteUri(Landroid/net/Uri;)V

    return-void
.end method

.method protected getAlreadyDownloadedBytes()J
    .locals 5

    .line 44
    invoke-direct {p0}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->getCachedFileUri()Landroid/net/Uri;

    move-result-object v0

    const-wide/16 v1, 0x0

    if-eqz v0, :cond_0

    .line 48
    :try_start_0
    iget-object v3, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->context:Landroid/content/Context;

    invoke-virtual {v3}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v3

    const-string v4, "r"

    .line 49
    invoke-virtual {v3, v0, v4}, Landroid/content/ContentResolver;->openFileDescriptor(Landroid/net/Uri;Ljava/lang/String;)Landroid/os/ParcelFileDescriptor;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 51
    invoke-virtual {v0}, Landroid/os/ParcelFileDescriptor;->getStatSize()J

    move-result-wide v3
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    move-wide v1, v3

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v3, "Helpshift_mediaRun"

    const-string v4, "Exception while getting file size via Uri"

    .line 55
    invoke-static {v3, v4, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    :goto_0
    return-wide v1
.end method

.method protected isGzipSupported()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method protected processHttpResponse(Ljava/io/InputStream;I)V
    .locals 13
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 77
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->getAlreadyDownloadedBytes()J

    move-result-wide v0

    .line 78
    invoke-direct {p0}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->getFileUriToWriteResponseData()Landroid/net/Uri;

    move-result-object v2

    const/4 v3, 0x0

    const/4 v4, 0x0

    if-nez v2, :cond_0

    .line 80
    invoke-virtual {p0, v4, v3}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    return-void

    .line 84
    :cond_0
    iget-object v5, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {v2}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->insertFilePath(Ljava/lang/String;)V

    .line 89
    :try_start_0
    iget-object v5, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->context:Landroid/content/Context;

    invoke-virtual {v5}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v5

    const-string v6, "w"

    invoke-virtual {v5, v2, v6}, Landroid/content/ContentResolver;->openFileDescriptor(Landroid/net/Uri;Ljava/lang/String;)Landroid/os/ParcelFileDescriptor;

    move-result-object v5
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    if-nez v5, :cond_1

    .line 91
    :try_start_1
    invoke-virtual {p0, v4, v3}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 122
    invoke-virtual {p0, v3}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->closeFileStream(Ljava/io/Closeable;)V

    .line 123
    invoke-static {v5}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->closeParcelFileDescriptor(Landroid/os/ParcelFileDescriptor;)V

    return-void

    :catchall_0
    move-exception p1

    move-object v6, v3

    goto/16 :goto_1

    .line 94
    :cond_1
    :try_start_2
    new-instance v6, Ljava/io/FileOutputStream;

    invoke-virtual {v5}, Landroid/os/ParcelFileDescriptor;->getFileDescriptor()Ljava/io/FileDescriptor;

    move-result-object v7

    invoke-direct {v6, v7}, Ljava/io/FileOutputStream;-><init>(Ljava/io/FileDescriptor;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    const/16 v3, 0x2000

    .line 96
    :try_start_3
    new-array v7, v3, [B

    const-wide/16 v8, 0x0

    .line 98
    :cond_2
    :goto_0
    invoke-virtual {p1, v7, v4, v3}, Ljava/io/InputStream;->read([BII)I

    move-result v10

    const/4 v11, -0x1

    if-eq v10, v11, :cond_4

    if-ltz v10, :cond_3

    .line 103
    invoke-virtual {v6, v7, v4, v10}, Ljava/io/FileOutputStream;->write([BII)V

    .line 104
    invoke-virtual {v5}, Landroid/os/ParcelFileDescriptor;->getStatSize()J

    move-result-wide v10

    long-to-float v10, v10

    int-to-long v11, p2

    add-long/2addr v11, v0

    long-to-float v11, v11

    div-float/2addr v10, v11

    const/high16 v11, 0x42c80000    # 100.0f

    mul-float v10, v10, v11

    float-to-long v10, v10

    cmp-long v12, v10, v8

    if-eqz v12, :cond_2

    long-to-int v8, v10

    .line 108
    invoke-virtual {p0, v8}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->notifyProgressChange(I)V

    move-wide v8, v10

    goto :goto_0

    .line 100
    :cond_3
    new-instance p1, Ljava/io/EOFException;

    invoke-direct {p1}, Ljava/io/EOFException;-><init>()V

    throw p1

    .line 113
    :cond_4
    iget-object p1, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object p1, p1, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->contentType:Ljava/lang/String;

    invoke-direct {p0, v2, p1}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->updateIsPendingFlag(Landroid/net/Uri;Ljava/lang/String;)V

    .line 116
    iget-object p1, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->downloadInProgressCacheDbStorage:Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;

    invoke-virtual {p1}, Lcom/helpshift/android/commons/downloader/storage/DownloadInProgressCacheDbStorage;->removeFilePath()V

    const-string p1, "Helpshift_mediaRun"

    .line 118
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Download finished : "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v0, v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "\n URI : "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, 0x1

    .line 119
    invoke-virtual {p0, p1, v2}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    .line 122
    invoke-virtual {p0, v6}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->closeFileStream(Ljava/io/Closeable;)V

    .line 123
    invoke-static {v5}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->closeParcelFileDescriptor(Landroid/os/ParcelFileDescriptor;)V

    return-void

    :catchall_1
    move-exception p1

    goto :goto_1

    :catchall_2
    move-exception p1

    move-object v5, v3

    move-object v6, v5

    .line 122
    :goto_1
    invoke-virtual {p0, v6}, Lcom/helpshift/android/commons/downloader/runnable/MediaStoreDownloadRunnable;->closeFileStream(Ljava/io/Closeable;)V

    .line 123
    invoke-static {v5}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->closeParcelFileDescriptor(Landroid/os/ParcelFileDescriptor;)V

    .line 124
    throw p1
.end method
