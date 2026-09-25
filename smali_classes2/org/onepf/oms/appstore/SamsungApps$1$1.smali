.class Lorg/onepf/oms/appstore/SamsungApps$1$1;
.super Ljava/lang/Object;
.source "SamsungApps.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/SamsungApps$1;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/oms/appstore/SamsungApps$1;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/SamsungApps$1;)V
    .locals 0

    .line 133
    iput-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps$1$1;->this$1:Lorg/onepf/oms/appstore/SamsungApps$1;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 136
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps$1$1;->this$1:Lorg/onepf/oms/appstore/SamsungApps$1;

    iget-object v0, v0, Lorg/onepf/oms/appstore/SamsungApps$1;->this$0:Lorg/onepf/oms/appstore/SamsungApps;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/SamsungApps;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v0

    invoke-static {}, Lorg/onepf/oms/SkuManager;->getInstance()Lorg/onepf/oms/SkuManager;

    move-result-object v1

    const-string v2, "com.samsung.apps"

    invoke-virtual {v1, v2}, Lorg/onepf/oms/SkuManager;->getAllStoreSkus(Ljava/lang/String;)Ljava/util/List;

    move-result-object v1

    const/4 v2, 0x0

    const/4 v3, 0x1

    invoke-interface {v0, v3, v1, v2}, Lorg/onepf/oms/AppstoreInAppBillingService;->queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 139
    invoke-virtual {v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getAllOwnedSkus()Ljava/util/List;

    move-result-object v0

    invoke-static {v0}, Lorg/onepf/oms/util/CollectionUtils;->isEmpty(Ljava/util/Collection;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 140
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps$1$1;->this$1:Lorg/onepf/oms/appstore/SamsungApps$1;

    iget-object v0, v0, Lorg/onepf/oms/appstore/SamsungApps$1;->this$0:Lorg/onepf/oms/appstore/SamsungApps;

    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-static {v0, v1}, Lorg/onepf/oms/appstore/SamsungApps;->access$002(Lorg/onepf/oms/appstore/SamsungApps;Ljava/lang/Boolean;)Ljava/lang/Boolean;
    :try_end_0
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception v0

    goto :goto_1

    :catch_0
    move-exception v0

    :try_start_1
    const-string v1, "isBillingAvailable() failed"

    .line 143
    invoke-static {v1, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 145
    :cond_0
    :goto_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps$1$1;->this$1:Lorg/onepf/oms/appstore/SamsungApps$1;

    iget-object v0, v0, Lorg/onepf/oms/appstore/SamsungApps$1;->this$0:Lorg/onepf/oms/appstore/SamsungApps;

    invoke-virtual {v0}, Lorg/onepf/oms/appstore/SamsungApps;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v0

    invoke-interface {v0}, Lorg/onepf/oms/AppstoreInAppBillingService;->dispose()V

    .line 146
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps$1$1;->this$1:Lorg/onepf/oms/appstore/SamsungApps$1;

    iget-object v0, v0, Lorg/onepf/oms/appstore/SamsungApps$1;->val$mainLatch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v0}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    return-void

    .line 145
    :goto_1
    iget-object v1, p0, Lorg/onepf/oms/appstore/SamsungApps$1$1;->this$1:Lorg/onepf/oms/appstore/SamsungApps$1;

    iget-object v1, v1, Lorg/onepf/oms/appstore/SamsungApps$1;->this$0:Lorg/onepf/oms/appstore/SamsungApps;

    invoke-virtual {v1}, Lorg/onepf/oms/appstore/SamsungApps;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v1

    invoke-interface {v1}, Lorg/onepf/oms/AppstoreInAppBillingService;->dispose()V

    .line 146
    iget-object v1, p0, Lorg/onepf/oms/appstore/SamsungApps$1$1;->this$1:Lorg/onepf/oms/appstore/SamsungApps$1;

    iget-object v1, v1, Lorg/onepf/oms/appstore/SamsungApps$1;->val$mainLatch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    throw v0
.end method
