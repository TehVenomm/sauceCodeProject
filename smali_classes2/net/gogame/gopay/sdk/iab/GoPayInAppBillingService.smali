.class public Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;
.super Ljava/lang/Object;

# interfaces
.implements Lnet/gogame/gopay/sdk/GoPayInAppBillingServiceExt;
.implements Lorg/onepf/oms/AppstoreInAppBillingService;


# instance fields
.field private final a:Ljava/lang/String;

.field private b:Ljava/lang/String;

.field private final c:Ljava/lang/String;

.field private final d:Ljava/util/Map;

.field private final e:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
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

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->d:Ljava/util/Map;

    const/4 v0, 0x0

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->e:Z

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a:Ljava/lang/String;

    iput-object p3, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    const/4 p2, 0x0

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->c:Ljava/lang/String;

    invoke-static {p5}, Lnet/gogame/gopay/sdk/j;->b(Ljava/lang/String;)V

    invoke-static {p4}, Lnet/gogame/gopay/sdk/j;->c(Ljava/lang/String;)V

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Lnet/gogame/gopay/sdk/j;->e(Ljava/lang/String;)V

    invoke-virtual {p1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p2

    const-string p3, "android_id"

    invoke-static {p2, p3}, Landroid/provider/Settings$Secure;->getString(Landroid/content/ContentResolver;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Lnet/gogame/gopay/sdk/j;->d(Ljava/lang/String;)V

    :try_start_0
    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p1

    const/16 p3, 0x80

    invoke-virtual {p1, p2, p3}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object p1

    iget-object p1, p1, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->f(Ljava/lang/String;)V
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const-string p1, "0"

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->f(Ljava/lang/String;)V

    :goto_0
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a()V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->d:Ljava/util/Map;

    const/4 v0, 0x0

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->e:Z

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    const/4 p1, 0x0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->c:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .param p1    # Ljava/lang/String;
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
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->d:Ljava/util/Map;

    const/4 v0, 0x0

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->e:Z

    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    iput-object p3, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->c:Ljava/lang/String;

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a:Ljava/lang/String;

    return-object p0
.end method

.method private a()V
    .locals 2

    new-instance v0, Lnet/gogame/gopay/sdk/iab/d;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/iab/d;-><init>(Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;)V

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Void;

    invoke-virtual {v0, v1}, Lnet/gogame/gopay/sdk/iab/d;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    return-object p0
.end method


# virtual methods
.method public consume(Lorg/onepf/oms/appstore/googleUtils/Purchase;)V
    .locals 2

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v0, -0x3f1

    const/4 v1, 0x0

    invoke-direct {p1, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw p1
.end method

.method public dispose()V
    .locals 0

    return-void
.end method

.method public getCountries()Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;
    .locals 3

    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a:Ljava/lang/String;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {v0, v1, v2}, Lnet/gogame/gopay/sdk/j;->b(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v2, -0x3f0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v0

    invoke-direct {v1, v2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw v1
.end method

.method public getCountries(Ljava/lang/String;)Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;
    .locals 2

    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a:Ljava/lang/String;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    invoke-static {v0, v1, p1}, Lnet/gogame/gopay/sdk/j;->b(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/GetCountryDetailsResponse;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabException;

    const/16 v1, -0x3f0

    invoke-virtual {p1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, v1, p1}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(ILjava/lang/String;)V

    throw v0
.end method

.method public getCurrentCountry()Ljava/lang/String;
    .locals 1

    invoke-static {}, Lnet/gogame/gopay/sdk/j;->a()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getPurchaseFlowIntent(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;
    .locals 2

    new-instance v0, Landroid/content/Intent;

    const-class v1, Lnet/gogame/gopay/sdk/iab/PurchaseActivity;

    invoke-direct {v0, p1, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string p1, "gid"

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a:Ljava/lang/String;

    invoke-virtual {v0, p1, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "guid"

    iget-object v1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    invoke-virtual {v0, p1, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "payload"

    invoke-virtual {v0, p1, p4}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "sku"

    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "itemType"

    invoke-virtual {v0, p1, p3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "referenceId"

    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object p2

    invoke-virtual {p2}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object p2

    const-string p3, "-"

    const-string p4, ""

    invoke-virtual {p2, p3, p4}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    return-object v0
.end method

.method public handleActivityResult(IILandroid/content/Intent;)Z
    .locals 10

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->d:Ljava/util/Map;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gopay/sdk/iab/e;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    :cond_0
    iget-object v2, v0, Lnet/gogame/gopay/sdk/iab/e;->b:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    const/4 v3, 0x1

    const/16 v4, -0x3ea

    const/4 v5, 0x0

    if-nez p3, :cond_2

    if-eqz v2, :cond_1

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string p3, "Bad response!"

    invoke-direct {p2, v4, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    :goto_0
    invoke-interface {v2, p2, v5}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_1
    :goto_1
    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->d:Ljava/util/Map;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-interface {p2, p1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return v3

    :cond_2
    invoke-virtual {p3}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v6

    const-string v7, "RESPONSE_CODE"

    invoke-virtual {v6, v7}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v6

    const-string v7, "INAPP_PURCHASE_DATA"

    invoke-virtual {p3, v7}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const-string v8, "INAPP_DATA_SIGNATURE"

    invoke-virtual {p3, v8}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    const/4 v9, -0x1

    if-ne p2, v9, :cond_5

    if-nez v7, :cond_3

    new-instance p2, Ljava/lang/StringBuilder;

    const-string v0, "Extras: "

    invoke-direct {p2, v0}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p3}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object p3

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    if-eqz v2, :cond_1

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3f0

    const-string v0, "IAB returned null purchaseData or dataSignature"

    invoke-direct {p2, p3, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto :goto_0

    :cond_3
    :try_start_0
    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    iget-object p3, v0, Lnet/gogame/gopay/sdk/iab/e;->a:Ljava/lang/String;

    const-string v0, "GoGameStore"

    invoke-direct {p2, p3, v7, v8, v0}, Lorg/onepf/oms/appstore/googleUtils/Purchase;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    :catch_0
    move-exception p2

    invoke-virtual {p2}, Lorg/json/JSONException;->printStackTrace()V

    if-eqz v2, :cond_4

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string p3, "Failed to parse purchase data."

    invoke-direct {p2, v4, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {v2, p2, v5}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_4
    move-object p2, v5

    :goto_2
    if-eqz v2, :cond_1

    new-instance p3, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v0, "Success"

    invoke-direct {p3, v1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {v2, p3, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    goto :goto_1

    :cond_5
    if-nez p2, :cond_6

    new-instance p2, Ljava/lang/StringBuilder;

    const-string p3, "Purchase canceled - Response: "

    invoke-direct {p2, p3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-static {v6}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p2, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-eqz v2, :cond_1

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3ed

    const-string v0, "User canceled."

    invoke-direct {p2, p3, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto/16 :goto_0

    :cond_6
    new-instance p3, Ljava/lang/StringBuilder;

    const-string v0, "In-app billing error: Purchase failed. Result code: "

    invoke-direct {p3, v0}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-static {p2}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p2, ". Response: "

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v6}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getResponseDesc(I)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p3, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-eqz v2, :cond_1

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p3, -0x3ee

    const-string v0, "Unknown purchase response."

    invoke-direct {p2, p3, v0}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    goto/16 :goto_0
.end method

.method public isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method public launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    .locals 3

    :try_start_0
    invoke-virtual {p0, p1, p2, p3, p6}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->getPurchaseFlowIntent(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p6

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->d:Ljava/util/Map;

    invoke-static {p4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    new-instance v2, Lnet/gogame/gopay/sdk/iab/e;

    invoke-direct {v2, p2, p3, p5}, Lnet/gogame/gopay/sdk/iab/e;-><init>(Ljava/lang/String;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;)V

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    invoke-virtual {p1, p6, p4}, Landroid/app/Activity;->startActivityForResult(Landroid/content/Intent;I)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    if-eqz p5, :cond_0

    new-instance p1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 p2, -0x3ec

    const-string p3, "Failed to send intent."

    invoke-direct {p1, p2, p3}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    const/4 p2, 0x0

    invoke-interface {p5, p1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_0
    return-void
.end method

.method public queryInventory(ZLjava/util/List;Ljava/util/List;)Lorg/onepf/oms/appstore/googleUtils/Inventory;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    iget-object p1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a:Ljava/lang/String;

    iget-object p2, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    const/4 p3, 0x0

    invoke-static {p1, p2, p3}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gopay/sdk/h;

    move-result-object p1

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {p2}, Lorg/onepf/oms/appstore/googleUtils/Inventory;-><init>()V

    iget-object p3, p1, Lnet/gogame/gopay/sdk/h;->b:Ljava/util/List;

    if-eqz p3, :cond_1

    iget-object p3, p1, Lnet/gogame/gopay/sdk/h;->b:Ljava/util/List;

    invoke-interface {p3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p3

    :goto_0
    invoke-interface {p3}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {p3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lorg/onepf/oms/appstore/googleUtils/SkuDetails;

    invoke-virtual {p2, v0}, Lorg/onepf/oms/appstore/googleUtils/Inventory;->addSkuDetails(Lorg/onepf/oms/appstore/googleUtils/SkuDetails;)V

    goto :goto_0

    :cond_0
    iget-object p1, p1, Lnet/gogame/gopay/sdk/h;->a:Ljava/lang/String;

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->setCurrentCountry(Ljava/lang/String;)V

    :cond_1
    return-object p2
.end method

.method public setCurrentCountry(Ljava/lang/String;)V
    .locals 0

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;)V

    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 0

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->c(Ljava/lang/String;)V

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->a()V

    return-void
.end method

.method public setGameLanguage(Ljava/lang/String;)V
    .locals 0

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->g(Ljava/lang/String;)V

    return-void
.end method

.method public setGameUserId(Ljava/lang/String;)V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    iput-object p1, p0, Lnet/gogame/gopay/sdk/iab/GoPayInAppBillingService;->b:Ljava/lang/String;

    return-void
.end method

.method public startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 3

    if-eqz p1, :cond_0

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v1, 0x0

    const-string v2, "Setup successful."

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_0
    return-void
.end method

.method public subscriptionsSupported()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method
