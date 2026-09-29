.class final Lnet/gogame/gopay/sdk/iab/ak;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:I

.field final synthetic b:Landroid/view/View;

.field final synthetic c:Lnet/gogame/gopay/sdk/iab/aj;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/aj;ILandroid/view/View;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iput p2, p0, Lnet/gogame/gopay/sdk/iab/ak;->a:I

    iput-object p3, p0, Lnet/gogame/gopay/sdk/iab/ak;->b:Landroid/view/View;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/aj;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x0

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/aj;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/aj;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p1

    iget p2, p0, Lnet/gogame/gopay/sdk/iab/ak;->a:I

    iput p2, p1, Lnet/gogame/gopay/sdk/iab/i;->e:I

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/aj;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p1

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/aj;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p1

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    const/16 p2, 0xf1

    invoke-static {p2, p2, p2}, Landroid/graphics/Color;->rgb(III)I

    move-result p2

    invoke-virtual {p1, p2}, Landroid/view/View;->setBackgroundColor(I)V

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/aj;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p1

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/ak;->b:Landroid/view/View;

    iput-object p2, p1, Lnet/gogame/gopay/sdk/iab/i;->f:Landroid/view/View;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->b:Landroid/view/View;

    const/4 p2, -0x1

    invoke-virtual {p1, p2}, Landroid/view/View;->setBackgroundColor(I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/aj;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/ak;->c:Lnet/gogame/gopay/sdk/iab/aj;

    iget-object p2, p2, Lnet/gogame/gopay/sdk/iab/aj;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object p2

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/ak;->a:I

    invoke-virtual {p2, v0}, Lnet/gogame/gopay/sdk/iab/i;->getItem(I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lnet/gogame/gopay/sdk/iab/a;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/iab/a;)V

    return-void
.end method
