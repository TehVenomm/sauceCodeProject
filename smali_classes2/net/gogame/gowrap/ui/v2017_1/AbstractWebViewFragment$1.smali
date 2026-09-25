.class Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$1;
.super Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;
.source "AbstractWebViewFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Landroid/view/View;Landroid/view/ViewGroup;Landroid/view/View;Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)V
    .locals 0

    .line 154
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-direct {p0, p2, p3, p4, p5}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;-><init>(Landroid/view/View;Landroid/view/ViewGroup;Landroid/view/View;Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)V

    return-void
.end method


# virtual methods
.method public onConsoleMessage(Landroid/webkit/ConsoleMessage;)Z
    .locals 5

    const-string v0, "goWrap"

    const-string v1, "%s %s [%s:%d]"

    const/4 v2, 0x4

    .line 158
    new-array v2, v2, [Ljava/lang/Object;

    .line 159
    invoke-virtual {p1}, Landroid/webkit/ConsoleMessage;->messageLevel()Landroid/webkit/ConsoleMessage$MessageLevel;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {p1}, Landroid/webkit/ConsoleMessage;->message()Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x1

    aput-object v3, v2, v4

    .line 160
    invoke-virtual {p1}, Landroid/webkit/ConsoleMessage;->sourceId()Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x2

    aput-object v3, v2, v4

    invoke-virtual {p1}, Landroid/webkit/ConsoleMessage;->lineNumber()I

    move-result v3

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v4, 0x3

    aput-object v3, v2, v4

    .line 158
    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 161
    invoke-super {p0, p1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->onConsoleMessage(Landroid/webkit/ConsoleMessage;)Z

    move-result p1

    return p1
.end method

.method public onProgressChanged(Landroid/webkit/WebView;I)V
    .locals 1

    .line 166
    invoke-super {p0, p1, p2}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->onProgressChanged(Landroid/webkit/WebView;I)V

    .line 168
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object p1

    instance-of p1, p1, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz p1, :cond_1

    .line 169
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/ui/UIContext;

    const/16 v0, 0x64

    if-ne p2, v0, :cond_0

    .line 171
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/UIContext;->onLoadingFinished()V

    goto :goto_0

    .line 173
    :cond_0
    invoke-interface {p1}, Lnet/gogame/gowrap/ui/UIContext;->onLoadingStarted()V

    .line 175
    :goto_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;I)V

    :cond_1
    return-void
.end method
