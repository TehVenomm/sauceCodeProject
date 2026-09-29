.class final Lnet/gogame/gopay/sdk/iab/aq;
.super Landroid/webkit/WebViewClient;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Landroid/webkit/WebViewClient;-><init>()V

    return-void
.end method


# virtual methods
.method public final onPageFinished(Landroid/webkit/WebView;Ljava/lang/String;)V
    .locals 1

    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->onPageFinished(Landroid/webkit/WebView;Ljava/lang/String;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 v0, 0x1

    invoke-static {p1, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->n(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    const-string p1, "gopay:///"

    invoke-virtual {p2, p1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public final onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    invoke-super {p0, p1, p2, p3}, Landroid/webkit/WebViewClient;->onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V

    return-void
.end method

.method public final onReceivedError(Landroid/webkit/WebView;ILjava/lang/String;Ljava/lang/String;)V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;

    move-result-object v0

    invoke-virtual {v0, p2, p3}, Lnet/gogame/gopay/sdk/iab/g;->setError(ILjava/lang/String;)V

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;

    move-result-object p2

    invoke-virtual {p2, p4}, Lnet/gogame/gopay/sdk/iab/g;->setFailedUrl(Ljava/lang/String;)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->j()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_0

    const-string p3, "text/html; charset=UTF-8"

    const/4 p4, 0x0

    invoke-virtual {p1, p2, p3, p4}, Landroid/webkit/WebView;->loadData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public final onReceivedError(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceError;)V
    .locals 2
    .annotation build Landroid/annotation/TargetApi;
        value = 0x17
    .end annotation

    invoke-interface {p2}, Landroid/webkit/WebResourceRequest;->isForMainFrame()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p2}, Landroid/webkit/WebResourceRequest;->getUrl()Landroid/net/Uri;

    move-result-object p2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;

    move-result-object v0

    invoke-virtual {p3}, Landroid/webkit/WebResourceError;->getErrorCode()I

    move-result v1

    invoke-virtual {p3}, Landroid/webkit/WebResourceError;->getDescription()Ljava/lang/CharSequence;

    move-result-object p3

    invoke-interface {p3}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-virtual {v0, v1, p3}, Lnet/gogame/gopay/sdk/iab/g;->setError(ILjava/lang/String;)V

    iget-object p3, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;

    move-result-object p3

    invoke-virtual {p2}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p3, v0}, Lnet/gogame/gopay/sdk/iab/g;->setFailedUrl(Ljava/lang/String;)V

    new-instance p3, Ljava/lang/StringBuilder;

    const-string v0, "URL1: "

    invoke-direct {p3, v0}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->j()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_0

    const-string p3, "text/html; charset=UTF-8"

    const/4 v0, 0x0

    invoke-virtual {p1, p2, p3, v0}, Landroid/webkit/WebView;->loadData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public final shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    const-string v0, "gopay:///"

    invoke-virtual {p2, v0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aq;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)V

    const/4 p1, 0x1

    return p1

    :cond_0
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method
