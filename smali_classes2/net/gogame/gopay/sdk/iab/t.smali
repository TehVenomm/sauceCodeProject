.class final Lnet/gogame/gopay/sdk/iab/t;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/q;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/q;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/t;->a:Lnet/gogame/gopay/sdk/iab/q;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/t;->a:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->p(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Landroid/widget/Spinner;

    move-result-object p1

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/t;->a:Lnet/gogame/gopay/sdk/iab/q;

    iget-object p2, p2, Lnet/gogame/gopay/sdk/iab/q;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->w(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)I

    move-result p2

    invoke-virtual {p1, p2}, Landroid/widget/Spinner;->setSelection(I)V

    return-void
.end method
