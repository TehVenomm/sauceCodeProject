.class Lorg/onepf/oms/OpenIabHelper$8$1;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper$8;->openStoresDiscovered(Ljava/util/List;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/oms/OpenIabHelper$8;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper$8;)V
    .locals 0

    .line 502
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$8$1;->this$1:Lorg/onepf/oms/OpenIabHelper$8;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    .locals 4

    .line 505
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$8$1;->this$1:Lorg/onepf/oms/OpenIabHelper$8;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$8;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-interface {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    .line 506
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$8$1;->this$1:Lorg/onepf/oms/OpenIabHelper$8;

    iget-object p1, p1, Lorg/onepf/oms/OpenIabHelper$8;->val$instantiatedAppstores:Ljava/util/List;

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$8$1;->this$1:Lorg/onepf/oms/OpenIabHelper$8;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$8;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->access$400(Lorg/onepf/oms/OpenIabHelper;)Lorg/onepf/oms/Appstore;

    move-result-object v0

    invoke-interface {p1, v0}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    .line 507
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$8$1;->this$1:Lorg/onepf/oms/OpenIabHelper$8;

    iget-object p1, p1, Lorg/onepf/oms/OpenIabHelper$8;->val$instantiatedAppstores:Ljava/util/List;

    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lorg/onepf/oms/Appstore;

    .line 509
    invoke-interface {v0}, Lorg/onepf/oms/Appstore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 510
    invoke-interface {v1}, Lorg/onepf/oms/AppstoreInAppBillingService;->dispose()V

    const/4 v1, 0x2

    .line 511
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

    :cond_1
    return-void
.end method
