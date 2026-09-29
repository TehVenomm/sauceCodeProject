.class final Lnet/gogame/gopay/sdk/iab/s;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:I

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/q;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/q;I)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iput p2, p0, Lnet/gogame/gopay/sdk/iab/s;->a:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x0

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget p2, p0, Lnet/gogame/gopay/sdk/iab/s;->a:I

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)I

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->n(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x1

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p2, p2, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const-string v0, "paymentMethod"

    invoke-static {p2, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/s;->b:Lnet/gogame/gopay/sdk/iab/q;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->q(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/b;

    move-result-object v0

    iget v1, p0, Lnet/gogame/gopay/sdk/iab/s;->a:I

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/sdk/iab/b;->getItem(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gopay/sdk/k;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/k;->a:Ljava/util/List;

    invoke-static {p1, p2, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;Ljava/util/List;)V

    return-void
.end method
