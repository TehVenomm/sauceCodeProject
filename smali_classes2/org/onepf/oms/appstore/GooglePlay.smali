.class public Lorg/onepf/oms/appstore/GooglePlay;
.super Lorg/onepf/oms/DefaultAppstore;
.source "GooglePlay.java"


# static fields
.field public static final ANDROID_INSTALLER:Ljava/lang/String; = "com.android.vending"

.field private static final GOOGLE_INSTALLER:Ljava/lang/String; = "com.google.vending"

.field public static final VENDING_ACTION:Ljava/lang/String; = "com.android.vending.billing.InAppBillingService.BIND"


# instance fields
.field private volatile billingAvailable:Ljava/lang/Boolean;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private context:Landroid/content/Context;

.field private final isDebugMode:Z

.field private mBillingService:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

.field private publicKey:Ljava/lang/String;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1

    .line 66
    invoke-direct {p0}, Lorg/onepf/oms/DefaultAppstore;-><init>()V

    const/4 v0, 0x0

    .line 60
    iput-object v0, p0, Lorg/onepf/oms/appstore/GooglePlay;->billingAvailable:Ljava/lang/Boolean;

    const/4 v0, 0x0

    .line 64
    iput-boolean v0, p0, Lorg/onepf/oms/appstore/GooglePlay;->isDebugMode:Z

    .line 67
    iput-object p1, p0, Lorg/onepf/oms/appstore/GooglePlay;->context:Landroid/content/Context;

    .line 68
    iput-object p2, p0, Lorg/onepf/oms/appstore/GooglePlay;->publicKey:Ljava/lang/String;

    return-void
.end method

.method static synthetic access$000(Lorg/onepf/oms/appstore/GooglePlay;)Landroid/content/Context;
    .locals 0

    .line 51
    iget-object p0, p0, Lorg/onepf/oms/appstore/GooglePlay;->context:Landroid/content/Context;

    return-object p0
.end method

.method private packageExists(Landroid/content/Context;Ljava/lang/String;)Z
    .locals 2
    .param p1    # Landroid/content/Context;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 167
    :try_start_0
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p1

    invoke-virtual {p1, p2, v1}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    const/4 p1, 0x2

    .line 170
    new-array p1, p1, [Ljava/lang/Object;

    aput-object p2, p1, v1

    const-string p2, " package was not found."

    aput-object p2, p1, v0

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return v1
.end method


# virtual methods
.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    const-string v0, "com.google.play"

    return-object v0
.end method

.method public getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 3

    .line 154
    iget-object v0, p0, Lorg/onepf/oms/appstore/GooglePlay;->mBillingService:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    if-nez v0, :cond_0

    .line 155
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iget-object v1, p0, Lorg/onepf/oms/appstore/GooglePlay;->context:Landroid/content/Context;

    iget-object v2, p0, Lorg/onepf/oms/appstore/GooglePlay;->publicKey:Ljava/lang/String;

    invoke-direct {v0, v1, v2, p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/GooglePlay;->mBillingService:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    .line 157
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/GooglePlay;->mBillingService:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    return-object v0
.end method

.method public getPackageVersion(Ljava/lang/String;)I
    .locals 0

    const/4 p1, -0x1

    return p1
.end method

.method public isBillingAvailable(Ljava/lang/String;)Z
    .locals 6

    const/4 v0, 0x2

    .line 91
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "isBillingAvailable() packageName: "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    const/4 v1, 0x1

    aput-object p1, v0, v1

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 92
    iget-object v0, p0, Lorg/onepf/oms/appstore/GooglePlay;->billingAvailable:Ljava/lang/Boolean;

    if-eqz v0, :cond_0

    .line 93
    iget-object p1, p0, Lorg/onepf/oms/appstore/GooglePlay;->billingAvailable:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1

    .line 96
    :cond_0
    invoke-static {}, Lorg/onepf/oms/util/Utils;->uiThread()Z

    move-result v0

    if-nez v0, :cond_4

    .line 100
    iget-object v0, p0, Lorg/onepf/oms/appstore/GooglePlay;->context:Landroid/content/Context;

    const-string v3, "com.android.vending"

    invoke-direct {p0, v0, v3}, Lorg/onepf/oms/appstore/GooglePlay;->packageExists(Landroid/content/Context;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lorg/onepf/oms/appstore/GooglePlay;->context:Landroid/content/Context;

    const-string v3, "com.google.vending"

    invoke-direct {p0, v0, v3}, Lorg/onepf/oms/appstore/GooglePlay;->packageExists(Landroid/content/Context;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    const-string p1, "isBillingAvailable() Google Play is not available."

    .line 101
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    return v2

    .line 106
    :cond_1
    new-instance v0, Landroid/content/Intent;

    const-string v3, "com.android.vending.billing.InAppBillingService.BIND"

    invoke-direct {v0, v3}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v3, "com.android.vending"

    .line 107
    invoke-virtual {v0, v3}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 108
    iget-object v3, p0, Lorg/onepf/oms/appstore/GooglePlay;->context:Landroid/content/Context;

    invoke-virtual {v3}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    invoke-virtual {v3, v0, v2}, Landroid/content/pm/PackageManager;->queryIntentServices(Landroid/content/Intent;I)Ljava/util/List;

    move-result-object v3

    .line 109
    invoke-static {v3}, Lorg/onepf/oms/util/CollectionUtils;->isEmpty(Ljava/util/Collection;)Z

    move-result v3

    if-eqz v3, :cond_2

    const-string p1, "isBillingAvailable() billing service is not available, even though Google Play application seems to be installed."

    .line 110
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    return v2

    .line 114
    :cond_2
    new-instance v3, Ljava/util/concurrent/CountDownLatch;

    invoke-direct {v3, v1}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    .line 115
    new-array v4, v1, [Z

    .line 116
    new-instance v5, Lorg/onepf/oms/appstore/GooglePlay$1;

    invoke-direct {v5, p0, p1, v4, v3}, Lorg/onepf/oms/appstore/GooglePlay$1;-><init>(Lorg/onepf/oms/appstore/GooglePlay;Ljava/lang/String;[ZLjava/util/concurrent/CountDownLatch;)V

    .line 134
    iget-object p1, p0, Lorg/onepf/oms/appstore/GooglePlay;->context:Landroid/content/Context;

    invoke-virtual {p1, v0, v5, v1}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result p1

    if-eqz p1, :cond_3

    .line 136
    :try_start_0
    invoke-virtual {v3}, Ljava/util/concurrent/CountDownLatch;->await()V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "isBillingAvailable() InterruptedException while setting up in-app billing"

    .line 138
    invoke-static {v0, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_3
    aput-boolean v2, v4, v2

    const-string p1, "isBillingAvailable() billing is not supported. Initialization error."

    .line 142
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;)V

    .line 144
    :goto_0
    aget-boolean p1, v4, v2

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    iput-object p1, p0, Lorg/onepf/oms/appstore/GooglePlay;->billingAvailable:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1

    .line 97
    :cond_4
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Must no be called from UI thread."

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 1

    .line 76
    iget-object p1, p0, Lorg/onepf/oms/appstore/GooglePlay;->context:Landroid/content/Context;

    const-string v0, "com.android.vending"

    invoke-static {p1, v0}, Lorg/onepf/oms/util/Utils;->isPackageInstaller(Landroid/content/Context;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method
