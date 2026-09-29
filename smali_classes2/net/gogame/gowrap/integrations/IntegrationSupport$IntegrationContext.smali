.class public interface abstract Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;
.super Ljava/lang/Object;
.source "IntegrationSupport.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/integrations/IntegrationSupport;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "IntegrationContext"
.end annotation


# virtual methods
.method public abstract didCompleteRewardedAd(Ljava/lang/String;I)V
.end method

.method public abstract getAppId()Ljava/lang/String;
.end method

.method public abstract getCurrentActivity()Landroid/app/Activity;
.end method

.method public abstract getGuid()Ljava/lang/String;
.end method

.method public abstract getUids()Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end method

.method public abstract handleCustomUrl(Landroid/net/Uri;)Z
.end method

.method public abstract handleCustomUrl(Ljava/lang/String;)Z
.end method

.method public abstract isChatBotEnabled()Z
.end method

.method public abstract isForceEnableChat()Z
.end method

.method public abstract isVip()Z
.end method

.method public abstract onOffersAvailable()V
.end method

.method public abstract onVipStatusUpdated(Lnet/gogame/gowrap/VipStatus;)V
.end method
