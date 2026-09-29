.class final Lnet/gogame/gopay/sdk/iab/bp;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnTouchListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bp;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bp;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->n(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bp;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->o(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bp;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bp;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/bp;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const-string v0, "paymentMethod"

    invoke-static {p2, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bp;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->q(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/b;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bp;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->p(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Spinner;

    move-result-object v1

    invoke-virtual {v1}, Landroid/widget/Spinner;->getSelectedItemPosition()I

    move-result v1

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/sdk/iab/b;->getItem(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gopay/sdk/k;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/k;->a:Ljava/util/List;

    invoke-static {p1, p2, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;Ljava/util/List;)V

    const/4 p1, 0x1

    return p1

    :cond_0
    return p2
.end method
