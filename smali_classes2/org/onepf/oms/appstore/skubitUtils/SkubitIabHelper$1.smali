.class Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;
.super Ljava/lang/Object;
.source "SkubitIabHelper.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 180
    iput-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 4

    const-string v0, "Billing service connected."

    .line 189
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 190
    iget-object v0, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iget-object v1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    invoke-virtual {v1, p2}, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->getServiceFromBinder(Landroid/os/IBinder;)Lcom/skubit/android/billing/IBillingService;

    move-result-object p2

    iput-object p2, v0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    .line 191
    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iput-object p1, p2, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mComponentName:Landroid/content/ComponentName;

    .line 192
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iget-object p1, p1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mContext:Landroid/content/Context;

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p1

    :try_start_0
    const-string p2, "Checking for in-app billing 1 support."

    .line 194
    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 197
    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iget-object p2, p2, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    const-string v0, "inapp"

    const/4 v1, 0x1

    invoke-interface {p2, v1, p1, v0}, Lcom/skubit/android/billing/IBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p2

    const/4 v0, 0x0

    if-eqz p2, :cond_1

    .line 199
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz p1, :cond_0

    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance v1, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v2, "Error checking for billing v1 support."

    invoke-direct {v1, p2, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    .line 203
    :cond_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iput-boolean v0, p1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSubscriptionsSupported:Z

    return-void

    :cond_1
    const/4 p2, 0x2

    .line 206
    new-array v2, p2, [Ljava/lang/Object;

    const-string v3, "In-app billing version 1 supported for "

    aput-object v3, v2, v0

    aput-object p1, v2, v1

    invoke-static {v2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 209
    iget-object v2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iget-object v2, v2, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    const-string v3, "subs"

    invoke-interface {v2, v1, p1, v3}, Lcom/skubit/android/billing/IBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p1

    if-nez p1, :cond_2

    const-string p1, "Subscriptions AVAILABLE."

    .line 211
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 212
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iput-boolean v1, p1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSubscriptionsSupported:Z

    goto :goto_0

    .line 214
    :cond_2
    new-array p2, p2, [Ljava/lang/Object;

    const-string v2, "Subscriptions NOT AVAILABLE. Response: "

    aput-object v2, p2, v0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    aput-object p1, p2, v1

    invoke-static {p2}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 217
    :goto_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    iput-boolean v1, p1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mSetupDone:Z
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    .line 227
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz p1, :cond_3

    .line 228
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const-string v1, "Setup successful."

    invoke-direct {p2, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_3
    return-void

    :catch_0
    move-exception p1

    .line 219
    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz p2, :cond_4

    .line 220
    iget-object p2, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/16 v1, -0x3e9

    const-string v2, "RemoteException while setting up in-app billing."

    invoke-direct {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_4
    const-string p2, "RemoteException while setting up in-app billing"

    .line 223
    invoke-static {p2, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 1

    const-string p1, "Billing service disconnected."

    .line 183
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 184
    iget-object p1, p0, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper$1;->this$0:Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;

    const/4 v0, 0x0

    iput-object v0, p1, Lorg/onepf/oms/appstore/skubitUtils/SkubitIabHelper;->mService:Lcom/skubit/android/billing/IBillingService;

    return-void
.end method
