.class public Lorg/onepf/oms/appstore/AmazonAppstore;
.super Lorg/onepf/oms/DefaultAppstore;
.source "AmazonAppstore.java"


# static fields
.field public static final AMAZON_INSTALLER:Ljava/lang/String; = "com.amazon.venezia"


# instance fields
.field private final context:Landroid/content/Context;

.field private mBillingService:Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 43
    invoke-direct {p0}, Lorg/onepf/oms/DefaultAppstore;-><init>()V

    .line 44
    iput-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstore;->context:Landroid/content/Context;

    return-void
.end method

.method public static hasAmazonClasses()Z
    .locals 3

    .line 61
    const-class v0, Lorg/onepf/oms/appstore/AmazonAppstore;

    monitor-enter v0

    .line 63
    :try_start_0
    const-class v1, Lorg/onepf/oms/appstore/AmazonAppstore;

    invoke-virtual {v1}, Ljava/lang/Class;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v1

    const-string v2, "com.amazon.android.Kiwi"

    .line 64
    invoke-virtual {v1, v2}, Ljava/lang/ClassLoader;->loadClass(Ljava/lang/String;)Ljava/lang/Class;
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v1, 0x1

    goto :goto_0

    :catchall_0
    move-exception v1

    goto :goto_1

    :catch_0
    :try_start_1
    const-string v1, "hasAmazonClasses() cannot load class com.amazon.android.Kiwi"

    .line 67
    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    const/4 v1, 0x0

    .line 70
    :goto_0
    monitor-exit v0

    return v1

    :goto_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1
.end method


# virtual methods
.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    const-string v0, "com.amazon.apps"

    return-object v0
.end method

.method public getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 2

    .line 89
    iget-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstore;->mBillingService:Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;

    if-nez v0, :cond_0

    .line 90
    new-instance v0, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;

    iget-object v1, p0, Lorg/onepf/oms/appstore/AmazonAppstore;->context:Landroid/content/Context;

    invoke-direct {v0, v1}, Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstore;->mBillingService:Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;

    .line 92
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/AmazonAppstore;->mBillingService:Lorg/onepf/oms/appstore/AmazonAppstoreBillingService;

    return-object v0
.end method

.method public getPackageVersion(Ljava/lang/String;)I
    .locals 0

    const/4 p1, -0x1

    return p1
.end method

.method public isBillingAvailable(Ljava/lang/String;)Z
    .locals 0

    .line 79
    invoke-virtual {p0, p1}, Lorg/onepf/oms/appstore/AmazonAppstore;->isPackageInstaller(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 1

    .line 49
    iget-object p1, p0, Lorg/onepf/oms/appstore/AmazonAppstore;->context:Landroid/content/Context;

    const-string v0, "com.amazon.venezia"

    invoke-static {p1, v0}, Lorg/onepf/oms/util/Utils;->isPackageInstaller(Landroid/content/Context;Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_1

    .line 50
    invoke-static {}, Lorg/onepf/oms/appstore/AmazonAppstore;->hasAmazonClasses()Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method
