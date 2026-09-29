.class Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;
.super Ljava/lang/Object;
.source "NokiaStoreHelper.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->startSetup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 82
    iput-object p1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->this$0:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    iput-object p2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 4

    const-string v0, "NokiaStoreHelper:startSetup.onServiceConnected"

    .line 85
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    .line 86
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "name = "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V

    .line 88
    iget-object p1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->this$0:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    invoke-static {p2}, Lcom/nokia/payment/iap/aidl/INokiaIAPService$Stub;->asInterface(Landroid/os/IBinder;)Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    move-result-object p2

    invoke-static {p1, p2}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->access$002(Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;Lcom/nokia/payment/iap/aidl/INokiaIAPService;)Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    const/4 p1, 0x0

    .line 91
    :try_start_0
    iget-object p2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->this$0:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    invoke-static {p2}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->access$000(Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;)Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    move-result-object p2

    const/4 v0, 0x3

    iget-object v1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->this$0:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    invoke-virtual {v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->getPackageName()Ljava/lang/String;

    move-result-object v1

    const-string v2, "inapp"

    invoke-interface {p2, v0, v1, v2}, Lcom/nokia/payment/iap/aidl/INokiaIAPService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p2

    if-eqz p2, :cond_1

    .line 95
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz v0, :cond_0

    .line 96
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance v1, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v2, "Error checking for billing support."

    invoke-direct {v1, p2, v2}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-interface {v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_0
    return-void

    .line 116
    :cond_1
    iget-object p2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz p2, :cond_2

    .line 117
    iget-object p2, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance v0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const-string v1, "Setup successful."

    invoke-direct {v0, p1, v1}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p2, v0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_2
    return-void

    :catch_0
    move-exception p2

    .line 105
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    if-eqz v0, :cond_3

    .line 106
    iget-object v0, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    new-instance v1, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;

    const/16 v2, -0x3e9

    const-string v3, "RemoteException while setting up in-app billing."

    invoke-direct {v1, v2, v3}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;-><init>(ILjava/lang/String;)V

    invoke-interface {v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_3
    const/4 v0, 0x2

    .line 111
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Exception: "

    aput-object v1, v0, p1

    const/4 p1, 0x1

    aput-object p2, v0, p1

    invoke-static {p2, v0}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/Throwable;[Ljava/lang/Object;)V

    return-void
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 3

    const-string v0, "NokiaStoreHelper:startSetup.onServiceDisconnected"

    .line 125
    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->i(Ljava/lang/String;)V

    const/4 v0, 0x2

    .line 126
    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "name = "

    const/4 v2, 0x0

    aput-object v1, v0, v2

    const/4 v1, 0x1

    aput-object p1, v0, v1

    invoke-static {v0}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    .line 128
    iget-object p1, p0, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper$1;->this$0:Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;

    const/4 v0, 0x0

    invoke-static {p1, v0}, Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;->access$002(Lorg/onepf/oms/appstore/nokiaUtils/NokiaStoreHelper;Lcom/nokia/payment/iap/aidl/INokiaIAPService;)Lcom/nokia/payment/iap/aidl/INokiaIAPService;

    return-void
.end method
