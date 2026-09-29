.class final Lnet/gogame/gopay/sdk/iab/an;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/iab/h;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final a()V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->A(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/an;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lnet/gogame/gopay/sdk/iab/ao;

    invoke-direct {v1, p0}, Lnet/gogame/gopay/sdk/iab/ao;-><init>(Lnet/gogame/gopay/sdk/iab/an;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
