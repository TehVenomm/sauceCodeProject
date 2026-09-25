.class Lorg/onepf/oms/appstore/OpenAppstore$1;
.super Lorg/onepf/oms/appstore/googleUtils/IabHelper;
.source "OpenAppstore.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/OpenAppstore;-><init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/IOpenAppstore;Landroid/content/Intent;Ljava/lang/String;Landroid/content/ServiceConnection;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/OpenAppstore;

.field final synthetic val$billingIntent:Landroid/content/Intent;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/OpenAppstore;Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;Landroid/content/Intent;)V
    .locals 0

    .line 66
    iput-object p1, p0, Lorg/onepf/oms/appstore/OpenAppstore$1;->this$0:Lorg/onepf/oms/appstore/OpenAppstore;

    iput-object p5, p0, Lorg/onepf/oms/appstore/OpenAppstore$1;->val$billingIntent:Landroid/content/Intent;

    invoke-direct {p0, p2, p3, p4}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;-><init>(Landroid/content/Context;Ljava/lang/String;Lorg/onepf/oms/Appstore;)V

    return-void
.end method


# virtual methods
.method public dispose()V
    .locals 2

    .line 81
    invoke-super {p0}, Lorg/onepf/oms/appstore/googleUtils/IabHelper;->dispose()V

    .line 82
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore$1;->this$0:Lorg/onepf/oms/appstore/OpenAppstore;

    invoke-static {v0}, Lorg/onepf/oms/appstore/OpenAppstore;->access$200(Lorg/onepf/oms/appstore/OpenAppstore;)Landroid/content/Context;

    move-result-object v0

    iget-object v1, p0, Lorg/onepf/oms/appstore/OpenAppstore$1;->this$0:Lorg/onepf/oms/appstore/OpenAppstore;

    invoke-static {v1}, Lorg/onepf/oms/appstore/OpenAppstore;->access$100(Lorg/onepf/oms/appstore/OpenAppstore;)Landroid/content/ServiceConnection;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    return-void
.end method

.method protected getServiceFromBinder(Landroid/os/IBinder;)Lcom/android/vending/billing/IInAppBillingService;
    .locals 2
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 76
    new-instance v0, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;

    invoke-static {p1}, Lorg/onepf/oms/IOpenInAppBillingService$Stub;->asInterface(Landroid/os/IBinder;)Lorg/onepf/oms/IOpenInAppBillingService;

    move-result-object p1

    const/4 v1, 0x0

    invoke-direct {v0, p1, v1}, Lorg/onepf/oms/appstore/OpenAppstore$IOpenInAppBillingWrapper;-><init>(Lorg/onepf/oms/IOpenInAppBillingService;Lorg/onepf/oms/appstore/OpenAppstore$1;)V

    return-object v0
.end method

.method protected getServiceIntent()Landroid/content/Intent;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/Nullable;
    .end annotation

    .line 70
    iget-object v0, p0, Lorg/onepf/oms/appstore/OpenAppstore$1;->val$billingIntent:Landroid/content/Intent;

    return-object v0
.end method
