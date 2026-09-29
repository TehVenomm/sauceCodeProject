.class Lorg/onepf/openiab/UnityPlugin$5;
.super Ljava/lang/Object;
.source "UnityPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/openiab/UnityPlugin;->consumeProduct(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/openiab/UnityPlugin;

.field final synthetic val$json:Ljava/lang/String;


# direct methods
.method constructor <init>(Lorg/onepf/openiab/UnityPlugin;Ljava/lang/String;)V
    .locals 0

    .line 179
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin$5;->this$0:Lorg/onepf/openiab/UnityPlugin;

    iput-object p2, p0, Lorg/onepf/openiab/UnityPlugin$5;->val$json:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 7

    .line 183
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    iget-object v1, p0, Lorg/onepf/openiab/UnityPlugin$5;->val$json:Ljava/lang/String;

    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v1, "appstoreName"

    .line 184
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "originalJson"

    .line 185
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "packageName"

    .line 186
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v4, "token"

    .line 187
    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    if-eqz v2, :cond_1

    const-string v5, ""

    .line 189
    invoke-virtual {v2, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-nez v5, :cond_1

    const-string v5, "null"

    invoke-virtual {v2, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_0

    goto :goto_0

    :cond_0
    const-string v5, "itemType"

    .line 193
    invoke-virtual {v0, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "signature"

    .line 194
    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 195
    new-instance v6, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    invoke-direct {v6, v5, v2, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 197
    invoke-virtual {v6, v3}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPackageName(Ljava/lang/String;)V

    .line 198
    invoke-virtual {v6, v4}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setToken(Ljava/lang/String;)V

    .line 199
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin$5;->this$0:Lorg/onepf/openiab/UnityPlugin;

    invoke-static {v0}, Lorg/onepf/openiab/UnityPlugin;->access$000(Lorg/onepf/openiab/UnityPlugin;)Lorg/onepf/oms/OpenIabHelper;

    move-result-object v0

    iget-object v1, p0, Lorg/onepf/openiab/UnityPlugin$5;->this$0:Lorg/onepf/openiab/UnityPlugin;

    iget-object v1, v1, Lorg/onepf/openiab/UnityPlugin;->_consumeFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;

    invoke-virtual {v0, v6, v1}, Lorg/onepf/oms/OpenIabHelper;->consumeAsync(Lorg/onepf/oms/appstore/googleUtils/Purchase;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;)V

    goto :goto_1

    :cond_1
    :goto_0
    const-string v0, "OpenIABEventManager"

    const-string v1, "OnConsumePurchaseFailed"

    .line 190
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Original json is invalid: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lorg/onepf/openiab/UnityPlugin$5;->val$json:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v0, v1, v2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 201
    invoke-virtual {v0}, Lorg/json/JSONException;->printStackTrace()V

    const-string v1, "OpenIABEventManager"

    const-string v2, "OnConsumePurchaseFailed"

    .line 202
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Invalid json: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v4, p0, Lorg/onepf/openiab/UnityPlugin$5;->val$json:Ljava/lang/String;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, ". "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :goto_1
    return-void
.end method
