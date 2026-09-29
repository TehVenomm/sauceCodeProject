.class public abstract Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;
.super Landroid/app/Fragment;
.source "AbstractWebViewFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/BackPressedListener;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
    }
.end annotation


# static fields
.field protected static final CHARSET_NAME:Ljava/lang/String; = "UTF-8"

.field private static final COMMUNITY_SCHEME:Ljava/lang/String; = "community"

.field protected static final CONTENT_TYPE:Ljava/lang/String; = "text/html; charset=utf-8"

.field private static final HANDLE_VIMEO_URLS:Z = true

.field private static final HANDLE_YOUTUBE_URLS:Z = true

.field private static final SHARE_URL_PREFIX:Ljava/lang/String; = "share-"


# instance fields
.field private backgroundMode:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

.field private context:Landroid/content/Context;

.field private errorHtmlTemplate:Ljava/lang/String;

.field private errorHtmlTemplateInitialized:Z

.field private final layoutResourceId:I

.field private progressBar:Landroid/widget/ProgressBar;

.field private progressBar2:Landroid/widget/ProgressBar;

.field private savedInstanceState:Landroid/os/Bundle;

.field private webChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

.field private webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

.field private webViewIsAvailable:Z


# direct methods
.method public constructor <init>(I)V
    .locals 2

    .line 69
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    const/4 v0, 0x0

    .line 57
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    .line 58
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    .line 59
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->progressBar:Landroid/widget/ProgressBar;

    .line 60
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->progressBar2:Landroid/widget/ProgressBar;

    .line 62
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    const/4 v1, 0x0

    .line 64
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->errorHtmlTemplateInitialized:Z

    .line 65
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->errorHtmlTemplate:Ljava/lang/String;

    .line 66
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->backgroundMode:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    .line 71
    iput p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->layoutResourceId:I

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;
    .locals 0

    .line 48
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;I)V
    .locals 0

    .line 48
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->setProgress(I)V

    return-void
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;
    .locals 0

    .line 48
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    return-object p0
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    .line 48
    invoke-direct {p0, p1, p2, p3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->composeHtml(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$400(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 48
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->doLoadHtml(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method static synthetic access$500(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
    .locals 0

    .line 48
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->backgroundMode:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    return-object p0
.end method

.method static synthetic access$502(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
    .locals 0

    .line 48
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->backgroundMode:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    return-object p1
.end method

.method private composeHtml(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 4

    .line 90
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->errorHtmlTemplateInitialized:Z

    const/4 v1, 0x1

    if-nez v0, :cond_0

    :try_start_0
    const-string v0, "net/gogame/gowrap/webview-error-template.html"

    const-string v2, "net/gogame/gowrap/webview-error-template-default.html"

    .line 92
    filled-new-array {v0, v2}, [Ljava/lang/String;

    move-result-object v0

    const-string v2, "UTF-8"

    invoke-static {p1, v0, v2}, Lnet/gogame/gowrap/io/utils/IOUtils;->assetToString(Landroid/content/Context;[Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->errorHtmlTemplate:Ljava/lang/String;
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    .line 99
    :catch_0
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->errorHtmlTemplateInitialized:Z

    .line 101
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->errorHtmlTemplate:Ljava/lang/String;

    if-eqz p1, :cond_1

    .line 102
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->errorHtmlTemplate:Ljava/lang/String;

    const/4 v2, 0x2

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    .line 103
    invoke-static {p2}, Lnet/gogame/gowrap/support/StringUtils;->escapeHtml(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    aput-object p2, v2, v3

    .line 104
    invoke-static {p3}, Lnet/gogame/gowrap/support/StringUtils;->escapeHtml(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    aput-object p2, v2, v1

    .line 102
    invoke-static {p1, v0, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    return-object p1

    :cond_1
    const-string p1, ""

    return-object p1
.end method

.method private destroyWebView()V
    .locals 4

    .line 458
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    if-eqz v0, :cond_1

    const/4 v0, 0x0

    .line 459
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webViewIsAvailable:Z

    .line 460
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->stopLoading()V

    .line 461
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    const-string v1, "<html></html>"

    const-string v2, "text/plain"

    const-string v3, "UTF-8"

    invoke-virtual {v0, v1, v2, v3}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->loadData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 462
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->reload()V

    .line 463
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    const-string v1, "about:blank"

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->loadUrl(Ljava/lang/String;)V

    .line 464
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->getParent()Landroid/view/ViewParent;

    move-result-object v0

    instance-of v0, v0, Landroid/view/ViewGroup;

    if-eqz v0, :cond_0

    .line 465
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->getParent()Landroid/view/ViewParent;

    move-result-object v0

    check-cast v0, Landroid/view/ViewGroup;

    .line 466
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0, v1}, Landroid/view/ViewGroup;->removeView(Landroid/view/View;)V

    .line 468
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->removeAllViews()V

    .line 469
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->destroy()V

    const/4 v0, 0x0

    .line 470
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    :cond_1
    return-void
.end method

.method private doLoadHtml(Ljava/lang/String;Ljava/lang/String;)V
    .locals 7

    .line 475
    instance-of v0, p0, Lnet/gogame/gowrap/ui/InternalWebViewContext;

    if-eqz v0, :cond_0

    .line 476
    move-object v0, p0

    check-cast v0, Lnet/gogame/gowrap/ui/InternalWebViewContext;

    .line 477
    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/ui/InternalWebViewContext;->loadHtml(Ljava/lang/String;Ljava/lang/String;)Z

    goto :goto_0

    .line 478
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    instance-of v0, v0, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_1

    .line 479
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    check-cast v0, Lnet/gogame/gowrap/ui/UIContext;

    .line 480
    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/ui/UIContext;->loadHtml(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_1
    if-eqz p2, :cond_2

    .line 482
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    const-string v4, "text/html; charset=utf-8"

    const-string v5, "UTF-8"

    move-object v2, p2

    move-object v3, p1

    move-object v6, p2

    invoke-virtual/range {v1 .. v6}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->loadDataWithBaseURL(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 484
    :cond_2
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    const-string v0, "text/html; charset=utf-8"

    const-string v1, "UTF-8"

    invoke-virtual {p2, p1, v0, v1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->loadData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method private doSetProgress(Landroid/widget/ProgressBar;I)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    :cond_0
    const/16 v0, 0x64

    if-ne p2, v0, :cond_1

    .line 498
    invoke-virtual {p1}, Landroid/widget/ProgressBar;->getVisibility()I

    move-result p2

    const/16 v0, 0x8

    if-eq p2, v0, :cond_3

    .line 499
    invoke-virtual {p1, v0}, Landroid/widget/ProgressBar;->setVisibility(I)V

    goto :goto_0

    .line 502
    :cond_1
    invoke-virtual {p1}, Landroid/widget/ProgressBar;->getVisibility()I

    move-result v0

    if-eqz v0, :cond_2

    const/4 v0, 0x0

    .line 503
    invoke-virtual {p1, v0}, Landroid/widget/ProgressBar;->setVisibility(I)V

    .line 505
    :cond_2
    invoke-virtual {p1, p2}, Landroid/widget/ProgressBar;->setProgress(I)V

    :cond_3
    :goto_0
    return-void
.end method

.method private setProgress(I)V
    .locals 1

    .line 489
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->progressBar:Landroid/widget/ProgressBar;

    invoke-direct {p0, v0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->doSetProgress(Landroid/widget/ProgressBar;I)V

    .line 490
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->progressBar2:Landroid/widget/ProgressBar;

    invoke-direct {p0, v0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->doSetProgress(Landroid/widget/ProgressBar;I)V

    return-void
.end method


# virtual methods
.method protected abstract doHandleUri(Landroid/net/Uri;)Z
.end method

.method protected abstract getBackgroundMode(Landroid/net/Uri;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
.end method

.method public getCurrentUrl()Ljava/lang/String;
    .locals 1

    .line 83
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 86
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->getUrl()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected getUIContext()Lnet/gogame/gowrap/ui/UIContext;
    .locals 1

    .line 112
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    instance-of v0, v0, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_0

    .line 113
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    check-cast v0, Lnet/gogame/gowrap/ui/UIContext;

    return-object v0

    :cond_0
    const/4 v0, 0x0

    return-object v0
.end method

.method protected getWebView()Landroid/webkit/WebView;
    .locals 1

    .line 75
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    return-object v0
.end method

.method protected abstract init(Landroid/os/Bundle;)V
.end method

.method protected isWebViewAvailable()Z
    .locals 1

    .line 79
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webViewIsAvailable:Z

    return v0
.end method

.method public onAttach(Landroid/app/Activity;)V
    .locals 0

    .line 392
    invoke-super {p0, p1}, Landroid/app/Fragment;->onAttach(Landroid/app/Activity;)V

    .line 394
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    return-void
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 0

    .line 399
    invoke-super {p0, p1}, Landroid/app/Fragment;->onAttach(Landroid/content/Context;)V

    .line 401
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    return-void
.end method

.method public onBackPressed()Z
    .locals 2

    .line 413
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->onBackPressed()Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    return v1

    .line 416
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->canGoBack()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 417
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->goBack()V

    return v1

    :cond_1
    const/4 v0, 0x0

    return v0
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 9

    .line 122
    iget p3, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->layoutResourceId:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p3

    .line 124
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_progressBar:I

    invoke-virtual {p3, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/ProgressBar;

    iput-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->progressBar:Landroid/widget/ProgressBar;

    .line 125
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_progressBar2:I

    invoke-virtual {p3, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/ProgressBar;

    iput-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->progressBar2:Landroid/widget/ProgressBar;

    .line 127
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_main_webview:I

    invoke-virtual {p3, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    iput-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    const/4 v1, 0x1

    .line 128
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webViewIsAvailable:Z

    .line 129
    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v2, v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->setBackgroundColor(I)V

    .line 131
    sget v2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_webview_non_video_layout:I

    invoke-virtual {p3, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v5

    .line 133
    sget v2, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_webview_video_layout:I

    invoke-virtual {p3, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    move-object v6, v2

    check-cast v6, Landroid/view/ViewGroup;

    .line 135
    sget v2, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_view_loading_video:I

    invoke-virtual {p1, v2, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v7

    .line 138
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object p1

    const-string p2, "utf-8"

    .line 139
    invoke-virtual {p1, p2}, Landroid/webkit/WebSettings;->setDefaultTextEncodingName(Ljava/lang/String;)V

    .line 140
    invoke-virtual {p1, v1}, Landroid/webkit/WebSettings;->setJavaScriptEnabled(Z)V

    .line 141
    invoke-virtual {p1, v0}, Landroid/webkit/WebSettings;->setJavaScriptCanOpenWindowsAutomatically(Z)V

    .line 142
    invoke-virtual {p1, v1}, Landroid/webkit/WebSettings;->setDomStorageEnabled(Z)V

    .line 143
    invoke-virtual {p1, v1}, Landroid/webkit/WebSettings;->setGeolocationEnabled(Z)V

    .line 144
    sget p2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x11

    if-lt p2, v2, :cond_0

    .line 145
    invoke-virtual {p1, v0}, Landroid/webkit/WebSettings;->setMediaPlaybackRequiresUserGesture(Z)V

    .line 147
    :cond_0
    sget p2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x15

    if-lt p2, v2, :cond_1

    .line 148
    invoke-virtual {p1, v0}, Landroid/webkit/WebSettings;->setMixedContentMode(I)V

    .line 151
    :cond_1
    invoke-static {}, Landroid/webkit/CookieManager;->getInstance()Landroid/webkit/CookieManager;

    move-result-object p1

    invoke-virtual {p1, v1}, Landroid/webkit/CookieManager;->setAcceptCookie(Z)V

    .line 153
    new-instance p1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$1;

    iget-object v8, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    move-object v3, p1

    move-object v4, p0

    invoke-direct/range {v3 .. v8}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Landroid/view/View;Landroid/view/ViewGroup;Landroid/view/View;Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    .line 179
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    new-instance p2, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$2;

    invoke-direct {p2, p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$2;-><init>(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)V

    invoke-virtual {p1, p2}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->setOnToggledFullscreen(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient$ToggledFullscreenCallback;)V

    .line 193
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    invoke-virtual {p1, p2}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->setWebChromeClient(Landroid/webkit/WebChromeClient;)V

    .line 195
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    new-instance p2, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;

    invoke-direct {p2, p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;-><init>(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)V

    invoke-virtual {p1, p2}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->setWebViewClient(Landroid/webkit/WebViewClient;)V

    .line 375
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->savedInstanceState:Landroid/os/Bundle;

    if-eqz p1, :cond_2

    .line 376
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->savedInstanceState:Landroid/os/Bundle;

    invoke-virtual {p1, p2}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->restoreState(Landroid/os/Bundle;)Landroid/webkit/WebBackForwardList;

    goto :goto_0

    .line 378
    :cond_2
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->init(Landroid/os/Bundle;)V

    :goto_0
    return-object p3
.end method

.method public onDestroy()V
    .locals 0

    .line 453
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->destroyWebView()V

    .line 454
    invoke-super {p0}, Landroid/app/Fragment;->onDestroy()V

    return-void
.end method

.method public onDestroyView()V
    .locals 0

    .line 447
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->destroyWebView()V

    .line 448
    invoke-super {p0}, Landroid/app/Fragment;->onDestroyView()V

    return-void
.end method

.method public onDetach()V
    .locals 1

    .line 406
    invoke-super {p0}, Landroid/app/Fragment;->onDetach()V

    .line 407
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->destroyWebView()V

    const/4 v0, 0x0

    .line 408
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    return-void
.end method

.method public onPause()V
    .locals 2

    .line 431
    invoke-super {p0}, Landroid/app/Fragment;->onPause()V

    .line 433
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    instance-of v0, v0, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_0

    .line 434
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->context:Landroid/content/Context;

    check-cast v0, Lnet/gogame/gowrap/ui/UIContext;

    .line 435
    invoke-interface {v0}, Lnet/gogame/gowrap/ui/UIContext;->onLoadingFinished()V

    .line 438
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->onPause()V

    .line 440
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 441
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v1, v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->saveState(Landroid/os/Bundle;)Landroid/webkit/WebBackForwardList;

    .line 442
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->savedInstanceState:Landroid/os/Bundle;

    return-void
.end method

.method public onResume()V
    .locals 1

    .line 425
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->webView:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->onResume()V

    .line 426
    invoke-super {p0}, Landroid/app/Fragment;->onResume()V

    return-void
.end method
