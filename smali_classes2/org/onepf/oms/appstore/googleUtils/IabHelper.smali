.class public Lorg/onepf/oms/appstore/googleUtils/IabHelper;
.super Ljava/lang/Object;
.source "IabHelper.java"

# interfaces
.implements Lorg/onepf/oms/AppstoreInAppBillingService;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;,
        Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;,
        Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;,
        Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;,
        Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
    }
.end annotation


# static fields
.field public static final BILLING_RESPONSE_RESULT_BILLING_UNAVAILABLE:I = 0x3

.field public static final BILLING_RESPONSE_RESULT_DEVELOPER_ERROR:I = 0x5

.field public static final BILLING_RESPONSE_RESULT_ERROR:I = 0x6

.field public static final BILLING_RESPONSE_RESULT_ITEM_ALREADY_OWNED:I = 0x7

.field public static final BILLING_RESPONSE_RESULT_ITEM_NOT_OWNED:I = 0x8

.field public static final BILLING_RESPONSE_RESULT_ITEM_UNAVAILABLE:I = 0x4

.field public static final BILLING_RESPONSE_RESULT_OK:I = 0x0

.field public static final BILLING_RESPONSE_RESULT_USER_CANCELED:I = 0x1

.field public static final GET_SKU_DETAILS_ITEM_LIST:Ljava/lang/String; = "ITEM_ID_LIST"

.field public static final GET_SKU_DETAILS_ITEM_TYPE_LIST:Ljava/lang/String; = "ITEM_TYPE_LIST"

.field public static final IABHELPER_BAD_RESPONSE:I = -0x3ea

.field public static final IABHELPER_ERROR_BASE:I = -0x3e8

.field public static final IABHELPER_INVALID_CONSUMPTION:I = -0x3f2

.field public static final IABHELPER_MISSING_TOKEN:I = -0x3ef

.field public static final IABHELPER_REMOTE_EXCEPTION:I = -0x3e9

.field public static final IABHELPER_SEND_INTENT_FAILED:I = -0x3ec

.field public static final IABHELPER_SUBSCRIPTIONS_NOT_AVAILABLE:I = -0x3f1

.field public static final IABHELPER_UNKNOWN_ERROR:I = -0x3f0

.field public static final IABHELPER_UNKNOWN_PURCHASE_RESPONSE:I = -0x3ee

.field public static final IABHELPER_USER_CANCELLED:I = -0x3ed

.field public static final IABHELPER_VERIFICATION_FAILED:I = -0x3eb

.field public static final INAPP_CONTINUATION_TOKEN:Ljava/lang/String; = "INAPP_CONTINUATION_TOKEN"

.field public static final ITEM_TYPE_INAPP:Ljava/lang/String; = "inapp"

.field public static final ITEM_TYPE_SUBS:Ljava/lang/String; = "subs"

.field public static final QUERY_SKU_DETAILS_BATCH_SIZE:I = 0x14

.field public static final RESPONSE_BUY_INTENT:Ljava/lang/String; = "BUY_INTENT"

.field public static final RESPONSE_CODE:Ljava/lang/String; = "RESPONSE_CODE"

.field public static final RESPONSE_GET_SKU_DETAILS_LIST:Ljava/lang/String; = "DETAILS_LIST"

.field public static final RESPONSE_INAPP_ITEM_LIST:Ljava/lang/String; = "INAPP_PURCHASE_ITEM_LIST"

.field public static final RESPONSE_INAPP_PURCHASE_DATA:Ljava/lang/String; = "INAPP_PURCHASE_DATA"

.field public static final RESPONSE_INAPP_PURCHASE_DATA_LIST:Ljava/lang/String; = "INAPP_PURCHASE_DATA_LIST"

.field public static final RESPONSE_INAPP_SIGNATURE:Ljava/lang/String; = "INAPP_DATA_SIGNATURE"

.field public static final RESPONSE_INAPP_SIGNATURE_LIST:Ljava/lang/String; = "INAPP_DATA_SIGNATURE_LIST"


# instance fields
.field private appstore:Lorg/onepf/oms/Appstore;

.field componentName:Landroid/content/ComponentName;

.field mAsyncInProgress:Z

.field mAsyncOperation:Ljava/lang/String;

.field mContext:Landroid/content/Context;

.field mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field mPurchasingItemType:Ljava/lang/String;

.field mRequestCode:I

.field mService:Lcom/android/vending/billing/IInAppBillingService;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field mServiceConn:Landroid/content/ServiceConnection;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field mSetupDone:Z

.field mSignatureBase64:Ljava/lang/String;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field mSubscriptionsSupported:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;)V
    .locals 1
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 186
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 90
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSetupDone:Z

    .line 93
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSubscriptionsSupported:Z

    .line 97
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncInProgress:Z

    const-string v0, ""

    .line 101
    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncOperation:Ljava/lang/String;

    const/4 v0, 0x0

    .line 124
    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSignatureBase64:Ljava/lang/String;

    .line 187
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mContext:Landroid/content/Context;

    .line 188
    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSignatureBase64:Ljava/lang/String;

    .line 189
    iput-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->appstore:Lorg/onepf/oms/Appstore;

    const-string p1, "IAB helper created."

    .line 190
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return-void
.end method

.method public static getResponseDesc(I)Ljava/lang/String;
    .locals 3

    const-string v0, "0:OK/1:User Canceled/2:Unknown/3:Billing Unavailable/4:Item unavailable/5:Developer Error/6:Error/7:Item Already Owned/8:Item not owned"

    const-string v1, "/"

    .line 791
    invoke-virtual {v0, v1}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v0

    const-string v1, "0:OK/-1001:Remote exception during initialization/-1002:Bad response received/-1003:Purchase signature verification failed/-1004:Send intent failed/-1005:User cancelled/-1006:Unknown purchase response/-1007:Missing token/-1008:Unknown error/-1009:Subscriptions not available/-1010:Invalid consumption attempt"

    const-string v2, "/"

    .line 795
    invoke-virtual {v1, v2}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v1

    const/16 v2, -0x3e8

    if-gt p0, v2, :cond_1

    sub-int/2addr v2, p0

    if-ltz v2, :cond_0

    .line 808
    array-length v0, v1

    if-ge v2, v0, :cond_0

    aget-object p0, v1, v2

    return-object p0

    .line 809
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {p0}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, ":Unknown IAB Helper Error"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_1
    if-ltz p0, :cond_3

    .line 810
    array-length v1, v0

    if-lt p0, v1, :cond_2

    goto :goto_0

    .line 813
    :cond_2
    aget-object p0, v0, p0

    return-object p0

    .line 811
    :cond_3
    :goto_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {p0}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, ":Unknown"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
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

    .line 692
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 694
    iget-object v0, p1, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mItemType:Ljava/lang/String;

    const-string v1, "inapp"

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 700
    :try_start_0
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getToken()Ljava/lang/String;

    move-result-object v0

    .line 701
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x2

    const/4 v3, 0x1

    const/4 v4, 0x0

    const/4 v5, 0x3

    if-eqz v0, :cond_2

    const-string v6, ""

    .line 702
    invoke-virtual {v0, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-nez v6, :cond_2

    const/4 v6, 0x4

    .line 708
    new-array v7, v6, [Ljava/lang/Object;

    const-string v8, "Consuming sku: "

    aput-object v8, v7, v4

    aput-object v1, v7, v3

    const-string v8, ", token: "

    aput-object v8, v7, v2

    aput-object v0, v7, v5

    invoke-static {v7}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 709
    iget-object v7, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    if-eqz v7, :cond_1

    .line 713
    iget-object v7, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getPackageName()Ljava/lang/String;

    move-result-object v8

    invoke-interface {v7, v5, v8, v0}, Lcom/android/vending/billing/IInAppBillingService;->consumePurchase(ILjava/lang/String;Ljava/lang/String;)I

    move-result v0

    if-nez v0, :cond_0

    .line 715
    new-array v0, v2, [Ljava/lang/Object;

    const-string v2, "Successfully consumed sku: "

    aput-object v2, v0, v4

    aput-object v1, v0, v3

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-void

    .line 717
    :cond_0
    new-array v6, v6, [Ljava/lang/Object;

    const-string v7, "Error consuming consuming sku "

    aput-object v7, v6, v4

    aput-object v1, v6, v3

    const-string v3, ". "

    aput-object v3, v6, v2

    invoke-static {v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object v2

    aput-object v2, v6, v5

    invoke-static {v6}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 718
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

    .line 710
    :cond_1
    new-array v0, v5, [Ljava/lang/Object;

    const-string v5, "Error consuming consuming sku "

    aput-object v5, v0, v4

    aput-object v1, v0, v3

    const-string v3, ". Service is not connected."

    aput-object v3, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 711
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

    .line 703
    :cond_2
    new-array v0, v5, [Ljava/lang/Object;

    const-string v5, "In-app billing error: Can\'t consume "

    aput-object v5, v0, v4

    aput-object v1, v0, v3

    const-string v3, ". No token."

    aput-object v3, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 704
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

    .line 721
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

    .line 695
    :cond_3
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v1, -0x3f2

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Items of type \'"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object p1, p1, Lorg/onepf/oms/appstore/googleUtils/Purchase;->mItemType:Ljava/lang/String;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "\' can\'t be consumed."

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, v1, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw v0
.end method

.method public consumeAsync(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V
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
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;",
            ")V"
        }
    .end annotation

    const-string v0, "consume"

    .line 779
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->checkSetupDone(Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 780
    invoke-virtual {p0, p1, v0, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V

    return-void
.end method

.method public consumeAsync(Lorg/onepf/oms/appstore/googleUtils/Purchase;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;)V
    .locals 1

    const-string v0, "consume"

    .line 765
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 766
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 767
    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const/4 p1, 0x0

    .line 768
    invoke-virtual {p0, v0, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V

    return-void
.end method

.method consumeAsyncInternal(Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V
    .locals 8
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .param p3    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/appstore/googleUtils/Purchase;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;",
            ")V"
        }
    .end annotation

    .line 1027
    new-instance v4, Landroid/os/Handler;

    invoke-direct {v4}, Landroid/os/Handler;-><init>()V

    const-string v0, "consume"

    .line 1028
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->flagStartAsync(Ljava/lang/String;)V

    .line 1029
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lorg/onepf/oms/appstore/googleUtils/IabHelper$3;

    move-object v0, v7

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v5, p3

    invoke-direct/range {v0 .. v5}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$3;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabHelper;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;Landroid/os/Handler;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    return-void
.end method

.method public dispose()V
    .locals 2

    const-string v0, "Disposing."

    .line 312
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 313
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSetupDone:Z

    .line 314
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    if-eqz v0, :cond_1

    const-string v0, "Unbinding from service."

    .line 315
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 316
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mContext:Landroid/content/Context;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mContext:Landroid/content/Context;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    invoke-virtual {v0, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    :cond_0
    const/4 v0, 0x0

    .line 317
    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    .line 318
    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    .line 319
    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    :cond_1
    return-void
.end method

.method flagEndAsync()V
    .locals 4

    const/4 v0, 0x2

    .line 882
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Ending async operation: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncOperation:Ljava/lang/String;

    const/4 v3, 0x1

    aput-object v1, v0, v3

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const-string v0, ""

    .line 883
    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncOperation:Ljava/lang/String;

    .line 884
    iput-boolean v2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncInProgress:Z

    return-void
.end method

.method flagStartAsync(Ljava/lang/String;)V
    .locals 4

    .line 874
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncInProgress:Z

    if-nez v0, :cond_0

    .line 876
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncOperation:Ljava/lang/String;

    const/4 v0, 0x1

    .line 877
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncInProgress:Z

    const/4 v1, 0x2

    .line 878
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "Starting async operation: "

    aput-object v3, v1, v2

    aput-object p1, v1, v0

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-void

    .line 874
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can\'t start async operation ("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, ") because another async operation("

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mAsyncOperation:Ljava/lang/String;

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

    .line 726
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mContext:Landroid/content/Context;

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

    .line 845
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    const/4 v0, 0x0

    if-nez p1, :cond_0

    const-string p1, "Bundle with null response code, assuming OK (known issue)"

    .line 847
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return v0

    .line 849
    :cond_0
    instance-of v1, p1, Ljava/lang/Integer;

    if-eqz v1, :cond_1

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    return p1

    .line 850
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

    .line 852
    new-array v2, v1, [Ljava/lang/Object;

    const-string v3, "In-app billing error: "

    aput-object v3, v2, v0

    const/4 v3, 0x1

    const-string v4, "Unexpected type for bundle response code."

    aput-object v4, v2, v3

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 853
    new-array v1, v1, [Ljava/lang/Object;

    const-string v2, "In-app billing error: "

    aput-object v2, v1, v0

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v1, v3

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 854
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

    .line 860
    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p1

    const-string v0, "RESPONSE_CODE"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    const/4 v0, 0x0

    if-nez p1, :cond_0

    const-string p1, "In-app billing error: Intent with no response code, assuming OK (known issue)"

    .line 862
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    return v0

    .line 864
    :cond_0
    instance-of v1, p1, Ljava/lang/Integer;

    if-eqz v1, :cond_1

    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    return p1

    .line 865
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

    .line 867
    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    const/4 v1, 0x2

    .line 868
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

    .line 869
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

.method protected getServiceFromBinder(Landroid/os/IBinder;)Lcom/android/vending/billing/IInAppBillingService;
    .locals 0
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 302
    invoke-static {p1}, Lcom/android/vending/billing/IInAppBillingService$Stub;->asInterface(Landroid/os/IBinder;)Lcom/android/vending/billing/IInAppBillingService;

    move-result-object p1

    return-object p1
.end method

.method protected getServiceIntent()Landroid/content/Intent;
    .locals 2

    .line 292
    new-instance v0, Landroid/content/Intent;

    const-string v1, "com.android.vending.billing.InAppBillingService.BIND"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v1, "com.android.vending"

    .line 293
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    return-object v0
.end method

.method public handleActivityResult(IILandroid/content/Intent;)Z
    .locals 6
    .param p3    # Landroid/content/Intent;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 466
    iget v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mRequestCode:I

    const/4 v1, 0x0

    if-eq p1, v0, :cond_0

    return v1

    :cond_0
    const-string p1, "handleActivityResult"

    .line 468
    invoke-virtual {p0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 471
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->flagEndAsync()V

    const/4 p1, 0x0

    const/4 v0, 0x1

    if-nez p3, :cond_2

    const-string p2, "In-app billing error: Null data in IAB activity result."

    .line 474
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 475
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3ea

    const-string v1, "Null data in IAB result"

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 476
    iget-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_1

    iget-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_1
    return v0

    .line 480
    :cond_2
    invoke-virtual {p0, p3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseCodeFromIntent(Landroid/content/Intent;)I

    move-result v2

    const-string v3, "INAPP_PURCHASE_DATA"

    .line 481
    invoke-virtual {p3, v3}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v4, "INAPP_DATA_SIGNATURE"

    .line 482
    invoke-virtual {p3, v4}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const/4 v5, -0x1

    if-ne p2, v5, :cond_3

    if-nez v2, :cond_3

    const-string p1, "Purchase successful."

    .line 485
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 486
    invoke-virtual {p0, p3, v3, v4}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->processPurchaseSuccess(Landroid/content/Intent;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_3
    const/4 p3, 0x2

    if-ne p2, v5, :cond_4

    .line 488
    new-array p1, p3, [Ljava/lang/Object;

    const-string p2, "Purchase canceled - Response: "

    aput-object p2, p1, v1

    invoke-static {v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object p2

    aput-object p2, p1, v0

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 490
    invoke-virtual {p0, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->processPurchaseFail(I)V

    goto :goto_0

    :cond_4
    if-nez p2, :cond_5

    .line 492
    new-array p2, p3, [Ljava/lang/Object;

    const-string p3, "Purchase canceled - Response: "

    aput-object p3, p2, v1

    invoke-static {v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object p3

    aput-object p3, p2, v0

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 493
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3ed

    const-string v1, "User canceled."

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 494
    iget-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_6

    iget-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_0

    .line 496
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

    .line 498
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3ee

    const-string v1, "Unknown purchase response."

    invoke-direct {p2, p3, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 499
    iget-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_6

    iget-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_6
    :goto_0
    return v0
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

    .line 1063
    :cond_0
    invoke-static {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/Security;->verifyPurchase(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_1

    const-string p2, "In-app billing warning: Purchase signature verification **FAILED**."

    .line 1065
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

    .line 360
    invoke-virtual/range {v0 .. v5}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

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

    .line 365
    invoke-virtual/range {v0 .. v6}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

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

    .line 398
    invoke-virtual {v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->checkSetupDone(Ljava/lang/String;)V

    const-string v2, "launchPurchaseFlow"

    .line 399
    invoke-virtual {v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->flagStartAsync(Ljava/lang/String;)V

    const-string v2, "subs"

    .line 402
    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    const/4 v10, 0x0

    if-eqz v2, :cond_1

    iget-boolean v2, v1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSubscriptionsSupported:Z

    if-nez v2, :cond_1

    .line 403
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v2, -0x3f1

    const-string v3, "Subscriptions are not available."

    invoke-direct {v0, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    const-string v2, "Subscriptions are not available."

    .line 405
    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    if-eqz v9, :cond_0

    .line 406
    invoke-interface {v9, v0, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    .line 407
    :cond_0
    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->flagEndAsync()V

    return-void

    :cond_1
    const/4 v11, 0x4

    .line 412
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

    .line 413
    iget-object v2, v1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    if-nez v2, :cond_3

    .line 414
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v2, 0x6

    const-string v3, "Unable to buy item"

    invoke-direct {v0, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    const-string v2, "In-app billing error: Unable to buy item, Error response: service is not connected."

    .line 415
    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    if-eqz v9, :cond_2

    .line 416
    invoke-interface {v9, v0, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    .line 417
    :cond_2
    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->flagEndAsync()V

    return-void

    .line 420
    :cond_3
    iget-object v2, v1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    const/4 v3, 0x3

    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getPackageName()Ljava/lang/String;

    move-result-object v4

    move-object/from16 v5, p2

    move-object/from16 v6, p3

    move-object/from16 v7, p6

    invoke-interface/range {v2 .. v7}, Lcom/android/vending/billing/IInAppBillingService;->getBuyIntent(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v2

    .line 421
    invoke-virtual {v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseCodeFromBundle(Landroid/os/Bundle;)I

    move-result v3

    if-eqz v3, :cond_5

    .line 423
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v2, "Unable to buy item"

    invoke-direct {v0, v3, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 424
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "In-app billing error: Unable to buy item, Error response: "

    invoke-virtual {v2, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    if-eqz v9, :cond_4

    .line 425
    invoke-interface {v9, v0, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    .line 426
    :cond_4
    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->flagEndAsync()V

    return-void

    :cond_5
    const-string v3, "BUY_INTENT"

    .line 430
    invoke-virtual {v2, v3}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v2

    check-cast v2, Landroid/app/PendingIntent;

    .line 431
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

    .line 432
    iput v3, v1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mRequestCode:I

    .line 433
    iput-object v9, v1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    .line 434
    iput-object v0, v1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchasingItemType:Ljava/lang/String;

    .line 435
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

    .line 444
    new-instance v2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v3, -0x3e9

    const-string v4, "Remote exception while starting purchase flow"

    invoke-direct {v2, v3, v4}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 445
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "In-app billing error: RemoteException while launching purchase flow for sku "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v3, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    if-eqz v9, :cond_6

    .line 446
    invoke-interface {v9, v2, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_0

    :catch_1
    move-exception v0

    .line 440
    new-instance v2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v3, -0x3ec

    const-string v4, "Failed to send intent."

    invoke-direct {v2, v3, v4}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 441
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "In-app billing error: SendIntentException while launching purchase flow for sku "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v3, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    if-eqz v9, :cond_6

    .line 442
    invoke-interface {v9, v2, v10}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    .line 448
    :cond_6
    :goto_0
    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->flagEndAsync()V

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

    .line 370
    invoke-virtual/range {v0 .. v5}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->launchSubscriptionPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

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

    .line 375
    invoke-virtual/range {v0 .. v6}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    return-void
.end method

.method public processPurchaseFail(I)V
    .locals 3

    const/4 v0, 0x2

    .line 506
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Result code was OK but in-app billing response was not OK: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    invoke-static {p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x1

    aput-object v1, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 507
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz v0, :cond_0

    .line 508
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v1, "Problem purchashing item."

    invoke-direct {v0, p1, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 509
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

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

    .line 515
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    const/4 v0, 0x2

    .line 516
    new-array v1, v0, [Ljava/lang/Object;

    const-string v2, "Purchase data: "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    const/4 v2, 0x1

    aput-object p2, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 517
    new-array v1, v0, [Ljava/lang/Object;

    const-string v4, "Data signature: "

    aput-object v4, v1, v3

    aput-object p3, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 518
    new-array v1, v0, [Ljava/lang/Object;

    const-string v4, "Extras: "

    aput-object v4, v1, v3

    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v4

    aput-object v4, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 519
    new-array v1, v0, [Ljava/lang/Object;

    const-string v4, "Expected item type: "

    aput-object v4, v1, v3

    iget-object v4, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchasingItemType:Ljava/lang/String;

    aput-object v4, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const/4 v1, 0x0

    if-eqz p2, :cond_5

    if-nez p3, :cond_0

    goto/16 :goto_0

    .line 531
    :cond_0
    :try_start_0
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchasingItemType:Ljava/lang/String;

    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v2}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v2

    invoke-direct {p1, v0, p2, p3, v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 532
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v0

    .line 533
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v2

    iget-object v4, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v4}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v4, v0}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 535
    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSignatureBase64:Ljava/lang/String;

    invoke-virtual {p0, v2, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->isValidDataSignature(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p2

    if-nez p2, :cond_2

    .line 536
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string p3, "In-app billing error: Purchase signature verification FAILED for sku "

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 537
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

    .line 538
    iget-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p3, :cond_1

    .line 539
    iget-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p3, p2, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_1
    return-void

    :cond_2
    const-string p2, "Purchase signature successfully verified."

    .line 543
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 552
    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p2, :cond_3

    .line 553
    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "Success"

    invoke-direct {p3, v3, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p2, p3, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_3
    return-void

    :catch_0
    move-exception p1

    const-string p2, "In-app billing error: Failed to parse purchase data."

    .line 545
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 546
    invoke-virtual {p1}, Lorg/json/JSONException;->printStackTrace()V

    .line 547
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p2, -0x3ea

    const-string p3, "Failed to parse purchase data."

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 548
    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p2, :cond_4

    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    invoke-interface {p2, p1, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_4
    return-void

    :cond_5
    :goto_0
    const-string p2, "In-app billing error: BUG: either purchaseData or dataSignature is null."

    .line 522
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 523
    new-array p2, v0, [Ljava/lang/Object;

    const-string p3, "Extras: "

    aput-object p3, p2, v3

    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p1

    aput-object p1, p2, v2

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 524
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p2, -0x3f0

    const-string p3, "IAB returned null purchaseData or dataSignature"

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 525
    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    if-eqz p2, :cond_6

    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mPurchaseListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

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

    .line 559
    invoke-virtual {p0, p1, p2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

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

    .line 577
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->checkSetupDone(Ljava/lang/String;)V

    .line 579
    :try_start_0
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;-><init>()V

    const-string v1, "inapp"

    .line 580
    invoke-virtual {p0, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->queryPurchases(Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/lang/String;)I

    move-result v1

    if-nez v1, :cond_5

    if-eqz p1, :cond_1

    const-string v1, "inapp"

    .line 586
    invoke-virtual {p0, v1, v0, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->querySkuDetails(Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/util/List;)I

    move-result p2

    if-nez p2, :cond_0

    goto :goto_0

    .line 588
    :cond_0
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p3, "Error refreshing inventory (querying prices of items)."

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p1

    .line 593
    :cond_1
    :goto_0
    iget-boolean p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSubscriptionsSupported:Z

    if-eqz p2, :cond_4

    const-string p2, "subs"

    .line 594
    invoke-virtual {p0, v0, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->queryPurchases(Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/lang/String;)I

    move-result p2

    if-nez p2, :cond_3

    if-eqz p1, :cond_4

    const-string p1, "subs"

    .line 600
    invoke-virtual {p0, p1, v0, p3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->querySkuDetails(Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/Inventory;Ljava/util/List;)I

    move-result p1

    if-nez p1, :cond_2

    goto :goto_1

    .line 602
    :cond_2
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p3, "Error refreshing inventory (querying prices of subscriptions)."

    invoke-direct {p2, p1, p3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p2

    .line 596
    :cond_3
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const-string p3, "Error refreshing inventory (querying owned subscriptions)."

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p1

    :cond_4
    :goto_1
    return-object v0

    .line 582
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

    .line 611
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p3, -0x3ea

    const-string v0, "Error parsing JSON response while refreshing inventory."

    invoke-direct {p2, p3, v0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p2

    :catch_1
    move-exception p1

    .line 609
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 p3, -0x3e9

    const-string v0, "Remote exception while refreshing inventory."

    invoke-direct {p2, p3, v0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;Ljava/lang/Exception;)V

    throw p2
.end method

.method public queryInventoryAsync(Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 670
    invoke-virtual {p0, v0, v1, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method

.method public queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
    .locals 8
    .param p3    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;",
            ")V"
        }
    .end annotation

    .line 642
    new-instance v4, Landroid/os/Handler;

    invoke-direct {v4}, Landroid/os/Handler;-><init>()V

    const-string v0, "queryInventory"

    .line 643
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->checkSetupDone(Ljava/lang/String;)V

    const-string v0, "refresh inventory"

    .line 644
    invoke-virtual {p0, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->flagStartAsync(Ljava/lang/String;)V

    .line 645
    new-instance v6, Ljava/lang/Thread;

    new-instance v7, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2;

    move-object v0, v7

    move-object v1, p0

    move v2, p1

    move-object v3, p2

    move-object v5, p3

    invoke-direct/range {v0 .. v5}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabHelper;ZLjava/util/List;Landroid/os/Handler;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

    invoke-direct {v6, v7}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    invoke-virtual {v6}, Ljava/lang/Thread;->start()V

    return-void
.end method

.method public queryInventoryAsync(ZLorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
    .locals 1
    .param p2    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x0

    .line 674
    invoke-virtual {p0, p1, v0, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V

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

    .line 890
    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "Querying owned items, item type: "

    const/4 v5, 0x0

    aput-object v4, v3, v5

    const/4 v4, 0x1

    aput-object v1, v3, v4

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 891
    new-array v3, v2, [Ljava/lang/Object;

    const-string v6, "Package name: "

    aput-object v6, v3, v5

    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getPackageName()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v3, v4

    invoke-static {v3}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const/4 v3, 0x0

    const/4 v6, 0x0

    .line 896
    :goto_0
    new-array v7, v2, [Ljava/lang/Object;

    const-string v8, "Calling getPurchases with continuation token: "

    aput-object v8, v7, v5

    aput-object v3, v7, v4

    invoke-static {v7}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 897
    iget-object v7, v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    if-nez v7, :cond_0

    const-string v1, "getPurchases() failed: service is not connected."

    .line 898
    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    const/4 v1, 0x6

    return v1

    .line 901
    :cond_0
    iget-object v7, v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    const/4 v8, 0x3

    invoke-virtual/range {p0 .. p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getPackageName()Ljava/lang/String;

    move-result-object v9

    invoke-interface {v7, v8, v9, v1, v3}, Lcom/android/vending/billing/IInAppBillingService;->getPurchases(ILjava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v3

    .line 903
    invoke-virtual {v0, v3}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseCodeFromBundle(Landroid/os/Bundle;)I

    move-result v7

    .line 904
    new-array v8, v2, [Ljava/lang/Object;

    const-string v9, "Owned items response: "

    aput-object v9, v8, v5

    invoke-static {v7}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v9

    aput-object v9, v8, v4

    invoke-static {v8}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    if-eqz v7, :cond_1

    .line 906
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

    .line 909
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

    .line 916
    invoke-virtual {v3, v7}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v7

    const-string v8, "INAPP_PURCHASE_DATA_LIST"

    .line 917
    invoke-virtual {v3, v8}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v8

    const-string v9, "INAPP_DATA_SIGNATURE_LIST"

    .line 918
    invoke-virtual {v3, v9}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v9

    move v10, v6

    const/4 v6, 0x0

    .line 920
    :goto_1
    invoke-virtual {v8}, Ljava/util/ArrayList;->size()I

    move-result v11

    if-ge v6, v11, :cond_5

    .line 921
    invoke-virtual {v8, v6}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v11

    check-cast v11, Ljava/lang/String;

    .line 922
    invoke-virtual {v9, v6}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v12

    check-cast v12, Ljava/lang/String;

    .line 923
    invoke-virtual {v7, v6}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v13

    check-cast v13, Ljava/lang/String;

    .line 925
    iget-object v14, v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSignatureBase64:Ljava/lang/String;

    invoke-virtual {v0, v14, v11, v12}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->isValidDataSignature(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result v14

    if-eqz v14, :cond_4

    .line 926
    new-array v14, v2, [Ljava/lang/Object;

    const-string v15, "Sku is owned: "

    aput-object v15, v14, v5

    aput-object v13, v14, v4

    invoke-static {v14}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 927
    new-instance v13, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    iget-object v14, v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v14}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v14

    invoke-direct {v13, v1, v11, v12, v14}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 928
    invoke-virtual {v13}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getSku()Ljava/lang/String;

    move-result-object v12

    .line 929
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v14

    iget-object v15, v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v15}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v15

    invoke-virtual {v14, v15, v12}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v13, v12}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->setSku(Ljava/lang/String;)V

    .line 931
    invoke-virtual {v13}, Lorg/onepf/oms/appstore/googleUtils/Purchase;->getToken()Ljava/lang/String;

    move-result-object v12

    invoke-static {v12}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v12

    if-eqz v12, :cond_3

    const-string v12, "In-app billing warning: BUG: empty/null token!"

    .line 932
    invoke-static {v12}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;)V

    .line 933
    new-array v12, v2, [Ljava/lang/Object;

    const-string v14, "Purchase data: "

    aput-object v14, v12, v5

    aput-object v11, v12, v4

    invoke-static {v12}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    :cond_3
    move-object/from16 v14, p1

    .line 937
    invoke-virtual {v14, v13}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addPurchase(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_2

    :cond_4
    move-object/from16 v14, p1

    const-string v10, "In-app billing warning: Purchase signature verification **FAILED**. Not adding item."

    .line 939
    invoke-static {v10}, Lorg/onepf/oms/util/Logger;->w(Ljava/lang/String;)V

    .line 940
    new-array v10, v2, [Ljava/lang/Object;

    const-string v13, "   Purchase data: "

    aput-object v13, v10, v5

    aput-object v11, v10, v4

    invoke-static {v10}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 941
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

    .line 946
    invoke-virtual {v3, v6}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 947
    new-array v6, v2, [Ljava/lang/Object;

    const-string v7, "Continuation token: "

    aput-object v7, v6, v5

    aput-object v3, v6, v4

    invoke-static {v6}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 948
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

    .line 912
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

    .line 958
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 959
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v0

    .line 960
    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v1}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v1

    .line 962
    new-instance v2, Ljava/util/TreeSet;

    invoke-direct {v2}, Ljava/util/TreeSet;-><init>()V

    .line 963
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

    .line 964
    invoke-virtual {v0, v1, v4}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v2, v4}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    if-eqz p3, :cond_1

    .line 967
    invoke-interface {p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p3

    :goto_1
    invoke-interface {p3}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {p3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 968
    invoke-virtual {v0, v1, v3}, Lorg/onepf/oms/SkuManager;->getStoreSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v2, v3}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 971
    :cond_1
    invoke-interface {v2}, Ljava/util/Set;->isEmpty()Z

    move-result p3

    const/4 v0, 0x0

    if-eqz p3, :cond_2

    const-string p1, "querySkuDetails(): nothing to do because there are no SKUs."

    .line 972
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return v0

    .line 977
    :cond_2
    new-instance p3, Ljava/util/ArrayList;

    invoke-direct {p3}, Ljava/util/ArrayList;-><init>()V

    .line 978
    new-instance v3, Ljava/util/ArrayList;

    const/16 v4, 0x14

    invoke-direct {v3, v4}, Ljava/util/ArrayList;-><init>(I)V

    .line 980
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

    .line 981
    invoke-virtual {v3, v7}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    add-int/2addr v6, v8

    .line 983
    invoke-virtual {v3}, Ljava/util/ArrayList;->size()I

    move-result v7

    if-eq v7, v4, :cond_4

    invoke-interface {v2}, Ljava/util/Set;->size()I

    move-result v7

    if-ne v6, v7, :cond_3

    .line 984
    :cond_4
    invoke-virtual {p3, v3}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 985
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3, v4}, Ljava/util/ArrayList;-><init>(I)V

    goto :goto_2

    :cond_5
    const/4 v2, 0x4

    .line 989
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

    .line 991
    invoke-virtual {p3}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p3

    :cond_6
    invoke-interface {p3}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_a

    invoke-interface {p3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/ArrayList;

    .line 992
    new-instance v5, Landroid/os/Bundle;

    invoke-direct {v5}, Landroid/os/Bundle;-><init>()V

    const-string v6, "ITEM_ID_LIST"

    .line 993
    invoke-virtual {v5, v6, v2}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    .line 994
    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    const/16 v6, -0x3ea

    if-nez v2, :cond_7

    const-string p1, "In-app billing error: unable to get sku details: service is not connected."

    .line 995
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    return v6

    .line 998
    :cond_7
    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    iget-object v7, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mContext:Landroid/content/Context;

    invoke-virtual {v7}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v7

    invoke-interface {v2, v3, v7, p1, v5}, Lcom/android/vending/billing/IInAppBillingService;->getSkuDetails(ILjava/lang/String;Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object v2

    const-string v5, "DETAILS_LIST"

    .line 1000
    invoke-virtual {v2, v5}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v5

    if-nez v5, :cond_9

    .line 1001
    invoke-virtual {p0, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseCodeFromBundle(Landroid/os/Bundle;)I

    move-result p1

    if-eqz p1, :cond_8

    .line 1003
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

    .line 1006
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    return v6

    :cond_9
    const-string v5, "DETAILS_LIST"

    .line 1011
    invoke-virtual {v2, v5}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v2

    .line 1013
    invoke-virtual {v2}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_3
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_6

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    .line 1014
    new-instance v6, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-direct {v6, p1, v5}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    .line 1015
    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v5

    invoke-virtual {v6}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->getSku()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v5, v1, v7}, Lorg/onepf/oms/SkuManager;->getSku(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v6, v5}, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;->setSku(Ljava/lang/String;)V

    .line 1016
    new-array v5, v4, [Ljava/lang/Object;

    const-string v7, "querySkuDetails() Got sku details: "

    aput-object v7, v5, v0

    aput-object v6, v5, v8

    invoke-static {v5}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 1017
    invoke-virtual {p2, v6}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V

    goto :goto_3

    :cond_a
    return v0
.end method

.method public setSetupDone(Z)V
    .locals 0

    .line 335
    iput-boolean p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSetupDone:Z

    return-void
.end method

.method public setSubscriptionsSupported(Z)V
    .locals 0

    .line 331
    iput-boolean p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSubscriptionsSupported:Z

    return-void
.end method

.method public startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 3
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 215
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSetupDone:Z

    if-nez v0, :cond_2

    const-string v0, "Starting in-app billing setup."

    .line 218
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 219
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;

    invoke-direct {v0, p0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    .line 273
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getServiceIntent()Landroid/content/Intent;

    move-result-object v0

    .line 274
    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mContext:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->queryIntentServices(Landroid/content/Intent;I)Ljava/util/List;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 275
    invoke-interface {v1}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_0

    .line 277
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mContext:Landroid/content/Context;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mServiceConn:Landroid/content/ServiceConnection;

    const/4 v2, 0x1

    invoke-virtual {p1, v0, v1, v2}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    goto :goto_0

    :cond_0
    if-eqz p1, :cond_1

    .line 281
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v1, 0x3

    const-string v2, "Billing service unavailable on device."

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    const-string p1, "Billing service unavailable on device."

    .line 283
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    :cond_1
    :goto_0
    return-void

    .line 215
    :cond_2
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "IAB helper is already set up."

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public subscriptionsSupported()Z
    .locals 1

    .line 327
    iget-boolean v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSubscriptionsSupported:Z

    return v0
.end method
