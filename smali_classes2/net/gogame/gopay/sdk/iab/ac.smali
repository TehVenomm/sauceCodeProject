.class final Lnet/gogame/gopay/sdk/iab/ac;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/widget/AdapterView$OnItemClickListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0

    if-nez p3, :cond_0

    return-void

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->o(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-nez p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-nez p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    new-instance p2, Lnet/gogame/gopay/sdk/iab/ad;

    invoke-direct {p2, p0, p3}, Lnet/gogame/gopay/sdk/iab/ad;-><init>(Lnet/gogame/gopay/sdk/iab/ac;I)V

    new-instance p3, Lnet/gogame/gopay/sdk/iab/ae;

    invoke-direct {p3, p0}, Lnet/gogame/gopay/sdk/iab/ae;-><init>(Lnet/gogame/gopay/sdk/iab/ac;)V

    invoke-static {p1, p2, p3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/content/DialogInterface$OnClickListener;Landroid/content/DialogInterface$OnClickListener;)V

    return-void

    :cond_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x0

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->o(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)V

    :cond_2
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/bs;

    move-result-object p2

    invoke-virtual {p2, p3}, Lnet/gogame/gopay/sdk/iab/bs;->b(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/Integer;->intValue()I

    move-result p2

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)V

    return-void
.end method
