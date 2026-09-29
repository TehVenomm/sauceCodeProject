.class public Lcom/amazon/device/iap/internal/util/a;
.super Ljava/lang/Object;
.source "ReceiptHelper.java"


# static fields
.field private static final a:Ljava/lang/String; = "a"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 20
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static a(Lorg/json/JSONObject;)Lcom/amazon/device/iap/model/Receipt;
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "token"

    .line 72
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "sku"

    .line 73
    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "itemType"

    .line 74
    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->toUpperCase()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/amazon/device/iap/model/ProductType;->valueOf(Ljava/lang/String;)Lcom/amazon/device/iap/model/ProductType;

    move-result-object v2

    const-string v3, "startDate"

    .line 77
    invoke-virtual {p0, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 78
    invoke-static {v3}, Lcom/amazon/device/iap/internal/util/a;->a(Ljava/lang/String;)Z

    move-result v4

    const/4 v5, 0x0

    if-eqz v4, :cond_0

    move-object v3, v5

    goto :goto_0

    :cond_0
    invoke-static {v3}, Lcom/amazon/device/iap/internal/util/a;->b(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v3

    :goto_0
    const-string v4, "endDate"

    .line 79
    invoke-virtual {p0, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    .line 80
    invoke-static {p0}, Lcom/amazon/device/iap/internal/util/a;->a(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_1

    goto :goto_1

    :cond_1
    invoke-static {p0}, Lcom/amazon/device/iap/internal/util/a;->b(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v5

    .line 81
    :goto_1
    new-instance p0, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    invoke-direct {p0}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;-><init>()V

    invoke-virtual {p0, v0}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setReceiptId(Ljava/lang/String;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object p0

    invoke-virtual {p0, v1}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setSku(Ljava/lang/String;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object p0

    invoke-virtual {p0, v2}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setProductType(Lcom/amazon/device/iap/model/ProductType;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object p0

    invoke-virtual {p0, v3}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setPurchaseDate(Ljava/util/Date;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object p0

    invoke-virtual {p0, v5}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setCancelDate(Ljava/util/Date;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object p0

    invoke-virtual {p0}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->build()Lcom/amazon/device/iap/model/Receipt;

    move-result-object p0

    return-object p0
.end method

.method public static a(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/amazon/device/iap/internal/b/a;,
            Lcom/amazon/device/iap/internal/b/d;,
            Ljava/lang/IllegalArgumentException;
        }
    .end annotation

    .line 28
    invoke-static {p0}, Lcom/amazon/device/iap/internal/util/a;->b(Lorg/json/JSONObject;)Lcom/amazon/device/iap/internal/util/c;

    move-result-object v0

    .line 30
    sget-object v1, Lcom/amazon/device/iap/internal/util/a$1;->a:[I

    invoke-virtual {v0}, Lcom/amazon/device/iap/internal/util/c;->ordinal()I

    move-result v0

    aget v0, v1, v0

    packed-switch v0, :pswitch_data_0

    .line 39
    invoke-static {p0, p1, p2}, Lcom/amazon/device/iap/internal/util/a;->d(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;

    move-result-object p0

    goto :goto_0

    .line 35
    :pswitch_0
    invoke-static {p0, p1, p2}, Lcom/amazon/device/iap/internal/util/a;->b(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;

    move-result-object p0

    goto :goto_0

    .line 32
    :pswitch_1
    invoke-static {p0, p1, p2}, Lcom/amazon/device/iap/internal/util/a;->c(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;

    move-result-object p0

    :goto_0
    return-object p0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method protected static a(Ljava/lang/String;)Z
    .locals 0

    if-eqz p0, :cond_1

    .line 181
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result p0

    if-nez p0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p0, 0x1

    :goto_1
    return p0
.end method

.method private static b(Lorg/json/JSONObject;)Lcom/amazon/device/iap/internal/util/c;
    .locals 2

    const-string v0, "DeviceId"

    .line 204
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "receiptId"

    .line 205
    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    .line 206
    invoke-static {p0}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/String;)Z

    move-result p0

    if-nez p0, :cond_0

    .line 207
    sget-object p0, Lcom/amazon/device/iap/internal/util/c;->c:Lcom/amazon/device/iap/internal/util/c;

    return-object p0

    .line 208
    :cond_0
    invoke-static {v0}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_1

    .line 209
    sget-object p0, Lcom/amazon/device/iap/internal/util/c;->a:Lcom/amazon/device/iap/internal/util/c;

    return-object p0

    .line 211
    :cond_1
    sget-object p0, Lcom/amazon/device/iap/internal/util/c;->b:Lcom/amazon/device/iap/internal/util/c;

    return-object p0
.end method

.method private static b(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/amazon/device/iap/internal/b/a;,
            Lcom/amazon/device/iap/internal/b/d;
        }
    .end annotation

    const-string v0, "signature"

    .line 49
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 50
    invoke-static {v0}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_1

    .line 57
    :try_start_0
    invoke-static {p0}, Lcom/amazon/device/iap/internal/util/a;->a(Lorg/json/JSONObject;)Lcom/amazon/device/iap/model/Receipt;

    move-result-object v1
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 61
    new-instance p0, Ljava/lang/StringBuilder;

    invoke-direct {p0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "-"

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    .line 62
    invoke-static {p0, v0}, Lcom/amazon/android/Kiwi;->isSignedByKiwi(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    .line 63
    sget-object v2, Lcom/amazon/device/iap/internal/util/a;->a:Ljava/lang/String;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "stringToVerify/legacy:\n"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "\nsignature:\n"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Lcom/amazon/device/iap/internal/util/e;->a(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz p1, :cond_0

    return-object v1

    .line 65
    :cond_0
    invoke-static {p2, p0, v0}, Lcom/amazon/device/iap/internal/util/MetricsHelper;->submitReceiptVerificationFailureMetrics(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 66
    new-instance p1, Lcom/amazon/device/iap/internal/b/d;

    invoke-direct {p1, p2, p0, v0}, Lcom/amazon/device/iap/internal/b/d;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    throw p1

    :catch_0
    move-exception p1

    .line 59
    new-instance v0, Lcom/amazon/device/iap/internal/b/a;

    invoke-virtual {p0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p2, p0, p1}, Lcom/amazon/device/iap/internal/b/a;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v0

    .line 51
    :cond_1
    sget-object p0, Lcom/amazon/device/iap/internal/util/a;->a:Ljava/lang/String;

    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "a signature was not found in the receipt for request ID "

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1}, Lcom/amazon/device/iap/internal/util/e;->b(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "NO Signature found"

    .line 52
    invoke-static {p2, p0, v0}, Lcom/amazon/device/iap/internal/util/MetricsHelper;->submitReceiptVerificationFailureMetrics(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 53
    new-instance p0, Lcom/amazon/device/iap/internal/b/d;

    const/4 p1, 0x0

    invoke-direct {p0, p2, p1, v0}, Lcom/amazon/device/iap/internal/b/d;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    throw p0
.end method

.method protected static b(Ljava/lang/String;)Ljava/util/Date;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 188
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v1, "MM/dd/yyyy HH:mm:ss"

    invoke-direct {v0, v1}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;)V

    .line 190
    :try_start_0
    invoke-virtual {v0, p0}, Ljava/text/DateFormat;->parse(Ljava/lang/String;)Ljava/util/Date;

    move-result-object p0

    const-wide/16 v0, 0x0

    .line 194
    invoke-virtual {p0}, Ljava/util/Date;->getTime()J

    move-result-wide v2
    :try_end_0
    .catch Ljava/text/ParseException; {:try_start_0 .. :try_end_0} :catch_0

    cmp-long v4, v0, v2

    if-nez v4, :cond_0

    const/4 p0, 0x0

    :cond_0
    return-object p0

    :catch_0
    move-exception p0

    .line 199
    new-instance v0, Lorg/json/JSONException;

    invoke-virtual {p0}, Ljava/text/ParseException;->getMessage()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Lorg/json/JSONException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private static c(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/amazon/device/iap/internal/b/a;,
            Lcom/amazon/device/iap/internal/b/d;
        }
    .end annotation

    const-string v0, "DeviceId"

    .line 94
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "signature"

    .line 95
    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 96
    invoke-static {v1}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/String;)Z

    move-result v2

    const/4 v3, 0x0

    if-nez v2, :cond_3

    .line 103
    :try_start_0
    invoke-static {p0}, Lcom/amazon/device/iap/internal/util/a;->a(Lorg/json/JSONObject;)Lcom/amazon/device/iap/model/Receipt;

    move-result-object v2
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    const-string p0, "%s|%s|%s|%s|%s|%s|%s|%tQ|%tQ"

    const/16 v4, 0x9

    .line 107
    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    const-string v6, "2.0.1"

    aput-object v6, v4, v5

    const/4 v5, 0x1

    aput-object p1, v4, v5

    const/4 p1, 0x2

    aput-object v0, v4, p1

    const/4 p1, 0x3

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object v0

    aput-object v0, v4, p1

    const/4 p1, 0x4

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v4, p1

    const/4 p1, 0x5

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v4, p1

    const/4 p1, 0x6

    aput-object p2, v4, p1

    const/4 p1, 0x7

    sget-object v0, Lcom/amazon/device/iap/model/ProductType;->SUBSCRIPTION:Lcom/amazon/device/iap/model/ProductType;

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object v5

    if-ne v0, v5, :cond_0

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getPurchaseDate()Ljava/util/Date;

    move-result-object v0

    goto :goto_0

    :cond_0
    move-object v0, v3

    :goto_0
    aput-object v0, v4, p1

    const/16 p1, 0x8

    sget-object v0, Lcom/amazon/device/iap/model/ProductType;->SUBSCRIPTION:Lcom/amazon/device/iap/model/ProductType;

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object v5

    if-ne v0, v5, :cond_1

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getCancelDate()Ljava/util/Date;

    move-result-object v3

    :cond_1
    aput-object v3, v4, p1

    invoke-static {p0, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    .line 120
    sget-object p1, Lcom/amazon/device/iap/internal/util/a;->a:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "stringToVerify/v1:\n"

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "\nsignature:\n"

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/amazon/device/iap/internal/util/e;->a(Ljava/lang/String;Ljava/lang/String;)V

    .line 122
    invoke-static {p0, v1}, Lcom/amazon/android/Kiwi;->isSignedByKiwi(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_2

    return-object v2

    .line 123
    :cond_2
    invoke-static {p2, p0, v1}, Lcom/amazon/device/iap/internal/util/MetricsHelper;->submitReceiptVerificationFailureMetrics(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 124
    new-instance p1, Lcom/amazon/device/iap/internal/b/d;

    invoke-direct {p1, p2, p0, v1}, Lcom/amazon/device/iap/internal/b/d;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    throw p1

    :catch_0
    move-exception p1

    .line 105
    new-instance v0, Lcom/amazon/device/iap/internal/b/a;

    invoke-virtual {p0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p2, p0, p1}, Lcom/amazon/device/iap/internal/b/a;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v0

    .line 97
    :cond_3
    sget-object p0, Lcom/amazon/device/iap/internal/util/a;->a:Ljava/lang/String;

    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "a signature was not found in the receipt for request ID "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1}, Lcom/amazon/device/iap/internal/util/e;->b(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "NO Signature found"

    .line 98
    invoke-static {p2, p0, v1}, Lcom/amazon/device/iap/internal/util/MetricsHelper;->submitReceiptVerificationFailureMetrics(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 99
    new-instance p0, Lcom/amazon/device/iap/internal/b/d;

    invoke-direct {p0, p2, v3, v1}, Lcom/amazon/device/iap/internal/b/d;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    throw p0
.end method

.method private static d(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/amazon/device/iap/internal/b/a;,
            Lcom/amazon/device/iap/internal/b/d;
        }
    .end annotation

    const-string v0, "DeviceId"

    .line 133
    invoke-virtual {p0, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "signature"

    .line 134
    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 135
    invoke-static {v1}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/String;)Z

    move-result v2

    const/4 v3, 0x0

    if-nez v2, :cond_3

    :try_start_0
    const-string v2, "receiptId"

    .line 142
    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v4, "sku"

    .line 143
    invoke-virtual {p0, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v5, "itemType"

    .line 144
    invoke-virtual {p0, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->toUpperCase()Ljava/lang/String;

    move-result-object v5

    invoke-static {v5}, Lcom/amazon/device/iap/model/ProductType;->valueOf(Ljava/lang/String;)Lcom/amazon/device/iap/model/ProductType;

    move-result-object v5

    const-string v6, "purchaseDate"

    .line 147
    invoke-virtual {p0, v6}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 148
    invoke-static {v6}, Lcom/amazon/device/iap/internal/util/a;->a(Ljava/lang/String;)Z

    move-result v7

    if-eqz v7, :cond_0

    move-object v6, v3

    goto :goto_0

    :cond_0
    invoke-static {v6}, Lcom/amazon/device/iap/internal/util/a;->b(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v6

    :goto_0
    const-string v7, "cancelDate"

    .line 151
    invoke-virtual {p0, v7}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 152
    invoke-static {v7}, Lcom/amazon/device/iap/internal/util/a;->a(Ljava/lang/String;)Z

    move-result v8

    if-eqz v8, :cond_1

    goto :goto_1

    :cond_1
    invoke-static {v7}, Lcom/amazon/device/iap/internal/util/a;->b(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v3

    .line 153
    :goto_1
    new-instance v7, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    invoke-direct {v7}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;-><init>()V

    invoke-virtual {v7, v2}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setReceiptId(Ljava/lang/String;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object v2

    invoke-virtual {v2, v4}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setSku(Ljava/lang/String;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object v2

    invoke-virtual {v2, v5}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setProductType(Lcom/amazon/device/iap/model/ProductType;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object v2

    invoke-virtual {v2, v6}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setPurchaseDate(Ljava/util/Date;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object v2

    invoke-virtual {v2, v3}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->setCancelDate(Ljava/util/Date;)Lcom/amazon/device/iap/internal/model/ReceiptBuilder;

    move-result-object v2

    invoke-virtual {v2}, Lcom/amazon/device/iap/internal/model/ReceiptBuilder;->build()Lcom/amazon/device/iap/model/Receipt;

    move-result-object v2
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    const-string p0, "%s|%s|%s|%s|%s|%tQ|%tQ"

    const/4 v3, 0x7

    .line 164
    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    aput-object p1, v3, v4

    const/4 p1, 0x1

    aput-object v0, v3, p1

    const/4 p1, 0x2

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object v0

    aput-object v0, v3, p1

    const/4 p1, 0x3

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v3, p1

    const/4 p1, 0x4

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v3, p1

    const/4 p1, 0x5

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getPurchaseDate()Ljava/util/Date;

    move-result-object v0

    aput-object v0, v3, p1

    const/4 p1, 0x6

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/Receipt;->getCancelDate()Ljava/util/Date;

    move-result-object v0

    aput-object v0, v3, p1

    invoke-static {p0, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    .line 172
    sget-object p1, Lcom/amazon/device/iap/internal/util/a;->a:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "stringToVerify/v2:\n"

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "\nsignature:\n"

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/amazon/device/iap/internal/util/e;->a(Ljava/lang/String;Ljava/lang/String;)V

    .line 173
    invoke-static {p0, v1}, Lcom/amazon/android/Kiwi;->isSignedByKiwi(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_2

    return-object v2

    .line 174
    :cond_2
    invoke-static {p2, p0, v1}, Lcom/amazon/device/iap/internal/util/MetricsHelper;->submitReceiptVerificationFailureMetrics(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 175
    new-instance p1, Lcom/amazon/device/iap/internal/b/d;

    invoke-direct {p1, p2, p0, v1}, Lcom/amazon/device/iap/internal/b/d;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    throw p1

    :catch_0
    move-exception p1

    .line 161
    new-instance v0, Lcom/amazon/device/iap/internal/b/a;

    invoke-virtual {p0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p2, p0, p1}, Lcom/amazon/device/iap/internal/b/a;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v0

    .line 136
    :cond_3
    sget-object p0, Lcom/amazon/device/iap/internal/util/a;->a:Ljava/lang/String;

    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "a signature was not found in the receipt for request ID "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1}, Lcom/amazon/device/iap/internal/util/e;->b(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, "NO Signature found"

    .line 137
    invoke-static {p2, p0, v1}, Lcom/amazon/device/iap/internal/util/MetricsHelper;->submitReceiptVerificationFailureMetrics(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 138
    new-instance p0, Lcom/amazon/device/iap/internal/b/d;

    invoke-direct {p0, p2, v3, v1}, Lcom/amazon/device/iap/internal/b/d;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    throw p0
.end method
