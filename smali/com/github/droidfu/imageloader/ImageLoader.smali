.class public Lcom/github/droidfu/imageloader/ImageLoader;
.super Ljava/lang/Object;
.source "ImageLoader.java"

# interfaces
.implements Ljava/lang/Runnable;


# static fields
.field public static final BITMAP_EXTRA:Ljava/lang/String; = "droidfu:extra_bitmap"

.field private static final DEFAULT_NUM_RETRIES:I = 0x3

.field private static final DEFAULT_POOL_SIZE:I = 0x3

.field private static final DEFAULT_RETRY_HANDLER_SLEEP_TIME:I = 0x3e8

.field private static final DEFAULT_TTL_MINUTES:I = 0x5a0

.field public static final HANDLER_MESSAGE_ID:I = 0x0

.field public static final IMAGE_URL_EXTRA:Ljava/lang/String; = "droidfu:extra_image_url"

.field private static final LOG_TAG:Ljava/lang/String; = "Droid-Fu/ImageLoader"

.field private static executor:Ljava/util/concurrent/ThreadPoolExecutor; = null

.field private static imageCache:Lcom/github/droidfu/cachefu/ImageCache; = null

.field private static numRetries:I = 0x3


# instance fields
.field private handler:Lcom/github/droidfu/imageloader/ImageLoaderHandler;

.field private imageUrl:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;Lcom/github/droidfu/imageloader/ImageLoaderHandler;)V
    .locals 0

    .line 106
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 107
    iput-object p1, p0, Lcom/github/droidfu/imageloader/ImageLoader;->imageUrl:Ljava/lang/String;

    .line 108
    iput-object p2, p0, Lcom/github/droidfu/imageloader/ImageLoader;->handler:Lcom/github/droidfu/imageloader/ImageLoaderHandler;

    return-void
.end method

.method public static clearCache()V
    .locals 1

    .line 218
    sget-object v0, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    invoke-virtual {v0}, Lcom/github/droidfu/cachefu/ImageCache;->clear()V

    return-void
.end method

.method public static getImageCache()Lcom/github/droidfu/cachefu/ImageCache;
    .locals 1

    .line 227
    sget-object v0, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    return-object v0
.end method

.method public static declared-synchronized initialize(Landroid/content/Context;)V
    .locals 6

    const-class v0, Lcom/github/droidfu/imageloader/ImageLoader;

    monitor-enter v0

    .line 93
    :try_start_0
    sget-object v1, Lcom/github/droidfu/imageloader/ImageLoader;->executor:Ljava/util/concurrent/ThreadPoolExecutor;

    const/4 v2, 0x3

    if-nez v1, :cond_0

    .line 94
    invoke-static {v2}, Ljava/util/concurrent/Executors;->newFixedThreadPool(I)Ljava/util/concurrent/ExecutorService;

    move-result-object v1

    check-cast v1, Ljava/util/concurrent/ThreadPoolExecutor;

    sput-object v1, Lcom/github/droidfu/imageloader/ImageLoader;->executor:Ljava/util/concurrent/ThreadPoolExecutor;

    .line 96
    :cond_0
    sget-object v1, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    if-nez v1, :cond_1

    .line 97
    new-instance v1, Lcom/github/droidfu/cachefu/ImageCache;

    const/16 v3, 0x19

    const-wide/16 v4, 0x5a0

    invoke-direct {v1, v3, v4, v5, v2}, Lcom/github/droidfu/cachefu/ImageCache;-><init>(IJI)V

    sput-object v1, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    .line 98
    sget-object v1, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    const/4 v2, 0x1

    invoke-virtual {v1, p0, v2}, Lcom/github/droidfu/cachefu/ImageCache;->enableDiskCache(Landroid/content/Context;I)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 100
    :cond_1
    monitor-exit v0

    return-void

    :catchall_0
    move-exception p0

    .line 92
    monitor-exit v0

    throw p0
.end method

.method public static setMaxDownloadAttempts(I)V
    .locals 0

    .line 80
    sput p0, Lcom/github/droidfu/imageloader/ImageLoader;->numRetries:I

    return-void
.end method

.method public static setThreadPoolSize(I)V
    .locals 1

    .line 71
    sget-object v0, Lcom/github/droidfu/imageloader/ImageLoader;->executor:Ljava/util/concurrent/ThreadPoolExecutor;

    invoke-virtual {v0, p0}, Ljava/util/concurrent/ThreadPoolExecutor;->setMaximumPoolSize(I)V

    return-void
.end method

.method public static start(Ljava/lang/String;Landroid/widget/ImageView;)V
    .locals 2

    .line 122
    new-instance v0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;

    invoke-direct {v0, p1, p0}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;-><init>(Landroid/widget/ImageView;Ljava/lang/String;)V

    const/4 v1, 0x0

    invoke-static {p0, p1, v0, v1, v1}, Lcom/github/droidfu/imageloader/ImageLoader;->start(Ljava/lang/String;Landroid/widget/ImageView;Lcom/github/droidfu/imageloader/ImageLoaderHandler;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public static start(Ljava/lang/String;Landroid/widget/ImageView;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 142
    new-instance v0, Lcom/github/droidfu/imageloader/ImageLoaderHandler;

    invoke-direct {v0, p1, p0, p3}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;-><init>(Landroid/widget/ImageView;Ljava/lang/String;Landroid/graphics/drawable/Drawable;)V

    invoke-static {p0, p1, v0, p2, p3}, Lcom/github/droidfu/imageloader/ImageLoader;->start(Ljava/lang/String;Landroid/widget/ImageView;Lcom/github/droidfu/imageloader/ImageLoaderHandler;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method private static start(Ljava/lang/String;Landroid/widget/ImageView;Lcom/github/droidfu/imageloader/ImageLoaderHandler;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V
    .locals 1

    const/4 p4, 0x0

    if-eqz p1, :cond_2

    if-nez p0, :cond_0

    .line 190
    invoke-virtual {p1, p4}, Landroid/widget/ImageView;->setTag(Ljava/lang/Object;)V

    .line 191
    invoke-virtual {p1, p3}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    return-void

    .line 194
    :cond_0
    invoke-virtual {p1}, Landroid/widget/ImageView;->getTag()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 195
    invoke-virtual {p0, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    return-void

    .line 200
    :cond_1
    invoke-virtual {p1, p3}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 201
    invoke-virtual {p1, p0}, Landroid/widget/ImageView;->setTag(Ljava/lang/Object;)V

    .line 205
    :cond_2
    sget-object p1, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    invoke-virtual {p1, p0}, Lcom/github/droidfu/cachefu/ImageCache;->containsKeyInMemory(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_3

    .line 207
    sget-object p1, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    invoke-virtual {p1, p0}, Lcom/github/droidfu/cachefu/ImageCache;->getBitmap(Ljava/lang/Object;)Landroid/graphics/Bitmap;

    move-result-object p0

    invoke-virtual {p2, p0, p4}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->handleImageLoaded(Landroid/graphics/Bitmap;Landroid/os/Message;)Z

    goto :goto_0

    .line 209
    :cond_3
    sget-object p1, Lcom/github/droidfu/imageloader/ImageLoader;->executor:Ljava/util/concurrent/ThreadPoolExecutor;

    new-instance p3, Lcom/github/droidfu/imageloader/ImageLoader;

    invoke-direct {p3, p0, p2}, Lcom/github/droidfu/imageloader/ImageLoader;-><init>(Ljava/lang/String;Lcom/github/droidfu/imageloader/ImageLoaderHandler;)V

    invoke-virtual {p1, p3}, Ljava/util/concurrent/ThreadPoolExecutor;->execute(Ljava/lang/Runnable;)V

    :goto_0
    return-void
.end method

.method public static start(Ljava/lang/String;Lcom/github/droidfu/imageloader/ImageLoaderHandler;)V
    .locals 2

    .line 160
    invoke-virtual {p1}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->getImageView()Landroid/widget/ImageView;

    move-result-object v0

    const/4 v1, 0x0

    invoke-static {p0, v0, p1, v1, v1}, Lcom/github/droidfu/imageloader/ImageLoader;->start(Ljava/lang/String;Landroid/widget/ImageView;Lcom/github/droidfu/imageloader/ImageLoaderHandler;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method public static start(Ljava/lang/String;Lcom/github/droidfu/imageloader/ImageLoaderHandler;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V
    .locals 1

    .line 181
    invoke-virtual {p1}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->getImageView()Landroid/widget/ImageView;

    move-result-object v0

    invoke-static {p0, v0, p1, p2, p3}, Lcom/github/droidfu/imageloader/ImageLoader;->start(Ljava/lang/String;Landroid/widget/ImageView;Lcom/github/droidfu/imageloader/ImageLoaderHandler;Landroid/graphics/drawable/Drawable;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method


# virtual methods
.method protected downloadImage()Landroid/graphics/Bitmap;
    .locals 5

    const/4 v0, 0x1

    .line 253
    :goto_0
    sget v1, Lcom/github/droidfu/imageloader/ImageLoader;->numRetries:I

    if-gt v0, v1, :cond_0

    .line 255
    :try_start_0
    invoke-virtual {p0}, Lcom/github/droidfu/imageloader/ImageLoader;->retrieveImageData()[B

    move-result-object v1

    if-eqz v1, :cond_0

    .line 258
    sget-object v2, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    iget-object v3, p0, Lcom/github/droidfu/imageloader/ImageLoader;->imageUrl:Ljava/lang/String;

    invoke-virtual {v2, v3, v1}, Lcom/github/droidfu/cachefu/ImageCache;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const/4 v2, 0x0

    .line 263
    array-length v3, v1

    invoke-static {v1, v2, v3}, Landroid/graphics/BitmapFactory;->decodeByteArray([BII)Landroid/graphics/Bitmap;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-object v1

    :catch_0
    move-exception v1

    const-string v2, "Droid-Fu/ImageLoader"

    .line 266
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "download for "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v4, p0, Lcom/github/droidfu/imageloader/ImageLoader;->imageUrl:Ljava/lang/String;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, " failed (attempt "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v4, ")"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 267
    invoke-virtual {v1}, Ljava/lang/Throwable;->printStackTrace()V

    const-wide/16 v1, 0x3e8

    .line 268
    invoke-static {v1, v2}, Landroid/os/SystemClock;->sleep(J)V

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public notifyImageLoaded(Ljava/lang/String;Landroid/graphics/Bitmap;)V
    .locals 3

    .line 305
    new-instance v0, Landroid/os/Message;

    invoke-direct {v0}, Landroid/os/Message;-><init>()V

    const/4 v1, 0x0

    .line 306
    iput v1, v0, Landroid/os/Message;->what:I

    .line 307
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "droidfu:extra_image_url"

    .line 308
    invoke-virtual {v1, v2, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "droidfu:extra_bitmap"

    .line 310
    invoke-virtual {v1, p1, p2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 311
    invoke-virtual {v0, v1}, Landroid/os/Message;->setData(Landroid/os/Bundle;)V

    .line 313
    iget-object p1, p0, Lcom/github/droidfu/imageloader/ImageLoader;->handler:Lcom/github/droidfu/imageloader/ImageLoaderHandler;

    invoke-virtual {p1, v0}, Lcom/github/droidfu/imageloader/ImageLoaderHandler;->sendMessage(Landroid/os/Message;)Z

    return-void
.end method

.method protected retrieveImageData()[B
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 277
    new-instance v0, Ljava/net/URL;

    iget-object v1, p0, Lcom/github/droidfu/imageloader/ImageLoader;->imageUrl:Ljava/lang/String;

    invoke-direct {v0, v1}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 278
    invoke-virtual {v0}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v0

    check-cast v0, Ljava/net/HttpURLConnection;

    .line 281
    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->getContentLength()I

    move-result v1

    if-gez v1, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 285
    :cond_0
    new-array v2, v1, [B

    const-string v3, "Droid-Fu/ImageLoader"

    .line 288
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "fetching image "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v5, p0, Lcom/github/droidfu/imageloader/ImageLoader;->imageUrl:Ljava/lang/String;

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, " ("

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v5, ")"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 289
    new-instance v3, Ljava/io/BufferedInputStream;

    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v4

    invoke-direct {v3, v4}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    const/4 v4, 0x0

    const/4 v5, 0x0

    :goto_0
    const/4 v6, -0x1

    if-eq v4, v6, :cond_1

    if-ge v5, v1, :cond_1

    sub-int v4, v1, v5

    .line 293
    invoke-virtual {v3, v2, v5, v4}, Ljava/io/BufferedInputStream;->read([BII)I

    move-result v4

    add-int/2addr v5, v4

    goto :goto_0

    .line 298
    :cond_1
    invoke-virtual {v3}, Ljava/io/BufferedInputStream;->close()V

    .line 299
    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->disconnect()V

    return-object v2
.end method

.method public run()V
    .locals 2

    .line 237
    sget-object v0, Lcom/github/droidfu/imageloader/ImageLoader;->imageCache:Lcom/github/droidfu/cachefu/ImageCache;

    iget-object v1, p0, Lcom/github/droidfu/imageloader/ImageLoader;->imageUrl:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lcom/github/droidfu/cachefu/ImageCache;->getBitmap(Ljava/lang/Object;)Landroid/graphics/Bitmap;

    move-result-object v0

    if-nez v0, :cond_0

    .line 240
    invoke-virtual {p0}, Lcom/github/droidfu/imageloader/ImageLoader;->downloadImage()Landroid/graphics/Bitmap;

    move-result-object v0

    .line 244
    :cond_0
    iget-object v1, p0, Lcom/github/droidfu/imageloader/ImageLoader;->imageUrl:Ljava/lang/String;

    invoke-virtual {p0, v1, v0}, Lcom/github/droidfu/imageloader/ImageLoader;->notifyImageLoaded(Ljava/lang/String;Landroid/graphics/Bitmap;)V

    return-void
.end method
