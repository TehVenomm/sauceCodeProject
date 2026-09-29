.class Lorg/onepf/oms/OpenIabHelper$17;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper;->queryInventoryAsync(ZLjava/util/List;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;

.field final synthetic val$moreItemSkus:Ljava/util/List;

.field final synthetic val$moreSubsSkus:Ljava/util/List;

.field final synthetic val$querySkuDetails:Z


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;ZLjava/util/List;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;)V
    .locals 0

    .line 1418
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$17;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-boolean p2, p0, Lorg/onepf/oms/OpenIabHelper$17;->val$querySkuDetails:Z

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$17;->val$moreItemSkus:Ljava/util/List;

    iput-object p4, p0, Lorg/onepf/oms/OpenIabHelper$17;->val$moreSubsSkus:Ljava/util/List;

    iput-object p5, p0, Lorg/onepf/oms/OpenIabHelper$17;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 6

    const/4 v0, 0x0

    .line 1423
    :try_start_0
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$17;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iget-boolean v2, p0, Lorg/onepf/oms/OpenIabHelper$17;->val$querySkuDetails:Z

    iget-object v3, p0, Lorg/onepf/oms/OpenIabHelper$17;->val$moreItemSkus:Ljava/util/List;

    iget-object v4, p0, Lorg/onepf/oms/OpenIabHelper$17;->val$moreSubsSkus:Ljava/util/List;

    invoke-virtual {v1, v2, v3, v4}, Lorg/onepf/oms/OpenIabHelper;->queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

    move-result-object v1
    :try_end_0
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_0 .. :try_end_0} :catch_1

    .line 1424
    :try_start_1
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v2, 0x0

    const-string v3, "Inventory refresh successful."

    invoke-direct {v0, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V
    :try_end_1
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    move-object v5, v1

    move-object v1, v0

    move-object v0, v5

    goto :goto_0

    :catch_1
    move-exception v1

    .line 1426
    :goto_0
    invoke-virtual {v1}, Lorg/onepf/oms/appstore/googleUtils/IabException;->getResult()Lorg/onepf/oms/appstore/googleUtils/IabResult;

    move-result-object v2

    const-string v3, "queryInventoryAsync() Error : "

    .line 1427
    invoke-static {v3, v1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object v1, v0

    move-object v0, v2

    .line 1432
    :goto_1
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$17;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2}, Lorg/onepf/oms/OpenIabHelper;->access$1800(Lorg/onepf/oms/OpenIabHelper;)Landroid/os/Handler;

    move-result-object v2

    new-instance v3, Lorg/onepf/oms/OpenIabHelper$17$1;

    invoke-direct {v3, p0, v0, v1}, Lorg/onepf/oms/OpenIabHelper$17$1;-><init>(Lorg/onepf/oms/OpenIabHelper$17;Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V

    invoke-virtual {v2, v3}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
