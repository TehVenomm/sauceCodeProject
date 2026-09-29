.class public Lcom/github/droidfu/cachefu/HttpResponseCache;
.super Lcom/github/droidfu/cachefu/AbstractCache;
.source "HttpResponseCache.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/github/droidfu/cachefu/AbstractCache<",
        "Ljava/lang/String;",
        "Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;",
        ">;"
    }
.end annotation


# direct methods
.method public constructor <init>(IJI)V
    .locals 6

    const-string v1, "HttpCache"

    move-object v0, p0

    move v2, p1

    move-wide v3, p2

    move v5, p4

    .line 25
    invoke-direct/range {v0 .. v5}, Lcom/github/droidfu/cachefu/AbstractCache;-><init>(Ljava/lang/String;IJI)V

    return-void
.end method

.method private removeExpiredCache(Ljava/lang/String;)V
    .locals 3

    .line 43
    new-instance v0, Ljava/io/File;

    iget-object v1, p0, Lcom/github/droidfu/cachefu/HttpResponseCache;->diskCacheDirectory:Ljava/lang/String;

    invoke-direct {v0, v1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 45
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v1

    if-nez v1, :cond_0

    return-void

    .line 49
    :cond_0
    new-instance v1, Lcom/github/droidfu/cachefu/HttpResponseCache$1;

    invoke-direct {v1, p0, v0, p1}, Lcom/github/droidfu/cachefu/HttpResponseCache$1;-><init>(Lcom/github/droidfu/cachefu/HttpResponseCache;Ljava/io/File;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Ljava/io/File;->listFiles(Ljava/io/FilenameFilter;)[Ljava/io/File;

    move-result-object p1

    if-eqz p1, :cond_3

    .line 56
    array-length v0, p1

    if-nez v0, :cond_1

    goto :goto_1

    .line 60
    :cond_1
    array-length v0, p1

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_2

    aget-object v2, p1, v1

    .line 61
    invoke-virtual {v2}, Ljava/io/File;->delete()Z

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_2
    return-void

    :cond_3
    :goto_1
    return-void
.end method


# virtual methods
.method public bridge synthetic getFileNameForKey(Ljava/lang/Object;)Ljava/lang/String;
    .locals 0

    .line 22
    check-cast p1, Ljava/lang/String;

    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/HttpResponseCache;->getFileNameForKey(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getFileNameForKey(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 67
    invoke-static {p1}, Lcom/github/droidfu/cachefu/CacheHelper;->getFileNameFromUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method protected readValueFromDisk(Ljava/io/File;)Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 72
    new-instance v0, Ljava/io/BufferedInputStream;

    new-instance v1, Ljava/io/FileInputStream;

    invoke-direct {v1, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    invoke-direct {v0, v1}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    .line 73
    invoke-virtual {p1}, Ljava/io/File;->length()J

    move-result-wide v1

    const-wide/32 v3, 0x7fffffff

    cmp-long p1, v1, v3

    if-gtz p1, :cond_0

    .line 79
    invoke-virtual {v0}, Ljava/io/BufferedInputStream;->read()I

    move-result p1

    long-to-int v1, v1

    add-int/lit8 v1, v1, -0x1

    .line 84
    new-array v2, v1, [B

    const/4 v3, 0x0

    .line 85
    invoke-virtual {v0, v2, v3, v1}, Ljava/io/BufferedInputStream;->read([BII)I

    .line 86
    invoke-virtual {v0}, Ljava/io/BufferedInputStream;->close()V

    .line 88
    new-instance v0, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    invoke-direct {v0, p1, v2}, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;-><init>(I[B)V

    return-object v0

    .line 75
    :cond_0
    new-instance p1, Ljava/io/IOException;

    const-string v0, "Cannot read files larger than 2147483647 bytes"

    invoke-direct {p1, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method protected bridge synthetic readValueFromDisk(Ljava/io/File;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 22
    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/HttpResponseCache;->readValueFromDisk(Ljava/io/File;)Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    move-result-object p1

    return-object p1
.end method

.method public declared-synchronized removeAllWithPrefix(Ljava/lang/String;)V
    .locals 3

    monitor-enter p0

    .line 29
    :try_start_0
    invoke-virtual {p0}, Lcom/github/droidfu/cachefu/HttpResponseCache;->keySet()Ljava/util/Set;

    move-result-object v0

    .line 31
    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 32
    invoke-virtual {v1, p1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 33
    invoke-virtual {p0, v1}, Lcom/github/droidfu/cachefu/HttpResponseCache;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 37
    :cond_1
    invoke-virtual {p0}, Lcom/github/droidfu/cachefu/HttpResponseCache;->isDiskCacheEnabled()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 38
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/HttpResponseCache;->removeExpiredCache(Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 40
    :cond_2
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 28
    monitor-exit p0

    throw p1
.end method

.method protected writeValueToDisk(Ljava/io/File;Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 93
    new-instance v0, Ljava/io/BufferedOutputStream;

    new-instance v1, Ljava/io/FileOutputStream;

    invoke-direct {v1, p1}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V

    invoke-direct {v0, v1}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    .line 95
    invoke-virtual {p2}, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->getStatusCode()I

    move-result p1

    invoke-virtual {v0, p1}, Ljava/io/BufferedOutputStream;->write(I)V

    .line 96
    invoke-virtual {p2}, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->getResponseBody()[B

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/io/BufferedOutputStream;->write([B)V

    .line 98
    invoke-virtual {v0}, Ljava/io/BufferedOutputStream;->close()V

    return-void
.end method

.method protected bridge synthetic writeValueToDisk(Ljava/io/File;Ljava/lang/Object;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 22
    check-cast p2, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;

    invoke-virtual {p0, p1, p2}, Lcom/github/droidfu/cachefu/HttpResponseCache;->writeValueToDisk(Ljava/io/File;Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;)V

    return-void
.end method
