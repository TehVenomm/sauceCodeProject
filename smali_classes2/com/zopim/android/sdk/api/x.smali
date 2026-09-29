.class final Lcom/zopim/android/sdk/api/x;
.super Lcom/zopim/android/sdk/api/a;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/api/x$a;
    }
.end annotation


# static fields
.field private static final b:Ljava/lang/String; = "x"

.field private static final c:J


# instance fields
.field a:Landroid/os/Handler;

.field private d:Landroid/webkit/WebView;

.field private e:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0x5

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    sput-wide v0, Lcom/zopim/android/sdk/api/x;->c:J

    return-void
.end method

.method private constructor <init>()V
    .locals 2

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/a;-><init>()V

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/x;->a:Landroid/os/Handler;

    return-void
.end method

.method constructor <init>(Landroid/content/Context;)V
    .locals 9

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/a;-><init>()V

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/x;->a:Landroid/os/Handler;

    new-instance v0, Landroid/webkit/WebView;

    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/webkit/WebView;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    iget-object v0, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    invoke-virtual {v0}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/webkit/WebSettings;->setJavaScriptEnabled(Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    new-instance v2, Landroid/webkit/WebChromeClient;

    invoke-direct {v2}, Landroid/webkit/WebChromeClient;-><init>()V

    invoke-virtual {v0, v2}, Landroid/webkit/WebView;->setWebChromeClient(Landroid/webkit/WebChromeClient;)V

    new-instance v0, Lcom/zopim/android/sdk/data/WebWidgetListener;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/WebWidgetListener;-><init>()V

    iget-object v2, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    const-string v3, "JSInterface"

    invoke-virtual {v2, v0, v3}, Landroid/webkit/WebView;->addJavascriptInterface(Ljava/lang/Object;Ljava/lang/String;)V

    iget-object v2, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    invoke-virtual {v2, v0}, Landroid/webkit/WebView;->setWebViewClient(Landroid/webkit/WebViewClient;)V

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v2, "%s/%s"

    const/4 v3, 0x2

    new-array v4, v3, [Ljava/lang/Object;

    invoke-static {p1}, Lcom/zopim/android/sdk/util/AppInfo;->getApplicationName(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "\\s+"

    const-string v7, ""

    invoke-virtual {v5, v6, v7}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x0

    aput-object v5, v4, v6

    invoke-static {p1}, Lcom/zopim/android/sdk/util/AppInfo;->getApplicationVersionName(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v4, v1

    invoke-static {v0, v2, v4}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v2, "%s/%s"

    new-array v4, v3, [Ljava/lang/Object;

    invoke-static {}, Lcom/zopim/android/sdk/util/AppInfo;->getChatSdkName()Ljava/lang/String;

    move-result-object v5

    const-string v7, "\\s+"

    const-string v8, ""

    invoke-virtual {v5, v7, v8}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {}, Lcom/zopim/android/sdk/util/AppInfo;->getChatSdkVersionName()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v1

    invoke-static {v0, v2, v4}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    sget-object v2, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v4, "%s %s %s"

    const/4 v5, 0x3

    new-array v5, v5, [Ljava/lang/Object;

    iget-object v7, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    invoke-virtual {v7}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v7

    invoke-virtual {v7}, Landroid/webkit/WebSettings;->getUserAgentString()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    aput-object p1, v5, v1

    aput-object v0, v5, v3

    invoke-static {v2, v4, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/api/x;->e:Ljava/lang/String;

    return-void
.end method

.method static synthetic a(Lcom/zopim/android/sdk/api/x;)Landroid/webkit/WebView;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    return-object p0
.end method

.method static synthetic a(Lcom/zopim/android/sdk/api/x;Landroid/webkit/WebView;)Landroid/webkit/WebView;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    return-object p1
.end method

.method private a(Ljava/io/File;)V
    .locals 6
    .param p1    # Ljava/io/File;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->b(Ljava/io/File;)Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/FileTransfers;->add(Ljava/io/File;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_2

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_1

    goto :goto_0

    :cond_1
    invoke-static {v0}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "."

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p1}, Lcom/zopim/android/sdk/attachment/FileExtension;->getExtension(Ljava/io/File;)Lcom/zopim/android/sdk/attachment/FileExtension;

    move-result-object v2

    invoke-virtual {v2}, Lcom/zopim/android/sdk/attachment/FileExtension;->getValue()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1}, Ljava/io/File;->length()J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p1

    sget-object v2, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v3, "javascript:__z_sdk.sendFile(\'%s\', \'%s\', \'%s\');"

    const/4 v4, 0x3

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    aput-object v0, v4, v5

    const/4 v0, 0x1

    aput-object v1, v4, v0

    const/4 v0, 0x2

    aput-object p1, v4, v0

    invoke-static {v2, v3, v4}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void

    :cond_2
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "File name is invalid. Will not prepare attachment upload."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method private declared-synchronized a(Ljava/lang/String;)V
    .locals 2

    monitor-enter p0

    :try_start_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/x;->a:Landroid/os/Handler;

    new-instance v1, Lcom/zopim/android/sdk/api/y;

    invoke-direct {v1, p0, p1}, Lcom/zopim/android/sdk/api/y;-><init>(Lcom/zopim/android/sdk/api/x;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method private a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 8

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v1, "window.__z_sdk.initApp({bridge: \'%s\',register: {mID: \'%s\',ua: \'%s\',title: \'%s\',url: \'%s\',ref: \'%s\'}});"

    const/4 v2, 0x6

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p2, v2, v3

    const/4 v4, 0x1

    aput-object p3, v2, v4

    const/4 v5, 0x2

    aput-object p4, v2, v5

    const/4 v6, 0x3

    aput-object p5, v2, v6

    const/4 v6, 0x4

    aput-object p6, v2, v6

    const/4 v6, 0x5

    aput-object p7, v2, v6

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    sget-object v1, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v2, "<html><head></head><body><script src=\'%s\'></script><script type=\"text/javascript\">%s</script></body></html>"

    new-array v5, v5, [Ljava/lang/Object;

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "mobile_sdk.js?"

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v3

    aput-object v0, v5, v4

    invoke-static {v1, v2, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    iget-object v2, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    const-string v3, "https://dashboard.zopim.com/bin/"

    const-string v4, "text/html"

    const-string v5, "UTF-8"

    const-string v6, ""

    move-object p1, v2

    move-object p2, v3

    move-object p3, v0

    move-object p4, v4

    move-object p5, v5

    move-object p6, v6

    invoke-virtual/range {p1 .. p6}, Landroid/webkit/WebView;->loadDataWithBaseURL(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method private b(Ljava/io/File;)Z
    .locals 2

    const/4 v0, 0x0

    if-eqz p1, :cond_3

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_3

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_1

    :cond_0
    invoke-virtual {p1}, Ljava/io/File;->isDirectory()Z

    move-result v1

    if-eqz v1, :cond_1

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v1, "Directory is not supported"

    :goto_0
    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return v0

    :cond_1
    invoke-virtual {p1}, Ljava/io/File;->exists()Z

    move-result p1

    if-nez p1, :cond_2

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v1, "File does not exist"

    goto :goto_0

    :cond_2
    const/4 p1, 0x1

    return p1

    :cond_3
    :goto_1
    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v1, "File can not be null or empty"

    goto :goto_0
.end method

.method static synthetic c()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    return-object v0
.end method


# virtual methods
.method a()V
    .locals 1

    const-string v0, "javascript:__z_sdk.sendActive();"

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void
.end method

.method a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 8

    if-eqz p2, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Reconnecting to previous chat id: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    if-eqz p2, :cond_1

    :goto_0
    move-object v3, p2

    goto :goto_1

    :cond_1
    const-string p2, ""

    goto :goto_0

    :goto_1
    const-string v2, "jsinterface"

    iget-object v4, p0, Lcom/zopim/android/sdk/api/x;->e:Ljava/lang/String;

    const-string v6, ""

    move-object v0, p0

    move-object v1, p1

    move-object v5, p3

    move-object v7, p4

    invoke-direct/range {v0 .. v7}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method varargs a([Ljava/lang/String;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Tags must not be null. Will not set tags."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    array-length v0, p1

    new-array v0, v0, [Ljava/lang/String;

    const/4 v1, 0x0

    const/4 v2, 0x0

    :goto_0
    array-length v3, p1

    if-ge v2, v3, :cond_1

    aget-object v3, p1, v2

    invoke-static {v3}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    aput-object v3, v0, v2

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    invoke-static {v0}, Ljava/util/Arrays;->toString([Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    const/4 v2, 0x1

    sub-int/2addr v0, v2

    invoke-virtual {p1, v2, v0}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v3, "javascript:$zopim.livechat.addTags(\'%s\');"

    new-array v2, v2, [Ljava/lang/Object;

    aput-object p1, v2, v1

    invoke-static {v0, v3, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void
.end method

.method public b()V
    .locals 1

    const-string v0, "javascript:__z_sdk.sendButtonClicked();"

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void
.end method

.method public emailTranscript(Ljava/lang/String;)Z
    .locals 5

    const/4 v0, 0x0

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_0

    :cond_0
    sget-object v1, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v2, "javascript:__z_sdk.sendEmailTranscript(\'%s\');"

    const/4 v3, 0x1

    new-array v4, v3, [Ljava/lang/Object;

    aput-object p1, v4, v0

    invoke-static {v1, v2, v4}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return v3

    :cond_1
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v1, "Email address must not be null or empty. Will not email transcript."

    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return v0
.end method

.method public endChat()V
    .locals 4

    const-string v0, "javascript:$zopim.livechat.endChat();"

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    const-string v0, "javascript:__z_sdk.sendDisconnectTimeout(1);"

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/x;->a:Landroid/os/Handler;

    new-instance v1, Lcom/zopim/android/sdk/api/x$a;

    iget-object v2, p0, Lcom/zopim/android/sdk/api/x;->d:Landroid/webkit/WebView;

    invoke-direct {v1, p0, v2}, Lcom/zopim/android/sdk/api/x$a;-><init>(Lcom/zopim/android/sdk/api/x;Landroid/webkit/WebView;)V

    sget-wide v2, Lcom/zopim/android/sdk/api/x;->c:J

    invoke-virtual {v0, v1, v2, v3}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    iget-object v0, p0, Lcom/zopim/android/sdk/api/x;->a:Landroid/os/Handler;

    new-instance v1, Lcom/zopim/android/sdk/api/z;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/api/z;-><init>(Lcom/zopim/android/sdk/api/x;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public resend(Ljava/lang/String;)V
    .locals 5

    if-eqz p1, :cond_2

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object v0

    invoke-virtual {v0, p1}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/zopim/android/sdk/model/ChatLog;

    if-nez v0, :cond_1

    sget-object v1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Could not resend the message. No message with message id = "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/model/ChatLog;->setFailed(Z)V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    sget-object v2, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v3, "javascript:__z_sdk.sendChatMsg(\'%s\', \'%s\');"

    const/4 v4, 0x2

    new-array v4, v4, [Ljava/lang/Object;

    aput-object v0, v4, v1

    const/4 v0, 0x1

    aput-object p1, v4, v0

    invoke-static {v2, v3, v4}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void

    :cond_2
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Message ID must not be null or empty. Will not resend message."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public send(Ljava/io/File;)V
    .locals 1
    .param p1    # Ljava/io/File;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->b(Ljava/io/File;)Z

    move-result v0

    if-nez v0, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Could not send file"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/io/File;)V

    return-void
.end method

.method public send(Ljava/lang/String;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Message must not be null. Will not send message."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v1, "javascript:__z_sdk.sendChatMsg(\'%s\');"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void
.end method

.method public sendChatComment(Ljava/lang/String;)V
    .locals 4
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v1, "javascript:__z_sdk.sendChatComment(\'%s\');"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void

    :cond_1
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Comment must not be null or empty. Will not comment on this chat."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public sendChatRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V
    .locals 4
    .param p1    # Lcom/zopim/android/sdk/model/ChatLog$Rating;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Rating must not be null. Will not rate this chat."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/aa;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->ordinal()I

    move-result v1

    aget v0, v0, v1

    const/4 v1, 0x0

    packed-switch v0, :pswitch_data_0

    sget-object v0, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unknown rating "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p1, " will not be sent"

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_1

    :pswitch_0
    sget-object p1, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v0, "javascript:__z_sdk.sendChatRating(null);"

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {p1, v0, v1}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :pswitch_1
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->getValue()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v2, "javascript:__z_sdk.sendChatRating(\'%s\');"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    aput-object p1, v3, v1

    invoke-static {v0, v2, v3}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    :goto_0
    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    :goto_1
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public sendOfflineMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 4

    const/4 v0, 0x0

    if-eqz p2, :cond_4

    invoke-virtual {p2}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_2

    :cond_0
    if-eqz p3, :cond_3

    invoke-virtual {p3}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_1

    goto :goto_0

    :cond_1
    if-nez p1, :cond_2

    const-string p1, ""

    :cond_2
    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p2}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    invoke-static {p3}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    sget-object v1, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v2, "javascript:__z_sdk.sendOfflineMsg(\'%s\', \'%s\', \'%s\');"

    const/4 v3, 0x3

    new-array v3, v3, [Ljava/lang/Object;

    aput-object p1, v3, v0

    const/4 p1, 0x1

    aput-object p2, v3, p1

    const/4 p2, 0x2

    aput-object p3, v3, p2

    invoke-static {v1, v2, v3}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-direct {p0, p2}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return p1

    :cond_3
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string p2, "Message must not be null or empty. Will not send email."

    :goto_1
    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return v0

    :cond_4
    :goto_2
    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string p2, "Email address must not be null or empty. Will not send email."

    goto :goto_1
.end method

.method public setDepartment(Ljava/lang/String;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Department must not be null. Will not set department."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v1, "javascript:$zopim.livechat.departments.setVisitorDepartment(\'%s\');"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Email must not be null. Will not set email."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v1, "javascript:$zopim.livechat.setEmail(\'%s\');"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void
.end method

.method public setName(Ljava/lang/String;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Name must not be null. Will not set name."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v1, "javascript:$zopim.livechat.setName(\'%s\');"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void
.end method

.method public setPhoneNumber(Ljava/lang/String;)V
    .locals 4

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/x;->b:Ljava/lang/String;

    const-string v0, "Phone number must not be null. Will not set phone number."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    invoke-static {p1}, Lorg/apache/commons/lang3/StringEscapeUtils;->escapeEcmaScript(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v1, "javascript:$zopim.livechat.setPhone(\'%s\');"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/x;->a(Ljava/lang/String;)V

    return-void
.end method
