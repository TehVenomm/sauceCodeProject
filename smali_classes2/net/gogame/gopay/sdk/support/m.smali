.class public final Lnet/gogame/gopay/sdk/support/m;
.super Ljava/lang/Object;


# static fields
.field public static final a:[Ljava/lang/String;

.field private static b:Ljava/lang/String;

.field private static final c:I

.field private static final d:I

.field private static e:Ljava/util/ArrayList;

.field private static final f:Landroid/util/LruCache;

.field private static g:I

.field private static h:I


# direct methods
.method static constructor <clinit>()V
    .locals 7

    const-string v0, "ui/ic_tick.png"

    const-string v1, "ui/ic_arrow_dwn.png"

    const-string v2, "ui/ic_arrow_dwn_grey.png"

    const-string v3, "ui/separator.png"

    const-string v4, "ui/ic_close.png"

    const-string v5, "ui/ic_qest_white.png"

    const-string v6, "ui/gopay_logo.png"

    filled-new-array/range {v0 .. v6}, [Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    invoke-static {}, Ljava/lang/Runtime;->getRuntime()Ljava/lang/Runtime;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Runtime;->maxMemory()J

    move-result-wide v0

    const-wide/16 v2, 0x400

    div-long/2addr v0, v2

    long-to-int v0, v0

    sput v0, Lnet/gogame/gopay/sdk/support/m;->c:I

    div-int/lit8 v0, v0, 0x8

    sput v0, Lnet/gogame/gopay/sdk/support/m;->d:I

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    sput-object v0, Lnet/gogame/gopay/sdk/support/m;->e:Ljava/util/ArrayList;

    new-instance v0, Lnet/gogame/gopay/sdk/support/n;

    sget v1, Lnet/gogame/gopay/sdk/support/m;->d:I

    invoke-direct {v0, v1}, Lnet/gogame/gopay/sdk/support/n;-><init>(I)V

    sput-object v0, Lnet/gogame/gopay/sdk/support/m;->f:Landroid/util/LruCache;

    const/4 v0, 0x0

    sput v0, Lnet/gogame/gopay/sdk/support/m;->g:I

    sput v0, Lnet/gogame/gopay/sdk/support/m;->h:I

    return-void
.end method

.method static synthetic a(Ljava/lang/String;Ljava/lang/String;)Landroid/graphics/Bitmap;
    .locals 0

    invoke-static {p0, p1}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object p0

    return-object p0
.end method

.method private static a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)Landroid/graphics/Bitmap;
    .locals 3
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    if-eqz p2, :cond_3

    invoke-virtual {p2}, Ljava/lang/String;->length()I

    move-result v0

    if-nez v0, :cond_0

    goto :goto_1

    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->f:Landroid/util/LruCache;

    invoke-virtual {v0, p1}, Landroid/util/LruCache;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/graphics/Bitmap;

    if-nez v0, :cond_1

    sget-object v1, Lnet/gogame/gopay/sdk/support/m;->e:Ljava/util/ArrayList;

    new-instance v2, Lnet/gogame/gopay/sdk/support/p;

    invoke-direct {v2, p1, p0, p2, p3}, Lnet/gogame/gopay/sdk/support/p;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V

    const/4 p0, 0x0

    new-array p0, p0, [Ljava/lang/Void;

    invoke-virtual {v2, p0}, Lnet/gogame/gopay/sdk/support/p;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    if-eqz p3, :cond_2

    invoke-interface {p3, v0}, Lnet/gogame/gopay/sdk/support/q;->a(Landroid/graphics/Bitmap;)V

    :cond_2
    :goto_0
    return-object v0

    :cond_3
    :goto_1
    const/4 p0, 0x0

    if-eqz p3, :cond_4

    invoke-interface {p3, p0}, Lnet/gogame/gopay/sdk/support/q;->a(Landroid/graphics/Bitmap;)V

    :cond_4
    return-object p0
.end method

.method public static a(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/support/m;->b:Ljava/lang/String;

    return-void
.end method

.method public static a(Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lnet/gogame/gopay/sdk/support/q;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "flags/"

    invoke-static {p0, v0, p1, p2}, Lnet/gogame/gopay/sdk/support/m;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)Landroid/graphics/Bitmap;

    return-void
.end method

.method public static varargs a(Lnet/gogame/gopay/sdk/support/r;Ljava/lang/String;[Ljava/lang/String;)V
    .locals 8
    .param p0    # Lnet/gogame/gopay/sdk/support/r;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # [Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    if-eqz p2, :cond_4

    array-length v0, p2

    if-nez v0, :cond_0

    goto :goto_2

    :cond_0
    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->e:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    const/4 v2, 0x1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/os/AsyncTask;

    invoke-virtual {v1}, Landroid/os/AsyncTask;->getStatus()Landroid/os/AsyncTask$Status;

    move-result-object v3

    sget-object v4, Landroid/os/AsyncTask$Status;->FINISHED:Landroid/os/AsyncTask$Status;

    if-eq v3, v4, :cond_1

    invoke-virtual {v1, v2}, Landroid/os/AsyncTask;->cancel(Z)Z

    goto :goto_0

    :cond_2
    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->e:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    const/4 v0, 0x0

    sput v0, Lnet/gogame/gopay/sdk/support/m;->g:I

    sput v0, Lnet/gogame/gopay/sdk/support/m;->h:I

    array-length v1, p2

    new-instance v3, Lnet/gogame/gopay/sdk/support/o;

    invoke-direct {v3, v1, p0}, Lnet/gogame/gopay/sdk/support/o;-><init>(ILnet/gogame/gopay/sdk/support/r;)V

    const/4 p0, 0x0

    :goto_1
    if-ge p0, v1, :cond_3

    aget-object v4, p2, p0

    const-string v5, "/"

    invoke-virtual {v4, v5}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v4

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v5, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v6, "ui/"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    aget-object v7, v4, v0

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v7, "/"

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    aget-object v4, v4, v2

    invoke-static {v5, v6, v4, v3}, Lnet/gogame/gopay/sdk/support/m;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)Landroid/graphics/Bitmap;

    add-int/lit8 p0, p0, 0x1

    goto :goto_1

    :cond_3
    return-void

    :cond_4
    :goto_2
    invoke-interface {p0}, Lnet/gogame/gopay/sdk/support/r;->a()V

    return-void
.end method

.method public static a()Z
    .locals 3

    new-instance v0, Ljava/io/File;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v2, Lnet/gogame/gopay/sdk/support/m;->b:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "/assets"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result v0

    return v0
.end method

.method private static b(Ljava/lang/String;)Landroid/graphics/Bitmap;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->f:Landroid/util/LruCache;

    invoke-virtual {v0, p0}, Landroid/util/LruCache;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/graphics/Bitmap;

    return-object p0
.end method

.method private static b(Ljava/lang/String;Ljava/lang/String;)Landroid/graphics/Bitmap;
    .locals 2
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    :try_start_0
    new-instance v0, Ljava/net/URI;

    invoke-direct {v0, p0}, Ljava/net/URI;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, p1}, Ljava/net/URI;->resolve(Ljava/lang/String;)Ljava/net/URI;

    move-result-object v0

    invoke-virtual {v0}, Ljava/net/URI;->toString()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/net/URL;

    invoke-direct {v1, v0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openStream()Ljava/io/InputStream;

    move-result-object v0

    invoke-static {v0}, Landroid/graphics/BitmapFactory;->decodeStream(Ljava/io/InputStream;)Landroid/graphics/Bitmap;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "Bad Request: "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/4 p0, 0x0

    return-object p0
.end method

.method public static b()V
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->f:Landroid/util/LruCache;

    invoke-virtual {v0}, Landroid/util/LruCache;->evictAll()V

    return-void
.end method

.method public static b(Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lnet/gogame/gopay/sdk/support/q;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "pm/"

    invoke-static {p0, v0, p1, p2}, Lnet/gogame/gopay/sdk/support/m;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)Landroid/graphics/Bitmap;

    return-void
.end method

.method public static c()Landroid/graphics/Bitmap;
    .locals 2

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    const/4 v1, 0x0

    aget-object v0, v0, v1

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v0

    return-object v0
.end method

.method public static c(Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)V
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lnet/gogame/gopay/sdk/support/q;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "cat/"

    invoke-static {p0, v0, p1, p2}, Lnet/gogame/gopay/sdk/support/m;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gopay/sdk/support/q;)Landroid/graphics/Bitmap;

    return-void
.end method

.method public static d()Landroid/graphics/Bitmap;
    .locals 2

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    const/4 v1, 0x1

    aget-object v0, v0, v1

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v0

    return-object v0
.end method

.method public static e()Landroid/graphics/Bitmap;
    .locals 2

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    const/4 v1, 0x2

    aget-object v0, v0, v1

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v0

    return-object v0
.end method

.method public static f()Landroid/graphics/Bitmap;
    .locals 2

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    const/4 v1, 0x3

    aget-object v0, v0, v1

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v0

    return-object v0
.end method

.method public static g()Landroid/graphics/Bitmap;
    .locals 2

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    const/4 v1, 0x4

    aget-object v0, v0, v1

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v0

    return-object v0
.end method

.method public static h()Landroid/graphics/Bitmap;
    .locals 2

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    const/4 v1, 0x5

    aget-object v0, v0, v1

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v0

    return-object v0
.end method

.method public static i()Landroid/graphics/Bitmap;
    .locals 2

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    const/4 v1, 0x6

    aget-object v0, v0, v1

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/m;->b(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v0

    return-object v0
.end method

.method public static j()Ljava/lang/String;
    .locals 3

    :try_start_0
    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->b:Ljava/lang/String;

    new-instance v1, Ljava/io/File;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "/assets/error.html"

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {v1, v0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v0

    if-eqz v0, :cond_0

    new-instance v0, Ljava/io/FileInputStream;

    invoke-direct {v0, v1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    const-string v1, "UTF-8"

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/support/IOUtils;->readString(Ljava/io/InputStream;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    :cond_0
    const/4 v0, 0x0

    return-object v0
.end method

.method static synthetic k()I
    .locals 2

    sget v0, Lnet/gogame/gopay/sdk/support/m;->h:I

    add-int/lit8 v1, v0, 0x1

    sput v1, Lnet/gogame/gopay/sdk/support/m;->h:I

    return v0
.end method

.method static synthetic l()I
    .locals 1

    sget v0, Lnet/gogame/gopay/sdk/support/m;->g:I

    add-int/lit8 v0, v0, 0x1

    sput v0, Lnet/gogame/gopay/sdk/support/m;->g:I

    return v0
.end method

.method static synthetic m()Ljava/util/ArrayList;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->e:Ljava/util/ArrayList;

    return-object v0
.end method

.method static synthetic n()I
    .locals 1

    sget v0, Lnet/gogame/gopay/sdk/support/m;->h:I

    return v0
.end method

.method static synthetic o()Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->b:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic p()Landroid/util/LruCache;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/support/m;->f:Landroid/util/LruCache;

    return-object v0
.end method
