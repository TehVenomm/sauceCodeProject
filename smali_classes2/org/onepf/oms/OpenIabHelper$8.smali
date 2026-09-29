.class Lorg/onepf/oms/OpenIabHelper$8;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper;->startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;

.field final synthetic val$instantiatedAppstores:Ljava/util/List;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

.field final synthetic val$storeNames:Ljava/util/List;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/List;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/List;)V
    .locals 0

    .line 486
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$8;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$8;->val$storeNames:Ljava/util/List;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$8;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    iput-object p4, p0, Lorg/onepf/oms/OpenIabHelper$8;->val$instantiatedAppstores:Ljava/util/List;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public openStoresDiscovered(Ljava/util/List;)V
    .locals 4
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/Appstore;",
            ">;)V"
        }
    .end annotation

    .line 490
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lorg/onepf/oms/Appstore;

    .line 491
    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v1

    .line 492
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$8;->val$storeNames:Ljava/util/List;

    invoke-interface {v2, v1}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 493
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$8;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v1}, Lorg/onepf/oms/OpenIabHelper;->access$300(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1, v0}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 496
    :cond_1
    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 497
    invoke-interface {v1}, Lorg/onepf/oms/AppstoreInAppBillingService;->dispose()V

    const/4 v1, 0x2

    .line 498
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "startSetup() billing service disposed for "

    aput-object v3, v1, v2

    const/4 v2, 0x1

    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    goto :goto_0

    .line 502
    :cond_2
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$8;->this$0:Lorg/onepf/oms/OpenIabHelper;

    new-instance v0, Lorg/onepf/oms/OpenIabHelper$8$1;

    invoke-direct {v0, p0}, Lorg/onepf/oms/OpenIabHelper$8$1;-><init>(Lorg/onepf/oms/OpenIabHelper$8;)V

    invoke-static {p1, v0}, Lorg/onepf/oms/OpenIabHelper;->access$500(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    return-void
.end method
