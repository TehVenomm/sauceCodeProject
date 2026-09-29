.class final Lnet/gogame/gopay/sdk/iab/ag;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/widget/AdapterView$OnItemClickListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-nez p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    new-instance p4, Lnet/gogame/gopay/sdk/iab/ah;

    invoke-direct {p4, p0, p3, p2}, Lnet/gogame/gopay/sdk/iab/ah;-><init>(Lnet/gogame/gopay/sdk/iab/ag;ILandroid/view/View;)V

    new-instance p2, Lnet/gogame/gopay/sdk/iab/ai;

    invoke-direct {p2, p0}, Lnet/gogame/gopay/sdk/iab/ai;-><init>(Lnet/gogame/gopay/sdk/iab/ag;)V

    invoke-static {p1, p4, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/content/DialogInterface$OnClickListener;Landroid/content/DialogInterface$OnClickListener;)V

    return-void

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p4, 0x0

    invoke-static {p1, p4}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p1

    iput p3, p1, Lnet/gogame/gopay/sdk/iab/i;->e:I

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p1

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p1

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    const/16 p4, 0xf1

    invoke-static {p4, p4, p4}, Landroid/graphics/Color;->rgb(III)I

    move-result p4

    invoke-virtual {p1, p4}, Landroid/view/View;->setBackgroundColor(I)V

    :cond_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p1

    iput-object p2, p1, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    const/4 p1, -0x1

    invoke-virtual {p2, p1}, Landroid/view/View;->setBackgroundColor(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/ag;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p2

    invoke-virtual {p2, p3}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lnet/gogame/gopay/sdk/iab/a;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/iab/a;)V

    return-void
.end method
