.class public Lorg/onepf/oms/appstore/FortumoStore;
.super Lorg/onepf/oms/DefaultAppstore;
.source "FortumoStore.java"


# static fields
.field public static final FORTUMO_DETAILS_FILE_NAME:Ljava/lang/String; = "fortumo_inapps_details.xml"

.field public static final IN_APP_PRODUCTS_FILE_NAME:Ljava/lang/String; = "inapps_products.xml"


# instance fields
.field private billingService:Lorg/onepf/oms/appstore/FortumoBillingService;

.field private context:Landroid/content/Context;

.field private isBillingAvailable:Ljava/lang/Boolean;

.field private isNookDevice:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 59
    invoke-direct {p0}, Lorg/onepf/oms/DefaultAppstore;-><init>()V

    .line 60
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore;->context:Landroid/content/Context;

    .line 61
    invoke-static {}, Lorg/onepf/oms/appstore/FortumoStore;->isNookDevice()Z

    move-result p1

    iput-boolean p1, p0, Lorg/onepf/oms/appstore/FortumoStore;->isNookDevice:Z

    return-void
.end method

.method public static initFortumoStore(Landroid/content/Context;Z)Lorg/onepf/oms/appstore/FortumoStore;
    .locals 5
    .param p0    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x1

    .line 113
    new-array v1, v0, [Lorg/onepf/oms/appstore/FortumoStore;

    const/4 v2, 0x0

    const/4 v3, 0x0

    aput-object v3, v1, v2

    .line 114
    new-instance v3, Lorg/onepf/oms/appstore/FortumoStore;

    invoke-direct {v3, p0}, Lorg/onepf/oms/appstore/FortumoStore;-><init>(Landroid/content/Context;)V

    .line 115
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v3, p0}, Lorg/onepf/oms/appstore/FortumoStore;->isBillingAvailable(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_0

    .line 116
    new-instance p0, Ljava/util/concurrent/CountDownLatch;

    invoke-direct {p0, v0}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    .line 117
    invoke-virtual {v3}, Lorg/onepf/oms/appstore/FortumoStore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object v0

    new-instance v4, Lorg/onepf/oms/appstore/FortumoStore$1;

    invoke-direct {v4, p1, v3, v1, p0}, Lorg/onepf/oms/appstore/FortumoStore$1;-><init>(ZLorg/onepf/oms/appstore/FortumoStore;[Lorg/onepf/oms/appstore/FortumoStore;Ljava/util/concurrent/CountDownLatch;)V

    invoke-interface {v0, v4}, Lorg/onepf/oms/AppstoreInAppBillingService;->startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    .line 140
    :try_start_0
    invoke-virtual {p0}, Ljava/util/concurrent/CountDownLatch;->await()V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    const-string p1, "Setup was interrupted"

    .line 142
    invoke-static {p1, p0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 145
    :cond_0
    :goto_0
    aget-object p0, v1, v2

    return-object p0
.end method

.method private static isNookDevice()Z
    .locals 3

    .line 105
    sget-object v0, Landroid/os/Build;->BRAND:Ljava/lang/String;

    const-string v1, "ro.nook.manufacturer"

    .line 106
    invoke-static {v1}, Ljava/lang/System;->getProperty(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    if-eqz v0, :cond_0

    const-string v2, "nook"

    .line 107
    invoke-virtual {v0, v2}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    :cond_0
    if-eqz v1, :cond_2

    const-string v0, "nook"

    invoke-virtual {v1, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    :cond_1
    const/4 v0, 0x1

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    :goto_0
    return v0
.end method


# virtual methods
.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    const-string v0, "com.fortumo.billing"

    return-object v0
.end method

.method public getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 3

    .line 97
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoStore;->billingService:Lorg/onepf/oms/appstore/FortumoBillingService;

    if-nez v0, :cond_0

    .line 98
    new-instance v0, Lorg/onepf/oms/appstore/FortumoBillingService;

    iget-object v1, p0, Lorg/onepf/oms/appstore/FortumoStore;->context:Landroid/content/Context;

    iget-boolean v2, p0, Lorg/onepf/oms/appstore/FortumoStore;->isNookDevice:Z

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/FortumoBillingService;-><init>(Landroid/content/Context;Z)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/FortumoStore;->billingService:Lorg/onepf/oms/appstore/FortumoBillingService;

    .line 100
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/FortumoStore;->billingService:Lorg/onepf/oms/appstore/FortumoBillingService;

    return-object v0
.end method

.method public getPackageVersion(Ljava/lang/String;)I
    .locals 0

    const/4 p1, -0x1

    return p1
.end method

.method public isBillingAvailable(Ljava/lang/String;)Z
    .locals 2

    .line 76
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore;->isBillingAvailable:Ljava/lang/Boolean;

    if-eqz p1, :cond_0

    .line 77
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore;->isBillingAvailable:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1

    .line 79
    :cond_0
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/FortumoStore;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object p1

    check-cast p1, Lorg/onepf/oms/appstore/FortumoBillingService;

    iput-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore;->billingService:Lorg/onepf/oms/appstore/FortumoBillingService;

    .line 80
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore;->billingService:Lorg/onepf/oms/appstore/FortumoBillingService;

    iget-boolean v0, p0, Lorg/onepf/oms/appstore/FortumoStore;->isNookDevice:Z

    invoke-virtual {p1, v0}, Lorg/onepf/oms/appstore/FortumoBillingService;->setupBilling(Z)Z

    move-result p1

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore;->isBillingAvailable:Ljava/lang/Boolean;

    const/4 p1, 0x2

    .line 81
    new-array p1, p1, [Ljava/lang/Object;

    const/4 v0, 0x0

    const-string v1, "isBillingAvailable: "

    aput-object v1, p1, v0

    const/4 v0, 0x1

    iget-object v1, p0, Lorg/onepf/oms/appstore/FortumoStore;->isBillingAvailable:Ljava/lang/Boolean;

    aput-object v1, p1, v0

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 82
    iget-object p1, p0, Lorg/onepf/oms/appstore/FortumoStore;->isBillingAvailable:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method
