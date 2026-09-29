.class final Lnet/gogame/gopay/sdk/iab/bh;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/widget/AdapterView$OnItemClickListener;


# instance fields
.field final synthetic a:Landroid/content/DialogInterface$OnClickListener;

.field final synthetic b:Landroid/app/Dialog;

.field final synthetic c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/content/DialogInterface$OnClickListener;Landroid/app/Dialog;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bh;->c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/bh;->a:Landroid/content/DialogInterface$OnClickListener;

    iput-object p3, p0, Lnet/gogame/gopay/sdk/iab/bh;->b:Landroid/app/Dialog;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bh;->a:Landroid/content/DialogInterface$OnClickListener;

    const/4 p2, 0x0

    invoke-interface {p1, p2, p3}, Landroid/content/DialogInterface$OnClickListener;->onClick(Landroid/content/DialogInterface;I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bh;->c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Button;

    move-result-object p1

    const/4 p2, 0x1

    invoke-virtual {p1, p2}, Landroid/widget/Button;->setEnabled(Z)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bh;->b:Landroid/app/Dialog;

    invoke-virtual {p1}, Landroid/app/Dialog;->dismiss()V

    return-void
.end method
