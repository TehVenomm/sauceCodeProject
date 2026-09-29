.class final Lnet/gogame/gopay/sdk/iab/at;
.super Landroid/os/AsyncTask;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/a;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/iab/a;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/at;->a:Lnet/gogame/gopay/sdk/iab/a;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method

.method private varargs a()Ljava/lang/Void;
    .locals 10

    const/4 v0, 0x0

    :try_start_0
    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-result-object v1

    if-eqz v1, :cond_0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->K(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->L(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v2

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v3

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->J(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-result-object v4

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->K(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v5

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->L(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v6

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->a:Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {v1}, Lnet/gogame/gopay/sdk/iab/a;->getId()Ljava/lang/String;

    move-result-object v7

    invoke-static {}, Lnet/gogame/gopay/sdk/j;->a()Ljava/lang/String;

    move-result-object v8

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->M(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v9

    invoke-static/range {v2 .. v9}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/SkuDetails;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/n;

    move-result-object v1

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/Handler;

    move-result-object v2

    new-instance v3, Lnet/gogame/gopay/sdk/iab/au;

    invoke-direct {v3, p0, v1}, Lnet/gogame/gopay/sdk/iab/au;-><init>(Lnet/gogame/gopay/sdk/iab/at;Lnet/gogame/gopay/sdk/n;)V

    :goto_0
    invoke-virtual {v2, v3}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    goto/16 :goto_1

    :cond_0
    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v2

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v3

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->f(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v4

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->a:Lnet/gogame/gopay/sdk/iab/a;

    invoke-virtual {v1}, Lnet/gogame/gopay/sdk/iab/a;->getId()Ljava/lang/String;

    move-result-object v5

    invoke-static {}, Lnet/gogame/gopay/sdk/j;->a()Ljava/lang/String;

    move-result-object v6

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->M(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v7

    invoke-static/range {v2 .. v7}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/n;

    move-result-object v1

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/Handler;

    move-result-object v2

    new-instance v3, Lnet/gogame/gopay/sdk/iab/av;

    invoke-direct {v3, p0, v1}, Lnet/gogame/gopay/sdk/iab/av;-><init>(Lnet/gogame/gopay/sdk/iab/at;Lnet/gogame/gopay/sdk/n;)V
    :try_end_0
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result v1

    if-nez v1, :cond_2

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->o(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result v1

    if-eqz v1, :cond_1

    goto :goto_1

    :cond_1
    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->E(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 v2, 0x1

    invoke-static {v1, v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->n(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;

    move-result-object v1

    const/16 v2, 0x194

    const-string v3, "This Payment Method is not working at the moment. Please try another Payment Method."

    invoke-virtual {v1, v2, v3}, Lnet/gogame/gopay/sdk/iab/g;->setError(ILjava/lang/String;)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->B(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/g;

    move-result-object v1

    invoke-virtual {v1, v0}, Lnet/gogame/gopay/sdk/iab/g;->setFailedUrl(Ljava/lang/String;)V

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->j()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_2

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/Handler;

    move-result-object v2

    new-instance v3, Lnet/gogame/gopay/sdk/iab/aw;

    invoke-direct {v3, p0, v1}, Lnet/gogame/gopay/sdk/iab/aw;-><init>(Lnet/gogame/gopay/sdk/iab/at;Ljava/lang/String;)V

    invoke-virtual {v2, v3}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    :cond_2
    :goto_1
    return-object v0
.end method


# virtual methods
.method protected final synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/at;->a()Ljava/lang/Void;

    move-result-object p1

    return-object p1
.end method

.method protected final synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->N(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/AsyncTask;

    return-void
.end method

.method protected final onPreExecute()V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/Handler;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/Runnable;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    return-void
.end method
