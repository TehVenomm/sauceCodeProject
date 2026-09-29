.class final Lorg/onepf/oms/appstore/FortumoStore$1;
.super Ljava/lang/Object;
.source "FortumoStore.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/FortumoStore;->initFortumoStore(Landroid/content/Context;Z)Lorg/onepf/oms/appstore/FortumoStore;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$checkInventory:Z

.field final synthetic val$fortumoStore:Lorg/onepf/oms/appstore/FortumoStore;

.field final synthetic val$latch:Ljava/util/concurrent/CountDownLatch;

.field final synthetic val$storeToReturn:[Lorg/onepf/oms/appstore/FortumoStore;


# direct methods
.method constructor <init>(ZLorg/onepf/oms/appstore/FortumoStore;[Lorg/onepf/oms/appstore/FortumoStore;Ljava/util/concurrent/CountDownLatch;)V
    .locals 0

    .line 117
    iput-boolean p1, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$checkInventory:Z

    iput-object p2, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$fortumoStore:Lorg/onepf/oms/appstore/FortumoStore;

    iput-object p3, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$storeToReturn:[Lorg/onepf/oms/appstore/FortumoStore;

    iput-object p4, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$latch:Ljava/util/concurrent/CountDownLatch;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    .locals 2
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabResult;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 120
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->isSuccess()Z

    move-result p1

    if-eqz p1, :cond_2

    .line 121
    iget-boolean p1, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$checkInventory:Z

    const/4 v0, 0x0

    if-eqz p1, :cond_1

    .line 123
    :try_start_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$fortumoStore:Lorg/onepf/oms/appstore/FortumoStore;

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/FortumoStore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object p1

    const/4 v1, 0x0

    invoke-interface {p1, v0, v1, v1}, Lorg/onepf/oms/AppstoreInAppBillingService;->queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;

    move-result-object p1

    .line 124
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->getAllPurchases()Ljava/util/List;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/List;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_0

    .line 125
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$storeToReturn:[Lorg/onepf/oms/appstore/FortumoStore;

    iget-object v1, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$fortumoStore:Lorg/onepf/oms/appstore/FortumoStore;

    aput-object v1, p1, v0

    goto :goto_0

    :cond_0
    const-string p1, "Purchases not found"

    .line 127
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/onepf/oms/appstore/googleUtils/IabException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "Error while requesting purchases"

    .line 130
    invoke-static {v0, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    .line 133
    :cond_1
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$storeToReturn:[Lorg/onepf/oms/appstore/FortumoStore;

    iget-object v1, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$fortumoStore:Lorg/onepf/oms/appstore/FortumoStore;

    aput-object v1, p1, v0

    .line 136
    :cond_2
    :goto_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore$1;->val$latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    return-void
.end method
