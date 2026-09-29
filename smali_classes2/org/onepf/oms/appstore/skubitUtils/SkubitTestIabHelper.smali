.class public Lorg/onepf/oms/appstore/skubitUtils/SkubitTestIabHelper;
.super Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;
.source "SkubitTestIabHelper.java"


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;)V
    .locals 0

    .line 29
    invoke-direct {p0, p1, p2, p3}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;)V

    return-void
.end method


# virtual methods
.method protected getServiceIntent()Landroid/content/Intent;
    .locals 2
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 34
    new-instance v0, Landroid/content/Intent;

    const-string v1, "net.skubit.android.billing.IBillingService.BIND"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v1, "net.skubit.android"

    .line 35
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    return-object v0
.end method
