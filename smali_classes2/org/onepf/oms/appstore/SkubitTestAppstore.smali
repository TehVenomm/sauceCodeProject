.class public Lorg/onepf/oms/appstore/SkubitTestAppstore;
.super Lorg/onepf/oms/appstore/SkubitAppstore;
.source "SkubitTestAppstore.java"


# static fields
.field public static final SKUBIT_INSTALLER:Ljava/lang/String; = "net.skubit.android"

.field public static final VENDING_ACTION:Ljava/lang/String; = "net.skubit.android.billing.IBillingService.BIND"


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 35
    invoke-direct {p0, p1}, Lorg/onepf/oms/appstore/SkubitAppstore;-><init>(Landroid/content/Context;)V

    return-void
.end method


# virtual methods
.method public getAction()Ljava/lang/String;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    const-string v0, "net.skubit.android.billing.IBillingService.BIND"

    return-object v0
.end method

.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    const-string v0, "net.skubit.android"

    return-object v0
.end method

.method public declared-synchronized getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    monitor-enter p0

    .line 49
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/SkubitTestAppstore;->mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    if-nez v0, :cond_0

    .line 50
    new-instance v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitTestIabHelper;

    iget-object v1, p0, Lorg/onepf/oms/appstore/SkubitTestAppstore;->context:Landroid/content/Context;

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitTestIabHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/SkubitTestAppstore;->mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    .line 52
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/SkubitTestAppstore;->mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 48
    monitor-exit p0

    throw v0
.end method

.method public getInstaller()Ljava/lang/String;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    const-string v0, "net.skubit.android"

    return-object v0
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 1

    .line 43
    iget-object p1, p0, Lorg/onepf/oms/appstore/SkubitTestAppstore;->context:Landroid/content/Context;

    const-string v0, "net.skubit.android"

    invoke-static {p1, v0}, Lorg/onepf/oms/util/Utils;->isPackageInstaller(Landroid/content/Context;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method
