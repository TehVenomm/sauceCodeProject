.class final Lnet/gogame/gopay/sdk/support/f;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/support/c;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/support/c;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/f;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/f;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/support/c;->requestLayout()V

    return-void
.end method
