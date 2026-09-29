.class final Lnet/gogame/gopay/sdk/support/i;
.super Ljava/lang/Object;


# annotations
.annotation build Landroid/annotation/TargetApi;
    value = 0xe
.end annotation


# direct methods
.method static constructor <clinit>()V
    .locals 2

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xe

    if-lt v0, v1, :cond_0

    return-void

    :cond_0
    new-instance v0, Ljava/lang/RuntimeException;

    const-string v1, "Should not get to IceCreamSandwichPlus class unless sdk is >= 14!"

    invoke-direct {v0, v1}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static a(Landroid/widget/Scroller;)F
    .locals 0

    invoke-virtual {p0}, Landroid/widget/Scroller;->getCurrVelocity()F

    move-result p0

    return p0
.end method
