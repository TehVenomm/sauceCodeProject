.class Lorg/onepf/oms/appstore/SamsungAppsBillingService$1;
.super Ljava/lang/Object;
.source "SamsungAppsBillingService.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/SamsungAppsBillingService;->bindIapService()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/SamsungAppsBillingService;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/SamsungAppsBillingService;)V
    .locals 0

    .line 366
    iput-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService$1;->this$0:Lorg/onepf/oms/appstore/SamsungAppsBillingService;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 2

    .line 369
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService$1;->this$0:Lorg/onepf/oms/appstore/SamsungAppsBillingService;

    invoke-static {p2}, Lcom/sec/android/iap/IAPConnector$Stub;->asInterface(Landroid/os/IBinder;)Lcom/sec/android/iap/IAPConnector;

    move-result-object p2

    invoke-static {p1, p2}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->access$002(Lorg/onepf/oms/appstore/SamsungAppsBillingService;Lcom/sec/android/iap/IAPConnector;)Lcom/sec/android/iap/IAPConnector;

    .line 370
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService$1;->this$0:Lorg/onepf/oms/appstore/SamsungAppsBillingService;

    invoke-static {p1}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->access$000(Lorg/onepf/oms/appstore/SamsungAppsBillingService;)Lcom/sec/android/iap/IAPConnector;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 371
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService$1;->this$0:Lorg/onepf/oms/appstore/SamsungAppsBillingService;

    invoke-static {p1}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->access$100(Lorg/onepf/oms/appstore/SamsungAppsBillingService;)V

    goto :goto_0

    .line 373
    :cond_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungAppsBillingService$1;->this$0:Lorg/onepf/oms/appstore/SamsungAppsBillingService;

    invoke-static {p1}, Lorg/onepf/oms/appstore/SamsungAppsBillingService;->access$200(Lorg/onepf/oms/appstore/SamsungAppsBillingService;)Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    move-result-object p1

    new-instance p2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    const/4 v0, 0x6

    const-string v1, "IAP service bind failed"

    invoke-direct {p2, v0, v1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {p1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :goto_0
    return-void
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 0

    return-void
.end method
