.class final Lnet/gogame/gopay/sdk/iab/q;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/widget/AdapterView$OnItemSelectedListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onItemSelected(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->w(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I

    move-result p1

    if-ne p1, p3, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_0

    return-void

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-nez p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    new-instance p2, Lnet/gogame/gopay/sdk/iab/s;

    invoke-direct {p2, p0, p3}, Lnet/gogame/gopay/sdk/iab/s;-><init>(Lnet/gogame/gopay/sdk/iab/q;I)V

    new-instance p3, Lnet/gogame/gopay/sdk/iab/t;

    invoke-direct {p3, p0}, Lnet/gogame/gopay/sdk/iab/t;-><init>(Lnet/gogame/gopay/sdk/iab/q;)V

    invoke-static {p1, p2, p3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/content/DialogInterface$OnClickListener;Landroid/content/DialogInterface$OnClickListener;)V

    return-void

    :cond_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)I

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->n(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x1

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const-string p4, "paymentMethod"

    invoke-static {p2, p4}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iget-object p4, p0, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p4}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->q(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/b;

    move-result-object p4

    invoke-virtual {p4, p3}, Lnet/gogame/gopay/sdk/iab/b;->getItem(I)Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Lnet/gogame/gopay/sdk/k;

    iget-object p3, p3, Lnet/gogame/gopay/sdk/k;->a:Ljava/util/List;

    invoke-static {p1, p2, p3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;Ljava/util/List;)V

    return-void
.end method

.method public final onNothingSelected(Landroid/widget/AdapterView;)V
    .locals 0

    return-void
.end method
