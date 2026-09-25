.class final Lnet/gogame/gopay/sdk/iab/bd;
.super Landroid/os/AsyncTask;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bd;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method

.method private static varargs a()Lnet/gogame/gopay/sdk/iab/bu;
    .locals 1

    :try_start_0
    invoke-static {}, Lnet/gogame/gopay/sdk/j;->b()Lnet/gogame/gopay/sdk/iab/bu;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    const/4 v0, 0x0

    return-object v0
.end method


# virtual methods
.method protected final synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    invoke-static {}, Lnet/gogame/gopay/sdk/iab/bd;->a()Lnet/gogame/gopay/sdk/iab/bu;

    move-result-object p1

    return-object p1
.end method

.method protected final synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 4

    check-cast p1, Lnet/gogame/gopay/sdk/iab/bu;

    if-eqz p1, :cond_1

    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bd;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "_version_"

    const/4 v2, 0x0

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getFloat(Ljava/lang/String;F)F

    move-result v0

    iget v1, p1, Lnet/gogame/gopay/sdk/iab/bu;->a:F

    iget-boolean v2, p1, Lnet/gogame/gopay/sdk/iab/bu;->c:Z

    if-nez v2, :cond_1

    iget-object v2, p1, Lnet/gogame/gopay/sdk/iab/bu;->b:Ljava/lang/String;

    if-eqz v2, :cond_1

    iget-object v2, p1, Lnet/gogame/gopay/sdk/iab/bu;->b:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    if-lez v2, :cond_1

    cmpl-float v0, v1, v0

    if-gtz v0, :cond_0

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->a()Z

    move-result v0

    if-nez v0, :cond_1

    :cond_0
    new-instance v0, Lnet/gogame/gopay/sdk/support/t;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/bd;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getFilesDir()Ljava/io/File;

    move-result-object v2

    invoke-virtual {v2}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object v2

    new-instance v3, Lnet/gogame/gopay/sdk/iab/be;

    invoke-direct {v3, p0, v1}, Lnet/gogame/gopay/sdk/iab/be;-><init>(Lnet/gogame/gopay/sdk/iab/bd;F)V

    invoke-direct {v0, v2, v3}, Lnet/gogame/gopay/sdk/support/t;-><init>(Ljava/lang/String;Lnet/gogame/gopay/sdk/support/u;)V

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/bu;->b:Ljava/lang/String;

    aput-object p1, v1, v2

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/sdk/support/t;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    :cond_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bd;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    return-void
.end method
