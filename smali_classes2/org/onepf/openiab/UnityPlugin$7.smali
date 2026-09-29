.class Lorg/onepf/openiab/UnityPlugin$7;
.super Ljava/lang/Object;
.source "UnityPlugin.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/openiab/UnityPlugin;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/openiab/UnityPlugin;


# direct methods
.method constructor <init>(Lorg/onepf/openiab/UnityPlugin;)V
    .locals 0

    .line 251
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$7;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 3

    .line 253
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    new-instance v1, Landroid/content/Intent;

    const-string v2, "org.onepf.openiab.ACTION_FINISH"

    invoke-direct {v1, v2}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->sendBroadcast(Landroid/content/Intent;)V

    const-string v0, "OpenIAB-UnityPlugin"

    .line 254
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Purchase finished: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", purchase: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 255
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->isFailure()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string p2, "OpenIAB-UnityPlugin"

    .line 256
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Error purchasing: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-string p2, "OpenIABEventManager"

    const-string v0, "OnPurchaseFailed"

    .line 257
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->getResponse()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, "|"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, v0, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_0
    const-string p1, "OpenIAB-UnityPlugin"

    const-string v0, "Purchase successful."

    .line 260
    invoke-static {p1, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 263
    :try_start_0
    iget-object p1, p0, Lorg/onepf/openiab/UnityPlugin$7;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {p1, p2}, Lorg/onepf/openiab/UnityPlugin;->access$300(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/appstore/googleUtils/Purchase;)Ljava/lang/String;

    move-result-object p1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    const-string p2, "OpenIABEventManager"

    const-string v0, "OnPurchaseSucceeded"

    .line 268
    invoke-static {p2, v0, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :catch_0
    const-string p1, "OpenIABEventManager"

    const-string p2, "OnPurchaseFailed"

    const-string v0, "-1|Couldn\'t serialize the purchase"

    .line 265
    invoke-static {p1, p2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
