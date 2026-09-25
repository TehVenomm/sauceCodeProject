.class Lnet/gree/unitywebview/WebViewPlugin$2;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gree/unitywebview/WebViewPlugin;->Destroy()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gree/unitywebview/WebViewPlugin;


# direct methods
.method constructor <init>(Lnet/gree/unitywebview/WebViewPlugin;)V
    .locals 0

    .line 144
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPlugin$2;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 147
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$2;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 149
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$2;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$100(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/widget/FrameLayout;

    move-result-object v0

    iget-object v2, p0, Lnet/gree/unitywebview/WebViewPlugin$2;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v2}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v2

    invoke-virtual {v0, v2}, Landroid/widget/FrameLayout;->removeView(Landroid/view/View;)V

    .line 150
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$2;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0, v1}, Lnet/gree/unitywebview/WebViewPlugin;->access$002(Lnet/gree/unitywebview/WebViewPlugin;Landroid/webkit/WebView;)Landroid/webkit/WebView;

    .line 152
    :cond_0
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$2;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$100(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/widget/FrameLayout;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 153
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$2;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0, v1}, Lnet/gree/unitywebview/WebViewPlugin;->access$102(Lnet/gree/unitywebview/WebViewPlugin;Landroid/widget/FrameLayout;)Landroid/widget/FrameLayout;

    :cond_1
    return-void
.end method
