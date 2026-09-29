.class public Lcom/github/droidfu/cachefu/ModelCache;
.super Lcom/github/droidfu/cachefu/AbstractCache;
.source "ModelCache.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/github/droidfu/cachefu/AbstractCache<",
        "Ljava/lang/String;",
        "Lcom/github/droidfu/cachefu/CachedModel;",
        ">;"
    }
.end annotation


# instance fields
.field private transactionCount:J


# direct methods
.method public constructor <init>(IJI)V
    .locals 6

    const-string v1, "ModelCache"

    move-object v0, p0

    move v2, p1

    move-wide v3, p2

    move v5, p4

    .line 26
    invoke-direct/range {v0 .. v5}, Lcom/github/droidfu/cachefu/AbstractCache;-><init>(Ljava/lang/String;IJI)V

    const-wide p1, -0x7fffffffffffffffL    # -4.9E-324

    .line 29
    iput-wide p1, p0, Lcom/github/droidfu/cachefu/ModelCache;->transactionCount:J

    return-void
.end method

.method private removeExpiredCache(Ljava/lang/String;)V
    .locals 3

    .line 52
    new-instance v0, Ljava/io/File;

    iget-object v1, p0, Lcom/github/droidfu/cachefu/ModelCache;->diskCacheDirectory:Ljava/lang/String;

    invoke-direct {v0, v1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 54
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v1

    if-nez v1, :cond_0

    return-void

    .line 58
    :cond_0
    new-instance v1, Lcom/github/droidfu/cachefu/ModelCache$1;

    invoke-direct {v1, p0, v0, p1}, Lcom/github/droidfu/cachefu/ModelCache$1;-><init>(Lcom/github/droidfu/cachefu/ModelCache;Ljava/io/File;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Ljava/io/File;->listFiles(Ljava/io/FilenameFilter;)[Ljava/io/File;

    move-result-object p1

    if-eqz p1, :cond_3

    .line 65
    array-length v0, p1

    if-nez v0, :cond_1

    goto :goto_1

    .line 69
    :cond_1
    array-length v0, p1

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_2

    aget-object v2, p1, v1

    .line 70
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

    .line 23
    check-cast p1, Ljava/lang/String;

    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/ModelCache;->getFileNameForKey(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getFileNameForKey(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 76
    invoke-static {p1}, Lcom/github/droidfu/cachefu/CacheHelper;->getFileNameFromUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public declared-synchronized put(Ljava/lang/String;Lcom/github/droidfu/cachefu/CachedModel;)Lcom/github/droidfu/cachefu/CachedModel;
    .locals 4

    monitor-enter p0

    .line 33
    :try_start_0
    iget-wide v0, p0, Lcom/github/droidfu/cachefu/ModelCache;->transactionCount:J

    const-wide/16 v2, 0x1

    add-long/2addr v2, v0

    iput-wide v2, p0, Lcom/github/droidfu/cachefu/ModelCache;->transactionCount:J

    invoke-virtual {p2, v0, v1}, Lcom/github/droidfu/cachefu/CachedModel;->setTransactionId(J)V

    .line 34
    invoke-super {p0, p1, p2}, Lcom/github/droidfu/cachefu/AbstractCache;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/github/droidfu/cachefu/CachedModel;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 32
    monitor-exit p0

    throw p1
.end method

.method public bridge synthetic put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    .line 23
    check-cast p1, Ljava/lang/String;

    check-cast p2, Lcom/github/droidfu/cachefu/CachedModel;

    invoke-virtual {p0, p1, p2}, Lcom/github/droidfu/cachefu/ModelCache;->put(Ljava/lang/String;Lcom/github/droidfu/cachefu/CachedModel;)Lcom/github/droidfu/cachefu/CachedModel;

    move-result-object p1

    return-object p1
.end method

.method protected readValueFromDisk(Ljava/io/File;)Lcom/github/droidfu/cachefu/CachedModel;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 81
    new-instance v0, Ljava/io/FileInputStream;

    invoke-direct {v0, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    .line 83
    invoke-virtual {p1}, Ljava/io/File;->length()J

    move-result-wide v1

    long-to-int p1, v1

    new-array p1, p1, [B

    .line 84
    new-instance v1, Ljava/io/BufferedInputStream;

    invoke-direct {v1, v0}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    .line 85
    invoke-virtual {v1, p1}, Ljava/io/BufferedInputStream;->read([B)I

    .line 86
    invoke-virtual {v1}, Ljava/io/BufferedInputStream;->close()V

    .line 88
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object v0

    .line 89
    array-length v1, p1

    const/4 v2, 0x0

    invoke-virtual {v0, p1, v2, v1}, Landroid/os/Parcel;->unmarshall([BII)V

    .line 90
    invoke-virtual {v0, v2}, Landroid/os/Parcel;->setDataPosition(I)V

    .line 91
    new-instance p1, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;

    invoke-direct {p1}, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;-><init>()V

    .line 92
    invoke-virtual {p1, v0}, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->readFromParcel(Landroid/os/Parcel;)V

    .line 94
    invoke-virtual {p1}, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->getCachedModel()Lcom/github/droidfu/cachefu/CachedModel;

    move-result-object p1

    return-object p1
.end method

.method protected bridge synthetic readValueFromDisk(Ljava/io/File;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 23
    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/ModelCache;->readValueFromDisk(Ljava/io/File;)Lcom/github/droidfu/cachefu/CachedModel;

    move-result-object p1

    return-object p1
.end method

.method public declared-synchronized removeAllWithPrefix(Ljava/lang/String;)V
    .locals 3

    monitor-enter p0

    .line 38
    :try_start_0
    invoke-virtual {p0}, Lcom/github/droidfu/cachefu/ModelCache;->keySet()Ljava/util/Set;

    move-result-object v0

    .line 40
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

    .line 41
    invoke-virtual {v1, p1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 42
    invoke-virtual {p0, v1}, Lcom/github/droidfu/cachefu/ModelCache;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 46
    :cond_1
    invoke-virtual {p0}, Lcom/github/droidfu/cachefu/ModelCache;->isDiskCacheEnabled()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 47
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/ModelCache;->removeExpiredCache(Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 49
    :cond_2
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 37
    monitor-exit p0

    throw p1
.end method

.method protected writeValueToDisk(Ljava/io/File;Lcom/github/droidfu/cachefu/CachedModel;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 99
    new-instance v0, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;

    invoke-direct {v0}, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;-><init>()V

    .line 100
    invoke-virtual {v0, p2}, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->setCachedModel(Lcom/github/droidfu/cachefu/CachedModel;)V

    .line 102
    invoke-static {}, Landroid/os/Parcel;->obtain()Landroid/os/Parcel;

    move-result-object p2

    const/4 v1, 0x0

    .line 103
    invoke-virtual {v0, p2, v1}, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->writeToParcel(Landroid/os/Parcel;I)V

    .line 104
    invoke-virtual {p2}, Landroid/os/Parcel;->marshall()[B

    move-result-object p2

    .line 106
    new-instance v0, Ljava/io/FileOutputStream;

    invoke-direct {v0, p1}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V

    .line 107
    new-instance p1, Ljava/io/BufferedOutputStream;

    invoke-direct {p1, v0}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    .line 108
    invoke-virtual {p1, p2}, Ljava/io/BufferedOutputStream;->write([B)V

    return-void
.end method

.method protected bridge synthetic writeValueToDisk(Ljava/io/File;Ljava/lang/Object;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 23
    check-cast p2, Lcom/github/droidfu/cachefu/CachedModel;

    invoke-virtual {p0, p1, p2}, Lcom/github/droidfu/cachefu/ModelCache;->writeValueToDisk(Ljava/io/File;Lcom/github/droidfu/cachefu/CachedModel;)V

    return-void
.end method
