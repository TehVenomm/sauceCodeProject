.class final Lnet/gogame/gopay/sdk/iab/m;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/widget/AdapterView$OnItemSelectedListener;


# instance fields
.field a:Z

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p1, 0x1

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/m;->a:Z

    return-void
.end method


# virtual methods
.method public final onItemSelected(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0

    iget-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/m;->a:Z

    if-eqz p1, :cond_0

    const/4 p1, 0x0

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/iab/m;->a:Z

    return-void

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->r(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I

    move-result p1

    if-ne p1, p3, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_1

    return-void

    :cond_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-nez p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    new-instance p2, Lnet/gogame/gopay/sdk/iab/n;

    invoke-direct {p2, p0, p3}, Lnet/gogame/gopay/sdk/iab/n;-><init>(Lnet/gogame/gopay/sdk/iab/m;I)V

    new-instance p3, Lnet/gogame/gopay/sdk/iab/o;

    invoke-direct {p3, p0}, Lnet/gogame/gopay/sdk/iab/o;-><init>(Lnet/gogame/gopay/sdk/iab/m;)V

    invoke-static {p1, p2, p3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/content/DialogInterface$OnClickListener;Landroid/content/DialogInterface$OnClickListener;)V

    return-void

    :cond_2
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p3}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)V

    return-void
.end method

.method public final onNothingSelected(Landroid/widget/AdapterView;)V
    .locals 0

    return-void
.end method
