.class final Ljp/colopl/drapro/InAppBillingHelper$4;
.super Ljava/lang/Object;
.source "InAppBillingHelper.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/InAppBillingHelper;->checkAndGivePromotionitems(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$items:[Ljava/lang/String;


# direct methods
.method constructor <init>([Ljava/lang/String;)V
    .locals 0

    .line 314
    iput-object p1, p0, Ljp/colopl/drapro/InAppBillingHelper$4;->val$items:[Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onQueryInventoryFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V
    .locals 8

    .line 317
    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->isFailure()Z

    move-result p1

    const/4 v0, 0x0

    if-eqz p1, :cond_0

    .line 318
    invoke-static {v0}, Ljp/colopl/drapro/InAppBillingHelper;->FinishCheckPromotion(Z)V

    return-void

    .line 322
    :cond_0
    invoke-virtual {p2}, Ljp/colopl/iab/Inventory;->getAllPurchases()Ljava/util/List;

    move-result-object p1

    if-eqz p1, :cond_6

    .line 323
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result p2

    if-lez p2, :cond_6

    .line 325
    invoke-static {}, Ljp/colopl/drapro/NetworkHelper;->getHost()Ljava/lang/String;

    move-result-object p2

    const-string v1, ""

    if-ne p2, v1, :cond_1

    const-string p1, "InAppBillingHelper"

    const-string p2, "Host is null!!!"

    .line 326
    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 327
    invoke-static {v0}, Ljp/colopl/drapro/InAppBillingHelper;->FinishCheckPromotion(Z)V

    return-void

    .line 332
    :cond_1
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 p2, 0x1

    const/4 v1, 0x0

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_5

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljp/colopl/iab/Purchase;

    .line 334
    iget-object v3, p0, Ljp/colopl/drapro/InAppBillingHelper$4;->val$items:[Ljava/lang/String;

    array-length v4, v3

    const/4 v5, 0x0

    :goto_1
    if-ge v5, v4, :cond_3

    aget-object v6, v3, v5

    .line 335
    invoke-virtual {v2}, Ljp/colopl/iab/Purchase;->getSku()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v7, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_2

    const/4 v3, 0x1

    goto :goto_2

    :cond_2
    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    :cond_3
    const/4 v3, 0x0

    :goto_2
    if-nez v3, :cond_4

    goto :goto_0

    :cond_4
    const-string v1, "InAppBillingHelper"

    .line 345
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "checkAndGivePromotionitems Purchase:"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljp/colopl/iab/Purchase;->getSku()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 348
    sget v1, Ljp/colopl/drapro/InAppBillingHelper;->requestCount:I

    add-int/2addr v1, p2

    sput v1, Ljp/colopl/drapro/InAppBillingHelper;->requestCount:I

    .line 350
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {}, Ljp/colopl/drapro/NetworkHelper;->getHost()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "/ajax/payments/inappbilling/promotion"

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 351
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    .line 352
    new-instance v4, Lorg/apache/http/message/BasicNameValuePair;

    const-string v5, "mainToken"

    sget-object v6, Ljp/colopl/drapro/InAppBillingHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v6}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object v6

    invoke-virtual {v6}, Ljp/colopl/config/Config;->getSession()Ljp/colopl/config/Session;

    move-result-object v6

    invoke-virtual {v6}, Ljp/colopl/config/Session;->getSid()Ljava/lang/String;

    move-result-object v6

    invoke-direct {v4, v5, v6}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 353
    new-instance v4, Lorg/apache/http/message/BasicNameValuePair;

    const-string v5, "signedData"

    invoke-virtual {v2}, Ljp/colopl/iab/Purchase;->getOriginalJson()Ljava/lang/String;

    move-result-object v6

    invoke-direct {v4, v5, v6}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 354
    new-instance v4, Lorg/apache/http/message/BasicNameValuePair;

    const-string v5, "signature"

    invoke-virtual {v2}, Ljp/colopl/iab/Purchase;->getSignature()Ljava/lang/String;

    move-result-object v6

    invoke-direct {v4, v5, v6}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 355
    new-instance v4, Lorg/apache/http/message/BasicNameValuePair;

    const-string v5, "iabver"

    const-string v6, "3"

    invoke-direct {v4, v5, v6}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 356
    new-instance v4, Lorg/apache/http/message/BasicNameValuePair;

    const-string v5, "apv"

    sget-object v6, Ljp/colopl/drapro/InAppBillingHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v6}, Ljp/colopl/drapro/StartActivity;->getConfig()Ljp/colopl/config/Config;

    move-result-object v6

    invoke-virtual {v6}, Ljp/colopl/config/Config;->getVersionCode()I

    move-result v6

    invoke-static {v6}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v6

    invoke-direct {v4, v5, v6}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 357
    new-instance v4, Ljp/colopl/network/HttpPostAsyncTask;

    sget-object v5, Ljp/colopl/drapro/InAppBillingHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-direct {v4, v5, v1, v3}, Ljp/colopl/network/HttpPostAsyncTask;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/List;)V

    .line 358
    new-instance v1, Ljp/colopl/drapro/InAppBillingHelper$4$1;

    invoke-direct {v1, p0, v2}, Ljp/colopl/drapro/InAppBillingHelper$4$1;-><init>(Ljp/colopl/drapro/InAppBillingHelper$4;Ljp/colopl/iab/Purchase;)V

    invoke-virtual {v4, v1}, Ljp/colopl/network/HttpPostAsyncTask;->setListener(Ljp/colopl/network/HttpRequestListener;)V

    .line 384
    new-array v1, v0, [Ljava/lang/Void;

    invoke-virtual {v4, v1}, Ljp/colopl/network/HttpPostAsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    const/4 v1, 0x1

    goto/16 :goto_0

    :cond_5
    if-nez v1, :cond_7

    .line 388
    invoke-static {v0}, Ljp/colopl/drapro/InAppBillingHelper;->FinishCheckPromotion(Z)V

    goto :goto_3

    :cond_6
    const-string p1, "InAppBillingHelper"

    const-string p2, "Promotion List is null or Empty"

    .line 392
    invoke-static {p1, p2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 393
    invoke-static {v0}, Ljp/colopl/drapro/InAppBillingHelper;->FinishCheckPromotion(Z)V

    :cond_7
    :goto_3
    return-void
.end method
