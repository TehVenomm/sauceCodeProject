.class public Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;
.super Ljava/lang/Object;
.source "VideoEnabledWebView.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "JavascriptInterface"
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;


# direct methods
.method public constructor <init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)V
    .locals 0

    .line 111
    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;->this$0:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public notifyVideoEnd()V
    .locals 2
    .annotation runtime Landroid/webkit/JavascriptInterface;
    .end annotation

    const-string v0, "___"

    const-string v1, "GOT IT"

    .line 116
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 118
    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    new-instance v1, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface$1;-><init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
