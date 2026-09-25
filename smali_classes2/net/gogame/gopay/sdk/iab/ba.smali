.class final Lnet/gogame/gopay/sdk/iab/ba;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/ay;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/ay;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/ba;->a:Lnet/gogame/gopay/sdk/iab/ay;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onClick(Landroid/content/DialogInterface;I)V
    .locals 1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/ba;->a:Lnet/gogame/gopay/sdk/iab/ay;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/iab/ay;->c:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/ba;->a:Lnet/gogame/gopay/sdk/iab/ay;

    iget p2, p2, Lnet/gogame/gopay/sdk/iab/ay;->a:I

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/ba;->a:Lnet/gogame/gopay/sdk/iab/ay;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/ay;->b:Ljava/lang/String;

    invoke-static {p1, p2, v0}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->b(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;ILjava/lang/String;)V

    return-void
.end method
