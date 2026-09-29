.class public Lcom/github/droidfu/cachefu/ImageCache;
.super Lcom/github/droidfu/cachefu/AbstractCache;
.source "ImageCache.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/github/droidfu/cachefu/AbstractCache<",
        "Ljava/lang/String;",
        "[B>;"
    }
.end annotation


# direct methods
.method public constructor <init>(IJI)V
    .locals 6

    const-string v1, "ImageCache"

    move-object v0, p0

    move v2, p1

    move-wide v3, p2

    move v5, p4

    .line 38
    invoke-direct/range {v0 .. v5}, Lcom/github/droidfu/cachefu/AbstractCache;-><init>(Ljava/lang/String;IJI)V

    return-void
.end method


# virtual methods
.method public declared-synchronized getBitmap(Ljava/lang/Object;)Landroid/graphics/Bitmap;
    .locals 2

    monitor-enter p0

    .line 64
    :try_start_0
    invoke-super {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, [B
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez p1, :cond_0

    const/4 p1, 0x0

    .line 66
    monitor-exit p0

    return-object p1

    :cond_0
    const/4 v0, 0x0

    .line 68
    :try_start_1
    array-length v1, p1

    invoke-static {p1, v0, v1}, Landroid/graphics/BitmapFactory;->decodeByteArray([BII)Landroid/graphics/Bitmap;

    move-result-object p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 63
    monitor-exit p0

    throw p1
.end method

.method public bridge synthetic getFileNameForKey(Ljava/lang/Object;)Ljava/lang/String;
    .locals 0

    .line 35
    check-cast p1, Ljava/lang/String;

    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/ImageCache;->getFileNameForKey(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getFileNameForKey(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 43
    invoke-static {p1}, Lcom/github/droidfu/cachefu/CacheHelper;->getFileNameFromUrl(Ljava/lang/String;)Ljava/lang/String;

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

    .line 35
    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/ImageCache;->readValueFromDisk(Ljava/io/File;)[B

    move-result-object p1

    return-object p1
.end method

.method protected readValueFromDisk(Ljava/io/File;)[B
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 48
    new-instance v0, Ljava/io/BufferedInputStream;

    new-instance v1, Ljava/io/FileInputStream;

    invoke-direct {v1, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    invoke-direct {v0, v1}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    .line 49
    invoke-virtual {p1}, Ljava/io/File;->length()J

    move-result-wide v1

    const-wide/32 v3, 0x7fffffff

    cmp-long p1, v1, v3

    if-gtz p1, :cond_0

    long-to-int p1, v1

    .line 56
    new-array v1, p1, [B

    const/4 v2, 0x0

    .line 57
    invoke-virtual {v0, v1, v2, p1}, Ljava/io/BufferedInputStream;->read([BII)I

    .line 58
    invoke-virtual {v0}, Ljava/io/BufferedInputStream;->close()V

    return-object v1

    .line 51
    :cond_0
    new-instance p1, Ljava/io/IOException;

    const-string v0, "Cannot read files larger than 2147483647 bytes"

    invoke-direct {p1, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method protected bridge synthetic writeValueToDisk(Ljava/io/File;Ljava/lang/Object;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 35
    check-cast p2, [B

    invoke-virtual {p0, p1, p2}, Lcom/github/droidfu/cachefu/ImageCache;->writeValueToDisk(Ljava/io/File;[B)V

    return-void
.end method

.method protected writeValueToDisk(Ljava/io/File;[B)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 73
    new-instance v0, Ljava/io/BufferedOutputStream;

    new-instance v1, Ljava/io/FileOutputStream;

    invoke-direct {v1, p1}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V

    invoke-direct {v0, v1}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V

    .line 75
    invoke-virtual {v0, p2}, Ljava/io/BufferedOutputStream;->write([B)V

    .line 77
    invoke-virtual {v0}, Ljava/io/BufferedOutputStream;->close()V

    return-void
.end method
