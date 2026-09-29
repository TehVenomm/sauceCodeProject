.class public Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;
.super Ljava/lang/Object;
.source "SkubitIabHelper.java"

# interfaces
.implements Lorg/onepf/oms/AppstoreInAppBillingService;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;,
        Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeFinishedListener;,
        Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;
    }
.end annotation


# static fields
.field public static final QUERY_SKU_DETAILS_BATCH_SIZE:I = 0x1e


# instance fields
.field private mAppstore:Lorg/onepf/oms/Appstore;

.field protected mAsyncInProgress:Z

.field protected mAsyncOperation:Ljava/lang/String;

.field protected mComponentName:Landroid/content/ComponentName;

.field protected mContext:Landroid/content/Context;

.field mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field protected mPurchasingItemType:Ljava/lang/String;

.field protected mRequestCode:I

.field protected mService:Lcom/skubit/android/billing/IBillingService;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field protected mServiceConn:Landroid/content/ServiceConnection;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field protected mSetupDone:Z

.field protected mSignatureBase64:Ljava/lang/String;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field protected mSubscriptionsSupported:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;)V
    .locals 1
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 157
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 102
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSetupDone:Z

    .line 105
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSubscriptionsSupported:Z

    .line 109
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncInProgress:Z

    const-string v0, ""

    .line 113
    iput-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncOperation:Ljava/lang/String;

    const/4 v0, 0x0

    .line 137
    iput-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSignatureBase64:Ljava/lang/String;

    if-eqz p1, :cond_0

    .line 161
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mContext:Landroid/content/Context;

    .line 162
    iput-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSignatureBase64:Ljava/lang/String;

    .line 163
    iput-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAppstore:Lorg/onepf/oms/Appstore;

    const-string p1, "Skubit IAB helper created."

    .line 164
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return-void

    .line 159
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string p2, "context is null"

    invoke-direct {p1, p2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method checkSetupDone(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 9
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/Purchase;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation

    const-string v0, "consume"

    .line 637
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 639
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getItemType()Ljava/lang/String;

    move-result-object v0

    const-string v1, "inapp"

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 645
    :try_start_0
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getToken()Ljava/lang/String;

    move-result-object v0

    .line 646
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x3

    const/4 v3, 0x2

    const/4 v4, 0x0

    const/4 v5, 0x1

    if-eqz v0, :cond_2

    const-string v6, ""

    .line 647
    invoke-virtual {v0, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-nez v6, :cond_2

    const/4 v6, 0x4

    .line 653
    new-array v7, v6, [Ljava/lang/Object;

    const-string v8, "Consuming sku: "

    aput-object v8, v7, v4

    aput-object v1, v7, v5

    const-string v8, ", token: "

    aput-object v8, v7, v3

    aput-object v0, v7, v2

    invoke-static {v7}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 654
    iget-object v7, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    if-eqz v7, :cond_1

    .line 658
    iget-object v7, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getPackageName()Ljava/lang/String;

    move-result-object v8

    invoke-interface {v7, v5, v8, v0}, Lcom/skubit/android/billing/IBillingService;->consumePurchase(ILjava/lang/String;Ljava/lang/String;)I

    move-result v0

    if-nez v0, :cond_0

    .line 660
    new-array v0, v3, [Ljava/lang/Object;

    const-string v2, "Successfully consumed sku: "

    aput-object v2, v0, v4

    aput-object v1, v0, v5

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-void

    .line 662
    :cond_0
    new-array v6, v6, [Ljava/lang/Object;

    const-string v7, "Error consuming consuming sku "

    aput-object v7, v6, v4

    aput-object v1, v6, v5

    const-string v4, ". "

    aput-object v4, v6, v3

    invoke-static {v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object v3

    aput-object v3, v6, v2

    invoke-static {v6}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 663
    new-instance v2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Error consuming sku "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v2, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw v2

    .line 655
    :cond_1
    new-array v0, v2, [Ljava/lang/Object;

    const-string v2, "Error consuming consuming sku "

    aput-object v2, v0, v4

    aput-object v1, v0, v5

    const-string v2, ". Service is not connected."

    aput-object v2, v0, v3

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 656
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/4 v2, 0x6

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Error consuming sku "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v2, v1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw v0

    .line 648
    :cond_2
    new-array v0, v2, [Ljava/lang/Object;

    const-string v2, "In-app billing error: Can\'t consume "

    aput-object v2, v0, v4

    aput-object v1, v0, v5

    const-string v2, ". No token."

    aput-object v2, v0, v3

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 649
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v2, -0x3ef

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "PurchaseInfo is missing token for sku: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " "

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v2, v1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw v0
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    move-exception v0

    .line 666
    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v2, -0x3e9

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Remote exception while consuming. PurchaseInfo: "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v1, v2, p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw v1

    .line 640
    :cond_3
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v1, -0x3f2

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Items of type \'"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getItemType()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "\' can\'t be consumed."

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, v1, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw v0
.end method

.method public consumeAsync(Ljava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;)V
    .locals 1
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;",
            "Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;",
            ")V"
        }
    .end annotation

    const-string v0, "consume"

    .line 724
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->checkSetupDone(Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 725
    invoke-virtual {p0, p1, v0, p2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;)V

    return-void
.end method

.method public consumeAsync(Lorg/onepf/oms/appstore/googleUtils/Purchase;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeFinishedListener;)V
    .locals 1

    const-string v0, "consume"

    .line 710
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 711
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 712
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const/4 p1, 0x0

    .line 713
    invoke-virtual {p0, v0, p2, p1}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;)V

    return-void
.end method

.method consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;)V
    .locals 8
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;",
            "Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeFinishedListener;",
            "Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;",
            ")V"
        }
    .end annotation

    .line 938
    new-instance v4, Landroid/os/Handler;

    invoke-direct {v4}, Landroid/os/Handler;-><init>()V

    const-string v0, "consume"

    .line 939
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagStartAsync(Ljava/lang/String;)V

    .line 940
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$3;

    move-object v0, v7

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v5, p3

    invoke-direct/range {v0 .. v5}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$3;-><init>(Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;Ljava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeFinishedListener;Landroid/os/Handler;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$OnConsumeMultiFinishedListener;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    return-void
.end method

.method public dispose()V
    .locals 2

    const-string v0, "Disposing."

    .line 271
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 272
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSetupDone:Z

    .line 273
    iget-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    if-eqz v0, :cond_1

    const-string v0, "Unbinding from service."

    .line 274
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 275
    iget-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mContext:Landroid/content/Context;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mContext:Landroid/content/Context;

    iget-object v1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    invoke-virtual {v0, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    :cond_0
    const/4 v0, 0x0

    .line 276
    iput-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    .line 277
    iput-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    .line 278
    iput-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    :cond_1
    return-void
.end method

.method flagEndAsync()V
    .locals 4

    const/4 v0, 0x2

    .line 793
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Ending async operation: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncOperation:Ljava/lang/String;

    const/4 v3, 0x1

    aput-object v1, v0, v3

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const-string v0, ""

    .line 794
    iput-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncOperation:Ljava/lang/String;

    .line 795
    iput-boolean v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncInProgress:Z

    return-void
.end method

.method flagStartAsync(Ljava/lang/String;)V
    .locals 4

    .line 785
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncInProgress:Z

    if-nez v0, :cond_0

    .line 787
    iput-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncOperation:Ljava/lang/String;

    const/4 v0, 0x1

    .line 788
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncInProgress:Z

    const/4 v1, 0x2

    .line 789
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "Starting async operation: "

    aput-object v3, v1, v2

    aput-object p1, v1, v0

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-void

    .line 785
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can\'t start async operation ("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, ") because another async operation("

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAsyncOperation:Ljava/lang/String;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, ") is in progress."

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public getPackageName()Ljava/lang/String;
    .locals 1

    .line 671
    iget-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mContext:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method getResponseCodeFromBundle(Landroid/os/Bundle;)I
    .locals 5
    .param p1    # Landroid/os/Bundle;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "RESPONSE_CODE"

    .line 756
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    const/4 v0, 0x0

    if-nez p1, :cond_0

    const-string p1, "Bundle with null response code, assuming OK (known issue)"

    .line 758
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return v0

    .line 760
    :cond_0
    instance-of v1, p1, Ljava/lang/Integer;

    if-eqz v1, :cond_1

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    return p1

    .line 761
    :cond_1
    instance-of v1, p1, Ljava/lang/Long;

    if-eqz v1, :cond_2

    check-cast p1, Ljava/lang/Long;

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    long-to-int p1, v0

    return p1

    :cond_2
    const/4 v1, 0x2

    .line 763
    new-array v2, v1, [Ljava/lang/Object;

    const-string v3, "In-app billing error: "

    aput-object v3, v2, v0

    const/4 v3, 0x1

    const-string v4, "Unexpected type for bundle response code."

    aput-object v4, v2, v3

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 764
    new-array v1, v1, [Ljava/lang/Object;

    const-string v2, "In-app billing error: "

    aput-object v2, v1, v0

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v1, v3

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 765
    new-instance v0, Ljava/lang/RuntimeException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected type for bundle response code: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method getResponseCodeFromIntent(Landroid/content/Intent;)I
    .locals 3
    .param p1    # Landroid/content/Intent;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 771
    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p1

    const-string v0, "RESPONSE_CODE"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    const/4 v0, 0x0

    if-nez p1, :cond_0

    const-string p1, "In-app billing error: Intent with no response code, assuming OK (known issue)"

    .line 773
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    return v0

    .line 775
    :cond_0
    instance-of v1, p1, Ljava/lang/Integer;

    if-eqz v1, :cond_1

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    return p1

    .line 776
    :cond_1
    instance-of v1, p1, Ljava/lang/Long;

    if-eqz v1, :cond_2

    check-cast p1, Ljava/lang/Long;

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    long-to-int p1, v0

    return p1

    :cond_2
    const-string v1, "In-app billing error: Unexpected type for intent response code."

    .line 778
    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    const/4 v1, 0x2

    .line 779
    new-array v1, v1, [Ljava/lang/Object;

    const-string v2, "In-app billing error: "

    aput-object v2, v1, v0

    const/4 v0, 0x1

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    aput-object v2, v1, v0

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 780
    new-instance v0, Ljava/lang/RuntimeException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected type for intent response code: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method protected getServiceFromBinder(Landroid/os/IBinder;)Lcom/skubit/android/billing/IBillingService;
    .locals 0
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 261
    invoke-static {p1}, Lcom/skubit/android/billing/IBillingService$Stub;->asInterface(Landroid/os/IBinder;)Lcom/skubit/android/billing/IBillingService;

    move-result-object p1

    return-object p1
.end method

.method protected getServiceIntent()Landroid/content/Intent;
    .locals 2

    .line 251
    new-instance v0, Landroid/content/Intent;

    const-string v1, "com.skubit.android.billing.IBillingService.BIND"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v1, "com.skubit.android"

    .line 252
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    return-object v0
.end method

.method public handleActivityResult(IILandroid/content/Intent;)Z
    .locals 6
    .param p3    # Landroid/content/Intent;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 414
    iget v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mRequestCode:I

    const/4 v1, 0x0

    if-eq p1, v0, :cond_0

    return v1

    :cond_0
    const-string p1, "handleActivityResult"

    .line 416
    invoke-virtual {p0, p1}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 419
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagEndAsync()V

    const/4 p1, 0x1

    const/4 v0, 0x0

    if-nez p3, :cond_2

    const-string p2, "In-app billing error: Null data in IAB activity result."

    .line 422
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 423
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3ea

    const-string v1, "Null data in IAB result"

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 424
    iget-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_1

    iget-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_1
    return p1

    .line 428
    :cond_2
    invoke-virtual {p0, p3}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getResponseCodeFromIntent(Landroid/content/Intent;)I

    move-result v2

    const-string v3, "INAPP_PURCHASE_DATA"

    .line 429
    invoke-virtual {p3, v3}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v4, "INAPP_DATA_SIGNATURE"

    .line 430
    invoke-virtual {p3, v4}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const/4 v5, -0x1

    if-ne p2, v5, :cond_3

    if-nez v2, :cond_3

    .line 433
    invoke-virtual {p0, p3, v3, v4}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->processPurchaseSuccess(Landroid/content/Intent;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_3
    if-ne p2, v5, :cond_4

    .line 436
    invoke-virtual {p0, v2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->processPurchaseFail(I)V

    goto :goto_0

    :cond_4
    if-nez p2, :cond_5

    const/4 p2, 0x2

    .line 438
    new-array p2, p2, [Ljava/lang/Object;

    const-string p3, "Purchase canceled - Response: "

    aput-object p3, p2, v1

    invoke-static {v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object p3

    aput-object p3, p2, p1

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 439
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3ed

    const-string v1, "User canceled."

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 440
    iget-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_6

    iget-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_0

    .line 442
    :cond_5
    new-instance p3, Ljava/lang/StringBuilder;

    invoke-direct {p3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "In-app billing error: Purchase failed. Result code: "

    invoke-virtual {p3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p2}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, ". Response: "

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 444
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3ee

    const-string v1, "Unknown purchase response."

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 445
    iget-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_6

    iget-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_6
    :goto_0
    return p1
.end method

.method isValidDataSignature(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 0
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    if-nez p1, :cond_0

    const/4 p1, 0x1

    return p1

    .line 973
    :cond_0
    invoke-static {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/Security;->verifyPurchase(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_1

    const-string p2, "In-app billing warning: Purchase signature verification **FAILED**."

    .line 975
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;)V

    :cond_1
    return p1
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;)V
    .locals 6
    .param p1    # Landroid/app/Activity;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v5, ""

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v3, p3

    move-object v4, p4

    .line 304
    invoke-virtual/range {v0 .. v5}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 7
    .param p1    # Landroid/app/Activity;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v3, "inapp"

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v4, p3

    move-object v5, p4

    move-object v6, p5

    .line 309
    invoke-virtual/range {v0 .. v6}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 23
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

    move-object/from16 v1, p0

    move-object/from16 v8, p2

    move-object/from16 v0, p3

    move-object/from16 v9, p5

    const-string v2, "launchPurchaseFlow"

    .line 342
    invoke-virtual {v1, v2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->checkSetupDone(Ljava/lang/String;)V

    const-string v2, "launchPurchaseFlow"

    .line 343
    invoke-virtual {v1, v2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagStartAsync(Ljava/lang/String;)V

    const-string v2, "subs"

    .line 346
    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    const/4 v10, 0x0

    if-eqz v2, :cond_1

    iget-boolean v2, v1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSubscriptionsSupported:Z

    if-nez v2, :cond_1

    .line 347
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v2, -0x3f1

    const-string v3, "Subscriptions are not available."

    invoke-direct {v0, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    if-eqz v9, :cond_0

    .line 349
    invoke-interface {v9, v0, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    .line 350
    :cond_0
    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagEndAsync()V

    return-void

    :cond_1
    const/4 v11, 0x4

    .line 355
    :try_start_0
    new-array v2, v11, [Ljava/lang/Object;

    const-string v3, "Constructing buy intent for "

    const/4 v12, 0x0

    aput-object v3, v2, v12

    const/4 v13, 0x1

    aput-object v8, v2, v13

    const-string v3, ", item type: "

    const/4 v14, 0x2

    aput-object v3, v2, v14

    const/4 v15, 0x3

    aput-object v0, v2, v15

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 356
    iget-object v2, v1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    if-nez v2, :cond_3

    const-string v0, "In-app billing error: Unable to buy item, Error response: service is not connected."

    .line 357
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 358
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v2, 0x6

    const-string v3, "Unable to buy item"

    invoke-direct {v0, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    if-eqz v9, :cond_2

    .line 359
    invoke-interface {v9, v0, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    .line 360
    :cond_2
    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagEndAsync()V

    return-void

    .line 363
    :cond_3
    iget-object v2, v1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    const/4 v3, 0x1

    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getPackageName()Ljava/lang/String;

    move-result-object v4

    move-object/from16 v5, p2

    move-object/from16 v6, p3

    move-object/from16 v7, p6

    invoke-interface/range {v2 .. v7}, Lcom/skubit/android/billing/IBillingService;->getBuyIntent(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v2

    .line 364
    invoke-virtual {v1, v2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getResponseCodeFromBundle(Landroid/os/Bundle;)I

    move-result v3

    if-eqz v3, :cond_5

    .line 366
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "In-app billing error: Unable to buy item, Error response: "

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 368
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v2, "Unable to buy item"

    invoke-direct {v0, v3, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    if-eqz v9, :cond_4

    .line 369
    invoke-interface {v9, v0, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    .line 370
    :cond_4
    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagEndAsync()V

    return-void

    :cond_5
    const-string v3, "BUY_INTENT"

    .line 374
    invoke-virtual {v2, v3}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v2

    check-cast v2, Landroid/app/PendingIntent;

    .line 375
    new-array v3, v11, [Ljava/lang/Object;

    const-string v4, "Launching buy intent for "

    aput-object v4, v3, v12

    aput-object v8, v3, v13

    const-string v4, ". Request code: "

    aput-object v4, v3, v14

    invoke-static/range {p4 .. p4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v3, v15

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    move/from16 v3, p4

    .line 376
    iput v3, v1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mRequestCode:I

    .line 377
    iput-object v9, v1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    .line 378
    iput-object v0, v1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchasingItemType:Ljava/lang/String;

    .line 379
    invoke-virtual {v2}, Landroid/app/PendingIntent;->getIntentSender()Landroid/content/IntentSender;

    move-result-object v17

    new-instance v19, Landroid/content/Intent;

    invoke-direct/range {v19 .. v19}, Landroid/content/Intent;-><init>()V

    invoke-static {v12}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v20

    invoke-static {v12}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v21

    invoke-static {v12}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v22

    move-object/from16 v16, p1

    move/from16 v18, p4

    invoke-virtual/range {v16 .. v22}, Landroid/app/Activity;->startIntentSenderForResult(Landroid/content/IntentSender;ILandroid/content/Intent;III)V
    :try_end_0
    .catch Landroid/content/IntentSender$SendIntentException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 390
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "In-app billing error: RemoteException while launching purchase flow for sku "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 391
    invoke-virtual {v0}, Landroid/os/RemoteException;->printStackTrace()V

    .line 393
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v2, -0x3e9

    const-string v3, "Remote exception while starting purchase flow"

    invoke-direct {v0, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    if-eqz v9, :cond_6

    .line 394
    invoke-interface {v9, v0, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_0

    :catch_1
    move-exception v0

    .line 384
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "In-app billing error: SendIntentException while launching purchase flow for sku "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 385
    invoke-virtual {v0}, Landroid/content/IntentSender$SendIntentException;->printStackTrace()V

    .line 387
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v2, -0x3ec

    const-string v3, "Failed to send intent."

    invoke-direct {v0, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    if-eqz v9, :cond_6

    .line 388
    invoke-interface {v9, v0, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    .line 396
    :cond_6
    :goto_0
    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagEndAsync()V

    return-void
.end method

.method public launchSubscriptionPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;)V
    .locals 6
    .param p1    # Landroid/app/Activity;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v5, ""

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v3, p3

    move-object v4, p4

    .line 314
    invoke-virtual/range {v0 .. v5}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->launchSubscriptionPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public launchSubscriptionPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 7
    .param p1    # Landroid/app/Activity;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v3, "subs"

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v4, p3

    move-object v5, p4

    move-object v6, p5

    .line 319
    invoke-virtual/range {v0 .. v6}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public processPurchaseFail(I)V
    .locals 3

    const/4 v0, 0x2

    .line 452
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Result code was OK but in-app billing response was not OK: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    invoke-static {p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    aput-object v1, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 453
    iget-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz v0, :cond_0

    .line 454
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v1, "Problem purchashing item."

    invoke-direct {v0, p1, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 455
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    const/4 v1, 0x0

    invoke-interface {p1, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_0
    return-void
.end method

.method public processPurchaseSuccess(Landroid/content/Intent;Ljava/lang/String;Ljava/lang/String;)V
    .locals 5
    .param p1    # Landroid/content/Intent;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    const-string v0, "Successful resultcode from purchase activity."

    .line 461
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    const/4 v0, 0x2

    .line 462
    new-array v1, v0, [Ljava/lang/Object;

    const-string v2, "Purchase data: "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    const/4 v2, 0x1

    aput-object p2, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 463
    new-array v1, v0, [Ljava/lang/Object;

    const-string v4, "Data signature: "

    aput-object v4, v1, v3

    aput-object p3, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 464
    new-array v1, v0, [Ljava/lang/Object;

    const-string v4, "Extras: "

    aput-object v4, v1, v3

    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v4

    aput-object v4, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 465
    new-array v1, v0, [Ljava/lang/Object;

    const-string v4, "Expected item type: "

    aput-object v4, v1, v3

    iget-object v4, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchasingItemType:Ljava/lang/String;

    aput-object v4, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const/4 v1, 0x0

    if-eqz p2, :cond_5

    if-nez p3, :cond_0

    goto/16 :goto_0

    .line 477
    :cond_0
    :try_start_0
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    iget-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchasingItemType:Ljava/lang/String;

    iget-object v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAppstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v2}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v2

    invoke-direct {p1, v0, p2, p3, v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 478
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v0

    .line 479
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v2

    iget-object v4, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAppstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v4}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v4, v0}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 481
    iget-object v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSignatureBase64:Ljava/lang/String;

    invoke-virtual {p0, v2, p2, p3}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->isValidDataSignature(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p2

    if-nez p2, :cond_2

    .line 482
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string p3, "In-app billing error: Purchase signature verification FAILED for sku "

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 483
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3eb

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Signature verification failed for sku "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p2, p3, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 484
    iget-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_1

    .line 485
    iget-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_1
    return-void

    :cond_2
    const-string p2, "Purchase signature successfully verified."

    .line 489
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 498
    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p2, :cond_3

    .line 499
    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "Success"

    invoke-direct {p3, v3, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p2, p3, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_3
    return-void

    :catch_0
    move-exception p1

    const-string p2, "In-app billing error: Failed to parse purchase data."

    .line 491
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 492
    invoke-virtual {p1}, Lorg/json/JSONException;->printStackTrace()V

    .line 493
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p2, -0x3ea

    const-string p3, "Failed to parse purchase data."

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 494
    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p2, :cond_4

    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p2, p1, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_4
    return-void

    :cond_5
    :goto_0
    const-string p2, "In-app billing error: BUG: either purchaseData or dataSignature is null."

    .line 468
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 469
    new-array p2, v0, [Ljava/lang/Object;

    const-string p3, "Extras: "

    aput-object p3, p2, v3

    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p1

    aput-object p1, p2, v2

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 470
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p2, -0x3f0

    const-string p3, "IAB returned null purchaseData or dataSignature"

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 471
    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p2, :cond_6

    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p2, p1, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_6
    return-void
.end method

.method public queryInventory(ZLjava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
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

    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    const/4 v0, 0x0

    .line 505
    invoke-virtual {p0, p1, p2, v0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

    move-result-object p1

    return-object p1
.end method

.method public queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 2
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

    const-string v0, "queryInventory"

    .line 523
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 525
    :try_start_0
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;-><init>()V

    const-string v1, "inapp"

    .line 526
    invoke-virtual {p0, v0, v1}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->queryPurchases(Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/lang/String;)I

    move-result v1

    if-nez v1, :cond_5

    if-eqz p1, :cond_1

    const-string v1, "inapp"

    .line 532
    invoke-virtual {p0, v1, v0, p2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->querySkuDetails(Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/util/List;)I

    move-result p2

    if-nez p2, :cond_0

    goto :goto_0

    .line 534
    :cond_0
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p3, "Error refreshing inventory (querying prices of items)."

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p1

    .line 539
    :cond_1
    :goto_0
    iget-boolean p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSubscriptionsSupported:Z

    if-eqz p2, :cond_4

    const-string p2, "subs"

    .line 540
    invoke-virtual {p0, v0, p2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->queryPurchases(Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/lang/String;)I

    move-result p2

    if-nez p2, :cond_3

    if-eqz p1, :cond_4

    const-string p1, "subs"

    .line 546
    invoke-virtual {p0, p1, v0, p3}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->querySkuDetails(Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/util/List;)I

    move-result p1

    if-nez p1, :cond_2

    goto :goto_1

    .line 548
    :cond_2
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p3, "Error refreshing inventory (querying prices of subscriptions)."

    invoke-direct {p2, p1, p3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p2

    .line 542
    :cond_3
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p3, "Error refreshing inventory (querying owned subscriptions)."

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p1

    :cond_4
    :goto_1
    return-object v0

    .line 528
    :cond_5
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p2, "Error refreshing inventory (querying owned items)."

    invoke-direct {p1, v1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    move-exception p1

    .line 557
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p3, -0x3ea

    const-string v0, "Error parsing JSON response while refreshing inventory."

    invoke-direct {p2, p3, v0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p2

    :catch_1
    move-exception p1

    .line 555
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p3, -0x3e9

    const-string v0, "Remote exception while refreshing inventory."

    invoke-direct {p2, p3, v0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p2
.end method

.method public queryInventoryAsync(Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 615
    invoke-virtual {p0, v0, v1, p1}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;)V
    .locals 8
    .param p3    # Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;",
            ")V"
        }
    .end annotation

    .line 588
    new-instance v4, Landroid/os/Handler;

    invoke-direct {v4}, Landroid/os/Handler;-><init>()V

    const-string v0, "queryInventory"

    .line 589
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->checkSetupDone(Ljava/lang/String;)V

    const-string v0, "refresh inventory"

    .line 590
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagStartAsync(Ljava/lang/String;)V

    .line 591
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;

    move-object v0, v7

    move-object v1, p0

    move v2, p1

    move-object v3, p2

    move-object v5, p3

    invoke-direct/range {v0 .. v5}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;-><init>(Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;ZLjava/util/List;Landroid/os/Handler;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    return-void
.end method

.method public queryInventoryAsync(ZLorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;)V
    .locals 1
    .param p2    # Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x0

    .line 619
    invoke-virtual {p0, p1, v0, p2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method queryPurchases(Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/lang/String;)I
    .locals 16
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/Inventory;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;,
            Landroid/os/RemoteException;
        }
    .end annotation

    move-object/from16 v0, p0

    move-object/from16 v1, p2

    const/4 v2, 0x2

    .line 801
    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "Querying owned items, item type: "

    const/4 v5, 0x0

    aput-object v4, v3, v5

    const/4 v4, 0x1

    aput-object v1, v3, v4

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 802
    new-array v3, v2, [Ljava/lang/Object;

    const-string v6, "Package name: "

    aput-object v6, v3, v5

    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getPackageName()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v3, v4

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const/4 v3, 0x0

    const/4 v6, 0x0

    .line 807
    :goto_0
    new-array v7, v2, [Ljava/lang/Object;

    const-string v8, "Calling getPurchases with continuation token: "

    aput-object v8, v7, v5

    aput-object v3, v7, v4

    invoke-static {v7}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 808
    iget-object v7, v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    if-nez v7, :cond_0

    const-string v1, "getPurchases() failed: service is not connected."

    .line 809
    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    const/4 v1, 0x6

    return v1

    .line 812
    :cond_0
    iget-object v7, v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getPackageName()Ljava/lang/String;

    move-result-object v8

    invoke-interface {v7, v4, v8, v1, v3}, Lcom/skubit/android/billing/IBillingService;->getPurchases(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v3

    .line 814
    invoke-virtual {v0, v3}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getResponseCodeFromBundle(Landroid/os/Bundle;)I

    move-result v7

    .line 815
    new-array v8, v2, [Ljava/lang/Object;

    const-string v9, "Owned items response: "

    aput-object v9, v8, v5

    invoke-static {v7}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v9

    aput-object v9, v8, v4

    invoke-static {v8}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-eqz v7, :cond_1

    .line 817
    new-array v1, v2, [Ljava/lang/Object;

    const-string v2, "getPurchases() failed: "

    aput-object v2, v1, v5

    invoke-static {v7}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object v2

    aput-object v2, v1, v4

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return v7

    :cond_1
    const-string v7, "INAPP_PURCHASE_ITEM_LIST"

    .line 820
    invoke-virtual {v3, v7}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v7

    if-eqz v7, :cond_8

    const-string v7, "INAPP_PURCHASE_DATA_LIST"

    invoke-virtual {v3, v7}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v7

    if-eqz v7, :cond_8

    const-string v7, "INAPP_DATA_SIGNATURE_LIST"

    invoke-virtual {v3, v7}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v7

    if-nez v7, :cond_2

    goto/16 :goto_3

    :cond_2
    const-string v7, "INAPP_PURCHASE_ITEM_LIST"

    .line 827
    invoke-virtual {v3, v7}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v7

    const-string v8, "INAPP_PURCHASE_DATA_LIST"

    .line 828
    invoke-virtual {v3, v8}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v8

    const-string v9, "INAPP_DATA_SIGNATURE_LIST"

    .line 829
    invoke-virtual {v3, v9}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v9

    move v10, v6

    const/4 v6, 0x0

    .line 831
    :goto_1
    invoke-virtual {v8}, Ljava/util/ArrayList;->size()I

    move-result v11

    if-ge v6, v11, :cond_5

    .line 832
    invoke-virtual {v8, v6}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v11

    check-cast v11, Ljava/lang/String;

    .line 833
    invoke-virtual {v9, v6}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v12

    check-cast v12, Ljava/lang/String;

    .line 834
    invoke-virtual {v7, v6}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v13

    check-cast v13, Ljava/lang/String;

    .line 836
    iget-object v14, v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSignatureBase64:Ljava/lang/String;

    invoke-virtual {v0, v14, v11, v12}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->isValidDataSignature(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result v14

    if-eqz v14, :cond_4

    .line 837
    new-array v14, v2, [Ljava/lang/Object;

    const-string v15, "Sku is owned: "

    aput-object v15, v14, v5

    aput-object v13, v14, v4

    invoke-static {v14}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 838
    new-instance v13, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    iget-object v14, v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAppstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v14}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v14

    invoke-direct {v13, v1, v11, v12, v14}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 839
    invoke-virtual {v13}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v12

    .line 840
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v14

    iget-object v15, v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAppstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v15}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v15

    invoke-virtual {v14, v15, v12}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v13, v12}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 842
    invoke-virtual {v13}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getToken()Ljava/lang/String;

    move-result-object v12

    invoke-static {v12}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v12

    if-eqz v12, :cond_3

    const-string v12, "In-app billing warning: BUG: empty/null token!"

    .line 843
    invoke-static {v12}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;)V

    .line 844
    new-array v12, v2, [Ljava/lang/Object;

    const-string v14, "Purchase data: "

    aput-object v14, v12, v5

    aput-object v11, v12, v4

    invoke-static {v12}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    :cond_3
    move-object/from16 v14, p1

    .line 848
    invoke-virtual {v14, v13}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addPurchase(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_2

    :cond_4
    move-object/from16 v14, p1

    const-string v10, "In-app billing warning: Purchase signature verification **FAILED**. Not adding item."

    .line 850
    invoke-static {v10}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;)V

    .line 851
    new-array v10, v2, [Ljava/lang/Object;

    const-string v13, "   Purchase data: "

    aput-object v13, v10, v5

    aput-object v11, v10, v4

    invoke-static {v10}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 852
    new-array v10, v2, [Ljava/lang/Object;

    const-string v11, "   Signature: "

    aput-object v11, v10, v5

    aput-object v12, v10, v4

    invoke-static {v10}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const/4 v10, 0x1

    :goto_2
    add-int/lit8 v6, v6, 0x1

    goto/16 :goto_1

    :cond_5
    move-object/from16 v14, p1

    const-string v6, "INAPP_CONTINUATION_TOKEN"

    .line 857
    invoke-virtual {v3, v6}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 858
    new-array v6, v2, [Ljava/lang/Object;

    const-string v7, "Continuation token: "

    aput-object v7, v6, v5

    aput-object v3, v6, v4

    invoke-static {v6}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 859
    invoke-static {v3}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v6

    if-eqz v6, :cond_7

    if-eqz v10, :cond_6

    const/16 v5, -0x3eb

    :cond_6
    return v5

    :cond_7
    move v6, v10

    goto/16 :goto_0

    :cond_8
    :goto_3
    const-string v1, "In-app billing error: Bundle returned from getPurchases() doesn\'t contain required fields."

    .line 823
    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    const/16 v1, -0x3ea

    return v1
.end method

.method querySkuDetails(Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/util/List;)I
    .locals 9
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/Inventory;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lorg/onepf/oms/appstore/googleUtils/Inventory;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)I"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;,
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "querySkuDetails() Querying SKU details."

    .line 869
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 870
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    .line 871
    iget-object v1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mAppstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v1}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v1

    .line 873
    new-instance v2, Ljava/util/TreeSet;

    invoke-direct {v2}, Ljava/util/TreeSet;-><init>()V

    .line 874
    invoke-virtual {p2, p1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getAllOwnedSkus(Ljava/lang/String;)Ljava/util/List;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_0

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 875
    invoke-virtual {v0, v1, v4}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v2, v4}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    if-eqz p3, :cond_1

    .line 878
    invoke-interface {p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p3

    :goto_1
    invoke-interface {p3}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {p3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 879
    invoke-virtual {v0, v1, v3}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v2, v3}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 882
    :cond_1
    invoke-interface {v2}, Ljava/util/Set;->isEmpty()Z

    move-result p3

    const/4 v0, 0x0

    if-eqz p3, :cond_2

    const-string p1, "querySkuDetails(): nothing to do because there are no SKUs."

    .line 883
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return v0

    .line 888
    :cond_2
    new-instance p3, Ljava/util/ArrayList;

    invoke-direct {p3}, Ljava/util/ArrayList;-><init>()V

    .line 889
    new-instance v3, Ljava/util/ArrayList;

    const/16 v4, 0x1e

    invoke-direct {v3, v4}, Ljava/util/ArrayList;-><init>(I)V

    .line 891
    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v5

    const/4 v6, 0x0

    :cond_3
    :goto_2
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    const/4 v8, 0x1

    if-eqz v7, :cond_5

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    .line 892
    invoke-virtual {v3, v7}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    add-int/2addr v6, v8

    .line 894
    invoke-virtual {v3}, Ljava/util/ArrayList;->size()I

    move-result v7

    if-eq v7, v4, :cond_4

    invoke-interface {v2}, Ljava/util/Set;->size()I

    move-result v7

    if-ne v6, v7, :cond_3

    .line 895
    :cond_4
    invoke-virtual {p3, v3}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 896
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3, v4}, Ljava/util/ArrayList;-><init>(I)V

    goto :goto_2

    :cond_5
    const/4 v2, 0x4

    .line 900
    new-array v2, v2, [Ljava/lang/Object;

    const-string v3, "querySkuDetails() batches: "

    aput-object v3, v2, v0

    invoke-virtual {p3}, Ljava/util/ArrayList;->size()I

    move-result v3

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    aput-object v3, v2, v8

    const-string v3, ", "

    const/4 v4, 0x2

    aput-object v3, v2, v4

    const/4 v3, 0x3

    aput-object p3, v2, v3

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 902
    invoke-virtual {p3}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p3

    :cond_6
    invoke-interface {p3}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_a

    invoke-interface {p3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/ArrayList;

    .line 903
    new-instance v3, Landroid/os/Bundle;

    invoke-direct {v3}, Landroid/os/Bundle;-><init>()V

    const-string v5, "ITEM_ID_LIST"

    .line 904
    invoke-virtual {v3, v5, v2}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    .line 905
    iget-object v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    const/16 v5, -0x3ea

    if-nez v2, :cond_7

    const-string p1, "In-app billing error: unable to get sku details: service is not connected."

    .line 906
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    return v5

    .line 909
    :cond_7
    iget-object v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    iget-object v6, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mContext:Landroid/content/Context;

    invoke-virtual {v6}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v2, v8, v6, p1, v3}, Lcom/skubit/android/billing/IBillingService;->getSkuDetails(ILjava/lang/String;Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v2

    const-string v3, "DETAILS_LIST"

    .line 911
    invoke-virtual {v2, v3}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_9

    .line 912
    invoke-virtual {p0, v2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getResponseCodeFromBundle(Landroid/os/Bundle;)I

    move-result p1

    if-eqz p1, :cond_8

    .line 914
    new-array p2, v4, [Ljava/lang/Object;

    const-string p3, "getSkuDetails() failed: "

    aput-object p3, p2, v0

    invoke-static {p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object p3

    aput-object p3, p2, v8

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return p1

    :cond_8
    const-string p1, "In-app billing error: getSkuDetails() returned a bundle with neither an error nor a detail list."

    .line 917
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    return v5

    :cond_9
    const-string v3, "DETAILS_LIST"

    .line 922
    invoke-virtual {v2, v3}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v2

    .line 924
    invoke-virtual {v2}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_3
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_6

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 925
    new-instance v5, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-direct {v5, p1, v3}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 926
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v3

    invoke-virtual {v5}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v3, v1, v6}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v5, v3}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->setSku(Ljava/lang/String;)V

    .line 927
    new-array v3, v4, [Ljava/lang/Object;

    const-string v6, "querySkuDetails() Got sku details: "

    aput-object v6, v3, v0

    aput-object v5, v3, v8

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 928
    invoke-virtual {p2, v5}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V

    goto :goto_3

    :cond_a
    return v0
.end method

.method public setSetupDone(Z)V
    .locals 0

    .line 294
    iput-boolean p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSetupDone:Z

    return-void
.end method

.method public setSubscriptionsSupported(Z)V
    .locals 0

    .line 290
    iput-boolean p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSubscriptionsSupported:Z

    return-void
.end method

.method public startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 3
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 176
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSetupDone:Z

    if-nez v0, :cond_2

    const-string v0, "Starting in-app billing setup."

    .line 179
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 180
    new-instance v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;

    invoke-direct {v0, p0, p1}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;-><init>(Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    .line 233
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getServiceIntent()Landroid/content/Intent;

    move-result-object v0

    .line 234
    iget-object v1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mContext:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->queryIntentServices(Landroid/content/Intent;I)Ljava/util/List;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 235
    invoke-interface {v1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_0

    .line 237
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mContext:Landroid/content/Context;

    iget-object v1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    const/4 v2, 0x1

    invoke-virtual {p1, v0, v1, v2}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    goto :goto_0

    :cond_0
    if-eqz p1, :cond_1

    .line 241
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v1, 0x3

    const-string v2, "Billing service unavailable on device."

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_1
    :goto_0
    return-void

    .line 176
    :cond_2
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "IAB helper is already set up."

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public subscriptionsSupported()Z
    .locals 1

    .line 286
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSubscriptionsSupported:Z

    return v0
.end method
