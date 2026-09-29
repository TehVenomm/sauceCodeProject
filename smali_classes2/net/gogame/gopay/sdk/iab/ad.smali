.class final Lnet/gogame/gopay/sdk/iab/ad;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:I

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/ac;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/ac;I)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/ad;->b:Lnet/gogame/gopay/sdk/iab/ac;

    iput p2, p0, Lnet/gogame/gopay/sdk/iab/ad;->a:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ad;->b:Lnet/gogame/gopay/sdk/iab/ac;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x0

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ad;->b:Lnet/gogame/gopay/sdk/iab/ac;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ad;->b:Lnet/gogame/gopay/sdk/iab/ac;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/ad;->b:Lnet/gogame/gopay/sdk/iab/ac;

    iget-object p2, p2, Lnet/gogame/gopay/sdk/iab/ac;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->z(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)Lnet/gogame/gopay/sdk/iab/bs;

    move-result-object p2

    iget v0, p0, Lnet/gogame/gopay/sdk/iab/ad;->a:I

    invoke-virtual {p2, v0}, Lnet/gogame/gopay/sdk/iab/bs;->b(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/Integer;->intValue()I

    move-result p2

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;I)V

    return-void
.end method
