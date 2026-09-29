.class final Lnet/gogame/gopay/sdk/iab/y;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/view/View;)V
    .locals 3

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Button;

    move-result-object p1

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/widget/Button;->setEnabled(Z)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const-string v0, "Payment Channels"

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->y(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/i;

    move-result-object v1

    new-instance v2, Lnet/gogame/gopay/sdk/iab/z;

    invoke-direct {v2, p0}, Lnet/gogame/gopay/sdk/iab/z;-><init>(Lnet/gogame/gopay/sdk/iab/y;)V

    invoke-static {p1, v0, v1, v2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Ljava/lang/String;Lnet/gogame/gopay/sdk/a;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/Dialog;

    move-result-object p1

    invoke-virtual {p1}, Landroid/app/Dialog;->show()V

    return-void
.end method
