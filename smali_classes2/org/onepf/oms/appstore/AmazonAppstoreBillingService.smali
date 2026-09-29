.class public Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;
.super Ljava/lang/Object;
.source "AmazonAppstoreBillingService.java"

# interfaces
.implements Lorg/onepf/oms/AppstoreInAppBillingService;
.implements Lcom/amazon/device/iap/PurchasingListener;


# static fields
.field public static final JSON_KEY_ORDER_ID:Ljava/lang/String; = "orderId"

.field public static final JSON_KEY_PRODUCT_ID:Ljava/lang/String; = "productId"

.field public static final JSON_KEY_PURCHASE_STATUS:Ljava/lang/String; = "purchaseStatus"

.field public static final JSON_KEY_RECEIPT_ITEM_TYPE:Ljava/lang/String; = "itemType"

.field public static final JSON_KEY_RECEIPT_PURCHASE_TOKEN:Ljava/lang/String; = "purchaseToken"

.field public static final JSON_KEY_USER_ID:Ljava/lang/String; = "userId"


# instance fields
.field private final context:Landroid/content/Context;

.field private currentUserId:Ljava/lang/String;

.field private final inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

.field private final inventoryLatchQueue:Ljava/util/Queue;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Queue<",
            "Ljava/util/concurrent/CountDownLatch;",
            ">;"
        }
    .end annotation
.end field

.field private final requestListeners:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Lcom/amazon/device/iap/model/RequestId;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;",
            ">;"
        }
    .end annotation
.end field

.field private final requestSkuMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Lcom/amazon/device/iap/model/RequestId;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 123
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 77
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->requestListeners:Ljava/util/Map;

    .line 108
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    .line 114
    new-instance v0, Ljava/util/concurrent/ConcurrentLinkedQueue;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentLinkedQueue;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventoryLatchQueue:Ljava/util/Queue;

    .line 322
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->requestSkuMap:Ljava/util/Map;

    .line 124
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->context:Landroid/content/Context;

    return-void
.end method

.method private generateOriginalJson(Lcom/amazon/device/iap/model/PurchaseResponse;)Ljava/lang/String;
    .locals 4
    .param p1    # Lcom/amazon/device/iap/model/PurchaseResponse;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 418
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 420
    :try_start_0
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getReceipt()Lcom/amazon/device/iap/model/Receipt;

    move-result-object v1

    const-string v2, "orderId"

    .line 421
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getRequestId()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v2, "productId"

    .line 422
    invoke-virtual {v1}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 423
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    move-result-object v2

    if-eqz v2, :cond_0

    const-string v3, "purchaseStatus"

    .line 425
    invoke-virtual {v2}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->name()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v3, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 427
    :cond_0
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object p1

    if-eqz p1, :cond_1

    const-string v2, "userId"

    .line 429
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserData;->getUserId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, v2, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 431
    :cond_1
    invoke-virtual {v1}, Lcom/amazon/device/iap/model/Receipt;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object p1

    if-eqz p1, :cond_2

    const-string v2, "itemType"

    .line 433
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/ProductType;->name()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, v2, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    :cond_2
    const-string p1, "purchaseToken"

    .line 435
    invoke-virtual {v1}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, p1, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const/4 p1, 0x2

    .line 436
    new-array p1, p1, [Ljava/lang/Object;

    const/4 v1, 0x0

    const-string v2, "generateOriginalJson(): JSON\n"

    aput-object v2, p1, v1

    const/4 v1, 0x1

    aput-object v0, p1, v1

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v1, "generateOriginalJson() failed to generate JSON"

    .line 438
    invoke-static {v1, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 440
    :goto_0
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private getPurchase(Lcom/amazon/device/iap/model/Receipt;)Lorg/onepf/oms/appstore/googleUtils/Purchase;
    .locals 6
    .param p1    # Lcom/amazon/device/iap/model/Receipt;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 248
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    const-string v1, "com.amazon.apps"

    invoke-direct {v0, v1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;)V

    if-nez p1, :cond_0

    return-object v0

    .line 253
    :cond_0
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v1

    .line 254
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v2

    const-string v3, "com.amazon.apps"

    invoke-virtual {v2, v3, v1}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 255
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Receipt;->getReceiptId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setToken(Ljava/lang/String;)V

    .line 257
    sget-object v2, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductType:[I

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Receipt;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object p1

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/ProductType;->ordinal()I

    move-result p1

    aget p1, v2, p1

    const/4 v2, 0x1

    const/4 v3, 0x0

    const/4 v4, 0x2

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    const-string p1, "subs"

    .line 266
    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setItemType(Ljava/lang/String;)V

    .line 267
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object p1

    const-string v5, "com.amazon.apps"

    invoke-virtual {p1, v5, v1}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 268
    new-array p1, v4, [Ljava/lang/Object;

    const-string v4, "Add subscription to inventory SKU: "

    aput-object v4, p1, v3

    aput-object v1, p1, v2

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    goto :goto_0

    :pswitch_1
    const-string p1, "inapp"

    .line 261
    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setItemType(Ljava/lang/String;)V

    .line 262
    new-array p1, v4, [Ljava/lang/Object;

    const-string v4, "Add to inventory SKU: "

    aput-object v4, p1, v3

    aput-object v1, p1, v2

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    :goto_0
    return-object v0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private getSkuDetails(Lcom/amazon/device/iap/model/Product;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;
    .locals 8
    .param p1    # Lcom/amazon/device/iap/model/Product;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 302
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Product;->getSku()Ljava/lang/String;

    move-result-object v0

    .line 303
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Product;->getPrice()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->toString()Ljava/lang/String;

    move-result-object v6

    .line 304
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Product;->getTitle()Ljava/lang/String;

    move-result-object v5

    .line 305
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Product;->getDescription()Ljava/lang/String;

    move-result-object v7

    .line 306
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/Product;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object p1

    const-string v1, "Item: %s\n Type: %s\n SKU: %s\n Price: %s\n Description: %s\n"

    const/4 v2, 0x5

    .line 307
    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object v5, v2, v3

    const/4 v3, 0x1

    aput-object p1, v2, v3

    const/4 v3, 0x2

    aput-object v0, v2, v3

    const/4 v3, 0x3

    aput-object v6, v2, v3

    const/4 v3, 0x4

    aput-object v7, v2, v3

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 310
    sget-object v1, Lcom/amazon/device/iap/model/ProductType;->SUBSCRIPTION:Lcom/amazon/device/iap/model/ProductType;

    if-ne p1, v1, :cond_0

    const-string p1, "subs"

    :goto_0
    move-object v3, p1

    goto :goto_1

    :cond_0
    const-string p1, "inapp"

    goto :goto_0

    .line 313
    :goto_1
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object p1

    const-string v1, "com.amazon.apps"

    invoke-virtual {p1, v1, v0}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 314
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-object v2, p1

    invoke-direct/range {v2 .. v7}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-object p1
.end method


# virtual methods
.method public consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 1

    .line 445
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getToken()Ljava/lang/String;

    move-result-object p1

    sget-object v0, Lcom/amazon/device/iap/model/FulfillmentResult;->FULFILLED:Lcom/amazon/device/iap/model/FulfillmentResult;

    invoke-static {p1, v0}, Lcom/amazon/device/iap/PurchasingService;->notifyFulfillment(Ljava/lang/String;Lcom/amazon/device/iap/model/FulfillmentResult;)V

    return-void
.end method

.method public dispose()V
    .locals 1

    const/4 v0, 0x0

    .line 455
    iput-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    return-void
.end method

.method public handleActivityResult(IILandroid/content/Intent;)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 0

    .line 332
    invoke-static {p2}, Lcom/amazon/device/iap/PurchasingService;->purchase(Ljava/lang/String;)Lcom/amazon/device/iap/model/RequestId;

    move-result-object p1

    .line 333
    iget-object p3, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->requestSkuMap:Ljava/util/Map;

    invoke-interface {p3, p1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 334
    iget-object p2, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->requestListeners:Ljava/util/Map;

    invoke-interface {p2, p1, p5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public onProductDataResponse(Lcom/amazon/device/iap/model/ProductDataResponse;)V
    .locals 6
    .param p1    # Lcom/amazon/device/iap/model/ProductDataResponse;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 276
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/ProductDataResponse;->getRequestStatus()Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;

    move-result-object v0

    .line 277
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/ProductDataResponse;->getRequestId()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v1

    const/4 v2, 0x4

    .line 278
    new-array v2, v2, [Ljava/lang/Object;

    const-string v3, "onItemDataResponse() reqStatus: "

    const/4 v4, 0x0

    aput-object v3, v2, v4

    const/4 v3, 0x1

    aput-object v0, v2, v3

    const-string v4, ", reqId: "

    const/4 v5, 0x2

    aput-object v4, v2, v5

    const/4 v4, 0x3

    aput-object v1, v2, v4

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 281
    sget-object v1, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$ProductDataResponse$RequestStatus:[I

    invoke-virtual {v0}, Lcom/amazon/device/iap/model/ProductDataResponse$RequestStatus;->ordinal()I

    move-result v0

    aget v0, v1, v0

    if-eq v0, v3, :cond_0

    goto :goto_1

    .line 283
    :cond_0
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/ProductDataResponse;->getProductData()Ljava/util/Map;

    move-result-object p1

    .line 284
    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 285
    invoke-interface {p1, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/amazon/device/iap/model/Product;

    .line 286
    iget-object v2, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {p0, v1}, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->getSkuDetails(Lcom/amazon/device/iap/model/Product;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-result-object v1

    invoke-virtual {v2, v1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V

    goto :goto_0

    .line 294
    :cond_1
    :goto_1
    iget-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventoryLatchQueue:Ljava/util/Queue;

    invoke-interface {p1}, Ljava/util/Queue;->poll()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/concurrent/CountDownLatch;

    if-eqz p1, :cond_2

    .line 296
    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    :cond_2
    return-void
.end method

.method public onPurchaseResponse(Lcom/amazon/device/iap/model/PurchaseResponse;)V
    .locals 12
    .param p1    # Lcom/amazon/device/iap/model/PurchaseResponse;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 339
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;

    move-result-object v0

    .line 340
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getRequestId()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v1

    const/4 v2, 0x4

    .line 341
    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "onPurchaseResponse() PurchaseRequestStatus:"

    const/4 v5, 0x0

    aput-object v4, v3, v5

    const/4 v4, 0x1

    aput-object v0, v3, v4

    const-string v6, ", reqId: "

    const/4 v7, 0x2

    aput-object v6, v3, v7

    const/4 v6, 0x3

    aput-object v1, v3, v6

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 344
    iget-object v3, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->requestSkuMap:Ljava/util/Map;

    invoke-interface {v3, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 345
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getReceipt()Lcom/amazon/device/iap/model/Receipt;

    move-result-object v8

    .line 346
    invoke-direct {p0, v8}, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->getPurchase(Lcom/amazon/device/iap/model/Receipt;)Lorg/onepf/oms/appstore/googleUtils/Purchase;

    move-result-object v9

    .line 348
    sget-object v10, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseResponse$RequestStatus:[I

    invoke-virtual {v0}, Lcom/amazon/device/iap/model/PurchaseResponse$RequestStatus;->ordinal()I

    move-result v0

    aget v0, v10, v0

    const/4 v10, 0x6

    packed-switch v0, :pswitch_data_0

    const/4 p1, 0x0

    goto/16 :goto_1

    .line 388
    :pswitch_0
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "This call is not supported"

    invoke-direct {p1, v6, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto/16 :goto_1

    .line 385
    :pswitch_1
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "Purchase failed"

    invoke-direct {p1, v10, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto/16 :goto_1

    .line 382
    :pswitch_2
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v0, 0x7

    const-string v2, "Item is already purchased"

    invoke-direct {p1, v0, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto :goto_1

    .line 379
    :pswitch_3
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "Invalid SKU"

    invoke-direct {p1, v2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto :goto_1

    .line 350
    :pswitch_4
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseResponse;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object v0

    .line 351
    invoke-virtual {v0}, Lcom/amazon/device/iap/model/UserData;->getUserId()Ljava/lang/String;

    move-result-object v0

    .line 352
    iget-object v11, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->currentUserId:Ljava/lang/String;

    invoke-virtual {v0, v11}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v11

    if-nez v11, :cond_0

    .line 353
    new-array p1, v2, [Ljava/lang/Object;

    const-string v2, "onPurchaseResponse() Current UserId: "

    aput-object v2, p1, v5

    iget-object v2, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->currentUserId:Ljava/lang/String;

    aput-object v2, p1, v4

    const-string v2, ", purchase UserId: "

    aput-object v2, p1, v7

    aput-object v0, p1, v6

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->w([Ljava/lang/Object;)V

    .line 355
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "Current UserId doesn\'t match purchase UserId"

    invoke-direct {p1, v10, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto :goto_1

    .line 360
    :cond_0
    invoke-direct {p0, p1}, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->generateOriginalJson(Lcom/amazon/device/iap/model/PurchaseResponse;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v9, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setOriginalJson(Ljava/lang/String;)V

    .line 361
    invoke-virtual {v1}, Lcom/amazon/device/iap/model/RequestId;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v9, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setOrderId(Ljava/lang/String;)V

    .line 363
    invoke-virtual {v8}, Lcom/amazon/device/iap/model/Receipt;->getProductType()Lcom/amazon/device/iap/model/ProductType;

    move-result-object p1

    .line 364
    invoke-virtual {v8}, Lcom/amazon/device/iap/model/Receipt;->getSku()Ljava/lang/String;

    move-result-object v0

    .line 365
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v2

    const-string v4, "com.amazon.apps"

    sget-object v6, Lcom/amazon/device/iap/model/ProductType;->SUBSCRIPTION:Lcom/amazon/device/iap/model/ProductType;

    if-ne p1, v6, :cond_1

    move-object v0, v3

    :cond_1
    invoke-virtual {v2, v4, v0}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 368
    invoke-virtual {v9, v0}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 370
    sget-object v0, Lcom/amazon/device/iap/model/ProductType;->SUBSCRIPTION:Lcom/amazon/device/iap/model/ProductType;

    if-ne p1, v0, :cond_2

    const-string p1, "subs"

    goto :goto_0

    :cond_2
    const-string p1, "inapp"

    .line 373
    :goto_0
    invoke-virtual {v9, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setItemType(Ljava/lang/String;)V

    .line 375
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "Success"

    invoke-direct {p1, v5, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 393
    :goto_1
    iget-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->requestListeners:Ljava/util/Map;

    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz v0, :cond_3

    .line 395
    invoke-interface {v0, p1, v9}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_2

    :cond_3
    const-string p1, "Something went wrong: PurchaseFinishedListener is not found"

    .line 397
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    :goto_2
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public onPurchaseUpdatesResponse(Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;)V
    .locals 8

    .line 211
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    move-result-object v0

    .line 212
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->getRequestId()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v1

    const/4 v2, 0x4

    .line 213
    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "onPurchaseUpdatesResponse() reqStatus: "

    const/4 v5, 0x0

    aput-object v4, v3, v5

    const/4 v4, 0x1

    aput-object v0, v3, v4

    const-string v6, "reqId: "

    const/4 v7, 0x2

    aput-object v6, v3, v7

    const/4 v6, 0x3

    aput-object v1, v3, v6

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 216
    sget-object v1, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$PurchaseUpdatesResponse$RequestStatus:[I

    invoke-virtual {v0}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;->ordinal()I

    move-result v0

    aget v0, v1, v0

    if-eq v0, v4, :cond_0

    goto :goto_2

    .line 218
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getAllOwnedSkus()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 219
    iget-object v3, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-virtual {v3, v1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->erasePurchase(Ljava/lang/String;)V

    goto :goto_0

    .line 221
    :cond_1
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object v0

    .line 222
    invoke-virtual {v0}, Lcom/amazon/device/iap/model/UserData;->getUserId()Ljava/lang/String;

    move-result-object v0

    .line 223
    iget-object v1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->currentUserId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_2

    .line 224
    new-array p1, v2, [Ljava/lang/Object;

    const-string v1, "onPurchaseUpdatesResponse() Current UserId: "

    aput-object v1, p1, v5

    iget-object v1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->currentUserId:Ljava/lang/String;

    aput-object v1, p1, v4

    const-string v1, ", purchase UserId: "

    aput-object v1, p1, v7

    aput-object v0, p1, v6

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->w([Ljava/lang/Object;)V

    goto :goto_2

    .line 228
    :cond_2
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->getReceipts()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_3

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/amazon/device/iap/model/Receipt;

    .line 229
    iget-object v2, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {p0, v1}, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->getPurchase(Lcom/amazon/device/iap/model/Receipt;)Lorg/onepf/oms/appstore/googleUtils/Purchase;

    move-result-object v1

    invoke-virtual {v2, v1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addPurchase(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_1

    .line 231
    :cond_3
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->hasMore()Z

    move-result p1

    if-eqz p1, :cond_4

    .line 232
    invoke-static {v5}, Lcom/amazon/device/iap/PurchasingService;->getPurchaseUpdates(Z)Lcom/amazon/device/iap/model/RequestId;

    const-string p1, "Initiating Another Purchase Updates with offset: "

    .line 233
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return-void

    .line 240
    :cond_4
    :goto_2
    iget-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventoryLatchQueue:Ljava/util/Queue;

    invoke-interface {p1}, Ljava/util/Queue;->poll()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/concurrent/CountDownLatch;

    if-eqz p1, :cond_5

    .line 242
    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    :cond_5
    return-void
.end method

.method public onUserDataResponse(Lcom/amazon/device/iap/model/UserDataResponse;)V
    .locals 6

    const/4 v0, 0x4

    .line 139
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "onUserDataResponse() reqId: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserDataResponse;->getRequestId()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v1

    const/4 v3, 0x1

    aput-object v1, v0, v3

    const-string v1, ", status: "

    const/4 v4, 0x2

    aput-object v1, v0, v4

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserDataResponse;->getRequestStatus()Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;

    move-result-object v1

    const/4 v5, 0x3

    aput-object v1, v0, v5

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 143
    sget-object v0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService$1;->$SwitchMap$com$amazon$device$iap$model$UserDataResponse$RequestStatus:[I

    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserDataResponse;->getRequestStatus()Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;

    move-result-object v1

    invoke-virtual {v1}, Lcom/amazon/device/iap/model/UserDataResponse$RequestStatus;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    .line 158
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "Unknown response code"

    invoke-direct {p1, v5, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto :goto_0

    .line 154
    :pswitch_0
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v0, 0x6

    const-string v1, "Unable to get userId"

    invoke-direct {p1, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    const-string v0, "onUserDataResponse() Unable to get user ID"

    .line 155
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    goto :goto_0

    .line 145
    :pswitch_1
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserDataResponse;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object p1

    .line 146
    invoke-virtual {p1}, Lcom/amazon/device/iap/model/UserData;->getUserId()Ljava/lang/String;

    move-result-object p1

    .line 147
    iput-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->currentUserId:Ljava/lang/String;

    .line 148
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v1, "Setup successful."

    invoke-direct {v0, v2, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 149
    new-array v1, v4, [Ljava/lang/Object;

    const-string v4, "Set current userId: "

    aput-object v4, v1, v2

    aput-object p1, v1, v3

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    move-object p1, v0

    .line 160
    :goto_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz v0, :cond_0

    .line 161
    iget-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-interface {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    const/4 p1, 0x0

    .line 162
    iput-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    :cond_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method public queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 6
    .param p2    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;"
        }
    .end annotation

    const/4 v0, 0x6

    .line 168
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "queryInventory() querySkuDetails: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    const/4 v3, 0x1

    aput-object v1, v0, v3

    const-string v1, " moreItemSkus: "

    const/4 v4, 0x2

    aput-object v1, v0, v4

    const/4 v1, 0x3

    aput-object p2, v0, v1

    const-string v1, " moreSubsSkus: "

    const/4 v5, 0x4

    aput-object v1, v0, v5

    const/4 v1, 0x5

    aput-object p3, v0, v1

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 171
    new-instance v0, Ljava/util/concurrent/CountDownLatch;

    invoke-direct {v0, v3}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    .line 172
    iget-object v1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventoryLatchQueue:Ljava/util/Queue;

    invoke-interface {v1, v0}, Ljava/util/Queue;->offer(Ljava/lang/Object;)Z

    .line 173
    invoke-static {v3}, Lcom/amazon/device/iap/PurchasingService;->getPurchaseUpdates(Z)Lcom/amazon/device/iap/model/RequestId;

    const/4 v1, 0x0

    .line 175
    :try_start_0
    invoke-virtual {v0}, Ljava/util/concurrent/CountDownLatch;->await()V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_1

    if-eqz p1, :cond_3

    .line 182
    new-instance p1, Ljava/util/HashSet;

    iget-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getAllOwnedSkus()Ljava/util/List;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    if-eqz p2, :cond_0

    .line 184
    invoke-interface {p1, p2}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    :cond_0
    if-eqz p3, :cond_1

    .line 187
    invoke-interface {p1, p3}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    .line 189
    :cond_1
    invoke-interface {p1}, Ljava/util/Set;->isEmpty()Z

    move-result p2

    if-nez p2, :cond_3

    .line 190
    new-instance p2, Ljava/util/HashSet;

    invoke-interface {p1}, Ljava/util/Set;->size()I

    move-result p3

    invoke-direct {p2, p3}, Ljava/util/HashSet;-><init>(I)V

    .line 191
    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p3

    if-eqz p3, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Ljava/lang/String;

    .line 192
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    const-string v5, "com.amazon.apps"

    invoke-virtual {v0, v5, p3}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p2, p3}, Ljava/util/HashSet;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 194
    :cond_2
    new-instance p1, Ljava/util/concurrent/CountDownLatch;

    invoke-direct {p1, v3}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    .line 195
    iget-object p3, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventoryLatchQueue:Ljava/util/Queue;

    invoke-interface {p3, p1}, Ljava/util/Queue;->offer(Ljava/lang/Object;)Z

    .line 196
    invoke-static {p2}, Lcom/amazon/device/iap/PurchasingService;->getProductData(Ljava/util/Set;)Lcom/amazon/device/iap/model/RequestId;

    .line 198
    :try_start_1
    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->await()V
    :try_end_1
    .catch Ljava/lang/InterruptedException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_1

    :catch_0
    const-string p1, "queryInventory() SkuDetails fetching interrupted"

    .line 200
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;)V

    return-object v1

    .line 205
    :cond_3
    :goto_1
    new-array p1, v4, [Ljava/lang/Object;

    const-string p2, "queryInventory() finished. Inventory size: "

    aput-object p2, p1, v2

    iget-object p2, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-virtual {p2}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getAllOwnedSkus()Ljava/util/List;

    move-result-object p2

    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result p2

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, p1, v3

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 206
    iget-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->inventory:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    return-object p1

    :catch_1
    const-string p1, "queryInventory() await interrupted"

    .line 177
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    return-object v1
.end method

.method public startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 132
    iput-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    .line 133
    iget-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;->context:Landroid/content/Context;

    invoke-static {p1, p0}, Lcom/amazon/device/iap/PurchasingService;->registerListener(Landroid/content/Context;Lcom/amazon/device/iap/PurchasingListener;)V

    .line 134
    invoke-static {}, Lcom/amazon/device/iap/PurchasingService;->getUserData()Lcom/amazon/device/iap/model/RequestId;

    return-void
.end method

.method public subscriptionsSupported()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
