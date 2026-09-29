.class public Lnet/gogame/gopay/vip/PurchaseEvent;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Lnet/gogame/gopay/vip/BaseEvent;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;
    }
.end annotation


# static fields
.field public static final EVENT_TYPE:Ljava/lang/String; = "PurchaseEvent"


# instance fields
.field private a:Ljava/lang/String;

.field private b:Ljava/lang/String;

.field private c:Ljava/lang/String;

.field private d:Ljava/lang/String;

.field private e:D

.field private f:J

.field private g:Ljava/lang/String;

.field private h:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

.field private i:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 6
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getCurrencyCode()Ljava/lang/String;
    .locals 1

    .line 70
    iget-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->d:Ljava/lang/String;

    return-object v0
.end method

.method public getGuid()Ljava/lang/String;
    .locals 1

    .line 54
    iget-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->b:Ljava/lang/String;

    return-object v0
.end method

.method public getOrderId()Ljava/lang/String;
    .locals 1

    .line 94
    iget-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->g:Ljava/lang/String;

    return-object v0
.end method

.method public getPrice()D
    .locals 2

    .line 78
    iget-wide v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->e:D

    return-wide v0
.end method

.method public getProductId()Ljava/lang/String;
    .locals 1

    .line 62
    iget-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->c:Ljava/lang/String;

    return-object v0
.end method

.method public getReferenceId()Ljava/lang/String;
    .locals 1

    .line 46
    iget-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->a:Ljava/lang/String;

    return-object v0
.end method

.method public getTimestamp()J
    .locals 2

    .line 86
    iget-wide v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->f:J

    return-wide v0
.end method

.method public getVerificationStatus()Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;
    .locals 1

    .line 102
    iget-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->h:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    return-object v0
.end method

.method public isSandbox()Z
    .locals 1

    .line 110
    iget-boolean v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->i:Z

    return v0
.end method

.method public marshal()Lorg/json/JSONObject;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 119
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    const-string v1, "@eventType"

    const-string v2, "PurchaseEvent"

    .line 120
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "referenceId"

    .line 121
    iget-object v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->a:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "guid"

    .line 122
    iget-object v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->b:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "productId"

    .line 123
    iget-object v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->c:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "currencyCode"

    .line 124
    iget-object v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->d:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "price"

    .line 125
    iget-wide v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->e:D

    invoke-virtual {v0, v1, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;D)Lorg/json/JSONObject;

    const-string v1, "timestamp"

    .line 126
    iget-wide v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->f:J

    invoke-virtual {v0, v1, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;J)Lorg/json/JSONObject;

    const-string v1, "orderId"

    .line 127
    iget-object v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->g:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 128
    iget-object v1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->h:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    if-eqz v1, :cond_0

    const-string v1, "verificationStatus"

    .line 129
    iget-object v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->h:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    invoke-virtual {v2}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->getValue()I

    move-result v2

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    :cond_0
    const-string v1, "sandbox"

    .line 131
    iget-boolean v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->i:Z

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-object v0
.end method

.method public setCurrencyCode(Ljava/lang/String;)V
    .locals 0

    .line 74
    iput-object p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->d:Ljava/lang/String;

    return-void
.end method

.method public setGuid(Ljava/lang/String;)V
    .locals 0

    .line 58
    iput-object p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->b:Ljava/lang/String;

    return-void
.end method

.method public setOrderId(Ljava/lang/String;)V
    .locals 0

    .line 98
    iput-object p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->g:Ljava/lang/String;

    return-void
.end method

.method public setPrice(D)V
    .locals 0

    .line 82
    iput-wide p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->e:D

    return-void
.end method

.method public setProductId(Ljava/lang/String;)V
    .locals 0

    .line 66
    iput-object p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->c:Ljava/lang/String;

    return-void
.end method

.method public setReferenceId(Ljava/lang/String;)V
    .locals 0

    .line 50
    iput-object p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->a:Ljava/lang/String;

    return-void
.end method

.method public setSandbox(Z)V
    .locals 0

    .line 114
    iput-boolean p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->i:Z

    return-void
.end method

.method public setTimestamp(J)V
    .locals 0

    .line 90
    iput-wide p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->f:J

    return-void
.end method

.method public setVerificationStatus(Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;)V
    .locals 0

    .line 106
    iput-object p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->h:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    return-void
.end method

.method public unmarshal(Lorg/json/JSONObject;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "referenceId"

    const/4 v1, 0x0

    .line 137
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->a:Ljava/lang/String;

    const-string v0, "guid"

    .line 138
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->b:Ljava/lang/String;

    const-string v0, "productId"

    .line 139
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->c:Ljava/lang/String;

    const-string v0, "currencyCode"

    .line 140
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->d:Ljava/lang/String;

    const-string v0, "price"

    .line 141
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optDouble(Ljava/lang/String;)D

    move-result-wide v2

    iput-wide v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->e:D

    const-string v0, "timestamp"

    .line 142
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optLong(Ljava/lang/String;)J

    move-result-wide v2

    iput-wide v2, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->f:J

    const-string v0, "orderId"

    .line 143
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->g:Ljava/lang/String;

    const-string v0, "verificationStatus"

    .line 144
    sget-object v1, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->NOT_VERIFIED:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    .line 145
    invoke-virtual {v1}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->getValue()I

    move-result v1

    .line 144
    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v0

    invoke-static {v0}, Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;->fromValue(I)Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->h:Lnet/gogame/gopay/vip/PurchaseEvent$VerificationStatus;

    const-string v0, "sandbox"

    .line 146
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result p1

    iput-boolean p1, p0, Lnet/gogame/gopay/vip/PurchaseEvent;->i:Z

    return-void
.end method
