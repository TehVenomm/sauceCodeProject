.class Lorg/onepf/oms/OpenIabHelper$15;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper;->checkInventory(Ljava/util/Set;)Lorg/onepf/oms/Appstore;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;

.field final synthetic val$appstore:Lorg/onepf/oms/Appstore;

.field final synthetic val$billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

.field final synthetic val$inventoryAppstore:[Lorg/onepf/oms/Appstore;

.field final synthetic val$inventorySemaphore:Ljava/util/concurrent/Semaphore;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/concurrent/Semaphore;Lorg/onepf/oms/AppstoreInAppBillingService;[Lorg/onepf/oms/Appstore;Lorg/onepf/oms/Appstore;)V
    .locals 0

    .line 1200
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$15;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$15;->val$inventorySemaphore:Ljava/util/concurrent/Semaphore;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$15;->val$billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    iput-object p4, p0, Lorg/onepf/oms/OpenIabHelper$15;->val$inventoryAppstore:[Lorg/onepf/oms/Appstore;

    iput-object p5, p0, Lorg/onepf/oms/OpenIabHelper$15;->val$appstore:Lorg/onepf/oms/Appstore;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    .locals 1
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabResult;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 1203
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->isSuccess()Z

    move-result p1

    if-nez p1, :cond_0

    .line 1204
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$15;->val$inventorySemaphore:Ljava/util/concurrent/Semaphore;

    invoke-virtual {p1}, Ljava/util/concurrent/Semaphore;->release()V

    return-void

    .line 1208
    :cond_0
    new-instance p1, Lorg/onepf/oms/OpenIabHelper$15$1;

    invoke-direct {p1, p0}, Lorg/onepf/oms/OpenIabHelper$15$1;-><init>(Lorg/onepf/oms/OpenIabHelper$15;)V

    .line 1225
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$15;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->access$2000(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    invoke-interface {v0, p1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    return-void
.end method
