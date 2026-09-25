.class final Lnet/gogame/gopay/sdk/iab/bb;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnCancelListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/f;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/f;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bb;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/bb;->a:Lnet/gogame/gopay/sdk/f;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onCancel(Landroid/content/DialogInterface;)V
    .locals 2

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bb;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bb;->a:Lnet/gogame/gopay/sdk/f;

    iget v0, v0, Lnet/gogame/gopay/sdk/f;->a:I

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bb;->a:Lnet/gogame/gopay/sdk/f;

    iget-object v1, v1, Lnet/gogame/gopay/sdk/f;->c:Ljava/lang/String;

    invoke-static {p1, v0, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ILjava/lang/String;)V

    return-void
.end method
