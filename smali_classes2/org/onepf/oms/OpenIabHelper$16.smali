.class Lorg/onepf/oms/OpenIabHelper$16;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


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

.field final synthetic val$billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/AppstoreInAppBillingService;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 1229
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$16;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$16;->val$billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$16;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 1232
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$16;->val$billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$16;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-interface {v0, v1}, Lorg/onepf/oms/AppstoreInAppBillingService;->startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    return-void
.end method
