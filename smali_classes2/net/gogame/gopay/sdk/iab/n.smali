.class final Lnet/gogame/gopay/sdk/iab/n;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:I

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/m;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/m;I)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/n;->b:Lnet/gogame/gopay/sdk/iab/m;

    iput p2, p0, Lnet/gogame/gopay/sdk/iab/n;->a:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/n;->b:Lnet/gogame/gopay/sdk/iab/m;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x0

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/n;->b:Lnet/gogame/gopay/sdk/iab/m;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/n;->b:Lnet/gogame/gopay/sdk/iab/m;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/m;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget p2, p0, Lnet/gogame/gopay/sdk/iab/n;->a:I

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)V

    return-void
.end method
