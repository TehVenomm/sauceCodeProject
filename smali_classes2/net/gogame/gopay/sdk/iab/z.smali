.class final Lnet/gogame/gopay/sdk/iab/z;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/y;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/y;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->s(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->u(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-nez p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->t(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    new-instance v0, Lnet/gogame/gopay/sdk/iab/aa;

    invoke-direct {v0, p0, p2}, Lnet/gogame/gopay/sdk/iab/aa;-><init>(Lnet/gogame/gopay/sdk/iab/z;I)V

    new-instance p2, Lnet/gogame/gopay/sdk/iab/ab;

    invoke-direct {p2, p0}, Lnet/gogame/gopay/sdk/iab/ab;-><init>(Lnet/gogame/gopay/sdk/iab/z;)V

    invoke-static {p1, v0, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/content/DialogInterface$OnClickListener;Landroid/content/DialogInterface$OnClickListener;)V

    return-void

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 v0, 0x0

    invoke-static {p1, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->d(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Button;

    move-result-object p1

    const/4 p2, 0x1

    invoke-virtual {p1, p2}, Landroid/widget/Button;->setEnabled(Z)V

    return-void
.end method
