.class public Lorg/onepf/oms/appstore/googleUtils/Purchase;
.super Ljava/lang/Object;
.source "Purchase.java"

# interfaces
.implements Ljava/lang/Cloneable;


# instance fields
.field appstoreName:Ljava/lang/String;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field mDeveloperPayload:Ljava/lang/String;

.field mItemType:Ljava/lang/String;

.field mOrderId:Ljava/lang/String;

.field mOriginalJson:Ljava/lang/String;

.field mPackageName:Ljava/lang/String;

.field mPurchaseState:I

.field mPurchaseTime:J

.field mSignature:Ljava/lang/String;

.field mSku:Ljava/lang/String;

.field mToken:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 49
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    if-eqz p1, :cond_0

    .line 52
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->appstoreName:Ljava/lang/String;

    return-void

    .line 51
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "appstoreName must be defined"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 91
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 92
    iput-object p4, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->appstoreName:Ljava/lang/String;

    .line 93
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mItemType:Ljava/lang/String;

    .line 94
    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mOriginalJson:Ljava/lang/String;

    .line 95
    new-instance p1, Lorg/json/JSONObject;

    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mOriginalJson:Ljava/lang/String;

    invoke-direct {p1, p2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p2, "orderId"

    .line 96
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mOrderId:Ljava/lang/String;

    const-string p2, "packageName"

    .line 97
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPackageName:Ljava/lang/String;

    const-string p2, "productId"

    .line 98
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mSku:Ljava/lang/String;

    const-string p2, "purchaseTime"

    .line 99
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optLong(Ljava/lang/String;)J

    move-result-wide v0

    iput-wide v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPurchaseTime:J

    const-string p2, "purchaseState"

    .line 100
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;)I

    move-result p2

    iput p2, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPurchaseState:I

    const-string p2, "developerPayload"

    .line 101
    invoke-virtual {p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mDeveloperPayload:Ljava/lang/String;

    const-string p2, "token"

    const-string p4, "purchaseToken"

    .line 102
    invoke-virtual {p1, p4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p4

    invoke-virtual {p1, p2, p4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mToken:Ljava/lang/String;

    .line 103
    iput-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mSignature:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public clone()Ljava/lang/Object;
    .locals 3

    .line 108
    :try_start_0
    invoke-super {p0}, Ljava/lang/Object;->clone()Ljava/lang/Object;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/CloneNotSupportedException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    .line 110
    new-instance v1, Ljava/lang/IllegalStateException;

    const-string v2, "Somebody forgot to add Cloneable to class"

    invoke-direct {v1, v2, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1
.end method

.method public getAppstoreName()Ljava/lang/String;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 156
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->appstoreName:Ljava/lang/String;

    return-object v0
.end method

.method public getDeveloperPayload()Ljava/lang/String;
    .locals 1

    .line 139
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mDeveloperPayload:Ljava/lang/String;

    return-object v0
.end method

.method public getItemType()Ljava/lang/String;
    .locals 1

    .line 115
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mItemType:Ljava/lang/String;

    return-object v0
.end method

.method public getOrderId()Ljava/lang/String;
    .locals 1

    .line 119
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mOrderId:Ljava/lang/String;

    return-object v0
.end method

.method public getOriginalJson()Ljava/lang/String;
    .locals 1

    .line 147
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mOriginalJson:Ljava/lang/String;

    return-object v0
.end method

.method public getPackageName()Ljava/lang/String;
    .locals 1

    .line 123
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPackageName:Ljava/lang/String;

    return-object v0
.end method

.method public getPurchaseState()I
    .locals 1

    .line 135
    iget v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPurchaseState:I

    return v0
.end method

.method public getPurchaseTime()J
    .locals 2

    .line 131
    iget-wide v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPurchaseTime:J

    return-wide v0
.end method

.method public getSignature()Ljava/lang/String;
    .locals 1

    .line 151
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mSignature:Ljava/lang/String;

    return-object v0
.end method

.method public getSku()Ljava/lang/String;
    .locals 1

    .line 127
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mSku:Ljava/lang/String;

    return-object v0
.end method

.method public getToken()Ljava/lang/String;
    .locals 1

    .line 143
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mToken:Ljava/lang/String;

    return-object v0
.end method

.method public setDeveloperPayload(Ljava/lang/String;)V
    .locals 0

    .line 84
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mDeveloperPayload:Ljava/lang/String;

    return-void
.end method

.method public setItemType(Ljava/lang/String;)V
    .locals 0

    .line 60
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mItemType:Ljava/lang/String;

    return-void
.end method

.method public setOrderId(Ljava/lang/String;)V
    .locals 0

    .line 64
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mOrderId:Ljava/lang/String;

    return-void
.end method

.method public setOriginalJson(Ljava/lang/String;)V
    .locals 0

    .line 56
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mOriginalJson:Ljava/lang/String;

    return-void
.end method

.method public setPackageName(Ljava/lang/String;)V
    .locals 0

    .line 68
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPackageName:Ljava/lang/String;

    return-void
.end method

.method public setPurchaseState(I)V
    .locals 0

    .line 80
    iput p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPurchaseState:I

    return-void
.end method

.method public setPurchaseTime(J)V
    .locals 0

    .line 76
    iput-wide p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPurchaseTime:J

    return-void
.end method

.method public setSku(Ljava/lang/String;)V
    .locals 0

    .line 72
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mSku:Ljava/lang/String;

    return-void
.end method

.method public setToken(Ljava/lang/String;)V
    .locals 0

    .line 88
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mToken:Ljava/lang/String;

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 162
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "PurchaseInfo(type:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mItemType:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "): "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "{\"orderId\":"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mOrderId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",\"packageName\":"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPackageName:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",\"productId\":"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mSku:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",\"purchaseTime\":"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-wide v1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPurchaseTime:J

    invoke-virtual {v0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string v1, ",\"purchaseState\":"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mPurchaseState:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ",\"developerPayload\":"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mDeveloperPayload:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ",\"token\":"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mToken:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "}"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
