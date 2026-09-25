.class public final Lnet/gogame/gopay/sdk/iab/GoPayHelper;
.super Ljava/lang/Object;


# static fields
.field private static a:Lnet/gogame/gopay/sdk/iab/GoGameStore; = null

.field private static b:Z = false

.field private static c:Ljava/lang/String;

.field private static d:Ljava/lang/String;

.field private static e:Ljava/lang/String;

.field private static f:Ljava/lang/String;

.field private static g:Ljava/lang/String;

.field private static h:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getEmail()Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->f:Ljava/lang/String;

    return-object v0
.end method

.method public static getGameLanguage()Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->g:Ljava/lang/String;

    return-object v0
.end method

.method public static getGoPayAppId()Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->c:Ljava/lang/String;

    return-object v0
.end method

.method public static getGoPayAppSecret()Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->d:Ljava/lang/String;

    return-object v0
.end method

.method public static getGooglePlayPublicKey()Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->h:Ljava/lang/String;

    return-object v0
.end method

.method public static getGuid()Ljava/lang/String;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->e:Ljava/lang/String;

    return-object v0
.end method

.method public static getStoreInstance()Lnet/gogame/gopay/sdk/iab/GoGameStore;
    .locals 1

    sget-object v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->a:Lnet/gogame/gopay/sdk/iab/GoGameStore;

    return-object v0
.end method

.method public static isDisable3rdParty()Z
    .locals 1

    sget-boolean v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->b:Z

    return v0
.end method

.method public static newOpenIabHelper(Landroid/content/Context;)Lorg/onepf/oms/OpenIabHelper;
    .locals 13

    sget-boolean v0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->b:Z

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz v0, :cond_0

    new-instance v0, Lorg/onepf/oms/OpenIabHelper;

    new-instance v3, Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    invoke-direct {v3}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;-><init>()V

    const-string v4, "com.google.play"

    filled-new-array {v4}, [Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStoreNames([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object v3

    new-array v4, v2, [Lorg/onepf/oms/Appstore;

    new-instance v5, Lorg/onepf/oms/appstore/GooglePlay;

    sget-object v6, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->h:Ljava/lang/String;

    invoke-direct {v5, p0, v6}, Lorg/onepf/oms/appstore/GooglePlay;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    aput-object v5, v4, v1

    invoke-virtual {v3, v4}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStores([Lorg/onepf/oms/Appstore;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object v1

    invoke-virtual {v1, v2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->setStoreSearchStrategy(I)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object v1

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->build()Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object v1

    invoke-direct {v0, p0, v1}, Lorg/onepf/oms/OpenIabHelper;-><init>(Landroid/content/Context;Lorg/onepf/oms/OpenIabHelper$Options;)V

    return-object v0

    :cond_0
    new-instance v0, Lorg/onepf/oms/OpenIabHelper;

    new-instance v3, Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    invoke-direct {v3}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;-><init>()V

    const-string v4, "GoGameStore"

    filled-new-array {v4}, [Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStoreNames([Ljava/lang/String;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object v3

    new-array v4, v2, [Lorg/onepf/oms/Appstore;

    new-instance v12, Lnet/gogame/gopay/sdk/iab/GoGameStore;

    sget-object v7, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->c:Ljava/lang/String;

    sget-object v8, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->e:Ljava/lang/String;

    sget-object v9, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->d:Ljava/lang/String;

    sget-object v10, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->f:Ljava/lang/String;

    sget-object v11, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->g:Ljava/lang/String;

    move-object v5, v12

    move-object v6, p0

    invoke-direct/range {v5 .. v11}, Lnet/gogame/gopay/sdk/iab/GoGameStore;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    sput-object v12, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->a:Lnet/gogame/gopay/sdk/iab/GoGameStore;

    aput-object v12, v4, v1

    invoke-virtual {v3, v4}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->addAvailableStores([Lorg/onepf/oms/Appstore;)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object v3

    invoke-virtual {v3, v1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->setCheckInventory(Z)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object v1

    invoke-virtual {v1, v2}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->setStoreSearchStrategy(I)Lorg/onepf/oms/OpenIabHelper$Options$Builder;

    move-result-object v1

    invoke-virtual {v1}, Lorg/onepf/oms/OpenIabHelper$Options$Builder;->build()Lorg/onepf/oms/OpenIabHelper$Options;

    move-result-object v1

    invoke-direct {v0, p0, v1}, Lorg/onepf/oms/OpenIabHelper;-><init>(Landroid/content/Context;Lorg/onepf/oms/OpenIabHelper$Options;)V

    return-object v0
.end method

.method public static setDisable3rdParty(Z)V
    .locals 0

    sput-boolean p0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->b:Z

    return-void
.end method

.method public static setEmail(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->f:Ljava/lang/String;

    return-void
.end method

.method public static setGameLanguage(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->g:Ljava/lang/String;

    return-void
.end method

.method public static setGoPayAppId(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->c:Ljava/lang/String;

    return-void
.end method

.method public static setGoPayAppSecret(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->d:Ljava/lang/String;

    return-void
.end method

.method public static setGooglePlayPublicKey(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->h:Ljava/lang/String;

    return-void
.end method

.method public static setGuid(Ljava/lang/String;)V
    .locals 0

    sput-object p0, Lnet/gogame/gopay/sdk/iab/GoPayHelper;->e:Ljava/lang/String;

    return-void
.end method
