.class public Lorg/onepf/oms/appstore/FortumoBillingService;
.super Ljava/lang/Object;
.source "FortumoBillingService.java"

# interfaces
.implements Lorg/onepf/oms/AppstoreInAppBillingService;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser;,
        Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;
    }
.end annotation


# static fields
.field private static final SHARED_PREFS_FORTUMO:Ljava/lang/String; = "onepf_shared_prefs_fortumo"


# instance fields
.field private activityRequestCode:I

.field private context:Landroid/content/Context;

.field private developerPayload:Ljava/lang/String;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private inappsMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;",
            ">;"
        }
    .end annotation
.end field

.field private isNook:Z

.field private purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;Z)V
    .locals 0

    .line 73
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 74
    iput-object p1, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    .line 75
    iput-boolean p2, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->isNook:Z

    return-void
.end method

.method static addPendingPayment(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0
    .param p0    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 372
    invoke-static {p0}, Lorg/onepf/oms/appstore/FortumoBillingService;->getFortumoSharedPrefs(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 373
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 374
    invoke-interface {p0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 375
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    const/4 p0, 0x2

    .line 376
    new-array p0, p0, [Ljava/lang/Object;

    const/4 p2, 0x0

    aput-object p1, p0, p2

    const-string p1, " was added to pending"

    const/4 p2, 0x1

    aput-object p1, p0, p2

    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-void
.end method

.method static getFortumoInapps(Landroid/content/Context;Z)Ljava/util/Map;
    .locals 14
    .param p0    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Z)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lorg/xmlpull/v1/XmlPullParserException;,
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 274
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 275
    new-instance v1, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;

    invoke-direct {v1}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;-><init>()V

    .line 276
    invoke-virtual {v1, p0}, Lorg/onepf/oms/appstore/fortumoUtils/InappsXMLParser;->parse(Landroid/content/Context;)Landroid/util/Pair;

    move-result-object v1

    .line 277
    iget-object v1, v1, Landroid/util/Pair;->first:Ljava/lang/Object;

    check-cast v1, Ljava/util/List;

    .line 278
    invoke-static {p0, p1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser;->parse(Landroid/content/Context;Z)Ljava/util/Map;

    move-result-object v2

    .line 280
    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    const/4 v4, 0x0

    const/4 v5, 0x0

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    const/16 v7, -0x3e8

    if-eqz v6, :cond_7

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;

    .line 281
    invoke-virtual {v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->getProductId()Ljava/lang/String;

    move-result-object v8

    .line 282
    invoke-interface {v2, v8}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;

    if-eqz v9, :cond_6

    if-eqz p1, :cond_0

    .line 286
    invoke-virtual {v9}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getNookServiceId()Ljava/lang/String;

    move-result-object v7

    goto :goto_1

    :cond_0
    invoke-virtual {v9}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getServiceId()Ljava/lang/String;

    move-result-object v7

    :goto_1
    if-eqz p1, :cond_1

    .line 287
    invoke-virtual {v9}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getNookInAppSecret()Ljava/lang/String;

    move-result-object v10

    goto :goto_2

    :cond_1
    invoke-virtual {v9}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;->getServiceInAppSecret()Ljava/lang/String;

    move-result-object v10

    .line 288
    :goto_2
    invoke-static {p0, v7, v10}, Lmp/MpUtils;->getFetchedPriceData(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Ljava/util/List;

    move-result-object v11

    const/4 v12, 0x0

    if-eqz v11, :cond_2

    .line 290
    invoke-interface {v11}, Ljava/util/List;->size()I

    move-result v13

    if-nez v13, :cond_3

    .line 291
    :cond_2
    invoke-static {p0, v7, v10}, Lmp/MpUtils;->isSupportedOperator(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Z

    move-result v13

    if-eqz v13, :cond_3

    .line 293
    invoke-static {p0, v7, v10}, Lmp/MpUtils;->getFetchedPriceData(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Ljava/util/List;

    move-result-object v11

    :cond_3
    if-eqz v11, :cond_4

    .line 296
    invoke-interface {v11}, Ljava/util/List;->isEmpty()Z

    move-result v7

    if-nez v7, :cond_4

    .line 297
    invoke-interface {v11, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v7

    move-object v12, v7

    check-cast v12, Ljava/lang/String;

    .line 299
    :cond_4
    invoke-static {v12}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v7

    if-eqz v7, :cond_5

    .line 300
    invoke-virtual {v6}, Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;->getPriceDetails()Ljava/lang/String;

    move-result-object v12

    .line 301
    invoke-static {v12}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v7

    if-eqz v7, :cond_5

    const/4 v6, 0x2

    .line 302
    new-array v6, v6, [Ljava/lang/Object;

    aput-object v8, v6, v4

    const-string v7, " not available for this carrier and the price is not specified in the inapps_products.xml"

    const/4 v8, 0x1

    aput-object v7, v6, v8

    invoke-static {v6}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    add-int/lit8 v5, v5, 0x1

    goto :goto_0

    .line 308
    :cond_5
    new-instance v7, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;

    invoke-direct {v7, v6, v9, v12}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;-><init>(Lorg/onepf/oms/appstore/fortumoUtils/InappBaseProduct;Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProductParser$FortumoDetails;Ljava/lang/String;)V

    .line 309
    invoke-interface {v0, v8, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 284
    :cond_6
    new-instance p0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p1, "Fortumo inapp product details were not found"

    invoke-direct {p0, v7, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p0

    .line 312
    :cond_7
    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result p0

    if-eq v5, p0, :cond_8

    return-object v0

    .line 313
    :cond_8
    new-instance p0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p1, "No inventory available for this carrier/country."

    invoke-direct {p0, v7, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p0
.end method

.method static getFortumoSharedPrefs(Landroid/content/Context;)Landroid/content/SharedPreferences;
    .locals 2
    .param p0    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "onepf_shared_prefs_fortumo"

    const/4 v1, 0x0

    .line 394
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    return-object p0
.end method

.method static getMessageIdInPending(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;
    .locals 1
    .param p0    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 380
    invoke-static {p0}, Lorg/onepf/oms/appstore/FortumoBillingService;->getFortumoSharedPrefs(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object p0

    const/4 v0, 0x0

    .line 381
    invoke-interface {p0, p1, v0}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private getSkuPrice(Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;)Ljava/lang/String;
    .locals 3
    .param p1    # Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    .line 219
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getFortumoPrice()Ljava/lang/String;

    move-result-object v0

    .line 220
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_2

    .line 221
    iget-boolean v1, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->isNook:Z

    if-eqz v1, :cond_0

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getNookServiceId()Ljava/lang/String;

    move-result-object v1

    goto :goto_0

    :cond_0
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getServiceId()Ljava/lang/String;

    move-result-object v1

    .line 222
    :goto_0
    iget-boolean v2, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->isNook:Z

    if-eqz v2, :cond_1

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getNookInAppSecret()Ljava/lang/String;

    move-result-object p1

    goto :goto_1

    :cond_1
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getInAppSecret()Ljava/lang/String;

    move-result-object p1

    .line 223
    :goto_1
    iget-object v2, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {v2, v1, p1}, Lmp/MpUtils;->fetchPaymentData(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 225
    iget-object v2, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {v2, v1, p1}, Lmp/MpUtils;->getFetchedPriceData(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Ljava/util/List;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 226
    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_2

    const/4 v0, 0x0

    .line 227
    invoke-interface {p1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    move-object v0, p1

    check-cast v0, Ljava/lang/String;

    :cond_2
    return-object v0
.end method

.method private static purchaseFromPaymentResponse(Landroid/content/Context;Lmp/PaymentResponse;)Lorg/onepf/oms/appstore/googleUtils/Purchase;
    .locals 2
    .param p0    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p1    # Lmp/PaymentResponse;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 260
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    const-string v1, "com.fortumo.billing"

    invoke-direct {v0, v1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;)V

    .line 261
    invoke-virtual {p1}, Lmp/PaymentResponse;->getProductName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 262
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPackageName(Ljava/lang/String;)V

    .line 263
    invoke-virtual {p1}, Lmp/PaymentResponse;->getPaymentCode()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setOrderId(Ljava/lang/String;)V

    .line 264
    invoke-virtual {p1}, Lmp/PaymentResponse;->getDate()Ljava/util/Date;

    move-result-object p0

    if-eqz p0, :cond_0

    .line 266
    invoke-virtual {p0}, Ljava/util/Date;->getTime()J

    move-result-wide p0

    invoke-virtual {v0, p0, p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setPurchaseTime(J)V

    :cond_0
    const-string p0, "inapp"

    .line 268
    invoke-virtual {v0, p0}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setItemType(Ljava/lang/String;)V

    return-object v0
.end method

.method static removePendingProduct(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1
    .param p0    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 385
    invoke-static {p0}, Lorg/onepf/oms/appstore/FortumoBillingService;->getFortumoSharedPrefs(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 386
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 387
    invoke-interface {p0, p1}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 388
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    const/4 p0, 0x2

    .line 389
    new-array p0, p0, [Ljava/lang/Object;

    const/4 v0, 0x0

    aput-object p1, p0, v0

    const-string p1, " was removed from pending"

    const/4 v0, 0x1

    aput-object p1, p0, v0

    invoke-static {p0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-void
.end method


# virtual methods
.method public consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 1
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/Purchase;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    .line 235
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lorg/onepf/oms/appstore/FortumoBillingService;->removePendingProduct(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method public dispose()V
    .locals 1

    const/4 v0, 0x0

    .line 245
    iput-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    return-void
.end method

.method public handleActivityResult(IILandroid/content/Intent;)Z
    .locals 8
    .param p3    # Landroid/content/Intent;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 127
    iget v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->activityRequestCode:I

    const/4 v1, 0x0

    if-eq v0, p1, :cond_0

    return v1

    :cond_0
    const/4 p1, 0x1

    const/4 v0, 0x0

    if-nez p3, :cond_1

    const-string p2, "handleActivityResult: null intent data"

    .line 129
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 130
    iget-object p2, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v1, -0x3ea

    const-string v2, "Null data in Fortumo IAB result"

    invoke-direct {p3, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p2, p3, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_1

    :cond_1
    const-string v2, "Purchase error."

    const/4 v3, -0x1

    const/4 v4, 0x6

    const/4 v5, 0x2

    if-ne p2, v3, :cond_3

    .line 136
    new-instance p2, Lmp/PaymentResponse;

    invoke-direct {p2, p3}, Lmp/PaymentResponse;-><init>(Landroid/content/Intent;)V

    .line 137
    iget-object p3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {p3, p2}, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFromPaymentResponse(Landroid/content/Context;Lmp/PaymentResponse;)Lorg/onepf/oms/appstore/googleUtils/Purchase;

    move-result-object p3

    .line 138
    iget-object v3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->developerPayload:Ljava/lang/String;

    invoke-virtual {p3, v3}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setDeveloperPayload(Ljava/lang/String;)V

    .line 139
    invoke-virtual {p2}, Lmp/PaymentResponse;->getBillingStatus()I

    move-result v3

    if-ne v3, v5, :cond_2

    const/4 v4, 0x0

    goto :goto_0

    .line 141
    :cond_2
    invoke-virtual {p2}, Lmp/PaymentResponse;->getBillingStatus()I

    move-result v3

    if-ne v3, p1, :cond_4

    .line 142
    new-array v2, v5, [Ljava/lang/Object;

    const-string v3, "handleActivityResult: status pending for "

    aput-object v3, v2, v1

    invoke-virtual {p2}, Lmp/PaymentResponse;->getProductName()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v2, p1

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const-string v2, "Purchase is pending"

    .line 145
    iget-object v3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->inappsMap:Ljava/util/Map;

    invoke-virtual {p2}, Lmp/PaymentResponse;->getProductName()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v3, v6}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;

    invoke-virtual {v3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->isConsumable()Z

    move-result v3

    if-eqz v3, :cond_4

    .line 146
    iget-object p3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-virtual {p2}, Lmp/PaymentResponse;->getProductName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {p2}, Lmp/PaymentResponse;->getMessageId()J

    move-result-wide v6

    invoke-static {v6, v7}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object p2

    invoke-static {p3, v3, p2}, Lorg/onepf/oms/appstore/FortumoBillingService;->addPendingPayment(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    :cond_3
    move-object p3, v0

    .line 151
    :cond_4
    :goto_0
    iput-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->developerPayload:Ljava/lang/String;

    .line 152
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    invoke-direct {p2, v4, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 153
    new-array v0, v5, [Ljava/lang/Object;

    const-string v2, "handleActivityResult: "

    aput-object v2, v0, v1

    aput-object p2, v0, p1

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 154
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {v0, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :goto_1
    return p1
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 4
    .param p1    # Landroid/app/Activity;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 87
    iput-object p5, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    .line 88
    iput p4, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->activityRequestCode:I

    .line 89
    iput-object p6, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->developerPayload:Ljava/lang/String;

    .line 90
    iget-object p3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->inappsMap:Ljava/util/Map;

    invoke-interface {p3, p2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;

    const/4 p5, 0x0

    const/4 p6, 0x2

    const/4 v0, 0x3

    const/4 v1, 0x0

    if-nez p3, :cond_0

    .line 92
    new-array p1, v0, [Ljava/lang/Object;

    const-string p3, "launchPurchaseFlow: required sku "

    aput-object p3, p1, v1

    const/4 p3, 0x1

    aput-object p2, p1, p3

    const-string p4, " was not defined"

    aput-object p4, p1, p6

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 93
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    new-instance p4, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 p6, 0x5

    const-string v0, "Required product %s was not defined in xml files."

    new-array p3, p3, [Ljava/lang/Object;

    aput-object p2, p3, v1

    invoke-static {v0, p3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-direct {p4, p6, p2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, p4, p5}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto/16 :goto_4

    .line 95
    :cond_0
    iget-object v2, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getProductId()Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Lorg/onepf/oms/appstore/FortumoBillingService;->getMessageIdInPending(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 96
    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->isConsumable()Z

    move-result v3

    if-eqz v3, :cond_4

    invoke-static {v2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v3

    if-nez v3, :cond_4

    const-string v3, "-1"

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_4

    .line 97
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {v2}, Ljava/lang/Long;->valueOf(Ljava/lang/String;)Ljava/lang/Long;

    move-result-object p3

    invoke-virtual {p3}, Ljava/lang/Long;->longValue()J

    move-result-wide p3

    invoke-static {p1, p3, p4}, Lmp/MpUtils;->getPaymentResponse(Landroid/content/Context;J)Lmp/PaymentResponse;

    move-result-object p1

    .line 100
    invoke-virtual {p1}, Lmp/PaymentResponse;->getBillingStatus()I

    move-result p3

    if-ne p3, p6, :cond_1

    .line 102
    iget-object p3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {p3, p1}, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFromPaymentResponse(Landroid/content/Context;Lmp/PaymentResponse;)Lorg/onepf/oms/appstore/googleUtils/Purchase;

    move-result-object p5

    .line 103
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string p3, "Purchase was successful."

    invoke-direct {p1, v1, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 104
    iget-object p3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {p3, p2}, Lorg/onepf/oms/appstore/FortumoBillingService;->removePendingProduct(Landroid/content/Context;Ljava/lang/String;)V

    goto :goto_1

    :cond_1
    const/4 p1, 0x6

    if-eq p3, v0, :cond_3

    const/4 p4, 0x4

    if-ne p3, p4, :cond_2

    goto :goto_0

    .line 109
    :cond_2
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string p3, "Purchase is in pending."

    invoke-direct {p2, p1, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    move-object p1, p2

    goto :goto_1

    .line 106
    :cond_3
    :goto_0
    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string p4, "Purchase was failed."

    invoke-direct {p3, p1, p4}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 107
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {p1, p2}, Lorg/onepf/oms/appstore/FortumoBillingService;->removePendingProduct(Landroid/content/Context;Ljava/lang/String;)V

    move-object p1, p3

    .line 111
    :goto_1
    iget-object p2, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFinishedListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p2, p1, p5}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_4

    .line 113
    :cond_4
    new-instance p2, Lmp/PaymentRequest$PaymentRequestBuilder;

    invoke-direct {p2}, Lmp/PaymentRequest$PaymentRequestBuilder;-><init>()V

    iget-boolean p5, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->isNook:Z

    if-eqz p5, :cond_5

    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getNookServiceId()Ljava/lang/String;

    move-result-object p5

    goto :goto_2

    :cond_5
    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getServiceId()Ljava/lang/String;

    move-result-object p5

    :goto_2
    iget-boolean p6, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->isNook:Z

    if-eqz p6, :cond_6

    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getNookInAppSecret()Ljava/lang/String;

    move-result-object p6

    goto :goto_3

    :cond_6
    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getInAppSecret()Ljava/lang/String;

    move-result-object p6

    :goto_3
    invoke-virtual {p2, p5, p6}, Lmp/PaymentRequest$PaymentRequestBuilder;->setService(Ljava/lang/String;Ljava/lang/String;)Lmp/PaymentRequest$PaymentRequestBuilder;

    move-result-object p2

    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->isConsumable()Z

    move-result p5

    invoke-virtual {p2, p5}, Lmp/PaymentRequest$PaymentRequestBuilder;->setConsumable(Z)Lmp/PaymentRequest$PaymentRequestBuilder;

    move-result-object p2

    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getProductId()Ljava/lang/String;

    move-result-object p5

    invoke-virtual {p2, p5}, Lmp/PaymentRequest$PaymentRequestBuilder;->setProductName(Ljava/lang/String;)Lmp/PaymentRequest$PaymentRequestBuilder;

    move-result-object p2

    invoke-virtual {p3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getTitle()Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p2, p3}, Lmp/PaymentRequest$PaymentRequestBuilder;->setDisplayString(Ljava/lang/String;)Lmp/PaymentRequest$PaymentRequestBuilder;

    move-result-object p2

    invoke-virtual {p2}, Lmp/PaymentRequest$PaymentRequestBuilder;->build()Lmp/PaymentRequest;

    move-result-object p2

    .line 119
    invoke-virtual {p2, p1}, Lmp/PaymentRequest;->toIntent(Landroid/content/Context;)Landroid/content/Intent;

    move-result-object p2

    .line 120
    invoke-virtual {p1, p2, p4}, Landroid/app/Activity;->startActivityForResult(Landroid/content/Intent;I)V

    :goto_4
    return-void
.end method

.method public queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 9
    .param p2    # Ljava/util/List;
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

    .line 161
    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {p3}, Lorg/onepf/oms/appstore/googleUtils/Inventory;-><init>()V

    .line 162
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    const-string v1, "onepf_shared_prefs_fortumo"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 163
    invoke-interface {v0}, Landroid/content/SharedPreferences;->getAll()Ljava/util/Map;

    move-result-object v1

    if-eqz v1, :cond_4

    .line 165
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 166
    invoke-interface {v1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v3

    .line 167
    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_0
    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_3

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 168
    invoke-interface {v1, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    if-eqz v5, :cond_2

    .line 170
    invoke-static {v5}, Ljava/lang/Long;->valueOf(Ljava/lang/String;)Ljava/lang/Long;

    move-result-object v5

    .line 171
    iget-object v6, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-virtual {v5}, Ljava/lang/Long;->longValue()J

    move-result-wide v7

    invoke-static {v6, v7, v8}, Lmp/MpUtils;->getPaymentResponse(Landroid/content/Context;J)Lmp/PaymentResponse;

    move-result-object v5

    .line 172
    invoke-virtual {v5}, Lmp/PaymentResponse;->getBillingStatus()I

    move-result v6

    const/4 v7, 0x2

    if-ne v6, v7, :cond_1

    .line 173
    iget-object v4, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {v4, v5}, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFromPaymentResponse(Landroid/content/Context;Lmp/PaymentResponse;)Lorg/onepf/oms/appstore/googleUtils/Purchase;

    move-result-object v4

    .line 174
    invoke-virtual {p3, v4}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addPurchase(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_0

    .line 175
    :cond_1
    invoke-virtual {v5}, Lmp/PaymentResponse;->getBillingStatus()I

    move-result v5

    const/4 v6, 0x3

    if-ne v5, v6, :cond_0

    .line 176
    invoke-interface {v0, v4}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    goto :goto_0

    .line 179
    :cond_2
    invoke-interface {v1, v4}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 182
    :cond_3
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 184
    :cond_4
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->inappsMap:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->values()Ljava/util/Collection;

    move-result-object v0

    .line 185
    invoke-interface {v0}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_5
    :goto_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_7

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;

    .line 186
    invoke-virtual {v1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->isConsumable()Z

    move-result v3

    if-nez v3, :cond_5

    .line 187
    iget-object v3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-virtual {v1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getServiceId()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getInAppSecret()Ljava/lang/String;

    move-result-object v5

    const/16 v6, 0x1388

    invoke-static {v3, v4, v5, v6}, Lmp/MpUtils;->getPurchaseHistory(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)Ljava/util/List;

    move-result-object v3

    if-eqz v3, :cond_5

    .line 188
    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v4

    if-lez v4, :cond_5

    .line 189
    invoke-interface {v3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_6
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_5

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    .line 190
    check-cast v4, Lmp/PaymentResponse;

    .line 191
    invoke-virtual {v4}, Lmp/PaymentResponse;->getProductName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v1}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->getProductId()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_6

    .line 192
    iget-object v3, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {v3, v4}, Lorg/onepf/oms/appstore/FortumoBillingService;->purchaseFromPaymentResponse(Landroid/content/Context;Lmp/PaymentResponse;)Lorg/onepf/oms/appstore/googleUtils/Purchase;

    move-result-object v3

    invoke-virtual {p3, v3}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addPurchase(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    if-eqz p1, :cond_5

    .line 194
    invoke-direct {p0, v1}, Lorg/onepf/oms/appstore/FortumoBillingService;->getSkuPrice(Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;)Ljava/lang/String;

    move-result-object v3

    .line 195
    invoke-virtual {v1, v3}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->toSkuDetails(Ljava/lang/String;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-result-object v1

    invoke-virtual {p3, v1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V

    goto :goto_1

    :cond_7
    if-eqz p1, :cond_9

    if-eqz p2, :cond_9

    .line 204
    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result p1

    if-lez p1, :cond_9

    .line 205
    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_2
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_9

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Ljava/lang/String;

    .line 206
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->inappsMap:Ljava/util/Map;

    invoke-interface {v0, p2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;

    if-eqz v0, :cond_8

    .line 208
    invoke-direct {p0, v0}, Lorg/onepf/oms/appstore/FortumoBillingService;->getSkuPrice(Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;)Ljava/lang/String;

    move-result-object p2

    .line 209
    invoke-virtual {v0, p2}, Lorg/onepf/oms/appstore/FortumoBillingService$FortumoProduct;->toSkuDetails(Ljava/lang/String;)Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    move-result-object p2

    invoke-virtual {p3, p2}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V

    goto :goto_2

    .line 211
    :cond_8
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/4 p3, 0x5

    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    aput-object p2, v0, v2

    const-string p2, "Data %s not found"

    invoke-static {p2, v0}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p3, p2}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p1

    :cond_9
    return-object p3
.end method

.method setupBilling(Z)Z
    .locals 4

    const/4 v0, 0x1

    .line 250
    :try_start_0
    iget-object v1, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->context:Landroid/content/Context;

    invoke-static {v1, p1}, Lorg/onepf/oms/appstore/FortumoBillingService;->getFortumoInapps(Landroid/content/Context;Z)Ljava/util/Map;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/FortumoBillingService;->inappsMap:Ljava/util/Map;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    move-exception p1

    const/4 v1, 0x2

    .line 252
    new-array v1, v1, [Ljava/lang/Object;

    const-string v2, "billing is not supported due to "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    invoke-virtual {p1}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p1

    aput-object p1, v1, v0

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return v3
.end method

.method public startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 4
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 80
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v1, "Fortumo: successful setup."

    const/4 v2, 0x0

    invoke-direct {v0, v2, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    const/4 v1, 0x2

    .line 81
    new-array v1, v1, [Ljava/lang/Object;

    const-string v3, "Setup result: "

    aput-object v3, v1, v2

    const/4 v2, 0x1

    aput-object v0, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 82
    invoke-interface {p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    return-void
.end method

.method public subscriptionsSupported()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method
