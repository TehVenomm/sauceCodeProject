.class Lcom/amazon/device/iap/internal/c/d;
.super Ljava/lang/Object;
.source "PendingReceipt.java"


# instance fields
.field private final a:Ljava/lang/String;

.field private final b:Ljava/lang/String;

.field private final c:J

.field private final d:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;J)V
    .locals 0

    .line 36
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 37
    iput-object p1, p0, Lcom/amazon/device/iap/internal/c/d;->a:Ljava/lang/String;

    .line 38
    iput-object p2, p0, Lcom/amazon/device/iap/internal/c/d;->b:Ljava/lang/String;

    .line 39
    iput-object p3, p0, Lcom/amazon/device/iap/internal/c/d;->d:Ljava/lang/String;

    .line 40
    iput-wide p4, p0, Lcom/amazon/device/iap/internal/c/d;->c:J

    return-void
.end method

.method public static a(Ljava/lang/String;)Lcom/amazon/device/iap/internal/c/d;
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/amazon/device/iap/internal/c/e;
        }
    .end annotation

    .line 45
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 47
    new-instance v7, Lcom/amazon/device/iap/internal/c/d;

    const-string v1, "KEY_USER_ID"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v1, "KEY_RECEIPT_STRING"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v1, "KEY_REQUEST_ID"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v1, "KEY_TIMESTAMP"

    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getLong(Ljava/lang/String;)J

    move-result-wide v5

    move-object v1, v7

    invoke-direct/range {v1 .. v6}, Lcom/amazon/device/iap/internal/c/d;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;J)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-object v7

    :catch_0
    move-exception v0

    .line 52
    new-instance v1, Lcom/amazon/device/iap/internal/c/e;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Input invalid for PendingReceipt Object:"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v1, p0, v0}, Lcom/amazon/device/iap/internal/c/e;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1
.end method


# virtual methods
.method public a()Ljava/lang/String;
    .locals 1

    .line 25
    iget-object v0, p0, Lcom/amazon/device/iap/internal/c/d;->d:Ljava/lang/String;

    return-object v0
.end method

.method public b()Ljava/lang/String;
    .locals 1

    .line 29
    iget-object v0, p0, Lcom/amazon/device/iap/internal/c/d;->b:Ljava/lang/String;

    return-object v0
.end method

.method public c()J
    .locals 2

    .line 33
    iget-wide v0, p0, Lcom/amazon/device/iap/internal/c/d;->c:J

    return-wide v0
.end method

.method public d()Ljava/lang/String;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 57
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    const-string v1, "KEY_USER_ID"

    .line 58
    iget-object v2, p0, Lcom/amazon/device/iap/internal/c/d;->a:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "KEY_RECEIPT_STRING"

    .line 59
    iget-object v2, p0, Lcom/amazon/device/iap/internal/c/d;->b:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "KEY_REQUEST_ID"

    .line 60
    iget-object v2, p0, Lcom/amazon/device/iap/internal/c/d;->d:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "KEY_TIMESTAMP"

    .line 61
    iget-wide v2, p0, Lcom/amazon/device/iap/internal/c/d;->c:J

    invoke-virtual {v0, v1, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;J)Lorg/json/JSONObject;

    .line 63
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
