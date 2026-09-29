.class final Lnet/gogame/gopay/sdk/iab/bk;
.super Landroid/os/AsyncTask;


# instance fields
.field a:Lorg/onepf/oms/appstore/googleUtils/IabResult;

.field final synthetic b:Z

.field final synthetic c:Lnet/gogame/gopay/sdk/iab/bq;

.field final synthetic d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ZLnet/gogame/gopay/sdk/iab/bq;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iput-boolean p2, p0, Lnet/gogame/gopay/sdk/iab/bk;->b:Z

    iput-object p3, p0, Lnet/gogame/gopay/sdk/iab/bk;->c:Lnet/gogame/gopay/sdk/iab/bq;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method

.method private varargs a()Lnet/gogame/gopay/sdk/g;
    .locals 5

    :try_start_0
    invoke-static {}, Lnet/gogame/gopay/sdk/j;->a()Ljava/lang/String;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v1

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->e(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->f(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/String;

    move-result-object v3

    iget-boolean v4, p0, Lnet/gogame/gopay/sdk/iab/bk;->b:Z

    xor-int/lit8 v4, v4, 0x1

    invoke-static {v1, v2, v3, v0, v4}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)Lnet/gogame/gopay/sdk/g;

    move-result-object v0
    :try_end_0
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/googleUtils/IabException;->getResult()Lorg/onepf/oms/appstore/googleUtils/IabResult;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->a:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v0, 0x0

    return-object v0
.end method


# virtual methods
.method protected final synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/bk;->a()Lnet/gogame/gopay/sdk/g;

    move-result-object p1

    return-object p1
.end method

.method protected final synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 2

    check-cast p1, Lnet/gogame/gopay/sdk/g;

    invoke-super {p0, p1}, Landroid/os/AsyncTask;->onPostExecute(Ljava/lang/Object;)V

    if-eqz p1, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object v1, p1, Lnet/gogame/gopay/sdk/g;->f:Lnet/gogame/gopay/sdk/f;

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/f;)Z

    move-result v0

    if-nez v0, :cond_4

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->g(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I

    move-result v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->h(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I

    move-result v1

    if-gt v0, v1, :cond_5

    if-eqz p1, :cond_5

    iget-object v0, p1, Lnet/gogame/gopay/sdk/g;->c:Ljava/util/List;

    if-eqz v0, :cond_5

    iget-object v0, p1, Lnet/gogame/gopay/sdk/g;->c:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_1

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->b:Z

    if-nez v0, :cond_5

    :cond_1
    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->b:Z

    if-nez v0, :cond_2

    iget-object v0, p1, Lnet/gogame/gopay/sdk/g;->e:Ljava/util/List;

    if-eqz v0, :cond_5

    iget-object v0, p1, Lnet/gogame/gopay/sdk/g;->e:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_2

    goto :goto_0

    :cond_2
    iget-object v0, p1, Lnet/gogame/gopay/sdk/g;->c:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_3

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->b:Z

    if-nez v0, :cond_3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->i(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I

    iget-object p1, p1, Lnet/gogame/gopay/sdk/g;->e:Ljava/util/List;

    const/4 v0, 0x0

    invoke-interface {p1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gopay/sdk/Country;

    invoke-virtual {p1}, Lnet/gogame/gopay/sdk/Country;->getCode()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->c:Lnet/gogame/gopay/sdk/iab/bq;

    invoke-static {p1, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/iab/bq;)V

    return-void

    :cond_3
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->j(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I

    iget-object v0, p1, Lnet/gogame/gopay/sdk/g;->a:Ljava/lang/String;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lnet/gogame/gopay/sdk/iab/bl;

    invoke-direct {v1, p0, p1}, Lnet/gogame/gopay/sdk/iab/bl;-><init>(Lnet/gogame/gopay/sdk/iab/bk;Lnet/gogame/gopay/sdk/g;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    :cond_4
    return-void

    :cond_5
    :goto_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bk;->a:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    if-nez p1, :cond_6

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v0, -0x3ea

    const-string v1, "GetProductDetails Bad Response!"

    invoke-direct {p1, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bk;->a:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    :cond_6
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bk;->d:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bk;->a:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->getResponse()I

    move-result v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bk;->a:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    invoke-virtual {v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-static {p1, v0, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ILjava/lang/String;)V

    return-void
.end method
