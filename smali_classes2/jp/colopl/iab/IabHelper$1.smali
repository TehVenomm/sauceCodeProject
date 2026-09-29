.class Ljp/colopl/iab/IabHelper$1;
.super Ljava/lang/Object;
.source "IabHelper.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/iab/IabHelper;->startSetup(Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/iab/IabHelper;

.field final synthetic val$listener:Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;


# direct methods
.method constructor <init>(Ljp/colopl/iab/IabHelper;Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 213
    iput-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iput-object p2, p0, Ljp/colopl/iab/IabHelper$1;->val$listener:Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 6

    .line 222
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    const-string v0, "Billing service connected."

    invoke-virtual {p1, v0}, Ljp/colopl/iab/IabHelper;->logDebug(Ljava/lang/String;)V

    .line 223
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    invoke-static {p2}, Lcom/android/vending/billing/IInAppBillingService$Stub;->asInterface(Landroid/os/IBinder;)Lcom/android/vending/billing/IInAppBillingService;

    move-result-object p2

    iput-object p2, p1, Ljp/colopl/iab/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    .line 224
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iget-object p1, p1, Ljp/colopl/iab/IabHelper;->mContext:Landroid/content/Context;

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p1

    .line 227
    :try_start_0
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    const-string v0, "Checking for in-app billing 3 support."

    invoke-virtual {p2, v0}, Ljp/colopl/iab/IabHelper;->logDebug(Ljava/lang/String;)V

    .line 230
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iget-object p2, p2, Ljp/colopl/iab/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    const-string v0, "inapp"

    const/4 v1, 0x6

    invoke-interface {p2, v1, p1, v0}, Lcom/android/vending/billing/IInAppBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p2

    const/4 v0, 0x0

    const/4 v2, 0x3

    const/4 v3, 0x1

    if-eqz p2, :cond_1

    .line 232
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iget-object p2, p2, Ljp/colopl/iab/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    const-string v4, "inapp"

    invoke-interface {p2, v2, p1, v4}, Lcom/android/vending/billing/IInAppBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p2

    if-eqz p2, :cond_2

    .line 234
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->val$listener:Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;

    if-eqz p1, :cond_0

    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->val$listener:Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;

    new-instance v1, Ljp/colopl/iab/IabResult;

    const-string v2, "Error checking for billing v3 support."

    invoke-direct {v1, p2, v2}, Ljp/colopl/iab/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, v1}, Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Ljp/colopl/iab/IabResult;)V

    .line 238
    :cond_0
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iput-boolean v0, p1, Ljp/colopl/iab/IabHelper;->mSubscriptionsSupported:Z

    return-void

    .line 243
    :cond_1
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iput-boolean v3, p2, Ljp/colopl/iab/IabHelper;->mV6IAPSupported:Z

    .line 245
    :cond_2
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "In-app billing version "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v5, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iget-boolean v5, v5, Ljp/colopl/iab/IabHelper;->mV6IAPSupported:Z

    if-eqz v5, :cond_3

    goto :goto_0

    :cond_3
    const/4 v1, 0x3

    :goto_0
    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, " supported for "

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p2, v1}, Ljp/colopl/iab/IabHelper;->logDebug(Ljava/lang/String;)V

    .line 248
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iget-object p2, p2, Ljp/colopl/iab/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    const-string v1, "subs"

    invoke-interface {p2, v2, p1, v1}, Lcom/android/vending/billing/IInAppBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p1

    if-nez p1, :cond_4

    .line 250
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    const-string p2, "Subscriptions AVAILABLE."

    invoke-virtual {p1, p2}, Ljp/colopl/iab/IabHelper;->logDebug(Ljava/lang/String;)V

    .line 251
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iput-boolean v3, p1, Ljp/colopl/iab/IabHelper;->mSubscriptionsSupported:Z

    goto :goto_1

    .line 254
    :cond_4
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Subscriptions NOT AVAILABLE. Response: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Ljp/colopl/iab/IabHelper;->logDebug(Ljava/lang/String;)V

    .line 257
    :goto_1
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    iput-boolean v3, p1, Ljp/colopl/iab/IabHelper;->mSetupDone:Z
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    .line 268
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->val$listener:Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;

    if-eqz p1, :cond_5

    .line 269
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->val$listener:Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;

    new-instance p2, Ljp/colopl/iab/IabResult;

    const-string v1, "Setup successful."

    invoke-direct {p2, v0, v1}, Ljp/colopl/iab/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, p2}, Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Ljp/colopl/iab/IabResult;)V

    :cond_5
    return-void

    :catch_0
    move-exception p1

    .line 260
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->val$listener:Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;

    if-eqz p2, :cond_6

    .line 261
    iget-object p2, p0, Ljp/colopl/iab/IabHelper$1;->val$listener:Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;

    new-instance v0, Ljp/colopl/iab/IabResult;

    const/16 v1, -0x3e9

    const-string v2, "RemoteException while setting up in-app billing."

    invoke-direct {v0, v1, v2}, Ljp/colopl/iab/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p2, v0}, Ljp/colopl/iab/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Ljp/colopl/iab/IabResult;)V

    .line 264
    :cond_6
    invoke-virtual {p1}, Landroid/os/RemoteException;->printStackTrace()V

    return-void
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 1

    .line 216
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    const-string v0, "Billing service disconnected."

    invoke-virtual {p1, v0}, Ljp/colopl/iab/IabHelper;->logDebug(Ljava/lang/String;)V

    .line 217
    iget-object p1, p0, Ljp/colopl/iab/IabHelper$1;->this$0:Ljp/colopl/iab/IabHelper;

    const/4 v0, 0x0

    iput-object v0, p1, Ljp/colopl/iab/IabHelper;->mService:Lcom/android/vending/billing/IInAppBillingService;

    return-void
.end method
