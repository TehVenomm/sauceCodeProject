.class public Ljp/colopl/drapro/InAppBillingHelper;
.super Ljava/lang/Object;
.source "InAppBillingHelper.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "InAppBillingHelper"

.field public static activity:Ljp/colopl/drapro/StartActivity;

.field static consumeList:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljp/colopl/iab/Purchase;",
            ">;"
        }
    .end annotation
.end field

.field private static handler:Landroid/os/Handler;

.field public static mHelper:Ljp/colopl/iab/IabHelper;

.field private static mPromotionPurchaseInfo:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljp/colopl/iab/Purchase;",
            ">;"
        }
    .end annotation
.end field

.field public static paymentReturnUrl:Ljava/lang/String;

.field private static productId:Ljava/lang/String;

.field public static productName:Ljava/lang/String;

.field static requestCount:I

.field public static userId:Ljava/lang/String;

.field public static userIdHash:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 432
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    sput-object v0, Ljp/colopl/drapro/InAppBillingHelper;->consumeList:Ljava/util/ArrayList;

    .line 433
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    sput-object v0, Ljp/colopl/drapro/InAppBillingHelper;->mPromotionPurchaseInfo:Ljava/util/ArrayList;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 35
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static ConsumePromotionItems()V
    .locals 3

    const-string v0, "InAppBillingHelper"

    .line 402
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "ConsumePromotionItems requestCount:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget v2, Ljp/colopl/drapro/InAppBillingHelper;->requestCount:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, " consumeList:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v2, Ljp/colopl/drapro/InAppBillingHelper;->consumeList:Ljava/util/ArrayList;

    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 403
    sget v0, Ljp/colopl/drapro/InAppBillingHelper;->requestCount:I

    if-nez v0, :cond_1

    .line 404
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->consumeList:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    if-lez v0, :cond_0

    .line 405
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->mHelper:Ljp/colopl/iab/IabHelper;

    sget-object v1, Ljp/colopl/drapro/InAppBillingHelper;->consumeList:Ljava/util/ArrayList;

    new-instance v2, Ljp/colopl/drapro/InAppBillingHelper$5;

    invoke-direct {v2}, Ljp/colopl/drapro/InAppBillingHelper$5;-><init>()V

    invoke-virtual {v0, v1, v2}, Ljp/colopl/iab/IabHelper;->consumeAsync(Ljava/util/List;Ljp/colopl/iab/IabHelper$OnConsumeMultiFinishedListener;)V

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 421
    invoke-static {v0}, Ljp/colopl/drapro/InAppBillingHelper;->FinishCheckPromotion(Z)V

    :cond_1
    :goto_0
    return-void
.end method

.method static FinishCheckPromotion(Z)V
    .locals 3

    const-string v0, "InAppBillingHelper"

    .line 427
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "FinishCheckPromotion success:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "ShopReceiver"

    const-string v1, "promoteItem"

    .line 428
    invoke-static {p0}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, v1, p0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method static synthetic access$000()Ljava/util/ArrayList;
    .locals 1

    .line 35
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->mPromotionPurchaseInfo:Ljava/util/ArrayList;

    return-object v0
.end method

.method public static checkAndGivePromotionitems(Ljava/lang/String;)V
    .locals 3

    const-string v0, "InAppBillingHelper"

    .line 312
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "checkAndGivePromotionitems "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "----"

    .line 313
    invoke-virtual {p0, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p0

    .line 314
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->mHelper:Ljp/colopl/iab/IabHelper;

    new-instance v1, Ljp/colopl/drapro/InAppBillingHelper$4;

    invoke-direct {v1, p0}, Ljp/colopl/drapro/InAppBillingHelper$4;-><init>([Ljava/lang/String;)V

    const/4 p0, 0x1

    invoke-virtual {v0, p0, v1}, Ljp/colopl/iab/IabHelper;->queryInventoryAsync(ZLjp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public static getProductDatas(Ljava/lang/String;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljp/colopl/iab/IabException;
        }
    .end annotation

    const-string v0, "----"

    .line 202
    invoke-virtual {p0, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p0

    .line 204
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->mHelper:Ljp/colopl/iab/IabHelper;

    invoke-virtual {v0}, Ljp/colopl/iab/IabHelper;->IsSetupDone()Z

    move-result v0

    if-nez v0, :cond_0

    const-string p0, "ShopReceiver"

    const-string v0, "getProductDatas"

    const-string v1, ""

    .line 205
    invoke-static {p0, v0, v1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 209
    :cond_0
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->mHelper:Ljp/colopl/iab/IabHelper;

    const/4 v1, 0x1

    new-instance v2, Ljp/colopl/drapro/InAppBillingHelper$2;

    invoke-direct {v2, p0}, Ljp/colopl/drapro/InAppBillingHelper$2;-><init>(Ljava/util/List;)V

    invoke-virtual {v0, v1, p0, v2}, Ljp/colopl/iab/IabHelper;->queryInventoryAsync(ZLjava/util/List;Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public static getProductId()Ljava/lang/String;
    .locals 1

    const-string v0, "productId"

    .line 108
    invoke-static {v0}, Ljp/colopl/drapro/NetworkHelper;->getSharedString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    sput-object v0, Ljp/colopl/drapro/InAppBillingHelper;->productId:Ljava/lang/String;

    .line 109
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->productId:Ljava/lang/String;

    return-object v0
.end method

.method public static getSkuDetails()V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljp/colopl/iab/IabException;
        }
    .end annotation

    .line 150
    sget-object v0, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    invoke-static {v0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    .line 152
    sget-object v1, Ljp/colopl/drapro/InAppBillingHelper;->mHelper:Ljp/colopl/iab/IabHelper;

    new-instance v2, Ljp/colopl/drapro/InAppBillingHelper$1;

    invoke-direct {v2}, Ljp/colopl/drapro/InAppBillingHelper$1;-><init>()V

    const/4 v3, 0x1

    invoke-virtual {v1, v3, v0, v2}, Ljp/colopl/iab/IabHelper;->queryInventoryAsync(ZLjava/util/List;Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public static init(Ljp/colopl/iab/IabHelper;Ljp/colopl/drapro/StartActivity;)V
    .locals 1

    .line 62
    sput-object p0, Ljp/colopl/drapro/InAppBillingHelper;->mHelper:Ljp/colopl/iab/IabHelper;

    .line 63
    sput-object p1, Ljp/colopl/drapro/InAppBillingHelper;->activity:Ljp/colopl/drapro/StartActivity;

    const-string p0, "InAppBillingHelper"

    .line 64
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "  "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->activity:Ljp/colopl/drapro/StartActivity;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static requestMarket(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 77
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->mHelper:Ljp/colopl/iab/IabHelper;

    invoke-virtual {v0}, Ljp/colopl/iab/IabHelper;->IsSetupDone()Z

    move-result v0

    if-nez v0, :cond_0

    const-string p0, "ShopReceiver"

    const-string p1, "buyItem"

    const/4 p2, 0x3

    .line 78
    invoke-static {p2}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object p2

    invoke-static {p0, p1, p2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 82
    :cond_0
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-static {p0, v0}, Ljp/colopl/drapro/AppConsts;->getProductNameById(Ljava/lang/String;Landroid/app/Activity;)Ljava/lang/String;

    move-result-object v0

    sput-object v0, Ljp/colopl/drapro/InAppBillingHelper;->productName:Ljava/lang/String;

    .line 83
    invoke-static {p0}, Ljp/colopl/drapro/InAppBillingHelper;->setProductId(Ljava/lang/String;)V

    .line 84
    sput-object p1, Ljp/colopl/drapro/InAppBillingHelper;->userId:Ljava/lang/String;

    .line 85
    sput-object p2, Ljp/colopl/drapro/InAppBillingHelper;->userIdHash:Ljava/lang/String;

    const-string p1, "InAppBillingHelper"

    .line 86
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "productName:"

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->productName:Ljava/lang/String;

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, ", productId: "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p1, p0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 88
    sget-object p0, Ljp/colopl/drapro/InAppBillingHelper;->activity:Ljp/colopl/drapro/StartActivity;

    sget-object p1, Ljp/colopl/drapro/InAppBillingHelper;->productId:Ljava/lang/String;

    invoke-virtual {p0, p1}, Ljp/colopl/drapro/StartActivity;->inappbillingStart(Ljava/lang/String;)V

    return-void
.end method

.method public static restorePurchasedItem(Z)V
    .locals 1

    .line 98
    sget-object v0, Ljp/colopl/drapro/InAppBillingHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0, p0}, Ljp/colopl/drapro/StartActivity;->restoreInventory(Z)V

    return-void
.end method

.method public static sendInventoryToUnity(Ljp/colopl/iab/Inventory;)V
    .locals 6

    .line 115
    new-instance v0, Lorg/json/JSONArray;

    invoke-direct {v0}, Lorg/json/JSONArray;-><init>()V

    const/4 v1, 0x0

    .line 116
    :goto_0
    sget-object v2, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    array-length v2, v2

    if-ge v1, v2, :cond_1

    .line 117
    sget-object v2, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    aget-object v2, v2, v1

    .line 118
    invoke-virtual {p0, v2}, Ljp/colopl/iab/Inventory;->getSkuDetails(Ljava/lang/String;)Ljp/colopl/iab/SkuDetails;

    move-result-object v2

    .line 119
    new-instance v3, Lorg/json/JSONObject;

    invoke-direct {v3}, Lorg/json/JSONObject;-><init>()V

    if-eqz v2, :cond_0

    :try_start_0
    const-string v4, "sku"

    .line 123
    invoke-virtual {v2}, Ljp/colopl/iab/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v4, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v4, "price"

    .line 124
    invoke-virtual {v2}, Ljp/colopl/iab/SkuDetails;->getPrice()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v4, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v4, "title"

    .line 125
    invoke-virtual {v2}, Ljp/colopl/iab/SkuDetails;->getTitle()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v4, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v4, "description"

    .line 126
    invoke-virtual {v2}, Ljp/colopl/iab/SkuDetails;->getDescription()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v3, v4, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v2

    .line 129
    invoke-virtual {v2}, Lorg/json/JSONException;->printStackTrace()V

    .line 131
    :cond_0
    :goto_1
    invoke-virtual {v0, v3}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 134
    :cond_1
    new-instance p0, Lorg/json/JSONObject;

    invoke-direct {p0}, Lorg/json/JSONObject;-><init>()V

    :try_start_1
    const-string v1, "ItemList"

    .line 136
    invoke-virtual {p0, v1, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_2

    :catch_1
    move-exception v0

    .line 139
    invoke-virtual {v0}, Lorg/json/JSONException;->printStackTrace()V

    .line 142
    :goto_2
    invoke-virtual {p0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p0

    const-string v0, "ShopReceiver"

    const-string v1, "ReceiveItemPriceData"

    .line 144
    invoke-static {v0, v1, p0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static setProductId(Ljava/lang/String;)V
    .locals 1

    .line 102
    sput-object p0, Ljp/colopl/drapro/InAppBillingHelper;->productId:Ljava/lang/String;

    const-string v0, "productId"

    .line 103
    invoke-static {v0, p0}, Ljp/colopl/drapro/NetworkHelper;->setSharedString(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static setProductIdData(Ljava/lang/String;)V
    .locals 1

    const-string v0, "----"

    .line 305
    invoke-virtual {p0, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p0

    sput-object p0, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    return-void
.end method

.method public static setProductNameData(Ljava/lang/String;)V
    .locals 1

    const-string v0, "----"

    .line 300
    invoke-virtual {p0, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p0

    sput-object p0, Ljp/colopl/drapro/AppConsts;->itemNameId:[Ljava/lang/String;

    return-void
.end method

.method public static trackPurtraceData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljp/colopl/iab/IabException;
        }
    .end annotation

    const-string v0, "InAppBillingHelper"

    const-string v1, "call Track"

    .line 260
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 261
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 262
    invoke-interface {v0, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 264
    sget-object v1, Ljp/colopl/drapro/InAppBillingHelper;->mHelper:Ljp/colopl/iab/IabHelper;

    new-instance v2, Ljp/colopl/drapro/InAppBillingHelper$3;

    invoke-direct {v2, p0, p1, p2}, Ljp/colopl/drapro/InAppBillingHelper$3;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    const/4 p0, 0x1

    invoke-virtual {v1, p0, v0, v2}, Ljp/colopl/iab/IabHelper;->queryInventoryAsync(ZLjava/util/List;Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method
