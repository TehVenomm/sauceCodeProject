.class final Ljp/colopl/drapro/InAppBillingHelper$1;
.super Ljava/lang/Object;
.source "InAppBillingHelper.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/InAppBillingHelper;->getSkuDetails()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 153
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onQueryInventoryFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V
    .locals 5

    .line 157
    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->isFailure()Z

    move-result p1

    if-eqz p1, :cond_0

    return-void

    .line 164
    :cond_0
    new-instance p1, Lorg/json/JSONArray;

    invoke-direct {p1}, Lorg/json/JSONArray;-><init>()V

    const/4 v0, 0x0

    .line 165
    :goto_0
    sget-object v1, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    array-length v1, v1

    if-ge v0, v1, :cond_2

    .line 166
    sget-object v1, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    aget-object v1, v1, v0

    .line 168
    invoke-virtual {p2, v1}, Ljp/colopl/iab/Inventory;->getSkuDetails(Ljava/lang/String;)Ljp/colopl/iab/SkuDetails;

    move-result-object v1

    .line 169
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    if-eqz v1, :cond_1

    :try_start_0
    const-string v3, "sku"

    .line 173
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v3, "price"

    .line 174
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getPrice()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v3, "title"

    .line 175
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getTitle()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v3, "description"

    .line 177
    invoke-virtual {v1}, Ljp/colopl/iab/SkuDetails;->getDescription()Ljava/lang/String;

    move-result-object v1

    .line 176
    invoke-virtual {v2, v3, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v1

    .line 180
    invoke-virtual {v1}, Lorg/json/JSONException;->printStackTrace()V

    .line 182
    :cond_1
    :goto_1
    invoke-virtual {p1, v2}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 185
    :cond_2
    new-instance p2, Lorg/json/JSONObject;

    invoke-direct {p2}, Lorg/json/JSONObject;-><init>()V

    :try_start_1
    const-string v0, "ItemList"

    .line 187
    invoke-virtual {p2, v0, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_2

    :catch_1
    move-exception p1

    .line 190
    invoke-virtual {p1}, Lorg/json/JSONException;->printStackTrace()V

    .line 193
    :goto_2
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    const-string p2, "ShopReceiver"

    const-string v0, "ReceiveItemPriceData"

    .line 195
    invoke-static {p2, v0, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
