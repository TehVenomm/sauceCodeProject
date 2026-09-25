.class Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface$1;
.super Ljava/lang/Object;
.source "VideoEnabledWebView.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;->notifyVideoEnd()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;)V
    .locals 0

    .line 118
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface$1;->this$1:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 121
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface$1;->this$1:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;->this$0:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->access$100(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 122
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface$1;->this$1:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;->this$0:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->access$100(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->onHideCustomView()V

    :cond_0
    return-void
.end method
