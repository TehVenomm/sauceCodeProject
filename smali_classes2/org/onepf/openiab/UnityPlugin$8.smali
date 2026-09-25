.class Lorg/onepf/openiab/UnityPlugin$8;
.super Ljava/lang/Object;
.source "UnityPlugin.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;


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

    .line 275
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$8;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onConsumeFinished(Lorg/onepf/oms/appstore/googleUtils/Purchase;Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    .locals 3

    const-string v0, "OpenIAB-UnityPlugin"

    .line 277
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Consumption finished. Purchase: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", result: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 279
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getAppstoreName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 281
    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->isFailure()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string p1, "OpenIAB-UnityPlugin"

    .line 282
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Error while consuming: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-string p1, "OpenIABEventManager"

    const-string v0, "OnConsumePurchaseFailed"

    .line 283
    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, v0, p2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_0
    const-string p2, "OpenIAB-UnityPlugin"

    const-string v0, "Consumption successful. Provisioning."

    .line 286
    invoke-static {p2, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 289
    :try_start_0
    iget-object p2, p0, Lorg/onepf/openiab/UnityPlugin$8;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {p2, p1}, Lorg/onepf/openiab/UnityPlugin;->access$300(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/appstore/googleUtils/Purchase;)Ljava/lang/String;

    move-result-object p1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    const-string p2, "OpenIABEventManager"

    const-string v0, "OnConsumePurchaseSucceeded"

    .line 294
    invoke-static {p2, v0, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :catch_0
    const-string p1, "OpenIABEventManager"

    const-string p2, "OnConsumePurchaseFailed"

    const-string v0, "Couldn\'t serialize the purchase"

    .line 291
    invoke-static {p1, p2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
