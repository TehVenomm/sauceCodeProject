.class public Lnet/gogame/gowrap/support/DefaultDownloadManager;
.super Ljava/lang/Object;
.source "DefaultDownloadManager.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadManager;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/support/DefaultDownloadManager$DefaultDownloadResult;
    }
.end annotation


# instance fields
.field private final context:Landroid/content/Context;

.field private final diskLruCache:Lnet/gogame/gowrap/support/DiskLruCache;

.field private downloadCount:I

.field private final handler:Landroid/os/Handler;

.field private final listeners:Ljava/util/Set;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Set<",
            "Lnet/gogame/gowrap/support/DownloadManager$Listener;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;Lnet/gogame/gowrap/support/DiskLruCache;)V
    .locals 1

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 24
    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0}, Landroid/os/Handler;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->handler:Landroid/os/Handler;

    .line 25
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->listeners:Ljava/util/Set;

    const/4 v0, 0x0

    .line 26
    iput v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    .line 31
    iput-object p1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->context:Landroid/content/Context;

    .line 32
    iput-object p2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->diskLruCache:Lnet/gogame/gowrap/support/DiskLruCache;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/support/DefaultDownloadManager;)V
    .locals 0

    .line 20
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->fireDownloadsFinished()V

    return-void
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Lnet/gogame/gowrap/support/DiskLruCache;
    .locals 0

    .line 20
    iget-object p0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->diskLruCache:Lnet/gogame/gowrap/support/DiskLruCache;

    return-object p0
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/support/DefaultDownloadManager;)V
    .locals 0

    .line 20
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->onDownloadFinished()V

    return-void
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Landroid/os/Handler;
    .locals 0

    .line 20
    iget-object p0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->handler:Landroid/os/Handler;

    return-object p0
.end method

.method static synthetic access$400(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Landroid/content/Context;
    .locals 0

    .line 20
    iget-object p0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->context:Landroid/content/Context;

    return-object p0
.end method

.method private static computeDigest(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/security/NoSuchAlgorithmException;,
            Ljava/io/UnsupportedEncodingException;
        }
    .end annotation

    .line 46
    invoke-static {p1}, Ljava/security/MessageDigest;->getInstance(Ljava/lang/String;)Ljava/security/MessageDigest;

    move-result-object p1

    const-string v0, "UTF-8"

    .line 47
    invoke-virtual {p0, v0}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object p0

    invoke-virtual {p1, p0}, Ljava/security/MessageDigest;->update([B)V

    .line 48
    invoke-virtual {p1}, Ljava/security/MessageDigest;->digest()[B

    move-result-object p0

    .line 49
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 50
    :goto_0
    array-length v2, p0

    if-ge v1, v2, :cond_0

    const-string v2, "%02x"

    const/4 v3, 0x1

    .line 51
    new-array v3, v3, [Ljava/lang/Object;

    aget-byte v4, p0, v1

    and-int/lit16 v4, v4, 0xff

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v3, v0

    invoke-static {v2, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 53
    :cond_0
    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private fireDownloadsFinished()V
    .locals 4

    .line 214
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->listeners:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/support/DownloadManager$Listener;

    .line 216
    :try_start_0
    invoke-interface {v1}, Lnet/gogame/gowrap/support/DownloadManager$Listener;->onDownloadsFinished()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 218
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method private fireDownloadsStarted()V
    .locals 4

    .line 204
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->listeners:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/support/DownloadManager$Listener;

    .line 206
    :try_start_0
    invoke-interface {v1}, Lnet/gogame/gowrap/support/DownloadManager$Listener;->onDownloadsStarted()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 208
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :cond_0
    return-void
.end method

.method private declared-synchronized onDownloadFinished()V
    .locals 2

    monitor-enter p0

    .line 62
    :try_start_0
    iget v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    add-int/lit8 v0, v0, -0x1

    iput v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    .line 63
    iget v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    if-gez v0, :cond_0

    const/4 v0, 0x0

    .line 64
    iput v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    .line 66
    :cond_0
    iget v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    if-nez v0, :cond_1

    .line 67
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->handler:Landroid/os/Handler;

    new-instance v1, Lnet/gogame/gowrap/support/DefaultDownloadManager$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/support/DefaultDownloadManager$1;-><init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 75
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 61
    monitor-exit p0

    throw v0
.end method

.method private declared-synchronized onDownloadStarted()V
    .locals 1

    monitor-enter p0

    .line 57
    :try_start_0
    iget v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    .line 58
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->fireDownloadsStarted()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 59
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 56
    monitor-exit p0

    throw v0
.end method

.method private static toKey(Landroid/net/Uri;)Ljava/lang/String;
    .locals 3

    .line 37
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v1

    const-string v2, "SHA-1"

    invoke-static {v1, v2}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->computeDigest(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 38
    invoke-virtual {p0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object p0

    const-string v1, "MD5"

    invoke-static {p0, v1}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->computeDigest(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    .line 40
    new-instance v0, Ljava/lang/RuntimeException;

    invoke-direct {v0, p0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/Throwable;)V

    throw v0
.end method


# virtual methods
.method public addListener(Lnet/gogame/gowrap/support/DownloadManager$Listener;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 192
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->listeners:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    return-void
.end method

.method public download(Lnet/gogame/gowrap/support/DownloadManager$Request;)V
    .locals 7

    .line 79
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getUri()Landroid/net/Uri;

    move-result-object v0

    if-eqz v0, :cond_3

    invoke-virtual {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getTarget()Lnet/gogame/gowrap/support/DownloadManager$Target;

    move-result-object v0

    if-nez v0, :cond_0

    goto :goto_2

    .line 82
    :cond_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getTarget()Lnet/gogame/gowrap/support/DownloadManager$Target;

    move-result-object v0

    .line 84
    :try_start_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getUri()Landroid/net/Uri;

    move-result-object v1

    invoke-static {v1}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->toKey(Landroid/net/Uri;)Ljava/lang/String;

    move-result-object v1

    .line 85
    iget-object v2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->diskLruCache:Lnet/gogame/gowrap/support/DiskLruCache;

    invoke-virtual {v2, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->get(Ljava/lang/String;)Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 88
    invoke-virtual {v2}, Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;->close()V

    .line 89
    iget-object p1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->handler:Landroid/os/Handler;

    new-instance v2, Lnet/gogame/gowrap/support/DefaultDownloadManager$2;

    invoke-direct {v2, p0, v0, v1}, Lnet/gogame/gowrap/support/DefaultDownloadManager$2;-><init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;Lnet/gogame/gowrap/support/DownloadManager$Target;Ljava/lang/String;)V

    invoke-virtual {p1, v2}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void

    .line 102
    :cond_1
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getPlaceholderResourceId()Ljava/lang/Integer;

    move-result-object v2

    if-eqz v2, :cond_2

    .line 103
    iget-object v2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->context:Landroid/content/Context;

    invoke-virtual {v2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    .line 104
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getPlaceholderResourceId()Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Integer;->intValue()I

    move-result v3

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v2

    .line 103
    invoke-interface {v0, v2}, Lnet/gogame/gowrap/support/DownloadManager$Target;->onDownloadStarted(Landroid/graphics/drawable/Drawable;)V

    goto :goto_0

    :cond_2
    const/4 v2, 0x0

    .line 106
    invoke-interface {v0, v2}, Lnet/gogame/gowrap/support/DownloadManager$Target;->onDownloadStarted(Landroid/graphics/drawable/Drawable;)V

    .line 108
    :goto_0
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->onDownloadStarted()V

    .line 109
    iget-object v2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->context:Landroid/content/Context;

    new-instance v3, Ljava/net/URL;

    invoke-virtual {p1}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getUri()Landroid/net/Uri;

    move-result-object v4

    invoke-virtual {v4}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-direct {v3, v4}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    new-instance v4, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;

    invoke-direct {v4, p0, v1}, Lnet/gogame/gowrap/support/DefaultDownloadManager$3;-><init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;Ljava/lang/String;)V

    const/4 v5, 0x0

    new-instance v6, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;

    invoke-direct {v6, p0, v0, v1, p1}, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;-><init>(Lnet/gogame/gowrap/support/DefaultDownloadManager;Lnet/gogame/gowrap/support/DownloadManager$Target;Ljava/lang/String;Lnet/gogame/gowrap/support/DownloadManager$Request;)V

    invoke-static {v2, v3, v4, v5, v6}, Lnet/gogame/gowrap/support/DownloadUtils;->download(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 178
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_1
    return-void

    :cond_3
    :goto_2
    return-void
.end method

.method public isDownloading()Z
    .locals 1

    .line 184
    iget v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->downloadCount:I

    if-lez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public removeListener(Lnet/gogame/gowrap/support/DownloadManager$Listener;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 200
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager;->listeners:Ljava/util/Set;

    invoke-interface {v0, p1}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z

    return-void
.end method
