.class Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;
.super Ljava/lang/Object;
.source "IabHelper.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/googleUtils/IabHelper;->startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/googleUtils/IabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 219
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 5

    const-string v0, "Billing service connected."

    .line 228
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 229
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    invoke-virtual {v1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->getServiceFromBinder(Landroid/os/IBinder;)Lcom/android/vending/billing/IInAppBillingService;

    move-result-object p2

    iput-object p2, v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    .line 230
    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iput-object p1, p2, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->componentName:Landroid/content/ComponentName;

    .line 231
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iget-object p1, p1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mContext:Landroid/content/Context;

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p1

    :try_start_0
    const-string p2, "Checking for in-app billing 3 support."

    .line 233
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 236
    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iget-object p2, p2, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    const-string v0, "inapp"

    const/4 v1, 0x3

    invoke-interface {p2, v1, p1, v0}, Lcom/android/vending/billing/IInAppBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p2

    const/4 v0, 0x0

    if-eqz p2, :cond_1

    .line 238
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz p1, :cond_0

    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v2, "Error checking for billing v3 support."

    invoke-direct {v1, p2, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    .line 242
    :cond_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iput-boolean v0, p1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSubscriptionsSupported:Z

    return-void

    :cond_1
    const/4 p2, 0x2

    .line 245
    new-array v2, p2, [Ljava/lang/Object;

    const-string v3, "In-app billing version 3 supported for "

    aput-object v3, v2, v0

    const/4 v3, 0x1

    aput-object p1, v2, v3

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 248
    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iget-object v2, v2, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    const-string v4, "subs"

    invoke-interface {v2, v1, p1, v4}, Lcom/android/vending/billing/IInAppBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p1

    if-nez p1, :cond_2

    const-string p1, "Subscriptions AVAILABLE."

    .line 250
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 251
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iput-boolean v3, p1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSubscriptionsSupported:Z

    goto :goto_0

    .line 253
    :cond_2
    new-array p2, p2, [Ljava/lang/Object;

    const-string v1, "Subscriptions NOT AVAILABLE. Response: "

    aput-object v1, p2, v0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, p2, v3

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 256
    :goto_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    iput-boolean v3, p1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mSetupDone:Z
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    .line 266
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz p1, :cond_3

    .line 267
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v1, "Setup successful."

    invoke-direct {p2, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    const-string p1, "Setup successful."

    .line 268
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    :cond_3
    return-void

    :catch_0
    move-exception p1

    .line 258
    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz p2, :cond_4

    .line 259
    iget-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v1, -0x3e9

    const-string v2, "RemoteException while setting up in-app billing."

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_4
    const-string p2, "RemoteException while setting up in-app billing"

    .line 262
    invoke-static {p2, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 1

    const-string p1, "Billing service disconnected."

    .line 222
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 223
    iget-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$1;->this$0:Lorg/onepf/oms/appstore/googleUtils/IabHelper;

    const/4 v0, 0x0

    iput-object v0, p1, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    return-void
.end method
