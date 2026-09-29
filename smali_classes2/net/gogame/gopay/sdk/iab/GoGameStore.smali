.class public Lnet/gogame/gopay/sdk/iab/GoGameStore;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/GoPayInAppBillingServiceExt;
.implements Lorg/onepf/oms/Appstore;


# instance fields
.field private final a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

.field private final b:Landroid/content/Context;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 7
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/GoGameStore;->b:Landroid/content/Context;

    new-instance v6, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    const/4 v4, 0x0

    move-object v0, v6

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    iput-object v6, p0, Lnet/gogame/gopay/sdk/iab/GoGameStore;->a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 7
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p5    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/GoGameStore;->b:Landroid/content/Context;

    new-instance v6, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    move-object v0, v6

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p5

    move-object v5, p4

    invoke-direct/range {v0 .. v5}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    iput-object v6, p0, Lnet/gogame/gopay/sdk/iab/GoGameStore;->a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p5    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p6    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    invoke-direct/range {p0 .. p5}, Lnet/gogame/gopay/sdk/iab/GoGameStore;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/GoGameStore;->a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    invoke-virtual {p1, p6}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->setGameLanguage(Ljava/lang/String;)V

    return-void
.end method

.method public static newOpenIabHelper(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper;
    .locals 5
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    new-instance v0, Lorg/onepf/oms/OpenIabHelper;

    new-instance v1, Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    invoke-direct {v1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;-><init>()V

    const-string v2, "GoGameStore"

    filled-new-array {v2}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStoreNames([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object v1

    const/4 v2, 0x1

    new-array v3, v2, [Lorg/onepf/oms/Appstore;

    new-instance v4, Lnet/gogame/gopay/sdk/iab/GoGameStore;

    invoke-direct {v4, p0, p1, p2, p3}, Lnet/gogame/gopay/sdk/iab/GoGameStore;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, 0x0

    aput-object v4, v3, p1

    invoke-virtual {v1, v3}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStores([Lorg/onepf/oms/Appstore;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p2

    invoke-virtual {p2, p1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->setCheckInventory(Z)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p1

    invoke-virtual {p1, v2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->setStoreSearchStrategy(I)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object p1

    invoke-virtual {p1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->build()Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object p1

    invoke-direct {v0, p0, p1}, Lorg/onepf/oms/OpenIabHelper;-><init>(Landroid/content/Context;Lorg/onepf/oms/OpenIabHelper$Options;)V

    return-object v0
.end method


# virtual methods
.method public areOutsideLinksAllowed()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    const-string v0, "GoGameStore"

    return-object v0
.end method

.method public getCountries()Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;
    .locals 4

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/GoGameStore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v0

    check-cast v0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    if-eqz v0, :cond_0

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->getCountries()Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;

    move-result-object v0

    return-object v0

    :cond_0
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v2, -0x3f0

    const-string v3, "Unable to acquire billing service"

    invoke-direct {v1, v2, v3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-direct {v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    throw v0
.end method

.method public getCountries(Ljava/lang/String;)Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;
    .locals 3
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/iab/GoGameStore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v0

    check-cast v0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    if-eqz v0, :cond_0

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->getCountries(Ljava/lang/String;)Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;

    move-result-object p1

    return-object p1

    :cond_0
    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v1, -0x3f0

    const-string v2, "Unable to acquire billing service"

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-direct {p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    throw p1
.end method

.method public getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoGameStore;->a:Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;

    return-object v0
.end method

.method public getPackageVersion(Ljava/lang/String;)I
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method public getProductPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .locals 0
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    const/4 p1, 0x0

    return-object p1
.end method

.method public getRateItPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .locals 0
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    const/4 p1, 0x0

    return-object p1
.end method

.method public getSameDeveloperPageIntent(Ljava/lang/String;)Landroid/content/Intent;
    .locals 0
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    const/4 p1, 0x0

    return-object p1
.end method

.method public isBillingAvailable(Ljava/lang/String;)Z
    .locals 0

    const/4 p1, 0x1

    return p1
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method
