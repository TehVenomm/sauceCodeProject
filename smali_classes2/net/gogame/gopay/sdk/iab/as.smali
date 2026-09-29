.class final Lnet/gogame/gopay/sdk/iab/as;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/iab/bq;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final a(Lnet/gogame/gopay/sdk/g;)V
    .locals 4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->q(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/b;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const-string v2, "paymentType"

    invoke-static {v1, v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iget-object p1, p1, Lnet/gogame/gopay/sdk/g;->c:Ljava/util/List;

    invoke-virtual {v0, v1, p1}, Lnet/gogame/gopay/sdk/iab/b;->a(Ljava/lang/String;Ljava/util/List;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->p(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Spinner;

    move-result-object p1

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/widget/Spinner;->setSelection(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 v1, 0x1

    invoke-static {p1, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)I

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const-string v3, "paymentMethod"

    invoke-static {v2, v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->q(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/b;

    move-result-object v3

    invoke-virtual {v3, v0}, Lnet/gogame/gopay/sdk/iab/b;->getItem(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lnet/gogame/gopay/sdk/k;

    iget-object v3, v3, Lnet/gogame/gopay/sdk/k;->a:Ljava/util/List;

    invoke-static {p1, v2, v3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;Ljava/util/List;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->v(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Spinner;

    move-result-object p1

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->I(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/d;

    move-result-object v2

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/d;->getCount()I

    move-result v2

    if-le v2, v1, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    invoke-virtual {p1, v2}, Landroid/widget/Spinner;->setEnabled(Z)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->p(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Spinner;

    move-result-object p1

    iget-object v2, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->q(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/b;

    move-result-object v2

    invoke-virtual {v2}, Lnet/gogame/gopay/sdk/iab/b;->getCount()I

    move-result v2

    if-le v2, v1, :cond_1

    const/4 v0, 0x1

    :cond_1
    invoke-virtual {p1, v0}, Landroid/widget/Spinner;->setEnabled(Z)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Button;

    move-result-object p1

    if-eqz p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/as;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Button;

    move-result-object p1

    invoke-virtual {p1, v1}, Landroid/widget/Button;->setEnabled(Z)V

    :cond_2
    return-void
.end method
