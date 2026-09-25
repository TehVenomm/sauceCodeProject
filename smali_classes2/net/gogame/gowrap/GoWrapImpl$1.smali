.class Lnet/gogame/gowrap/GoWrapImpl$1;
.super Ljava/lang/Object;
.source "GoWrapImpl.java"

# interfaces
.implements Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/GoWrapImpl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/GoWrapImpl;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/GoWrapImpl;)V
    .locals 0

    .line 70
    iput-object p1, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public didCompleteRewardedAd(Ljava/lang/String;I)V
    .locals 1

    .line 118
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p1, p2}, Lnet/gogame/gowrap/GoWrapImpl;->didCompleteRewardedAd(Ljava/lang/String;I)V

    return-void
.end method

.method public getAppId()Ljava/lang/String;
    .locals 1

    .line 74
    sget-object v0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getCurrentActivity()Landroid/app/Activity;
    .locals 1

    .line 113
    sget-object v0, Lnet/gogame/gowrap/ui/ActivityHelper;->INSTANCE:Lnet/gogame/gowrap/ui/ActivityHelper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/ActivityHelper;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v0

    return-object v0
.end method

.method public getGuid()Ljava/lang/String;
    .locals 1

    .line 79
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-static {v0}, Lnet/gogame/gowrap/GoWrapImpl;->access$000(Lnet/gogame/gowrap/GoWrapImpl;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getUids()Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 84
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 85
    iget-object v1, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-static {v1}, Lnet/gogame/gowrap/GoWrapImpl;->access$100(Lnet/gogame/gowrap/GoWrapImpl;)Ljava/util/List;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :catch_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gowrap/integrations/CanGetUid;

    .line 87
    :try_start_0
    move-object v3, v2

    check-cast v3, Lnet/gogame/gowrap/integrations/IntegrationSupport;

    .line 88
    invoke-interface {v3}, Lnet/gogame/gowrap/integrations/IntegrationSupport;->getId()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v2}, Lnet/gogame/gowrap/integrations/CanGetUid;->getUid()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method public handleCustomUrl(Landroid/net/Uri;)Z
    .locals 1

    .line 139
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->handleCustomUri(Landroid/net/Uri;)Z

    move-result p1

    return p1
.end method

.method public handleCustomUrl(Ljava/lang/String;)Z
    .locals 1

    .line 134
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->handleCustomUri(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public isChatBotEnabled()Z
    .locals 1

    .line 103
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isChatBotEnabled()Z

    move-result v0

    return v0
.end method

.method public isForceEnableChat()Z
    .locals 1

    .line 108
    sget-object v0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->isForceEnableChat()Z

    move-result v0

    return v0
.end method

.method public isVip()Z
    .locals 1

    .line 98
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-static {v0}, Lnet/gogame/gowrap/GoWrapImpl;->access$200(Lnet/gogame/gowrap/GoWrapImpl;)Lnet/gogame/gowrap/VipStatus;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-static {v0}, Lnet/gogame/gowrap/GoWrapImpl;->access$200(Lnet/gogame/gowrap/GoWrapImpl;)Lnet/gogame/gowrap/VipStatus;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/VipStatus;->isVip()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-static {v0}, Lnet/gogame/gowrap/GoWrapImpl;->access$200(Lnet/gogame/gowrap/GoWrapImpl;)Lnet/gogame/gowrap/VipStatus;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/VipStatus;->isSuspended()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public onOffersAvailable()V
    .locals 1

    .line 128
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->onOffersAvailable()V

    .line 129
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-static {v0}, Lnet/gogame/gowrap/GoWrapImpl;->access$300(Lnet/gogame/gowrap/GoWrapImpl;)V

    return-void
.end method

.method public onVipStatusUpdated(Lnet/gogame/gowrap/VipStatus;)V
    .locals 1

    .line 123
    iget-object v0, p0, Lnet/gogame/gowrap/GoWrapImpl$1;->this$0:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->setVipStatus(Lnet/gogame/gowrap/VipStatus;)V

    return-void
.end method
