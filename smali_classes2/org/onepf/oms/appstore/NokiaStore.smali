.class public Lorg/onepf/oms/appstore/NokiaStore;
.super Lorg/onepf/oms/DefaultAppstore;
.source "NokiaStore.java"


# static fields
.field private static final EXPECTED_SHA1_FINGERPRINT:Ljava/lang/String; = "C476A7D71C4CB92641A699C1F1CAC93CA81E0396"

.field public static final NOKIA_BILLING_PERMISSION:Ljava/lang/String; = "com.nokia.payment.BILLING"

.field public static final NOKIA_INSTALLER:Ljava/lang/String; = "com.nokia.payment.iapenabler"

.field public static final VENDING_ACTION:Ljava/lang/String; = "com.nokia.payment.iapenabler.InAppBillingService.BIND"


# instance fields
.field private billingService:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation
.end field

.field private final context:Landroid/content/Context;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1

    .line 53
    invoke-direct {p0}, Lorg/onepf/oms/DefaultAppstore;-><init>()V

    const/4 v0, 0x0

    .line 44
    iput-object v0, p0, Lorg/onepf/oms/appstore/NokiaStore;->billingService:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    const-string v0, "NokiaStore.NokiaStore"

    .line 54
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 56
    iput-object p1, p0, Lorg/onepf/oms/appstore/NokiaStore;->context:Landroid/content/Context;

    return-void
.end method

.method public static checkSku(Ljava/lang/String;)V
    .locals 0
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 156
    invoke-static {p0}, Landroid/text/TextUtils;->isDigitsOnly(Ljava/lang/CharSequence;)Z

    move-result p0

    if-eqz p0, :cond_0

    return-void

    .line 157
    :cond_0
    new-instance p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaSkuFormatException;

    invoke-direct {p0}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaSkuFormatException;-><init>()V

    throw p0
.end method

.method private static hexStringToByteArray(Ljava/lang/String;)[B
    .locals 7
    .param p0    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    .line 163
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    .line 164
    div-int/lit8 v1, v0, 0x2

    new-array v1, v1, [B

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_0

    .line 166
    div-int/lit8 v3, v2, 0x2

    invoke-virtual {p0, v2}, Ljava/lang/String;->charAt(I)C

    move-result v4

    const/16 v5, 0x10

    invoke-static {v4, v5}, Ljava/lang/Character;->digit(CI)I

    move-result v4

    shl-int/lit8 v4, v4, 0x4

    add-int/lit8 v6, v2, 0x1

    invoke-virtual {p0, v6}, Ljava/lang/String;->charAt(I)C

    move-result v6

    invoke-static {v6, v5}, Ljava/lang/Character;->digit(CI)I

    move-result v5

    add-int/2addr v4, v5

    int-to-byte v4, v4

    aput-byte v4, v1, v3

    add-int/lit8 v2, v2, 0x2

    goto :goto_0

    :cond_0
    return-object v1
.end method

.method private verifyFingreprint()Z
    .locals 4

    const/4 v0, 0x0

    .line 124
    :try_start_0
    iget-object v1, p0, Lorg/onepf/oms/appstore/NokiaStore;->context:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const-string v2, "com.nokia.payment.iapenabler"

    const/16 v3, 0x40

    invoke-virtual {v1, v2, v3}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v1

    .line 128
    iget-object v2, v1, Landroid/content/pm/PackageInfo;->signatures:[Landroid/content/pm/Signature;

    array-length v2, v2

    const/4 v3, 0x1

    if-ne v2, v3, :cond_0

    .line 129
    iget-object v1, v1, Landroid/content/pm/PackageInfo;->signatures:[Landroid/content/pm/Signature;

    aget-object v1, v1, v0

    invoke-virtual {v1}, Landroid/content/pm/Signature;->toByteArray()[B

    move-result-object v1

    const-string v2, "SHA1"

    .line 131
    invoke-static {v2}, Ljava/security/MessageDigest;->getInstance(Ljava/lang/String;)Ljava/security/MessageDigest;

    move-result-object v2

    .line 132
    invoke-virtual {v2, v1}, Ljava/security/MessageDigest;->digest([B)[B

    move-result-object v1

    const-string v2, "C476A7D71C4CB92641A699C1F1CAC93CA81E0396"

    .line 133
    invoke-static {v2}, Lorg/onepf/oms/appstore/NokiaStore;->hexStringToByteArray(Ljava/lang/String;)[B

    move-result-object v2

    .line 135
    invoke-static {v1, v2}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v1, 0x2

    .line 136
    new-array v1, v1, [Ljava/lang/Object;

    const-string v2, "isBillingAvailable"

    aput-object v2, v1, v0

    const-string v2, "NIAP signature verified"

    aput-object v2, v1, v3

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->i([Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/security/NoSuchAlgorithmException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    return v3

    :catch_0
    move-exception v1

    .line 143
    invoke-virtual {v1}, Landroid/content/pm/PackageManager$NameNotFoundException;->printStackTrace()V

    goto :goto_0

    :catch_1
    move-exception v1

    .line 141
    invoke-virtual {v1}, Ljava/security/NoSuchAlgorithmException;->printStackTrace()V

    :cond_0
    :goto_0
    return v0
.end method


# virtual methods
.method public getAppstoreName()Ljava/lang/String;
    .locals 1

    const-string v0, "com.nokia.nstore"

    return-object v0
.end method

.method public getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;
    .locals 2
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 110
    iget-object v0, p0, Lorg/onepf/oms/appstore/NokiaStore;->billingService:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    if-nez v0, :cond_0

    .line 111
    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    iget-object v1, p0, Lorg/onepf/oms/appstore/NokiaStore;->context:Landroid/content/Context;

    invoke-direct {v0, v1, p0}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;-><init>(Landroid/content/Context;Lorg/onepf/oms/Appstore;)V

    iput-object v0, p0, Lorg/onepf/oms/appstore/NokiaStore;->billingService:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    .line 113
    :cond_0
    iget-object v0, p0, Lorg/onepf/oms/appstore/NokiaStore;->billingService:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    return-object v0
.end method

.method public getPackageVersion(Ljava/lang/String;)I
    .locals 2

    .line 98
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "getPackageVersion: packageName = "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    const/4 p1, -0x1

    return p1
.end method

.method public isBillingAvailable(Ljava/lang/String;)Z
    .locals 3

    const-string v0, "NokiaStore.isBillingAvailable"

    .line 65
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    const/4 v0, 0x2

    .line 66
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "packageName = "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    const/4 v1, 0x1

    aput-object p1, v0, v1

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 68
    iget-object p1, p0, Lorg/onepf/oms/appstore/NokiaStore;->context:Landroid/content/Context;

    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p1

    .line 69
    invoke-virtual {p1, v2}, Landroid/content/pm/PackageManager;->getInstalledPackages(I)Ljava/util/List;

    move-result-object p1

    .line 71
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/pm/PackageInfo;

    const-string v1, "com.nokia.payment.iapenabler"

    .line 73
    iget-object v0, v0, Landroid/content/pm/PackageInfo;->packageName:Ljava/lang/String;

    invoke-virtual {v1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 74
    invoke-direct {p0}, Lorg/onepf/oms/appstore/NokiaStore;->verifyFingreprint()Z

    move-result p1

    return p1

    :cond_1
    return v2
.end method

.method public isPackageInstaller(Ljava/lang/String;)Z
    .locals 4

    const/4 v0, 0x2

    .line 86
    new-array v1, v0, [Ljava/lang/Object;

    const-string v2, "sPackageInstaller: packageName = "

    const/4 v3, 0x0

    aput-object v2, v1, v3

    const/4 v2, 0x1

    aput-object p1, v1, v2

    invoke-static {v1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 88
    iget-object v1, p0, Lorg/onepf/oms/appstore/NokiaStore;->context:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 89
    invoke-virtual {v1, p1}, Landroid/content/pm/PackageManager;->getInstallerPackageName(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 91
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "installerPackageName = "

    aput-object v1, v0, v3

    aput-object p1, v0, v2

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    const-string v0, "com.nokia.payment.iapenabler"

    .line 93
    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method
