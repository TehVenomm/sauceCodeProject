.class final Lnet/gogame/gopay/sdk/iab/au;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/n;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/at;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/at;Lnet/gogame/gopay/sdk/n;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/au;->b:Lnet/gogame/gopay/sdk/iab/at;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/au;->a:Lnet/gogame/gopay/sdk/n;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/au;->b:Lnet/gogame/gopay/sdk/iab/at;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/at;->b:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/au;->a:Lnet/gogame/gopay/sdk/n;

    invoke-static {v0, v1}, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;->a(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;Lnet/gogame/gopay/sdk/n;)V

    return-void
.end method
