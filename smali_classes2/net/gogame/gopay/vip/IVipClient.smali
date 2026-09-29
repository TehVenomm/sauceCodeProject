.class public interface abstract Lnet/gogame/gopay/vip/IVipClient;
.super Ljava/lang/Object;
.source "SourceFile"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gopay/vip/IVipClient$Listener;
    }
.end annotation


# virtual methods
.method public abstract addListener(Lnet/gogame/gopay/vip/IVipClient$Listener;)V
.end method

.method public abstract checkVipStatus(Ljava/lang/String;Z)V
.end method

.method public abstract getVipStatus()Lnet/gogame/gopay/vip/VipStatus;
.end method

.method public abstract init(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract removeListener(Lnet/gogame/gopay/vip/IVipClient$Listener;)V
.end method

.method public abstract setExtraData(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract setExtraHeader(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract trackPurchase(Lnet/gogame/gopay/vip/PurchaseEvent;)V
.end method
