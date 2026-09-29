.class Lnet/gree/unitywebview/WebViewPlugin$1;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gree/unitywebview/WebViewPlugin;->Init(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gree/unitywebview/WebViewPlugin;

.field final synthetic val$a:Landroid/app/Activity;

.field final synthetic val$gameObject:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gree/unitywebview/WebViewPlugin;Landroid/app/Activity;Ljava/lang/String;)V
    .locals 0

    .line 40
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    iput-object p2, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->val$a:Landroid/app/Activity;

    iput-object p3, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->val$gameObject:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 6
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "WrongConstant"
        }
    .end annotation

    .line 44
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    new-instance v1, Landroid/webkit/WebView;

    iget-object v2, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->val$a:Landroid/app/Activity;

    invoke-direct {v1, v2}, Landroid/webkit/WebView;-><init>(Landroid/content/Context;)V

    invoke-static {v0, v1}, Lnet/gree/unitywebview/WebViewPlugin;->access$002(Lnet/gree/unitywebview/WebViewPlugin;Landroid/webkit/WebView;)Landroid/webkit/WebView;

    .line 45
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setVisibility(I)V

    .line 46
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setFocusable(Z)V

    .line 47
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setFocusableInTouchMode(Z)V

    .line 48
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$100(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/widget/FrameLayout;

    move-result-object v0

    const/4 v2, -0x1

    if-nez v0, :cond_0

    .line 50
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    new-instance v3, Landroid/widget/FrameLayout;

    iget-object v4, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->val$a:Landroid/app/Activity;

    invoke-direct {v3, v4}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    invoke-static {v0, v3}, Lnet/gree/unitywebview/WebViewPlugin;->access$102(Lnet/gree/unitywebview/WebViewPlugin;Landroid/widget/FrameLayout;)Landroid/widget/FrameLayout;

    .line 51
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->val$a:Landroid/app/Activity;

    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v3}, Lnet/gree/unitywebview/WebViewPlugin;->access$100(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/widget/FrameLayout;

    move-result-object v3

    new-instance v4, Landroid/view/ViewGroup$LayoutParams;

    invoke-direct {v4, v2, v2}, Landroid/view/ViewGroup$LayoutParams;-><init>(II)V

    invoke-virtual {v0, v3, v4}, Landroid/app/Activity;->addContentView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    .line 53
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$100(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/widget/FrameLayout;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/widget/FrameLayout;->setFocusable(Z)V

    .line 54
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$100(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/widget/FrameLayout;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/widget/FrameLayout;->setFocusableInTouchMode(Z)V

    .line 56
    :cond_0
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$100(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/widget/FrameLayout;

    move-result-object v0

    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v3}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v3

    new-instance v4, Landroid/widget/FrameLayout$LayoutParams;

    const/4 v5, 0x0

    invoke-direct {v4, v2, v2, v5}, Landroid/widget/FrameLayout$LayoutParams;-><init>(III)V

    invoke-virtual {v0, v3, v4}, Landroid/widget/FrameLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    .line 60
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    new-instance v2, Landroid/webkit/WebChromeClient;

    invoke-direct {v2}, Landroid/webkit/WebChromeClient;-><init>()V

    invoke-virtual {v0, v2}, Landroid/webkit/WebView;->setWebChromeClient(Landroid/webkit/WebChromeClient;)V

    .line 61
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    new-instance v2, Lnet/gree/unitywebview/WebViewPlugin$1$1;

    invoke-direct {v2, p0}, Lnet/gree/unitywebview/WebViewPlugin$1$1;-><init>(Lnet/gree/unitywebview/WebViewPlugin$1;)V

    invoke-virtual {v0, v2}, Landroid/webkit/WebView;->setWebViewClient(Landroid/webkit/WebViewClient;)V

    .line 128
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    new-instance v2, Lnet/gree/unitywebview/WebViewPluginInterface;

    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->val$gameObject:Ljava/lang/String;

    invoke-direct {v2, v3}, Lnet/gree/unitywebview/WebViewPluginInterface;-><init>(Ljava/lang/String;)V

    const-string v3, "Unity"

    invoke-virtual {v0, v2, v3}, Landroid/webkit/WebView;->addJavascriptInterface(Ljava/lang/Object;Ljava/lang/String;)V

    .line 131
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    invoke-virtual {v0}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v0

    .line 132
    invoke-virtual {v0, v5}, Landroid/webkit/WebSettings;->setSupportZoom(Z)V

    .line 133
    invoke-virtual {v0, v1}, Landroid/webkit/WebSettings;->setJavaScriptEnabled(Z)V

    .line 134
    sget-object v1, Landroid/webkit/WebSettings$PluginState;->ON:Landroid/webkit/WebSettings$PluginState;

    invoke-virtual {v0, v1}, Landroid/webkit/WebSettings;->setPluginState(Landroid/webkit/WebSettings$PluginState;)V

    return-void
.end method
