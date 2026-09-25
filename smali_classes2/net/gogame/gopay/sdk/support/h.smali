.class final Lnet/gogame/gopay/sdk/support/h;
.super Ljava/lang/Object;


# annotations
.annotation build Landroid/annotation/TargetApi;
    value = 0xb
.end annotation


# direct methods
.method static constructor <clinit>()V
    .locals 2

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xb

    if-lt v0, v1, :cond_0

    return-void

    :cond_0
    new-instance v0, Ljava/lang/RuntimeException;

    const-string v1, "Should not get to HoneycombPlus class unless sdk is >= 11!"

    invoke-direct {v0, v1}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static a(Landroid/widget/Scroller;)V
    .locals 1

    if-eqz p0, :cond_0

    const v0, 0x3c1374bc    # 0.009f

    invoke-virtual {p0, v0}, Landroid/widget/Scroller;->setFriction(F)V

    :cond_0
    return-void
.end method
