.class public Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;
.super Landroid/webkit/WebView;
.source "VideoEnabledWebView.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;,
        Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;
    }
.end annotation


# instance fields
.field private addedJavascriptInterface:Z

.field private final gestureDetector:Landroid/view/GestureDetector;

.field private videoEnabledWebChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 36
    invoke-direct {p0, p1}, Landroid/webkit/WebView;-><init>(Landroid/content/Context;)V

    .line 31
    new-instance p1, Landroid/view/GestureDetector;

    new-instance v0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;

    const/4 v1, 0x0

    invoke-direct {v0, p0, v1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;-><init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$1;)V

    invoke-direct {p1, v0}, Landroid/view/GestureDetector;-><init>(Landroid/view/GestureDetector$OnGestureListener;)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->gestureDetector:Landroid/view/GestureDetector;

    const/4 p1, 0x0

    .line 37
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addedJavascriptInterface:Z

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 1

    .line 42
    invoke-direct {p0, p1, p2}, Landroid/webkit/WebView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 31
    new-instance p1, Landroid/view/GestureDetector;

    new-instance p2, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;

    const/4 v0, 0x0

    invoke-direct {p2, p0, v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;-><init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$1;)V

    invoke-direct {p1, p2}, Landroid/view/GestureDetector;-><init>(Landroid/view/GestureDetector$OnGestureListener;)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->gestureDetector:Landroid/view/GestureDetector;

    const/4 p1, 0x0

    .line 43
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addedJavascriptInterface:Z

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 48
    invoke-direct {p0, p1, p2, p3}, Landroid/webkit/WebView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    .line 31
    new-instance p1, Landroid/view/GestureDetector;

    new-instance p2, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;

    const/4 p3, 0x0

    invoke-direct {p2, p0, p3}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$CustomGestureDetector;-><init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$1;)V

    invoke-direct {p1, p2}, Landroid/view/GestureDetector;-><init>(Landroid/view/GestureDetector$OnGestureListener;)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->gestureDetector:Landroid/view/GestureDetector;

    const/4 p1, 0x0

    .line 49
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addedJavascriptInterface:Z

    return-void
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;
    .locals 0

    .line 27
    iget-object p0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->videoEnabledWebChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    return-object p0
.end method

.method private addJavascriptInterface()V
    .locals 2

    .line 102
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addedJavascriptInterface:Z

    if-nez v0, :cond_0

    .line 105
    new-instance v0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView$JavascriptInterface;-><init>(Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;)V

    const-string v1, "_VideoEnabledWebView"

    invoke-virtual {p0, v0, v1}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addJavascriptInterface(Ljava/lang/Object;Ljava/lang/String;)V

    const/4 v0, 0x1

    .line 107
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addedJavascriptInterface:Z

    :cond_0
    return-void
.end method


# virtual methods
.method public isVideoFullscreen()Z
    .locals 1

    .line 59
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->videoEnabledWebChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->videoEnabledWebChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;->isVideoFullscreen()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public loadData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 79
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addJavascriptInterface()V

    .line 80
    invoke-super {p0, p1, p2, p3}, Landroid/webkit/WebView;->loadData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public loadDataWithBaseURL(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 85
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addJavascriptInterface()V

    .line 86
    invoke-super/range {p0 .. p5}, Landroid/webkit/WebView;->loadDataWithBaseURL(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public loadUrl(Ljava/lang/String;)V
    .locals 0

    .line 91
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addJavascriptInterface()V

    .line 92
    invoke-super {p0, p1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    return-void
.end method

.method public loadUrl(Ljava/lang/String;Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 97
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->addJavascriptInterface()V

    .line 98
    invoke-super {p0, p1, p2}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public onTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 1

    .line 131
    iget-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->gestureDetector:Landroid/view/GestureDetector;

    invoke-virtual {v0, p1}, Landroid/view/GestureDetector;->onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result v0

    if-nez v0, :cond_1

    invoke-super {p0, p1}, Landroid/webkit/WebView;->onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method

.method public setWebChromeClient(Landroid/webkit/WebChromeClient;)V
    .locals 2
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "SetJavaScriptEnabled"
        }
    .end annotation

    .line 68
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/webkit/WebSettings;->setJavaScriptEnabled(Z)V

    .line 70
    instance-of v0, p1, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    if-eqz v0, :cond_0

    .line 71
    move-object v0, p1

    check-cast v0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    iput-object v0, p0, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->videoEnabledWebChromeClient:Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebChromeClient;

    .line 74
    :cond_0
    invoke-super {p0, p1}, Landroid/webkit/WebView;->setWebChromeClient(Landroid/webkit/WebChromeClient;)V

    return-void
.end method
