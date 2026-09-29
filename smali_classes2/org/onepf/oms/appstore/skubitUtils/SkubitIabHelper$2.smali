.class Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;
.super Ljava/lang/Object;
.source "SkubitIabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->queryInventoryAsync(ZLjava/util/List;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

.field final synthetic val$handler:Landroid/os/Handler;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;

.field final synthetic val$moreSkus:Ljava/util/List;

.field final synthetic val$querySkuDetails:Z


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;ZLjava/util/List;Landroid/os/Handler;Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;)V
    .locals 0

    .line 591
    iput-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iput-boolean p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->val$querySkuDetails:Z

    iput-object p3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->val$moreSkus:Ljava/util/List;

    iput-object p4, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->val$handler:Landroid/os/Handler;

    iput-object p5, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->val$listener:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$QueryInventoryFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 593
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v1, "Inventory refresh successful."

    const/4 v2, 0x0

    invoke-direct {v0, v2, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    .line 596
    :try_start_0
    iget-object v1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iget-boolean v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->val$querySkuDetails:Z

    iget-object v3, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->val$moreSkus:Ljava/util/List;

    invoke-virtual {v1, v2, v3}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->queryInventory(ZLjava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

    move-result-object v1
    :try_end_0
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 598
    invoke-virtual {v0}, Lorg/onepf/oms/appstore/googleUtils/IabException;->getResult()Lorg/onepf/oms/appstore/googleUtils/IabResult;

    move-result-object v0

    const/4 v1, 0x0

    .line 601
    :goto_0
    iget-object v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    invoke-virtual {v2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->flagEndAsync()V

    .line 605
    iget-object v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;->val$handler:Landroid/os/Handler;

    new-instance v3, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2$1;

    invoke-direct {v3, p0, v0, v1}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2$1;-><init>(Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$2;Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V

    invoke-virtual {v2, v3}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
