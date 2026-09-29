.class public abstract Lcom/github/droidfu/cachefu/AbstractCache;
.super Ljava/lang/Object;
.source "AbstractCache.java"

# interfaces
.implements Ljava/util/Map;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<KeyT:",
        "Ljava/lang/Object;",
        "ValT:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;",
        "Ljava/util/Map<",
        "TKeyT;TValT;>;"
    }
.end annotation


# static fields
.field public static final DISK_CACHE_INTERNAL:I = 0x0

.field public static final DISK_CACHE_SDCARD:I = 0x1

.field private static final LOG_TAG:Ljava/lang/String; = "Droid-Fu[CacheFu]"


# instance fields
.field private cache:Ljava/util/concurrent/ConcurrentMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/ConcurrentMap<",
            "TKeyT;TValT;>;"
        }
    .end annotation
.end field

.field protected diskCacheDirectory:Ljava/lang/String;

.field private isDiskCacheEnabled:Z

.field private name:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;IJI)V
    .locals 2

    .line 88
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 90
    iput-object p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->name:Ljava/lang/String;

    .line 92
    new-instance p1, Lcom/google/common/collect/MapMaker;

    invoke-direct {p1}, Lcom/google/common/collect/MapMaker;-><init>()V

    .line 93
    invoke-virtual {p1, p2}, Lcom/google/common/collect/MapMaker;->initialCapacity(I)Lcom/google/common/collect/MapMaker;

    const-wide/16 v0, 0x3c

    mul-long p3, p3, v0

    .line 94
    sget-object p2, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {p1, p3, p4, p2}, Lcom/google/common/collect/MapMaker;->expiration(JLjava/util/concurrent/TimeUnit;)Lcom/google/common/collect/MapMaker;

    .line 95
    invoke-virtual {p1, p5}, Lcom/google/common/collect/MapMaker;->concurrencyLevel(I)Lcom/google/common/collect/MapMaker;

    .line 96
    invoke-virtual {p1}, Lcom/google/common/collect/MapMaker;->softValues()Lcom/google/common/collect/MapMaker;

    .line 97
    invoke-virtual {p1}, Lcom/google/common/collect/MapMaker;->makeMap()Ljava/util/concurrent/ConcurrentMap;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    return-void
.end method

.method private cacheToDisk(Ljava/lang/Object;Ljava/lang/Object;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TKeyT;TValT;)V"
        }
    .end annotation

    .line 201
    new-instance v0, Ljava/io/File;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v2, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "/"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->getFileNameForKey(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 203
    :try_start_0
    invoke-virtual {v0}, Ljava/io/File;->createNewFile()Z

    .line 204
    invoke-virtual {v0}, Ljava/io/File;->deleteOnExit()V

    .line 206
    invoke-virtual {p0, v0, p2}, Lcom/github/droidfu/cachefu/AbstractCache;->writeValueToDisk(Ljava/io/File;Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 211
    invoke-virtual {p1}, Ljava/io/IOException;->printStackTrace()V

    goto :goto_0

    :catch_1
    move-exception p1

    .line 209
    invoke-virtual {p1}, Ljava/io/FileNotFoundException;->printStackTrace()V

    :goto_0
    return-void
.end method

.method private getFileForKey(Ljava/lang/Object;)Ljava/io/File;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TKeyT;)",
            "Ljava/io/File;"
        }
    .end annotation

    .line 216
    new-instance v0, Ljava/io/File;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v2, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "/"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->getFileNameForKey(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    return-object v0
.end method

.method private setRootDir(Ljava/lang/String;)V
    .locals 3

    .line 152
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/cachefu/"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->name:Ljava/lang/String;

    const-string v1, "\\s"

    const-string v2, ""

    invoke-virtual {p1, v1, v2}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/github/droidfu/support/StringSupport;->underscore(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public declared-synchronized clear()V
    .locals 4

    monitor-enter p0

    .line 361
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0}, Ljava/util/concurrent/ConcurrentMap;->clear()V

    .line 363
    iget-boolean v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    if-eqz v0, :cond_1

    .line 364
    new-instance v0, Ljava/io/File;

    iget-object v1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    invoke-direct {v0, v1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0}, Ljava/io/File;->listFiles()[Ljava/io/File;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    .line 366
    monitor-exit p0

    return-void

    .line 368
    :cond_0
    :try_start_1
    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    .line 369
    invoke-virtual {v3}, Ljava/io/File;->delete()Z
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 372
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 360
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized containsKey(Ljava/lang/Object;)Z
    .locals 1

    monitor-enter p0

    .line 286
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0, p1}, Ljava/util/concurrent/ConcurrentMap;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    iget-boolean v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    if-eqz v0, :cond_0

    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->getFileForKey(Ljava/lang/Object;)Ljava/io/File;

    move-result-object p1

    invoke-virtual {p1}, Ljava/io/File;->exists()Z

    move-result p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    monitor-exit p0

    return p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized containsKeyInMemory(Ljava/lang/Object;)Z
    .locals 1

    monitor-enter p0

    .line 297
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0, p1}, Ljava/util/concurrent/ConcurrentMap;->containsKey(Ljava/lang/Object;)Z

    move-result p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized containsValue(Ljava/lang/Object;)Z
    .locals 1

    monitor-enter p0

    .line 304
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0, p1}, Ljava/util/concurrent/ConcurrentMap;->containsValue(Ljava/lang/Object;)Z

    move-result p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public enableDiskCache(Landroid/content/Context;I)Z
    .locals 2

    .line 111
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    const/4 v0, 0x1

    if-ne p2, v0, :cond_0

    const-string p2, "mounted"

    .line 114
    invoke-static {}, Landroid/os/Environment;->getExternalStorageState()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_0

    .line 117
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {}, Landroid/os/Environment;->getExternalStorageDirectory()Ljava/io/File;

    move-result-object v0

    invoke-virtual {v0}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "/Android/data/"

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "/cache"

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    .line 120
    :cond_0
    invoke-virtual {p1}, Landroid/content/Context;->getCacheDir()Ljava/io/File;

    move-result-object p1

    if-nez p1, :cond_1

    const/4 p1, 0x0

    .line 123
    iput-boolean p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    return p1

    .line 125
    :cond_1
    invoke-virtual {p1}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object p1

    .line 128
    :goto_0
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->setRootDir(Ljava/lang/String;)V

    .line 130
    new-instance p1, Ljava/io/File;

    iget-object p2, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    invoke-direct {p1, p2}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 131
    invoke-virtual {p1}, Ljava/io/File;->mkdirs()Z

    move-result p2

    if-eqz p2, :cond_2

    .line 132
    new-instance p2, Ljava/io/File;

    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    const-string v1, ".nomedia"

    invoke-direct {p2, v0, v1}, Ljava/io/File;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 134
    :try_start_0
    invoke-virtual {p2}, Ljava/io/File;->createNewFile()Z
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    const-string p2, "Droid-Fu[CacheFu]"

    const-string v0, "Failed creating .nomedia file"

    .line 136
    invoke-static {p2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 140
    :cond_2
    :goto_1
    invoke-virtual {p1}, Ljava/io/File;->exists()Z

    move-result p1

    iput-boolean p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    .line 142
    iget-boolean p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    if-nez p1, :cond_3

    const-string p1, "Droid-Fu[CacheFu]"

    .line 143
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Failed creating disk cache directory "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_2

    .line 145
    :cond_3
    iget-object p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->name:Ljava/lang/String;

    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "enabled write through to "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 148
    :goto_2
    iget-boolean p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    return p1
.end method

.method public entrySet()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "Ljava/util/Map$Entry<",
            "TKeyT;TValT;>;>;"
        }
    .end annotation

    .line 331
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0}, Ljava/util/concurrent/ConcurrentMap;->entrySet()Ljava/util/Set;

    move-result-object v0

    return-object v0
.end method

.method public declared-synchronized get(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            ")TValT;"
        }
    .end annotation

    monitor-enter p0

    .line 230
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0, p1}, Ljava/util/concurrent/ConcurrentMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 233
    iget-object v1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->name:Ljava/lang/String;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "MEM cache hit for "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 234
    monitor-exit p0

    return-object v0

    .line 238
    :cond_0
    :try_start_1
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->getFileForKey(Ljava/lang/Object;)Ljava/io/File;

    move-result-object v0

    .line 239
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v1

    const/4 v2, 0x0

    if-eqz v1, :cond_2

    .line 241
    iget-object v1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->name:Ljava/lang/String;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "DISK cache hit for "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 243
    :try_start_2
    invoke-virtual {p0, v0}, Lcom/github/droidfu/cachefu/AbstractCache;->readValueFromDisk(Ljava/io/File;)Ljava/lang/Object;

    move-result-object v0
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    if-nez v0, :cond_1

    .line 250
    monitor-exit p0

    return-object v2

    .line 252
    :cond_1
    :try_start_3
    iget-object v1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v1, p1, v0}, Ljava/util/concurrent/ConcurrentMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 253
    monitor-exit p0

    return-object v0

    :catch_0
    move-exception p1

    .line 246
    :try_start_4
    invoke-virtual {p1}, Ljava/io/IOException;->printStackTrace()V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    .line 247
    monitor-exit p0

    return-object v2

    .line 257
    :cond_2
    monitor-exit p0

    return-object v2

    :catchall_0
    move-exception p1

    .line 228
    monitor-exit p0

    throw p1
.end method

.method public getDiskCacheDirectory()Ljava/lang/String;
    .locals 1

    .line 163
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->diskCacheDirectory:Ljava/lang/String;

    return-object v0
.end method

.method public abstract getFileNameForKey(Ljava/lang/Object;)Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TKeyT;)",
            "Ljava/lang/String;"
        }
    .end annotation
.end method

.method public isDiskCacheEnabled()Z
    .locals 1

    .line 343
    iget-boolean v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    return v0
.end method

.method public declared-synchronized isEmpty()Z
    .locals 1

    monitor-enter p0

    .line 339
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0}, Ljava/util/concurrent/ConcurrentMap;->isEmpty()Z

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public keySet()Ljava/util/Set;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Set<",
            "TKeyT;>;"
        }
    .end annotation

    .line 327
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0}, Ljava/util/concurrent/ConcurrentMap;->keySet()Ljava/util/Set;

    move-result-object v0

    return-object v0
.end method

.method public declared-synchronized put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TKeyT;TValT;)TValT;"
        }
    .end annotation

    monitor-enter p0

    .line 265
    :try_start_0
    iget-boolean v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    if-eqz v0, :cond_0

    .line 266
    invoke-direct {p0, p1, p2}, Lcom/github/droidfu/cachefu/AbstractCache;->cacheToDisk(Ljava/lang/Object;Ljava/lang/Object;)V

    .line 269
    :cond_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0, p1, p2}, Ljava/util/concurrent/ConcurrentMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 264
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized putAll(Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "+TKeyT;+TValT;>;)V"
        }
    .end annotation

    monitor-enter p0

    .line 273
    :try_start_0
    new-instance p1, Ljava/lang/UnsupportedOperationException;

    invoke-direct {p1}, Ljava/lang/UnsupportedOperationException;-><init>()V

    throw p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method protected abstract readValueFromDisk(Ljava/io/File;)Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/io/File;",
            ")TValT;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method

.method public declared-synchronized remove(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            ")TValT;"
        }
    .end annotation

    monitor-enter p0

    .line 309
    :try_start_0
    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->removeKey(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .line 311
    iget-boolean v1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    if-eqz v1, :cond_0

    .line 312
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->getFileForKey(Ljava/lang/Object;)Ljava/io/File;

    move-result-object p1

    .line 313
    invoke-virtual {p1}, Ljava/io/File;->exists()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 314
    invoke-virtual {p1}, Ljava/io/File;->delete()Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 318
    :cond_0
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    .line 308
    monitor-exit p0

    throw p1
.end method

.method public removeKey(Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            ")TValT;"
        }
    .end annotation

    .line 323
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0, p1}, Ljava/util/concurrent/ConcurrentMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public setDiskCacheEnabled(Ljava/lang/String;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 352
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    .line 353
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/AbstractCache;->setRootDir(Ljava/lang/String;)V

    const/4 p1, 0x1

    .line 354
    iput-boolean p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    .line 356
    iput-boolean p1, p0, Lcom/github/droidfu/cachefu/AbstractCache;->isDiskCacheEnabled:Z

    :goto_0
    return-void
.end method

.method public declared-synchronized size()I
    .locals 1

    monitor-enter p0

    .line 335
    :try_start_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0}, Ljava/util/concurrent/ConcurrentMap;->size()I

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public values()Ljava/util/Collection;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Collection<",
            "TValT;>;"
        }
    .end annotation

    .line 375
    iget-object v0, p0, Lcom/github/droidfu/cachefu/AbstractCache;->cache:Ljava/util/concurrent/ConcurrentMap;

    invoke-interface {v0}, Ljava/util/concurrent/ConcurrentMap;->values()Ljava/util/Collection;

    move-result-object v0

    return-object v0
.end method

.method protected abstract writeValueToDisk(Ljava/io/File;Ljava/lang/Object;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/io/File;",
            "TValT;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method
