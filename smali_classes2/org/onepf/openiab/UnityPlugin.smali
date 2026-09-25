.class public Lorg/onepf/openiab/UnityPlugin;
.super Ljava/lang/Object;
.source "UnityPlugin.java"


# static fields
.field private static final BILLING_NOT_SUPPORTED_CALLBACK:Ljava/lang/String; = "OnBillingNotSupported"

.field private static final BILLING_SUPPORTED_CALLBACK:Ljava/lang/String; = "OnBillingSupported"

.field private static final CONSUME_PURCHASE_FAILED_CALLBACK:Ljava/lang/String; = "OnConsumePurchaseFailed"

.field private static final CONSUME_PURCHASE_SUCCEEDED_CALLBACK:Ljava/lang/String; = "OnConsumePurchaseSucceeded"

.field private static final EVENT_MANAGER:Ljava/lang/String; = "OpenIABEventManager"

.field private static final MAP_SKU_FAILED_CALLBACK:Ljava/lang/String; = "OnMapSkuFailed"

.field private static final PURCHASE_FAILED_CALLBACK:Ljava/lang/String; = "OnPurchaseFailed"

.field private static final PURCHASE_SUCCEEDED_CALLBACK:Ljava/lang/String; = "OnPurchaseSucceeded"

.field private static final QUERY_INVENTORY_FAILED_CALLBACK:Ljava/lang/String; = "OnQueryInventoryFailed"

.field private static final QUERY_INVENTORY_SUCCEEDED_CALLBACK:Ljava/lang/String; = "OnQueryInventorySucceeded"

.field public static final RC_REQUEST:I = 0x2711

.field public static final TAG:Ljava/lang/String; = "OpenIAB-UnityPlugin"

.field public static final YANDEX_STORE_ACTION_PURCHASE_STATE_CHANGED:Ljava/lang/String; = "com.yandex.store.service.PURCHASE_STATE_CHANGED"

.field public static final YANDEX_STORE_SERVICE:Ljava/lang/String; = "com.yandex.store.service"

.field private static _instance:Lorg/onepf/openiab/UnityPlugin; = null

.field public static sendRequest:Z = false


# instance fields
.field private _billingReceiver:Landroid/content/BroadcastReceiver;

.field _consumeFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;

.field private _helper:Lorg/onepf/oms/OpenIabHelper;

.field _purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

.field _queryInventoryListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 43
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 228
    new-instance v0, Lorg/onepf/openiab/UnityPlugin$6;

    invoke-direct {v0, p0}, Lorg/onepf/openiab/UnityPlugin$6;-><init>(Lorg/onepf/openiab/UnityPlugin;)V

    iput-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_queryInventoryListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;

    .line 251
    new-instance v0, Lorg/onepf/openiab/UnityPlugin$7;

    invoke-direct {v0, p0}, Lorg/onepf/openiab/UnityPlugin$7;-><init>(Lorg/onepf/openiab/UnityPlugin;)V

    iput-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    .line 275
    new-instance v0, Lorg/onepf/openiab/UnityPlugin$8;

    invoke-direct {v0, p0}, Lorg/onepf/openiab/UnityPlugin$8;-><init>(Lorg/onepf/openiab/UnityPlugin;)V

    iput-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_consumeFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;

    .line 383
    new-instance v0, Lorg/onepf/openiab/UnityPlugin$9;

    invoke-direct {v0, p0}, Lorg/onepf/openiab/UnityPlugin$9;-><init>(Lorg/onepf/openiab/UnityPlugin;)V

    iput-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_billingReceiver:Landroid/content/BroadcastReceiver;

    return-void
.end method

.method static synthetic access$000(Lorg/onepf/openiab/UnityPlugin;)Lorg/onepf/oms/OpenIabHelper;
    .locals 0

    .line 43
    iget-object p0, p0, Lorg/onepf/openiab/UnityPlugin;->_helper:Lorg/onepf/oms/OpenIabHelper;

    return-object p0
.end method

.method static synthetic access$002(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/OpenIabHelper;)Lorg/onepf/oms/OpenIabHelper;
    .locals 0

    .line 43
    iput-object p1, p0, Lorg/onepf/openiab/UnityPlugin;->_helper:Lorg/onepf/oms/OpenIabHelper;

    return-object p1
.end method

.method static synthetic access$100(Lorg/onepf/openiab/UnityPlugin;)V
    .locals 0

    .line 43
    invoke-direct {p0}, Lorg/onepf/openiab/UnityPlugin;->createBroadcasts()V

    return-void
.end method

.method static synthetic access$200(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/appstore/googleUtils/Inventory;)Ljava/lang/String;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 43
    invoke-direct {p0, p1}, Lorg/onepf/openiab/UnityPlugin;->inventoryToJson(Lorg/onepf/oms/appstore/googleUtils/Inventory;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$300(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/appstore/googleUtils/Purchase;)Ljava/lang/String;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 43
    invoke-direct {p0, p1}, Lorg/onepf/openiab/UnityPlugin;->purchaseToJson(Lorg/onepf/oms/appstore/googleUtils/Purchase;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private createBroadcasts()V
    .locals 3

    const-string v0, "OpenIAB-UnityPlugin"

    const-string v1, "createBroadcasts"

    .line 365
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 366
    new-instance v0, Landroid/content/IntentFilter;

    const-string v1, "com.yandex.store.service.PURCHASE_STATE_CHANGED"

    invoke-direct {v0, v1}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    .line 367
    sget-object v1, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    iget-object v2, p0, Lorg/onepf/openiab/UnityPlugin;->_billingReceiver:Landroid/content/BroadcastReceiver;

    invoke-virtual {v1, v2, v0}, Landroid/app/Activity;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    return-void
.end method

.method private destroyBroadcasts()V
    .locals 4

    const-string v0, "OpenIAB-UnityPlugin"

    const-string v1, "destroyBroadcasts"

    .line 371
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 373
    :try_start_0
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    iget-object v1, p0, Lorg/onepf/openiab/UnityPlugin;->_billingReceiver:Landroid/content/BroadcastReceiver;

    invoke-virtual {v0, v1}, Landroid/app/Activity;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "OpenIAB-UnityPlugin"

    .line 375
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "destroyBroadcasts exception:\n"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public static instance()Lorg/onepf/openiab/UnityPlugin;
    .locals 1

    .line 72
    sget-object v0, Lorg/onepf/openiab/UnityPlugin;->_instance:Lorg/onepf/openiab/UnityPlugin;

    if-nez v0, :cond_0

    .line 73
    new-instance v0, Lorg/onepf/openiab/UnityPlugin;

    invoke-direct {v0}, Lorg/onepf/openiab/UnityPlugin;-><init>()V

    sput-object v0, Lorg/onepf/openiab/UnityPlugin;->_instance:Lorg/onepf/openiab/UnityPlugin;

    .line 75
    :cond_0
    sget-object v0, Lorg/onepf/openiab/UnityPlugin;->_instance:Lorg/onepf/openiab/UnityPlugin;

    return-object v0
.end method

.method private inventoryToJson(Lorg/onepf/oms/appstore/googleUtils/Inventory;)Ljava/lang/String;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 300
    new-instance v0, Lorg/json/JSONStringer;

    invoke-direct {v0}, Lorg/json/JSONStringer;-><init>()V

    invoke-virtual {v0}, Lorg/json/JSONStringer;->object()Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "purchaseMap"

    .line 302
    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v1

    invoke-virtual {v1}, Lorg/json/JSONStringer;->array()Lorg/json/JSONStringer;

    .line 303
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getPurchaseMap()Ljava/util/Map;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 304
    invoke-virtual {v0}, Lorg/json/JSONStringer;->array()Lorg/json/JSONStringer;

    .line 305
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    invoke-virtual {v0, v3}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    .line 306
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    invoke-direct {p0, v2}, Lorg/onepf/openiab/UnityPlugin;->purchaseToJson(Lorg/onepf/oms/appstore/googleUtils/Purchase;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    .line 307
    invoke-virtual {v0}, Lorg/json/JSONStringer;->endArray()Lorg/json/JSONStringer;

    goto :goto_0

    .line 309
    :cond_0
    invoke-virtual {v0}, Lorg/json/JSONStringer;->endArray()Lorg/json/JSONStringer;

    const-string v1, "skuMap"

    .line 311
    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v1

    invoke-virtual {v1}, Lorg/json/JSONStringer;->array()Lorg/json/JSONStringer;

    .line 312
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getSkuMap()Ljava/util/Map;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 313
    invoke-virtual {v0}, Lorg/json/JSONStringer;->array()Lorg/json/JSONStringer;

    .line 314
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v0, v2}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    .line 315
    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-direct {p0, v1}, Lorg/onepf/openiab/UnityPlugin;->skuDetailsToJson(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    .line 316
    invoke-virtual {v0}, Lorg/json/JSONStringer;->endArray()Lorg/json/JSONStringer;

    goto :goto_1

    .line 318
    :cond_1
    invoke-virtual {v0}, Lorg/json/JSONStringer;->endArray()Lorg/json/JSONStringer;

    .line 320
    invoke-virtual {v0}, Lorg/json/JSONStringer;->endObject()Lorg/json/JSONStringer;

    .line 321
    invoke-virtual {v0}, Lorg/json/JSONStringer;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private purchaseToJson(Lorg/onepf/oms/appstore/googleUtils/Purchase;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 331
    new-instance v0, Lorg/json/JSONStringer;

    invoke-direct {v0}, Lorg/json/JSONStringer;-><init>()V

    invoke-virtual {v0}, Lorg/json/JSONStringer;->object()Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "itemType"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getItemType()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "orderId"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getOrderId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "packageName"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "sku"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "purchaseTime"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getPurchaseTime()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONStringer;->value(J)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "purchaseState"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getPurchaseState()I

    move-result v1

    int-to-long v1, v1

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONStringer;->value(J)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "developerPayload"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getDeveloperPayload()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "token"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getToken()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "originalJson"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getOriginalJson()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "signature"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSignature()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "appstoreName"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getAppstoreName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object p1

    invoke-virtual {p1}, Lorg/json/JSONStringer;->endObject()Lorg/json/JSONStringer;

    move-result-object p1

    invoke-virtual {p1}, Lorg/json/JSONStringer;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private skuDetailsToJson(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)Ljava/lang/String;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 353
    new-instance v0, Lorg/json/JSONStringer;

    invoke-direct {v0}, Lorg/json/JSONStringer;-><init>()V

    invoke-virtual {v0}, Lorg/json/JSONStringer;->object()Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "itemType"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getItemType()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "sku"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "type"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getType()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "price"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getPrice()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "title"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getTitle()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "description"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getDescription()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object v0

    const-string v1, "json"

    invoke-virtual {v0, v1}, Lorg/json/JSONStringer;->key(Ljava/lang/String;)Lorg/json/JSONStringer;

    move-result-object v0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getJson()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lorg/json/JSONStringer;->value(Ljava/lang/Object;)Lorg/json/JSONStringer;

    move-result-object p1

    invoke-virtual {p1}, Lorg/json/JSONStringer;->endObject()Lorg/json/JSONStringer;

    move-result-object p1

    invoke-virtual {p1}, Lorg/json/JSONStringer;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private startProxyPurchaseActivity(Ljava/lang/String;ZLjava/lang/String;)V
    .locals 3

    .line 210
    invoke-static {}, Lorg/onepf/openiab/UnityPlugin;->instance()Lorg/onepf/openiab/UnityPlugin;

    move-result-object v0

    invoke-virtual {v0}, Lorg/onepf/openiab/UnityPlugin;->getHelper()Lorg/onepf/oms/OpenIabHelper;

    move-result-object v0

    if-nez v0, :cond_0

    const-string p1, "OpenIAB-UnityPlugin"

    const-string p2, "OpenIAB UnityPlugin not initialized!"

    .line 211
    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    const/4 v0, 0x1

    .line 215
    sput-boolean v0, Lorg/onepf/openiab/UnityPlugin;->sendRequest:Z

    .line 216
    new-instance v0, Landroid/content/Intent;

    sget-object v1, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    const-class v2, Lorg/onepf/openiab/UnityProxyActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "sku"

    .line 217
    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "inapp"

    .line 218
    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    const-string p1, "developerPayload"

    .line 219
    invoke-virtual {v0, p1, p3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 222
    sget-object p1, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    invoke-virtual {p1, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method


# virtual methods
.method public areSubscriptionsSupported()Z
    .locals 1

    .line 140
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_helper:Lorg/onepf/oms/OpenIabHelper;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper;->subscriptionsSupported()Z

    move-result v0

    return v0
.end method

.method public consumeProduct(Ljava/lang/String;)V
    .locals 2

    .line 179
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    new-instance v1, Lorg/onepf/openiab/UnityPlugin$5;

    invoke-direct {v1, p0, p1}, Lorg/onepf/openiab/UnityPlugin$5;-><init>(Lorg/onepf/openiab/UnityPlugin;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public enableDebugLogging(Z)V
    .locals 0

    .line 408
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->setLoggable(Z)V

    return-void
.end method

.method public enableDebugLogging(ZLjava/lang/String;)V
    .locals 0

    .line 412
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->setLoggable(Z)V

    return-void
.end method

.method public getHelper()Lorg/onepf/oms/OpenIabHelper;
    .locals 1

    .line 64
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_helper:Lorg/onepf/oms/OpenIabHelper;

    return-object v0
.end method

.method public getPurchaseFinishedListener()Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;
    .locals 1

    .line 68
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    return-object v0
.end method

.method public init(Ljava/util/HashMap;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 95
    new-instance v0, Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    invoke-direct {v0}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;-><init>()V

    invoke-virtual {v0, p1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addStoreKeys(Ljava/util/Map;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p1

    invoke-virtual {p1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->build()Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object p1

    .line 98
    invoke-virtual {p0, p1}, Lorg/onepf/openiab/UnityPlugin;->initWithOptions(Lorg/onepf/oms/OpenIabHelper$Options;)V

    return-void
.end method

.method public initWithOptions(Lorg/onepf/oms/OpenIabHelper$Options;)V
    .locals 2

    .line 102
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    new-instance v1, Lorg/onepf/openiab/UnityPlugin$1;

    invoke-direct {v1, p0, p1}, Lorg/onepf/openiab/UnityPlugin$1;-><init>(Lorg/onepf/openiab/UnityPlugin;Lorg/onepf/oms/OpenIabHelper$Options;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public isDebugLog()Z
    .locals 1

    .line 404
    invoke-static {}, Lorg/onepf/oms/util/Logger;->isLoggable()Z

    move-result v0

    return v0
.end method

.method public mapSku(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 87
    :try_start_0
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    invoke-virtual {v0, p1, p2, p3}, Lorg/onepf/oms/SkuManager;->mapSku(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lorg/onepf/oms/SkuManager;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "OpenIAB-UnityPlugin"

    .line 89
    invoke-virtual {p1}, Ljava/lang/Exception;->toString()Ljava/lang/String;

    move-result-object p3

    invoke-static {p2, p3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const-string p2, "OpenIABEventManager"

    const-string p3, "OnMapSkuFailed"

    .line 90
    invoke-virtual {p1}, Ljava/lang/Exception;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p3, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public purchaseProduct(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x1

    .line 171
    invoke-direct {p0, p1, v0, p2}, Lorg/onepf/openiab/UnityPlugin;->startProxyPurchaseActivity(Ljava/lang/String;ZLjava/lang/String;)V

    return-void
.end method

.method public purchaseSubscription(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x0

    .line 175
    invoke-direct {p0, p1, v0, p2}, Lorg/onepf/openiab/UnityPlugin;->startProxyPurchaseActivity(Ljava/lang/String;ZLjava/lang/String;)V

    return-void
.end method

.method public queryInventory()V
    .locals 2

    .line 144
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    new-instance v1, Lorg/onepf/openiab/UnityPlugin$2;

    invoke-direct {v1, p0}, Lorg/onepf/openiab/UnityPlugin$2;-><init>(Lorg/onepf/openiab/UnityPlugin;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public queryInventory([Ljava/lang/String;)V
    .locals 2

    .line 153
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    new-instance v1, Lorg/onepf/openiab/UnityPlugin$3;

    invoke-direct {v1, p0, p1}, Lorg/onepf/openiab/UnityPlugin$3;-><init>(Lorg/onepf/openiab/UnityPlugin;[Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public queryInventory([Ljava/lang/String;[Ljava/lang/String;)V
    .locals 2

    .line 162
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    new-instance v1, Lorg/onepf/openiab/UnityPlugin$4;

    invoke-direct {v1, p0, p1, p2}, Lorg/onepf/openiab/UnityPlugin$4;-><init>(Lorg/onepf/openiab/UnityPlugin;[Ljava/lang/String;[Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public unbindService()V
    .locals 1

    .line 132
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_helper:Lorg/onepf/oms/OpenIabHelper;

    if-eqz v0, :cond_0

    .line 133
    iget-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_helper:Lorg/onepf/oms/OpenIabHelper;

    invoke-virtual {v0}, Lorg/onepf/oms/OpenIabHelper;->dispose()V

    const/4 v0, 0x0

    .line 134
    iput-object v0, p0, Lorg/onepf/openiab/UnityPlugin;->_helper:Lorg/onepf/oms/OpenIabHelper;

    .line 136
    :cond_0
    invoke-direct {p0}, Lorg/onepf/openiab/UnityPlugin;->destroyBroadcasts()V

    return-void
.end method
