.class Lnet/gree/unitywebview/WebViewPlugin$4;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gree/unitywebview/WebViewPlugin;->EvaluateJS(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gree/unitywebview/WebViewPlugin;

.field final synthetic val$js:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gree/unitywebview/WebViewPlugin;Ljava/lang/String;)V
    .locals 0

    .line 179
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPlugin$4;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    iput-object p2, p0, Lnet/gree/unitywebview/WebViewPlugin$4;->val$js:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 182
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$4;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 183
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$4;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "javascript:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lnet/gree/unitywebview/WebViewPlugin$4;->val$js:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    :cond_0
    return-void
.end method
