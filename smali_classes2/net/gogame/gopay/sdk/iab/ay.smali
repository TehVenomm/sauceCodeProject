.class final Lnet/gogame/gopay/sdk/iab/ay;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:I

.field final synthetic b:Ljava/lang/String;

.field final synthetic c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ILjava/lang/String;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/ay;->c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iput p2, p0, Lnet/gogame/gopay/sdk/iab/ay;->a:I

    iput-object p3, p0, Lnet/gogame/gopay/sdk/iab/ay;->b:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 3

    new-instance v0, Landroid/widget/FrameLayout;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/ay;->c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {v0, v1}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/FrameLayout;->setBackgroundColor(I)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/ay;->c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-virtual {v1, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->setContentView(Landroid/view/View;)V

    new-instance v0, Landroid/app/AlertDialog$Builder;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/ay;->c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {v0, v1}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    const-string v1, "Store Error"

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    const-string v1, "Cannot Connect to Store, Please try again."

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    const-string v1, "Dismiss"

    new-instance v2, Lnet/gogame/gopay/sdk/iab/ba;

    invoke-direct {v2, p0}, Lnet/gogame/gopay/sdk/iab/ba;-><init>(Lnet/gogame/gopay/sdk/iab/ay;)V

    invoke-virtual {v0, v1, v2}, Landroid/app/AlertDialog$Builder;->setNegativeButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    new-instance v1, Lnet/gogame/gopay/sdk/iab/az;

    invoke-direct {v1, p0}, Lnet/gogame/gopay/sdk/iab/az;-><init>(Lnet/gogame/gopay/sdk/iab/ay;)V

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setOnCancelListener(Landroid/content/DialogInterface$OnCancelListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    return-void
.end method
