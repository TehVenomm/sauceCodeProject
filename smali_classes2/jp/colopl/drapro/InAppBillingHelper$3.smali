.class final Ljp/colopl/drapro/InAppBillingHelper$3;
.super Ljava/lang/Object;
.source "InAppBillingHelper.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/InAppBillingHelper;->trackPurtraceData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$productId:Ljava/lang/String;

.field final synthetic val$purchaseData:Ljava/lang/String;

.field final synthetic val$signature:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 265
    iput-object p1, p0, Ljp/colopl/drapro/InAppBillingHelper$3;->val$productId:Ljava/lang/String;

    iput-object p2, p0, Ljp/colopl/drapro/InAppBillingHelper$3;->val$purchaseData:Ljava/lang/String;

    iput-object p3, p0, Ljp/colopl/drapro/InAppBillingHelper$3;->val$signature:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onQueryInventoryFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V
    .locals 4

    .line 269
    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->isFailure()Z

    move-result p1

    if-eqz p1, :cond_0

    return-void

    .line 273
    :cond_0
    iget-object p1, p0, Ljp/colopl/drapro/InAppBillingHelper$3;->val$productId:Ljava/lang/String;

    .line 274
    invoke-virtual {p2, p1}, Ljp/colopl/iab/Inventory;->getSkuDetails(Ljava/lang/String;)Ljp/colopl/iab/SkuDetails;

    move-result-object p1

    .line 278
    :try_start_0
    new-instance p2, Lorg/json/JSONObject;

    invoke-direct {p2}, Lorg/json/JSONObject;-><init>()V

    const-string v0, "productId"

    .line 280
    iget-object v1, p0, Ljp/colopl/drapro/InAppBillingHelper$3;->val$productId:Ljava/lang/String;

    invoke-virtual {p2, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "purchaseData"

    .line 281
    iget-object v1, p0, Ljp/colopl/drapro/InAppBillingHelper$3;->val$purchaseData:Ljava/lang/String;

    invoke-virtual {p2, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "signature"

    .line 282
    iget-object v1, p0, Ljp/colopl/drapro/InAppBillingHelper$3;->val$signature:Ljava/lang/String;

    invoke-virtual {p2, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "currency"

    .line 283
    invoke-virtual {p1}, Ljp/colopl/iab/SkuDetails;->getCurrency()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p2, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 285
    invoke-virtual {p1}, Ljp/colopl/iab/SkuDetails;->getPriceMicros()D

    move-result-wide v0

    const-wide v2, 0x412e848000000000L    # 1000000.0

    div-double/2addr v0, v2

    const-string p1, "price"

    .line 286
    invoke-virtual {p2, p1, v0, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;D)Lorg/json/JSONObject;

    const-string p1, "ShopReceiver"

    const-string v0, "TrackPurchase"

    .line 289
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, v0, p2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "InAppBillingHelper"

    .line 293
    invoke-virtual {p1}, Ljava/lang/Exception;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method
