.class final Lnet/gogame/gopay/sdk/iab/aa;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:I

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/z;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/z;I)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/aa;->b:Lnet/gogame/gopay/sdk/iab/z;

    iput p2, p0, Lnet/gogame/gopay/sdk/iab/aa;->a:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aa;->b:Lnet/gogame/gopay/sdk/iab/z;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x0

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aa;->b:Lnet/gogame/gopay/sdk/iab/z;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aa;->b:Lnet/gogame/gopay/sdk/iab/z;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget p2, p0, Lnet/gogame/gopay/sdk/iab/aa;->a:I

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/aa;->b:Lnet/gogame/gopay/sdk/iab/z;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/z;->a:Lnet/gogame/gopay/sdk/iab/y;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/y;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Button;

    move-result-object p1

    const/4 p2, 0x1

    invoke-virtual {p1, p2}, Landroid/widget/Button;->setEnabled(Z)V

    return-void
.end method
