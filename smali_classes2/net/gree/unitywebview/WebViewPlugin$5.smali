.class Lnet/gree/unitywebview/WebViewPlugin$5;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gree/unitywebview/WebViewPlugin;->SetMargins(IIII)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gree/unitywebview/WebViewPlugin;

.field final synthetic val$params:Landroid/widget/FrameLayout$LayoutParams;


# direct methods
.method constructor <init>(Lnet/gree/unitywebview/WebViewPlugin;Landroid/widget/FrameLayout$LayoutParams;)V
    .locals 0

    .line 202
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPlugin$5;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    iput-object p2, p0, Lnet/gree/unitywebview/WebViewPlugin$5;->val$params:Landroid/widget/FrameLayout$LayoutParams;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 205
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$5;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 206
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$5;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$000(Lnet/gree/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    iget-object v1, p0, Lnet/gree/unitywebview/WebViewPlugin$5;->val$params:Landroid/widget/FrameLayout$LayoutParams;

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    :cond_0
    return-void
.end method
