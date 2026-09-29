.class public Lorg/onepf/oms/appstore/SamsungApps;
.super Lorg/onepf/oms/DefaultAppstore;
.source "SamsungApps.java"


# static fields
.field public static final IAP_PACKAGE_NAME:Ljava/lang/String; = "com.sec.android.iap"

.field public static final IAP_SERVICE_NAME:Ljava/lang/String; = "com.sec.android.iap.service.iapService"

.field private static final IAP_SIGNATURE_HASHCODE:I = 0x7a7eaf4b

.field public static final SAMSUNG_INSTALLER:Ljava/lang/String; = "com.sec.android.app.samsungapps"

.field public static isSamsungTestMode:Z


# instance fields
.field private activity:Landroid/app/Activity;

.field private billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

.field private isBillingAvailable:Ljava/lang/Boolean;

.field private options:Lorg/onepf/oms/OpenIabHelper$Options;


# direct methods
.method public constructor <init>(Landroid/app/Activity;Lorg/onepf/oms/OpenIabHelper$Options;)V
    .locals 0

    .line 82
    invoke-direct {p0}, Lorg/onepf/oms/DefaultAppstore;-><init>()V

    .line 83
    iput-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps;->activity:Landroid/app/Activity;

    .line 84
    iput-object p2, p0, Lorg/onepf/oms/appstore/SamsungApps;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    return-void
.end method

.method static synthetic access$002(Lorg/onepf/oms/appstore/SamsungApps;Ljava/lang/Boolean;)Ljava/lang/Boolean;
    .locals 0

    .line 66
    iput-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps;->isBillingAvailable:Ljava/lang/Boolean;

    return-object p1
.end method

.method public static checkSku(Ljava/lang/String;)V
    .locals 2
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const-string v0, "/"

    .line 185
    invoke-virtual {p0, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p0

    .line 186
    array-length v0, p0

    const/4 v1, 0x2

    if-ne v0, v1, :cond_2

    const/4 v0, 0x0

    .line 189
    aget-object v0, p0, v0

    const/4 v1, 0x1

    .line 190
    aget-object p0, p0, v1

    .line 191
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_1

    invoke-static {v0}, Landroid/text/TextUtils;->isDigitsOnly(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 194
    invoke-static {p0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result p0

    if-nez p0, :cond_0

    return-void

    .line 195
    :cond_0
    new-instance p0, Lorg/onepf/oms/appstore/SamsungSkuFormatException;

    const-string v0, "Samsung SKU must contain ITEM_ID."

    invoke-direct {p0, v0}, Lorg/onepf/oms/appstore/SamsungSkuFormatException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 192
    :cond_1
    new-instance p0, Lorg/onepf/oms/appstore/SamsungSkuFormatException;

    const-string v0, "Samsung SKU must contain numeric ITEM_GROUP_ID."

    invoke-direct {p0, v0}, Lorg/onepf/oms/appstore/SamsungSkuFormatException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 187
    :cond_2
    new-instance p0, Lorg/onepf/oms/appstore/SamsungSkuFormatException;

    const-string v0, "Samsung SKU must contain ITEM_GROUP_ID and ITEM_ID."

    invoke-direct {p0, v0}, Lorg/onepf/oms/appstore/SamsungSkuFormatException;-><init>(Ljava/lang/String;)V

    throw p0
.end method


# virtual methods
.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    const-string v0, "com.samsung.apps"

    return-object v0
.end method

.method public getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 3

    .line 173
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps;->billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    if-nez v0, :cond_0

    .line 174
    new-instance v0, Lorg/onepf/oms/appstore/SamsungAppsBillingService;

    iget-object v1, p0, Lorg/onepf/oms/appstore/SamsungApps;->activity:Landroid/app/Activity;

    iget-object v2, p0, Lorg/onepf/oms/appstore/SamsungApps;->options:Lorg/onepf/oms/OpenIabHelper$Options;

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;-><init>(Landroid/app/Activity;Lorg/onepf/oms/OpenIabHelper$Options;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps;->billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    .line 176
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps;->billingService:Lorg/onepf/oms/AppstoreInAppBillingService;

    return-object v0
.end method

.method public getPackageVersion(Ljava/lang/String;)I
    .locals 0

    const/4 p1, -0x1

    return p1
.end method

.method public isBillingAvailable(Ljava/lang/String;)Z
    .locals 4

    .line 97
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps;->isBillingAvailable:Ljava/lang/Boolean;

    if-eqz p1, :cond_0

    .line 98
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps;->isBillingAvailable:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1

    .line 101
    :cond_0
    invoke-static {}, Lorg/onepf/oms/util/Utils;->uiThread()Z

    move-result p1

    if-nez p1, :cond_4

    const/4 p1, 0x1

    const/4 v0, 0x0

    .line 108
    :try_start_0
    iget-object v1, p0, Lorg/onepf/oms/appstore/SamsungApps;->activity:Landroid/app/Activity;

    invoke-virtual {v1}, Landroid/app/Activity;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const-string v2, "com.sec.android.iap"

    const/16 v3, 0x80

    .line 109
    invoke-virtual {v1, v2, v3}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    .line 110
    iget-object v1, p0, Lorg/onepf/oms/appstore/SamsungApps;->activity:Landroid/app/Activity;

    invoke-virtual {v1}, Landroid/app/Activity;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const-string v2, "com.sec.android.iap"

    const/16 v3, 0x40

    invoke-virtual {v1, v2, v3}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v1

    iget-object v1, v1, Landroid/content/pm/PackageInfo;->signatures:[Landroid/content/pm/Signature;

    .line 111
    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroid/content/pm/Signature;->hashCode()I

    move-result v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    const v2, 0x7a7eaf4b

    if-ne v1, v2, :cond_1

    const/4 v1, 0x1

    goto :goto_0

    :catch_0
    const-string v1, "isBillingAvailable() Samsung IAP Service is not installed"

    .line 115
    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    :cond_1
    const/4 v1, 0x0

    :goto_0
    if-nez v1, :cond_2

    return v0

    .line 122
    :cond_2
    sget-boolean v1, Lorg/onepf/oms/appstore/SamsungApps;->isSamsungTestMode:Z

    if-eqz v1, :cond_3

    const-string v0, "isBillingAvailable() billing is supported in test mode."

    .line 123
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 124
    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps;->isBillingAvailable:Ljava/lang/Boolean;

    return p1

    .line 128
    :cond_3
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lorg/onepf/oms/appstore/SamsungApps;->isBillingAvailable:Ljava/lang/Boolean;

    .line 129
    new-instance v0, Ljava/util/concurrent/CountDownLatch;

    invoke-direct {v0, p1}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    .line 130
    invoke-virtual {p0}, Lorg/onepf/oms/appstore/SamsungApps;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object p1

    new-instance v1, Lorg/onepf/oms/appstore/SamsungApps$1;

    invoke-direct {v1, p0, v0}, Lorg/onepf/oms/appstore/SamsungApps$1;-><init>(Lorg/onepf/oms/appstore/SamsungApps;Ljava/util/concurrent/CountDownLatch;)V

    invoke-interface {p1, v1}, Lorg/onepf/oms/AppstoreInAppBillingService;->startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V

    .line 158
    :try_start_1
    invoke-virtual {v0}, Ljava/util/concurrent/CountDownLatch;->await()V
    :try_end_1
    .catch Ljava/lang/InterruptedException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_1

    :catch_1
    move-exception p1

    const-string v0, "isBillingAvailable() interrupted"

    .line 160
    invoke-static {v0, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 163
    :goto_1
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps;->isBillingAvailable:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1

    .line 102
    :cond_4
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Must no be called from UI thread."

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 1

    .line 89
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps;->activity:Landroid/app/Activity;

    const-string v0, "com.sec.android.app.samsungapps"

    invoke-static {p1, v0}, Lorg/onepf/oms/util/Utils;->isPackageInstaller(Landroid/content/Context;Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_1

    sget-boolean p1, Lorg/onepf/oms/appstore/SamsungApps;->isSamsungTestMode:Z

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
