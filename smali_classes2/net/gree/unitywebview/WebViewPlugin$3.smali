.class Lnet/gree/unitywebview/WebViewPlugin$3;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gree/unitywebview/WebViewPlugin;->LoadURL(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gree/unitywebview/WebViewPlugin;

.field final synthetic val$url:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gree/unitywebview/WebViewPlugin;Ljava/lang/String;)V
    .locals 0

    .line 163
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPlugin$3;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    iput-object p2, p0, Lnet/gree/unitywebview/WebViewPlugin$3;->val$url:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 166
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$3;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 167
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$3;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    invoke-virtual {v0}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v0

    const-string v1, "utf-8"

    .line 168
    invoke-virtual {v0, v1}, Landroid/webkit/WebSettings;->setDefaultTextEncodingName(Ljava/lang/String;)V

    .line 169
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$3;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    iget-object v1, p0, Lnet/gree/unitywebview/WebViewPlugin$3;->val$url:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    :cond_0
    return-void
.end method
