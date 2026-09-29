.class final Lnet/gogame/gopay/sdk/iab/ar;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnTouchListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/ar;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onTouch(Landroid/view/View;Landroid/view/MotionEvent;)Z
    .locals 2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ar;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ar;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->o(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-nez p1, :cond_1

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ar;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->F(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/br;

    move-result-object p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ar;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x1

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ar;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->k(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/os/Handler;

    move-result-object p1

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/ar;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->G(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Ljava/lang/Runnable;

    move-result-object p2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/ar;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->H(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I

    move-result v0

    int-to-long v0, v0

    invoke-virtual {p1, p2, v0, v1}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    :cond_1
    const/4 p1, 0x0

    return p1
.end method
