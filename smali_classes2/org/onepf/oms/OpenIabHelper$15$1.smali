.class Lorg/onepf/oms/OpenIabHelper$15$1;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper$15;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/oms/OpenIabHelper$15;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper$15;)V
    .locals 0

    .line 1208
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$15$1;->this$1:Lorg/onepf/oms/OpenIabHelper$15;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 7

    const/4 v0, 0x2

    const/4 v1, 0x1

    const/4 v2, 0x3

    const/4 v3, 0x0

    .line 1212
    :try_start_0
    iget-object v4, p0, Lorg/onepf/oms/OpenIabHelper$15$1;->this$1:Lorg/onepf/oms/OpenIabHelper$15;

    iget-object v4, v4, Lorg/onepf/oms/OpenIabHelper$15;->val$billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    const/4 v5, 0x0

    invoke-interface {v4, v3, v5, v5}, Lorg/onepf/oms/AppstoreInAppBillingService;->queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

    move-result-object v4

    if-eqz v4, :cond_0

    .line 1213
    invoke-virtual {v4}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getAllPurchases()Ljava/util/List;

    move-result-object v5

    invoke-interface {v5}, Ljava/util/List;->isEmpty()Z

    move-result v5

    if-nez v5, :cond_0

    .line 1214
    iget-object v5, p0, Lorg/onepf/oms/OpenIabHelper$15$1;->this$1:Lorg/onepf/oms/OpenIabHelper$15;

    iget-object v5, v5, Lorg/onepf/oms/OpenIabHelper$15;->val$inventoryAppstore:[Lorg/onepf/oms/Appstore;

    iget-object v6, p0, Lorg/onepf/oms/OpenIabHelper$15$1;->this$1:Lorg/onepf/oms/OpenIabHelper$15;

    iget-object v6, v6, Lorg/onepf/oms/OpenIabHelper$15;->val$appstore:Lorg/onepf/oms/Appstore;

    aput-object v6, v5, v3

    const/4 v5, 0x5

    .line 1215
    new-array v5, v5, [Ljava/lang/Object;

    const-string v6, "inventoryCheck() in "

    aput-object v6, v5, v3

    iget-object v6, p0, Lorg/onepf/oms/OpenIabHelper$15$1;->this$1:Lorg/onepf/oms/OpenIabHelper$15;

    iget-object v6, v6, Lorg/onepf/oms/OpenIabHelper$15;->val$appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v6}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v1

    const-string v6, " found: "

    aput-object v6, v5, v0

    invoke-virtual {v4}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getAllPurchases()Ljava/util/List;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/List;->size()I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v5, v2

    const/4 v4, 0x4

    const-string v6, " purchases"

    aput-object v6, v5, v4

    invoke-static {v5}, Lorg/onepf/oms/util/Logger;->dWithTimeFromUp([Ljava/lang/Object;)V
    :try_end_0
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v4

    .line 1220
    new-array v2, v2, [Ljava/lang/Object;

    const-string v5, "inventoryCheck() failed for "

    aput-object v5, v2, v3

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v5, p0, Lorg/onepf/oms/OpenIabHelper$15$1;->this$1:Lorg/onepf/oms/OpenIabHelper$15;

    iget-object v5, v5, Lorg/onepf/oms/OpenIabHelper$15;->val$appstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v5}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, " : "

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v2, v1

    aput-object v4, v2, v0

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->e([Ljava/lang/Object;)V

    .line 1222
    :cond_0
    :goto_0
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$15$1;->this$1:Lorg/onepf/oms/OpenIabHelper$15;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$15;->val$inventorySemaphore:Ljava/util/concurrent/Semaphore;

    invoke-virtual {v0}, Ljava/util/concurrent/Semaphore;->release()V

    return-void
.end method
