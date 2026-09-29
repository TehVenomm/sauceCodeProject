.class final Lnet/gogame/gopay/sdk/iab/bt;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/support/q;


# instance fields
.field a:Lnet/gogame/gopay/sdk/iab/bv;

.field final synthetic b:Lnet/gogame/gopay/sdk/iab/bv;

.field final synthetic c:I

.field final synthetic d:Lnet/gogame/gopay/sdk/iab/bs;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/iab/bs;Lnet/gogame/gopay/sdk/iab/bv;I)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bt;->d:Lnet/gogame/gopay/sdk/iab/bs;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/bt;->b:Lnet/gogame/gopay/sdk/iab/bv;

    iput p3, p0, Lnet/gogame/gopay/sdk/iab/bt;->c:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/bt;->b:Lnet/gogame/gopay/sdk/iab/bv;

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/bt;->a:Lnet/gogame/gopay/sdk/iab/bv;

    return-void
.end method


# virtual methods
.method public final a(Landroid/graphics/Bitmap;)V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bt;->a:Lnet/gogame/gopay/sdk/iab/bv;

    iget v0, v0, Lnet/gogame/gopay/sdk/iab/bv;->c:I

    iget v1, p0, Lnet/gogame/gopay/sdk/iab/bt;->c:I

    if-ne v0, v1, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/bt;->d:Lnet/gogame/gopay/sdk/iab/bs;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/bt;->a:Lnet/gogame/gopay/sdk/iab/bv;

    invoke-virtual {v0, v1, p1}, Lnet/gogame/gopay/sdk/iab/bs;->a(Lnet/gogame/gopay/sdk/iab/bv;Landroid/graphics/Bitmap;)V

    :cond_0
    return-void
.end method
