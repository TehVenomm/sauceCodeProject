.class public Lnet/gogame/gowrap/support/InAppPurchaseData;
.super Ljava/lang/Object;
.source "InAppPurchaseData.java"


# static fields
.field private static final AUTO_RENEWING:Ljava/lang/String; = "autoRenewing"

.field private static final DEVELOPER_PAYLOAD:Ljava/lang/String; = "developerPayload"

.field private static final ORDER_ID:Ljava/lang/String; = "orderId"

.field private static final PACKAGE_NAME:Ljava/lang/String; = "packageName"

.field private static final PRODUCT_ID:Ljava/lang/String; = "productId"

.field private static final PURCHASE_STATUS:Ljava/lang/String; = "purchaseStatus"

.field private static final PURCHASE_TIME:Ljava/lang/String; = "purchaseTime"

.field private static final PURCHASE_TOKEN:Ljava/lang/String; = "purchaseToken"


# instance fields
.field private final autoRenewing:Ljava/lang/Boolean;

.field private final developerPayload:Ljava/lang/String;

.field private final orderId:Ljava/lang/String;

.field private final packageName:Ljava/lang/String;

.field private final productId:Ljava/lang/String;

.field private final purchaseStatus:Ljava/lang/Integer;

.field private final purchaseTime:Ljava/lang/Long;

.field private final purchaseToken:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 27
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/support/InAppPurchaseData;-><init>(Lorg/json/JSONObject;)V

    return-void
.end method

.method public constructor <init>(Lorg/json/JSONObject;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 31
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "autoRenewing"

    .line 33
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    const-string v0, "autoRenewing"

    const/4 v2, 0x0

    .line 34
    invoke-virtual {p1, v0, v2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v0

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->autoRenewing:Ljava/lang/Boolean;

    goto :goto_0

    .line 36
    :cond_0
    iput-object v1, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->autoRenewing:Ljava/lang/Boolean;

    :goto_0
    const-string v0, "orderId"

    .line 38
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->orderId:Ljava/lang/String;

    const-string v0, "packageName"

    .line 39
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->packageName:Ljava/lang/String;

    const-string v0, "productId"

    .line 40
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->productId:Ljava/lang/String;

    const-string v0, "purchaseTime"

    .line 41
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    const-string v0, "purchaseTime"

    .line 42
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optLong(Ljava/lang/String;)J

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->purchaseTime:Ljava/lang/Long;

    goto :goto_1

    .line 44
    :cond_1
    iput-object v1, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->purchaseTime:Ljava/lang/Long;

    :goto_1
    const-string v0, "purchaseStatus"

    .line 46
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    const-string v0, "purchaseStatus"

    .line 47
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;)I

    move-result v0

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->purchaseStatus:Ljava/lang/Integer;

    goto :goto_2

    .line 49
    :cond_2
    iput-object v1, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->purchaseStatus:Ljava/lang/Integer;

    :goto_2
    const-string v0, "developerPayload"

    .line 51
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->developerPayload:Ljava/lang/String;

    const-string v0, "purchaseToken"

    .line 52
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->purchaseToken:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getAutoRenewing()Ljava/lang/Boolean;
    .locals 1

    .line 56
    iget-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->autoRenewing:Ljava/lang/Boolean;

    return-object v0
.end method

.method public getDeveloperPayload()Ljava/lang/String;
    .locals 1

    .line 80
    iget-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->developerPayload:Ljava/lang/String;

    return-object v0
.end method

.method public getOrderId()Ljava/lang/String;
    .locals 1

    .line 60
    iget-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->orderId:Ljava/lang/String;

    return-object v0
.end method

.method public getPackageName()Ljava/lang/String;
    .locals 1

    .line 64
    iget-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->packageName:Ljava/lang/String;

    return-object v0
.end method

.method public getProductId()Ljava/lang/String;
    .locals 1

    .line 68
    iget-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->productId:Ljava/lang/String;

    return-object v0
.end method

.method public getPurchaseStatus()Ljava/lang/Integer;
    .locals 1

    .line 76
    iget-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->purchaseStatus:Ljava/lang/Integer;

    return-object v0
.end method

.method public getPurchaseTime()Ljava/lang/Long;
    .locals 1

    .line 72
    iget-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->purchaseTime:Ljava/lang/Long;

    return-object v0
.end method

.method public getPurchaseToken()Ljava/lang/String;
    .locals 1

    .line 84
    iget-object v0, p0, Lnet/gogame/gowrap/support/InAppPurchaseData;->purchaseToken:Ljava/lang/String;

    return-object v0
.end method
