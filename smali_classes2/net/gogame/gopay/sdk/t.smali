.class final Lnet/gogame/gopay/sdk/t;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/StoreActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/StoreActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/t;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/t;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/StoreActivity;->e(Lnet/gogame/gopay/sdk/StoreActivity;)Landroid/app/ProgressDialog;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/t;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/StoreActivity;->e(Lnet/gogame/gopay/sdk/StoreActivity;)Landroid/app/ProgressDialog;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/ProgressDialog;->hide()V

    :cond_0
    return-void
.end method
