.class public Lorg/onepf/openiab/UnityProxyActivity;
.super Landroid/app/Activity;
.source "UnityProxyActivity.java"


# static fields
.field static final ACTION_FINISH:Ljava/lang/String; = "org.onepf.openiab.ACTION_FINISH"


# instance fields
.field private broadcastReceiver:Landroid/content/BroadcastReceiver;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 33
    invoke-direct {p0}, Landroid/app/Activity;-><init>()V

    return-void
.end method


# virtual methods
.method protected onActivityResult(IILandroid/content/Intent;)V
    .locals 3

    const-string v0, "OpenIAB-UnityPlugin"

    .line 80
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "onActivityResult("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, ", "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, ", "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 83
    invoke-static {}, Lorg/onepf/openiab/UnityPlugin;->instance()Lorg/onepf/openiab/UnityPlugin;

    move-result-object v0

    invoke-virtual {v0}, Lorg/onepf/openiab/UnityPlugin;->getHelper()Lorg/onepf/oms/OpenIabHelper;

    move-result-object v0

    invoke-virtual {v0, p1, p2, p3}, Lorg/onepf/oms/OpenIabHelper;->handleActivityResult(IILandroid/content/Intent;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 87
    invoke-super {p0, p1, p2, p3}, Landroid/app/Activity;->onActivityResult(IILandroid/content/Intent;)V

    goto :goto_0

    :cond_0
    const-string p1, "OpenIAB-UnityPlugin"

    const-string p2, "onActivityResult handled by IABUtil."

    .line 89
    invoke-static {p1, p2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 7

    .line 39
    invoke-super {p0, p1}, Landroid/app/Activity;->onCreate(Landroid/os/Bundle;)V

    .line 41
    new-instance p1, Lorg/onepf/openiab/UnityProxyActivity$1;

    invoke-direct {p1, p0}, Lorg/onepf/openiab/UnityProxyActivity$1;-><init>(Lorg/onepf/openiab/UnityProxyActivity;)V

    iput-object p1, p0, Lorg/onepf/openiab/UnityProxyActivity;->broadcastReceiver:Landroid/content/BroadcastReceiver;

    .line 50
    iget-object p1, p0, Lorg/onepf/openiab/UnityProxyActivity;->broadcastReceiver:Landroid/content/BroadcastReceiver;

    new-instance v0, Landroid/content/IntentFilter;

    const-string v1, "org.onepf.openiab.ACTION_FINISH"

    invoke-direct {v0, v1}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0, p1, v0}, Lorg/onepf/openiab/UnityProxyActivity;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    .line 52
    sget-boolean p1, Lorg/onepf/openiab/UnityPlugin;->sendRequest:Z

    if-eqz p1, :cond_1

    const/4 p1, 0x0

    .line 53
    sput-boolean p1, Lorg/onepf/openiab/UnityPlugin;->sendRequest:Z

    .line 55
    invoke-virtual {p0}, Lorg/onepf/openiab/UnityProxyActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    const-string v0, "sku"

    .line 56
    invoke-virtual {p1, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v0, "developerPayload"

    .line 57
    invoke-virtual {p1, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v0, "inapp"

    const/4 v1, 0x1

    .line 58
    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->getBooleanExtra(Ljava/lang/String;Z)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 62
    :try_start_0
    invoke-static {}, Lorg/onepf/openiab/UnityPlugin;->instance()Lorg/onepf/openiab/UnityPlugin;

    move-result-object p1

    invoke-virtual {p1}, Lorg/onepf/openiab/UnityPlugin;->getHelper()Lorg/onepf/oms/OpenIabHelper;

    move-result-object v1

    const/16 v4, 0x2711

    invoke-static {}, Lorg/onepf/openiab/UnityPlugin;->instance()Lorg/onepf/openiab/UnityPlugin;

    move-result-object p1

    invoke-virtual {p1}, Lorg/onepf/openiab/UnityPlugin;->getPurchaseFinishedListener()Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    move-result-object v5

    move-object v2, p0

    invoke-virtual/range {v1 .. v6}, Lorg/onepf/oms/OpenIabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V

    goto :goto_0

    .line 64
    :cond_0
    invoke-static {}, Lorg/onepf/openiab/UnityPlugin;->instance()Lorg/onepf/openiab/UnityPlugin;

    move-result-object p1

    invoke-virtual {p1}, Lorg/onepf/openiab/UnityPlugin;->getHelper()Lorg/onepf/oms/OpenIabHelper;

    move-result-object v1

    const/16 v4, 0x2711

    invoke-static {}, Lorg/onepf/openiab/UnityPlugin;->instance()Lorg/onepf/openiab/UnityPlugin;

    move-result-object p1

    invoke-virtual {p1}, Lorg/onepf/openiab/UnityPlugin;->getPurchaseFinishedListener()Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    move-result-object v5

    move-object v2, p0

    invoke-virtual/range {v1 .. v6}, Lorg/onepf/oms/OpenIabHelper;->launchSubscriptionPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;ILorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 67
    :catch_0
    invoke-static {}, Lorg/onepf/openiab/UnityPlugin;->instance()Lorg/onepf/openiab/UnityPlugin;

    move-result-object p1

    invoke-virtual {p1}, Lorg/onepf/openiab/UnityPlugin;->getPurchaseFinishedListener()Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;

    move-result-object p1

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v1, 0x3

    const-string v2, "Cannot start purchase process. Billing unavailable."

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    const/4 v1, 0x0

    invoke-interface {p1, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabPurchaseFinishedListener;->onIabPurchaseFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Purchase;)V

    :cond_1
    :goto_0
    return-void
.end method

.method protected onDestroy()V
    .locals 1

    .line 74
    invoke-super {p0}, Landroid/app/Activity;->onDestroy()V

    .line 75
    iget-object v0, p0, Lorg/onepf/openiab/UnityProxyActivity;->broadcastReceiver:Landroid/content/BroadcastReceiver;

    invoke-virtual {p0, v0}, Lorg/onepf/openiab/UnityProxyActivity;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    return-void
.end method
