.class public Lorg/onepf/oms/appstore/SkubitAppstore;
.super Lorg/onepf/oms/DefaultAppstore;
.source "SkubitAppstore.java"


# static fields
.field public static final SKUBIT_INSTALLER:Ljava/lang/String; = "com.skubit.android"

.field public static final TIMEOUT_BILLING_SUPPORTED:I = 0x7d0

.field public static final VENDING_ACTION:Ljava/lang/String; = "com.skubit.android.billing.IBillingService.BIND"


# instance fields
.field private volatile billingAvailable:Ljava/lang/Boolean;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field protected final context:Landroid/content/Context;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field protected final isDebugMode:Z

.field protected mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/Nullable;
        .end annotation
    .end param

    .line 66
    invoke-direct {p0}, Lorg/onepf/oms/DefaultAppstore;-><init>()V

    const/4 v0, 0x0

    .line 61
    iput-object v0, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->billingAvailable:Ljava/lang/Boolean;

    const/4 v0, 0x0

    .line 64
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->isDebugMode:Z

    if-eqz p1, :cond_0

    .line 70
    iput-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->context:Landroid/content/Context;

    return-void

    .line 68
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "context is null"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method static synthetic access$002(Lorg/onepf/oms/appstore/SkubitAppstore;Ljava/lang/Boolean;)Ljava/lang/Boolean;
    .locals 0

    .line 47
    iput-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->billingAvailable:Ljava/lang/Boolean;

    return-object p1
.end method

.method private packageExists(Landroid/content/Context;Ljava/lang/String;)Z
    .locals 2
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 168
    :try_start_0
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p1

    invoke-virtual {p1, p2, v1}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    const/4 p1, 0x2

    .line 171
    new-array p1, p1, [Ljava/lang/Object;

    aput-object p2, p1, v1

    const-string p2, " package was not found."

    aput-object p2, p1, v0

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return v1
.end method


# virtual methods
.method public getAction()Ljava/lang/String;
    .locals 1

    const-string v0, "com.skubit.android.billing.IBillingService.BIND"

    return-object v0
.end method

.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    const-string v0, "com.skubit.android"

    return-object v0
.end method

.method public declared-synchronized getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 3
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    monitor-enter p0

    .line 155
    :try_start_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    if-nez v0, :cond_0

    .line 156
    new-instance v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iget-object v1, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->context:Landroid/content/Context;

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, p0}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    .line 158
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->mBillingService:Lorg/onepf/oms/AppstoreInAppBillingService;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 154
    monitor-exit p0

    throw v0
.end method

.method public getInstaller()Ljava/lang/String;
    .locals 1

    const-string v0, "com.skubit.android"

    return-object v0
.end method

.method public getPackageVersion(Ljava/lang/String;)I
    .locals 0

    const/4 p1, -0x1

    return p1
.end method

.method public isBillingAvailable(Ljava/lang/String;)Z
    .locals 4

    const/4 v0, 0x2

    .line 94
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "isBillingAvailable() packageName: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    const/4 v1, 0x1

    aput-object p1, v0, v1

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 95
    iget-object v0, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->billingAvailable:Ljava/lang/Boolean;

    if-eqz v0, :cond_0

    .line 96
    iget-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->billingAvailable:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1

    .line 99
    :cond_0
    invoke-static {}, Lorg/onepf/oms/util/Utils;->uiThread()Z

    move-result v0

    if-nez v0, :cond_4

    .line 103
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_3

    .line 107
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->billingAvailable:Ljava/lang/Boolean;

    .line 108
    iget-object v0, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->context:Landroid/content/Context;

    const-string v3, "com.skubit.android"

    invoke-direct {p0, v0, v3}, Lorg/onepf/oms/appstore/SkubitAppstore;->packageExists(Landroid/content/Context;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 109
    new-instance v0, Landroid/content/Intent;

    invoke-virtual {p0}, Lorg/onepf/oms/appstore/SkubitAppstore;->getAction()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v0, v3}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 110
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/SkubitAppstore;->getInstaller()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 111
    iget-object v3, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->context:Landroid/content/Context;

    invoke-virtual {v3}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    invoke-virtual {v3, v0, v2}, Landroid/content/pm/PackageManager;->queryIntentServices(Landroid/content/Intent;I)Ljava/util/List;

    move-result-object v2

    .line 112
    invoke-static {v2}, Lorg/onepf/oms/util/CollectionUtils;->isEmpty(Ljava/util/Collection;)Z

    move-result v2

    if-nez v2, :cond_2

    .line 113
    new-instance v2, Ljava/util/concurrent/CountDownLatch;

    invoke-direct {v2, v1}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    .line 114
    new-instance v3, Lorg/onepf/oms/appstore/SkubitAppstore$1;

    invoke-direct {v3, p0, p1, v2}, Lorg/onepf/oms/appstore/SkubitAppstore$1;-><init>(Lorg/onepf/oms/appstore/SkubitAppstore;Ljava/lang/String;Ljava/util/concurrent/CountDownLatch;)V

    .line 135
    iget-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->context:Landroid/content/Context;

    invoke-virtual {p1, v0, v3, v1}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result p1

    if-eqz p1, :cond_1

    const-wide/16 v0, 0x7d0

    .line 137
    :try_start_0
    sget-object p1, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {v2, v0, v1, p1}, Ljava/util/concurrent/CountDownLatch;->await(JLjava/util/concurrent/TimeUnit;)Z
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :cond_1
    const-string p1, "isBillingAvailable() billing is not supported. Initialization error."

    .line 141
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 144
    :cond_2
    iget-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->billingAvailable:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1

    .line 104
    :cond_3
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "packageName is null"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 100
    :cond_4
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Must no be called from UI thread."

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 1

    .line 86
    iget-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore;->context:Landroid/content/Context;

    const-string v0, "com.skubit.android"

    invoke-static {p1, v0}, Lorg/onepf/oms/util/Utils;->isPackageInstaller(Landroid/content/Context;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method
