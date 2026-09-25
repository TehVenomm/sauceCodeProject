.class Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$1;
.super Ljava/lang/Object;
.source "GoPaySupport.java"

# interfaces
.implements Lnet/gogame/gopay/vip/IVipClient$Listener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;)V
    .locals 0

    .line 36
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$1;->this$0:Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onVipStatus(Lnet/gogame/gopay/vip/VipStatus;)V
    .locals 2

    .line 40
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$1;->this$0:Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;

    invoke-static {v0}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->access$000(Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 43
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$1;->this$0:Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;

    invoke-static {v0}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->access$100(Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;)Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    move-result-object v0

    if-eqz v0, :cond_2

    const/4 v0, 0x0

    if-eqz p1, :cond_1

    .line 46
    new-instance v0, Lnet/gogame/gowrap/VipStatus;

    invoke-direct {v0}, Lnet/gogame/gowrap/VipStatus;-><init>()V

    .line 47
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/VipStatus;->isVip()Z

    move-result v1

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/VipStatus;->setVip(Z)V

    .line 48
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/VipStatus;->isSuspended()Z

    move-result v1

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/VipStatus;->setSuspended(Z)V

    .line 49
    invoke-virtual {p1}, Lnet/gogame/gopay/vip/VipStatus;->getSuspensionMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/VipStatus;->setSuspensionMessage(Ljava/lang/String;)V

    .line 51
    :cond_1
    iget-object p1, p0, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport$1;->this$0:Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;

    invoke-static {p1}, Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;->access$100(Lnet/gogame/gowrap/integrations/gopay/GoPaySupport;)Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;

    move-result-object p1

    invoke-interface {p1, v0}, Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;->onVipStatusUpdated(Lnet/gogame/gowrap/VipStatus;)V

    :cond_2
    return-void
.end method
