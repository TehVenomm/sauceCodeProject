.class public Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;
.super Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;
.source "WebViewFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/WebViewContext;


# static fields
.field public static final START_URL_ARGUMENT:Ljava/lang/String; = "startUrl"


# instance fields
.field private currentRequestedUrl:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 20
    sget v0, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_webview:I

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;-><init>(I)V

    const/4 v0, 0x0

    .line 17
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->currentRequestedUrl:Ljava/lang/String;

    return-void
.end method

.method public static newFragmentWithUrl(Ljava/lang/String;)Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;
    .locals 3

    .line 24
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;-><init>()V

    .line 25
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "startUrl"

    .line 26
    invoke-virtual {v1, v2, p0}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 27
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method


# virtual methods
.method protected doHandleUri(Landroid/net/Uri;)Z
    .locals 2

    .line 50
    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->currentRequestedUrl:Ljava/lang/String;

    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 52
    :try_start_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->openUrlInExternalBrowser(Landroid/app/Activity;Ljava/lang/String;)Z

    move-result p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    return p1

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 56
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method protected getBackgroundMode(Landroid/net/Uri;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
    .locals 1

    .line 65
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object p1

    const-string v0, ".gogame.net"

    invoke-virtual {p1, v0}, Ljava/lang/String;->endsWith(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 66
    sget-object p1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->TRANSPARENT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    return-object p1

    .line 68
    :cond_0
    sget-object p1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->DEFAULT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    return-object p1
.end method

.method protected init(Landroid/os/Bundle;)V
    .locals 1

    const-string v0, "startUrl"

    .line 43
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 44
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->loadUrl(Ljava/lang/String;)Z

    return-void
.end method

.method public loadUrl(Ljava/lang/String;)Z
    .locals 3

    .line 33
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->isWebViewAvailable()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->getWebView()Landroid/webkit/WebView;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 34
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->getWebView()Landroid/webkit/WebView;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "javascript:window.location.href=\'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "\'"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    .line 35
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->currentRequestedUrl:Ljava/lang/String;

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method
