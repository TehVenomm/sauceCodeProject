.class Lorg/onepf/openiab/UnityPlugin$6;
.super Ljava/lang/Object;
.source "UnityPlugin.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;


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

    .line 228
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$6;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onQueryInventoryFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V
    .locals 2

    const-string v0, "OpenIAB-UnityPlugin"

    const-string v1, "Query inventory finished."

    .line 230
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 231
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->isFailure()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string p2, "OpenIABEventManager"

    const-string v0, "OnQueryInventoryFailed"

    .line 232
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, v0, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_0
    const-string p1, "OpenIAB-UnityPlugin"

    const-string v0, "Query inventory was successful."

    .line 236
    invoke-static {p1, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 239
    :try_start_0
    iget-object p1, p0, Lorg/onepf/openiab/UnityPlugin$6;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {p1, p2}, Lorg/onepf/openiab/UnityPlugin;->access$200(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/appstore/googleUtils/Inventory;)Ljava/lang/String;

    move-result-object p1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    const-string p2, "OpenIABEventManager"

    const-string v0, "OnQueryInventorySucceeded"

    .line 244
    invoke-static {p2, v0, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :catch_0
    const-string p1, "OpenIABEventManager"

    const-string p2, "OnQueryInventoryFailed"

    const-string v0, "Couldn\'t serialize the inventory"

    .line 241
    invoke-static {p1, p2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
