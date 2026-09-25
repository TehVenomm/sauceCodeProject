.class final Lnet/gogame/gopay/sdk/support/o;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/support/q;


# instance fields
.field final synthetic a:I

.field final synthetic b:Lnet/gogame/gopay/sdk/support/r;


# direct methods
.method constructor <init>(ILnet/gogame/gopay/sdk/support/r;)V
    .locals 0

    iput p1, p0, Lnet/gogame/gopay/sdk/support/o;->a:I

    iput-object p2, p0, Lnet/gogame/gopay/sdk/support/o;->b:Lnet/gogame/gopay/sdk/support/r;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final a(Landroid/graphics/Bitmap;)V
    .locals 1

    if-eqz p1, :cond_0

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->k()I

    :cond_0
    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->l()I

    move-result p1

    iget v0, p0, Lnet/gogame/gopay/sdk/support/o;->a:I

    if-lt p1, v0, :cond_1

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->m()Ljava/util/ArrayList;

    move-result-object p1

    invoke-virtual {p1}, Ljava/util/ArrayList;->clear()V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/o;->b:Lnet/gogame/gopay/sdk/support/r;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/o;->b:Lnet/gogame/gopay/sdk/support/r;

    invoke-static {}, Lnet/gogame/gopay/sdk/support/m;->n()I

    invoke-interface {p1}, Lnet/gogame/gopay/sdk/support/r;->a()V

    :cond_1
    return-void
.end method
