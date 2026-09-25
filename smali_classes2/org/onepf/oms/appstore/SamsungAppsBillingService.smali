.class public Lorg/onepf/oms/appstore/SamsungAppsBillingService;
.super Ljava/lang/Object;
.source "SamsungAppsBillingService.java"

# interfaces
.implements Lorg/onepf/oms/AppstoreInAppBillingService;


# static fields
.field public static final ACCOUNT_ACTIVITY_NAME:Ljava/lang/String; = "com.sec.android.iap.activity.AccountActivity"

.field private static final CURRENT_MODE:I

.field public static final FLAG_INCLUDE_STOPPED_PACKAGES:I = 0x20

.field public static final IAP_ERROR_ALREADY_PURCHASED:I = -0x3eb

.field public static final IAP_ERROR_COMMON:I = -0x3ea

.field public static final IAP_ERROR_CONFIRM_INBOX:I = -0x3ee

.field public static final IAP_ERROR_INITIALIZATION:I = -0x3e8

.field public static final IAP_ERROR_NEED_APP_UPGRADE:I = -0x3e9

.field public static final IAP_ERROR_NONE:I = 0x0

.field public static final IAP_ERROR_PRODUCT_DOES_NOT_EXIST:I = -0x3ed

.field public static final IAP_ERROR_WHILE_RUNNING:I = -0x3ec

.field public static final IAP_MODE_COMMERCIAL:I = 0x0

.field public static final IAP_MODE_TEST_FAIL:I = -0x1

.field public static final IAP_MODE_TEST_SUCCESS:I = 0x1

.field public static final IAP_PAYMENT_IS_CANCELED:I = 0x1

.field public static final IAP_RESPONSE_RESULT_OK:I = 0x0

.field public static final IAP_RESPONSE_RESULT_UNAVAILABLE:I = 0x2

.field public static final IAP_SERVICE_NAME:Ljava/lang/String; = "com.sec.android.iap.service.iapService"

.field private static final ITEM_RESPONSE_COUNT:I = 0x64

.field public static final ITEM_TYPE_ALL:Ljava/lang/String; = "10"

.field public static final ITEM_TYPE_CONSUMABLE:Ljava/lang/String; = "00"

.field public static final ITEM_TYPE_NON_CONSUMABLE:Ljava/lang/String; = "01"

.field public static final ITEM_TYPE_SUBSCRIPTION:Ljava/lang/String; = "02"

.field public static final JSON_KEY_CURRENCY_UNIT:Ljava/lang/String; = "mCurrencyUnit"

.field public static final JSON_KEY_ITEM_DESC:Ljava/lang/String; = "mItemDesc"

.field public static final JSON_KEY_ITEM_DOWNLOAD_URL:Ljava/lang/String; = "mItemDownloadUrl"

.field public static final JSON_KEY_ITEM_ID:Ljava/lang/String; = "mItemId"

.field public static final JSON_KEY_ITEM_IMAGE_URL:Ljava/lang/String; = "mItemImageUrl"

.field public static final JSON_KEY_ITEM_NAME:Ljava/lang/String; = "mItemName"

.field public static final JSON_KEY_ITEM_PRICE:Ljava/lang/String; = "mItemPrice"

.field public static final JSON_KEY_ITEM_PRICE_STRING:Ljava/lang/String; = "mItemPriceString"

.field public static final JSON_KEY_PAYMENT_ID:Ljava/lang/String; = "mPaymentId"

.field public static final JSON_KEY_PURCHASE_DATE:Ljava/lang/String; = "mPurchaseDate"

.field public static final JSON_KEY_PURCHASE_ID:Ljava/lang/String; = "mPurchaseId"

.field public static final JSON_KEY_TYPE:Ljava/lang/String; = "mType"

.field public static final KEY_NAME_ERROR_STRING:Ljava/lang/String; = "ERROR_STRING"

.field public static final KEY_NAME_IAP_UPGRADE_URL:Ljava/lang/String; = "IAP_UPGRADE_URL"

.field public static final KEY_NAME_ITEM_GROUP_ID:Ljava/lang/String; = "ITEM_GROUP_ID"

.field public static final KEY_NAME_ITEM_ID:Ljava/lang/String; = "ITEM_ID"

.field public static final KEY_NAME_RESULT_LIST:Ljava/lang/String; = "RESULT_LIST"

.field public static final KEY_NAME_RESULT_OBJECT:Ljava/lang/String; = "RESULT_OBJECT"

.field public static final KEY_NAME_STATUS_CODE:Ljava/lang/String; = "STATUS_CODE"

.field public static final KEY_NAME_THIRD_PARTY_NAME:Ljava/lang/String; = "THIRD_PARTY_NAME"

.field public static final PAYMENT_ACTIVITY_NAME:Ljava/lang/String; = "com.sec.android.iap.activity.PaymentMethodListActivity"

.field public static final REQUEST_CODE_IS_ACCOUNT_CERTIFICATION:I = 0x383

.field public static final REQUEST_CODE_IS_IAP_PAYMENT:I = 0x1


# instance fields
.field private activity:Landroid/app/Activity;

.field private volatile isBound:Z

.field private mExtraData:Ljava/lang/String;

.field private mIapConnector:Lcom/sec/android/iap/IAPConnector;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private mItemGroupId:Ljava/lang/String;

.field private mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private mRequestCode:I

.field private options:Lorg/onepf/oms/OpenIabHelper$Options;

.field private purchasingItemType:Ljava/lang/String;

.field private serviceConnection:Landroid/content/ServiceConnection;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 68
    sget-boolean v0, Lorg/onepf/oms/appstore/SamsungApps;->isSamsungTestMode:Z

    sput v0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->CURRENT_MODE:I

    return-void
.end method

.method public constructor <init>(Landroid/app/Activity;Lorg/onepf/oms/OpenIabHelper$Options;)V
    .locals 1

    .line 153
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 143
    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    .line 147
    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    .line 154
    iput-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->activity:Landroid/app/Activity;

    .line 155
    iput-object p2, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    return-void
.end method

.method static synthetic access$000(Lorg/onepf/oms/appstore/SamsungAppsBillingService;)Lcom/sec/android/iap/IAPConnector;
    .locals 0

    .line 61
    iget-object p0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mIapConnector:Lcom/sec/android/iap/IAPConnector;

    return-object p0
.end method

.method static synthetic access$002(Lorg/onepf/oms/appstore/SamsungAppsBillingService;Lcom/sec/android/iap/IAPConnector;)Lcom/sec/android/iap/IAPConnector;
    .locals 0

    .line 61
    iput-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mIapConnector:Lcom/sec/android/iap/IAPConnector;

    return-object p1
.end method

.method static synthetic access$100(Lorg/onepf/oms/appstore/SamsungAppsBillingService;)V
    .locals 0

    .line 61
    invoke-direct {p0}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->initIap()V

    return-void
.end method

.method static synthetic access$200(Lorg/onepf/oms/appstore/SamsungAppsBillingService;)Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
    .locals 0

    .line 61
    iget-object p0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    return-object p0
.end method

.method private bindIapService()V
    .locals 4

    .line 366
    new-instance v0, Lorg/onepf/oms/appstore/SamsungAppsBillingService$1;

    invoke-direct {v0, p0}, Lorg/onepf/oms/appstore/SamsungAppsBillingService$1;-><init>(Lorg/onepf/oms/appstore/SamsungAppsBillingService;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->serviceConnection:Landroid/content/ServiceConnection;

    .line 382
    new-instance v0, Landroid/content/Intent;

    const-string v1, "com.sec.android.iap.service.iapService"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 383
    iget-object v1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->activity:Landroid/app/Activity;

    invoke-virtual {v1}, Landroid/app/Activity;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    iget-object v2, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->serviceConnection:Landroid/content/ServiceConnection;

    const/4 v3, 0x1

    invoke-virtual {v1, v0, v2, v3}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result v0

    iput-boolean v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->isBound:Z

    return-void
.end method

.method private getItemGroupId(Ljava/lang/String;)Ljava/lang/String;
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 354
    invoke-static {p1}, Lorg/onepf/oms/appstore/SamsungApps;->checkSku(Ljava/lang/String;)V

    const-string v0, "/"

    .line 355
    invoke-virtual {p1, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    const/4 v0, 0x0

    .line 356
    aget-object p1, p1, v0

    return-object p1
.end method

.method private getItemId(Ljava/lang/String;)Ljava/lang/String;
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 360
    invoke-static {p1}, Lorg/onepf/oms/appstore/SamsungApps;->checkSku(Ljava/lang/String;)V

    const-string v0, "/"

    .line 361
    invoke-virtual {p1, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    const/4 v0, 0x1

    .line 362
    aget-object p1, p1, v0

    return-object p1
.end method

.method private initIap()V
    .locals 8

    const-string v0, "Init IAP service failed"

    const/4 v1, 0x6

    .line 391
    :try_start_0
    iget-object v2, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mIapConnector:Lcom/sec/android/iap/IAPConnector;

    sget v3, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->CURRENT_MODE:I

    invoke-interface {v2, v3}, Lcom/sec/android/iap/IAPConnector;->init(I)Landroid/os/Bundle;

    move-result-object v2

    const/4 v3, 0x0

    if-eqz v2, :cond_1

    const-string v4, "STATUS_CODE"

    .line 393
    invoke-virtual {v2, v4}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v4

    const/4 v5, 0x2

    .line 394
    new-array v5, v5, [Ljava/lang/Object;

    const-string v6, "Init IAP connection status code: "

    aput-object v6, v5, v3

    const/4 v6, 0x1

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v5}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const-string v5, "ERROR_STRING"

    .line 395
    invoke-virtual {v2, v5}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    if-nez v4, :cond_0

    move-object v0, v2

    const/4 v1, 0x0

    goto :goto_0

    :cond_0
    move-object v0, v2

    goto :goto_0

    :catch_0
    move-exception v2

    const-string v3, "Init IAP: "

    .line 401
    invoke-static {v3, v2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 403
    :cond_1
    :goto_0
    iget-object v2, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance v3, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    invoke-direct {v3, v1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    return-void
.end method

.method private processItemsBundle(Landroid/os/Bundle;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/Inventory;ZZZLjava/util/Set;)Z
    .locals 17
    .param p1    # Landroid/os/Bundle;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Lorg/onepf/oms/appstore/googleUtils/Inventory;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p7    # Ljava/util/Set;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/os/Bundle;",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;",
            "ZZZ",
            "Ljava/util/Set<",
            "Ljava/lang/String;",
            ">;)Z"
        }
    .end annotation

    move-object/from16 v0, p1

    move-object/from16 v1, p2

    move-object/from16 v2, p3

    move-object/from16 v3, p7

    const/4 v4, 0x0

    if-eqz v0, :cond_9

    const-string v5, "STATUS_CODE"

    .line 407
    invoke-virtual {v0, v5}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v5

    if-eqz v5, :cond_0

    goto/16 :goto_6

    :cond_0
    const-string v5, "RESULT_LIST"

    .line 411
    invoke-virtual {v0, v5}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v5

    .line 412
    invoke-virtual {v5}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_0
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_7

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 414
    :try_start_0
    new-instance v7, Lorg/json/JSONObject;

    invoke-direct {v7, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v0, "mItemId"

    .line 415
    invoke-virtual {v7, v0}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v3, :cond_1

    .line 416
    invoke-interface {v3, v0}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_6

    :cond_1
    const-string v8, "mType"

    .line 417
    invoke-virtual {v7, v8}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    const-string v9, "00"

    .line 419
    invoke-virtual {v8, v9}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v9

    if-eqz v9, :cond_2

    if-nez p6, :cond_2

    goto :goto_0

    :cond_2
    const-string v10, "02"

    .line 422
    invoke-virtual {v8, v10}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_3

    const-string v8, "subs"

    goto :goto_1

    :cond_3
    const-string v8, "inapp"
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    :goto_1
    move-object v11, v8

    const/16 v8, 0x2f

    if-eqz p5, :cond_4

    .line 425
    :try_start_1
    new-instance v10, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    const-string v12, "com.samsung.apps"

    invoke-direct {v10, v12}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;)V

    .line 426
    invoke-virtual {v10, v11}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setItemType(Ljava/lang/String;)V

    .line 427
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v12

    const-string v13, "com.samsung.apps"

    new-instance v14, Ljava/lang/StringBuilder;

    invoke-direct {v14}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v14, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v14, v8}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v14, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v14}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v14

    invoke-virtual {v12, v13, v14}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v10, v12}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    move-object/from16 v14, p0

    .line 429
    :try_start_2
    iget-object v12, v14, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->activity:Landroid/app/Activity;

    invoke-virtual {v12}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v10, v12}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPackageName(Ljava/lang/String;)V

    .line 430
    invoke-virtual {v10, v4}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPurchaseState(I)V

    const-string v12, ""

    .line 431
    invoke-virtual {v10, v12}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setDeveloperPayload(Ljava/lang/String;)V

    const-string v12, "mPaymentId"

    .line 433
    invoke-virtual {v7, v12}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v10, v12}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setOrderId(Ljava/lang/String;)V

    const-string v12, "mPurchaseDate"

    .line 434
    invoke-virtual {v7, v12}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    invoke-static {v12}, Ljava/lang/Long;->parseLong(Ljava/lang/String;)J

    move-result-wide v12

    invoke-virtual {v10, v12, v13}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPurchaseTime(J)V

    const-string v12, "mPurchaseId"

    .line 435
    invoke-virtual {v7, v12}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v10, v12}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setToken(Ljava/lang/String;)V

    .line 437
    invoke-virtual {v2, v10}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addPurchase(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_2

    :catch_0
    move-exception v0

    move-object/from16 v14, p0

    goto :goto_3

    :cond_4
    move-object/from16 v14, p0

    :goto_2
    if-eqz p5, :cond_5

    if-eqz p4, :cond_6

    :cond_5
    const-string v10, "mItemName"

    .line 440
    invoke-virtual {v7, v10}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    const-string v10, "mItemPriceString"

    .line 441
    invoke-virtual {v7, v10}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v15

    const-string v10, "mItemDesc"

    .line 442
    invoke-virtual {v7, v10}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 443
    new-instance v12, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v10

    const-string v4, "com.samsung.apps"

    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v8, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v1, 0x2f

    invoke-virtual {v8, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v10, v4, v0}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    move-object v10, v12

    move-object v1, v12

    move-object v12, v0

    move-object v14, v15

    move-object v15, v7

    invoke-direct/range {v10 .. v15}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v2, v1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_4

    :catch_1
    move-exception v0

    :goto_3
    const-string v1, "JSON parse error"

    .line 449
    invoke-static {v1, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_6
    :goto_4
    move-object/from16 v1, p2

    const/4 v4, 0x0

    goto/16 :goto_0

    .line 452
    :cond_7
    invoke-virtual {v5}, Ljava/util/ArrayList;->size()I

    move-result v0

    const/16 v1, 0x64

    if-ne v0, v1, :cond_8

    const/4 v4, 0x1

    const/16 v16, 0x1

    goto :goto_5

    :cond_8
    const/16 v16, 0x0

    :goto_5
    return v16

    :cond_9
    :goto_6
    const/4 v1, 0x0

    return v1
.end method


# virtual methods
.method public consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    return-void
.end method

.method public dispose()V
    .locals 2

    .line 345
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->serviceConnection:Landroid/content/ServiceConnection;

    if-eqz v0, :cond_0

    iget-boolean v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->isBound:Z

    if-eqz v0, :cond_0

    .line 346
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->activity:Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    iget-object v1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->serviceConnection:Landroid/content/ServiceConnection;

    invoke-virtual {v0, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    const/4 v0, 0x0

    .line 347
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->isBound:Z

    :cond_0
    const/4 v0, 0x0

    .line 349
    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->serviceConnection:Landroid/content/ServiceConnection;

    .line 350
    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mIapConnector:Lcom/sec/android/iap/IAPConnector;

    return-void
.end method

.method public handleActivityResult(IILandroid/content/Intent;)Z
    .locals 9
    .param p3    # Landroid/content/Intent;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 266
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper$Options;->getSamsungCertificationRequestCode()I

    move-result v0

    const/4 v1, 0x6

    const/4 v2, 0x1

    if-ne p1, v0, :cond_2

    const/4 p1, -0x1

    if-ne p2, p1, :cond_0

    .line 268
    invoke-direct {p0}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->bindIapService()V

    goto :goto_0

    :cond_0
    if-nez p2, :cond_1

    .line 270
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string p3, "Account certification canceled"

    invoke-direct {p2, v2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    goto :goto_0

    .line 273
    :cond_1
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Unknown error. Result code: "

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p3, v1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, p3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :goto_0
    return v2

    .line 278
    :cond_2
    iget v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mRequestCode:I

    const/4 v3, 0x0

    if-eq p1, v0, :cond_3

    return v3

    :cond_3
    const-string p1, "Unknown error"

    .line 283
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    const-string v4, "com.samsung.apps"

    invoke-direct {v0, v4}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;)V

    const/4 v4, 0x4

    if-eqz p3, :cond_7

    .line 285
    invoke-virtual {p3}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p3

    if-eqz p3, :cond_7

    const-string p1, "STATUS_CODE"

    .line 287
    invoke-virtual {p3, p1}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result p1

    const-string v5, "ERROR_STRING"

    .line 288
    invoke-virtual {p3, v5}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "ITEM_ID"

    .line 289
    invoke-virtual {p3, v6}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    packed-switch p2, :pswitch_data_0

    goto :goto_1

    :pswitch_0
    const/4 v1, 0x1

    goto :goto_1

    :pswitch_1
    const/16 p2, -0x3ed

    if-eq p1, p2, :cond_6

    const/16 p2, -0x3eb

    if-eq p1, p2, :cond_5

    if-eqz p1, :cond_4

    goto :goto_1

    :cond_4
    const/4 v1, 0x0

    goto :goto_1

    :cond_5
    const/4 v1, 0x7

    goto :goto_1

    :cond_6
    const/4 v1, 0x4

    :goto_1
    const-string p1, "RESULT_OBJECT"

    .line 308
    invoke-virtual {p3, p1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 310
    :try_start_0
    new-instance p2, Lorg/json/JSONObject;

    invoke-direct {p2, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 312
    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setOriginalJson(Ljava/lang/String;)V

    const-string p1, "mPaymentId"

    .line 313
    invoke-virtual {p2, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setOrderId(Ljava/lang/String;)V

    const-string p1, "mPurchaseDate"

    .line 314
    invoke-virtual {p2, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Ljava/lang/Long;->parseLong(Ljava/lang/String;)J

    move-result-wide v7

    invoke-virtual {v0, v7, v8}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPurchaseTime(J)V

    const-string p1, "mPurchaseId"

    .line 315
    invoke-virtual {p2, p1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setToken(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-exception p1

    const-string p2, "JSON parse error: "

    .line 317
    invoke-static {p2, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 320
    :goto_2
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->purchasingItemType:Ljava/lang/String;

    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setItemType(Ljava/lang/String;)V

    .line 321
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object p1

    const-string p2, "com.samsung.apps"

    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v7, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mItemGroupId:Ljava/lang/String;

    invoke-virtual {p3, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v7, 0x2f

    invoke-virtual {p3, v7}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {p3, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p1, p2, p3}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 322
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->activity:Landroid/app/Activity;

    invoke-virtual {p1}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPackageName(Ljava/lang/String;)V

    .line 323
    invoke-virtual {v0, v3}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPurchaseState(I)V

    .line 324
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mExtraData:Ljava/lang/String;

    invoke-virtual {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setDeveloperPayload(Ljava/lang/String;)V

    move-object p1, v5

    .line 327
    :cond_7
    new-array p2, v4, [Ljava/lang/Object;

    const-string p3, "Samsung result code: "

    aput-object p3, p2, v3

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p3

    aput-object p3, p2, v2

    const/4 p3, 0x2

    const-string v3, ", msg: "

    aput-object v3, p2, p3

    const/4 p3, 0x3

    aput-object p1, p2, p3

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 328
    iget-object p2, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    invoke-direct {p3, v1, p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p2, p3, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    return v2

    :pswitch_data_0
    .packed-switch -0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 7
    .param p1    # Landroid/app/Activity;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 242
    invoke-direct {p0, p2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->getItemGroupId(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 243
    invoke-direct {p0, p2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->getItemId(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    .line 245
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "THIRD_PARTY_NAME"

    .line 246
    invoke-virtual {p1}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "ITEM_GROUP_ID"

    .line 247
    invoke-virtual {v1, v2, v0}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "ITEM_ID"

    .line 248
    invoke-virtual {v1, v2, p2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v2, 0x4

    .line 249
    new-array v2, v2, [Ljava/lang/Object;

    const-string v3, "launchPurchase: itemGroupId = "

    const/4 v4, 0x0

    aput-object v3, v2, v4

    const/4 v3, 0x1

    aput-object v0, v2, v3

    const-string v5, ", itemId = "

    const/4 v6, 0x2

    aput-object v5, v2, v6

    const/4 v5, 0x3

    aput-object p2, v2, v5

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 250
    new-instance p2, Landroid/content/ComponentName;

    const-string v2, "com.sec.android.iap"

    const-string v5, "com.sec.android.iap.activity.PaymentMethodListActivity"

    invoke-direct {p2, v2, v5}, Landroid/content/ComponentName;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 251
    new-instance v2, Landroid/content/Intent;

    const-string v5, "android.intent.action.MAIN"

    invoke-direct {v2, v5}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v5, "android.intent.category.LAUNCHER"

    .line 252
    invoke-virtual {v2, v5}, Landroid/content/Intent;->addCategory(Ljava/lang/String;)Landroid/content/Intent;

    .line 253
    invoke-virtual {v2, p2}, Landroid/content/Intent;->setComponent(Landroid/content/ComponentName;)Landroid/content/Intent;

    .line 254
    invoke-virtual {v2, v1}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    .line 255
    iput p4, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mRequestCode:I

    .line 256
    iput-object p5, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    .line 257
    iput-object p3, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->purchasingItemType:Ljava/lang/String;

    .line 258
    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mItemGroupId:Ljava/lang/String;

    .line 259
    iput-object p6, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mExtraData:Ljava/lang/String;

    .line 260
    new-array p2, v6, [Ljava/lang/Object;

    const-string p3, "Request code: "

    aput-object p3, p2, v4

    invoke-static {p4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p3

    aput-object p3, p2, v3

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 261
    invoke-virtual {p1, v2, p4}, Landroid/app/Activity;->startActivityForResult(Landroid/content/Intent;I)V

    return-void
.end method

.method public queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 23
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

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    move-object/from16 v9, p0

    .line 170
    new-instance v10, Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {v10}, Lorg/onepf/oms/appstore/googleUtils/Inventory;-><init>()V

    .line 172
    new-instance v0, Ljava/util/Date;

    invoke-direct {v0}, Ljava/util/Date;-><init>()V

    .line 173
    new-instance v1, Ljava/text/SimpleDateFormat;

    const-string v2, "yyyyMMdd"

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v3

    invoke-direct {v1, v2, v3}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 174
    invoke-virtual {v1, v0}, Ljava/text/SimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v18

    .line 177
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    .line 178
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v1

    const-string v2, "com.samsung.apps"

    invoke-virtual {v1, v2}, Lorg/onepf/oms/SkuManager;->getAllStoreSkus(Ljava/lang/String;)Ljava/util/List;

    move-result-object v1

    .line 179
    invoke-static {v1}, Lorg/onepf/oms/util/CollectionUtils;->isEmpty(Ljava/util/Collection;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 180
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 181
    invoke-direct {v9, v2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->getItemGroupId(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 186
    :cond_0
    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v19

    :goto_1
    invoke-interface/range {v19 .. v19}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    const/16 v20, 0x0

    const/16 v21, 0x1

    const/16 v11, 0x64

    if-eqz v0, :cond_2

    invoke-interface/range {v19 .. v19}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    move-object/from16 v22, v0

    check-cast v22, Ljava/lang/String;

    const/4 v1, 0x1

    const/16 v2, 0x64

    :goto_2
    const/4 v0, 0x4

    .line 193
    :try_start_0
    new-array v0, v0, [Ljava/lang/Object;

    const/4 v3, 0x0

    const-string v4, "getItemsInbox, startNum = "

    aput-object v4, v0, v3

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    aput-object v3, v0, v21

    const/4 v3, 0x2

    const-string v4, ", endNum = "

    aput-object v4, v0, v3

    const/4 v3, 0x3

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v0, v3

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 194
    iget-object v11, v9, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mIapConnector:Lcom/sec/android/iap/IAPConnector;

    iget-object v0, v9, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->activity:Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v12

    const-string v16, "19700101"

    move-object/from16 v13, v22

    move v14, v1

    move v15, v2

    move-object/from16 v17, v18

    invoke-interface/range {v11 .. v17}, Lcom/sec/android/iap/IAPConnector;->getItemsInbox(Ljava/lang/String;Ljava/lang/String;IILjava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_3

    :catch_0
    move-exception v0

    const-string v3, "Samsung getItemsInbox: "

    .line 196
    invoke-static {v3, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object/from16 v0, v20

    :goto_3
    add-int/lit8 v11, v1, 0x64

    add-int/lit8 v12, v2, 0x64

    const/4 v6, 0x1

    const/4 v7, 0x0

    const/4 v8, 0x0

    move-object/from16 v1, p0

    move-object v2, v0

    move-object/from16 v3, v22

    move-object v4, v10

    move/from16 v5, p1

    .line 201
    invoke-direct/range {v1 .. v8}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->processItemsBundle(Landroid/os/Bundle;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/Inventory;ZZZLjava/util/Set;)Z

    move-result v0

    if-nez v0, :cond_1

    goto :goto_1

    :cond_1
    move v1, v11

    move v2, v12

    goto :goto_2

    :cond_2
    if-eqz p1, :cond_6

    .line 204
    new-instance v0, Ljava/util/HashSet;

    invoke-direct {v0}, Ljava/util/HashSet;-><init>()V

    .line 205
    new-instance v13, Ljava/util/HashSet;

    invoke-direct {v13}, Ljava/util/HashSet;-><init>()V

    if-eqz p2, :cond_3

    .line 207
    invoke-interface/range {p2 .. p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_4
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_3

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 208
    invoke-direct {v9, v2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->getItemGroupId(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v0, v3}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 209
    invoke-direct {v9, v2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->getItemId(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v13, v2}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_4

    :cond_3
    if-eqz p3, :cond_4

    .line 213
    invoke-interface/range {p3 .. p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_5
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_4

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 214
    invoke-direct {v9, v2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->getItemGroupId(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v0, v3}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    .line 215
    invoke-direct {v9, v2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->getItemId(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v13, v2}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_5

    .line 218
    :cond_4
    invoke-interface {v13}, Ljava/util/Set;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_6

    .line 219
    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v14

    :goto_6
    invoke-interface {v14}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_6

    invoke-interface {v14}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    move-object v15, v0

    check-cast v15, Ljava/lang/String;

    const/4 v8, 0x1

    const/16 v16, 0x64

    .line 226
    :goto_7
    :try_start_1
    iget-object v1, v9, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->mIapConnector:Lcom/sec/android/iap/IAPConnector;

    sget v2, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->CURRENT_MODE:I

    iget-object v0, v9, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->activity:Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    const-string v7, "10"

    move-object v4, v15

    move v5, v8

    move/from16 v6, v16

    invoke-interface/range {v1 .. v7}, Lcom/sec/android/iap/IAPConnector;->getItemList(ILjava/lang/String;Ljava/lang/String;IILjava/lang/String;)Landroid/os/Bundle;

    move-result-object v0
    :try_end_1
    .catch Landroid/os/RemoteException; {:try_start_1 .. :try_end_1} :catch_1

    move-object v2, v0

    goto :goto_8

    :catch_1
    move-exception v0

    const-string v1, "Samsung getItemList: "

    .line 228
    invoke-static {v1, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object/from16 v2, v20

    :goto_8
    add-int/lit8 v0, v8, 0x64

    add-int/lit8 v16, v16, 0x64

    const/4 v6, 0x0

    const/4 v7, 0x1

    move-object/from16 v1, p0

    move-object v3, v15

    move-object v4, v10

    move/from16 v5, p1

    move-object v8, v13

    .line 233
    invoke-direct/range {v1 .. v8}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->processItemsBundle(Landroid/os/Bundle;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/Inventory;ZZZLjava/util/Set;)Z

    move-result v1

    if-nez v1, :cond_5

    goto :goto_6

    :cond_5
    move v8, v0

    goto :goto_7

    :cond_6
    return-object v10
.end method

.method public startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 2

    .line 160
    iput-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->setupListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    .line 162
    new-instance p1, Landroid/content/ComponentName;

    const-string v0, "com.sec.android.iap"

    const-string v1, "com.sec.android.iap.activity.AccountActivity"

    invoke-direct {p1, v0, v1}, Landroid/content/ComponentName;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 163
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    .line 164
    invoke-virtual {v0, p1}, Landroid/content/Intent;->setComponent(Landroid/content/ComponentName;)Landroid/content/Intent;

    .line 165
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->activity:Landroid/app/Activity;

    iget-object v1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options;->getSamsungCertificationRequestCode()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/app/Activity;->startActivityForResult(Landroid/content/Intent;I)V

    return-void
.end method

.method public subscriptionsSupported()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
