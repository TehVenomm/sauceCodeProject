.class Lorg/onepf/oms/appstore/SkubitAppstore$1;
.super Ljava/lang/Object;
.source "SkubitAppstore.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/SkubitAppstore;->isBillingAvailable(Ljava/lang/String;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/SkubitAppstore;

.field final synthetic val$latch:Ljava/util/concurrent/CountDownLatch;

.field final synthetic val$packageName:Ljava/lang/String;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/SkubitAppstore;Ljava/lang/String;Ljava/util/concurrent/CountDownLatch;)V
    .locals 0

    .line 114
    iput-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->this$0:Lorg/onepf/oms/appstore/SkubitAppstore;

    iput-object p2, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->val$packageName:Ljava/lang/String;

    iput-object p3, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->val$latch:Ljava/util/concurrent/CountDownLatch;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 2

    .line 116
    invoke-static {p2}, Lcom/skubit/android/billing/IBillingService$Stub;->asInterface(Landroid/os/IBinder;)Lcom/skubit/android/billing/IBillingService;

    move-result-object p1

    .line 119
    :try_start_0
    iget-object p2, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->val$packageName:Ljava/lang/String;

    const-string v0, "inapp"

    const/4 v1, 0x1

    invoke-interface {p1, v1, p2, v0}, Lcom/skubit/android/billing/IBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p1

    if-nez p1, :cond_0

    .line 121
    iget-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->this$0:Lorg/onepf/oms/appstore/SkubitAppstore;

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p2

    invoke-static {p1, p2}, Lorg/onepf/oms/appstore/SkubitAppstore;->access$002(Lorg/onepf/oms/appstore/SkubitAppstore;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    goto :goto_0

    :cond_0
    const-string p1, "isBillingAvailable() Google Play billing unavaiable"

    .line 123
    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d(Ljava/lang/String;)V
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string p2, "isBillingAvailable() RemoteException while setting up in-app billing"

    .line 126
    invoke-static {p2, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 128
    :goto_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->val$latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    .line 129
    iget-object p1, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->this$0:Lorg/onepf/oms/appstore/SkubitAppstore;

    iget-object p1, p1, Lorg/onepf/oms/appstore/SkubitAppstore;->context:Landroid/content/Context;

    invoke-virtual {p1, p0}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    return-void

    .line 128
    :goto_1
    iget-object p2, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->val$latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p2}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    .line 129
    iget-object p2, p0, Lorg/onepf/oms/appstore/SkubitAppstore$1;->this$0:Lorg/onepf/oms/appstore/SkubitAppstore;

    iget-object p2, p2, Lorg/onepf/oms/appstore/SkubitAppstore;->context:Landroid/content/Context;

    invoke-virtual {p2, p0}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    throw p1
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 0

    return-void
.end method
