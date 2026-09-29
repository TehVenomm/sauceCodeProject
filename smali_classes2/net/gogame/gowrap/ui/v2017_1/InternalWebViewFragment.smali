.class public Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;
.super Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;
.source "InternalWebViewFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/InternalWebViewContext;


# static fields
.field public static final BASE_URL_ARGUMENT:Ljava/lang/String; = "baseUrl"

.field public static final START_HTML_ARGUMENT:Ljava/lang/String; = "startHtml"


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 20
    sget v0, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_webview_internal:I

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;-><init>(I)V

    return-void
.end method

.method public static newFragmentWithHtml(Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;
    .locals 3

    .line 24
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;-><init>()V

    .line 25
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "startHtml"

    .line 26
    invoke-virtual {v1, v2, p0}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "baseUrl"

    .line 27
    invoke-virtual {v1, p0, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 28
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method


# virtual methods
.method protected doHandleUri(Landroid/net/Uri;)Z
    .locals 4

    const/4 v0, 0x1

    .line 57
    :try_start_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->openUrlInExternalBrowser(Landroid/app/Activity;Ljava/lang/String;)Z

    move-result v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    if-eqz v1, :cond_0

    return v0

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 61
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 64
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->getUIContext()Lnet/gogame/gowrap/ui/UIContext;

    move-result-object v1

    const/4 v2, 0x0

    if-eqz v1, :cond_1

    .line 65
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->getUIContext()Lnet/gogame/gowrap/ui/UIContext;

    move-result-object v1

    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v1, p1, v2}, Lnet/gogame/gowrap/ui/UIContext;->loadUrl(Ljava/lang/String;Z)V

    return v0

    :cond_1
    return v2
.end method

.method protected getBackgroundMode(Landroid/net/Uri;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
    .locals 0

    .line 73
    sget-object p1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->TRANSPARENT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    return-object p1
.end method

.method protected init(Landroid/os/Bundle;)V
    .locals 2

    const-string v0, "startHtml"

    .line 48
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "baseUrl"

    .line 49
    invoke-virtual {p1, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 50
    invoke-virtual {p0, v0, p1}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->loadHtml(Ljava/lang/String;Ljava/lang/String;)Z

    return-void
.end method

.method public loadHtml(Ljava/lang/String;Ljava/lang/String;)Z
    .locals 7

    .line 34
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->isWebViewAvailable()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->getWebView()Landroid/webkit/WebView;

    move-result-object v0

    if-eqz v0, :cond_1

    if-eqz p2, :cond_0

    .line 36
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->getWebView()Landroid/webkit/WebView;

    move-result-object v1

    const-string v4, "text/html; charset=utf-8"

    const-string v5, "UTF-8"

    move-object v2, p2

    move-object v3, p1

    move-object v6, p2

    invoke-virtual/range {v1 .. v6}, Landroid/webkit/WebView;->loadDataWithBaseURL(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 39
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->getWebView()Landroid/webkit/WebView;

    move-result-object p2

    const-string v0, "text/html; charset=utf-8"

    const-string v1, "UTF-8"

    invoke-virtual {p2, p1, v0, v1}, Landroid/webkit/WebView;->loadData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    const/4 p1, 0x1

    return p1

    :cond_1
    const/4 p1, 0x0

    return p1
.end method

.method public onBackPressed()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method
