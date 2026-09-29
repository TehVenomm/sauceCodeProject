.class public Lnet/gree/unitywebview/WebViewPlugin;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"


# instance fields
.field private bottomMargin:I

.field private cookies:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;>;"
        }
    .end annotation
.end field

.field private isDestroyed:Z

.field private layout:Landroid/widget/FrameLayout;

.field private leftMargin:I

.field private mDownTime:J

.field private mWebView:Landroid/webkit/WebView;

.field private rightMargin:I

.field private topMargin:I


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 25
    iput-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    return-void
.end method

.method static synthetic access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;
    .locals 0

    .line 23
    iget-object p0, p0, Lnet/gree/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    return-object p0
.end method

.method static synthetic access$002(Lnet/gree/unitywebview/WebViewPlugin;Landroid/webkit/WebView;)Landroid/webkit/WebView;
    .locals 0

    .line 23
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    return-object p1
.end method

.method static synthetic access$100(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/widget/FrameLayout;
    .locals 0

    .line 23
    iget-object p0, p0, Lnet/gree/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    return-object p0
.end method

.method static synthetic access$102(Lnet/gree/unitywebview/WebViewPlugin;Landroid/widget/FrameLayout;)Landroid/widget/FrameLayout;
    .locals 0

    .line 23
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    return-object p1
.end method

.method static synthetic access$200(Lnet/gree/unitywebview/WebViewPlugin;)Z
    .locals 0

    .line 23
    iget-boolean p0, p0, Lnet/gree/unitywebview/WebViewPlugin;->isDestroyed:Z

    return p0
.end method

.method static synthetic access$300(Lnet/gree/unitywebview/WebViewPlugin;)I
    .locals 0

    .line 23
    iget p0, p0, Lnet/gree/unitywebview/WebViewPlugin;->leftMargin:I

    return p0
.end method

.method static synthetic access$400(Lnet/gree/unitywebview/WebViewPlugin;)I
    .locals 0

    .line 23
    iget p0, p0, Lnet/gree/unitywebview/WebViewPlugin;->rightMargin:I

    return p0
.end method

.method static synthetic access$500(Lnet/gree/unitywebview/WebViewPlugin;)I
    .locals 0

    .line 23
    iget p0, p0, Lnet/gree/unitywebview/WebViewPlugin;->topMargin:I

    return p0
.end method

.method static synthetic access$600(Lnet/gree/unitywebview/WebViewPlugin;)I
    .locals 0

    .line 23
    iget p0, p0, Lnet/gree/unitywebview/WebViewPlugin;->bottomMargin:I

    return p0
.end method

.method static synthetic access$700(Lnet/gree/unitywebview/WebViewPlugin;)Ljava/util/HashMap;
    .locals 0

    .line 23
    iget-object p0, p0, Lnet/gree/unitywebview/WebViewPlugin;->cookies:Ljava/util/HashMap;

    return-object p0
.end method


# virtual methods
.method public Destroy()V
    .locals 2

    const/4 v0, 0x1

    .line 141
    iput-boolean v0, p0, Lnet/gree/unitywebview/WebViewPlugin;->isDestroyed:Z

    .line 142
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 143
    new-instance v1, Lnet/gree/unitywebview/WebViewPlugin$2;

    invoke-direct {v1, p0}, Lnet/gree/unitywebview/WebViewPlugin$2;-><init>(Lnet/gree/unitywebview/WebViewPlugin;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public EvaluateJS(Ljava/lang/String;)V
    .locals 2

    .line 177
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 178
    new-instance v1, Lnet/gree/unitywebview/WebViewPlugin$4;

    invoke-direct {v1, p0, p1}, Lnet/gree/unitywebview/WebViewPlugin$4;-><init>(Lnet/gree/unitywebview/WebViewPlugin;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public Init(Ljava/lang/String;)V
    .locals 2

    .line 37
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 38
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    iput-object v1, p0, Lnet/gree/unitywebview/WebViewPlugin;->cookies:Ljava/util/HashMap;

    .line 39
    new-instance v1, Lnet/gree/unitywebview/WebViewPlugin$1;

    invoke-direct {v1, p0, v0, p1}, Lnet/gree/unitywebview/WebViewPlugin$1;-><init>(Lnet/gree/unitywebview/WebViewPlugin;Landroid/app/Activity;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public LoadURL(Ljava/lang/String;)V
    .locals 2

    .line 161
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 162
    new-instance v1, Lnet/gree/unitywebview/WebViewPlugin$3;

    invoke-direct {v1, p0, p1}, Lnet/gree/unitywebview/WebViewPlugin$3;-><init>(Lnet/gree/unitywebview/WebViewPlugin;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public SetCookie(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 238
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 239
    invoke-static {v0}, Landroid/webkit/CookieSyncManager;->createInstance(Landroid/content/Context;)Landroid/webkit/CookieSyncManager;

    .line 240
    invoke-static {}, Landroid/webkit/CookieManager;->getInstance()Landroid/webkit/CookieManager;

    move-result-object v0

    .line 241
    invoke-virtual {v0, p1, p2}, Landroid/webkit/CookieManager;->setCookie(Ljava/lang/String;Ljava/lang/String;)V

    .line 242
    invoke-static {}, Landroid/webkit/CookieSyncManager;->getInstance()Landroid/webkit/CookieSyncManager;

    move-result-object v0

    invoke-virtual {v0}, Landroid/webkit/CookieSyncManager;->sync()V

    .line 243
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin;->cookies:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 245
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin;->cookies:Ljava/util/HashMap;

    invoke-virtual {v0, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/ArrayList;

    .line 246
    invoke-virtual {p1, p2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 250
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 251
    invoke-virtual {v0, p2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 252
    iget-object p2, p0, Lnet/gree/unitywebview/WebViewPlugin;->cookies:Ljava/util/HashMap;

    invoke-virtual {p2, p1, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :goto_0
    return-void
.end method

.method public SetMargins(IIII)V
    .locals 3

    .line 191
    iput p1, p0, Lnet/gree/unitywebview/WebViewPlugin;->leftMargin:I

    .line 192
    iput p2, p0, Lnet/gree/unitywebview/WebViewPlugin;->topMargin:I

    .line 193
    iput p3, p0, Lnet/gree/unitywebview/WebViewPlugin;->rightMargin:I

    .line 194
    iput p4, p0, Lnet/gree/unitywebview/WebViewPlugin;->bottomMargin:I

    .line 195
    new-instance v0, Landroid/widget/FrameLayout$LayoutParams;

    const/4 v1, -0x1

    const/4 v2, 0x0

    invoke-direct {v0, v1, v1, v2}, Landroid/widget/FrameLayout$LayoutParams;-><init>(III)V

    .line 198
    invoke-virtual {v0, p1, p2, p3, p4}, Landroid/widget/FrameLayout$LayoutParams;->setMargins(IIII)V

    .line 200
    sget-object p1, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 201
    new-instance p2, Lnet/gree/unitywebview/WebViewPlugin$5;

    invoke-direct {p2, p0, v0}, Lnet/gree/unitywebview/WebViewPlugin$5;-><init>(Lnet/gree/unitywebview/WebViewPlugin;Landroid/widget/FrameLayout$LayoutParams;)V

    invoke-virtual {p1, p2}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public SetVisibility(Z)V
    .locals 2

    .line 214
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 215
    new-instance v1, Lnet/gree/unitywebview/WebViewPlugin$6;

    invoke-direct {v1, p0, p1}, Lnet/gree/unitywebview/WebViewPlugin$6;-><init>(Lnet/gree/unitywebview/WebViewPlugin;Z)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method
