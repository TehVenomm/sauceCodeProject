.class public interface abstract Lorg/onepf/oms/AppstoreInAppBillingService;
.super Ljava/lang/Object;
.source "AppstoreInAppBillingService.java"


# virtual methods
.method public abstract consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/onepf/oms/appstore/googleUtils/IabException;
        }
    .end annotation
.end method

.method public abstract dispose()V
.end method

.method public abstract handleActivityResult(IILandroid/content/Intent;)Z
.end method

.method public abstract launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
.end method

.method public abstract queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
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

    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end method

.method public abstract startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
.end method

.method public abstract subscriptionsSupported()Z
.end method
