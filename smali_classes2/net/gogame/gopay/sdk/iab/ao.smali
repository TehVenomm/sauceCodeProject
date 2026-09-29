.class final Lnet/gogame/gopay/sdk/iab/ao;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/an;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/an;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/ao;->a:Lnet/gogame/gopay/sdk/iab/an;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/ao;->a:Lnet/gogame/gopay/sdk/iab/an;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/iab/g;->canRetry()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/ao;->a:Lnet/gogame/gopay/sdk/iab/an;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/webkit/WebView;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/ao;->a:Lnet/gogame/gopay/sdk/iab/an;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->C(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/webkit/WebView;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/ao;->a:Lnet/gogame/gopay/sdk/iab/an;

    iget-object v1, v1, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;

    move-result-object v1

    invoke-virtual {v1}, Lnet/gogame/gopay/sdk/iab/g;->getFailedUrl()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->loadUrl(Ljava/lang/String;)V

    return-void

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/ao;->a:Lnet/gogame/gopay/sdk/iab/an;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 v1, 0x1

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/ao;->a:Lnet/gogame/gopay/sdk/iab/an;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->D(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/br;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/ao;->a:Lnet/gogame/gopay/sdk/iab/an;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->onBackPressed()V

    return-void
.end method
