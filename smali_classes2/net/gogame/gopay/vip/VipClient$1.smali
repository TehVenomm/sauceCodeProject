.class Lnet/gogame/gopay/vip/VipClient$1;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Lnet/gogame/gopay/vip/TaskQueue$Listener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gopay/vip/VipClient;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lnet/gogame/gopay/vip/TaskQueue$Listener<",
        "Lnet/gogame/gopay/vip/BaseEvent;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic a:Lnet/gogame/gopay/vip/VipClient;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/vip/VipClient;)V
    .locals 0

    .line 41
    iput-object p1, p0, Lnet/gogame/gopay/vip/VipClient$1;->a:Lnet/gogame/gopay/vip/VipClient;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public a(Lnet/gogame/gopay/vip/BaseEvent;)Z
    .locals 3

    .line 45
    instance-of v0, p1, Lnet/gogame/gopay/vip/PurchaseEvent;

    const/4 v1, 0x1

    if-eqz v0, :cond_1

    .line 46
    check-cast p1, Lnet/gogame/gopay/vip/PurchaseEvent;

    :try_start_0
    const-string v0, "goPay"

    const-string v2, "Tracking purchase..."

    .line 48
    invoke-static {v0, v2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 49
    iget-object v0, p0, Lnet/gogame/gopay/vip/VipClient$1;->a:Lnet/gogame/gopay/vip/VipClient;

    invoke-static {v0, p1}, Lnet/gogame/gopay/vip/VipClient;->a(Lnet/gogame/gopay/vip/VipClient;Lnet/gogame/gopay/vip/PurchaseEvent;)Lnet/gogame/gopay/vip/BaseBillingResponse;

    move-result-object p1
    :try_end_0
    .catch Lnet/gogame/gopay/vip/HttpException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    if-nez p1, :cond_0

    return v1

    :cond_0
    return v1

    :catch_0
    return v1

    :catch_1
    const/4 p1, 0x0

    return p1

    :cond_1
    return v1
.end method

.method public synthetic onTask(Ljava/lang/Object;)Z
    .locals 0

    .line 41
    check-cast p1, Lnet/gogame/gopay/vip/BaseEvent;

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/vip/VipClient$1;->a(Lnet/gogame/gopay/vip/BaseEvent;)Z

    move-result p1

    return p1
.end method
