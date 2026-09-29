.class final Ljp/colopl/drapro/InAppBillingHelper$2;
.super Ljava/lang/Object;
.source "InAppBillingHelper.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/InAppBillingHelper;->getProductDatas(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$skuList:Ljava/util/List;


# direct methods
.method constructor <init>(Ljava/util/List;)V
    .locals 0

    .line 210
    iput-object p1, p0, Ljp/colopl/drapro/InAppBillingHelper$2;->val$skuList:Ljava/util/List;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onQueryInventoryFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V
    .locals 6

    .line 214
    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->isFailure()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string p2, "InAppBillingHelper"

    .line 215
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "getProductDatas fail error: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->getResponse()I

    move-result p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "ShopReceiver"

    const-string p2, "getProductDatas"

    const-string v0, ""

    .line 216
    invoke-static {p1, p2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 221
    :cond_0
    new-instance p1, Lorg/json/JSONArray;

    invoke-direct {p1}, Lorg/json/JSONArray;-><init>()V

    const/4 v0, 0x0

    .line 222
    :goto_0
    iget-object v1, p0, Ljp/colopl/drapro/InAppBillingHelper$2;->val$skuList:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    if-ge v0, v1, :cond_2

    .line 223
    iget-object v1, p0, Ljp/colopl/drapro/InAppBillingHelper$2;->val$skuList:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 225
    invoke-virtual {p2, v1}, Ljp/colopl/iab/Inventory;->getSkuDetails(Ljava/lang/String;)Ljp/colopl/iab/SkuDetails;

    move-result-object v1

    .line 226
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    if-eqz v1, :cond_1

    :try_start_0
    const-string v3, "productId"

    .line 230
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v3, "price"

    .line 231
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getPrice()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v3, "priceMicros"

    .line 232
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getPriceMicros()D

    move-result-wide v4

    invoke-virtual {v2, v3, v4, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;D)Lorg/json/JSONObject;

    const-string v3, "name"

    .line 233
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getTitle()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v3, "desc"

    .line 234
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getDescription()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v2, v3, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v1

    .line 237
    invoke-virtual {v1}, Lorg/json/JSONException;->printStackTrace()V

    .line 239
    :cond_1
    :goto_1
    invoke-virtual {p1, v2}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 242
    :cond_2
    new-instance p2, Lorg/json/JSONObject;

    invoke-direct {p2}, Lorg/json/JSONObject;-><init>()V

    :try_start_1
    const-string v0, "shopList"

    .line 244
    invoke-virtual {p2, v0, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_2

    :catch_1
    move-exception p1

    .line 247
    invoke-virtual {p1}, Lorg/json/JSONException;->printStackTrace()V

    .line 250
    :goto_2
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    const-string p2, "ShopReceiver"

    const-string v0, "getProductDatas"

    .line 252
    invoke-static {p2, v0, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
