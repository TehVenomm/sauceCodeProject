.class public Lcom/zopim/android/sdk/data/WebWidgetListener;
.super Landroid/webkit/WebViewClient;


# static fields
.field private static final DEBUG:Z = false

.field private static final DELIMITER:Ljava/lang/String; = "z://"

.field private static final ENCODING:Ljava/lang/String; = "utf-8"

.field private static final LOG_TAG:Ljava/lang/String; = "WebWidgetListener"

.field private static final MY_EXECUTOR:Ljava/util/concurrent/Executor;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    new-instance v0, Lcom/zopim/android/sdk/data/k;

    invoke-static {}, Ljava/util/concurrent/Executors;->newCachedThreadPool()Ljava/util/concurrent/ExecutorService;

    move-result-object v1

    invoke-direct {v0, v1}, Lcom/zopim/android/sdk/data/k;-><init>(Ljava/util/concurrent/Executor;)V

    sput-object v0, Lcom/zopim/android/sdk/data/WebWidgetListener;->MY_EXECUTOR:Ljava/util/concurrent/Executor;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Landroid/webkit/WebViewClient;-><init>()V

    return-void
.end method

.method private executePathUpdate(Lcom/zopim/android/sdk/data/i;Ljava/lang/String;)V
    .locals 4
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    :try_start_0
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xb

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-lt v0, v1, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/data/WebWidgetListener;->MY_EXECUTOR:Ljava/util/concurrent/Executor;

    new-array v1, v3, [Ljava/lang/String;

    aput-object p2, v1, v2

    invoke-virtual {p1, v0, v1}, Lcom/zopim/android/sdk/data/i;->executeOnExecutor(Ljava/util/concurrent/Executor;[Ljava/lang/Object;)Landroid/os/AsyncTask;

    goto :goto_0

    :cond_0
    new-array v0, v3, [Ljava/lang/String;

    aput-object p2, v0, v2

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/data/i;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;
    :try_end_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    sget-object p2, Lcom/zopim/android/sdk/data/WebWidgetListener;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Could not execute path update due to a state error"

    invoke-static {p2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p1}, Ljava/lang/IllegalStateException;->printStackTrace()V

    :goto_0
    return-void
.end method


# virtual methods
.method public msg(Ljava/lang/String;)V
    .locals 1
    .annotation runtime Landroid/webkit/JavascriptInterface;
    .end annotation

    new-instance v0, Lcom/zopim/android/sdk/data/i;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/i;-><init>()V

    invoke-direct {p0, v0, p1}, Lcom/zopim/android/sdk/data/WebWidgetListener;->executePathUpdate(Lcom/zopim/android/sdk/data/i;Ljava/lang/String;)V

    return-void
.end method

.method public shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z
    .locals 4

    const/4 p1, 0x1

    if-eqz p2, :cond_0

    const-string v0, "z://"

    invoke-virtual {p2, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "z://"

    invoke-virtual {p2, v0}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v0

    const-string v1, "z://"

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    add-int/2addr v0, v1

    :try_start_0
    invoke-virtual {p2, v0}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p2
    :try_end_0
    .catch Ljava/lang/IndexOutOfBoundsException; {:try_start_0 .. :try_end_0} :catch_1

    :try_start_1
    const-string v0, "utf-8"

    invoke-static {p2, v0}, Ljava/net/URLDecoder;->decode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0
    :try_end_1
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_1 .. :try_end_1} :catch_0

    move-object p2, v0

    goto :goto_0

    :catch_0
    move-exception v0

    sget-object v1, Lcom/zopim/android/sdk/data/WebWidgetListener;->LOG_TAG:Ljava/lang/String;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Error encoding "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {v0}, Ljava/io/UnsupportedEncodingException;->printStackTrace()V

    :goto_0
    new-instance v0, Lcom/zopim/android/sdk/data/i;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/i;-><init>()V

    invoke-direct {p0, v0, p2}, Lcom/zopim/android/sdk/data/WebWidgetListener;->executePathUpdate(Lcom/zopim/android/sdk/data/i;Ljava/lang/String;)V

    return p1

    :catch_1
    move-exception p2

    sget-object v0, Lcom/zopim/android/sdk/data/WebWidgetListener;->LOG_TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Error parsing url. "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/IndexOutOfBoundsException;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {v0, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return p1

    :cond_0
    sget-object p2, Lcom/zopim/android/sdk/data/WebWidgetListener;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Not interested in handling URL loading"

    invoke-static {p2, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    return p1
.end method
