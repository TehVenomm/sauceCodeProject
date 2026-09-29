.class final Lnet/gogame/gopay/sdk/support/e;
.super Landroid/database/DataSetObserver;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/support/c;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/support/c;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-direct {p0}, Landroid/database/DataSetObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public final onChanged()V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->b(Lnet/gogame/gopay/sdk/support/c;)Z

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->c(Lnet/gogame/gopay/sdk/support/c;)Z

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->d(Lnet/gogame/gopay/sdk/support/c;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/support/c;->invalidate()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/support/c;->requestLayout()V

    return-void
.end method

.method public final onInvalidated()V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->c(Lnet/gogame/gopay/sdk/support/c;)Z

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->d(Lnet/gogame/gopay/sdk/support/c;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->e(Lnet/gogame/gopay/sdk/support/c;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/support/c;->invalidate()V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/e;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/support/c;->requestLayout()V

    return-void
.end method
