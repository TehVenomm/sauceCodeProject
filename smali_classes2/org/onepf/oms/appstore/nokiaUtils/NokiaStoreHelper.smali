.class public Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;
.super Ljava/lang/Object;
.source "NokiaStoreHelper.java"

# interfaces
.implements Lorg/onepf/oms/AppstoreInAppBillingService;


# static fields
.field public static final RESULT_BAD_RESPONSE:I = -0x3ea

.field public static final RESULT_BILLING_UNAVAILABLE:I = 0x3

.field public static final RESULT_DEVELOPER_ERROR:I = 0x5

.field public static final RESULT_ERROR:I = 0x6

.field public static final RESULT_ITEM_ALREADY_OWNED:I = 0x7

.field public static final RESULT_ITEM_NOT_OWNED:I = 0x8

.field public static final RESULT_ITEM_UNAVAILABLE:I = 0x4

.field public static final RESULT_NO_SIM:I = 0x9

.field public static final RESULT_OK:I = 0x0

.field public static final RESULT_USER_CANCELED:I = 0x1


# instance fields
.field private final mContext:Landroid/content/Context;

.field private mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field mRequestCode:I

.field private mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private mServiceConn:Landroid/content/ServiceConnection;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;Lorg/onepf/oms/Appstore;)V
    .locals 0

    .line 69
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p2, 0x0

    .line 62
    iput-object p2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mServiceConn:Landroid/content/ServiceConnection;

    .line 64
    iput-object p2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    .line 66
    iput-object p2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    .line 71
    iput-object p1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mContext:Landroid/content/Context;

    return-void
.end method

.method static synthetic access$000(Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;)Lcom/nokia/payment/iap/aidl/INokiaIAPService;
    .locals 0

    .line 45
    iget-object p0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    return-object p0
.end method

.method static synthetic access$002(Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;Lcom/nokia/payment/iap/aidl/INokiaIAPService;)Lcom/nokia/payment/iap/aidl/INokiaIAPService;
    .locals 0

    .line 45
    iput-object p1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    return-object p1
.end method

.method private getServiceIntent()Landroid/content/Intent;
    .locals 2
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 157
    new-instance v0, Landroid/content/Intent;

    const-string v1, "com.nokia.payment.iapenabler.InAppBillingService.BIND"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v1, "com.nokia.payment.iapenabler"

    .line 159
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    return-object v0
.end method

.method private processDetailsList(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V
    .locals 8
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/Inventory;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "NokiaStoreHelper.processDetailsList"

    .line 582
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 584
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 585
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 586
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    const-string v3, "inapp"

    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v2

    const-string v4, "com.nokia.nstore"

    const-string v5, "productId"

    invoke-virtual {v1, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v4, v5}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v2, "title"

    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v2, "price"

    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v2, "shortdescription"

    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    move-object v2, v0

    invoke-direct/range {v2 .. v7}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p2, v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method private processPurchaseSuccess(Ljava/lang/String;)V
    .locals 6

    const-string v0, "NokiaStoreHelper.processPurchaseSuccess"

    .line 345
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    const/4 v0, 0x2

    .line 346
    new-array v1, v0, [Ljava/lang/Object;

    const-string v2, "purchaseData = "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    const/4 v2, 0x1

    aput-object p1, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 350
    :try_start_0
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 352
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object p1

    const-string v4, "com.nokia.nstore"

    const-string v5, "productId"

    invoke-virtual {v1, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {p1, v4, v5}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 354
    new-array v4, v0, [Ljava/lang/Object;

    const-string v5, "sku = "

    aput-object v5, v4, v3

    aput-object p1, v4, v2

    invoke-static {v4}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 356
    new-instance v4, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    const-string v5, "com.nokia.nstore"

    invoke-direct {v4, v5}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;)V

    const-string v5, "inapp"

    .line 358
    invoke-virtual {v4, v5}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setItemType(Ljava/lang/String;)V

    const-string v5, "orderId"

    .line 359
    invoke-virtual {v1, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setOrderId(Ljava/lang/String;)V

    const-string v5, "packageName"

    .line 360
    invoke-virtual {v1, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPackageName(Ljava/lang/String;)V

    .line 361
    invoke-virtual {v4, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    const-string p1, "purchaseToken"

    .line 362
    invoke-virtual {v1, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v4, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setToken(Ljava/lang/String;)V

    const-string p1, "developerPayload"

    .line 363
    invoke-virtual {v1, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v4, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setDeveloperPayload(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 376
    iget-object p1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p1, :cond_0

    .line 377
    iget-object p1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v1, "Success"

    invoke-direct {v0, v3, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v0, v4}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_0
    return-void

    :catch_0
    move-exception p1

    .line 366
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "JSONException: "

    aput-object v1, v0, v3

    aput-object p1, v0, v2

    invoke-static {p1, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    .line 368
    new-instance p1, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const/16 v0, -0x3ea

    const-string v1, "Failed to parse purchase data."

    invoke-direct {p1, v0, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    .line 369
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz v0, :cond_1

    .line 370
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    const/4 v1, 0x0

    invoke-interface {v0, p1, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_1
    return-void
.end method

.method private processPurchasedList(Ljava/util/ArrayList;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V
    .locals 6
    .param p1    # Ljava/util/ArrayList;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/Inventory;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;",
            ")V"
        }
    .end annotation

    const-string v0, "NokiaStoreHelper.processPurchasedList"

    .line 510
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 512
    invoke-virtual {p1}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    const/4 v1, 0x0

    .line 514
    :try_start_0
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 515
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    const-string v3, "com.nokia.nstore"

    invoke-direct {v0, v3}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;)V

    const-string v3, "inapp"

    .line 516
    invoke-virtual {v0, v3}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setItemType(Ljava/lang/String;)V

    .line 517
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v3

    const-string v4, "com.nokia.nstore"

    const-string v5, "productId"

    invoke-virtual {v2, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v4, v5}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    const-string v3, "purchaseToken"

    .line 518
    invoke-virtual {v2, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setToken(Ljava/lang/String;)V

    .line 519
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPackageName(Ljava/lang/String;)V

    .line 520
    invoke-virtual {v0, v1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPurchaseState(I)V

    const-string v3, "developerPayload"

    const-string v4, ""

    .line 521
    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setDeveloperPayload(Ljava/lang/String;)V

    .line 522
    invoke-virtual {p2, v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addPurchase(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const/4 v2, 0x2

    .line 524
    new-array v2, v2, [Ljava/lang/Object;

    const-string v3, "Exception: "

    aput-object v3, v2, v1

    const/4 v1, 0x1

    aput-object v0, v2, v1

    invoke-static {v0, v2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    goto :goto_0

    :cond_0
    return-void
.end method

.method private refreshItemDetails(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V
    .locals 7
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/Inventory;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    const-string v0, "NokiaStoreHelper.refreshItemDetails"

    .line 530
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 532
    new-instance v0, Landroid/os/Bundle;

    const/16 v1, 0x20

    invoke-direct {v0, v1}, Landroid/os/Bundle;-><init>(I)V

    .line 534
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2, v1}, Ljava/util/ArrayList;-><init>(I)V

    .line 536
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v1

    const-string v3, "com.nokia.nstore"

    invoke-virtual {v1, v3}, Lorg/onepf/oms/SkuManager;->getAllStoreSkus(Ljava/lang/String;)Ljava/util/List;

    move-result-object v1

    .line 538
    invoke-static {v1}, Lorg/onepf/oms/util/CollectionUtils;->isEmpty(Ljava/util/Collection;)Z

    move-result v3

    if-nez v3, :cond_0

    .line 539
    invoke-virtual {v2, v1}, Ljava/util/ArrayList;->addAll(Ljava/util/Collection;)Z

    :cond_0
    if-eqz p1, :cond_1

    .line 543
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 544
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v3

    const-string v4, "com.nokia.nstore"

    invoke-virtual {v3, v4, v1}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v2, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    const-string p1, "ITEM_ID_LIST"

    .line 548
    invoke-virtual {v0, p1, v2}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    const/4 p1, 0x1

    const/4 v1, 0x0

    const/4 v2, 0x2

    .line 551
    :try_start_0
    iget-object v3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    if-eqz v3, :cond_3

    .line 556
    iget-object v3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    const/4 v4, 0x3

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->getPackageName()Ljava/lang/String;

    move-result-object v5

    const-string v6, "inapp"

    invoke-interface {v3, v4, v5, v6, v0}, Lcom/nokia/payment/iap/aidl/INokiaIAPService;->getProductDetails(ILjava/lang/String;Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v0

    const-string v3, "RESPONSE_CODE"

    .line 560
    invoke-virtual {v0, v3}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v3

    const-string v4, "DETAILS_LIST"

    .line 561
    invoke-virtual {v0, v4}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v0

    .line 563
    new-array v4, v2, [Ljava/lang/Object;

    const-string v5, "responseCode = "

    aput-object v5, v4, v1

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    aput-object v5, v4, p1

    invoke-static {v4}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 564
    new-array v4, v2, [Ljava/lang/Object;

    const-string v5, "detailsList = "

    aput-object v5, v4, v1

    aput-object v0, v4, p1

    invoke-static {v4}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-nez v3, :cond_2

    .line 570
    invoke-direct {p0, v0, p2}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->processDetailsList(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V

    goto :goto_1

    .line 567
    :cond_2
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v4, "Error refreshing inventory (querying prices of items)."

    invoke-direct {v0, v3, v4}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-direct {p2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    throw p2

    :cond_3
    const-string p2, "Unable to refresh item details."

    .line 552
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 553
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v0, -0x3ea

    const-string v3, "Error refreshing item details."

    invoke-direct {p2, v0, v3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p2
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    move-exception p2

    .line 575
    new-array v0, v2, [Ljava/lang/Object;

    const-string v2, "Exception: "

    aput-object v2, v0, v1

    aput-object p2, v0, p1

    invoke-static {p2, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    goto :goto_1

    :catch_1
    move-exception p2

    .line 573
    new-array v0, v2, [Ljava/lang/Object;

    const-string v2, "Exception: "

    aput-object v2, v0, v1

    aput-object p2, v0, p1

    invoke-static {p2, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    :goto_1
    return-void
.end method

.method private refreshPurchasedItems(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V
    .locals 8
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/Inventory;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    const-string v0, "NokiaStoreHelper.refreshPurchasedItems"

    .line 466
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 468
    new-instance v0, Ljava/util/ArrayList;

    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v1

    const-string v2, "com.nokia.nstore"

    invoke-virtual {v1, v2}, Lorg/onepf/oms/SkuManager;->getAllStoreSkus(Ljava/lang/String;)Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 470
    new-instance v6, Landroid/os/Bundle;

    const/16 v1, 0x20

    invoke-direct {v6, v1}, Landroid/os/Bundle;-><init>(I)V

    if-eqz p1, :cond_0

    .line 473
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 474
    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    const-string p1, "ITEM_ID_LIST"

    .line 478
    invoke-virtual {v6, p1, v0}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    const/4 p1, 0x1

    const/4 v0, 0x0

    const/4 v1, 0x2

    .line 481
    :try_start_0
    iget-object v2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    if-eqz v2, :cond_2

    .line 486
    iget-object v2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    const/4 v3, 0x3

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->getPackageName()Ljava/lang/String;

    move-result-object v4

    const-string v5, "inapp"

    const/4 v7, 0x0

    invoke-interface/range {v2 .. v7}, Lcom/nokia/payment/iap/aidl/INokiaIAPService;->getPurchases(ILjava/lang/String;Ljava/lang/String;Landroid/os/Bundle;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v2

    const-string v3, "RESPONSE_CODE"

    .line 490
    invoke-virtual {v2, v3}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v3

    const-string v4, "INAPP_PURCHASE_ITEM_LIST"

    .line 491
    invoke-virtual {v2, v4}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v4

    const-string v5, "INAPP_PURCHASE_DATA_LIST"

    .line 492
    invoke-virtual {v2, v5}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v2

    .line 494
    new-array v5, v1, [Ljava/lang/Object;

    const-string v6, "responseCode = "

    aput-object v6, v5, v0

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    aput-object v6, v5, p1

    invoke-static {v5}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 495
    new-array v5, v1, [Ljava/lang/Object;

    const-string v6, "purchasedItemList = "

    aput-object v6, v5, v0

    aput-object v4, v5, p1

    invoke-static {v5}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 496
    new-array v4, v1, [Ljava/lang/Object;

    const-string v5, "purchasedDataList = "

    aput-object v5, v4, v0

    aput-object v2, v4, p1

    invoke-static {v4}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-nez v3, :cond_1

    .line 502
    invoke-direct {p0, v2, p2}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->processPurchasedList(Ljava/util/ArrayList;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V

    goto :goto_1

    .line 499
    :cond_1
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    new-instance v2, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v4, "Error refreshing inventory (querying owned items)."

    invoke-direct {v2, v3, v4}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-direct {p2, v2}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    throw p2

    :cond_2
    const-string p2, "Unable to refresh purchased items."

    .line 482
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 483
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v2, -0x3ea

    const-string v3, "Error refreshing inventory (querying owned items)."

    invoke-direct {p2, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p2
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    move-exception p2

    .line 505
    new-array v1, v1, [Ljava/lang/Object;

    const-string v2, "Exception: "

    aput-object v2, v1, v0

    aput-object p2, v1, p1

    invoke-static {p2, v1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    :goto_1
    return-void
.end method


# virtual methods
.method public consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 7
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/Purchase;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    const-string v0, "NokiaStoreHelper.consume"

    .line 394
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 396
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getToken()Ljava/lang/String;

    move-result-object v0

    .line 397
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v1

    .line 398
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getPackageName()Ljava/lang/String;

    move-result-object p1

    const/4 v2, 0x2

    .line 400
    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "productId = "

    const/4 v5, 0x0

    aput-object v4, v3, v5

    const/4 v4, 0x1

    aput-object v1, v3, v4

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 401
    new-array v3, v2, [Ljava/lang/Object;

    const-string v6, "token = "

    aput-object v6, v3, v5

    aput-object v0, v3, v4

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 402
    new-array v3, v2, [Ljava/lang/Object;

    const-string v6, "packageName = "

    aput-object v6, v3, v5

    aput-object p1, v3, v4

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const/4 v3, 0x3

    .line 406
    :try_start_0
    iget-object v6, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    invoke-interface {v6, v3, p1, v1, v0}, Lcom/nokia/payment/iap/aidl/INokiaIAPService;->consumePurchase(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 408
    new-array v0, v2, [Ljava/lang/Object;

    const-string v6, "RemoteException: "

    aput-object v6, v0, v5

    aput-object p1, v0, v4

    invoke-static {p1, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    const/4 p1, 0x0

    :goto_0
    if-nez p1, :cond_0

    .line 412
    new-array p1, v2, [Ljava/lang/Object;

    const-string v0, "Successfully consumed productId: "

    aput-object v0, p1, v5

    aput-object v1, p1, v4

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const-string p1, "consume: done"

    .line 418
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return-void

    :cond_0
    const/4 v0, 0x4

    .line 414
    new-array v0, v0, [Ljava/lang/Object;

    const-string v6, "Error consuming consuming productId "

    aput-object v6, v0, v5

    aput-object v1, v0, v4

    const-string v4, ". Code: "

    aput-object v4, v0, v2

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    aput-object v2, v0, v3

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 415
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    new-instance v2, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Error consuming productId "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v2, p1, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-direct {v0, v2}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    throw v0
.end method

.method public dispose()V
    .locals 2

    const-string v0, "NokiaStoreHelper.dispose"

    .line 597
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 599
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mServiceConn:Landroid/content/ServiceConnection;

    if-eqz v0, :cond_1

    .line 600
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mContext:Landroid/content/Context;

    if-eqz v0, :cond_0

    .line 601
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mContext:Landroid/content/Context;

    iget-object v1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mServiceConn:Landroid/content/ServiceConnection;

    invoke-virtual {v0, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    :cond_0
    const/4 v0, 0x0

    .line 603
    iput-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mServiceConn:Landroid/content/ServiceConnection;

    .line 604
    iput-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    :cond_1
    return-void
.end method

.method public getPackageName()Ljava/lang/String;
    .locals 1

    .line 609
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mContext:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public handleActivityResult(IILandroid/content/Intent;)Z
    .locals 6
    .param p3    # Landroid/content/Intent;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    const-string v0, "NokiaStoreHelper.handleActivityResult"

    .line 267
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 269
    iget v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mRequestCode:I

    const/4 v1, 0x0

    if-eq p1, v0, :cond_0

    return v1

    :cond_0
    const/4 p1, 0x0

    const/4 v0, 0x1

    if-nez p3, :cond_2

    const-string p2, "Null data in IAB activity result."

    .line 276
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 277
    new-instance p2, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const/16 p3, -0x3ea

    const-string v1, "Null data in IAB result"

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    .line 279
    iget-object p3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_1

    .line 280
    iget-object p3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_1
    return v0

    :cond_2
    const-string v2, "RESPONSE_CODE"

    .line 286
    invoke-virtual {p3, v2, v1}, Landroid/content/Intent;->getIntExtra(Ljava/lang/String;I)I

    move-result v2

    const-string v3, "INAPP_PURCHASE_DATA"

    .line 287
    invoke-virtual {p3, v3}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    const/4 v3, 0x2

    .line 289
    new-array v4, v3, [Ljava/lang/Object;

    const-string v5, "responseCode = "

    aput-object v5, v4, v1

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    aput-object v5, v4, v0

    invoke-static {v4}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 290
    new-array v4, v3, [Ljava/lang/Object;

    const-string v5, "purchaseData = "

    aput-object v5, v4, v1

    aput-object p3, v4, v0

    invoke-static {v4}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const/4 v4, -0x1

    if-ne p2, v4, :cond_3

    if-nez v2, :cond_3

    .line 294
    invoke-direct {p0, p3}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->processPurchaseSuccess(Ljava/lang/String;)V

    goto :goto_0

    :cond_3
    if-ne p2, v4, :cond_4

    .line 298
    invoke-virtual {p0, v2}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->processPurchaseFail(I)V

    goto :goto_0

    :cond_4
    if-nez p2, :cond_5

    .line 302
    new-array p2, v3, [Ljava/lang/Object;

    const-string p3, "Purchase canceled - Response: "

    aput-object p3, p2, v1

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p3

    aput-object p3, p2, v0

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 304
    new-instance p2, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const/16 p3, -0x3ed

    const-string v1, "User canceled."

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    .line 306
    iget-object p3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_6

    .line 307
    iget-object p3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_0

    .line 312
    :cond_5
    new-array p3, v3, [Ljava/lang/Object;

    const-string v2, "Purchase failed. Result code: "

    aput-object v2, p3, v1

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    aput-object p2, p3, v0

    invoke-static {p3}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 314
    new-instance p2, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const/16 p3, -0x3ee

    const-string v1, "Unknown purchase response."

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    .line 316
    iget-object p3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_6

    .line 317
    iget-object p3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_6
    :goto_0
    return v0
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 15
    .param p1    # Landroid/app/Activity;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p5    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    move-object v1, p0

    move-object/from16 v2, p5

    const-string v0, "NokiaStoreHelper.launchPurchaseFlow"

    .line 187
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    const-string v0, "subs"

    move-object/from16 v3, p3

    .line 189
    invoke-virtual {v3, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v3, 0x0

    if-eqz v0, :cond_1

    .line 191
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v4, -0x3f1

    const-string v5, "Subscriptions are not available."

    invoke-direct {v0, v4, v5}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    if-eqz v2, :cond_0

    .line 194
    invoke-interface {v2, v0, v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_0
    return-void

    :cond_1
    const/4 v4, 0x1

    const/4 v5, 0x2

    const/4 v6, 0x0

    .line 201
    :try_start_0
    iget-object v0, v1, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    if-nez v0, :cond_2

    if-eqz v2, :cond_4

    const-string v0, "Unable to buy item, Error response: service is not connected."

    .line 203
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 204
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const/4 v7, 0x6

    const-string v8, "Unable to buy item"

    invoke-direct {v0, v7, v8}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    .line 205
    invoke-interface {v2, v0, v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto/16 :goto_0

    .line 208
    :cond_2
    iget-object v9, v1, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mService:Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    const/4 v10, 0x3

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->getPackageName()Ljava/lang/String;

    move-result-object v11

    const-string v13, "inapp"

    move-object/from16 v12, p2

    move-object/from16 v14, p6

    invoke-interface/range {v9 .. v14}, Lcom/nokia/payment/iap/aidl/INokiaIAPService;->getBuyIntent(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0

    .line 212
    new-array v7, v5, [Ljava/lang/Object;

    const-string v8, "buyIntentBundle = "

    aput-object v8, v7, v6

    aput-object v0, v7, v4

    invoke-static {v7}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const-string v7, "RESPONSE_CODE"

    .line 214
    invoke-virtual {v0, v7, v6}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v7

    const-string v8, "BUY_INTENT"

    .line 215
    invoke-virtual {v0, v8}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v0

    check-cast v0, Landroid/app/PendingIntent;

    if-nez v7, :cond_3

    move/from16 v10, p4

    .line 218
    iput v10, v1, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mRequestCode:I

    .line 219
    iput-object v2, v1, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    .line 221
    invoke-virtual {v0}, Landroid/app/PendingIntent;->getIntentSender()Landroid/content/IntentSender;

    move-result-object v9

    .line 222
    new-instance v11, Landroid/content/Intent;

    invoke-direct {v11}, Landroid/content/Intent;-><init>()V

    const/4 v12, 0x0

    const/4 v13, 0x0

    const/4 v14, 0x0

    move-object/from16 v8, p1

    move/from16 v10, p4

    invoke-virtual/range {v8 .. v14}, Landroid/app/Activity;->startIntentSenderForResult(Landroid/content/IntentSender;ILandroid/content/Intent;III)V

    goto :goto_0

    :cond_3
    if-eqz v2, :cond_4

    .line 226
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v8, "Failed to get buy intent."

    invoke-direct {v0, v7, v8}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    .line 227
    invoke-interface {v2, v0, v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Landroid/content/IntentSender$SendIntentException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 239
    new-array v5, v5, [Ljava/lang/Object;

    const-string v7, "SendIntentException: "

    aput-object v7, v5, v6

    aput-object v0, v5, v4

    invoke-static {v0, v5}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    .line 241
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const/16 v4, -0x3e9

    const-string v5, "Remote exception while starting purchase flow"

    invoke-direct {v0, v4, v5}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    if-eqz v2, :cond_4

    .line 245
    invoke-interface {v2, v0, v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_0

    :catch_1
    move-exception v0

    .line 231
    new-array v5, v5, [Ljava/lang/Object;

    const-string v7, "RemoteException: "

    aput-object v7, v5, v6

    aput-object v0, v5, v4

    invoke-static {v0, v5}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    .line 233
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const/16 v4, -0x3ec

    const-string v5, "Failed to send intent."

    invoke-direct {v0, v4, v5}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    if-eqz v2, :cond_4

    .line 235
    invoke-interface {v2, v0, v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_4
    :goto_0
    return-void
.end method

.method public processPurchaseFail(I)V
    .locals 3

    const/4 v0, 0x2

    .line 331
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Result code was OK but in-app billing response was not OK: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x1

    aput-object v1, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 333
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz v0, :cond_0

    .line 334
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v1, "Problem purchashing item."

    invoke-direct {v0, p1, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    .line 335
    iget-object p1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    const/4 v1, 0x0

    invoke-interface {p1, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_0
    return-void
.end method

.method public queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 5
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

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    .line 449
    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {p3}, Lorg/onepf/oms/appstore/googleUtils/Inventory;-><init>()V

    const-string v0, "NokiaStoreHelper.queryInventory"

    .line 451
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    const/4 v0, 0x2

    .line 452
    new-array v1, v0, [Ljava/lang/Object;

    const-string v2, "querySkuDetails = "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    const/4 v4, 0x1

    aput-object v2, v1, v4

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 453
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "moreItemSkus = "

    aput-object v1, v0, v3

    aput-object p2, v0, v4

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-eqz p1, :cond_0

    .line 456
    invoke-direct {p0, p2, p3}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->refreshItemDetails(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V

    .line 459
    :cond_0
    invoke-direct {p0, p2, p3}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->refreshPurchasedItems(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V

    return-object p3
.end method

.method public startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 5
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    const-string v0, "NokiaStoreHelper.startSetup"

    .line 80
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 82
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;

    invoke-direct {v0, p0, p1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;-><init>(Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mServiceConn:Landroid/content/ServiceConnection;

    .line 132
    invoke-direct {p0}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->getServiceIntent()Landroid/content/Intent;

    move-result-object v0

    .line 133
    iget-object v1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mContext:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->queryIntentServices(Landroid/content/Intent;I)Ljava/util/List;

    move-result-object v1

    const/4 v2, 0x3

    if-eqz v1, :cond_1

    .line 135
    invoke-interface {v1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_0

    .line 144
    :cond_0
    :try_start_0
    iget-object v1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mContext:Landroid/content/Context;

    iget-object v3, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->mServiceConn:Landroid/content/ServiceConnection;

    const/4 v4, 0x1

    invoke-virtual {v1, v0, v3, v4}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z
    :try_end_0
    .catch Ljava/lang/SecurityException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    const-string v1, "Can\'t bind to the service"

    .line 146
    invoke-static {v1, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    if-eqz p1, :cond_2

    .line 148
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v1, "Billing service unavailable on device due to lack of the permission \"com.nokia.payment.BILLING\"."

    invoke-direct {v0, v2, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    goto :goto_1

    :cond_1
    :goto_0
    if-eqz p1, :cond_2

    .line 138
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v1, "Billing service unavailable on device."

    invoke-direct {v0, v2, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_2
    :goto_1
    return-void
.end method

.method public subscriptionsSupported()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method
