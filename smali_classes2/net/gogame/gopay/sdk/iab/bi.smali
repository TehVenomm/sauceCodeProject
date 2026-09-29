.class final Lnet/gogame/gopay/sdk/iab/bi;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/iab/bq;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/PurchaseActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bi;->a:Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final a(Lnet/gogame/gopay/sdk/g;)V
    .locals 2

    new-instance v0, Lnet/gogame/gopay/sdk/iab/bj;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gopay/sdk/iab/bj;-><init>(Lnet/gogame/gopay/sdk/iab/bi;Lnet/gogame/gopay/sdk/g;)V

    iget-object p1, p1, Lnet/gogame/gopay/sdk/g;->d:Ljava/util/Map;

    const-string v1, "paymentType"

    invoke-interface {p1, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    sget-object v1, Lnet/gogame/gopay/sdk/support/m;->a:[Ljava/lang/String;

    invoke-static {v0, p1, v1}, Lnet/gogame/gopay/sdk/support/m;->a(Lnet/gogame/gopay/sdk/support/r;Ljava/lang/String;[Ljava/lang/String;)V

    return-void
.end method
