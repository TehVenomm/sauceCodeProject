.class final Lnet/gogame/gopay/sdk/iab/bj;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/support/r;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/g;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/bi;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/bi;Lnet/gogame/gopay/sdk/g;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bj;->b:Lnet/gogame/gopay/sdk/iab/bi;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/bj;->a:Lnet/gogame/gopay/sdk/g;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final a()V
    .locals 4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bj;->b:Lnet/gogame/gopay/sdk/iab/bi;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/bi;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v0

    iget v0, v0, Landroid/content/res/Configuration;->orientation:I

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bj;->b:Lnet/gogame/gopay/sdk/iab/bi;

    iget-object v1, v1, Lnet/gogame/gopay/sdk/iab/bi;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/bj;->a:Lnet/gogame/gopay/sdk/g;

    const/4 v3, 0x1

    if-ne v0, v3, :cond_0

    goto :goto_0

    :cond_0
    const/4 v3, 0x0

    :goto_0
    invoke-static {v1, v2, v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/g;Z)V

    return-void
.end method
