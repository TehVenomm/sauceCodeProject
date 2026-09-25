.class public final Lcom/amazon/device/iap/internal/b/a/b;
.super Lcom/amazon/device/iap/internal/b/a/c;
.source "PurchaseResponseCommandV1.java"


# static fields
.field private static final a:Ljava/lang/String; = "b"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Lcom/amazon/device/iap/internal/b/e;)V
    .locals 1

    const-string v0, "1.0"

    .line 34
    invoke-direct {p0, p1, v0}, Lcom/amazon/device/iap/internal/b/a/c;-><init>(Lcom/amazon/device/iap/internal/b/e;Ljava/lang/String;)V

    return-void
.end method

.method private a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    if-eqz p1, :cond_2

    if-eqz p2, :cond_2

    if-nez p3, :cond_0

    goto :goto_1

    .line 43
    :cond_0
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p3}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v1, "orderStatus"

    .line 44
    invoke-virtual {v0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 45
    invoke-static {v1}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->safeValueOf(Ljava/lang/String;)Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    move-result-object v1

    .line 46
    sget-object v2, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->SUCCESSFUL:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    if-ne v1, v2, :cond_1

    .line 47
    invoke-static {v0, p2, p1}, Lcom/amazon/device/iap/internal/util/a;->a(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;

    move-result-object v0

    .line 48
    invoke-static {}, Lcom/amazon/device/iap/internal/c/a;->a()Lcom/amazon/device/iap/internal/c/a;

    move-result-object v1

    invoke-virtual {v0}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, p1, p2, v0, p3}, Lcom/amazon/device/iap/internal/c/a;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 54
    sget-object p2, Lcom/amazon/device/iap/internal/b/a/b;->a:Ljava/lang/String;

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Error in savePendingReceipt: "

    invoke-virtual {p3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Lcom/amazon/device/iap/internal/util/e;->b(Ljava/lang/String;Ljava/lang/String;)V

    :cond_1
    :goto_0
    return-void

    :cond_2
    :goto_1
    return-void
.end method


# virtual methods
.method protected a(Lcom/amazon/venezia/command/SuccessResult;)Z
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 62
    invoke-interface {p1}, Lcom/amazon/venezia/command/SuccessResult;->getData()Ljava/util/Map;

    move-result-object p1

    .line 63
    sget-object v0, Lcom/amazon/device/iap/internal/b/a/b;->a:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "data: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/amazon/device/iap/internal/util/e;->a(Ljava/lang/String;Ljava/lang/String;)V

    .line 65
    invoke-virtual {p0}, Lcom/amazon/device/iap/internal/b/a/b;->getCommandData()Ljava/util/Map;

    move-result-object v0

    const-string v1, "requestId"

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    const-string v1, "userId"

    .line 66
    invoke-interface {p1, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    const-string v2, "marketplace"

    .line 67
    invoke-interface {p1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    const-string v3, "receipt"

    .line 68
    invoke-interface {p1, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    const/4 v3, 0x1

    if-eqz v0, :cond_3

    .line 70
    invoke-static {}, Lcom/amazon/device/iap/internal/c/b;->a()Lcom/amazon/device/iap/internal/c/b;

    move-result-object v4

    invoke-virtual {v4, v0}, Lcom/amazon/device/iap/internal/c/b;->a(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_0

    goto :goto_1

    .line 77
    :cond_0
    invoke-static {p1}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/String;)Z

    move-result v4

    const/4 v5, 0x0

    if-eqz v4, :cond_1

    .line 78
    sget-object p1, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->FAILED:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    invoke-virtual {p0, v1, v2, v0, p1}, Lcom/amazon/device/iap/internal/b/a/b;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;)V

    return v5

    :cond_1
    const/4 v4, 0x0

    .line 83
    new-instance v6, Lorg/json/JSONObject;

    invoke-direct {v6, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v7, "orderStatus"

    .line 84
    invoke-virtual {v6, v7}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 85
    invoke-static {v7}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->safeValueOf(Ljava/lang/String;)Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    move-result-object v7

    .line 86
    sget-object v8, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->SUCCESSFUL:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    if-ne v7, v8, :cond_2

    .line 88
    :try_start_0
    invoke-static {v6, v1, v0}, Lcom/amazon/device/iap/internal/util/a;->a(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Lcom/amazon/device/iap/model/Receipt;

    move-result-object v4
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    .line 95
    sget-object v5, Lcom/amazon/device/iap/model/ProductType;->CONSUMABLE:Lcom/amazon/device/iap/model/ProductType;

    invoke-virtual {v4}, Lcom/amazon/device/iap/model/Receipt;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object v6

    if-ne v5, v6, :cond_2

    .line 96
    invoke-direct {p0, v0, v1, p1}, Lcom/amazon/device/iap/internal/b/a/b;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 90
    :catch_0
    sget-object p1, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->FAILED:Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    invoke-virtual {p0, v1, v2, v0, p1}, Lcom/amazon/device/iap/internal/b/a/b;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;)V

    return v5

    .line 100
    :cond_2
    :goto_0
    invoke-virtual {p0}, Lcom/amazon/device/iap/internal/b/a/b;->b()Lcom/amazon/device/iap/internal/b/e;

    move-result-object p1

    .line 101
    new-instance v0, Lcom/amazon/device/iap/internal/model/UserDataBuilder;

    invoke-direct {v0}, Lcom/amazon/device/iap/internal/model/UserDataBuilder;-><init>()V

    invoke-virtual {v0, v1}, Lcom/amazon/device/iap/internal/model/UserDataBuilder;->setUserId(Ljava/lang/String;)Lcom/amazon/device/iap/internal/model/UserDataBuilder;

    move-result-object v0

    invoke-virtual {v0, v2}, Lcom/amazon/device/iap/internal/model/UserDataBuilder;->setMarketplace(Ljava/lang/String;)Lcom/amazon/device/iap/internal/model/UserDataBuilder;

    move-result-object v0

    invoke-virtual {v0}, Lcom/amazon/device/iap/internal/model/UserDataBuilder;->build()Lcom/amazon/device/iap/model/UserData;

    move-result-object v0

    .line 102
    new-instance v1, Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;

    invoke-direct {v1}, Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;-><init>()V

    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/b/e;->c()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;->setRequestId(Lcom/amazon/device/iap/model/RequestId;)Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;

    move-result-object v1

    invoke-virtual {v1, v7}, Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;->setRequestStatus(Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;)Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;->setUserData(Lcom/amazon/device/iap/model/UserData;)Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;

    move-result-object v0

    invoke-virtual {v0, v4}, Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;->setReceipt(Lcom/amazon/device/iap/model/Receipt;)Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;

    move-result-object v0

    invoke-virtual {v0}, Lcom/amazon/device/iap/internal/model/PurchaseResponseBuilder;->build()Lcom/amazon/device/iap/model/PurchaseResponse;

    move-result-object v0

    .line 108
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/b/e;->d()Lcom/amazon/device/iap/internal/b/h;

    move-result-object p1

    invoke-virtual {p1, v0}, Lcom/amazon/device/iap/internal/b/h;->a(Ljava/lang/Object;)V

    return v3

    .line 73
    :cond_3
    :goto_1
    invoke-virtual {p0}, Lcom/amazon/device/iap/internal/b/a/b;->b()Lcom/amazon/device/iap/internal/b/e;

    move-result-object p1

    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/b/e;->d()Lcom/amazon/device/iap/internal/b/h;

    move-result-object p1

    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/b/h;->b()V

    return v3
.end method
