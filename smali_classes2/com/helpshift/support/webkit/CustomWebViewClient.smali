.class public Lcom/helpshift/support/webkit/CustomWebViewClient;
.super Landroid/webkit/WebViewClient;
.source "CustomWebViewClient.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;
    }
.end annotation


# static fields
.field public static final TAG:Ljava/lang/String; = "CustomWebViewClient"


# instance fields
.field private context:Landroid/content/Context;

.field private final customWebViewClientListeners:Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;)V
    .locals 0

    .line 36
    invoke-direct {p0}, Landroid/webkit/WebViewClient;-><init>()V

    .line 37
    iput-object p2, p0, Lcom/helpshift/support/webkit/CustomWebViewClient;->customWebViewClientListeners:Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;

    .line 38
    iput-object p1, p0, Lcom/helpshift/support/webkit/CustomWebViewClient;->context:Landroid/content/Context;

    return-void
.end method

.method private handleUrlClick(Landroid/webkit/WebView;Ljava/lang/String;)Z
    .locals 3

    .line 62
    invoke-virtual {p1}, Landroid/webkit/WebView;->getContext()Landroid/content/Context;

    move-result-object p1

    .line 63
    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 64
    invoke-virtual {p2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p2

    .line 65
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 66
    invoke-static {p2}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v1

    .line 67
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 68
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    invoke-virtual {v0, v2}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 69
    invoke-virtual {v1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v1

    .line 70
    invoke-direct {p0, p2, v1}, Lcom/helpshift/support/webkit/CustomWebViewClient;->pushFAQDeeplinkClickEvent(Ljava/lang/String;Ljava/lang/String;)V

    .line 71
    invoke-virtual {p1, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method private interceptRequest(Ljava/lang/String;)Landroid/webkit/WebResourceResponse;
    .locals 6

    .line 99
    iget-object v0, p0, Lcom/helpshift/support/webkit/CustomWebViewClient;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getExternalCacheDir()Ljava/io/File;

    move-result-object v0

    const/4 v1, 0x0

    .line 102
    :try_start_0
    new-instance v2, Ljava/net/URL;

    invoke-direct {v2, p1}, Ljava/net/URL;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 105
    sget-object v3, Lcom/helpshift/support/webkit/CustomWebViewClient;->TAG:Ljava/lang/String;

    const-string v4, "MalformedURLException"

    invoke-static {v3, v4, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object v2, v1

    :goto_0
    if-eqz v2, :cond_1

    .line 109
    new-instance v3, Ljava/io/File;

    const-string v4, "/"

    const-string v5, "_"

    invoke-virtual {p1, v4, v5}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {v3, v0, p1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 111
    invoke-virtual {v3}, Ljava/io/File;->exists()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 113
    :try_start_1
    new-instance p1, Landroid/webkit/WebResourceResponse;

    const-string v0, ""

    const-string v2, ""

    new-instance v4, Ljava/io/FileInputStream;

    invoke-direct {v4, v3}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    invoke-direct {p1, v0, v2, v4}, Landroid/webkit/WebResourceResponse;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/io/InputStream;)V
    :try_end_1
    .catch Ljava/io/FileNotFoundException; {:try_start_1 .. :try_end_1} :catch_1

    return-object p1

    :catch_1
    move-exception p1

    .line 116
    sget-object v0, Lcom/helpshift/support/webkit/CustomWebViewClient;->TAG:Ljava/lang/String;

    const-string v2, "FileNotFoundException"

    invoke-static {v0, v2, p1}, Lcom/helpshift/util/HSLogger;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_1

    .line 120
    :cond_0
    invoke-static {v2}, Lcom/helpshift/util/FileUtil;->getMimeType(Ljava/net/URL;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/util/FileUtil;->isSupportedMimeType(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 121
    invoke-static {v2, v3}, Lcom/helpshift/util/FileUtil;->saveFile(Ljava/net/URL;Ljava/io/File;)V

    :cond_1
    :goto_1
    return-object v1
.end method

.method private pushFAQDeeplinkClickEvent(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 79
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "p"

    .line 80
    invoke-interface {v0, v1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "u"

    .line 81
    invoke-interface {v0, p2, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 82
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object p1

    sget-object p2, Lcom/helpshift/analytics/AnalyticsEventType;->LINK_VIA_FAQ:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {p1, p2, v0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    return-void
.end method


# virtual methods
.method public onPageFinished(Landroid/webkit/WebView;Ljava/lang/String;)V
    .locals 0

    .line 93
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->onPageFinished(Landroid/webkit/WebView;Ljava/lang/String;)V

    .line 94
    iget-object p1, p0, Lcom/helpshift/support/webkit/CustomWebViewClient;->customWebViewClientListeners:Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;

    invoke-interface {p1}, Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;->onPageFinished()V

    return-void
.end method

.method public onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V
    .locals 0

    .line 87
    invoke-super {p0, p1, p2, p3}, Landroid/webkit/WebViewClient;->onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V

    .line 88
    iget-object p1, p0, Lcom/helpshift/support/webkit/CustomWebViewClient;->customWebViewClientListeners:Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;

    invoke-interface {p1}, Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;->onPageStarted()V

    return-void
.end method

.method public shouldInterceptRequest(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Landroid/webkit/WebResourceResponse;
    .locals 2

    .line 130
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_0

    .line 131
    invoke-interface {p2}, Landroid/webkit/WebResourceRequest;->getUrl()Landroid/net/Uri;

    move-result-object v0

    invoke-virtual {v0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/support/webkit/CustomWebViewClient;->interceptRequest(Ljava/lang/String;)Landroid/webkit/WebResourceResponse;

    move-result-object v0

    if-eqz v0, :cond_0

    return-object v0

    .line 136
    :cond_0
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->shouldInterceptRequest(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Landroid/webkit/WebResourceResponse;

    move-result-object p1

    return-object p1
.end method

.method public shouldInterceptRequest(Landroid/webkit/WebView;Ljava/lang/String;)Landroid/webkit/WebResourceResponse;
    .locals 2

    .line 141
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-ge v0, v1, :cond_0

    .line 142
    invoke-direct {p0, p2}, Lcom/helpshift/support/webkit/CustomWebViewClient;->interceptRequest(Ljava/lang/String;)Landroid/webkit/WebResourceResponse;

    move-result-object v0

    if-eqz v0, :cond_0

    return-object v0

    .line 147
    :cond_0
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->shouldInterceptRequest(Landroid/webkit/WebView;Ljava/lang/String;)Landroid/webkit/WebResourceResponse;

    move-result-object p1

    return-object p1
.end method

.method public shouldOverrideUrlLoading(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z
    .locals 2

    .line 43
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x18

    if-lt v0, v1, :cond_0

    .line 44
    invoke-interface {p2}, Landroid/webkit/WebResourceRequest;->getUrl()Landroid/net/Uri;

    move-result-object v0

    invoke-virtual {v0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/webkit/CustomWebViewClient;->handleUrlClick(Landroid/webkit/WebView;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x1

    return p1

    .line 48
    :cond_0
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->shouldOverrideUrlLoading(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z

    move-result p1

    return p1
.end method

.method public shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z
    .locals 2

    .line 53
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x18

    if-ge v0, v1, :cond_0

    .line 54
    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/webkit/CustomWebViewClient;->handleUrlClick(Landroid/webkit/WebView;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x1

    return p1

    .line 58
    :cond_0
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method
