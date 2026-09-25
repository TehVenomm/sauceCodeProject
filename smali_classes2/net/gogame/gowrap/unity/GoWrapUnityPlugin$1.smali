.class Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;
.super Ljava/lang/Object;
.source "GoWrapUnityPlugin.java"

# interfaces
.implements Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)V
    .locals 0

    .line 17
    iput-object p1, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public didCompleteRewardedAd(Ljava/lang/String;I)V
    .locals 2

    .line 75
    iget-object v0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v0}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 79
    :cond_0
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    const-string v1, "rewardId"

    .line 80
    invoke-virtual {v0, v1, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string p1, "rewardQuantity"

    .line 81
    invoke-virtual {v0, p1, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    .line 82
    iget-object p1, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {p1}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object p1

    const-string p2, "handleAdsCompletedWithReward"

    .line 83
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    .line 82
    invoke-static {p1, p2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "goWrap-Unity"

    const-string v0, "Exception"

    .line 85
    invoke-static {p2, v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public onCustomUrl(Ljava/lang/String;)V
    .locals 2

    .line 49
    iget-object v0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v0}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 53
    :cond_0
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v0}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "handleOnCustomUrl"

    invoke-static {v0, v1, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "goWrap-Unity"

    const-string v1, "Exception"

    .line 55
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public onMenuClosed()V
    .locals 3

    .line 35
    iget-object v0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v0}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 39
    :cond_0
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 40
    iget-object v1, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v1}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "handleMenuClosed"

    .line 41
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    .line 40
    invoke-static {v1, v2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap-Unity"

    const-string v2, "Exception"

    .line 43
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public onMenuOpened()V
    .locals 3

    .line 21
    iget-object v0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v0}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 25
    :cond_0
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 26
    iget-object v1, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v1}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "handleMenuOpened"

    .line 27
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    .line 26
    invoke-static {v1, v2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap-Unity"

    const-string v2, "Exception"

    .line 29
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public onOffersAvailable()V
    .locals 3

    .line 61
    iget-object v0, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v0}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 65
    :cond_0
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 66
    iget-object v1, p0, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin$1;->this$0:Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;

    invoke-static {v1}, Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;->access$000(Lnet/gogame/gowrap/unity/GoWrapUnityPlugin;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "handleOnOffersAvailable"

    .line 67
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    .line 66
    invoke-static {v1, v2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap-Unity"

    const-string v2, "Exception"

    .line 69
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method
