.class public abstract Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;
.super Ljava/lang/Object;
.source "BaseDownloadRunnable.java"

# interfaces
.implements Ljava/lang/Runnable;


# static fields
.field protected static final DOWNLOAD_MANAGER_DB_KEY:Ljava/lang/String; = "kDownloadManagerCachedFiles"

.field private static final TAG:Ljava/lang/String; = "Helpshift_DownloadRun"


# instance fields
.field private networkAuthDataFetcher:Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;

.field private onDownloadFinishListener:Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;

.field private onProgressChangedListener:Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;

.field protected requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;


# direct methods
.method constructor <init>(Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;)V
    .locals 0

    .line 47
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 48
    iput-object p1, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    .line 49
    iput-object p2, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->networkAuthDataFetcher:Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;

    .line 50
    iput-object p3, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->onProgressChangedListener:Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;

    .line 51
    iput-object p4, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->onDownloadFinishListener:Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;

    return-void
.end method

.method private buildUrl()Ljava/net/URL;
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/net/MalformedURLException;,
            Ljava/net/URISyntaxException;,
            Ljava/security/GeneralSecurityException;
        }
    .end annotation

    .line 163
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-boolean v0, v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->isSecured:Z

    if-eqz v0, :cond_1

    .line 164
    new-instance v0, Ljava/net/URI;

    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v1, v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-direct {v0, v1}, Ljava/net/URI;-><init>(Ljava/lang/String;)V

    .line 165
    invoke-virtual {v0}, Ljava/net/URI;->getPath()Ljava/lang/String;

    move-result-object v1

    .line 166
    invoke-virtual {v0}, Ljava/net/URI;->getQuery()Ljava/lang/String;

    move-result-object v2

    .line 168
    invoke-direct {p0, v2}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->getQueryMap(Ljava/lang/String;)Ljava/util/Map;

    move-result-object v2

    const-string v3, "v"

    const-string v4, "1"

    .line 169
    invoke-interface {v2, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "uri"

    .line 170
    invoke-interface {v2, v3, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 172
    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->networkAuthDataFetcher:Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;

    invoke-interface {v1, v2}, Lcom/helpshift/android/commons/downloader/contracts/NetworkAuthDataFetcher;->getAuthData(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v1

    .line 174
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 175
    invoke-interface {v1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/Map$Entry;

    .line 176
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    invoke-interface {v3}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "="

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-interface {v3}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v2, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    const-string v1, "&"

    .line 178
    invoke-direct {p0, v1, v2}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->join(Ljava/lang/CharSequence;Ljava/lang/Iterable;)Ljava/lang/String;

    move-result-object v7

    .line 180
    new-instance v1, Ljava/net/URI;

    invoke-virtual {v0}, Ljava/net/URI;->getScheme()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0}, Ljava/net/URI;->getAuthority()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0}, Ljava/net/URI;->getPath()Ljava/lang/String;

    move-result-object v6

    const/4 v8, 0x0

    move-object v3, v1

    invoke-direct/range {v3 .. v8}, Ljava/net/URI;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_1

    .line 183
    :cond_1
    new-instance v1, Ljava/net/URI;

    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v0, v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-direct {v1, v0}, Ljava/net/URI;-><init>(Ljava/lang/String;)V

    .line 185
    :goto_1
    new-instance v0, Ljava/net/URL;

    invoke-virtual {v1}, Ljava/net/URI;->toASCIIString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    return-object v0
.end method

.method private fixSSLSocketProtocols(Ljavax/net/ssl/HttpsURLConnection;)V
    .locals 4

    .line 190
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x10

    if-lt v0, v1, :cond_0

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-gt v0, v1, :cond_0

    .line 194
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "TLSv1.2"

    .line 195
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 198
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    const-string v2, "SSLv3"

    .line 199
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 201
    invoke-virtual {p1}, Ljavax/net/ssl/HttpsURLConnection;->getSSLSocketFactory()Ljavax/net/ssl/SSLSocketFactory;

    move-result-object v2

    .line 202
    new-instance v3, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;

    invoke-direct {v3, v2, v0, v1}, Lcom/helpshift/android/commons/downloader/HelpshiftSSLSocketFactory;-><init>(Ljavax/net/ssl/SSLSocketFactory;Ljava/util/List;Ljava/util/List;)V

    .line 203
    invoke-virtual {p1, v3}, Ljavax/net/ssl/HttpsURLConnection;->setSSLSocketFactory(Ljavax/net/ssl/SSLSocketFactory;)V

    :cond_0
    return-void
.end method

.method private getQueryMap(Ljava/lang/String;)Ljava/util/Map;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    const-string v0, "&"

    .line 208
    invoke-virtual {p1, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    .line 209
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 210
    array-length v1, p1

    const/4 v2, 0x0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v1, :cond_1

    aget-object v4, p1, v3

    const-string v5, "="

    .line 211
    invoke-virtual {v4, v5}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v4

    .line 212
    array-length v5, v4

    const/4 v6, 0x2

    if-ne v5, v6, :cond_0

    .line 213
    aget-object v5, v4, v2

    const/4 v6, 0x1

    .line 214
    aget-object v4, v4, v6

    .line 215
    invoke-interface {v0, v5, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method private join(Ljava/lang/CharSequence;Ljava/lang/Iterable;)Ljava/lang/String;
    .locals 3

    if-nez p2, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 227
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const/4 v1, 0x1

    .line 229
    invoke-interface {p2}, Ljava/lang/Iterable;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    if-eqz v1, :cond_1

    const/4 v1, 0x0

    goto :goto_1

    .line 234
    :cond_1
    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/CharSequence;)Ljava/lang/StringBuilder;

    .line 236
    :goto_1
    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 238
    :cond_2
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method


# virtual methods
.method protected abstract clearCache()V
.end method

.method closeFileStream(Ljava/io/Closeable;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 243
    invoke-interface {p1}, Ljava/io/Closeable;->close()V

    :cond_0
    return-void
.end method

.method protected abstract getAlreadyDownloadedBytes()J
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/FileNotFoundException;
        }
    .end annotation
.end method

.method protected abstract isGzipSupported()Z
.end method

.method notifyDownloadFinish(ZLjava/lang/Object;)V
    .locals 2

    .line 254
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->onDownloadFinishListener:Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;

    if-eqz v0, :cond_0

    .line 255
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->onDownloadFinishListener:Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;

    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v1, v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-interface {v0, p1, v1, p2}, Lcom/helpshift/android/commons/downloader/contracts/OnDownloadFinishListener;->onDownloadFinish(ZLjava/lang/String;Ljava/lang/Object;)V

    :cond_0
    return-void
.end method

.method notifyProgressChange(I)V
    .locals 2

    .line 248
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->onProgressChangedListener:Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;

    if-eqz v0, :cond_0

    .line 249
    iget-object v0, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->onProgressChangedListener:Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;

    iget-object v1, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v1, v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-interface {v0, v1, p1}, Lcom/helpshift/android/commons/downloader/contracts/OnProgressChangedListener;->onProgressChanged(Ljava/lang/String;I)V

    :cond_0
    return-void
.end method

.method protected abstract processHttpResponse(Ljava/io/InputStream;I)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method

.method public run()V
    .locals 11

    const-string v0, "Helpshift_DownloadRun"

    .line 56
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Starting download : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v2, v2, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/16 v0, 0xa

    .line 57
    invoke-static {v0}, Landroid/os/Process;->setThreadPriority(I)V

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 60
    :try_start_0
    invoke-static {}, Ljava/lang/Thread;->interrupted()Z

    move-result v2

    if-nez v2, :cond_6

    .line 63
    invoke-direct {p0}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->buildUrl()Ljava/net/URL;

    move-result-object v2

    const-string v3, "https"

    .line 66
    invoke-virtual {v2}, Ljava/net/URL;->getProtocol()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 67
    invoke-virtual {v2}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v2

    check-cast v2, Ljavax/net/ssl/HttpsURLConnection;

    .line 68
    move-object v3, v2

    check-cast v3, Ljavax/net/ssl/HttpsURLConnection;

    invoke-direct {p0, v3}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->fixSSLSocketProtocols(Ljavax/net/ssl/HttpsURLConnection;)V

    goto :goto_0

    .line 71
    :cond_0
    invoke-virtual {v2}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v2

    check-cast v2, Ljava/net/HttpURLConnection;

    .line 73
    :goto_0
    invoke-virtual {v2, v0}, Ljava/net/HttpURLConnection;->setInstanceFollowRedirects(Z)V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_9
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_8
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_7
    .catch Ljava/security/GeneralSecurityException; {:try_start_0 .. :try_end_0} :catch_6
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_5

    const/4 v3, 0x0

    .line 81
    :try_start_1
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->getAlreadyDownloadedBytes()J

    move-result-wide v4

    const-string v6, "Range"

    .line 82
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "bytes="

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4, v5}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string v4, "-"

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v6, v4}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 84
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v4

    const/16 v5, 0x1a0

    if-eq v4, v5, :cond_4

    .line 95
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v4
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_2
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 96
    :try_start_2
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->isGzipSupported()Z

    move-result v3

    if-eqz v3, :cond_2

    .line 97
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->getHeaderFields()Ljava/util/Map;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v3

    .line 98
    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_1
    :goto_1
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_2

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/Map$Entry;

    .line 99
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    if-eqz v6, :cond_1

    .line 100
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    const-string v7, "Content-Encoding"

    invoke-virtual {v6, v7}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_1

    .line 101
    invoke-interface {v5}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/List;

    invoke-interface {v5, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    const-string v6, "gzip"

    invoke-virtual {v5, v6}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_1

    .line 102
    new-instance v5, Ljava/util/zip/GZIPInputStream;

    invoke-direct {v5, v4}, Ljava/util/zip/GZIPInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    move-object v4, v5

    goto :goto_1

    :cond_2
    move-object v3, v4

    .line 107
    :try_start_3
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->getContentLength()I

    move-result v4

    .line 109
    invoke-virtual {p0, v3, v4}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->processHttpResponse(Ljava/io/InputStream;I)V

    .line 111
    invoke-static {}, Ljava/lang/Thread;->interrupted()Z
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_2
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz v3, :cond_3

    .line 121
    :try_start_4
    invoke-virtual {v3}, Ljava/io/InputStream;->close()V
    :try_end_4
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_0
    .catch Ljava/lang/InterruptedException; {:try_start_4 .. :try_end_4} :catch_9
    .catch Ljava/net/MalformedURLException; {:try_start_4 .. :try_end_4} :catch_8
    .catch Ljava/security/GeneralSecurityException; {:try_start_4 .. :try_end_4} :catch_6
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_5

    goto :goto_2

    :catch_0
    move-exception v3

    .line 124
    :try_start_5
    invoke-virtual {p0, v1, v3}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v4, "Helpshift_DownloadRun"

    const-string v5, "Exception in closing download response"

    .line 125
    new-array v6, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v7, "route"

    iget-object v8, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v8, v8, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    .line 126
    invoke-static {v7, v8}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v7

    aput-object v7, v6, v1

    .line 125
    invoke-static {v4, v5, v3, v6}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 129
    :cond_3
    :goto_2
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_5
    .catch Ljava/lang/InterruptedException; {:try_start_5 .. :try_end_5} :catch_9
    .catch Ljava/net/MalformedURLException; {:try_start_5 .. :try_end_5} :catch_8
    .catch Ljava/io/IOException; {:try_start_5 .. :try_end_5} :catch_7
    .catch Ljava/security/GeneralSecurityException; {:try_start_5 .. :try_end_5} :catch_6
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_5

    goto/16 :goto_6

    :catchall_0
    move-exception v3

    move-object v10, v4

    move-object v4, v3

    move-object v3, v10

    goto :goto_4

    :catch_1
    move-exception v3

    move-object v10, v4

    move-object v4, v3

    move-object v3, v10

    goto :goto_3

    .line 91
    :cond_4
    :try_start_6
    invoke-virtual {p0}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->clearCache()V

    .line 92
    new-instance v4, Ljava/io/IOException;

    const-string v5, "Requested Range Not Satisfiable, failed with 416 status"

    invoke-direct {v4, v5}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v4
    :try_end_6
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_2
    .catchall {:try_start_6 .. :try_end_6} :catchall_1

    :catchall_1
    move-exception v4

    goto :goto_4

    :catch_2
    move-exception v4

    .line 114
    :goto_3
    :try_start_7
    invoke-virtual {p0, v1, v4}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v5, "Helpshift_DownloadRun"

    const-string v6, "Exception in download"

    .line 115
    new-array v7, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v8, "route"

    iget-object v9, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v9, v9, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    .line 116
    invoke-static {v8, v9}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v8

    aput-object v8, v7, v1

    .line 115
    invoke-static {v5, v6, v4, v7}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_1

    if-eqz v3, :cond_3

    .line 121
    :try_start_8
    invoke-virtual {v3}, Ljava/io/InputStream;->close()V
    :try_end_8
    .catch Ljava/io/IOException; {:try_start_8 .. :try_end_8} :catch_3
    .catch Ljava/lang/InterruptedException; {:try_start_8 .. :try_end_8} :catch_9
    .catch Ljava/net/MalformedURLException; {:try_start_8 .. :try_end_8} :catch_8
    .catch Ljava/security/GeneralSecurityException; {:try_start_8 .. :try_end_8} :catch_6
    .catch Ljava/lang/Exception; {:try_start_8 .. :try_end_8} :catch_5

    goto :goto_2

    :catch_3
    move-exception v3

    .line 124
    :try_start_9
    invoke-virtual {p0, v1, v3}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v4, "Helpshift_DownloadRun"

    const-string v5, "Exception in closing download response"

    .line 125
    new-array v6, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v7, "route"

    iget-object v8, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v8, v8, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    .line 126
    invoke-static {v7, v8}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v7

    aput-object v7, v6, v1

    .line 125
    invoke-static {v4, v5, v3, v6}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V
    :try_end_9
    .catch Ljava/lang/InterruptedException; {:try_start_9 .. :try_end_9} :catch_9
    .catch Ljava/net/MalformedURLException; {:try_start_9 .. :try_end_9} :catch_8
    .catch Ljava/io/IOException; {:try_start_9 .. :try_end_9} :catch_7
    .catch Ljava/security/GeneralSecurityException; {:try_start_9 .. :try_end_9} :catch_6
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_5

    goto :goto_2

    :goto_4
    if-eqz v3, :cond_5

    .line 121
    :try_start_a
    invoke-virtual {v3}, Ljava/io/InputStream;->close()V
    :try_end_a
    .catch Ljava/io/IOException; {:try_start_a .. :try_end_a} :catch_4
    .catch Ljava/lang/InterruptedException; {:try_start_a .. :try_end_a} :catch_9
    .catch Ljava/net/MalformedURLException; {:try_start_a .. :try_end_a} :catch_8
    .catch Ljava/security/GeneralSecurityException; {:try_start_a .. :try_end_a} :catch_6
    .catch Ljava/lang/Exception; {:try_start_a .. :try_end_a} :catch_5

    goto :goto_5

    :catch_4
    move-exception v3

    .line 124
    :try_start_b
    invoke-virtual {p0, v1, v3}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v5, "Helpshift_DownloadRun"

    const-string v6, "Exception in closing download response"

    .line 125
    new-array v7, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v8, "route"

    iget-object v9, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v9, v9, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    .line 126
    invoke-static {v8, v9}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v8

    aput-object v8, v7, v1

    .line 125
    invoke-static {v5, v6, v3, v7}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 129
    :cond_5
    :goto_5
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 130
    throw v4

    .line 61
    :cond_6
    new-instance v2, Ljava/lang/InterruptedException;

    invoke-direct {v2}, Ljava/lang/InterruptedException;-><init>()V

    throw v2
    :try_end_b
    .catch Ljava/lang/InterruptedException; {:try_start_b .. :try_end_b} :catch_9
    .catch Ljava/net/MalformedURLException; {:try_start_b .. :try_end_b} :catch_8
    .catch Ljava/io/IOException; {:try_start_b .. :try_end_b} :catch_7
    .catch Ljava/security/GeneralSecurityException; {:try_start_b .. :try_end_b} :catch_6
    .catch Ljava/lang/Exception; {:try_start_b .. :try_end_b} :catch_5

    :catch_5
    move-exception v2

    .line 150
    invoke-virtual {p0, v1, v2}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v3, "Helpshift_DownloadRun"

    const-string v4, "Unknown Exception"

    .line 151
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v5, "route"

    iget-object v6, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v6, v6, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-static {v5, v6}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v5

    aput-object v5, v0, v1

    invoke-static {v3, v4, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_6

    :catch_6
    move-exception v2

    .line 146
    invoke-virtual {p0, v1, v2}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v3, "Helpshift_DownloadRun"

    const-string v4, "GeneralSecurityException"

    .line 147
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v5, "route"

    iget-object v6, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v6, v6, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-static {v5, v6}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v5

    aput-object v5, v0, v1

    invoke-static {v3, v4, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_6

    :catch_7
    move-exception v2

    .line 142
    invoke-virtual {p0, v1, v2}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v3, "Helpshift_DownloadRun"

    const-string v4, "Exception IO"

    .line 143
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v5, "route"

    iget-object v6, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v6, v6, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-static {v5, v6}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v5

    aput-object v5, v0, v1

    invoke-static {v3, v4, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_6

    :catch_8
    move-exception v2

    .line 138
    invoke-virtual {p0, v1, v2}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v3, "Helpshift_DownloadRun"

    const-string v4, "MalformedURLException"

    .line 139
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v5, "route"

    iget-object v6, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v6, v6, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-static {v5, v6}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v5

    aput-object v5, v0, v1

    invoke-static {v3, v4, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    goto :goto_6

    :catch_9
    move-exception v2

    .line 133
    invoke-virtual {p0, v1, v2}, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->notifyDownloadFinish(ZLjava/lang/Object;)V

    const-string v3, "Helpshift_DownloadRun"

    const-string v4, "Exception Interrupted"

    .line 134
    new-array v0, v0, [Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    const-string v5, "route"

    iget-object v6, p0, Lcom/helpshift/android/commons/downloader/runnable/BaseDownloadRunnable;->requestInfo:Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;

    iget-object v6, v6, Lcom/helpshift/android/commons/downloader/contracts/DownloadRequestedFileInfo;->url:Ljava/lang/String;

    invoke-static {v5, v6}, Lcom/helpshift/logger/logmodels/LogExtrasModelProvider;->fromString(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/logger/logmodels/ILogExtrasModel;

    move-result-object v5

    aput-object v5, v0, v1

    invoke-static {v3, v4, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;[Lcom/helpshift/logger/logmodels/ILogExtrasModel;)V

    .line 135
    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Thread;->interrupt()V

    :goto_6
    return-void
.end method
