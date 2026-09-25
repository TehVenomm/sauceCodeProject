.class final Lnet/gogame/gopay/sdk/s;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/StoreActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/StoreActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/s;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/s;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/StoreActivity;->e(Lnet/gogame/gopay/sdk/StoreActivity;)Landroid/app/ProgressDialog;

    move-result-object v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/s;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    new-instance v1, Landroid/app/ProgressDialog;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/s;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-direct {v1, v2}, Landroid/app/ProgressDialog;-><init>(Landroid/content/Context;)V

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/StoreActivity;->a(Lnet/gogame/gopay/sdk/StoreActivity;Landroid/app/ProgressDialog;)Landroid/app/ProgressDialog;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/s;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/StoreActivity;->e(Lnet/gogame/gopay/sdk/StoreActivity;)Landroid/app/ProgressDialog;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/app/ProgressDialog;->setCancelable(Z)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/s;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/StoreActivity;->e(Lnet/gogame/gopay/sdk/StoreActivity;)Landroid/app/ProgressDialog;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/app/ProgressDialog;->setIndeterminate(Z)V

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/s;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/StoreActivity;->e(Lnet/gogame/gopay/sdk/StoreActivity;)Landroid/app/ProgressDialog;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/ProgressDialog;->show()V

    return-void
.end method
