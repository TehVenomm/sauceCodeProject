.class final Lnet/gogame/gopay/sdk/iab/bg;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Landroid/app/Dialog;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Landroid/app/Dialog;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bg;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/bg;->a:Landroid/app/Dialog;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/view/View;)V
    .locals 1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bg;->a:Landroid/app/Dialog;

    invoke-virtual {p1}, Landroid/app/Dialog;->dismiss()V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bg;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->x(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Button;

    move-result-object p1

    const/4 v0, 0x1

    invoke-virtual {p1, v0}, Landroid/widget/Button;->setEnabled(Z)V

    return-void
.end method
