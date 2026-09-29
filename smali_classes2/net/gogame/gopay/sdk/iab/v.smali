.class final Lnet/gogame/gopay/sdk/iab/v;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/u;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/u;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/v;->a:Lnet/gogame/gopay/sdk/iab/u;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/v;->a:Lnet/gogame/gopay/sdk/iab/u;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/u;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x0

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/v;->a:Lnet/gogame/gopay/sdk/iab/u;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/u;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->c(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/v;->a:Lnet/gogame/gopay/sdk/iab/u;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/u;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    const/4 p2, 0x1

    invoke-static {p1, p2}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Z)V

    return-void
.end method
