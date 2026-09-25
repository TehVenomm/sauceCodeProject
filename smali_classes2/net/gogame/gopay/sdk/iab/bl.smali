.class final Lnet/gogame/gopay/sdk/iab/bl;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/g;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/bk;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/bk;Lnet/gogame/gopay/sdk/g;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bl;->b:Lnet/gogame/gopay/sdk/iab/bk;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/bl;->a:Lnet/gogame/gopay/sdk/g;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bl;->b:Lnet/gogame/gopay/sdk/iab/bk;

    iget-object v0, v0, Lnet/gogame/gopay/sdk/iab/bk;->c:Lnet/gogame/gopay/sdk/iab/bq;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bl;->a:Lnet/gogame/gopay/sdk/g;

    invoke-interface {v0, v1}, Lnet/gogame/gopay/sdk/iab/bq;->a(Lnet/gogame/gopay/sdk/g;)V

    return-void
.end method
