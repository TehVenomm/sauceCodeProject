.class Lorg/onepf/oms/appstore/GooglePlay$1;
.super Ljava/lang/Object;
.source "GooglePlay.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/GooglePlay;->isBillingAvailable(Ljava/lang/String;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/GooglePlay;

.field final synthetic val$latch:Ljava/util/concurrent/CountDownLatch;

.field final synthetic val$packageName:Ljava/lang/String;

.field final synthetic val$result:[Z


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/GooglePlay;Ljava/lang/String;[ZLjava/util/concurrent/CountDownLatch;)V
    .locals 0

    .line 116
    iput-object p1, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->this$0:Lorg/onepf/oms/appstore/GooglePlay;

    iput-object p2, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$packageName:Ljava/lang/String;

    iput-object p3, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$result:[Z

    iput-object p4, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$latch:Ljava/util/concurrent/CountDownLatch;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 4

    .line 118
    invoke-static {p2}, Lcom/android/vending/billing/IInAppBillingService$Stub;->asInterface(Landroid/os/IBinder;)Lcom/android/vending/billing/IInAppBillingService;

    move-result-object p1

    const/4 p2, 0x3

    const/4 v0, 0x1

    const/4 v1, 0x0

    .line 120
    :try_start_0
    iget-object v2, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$packageName:Ljava/lang/String;

    const-string v3, "inapp"

    invoke-interface {p1, p2, v2, v3}, Lcom/android/vending/billing/IInAppBillingService;->isBillingSupported(ILjava/lang/String;Ljava/lang/String;)I

    move-result p1

    .line 121
    iget-object p2, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$result:[Z

    if-nez p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    aput-boolean p1, p2, v1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_1

    :catchall_0
    move-exception p1

    goto :goto_2

    :catch_0
    move-exception p1

    .line 123
    :try_start_1
    iget-object p2, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$result:[Z

    aput-boolean v1, p2, v1

    const-string p2, "isBillingAvailable() RemoteException while setting up in-app billing"

    .line 124
    invoke-static {p2, p1}, Lorg/onepf/oms/util/Logger;->e(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 126
    :goto_1
    iget-object p1, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    .line 127
    iget-object p1, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->this$0:Lorg/onepf/oms/appstore/GooglePlay;

    invoke-static {p1}, Lorg/onepf/oms/appstore/GooglePlay;->access$000(Lorg/onepf/oms/appstore/GooglePlay;)Landroid/content/Context;

    move-result-object p1

    invoke-virtual {p1, p0}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    const/4 p1, 0x2

    .line 129
    new-array p1, p1, [Ljava/lang/Object;

    const-string p2, "isBillingAvailable() Google Play result: "

    aput-object p2, p1, v1

    iget-object p2, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$result:[Z

    aget-boolean p2, p2, v1

    invoke-static {p2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p2

    aput-object p2, p1, v0

    invoke-static {p1}, Lorg/onepf/oms/util/Logger;->d([Ljava/lang/Object;)V

    return-void

    .line 126
    :goto_2
    iget-object p2, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->val$latch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p2}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    .line 127
    iget-object p2, p0, Lorg/onepf/oms/appstore/GooglePlay$1;->this$0:Lorg/onepf/oms/appstore/GooglePlay;

    invoke-static {p2}, Lorg/onepf/oms/appstore/GooglePlay;->access$000(Lorg/onepf/oms/appstore/GooglePlay;)Landroid/content/Context;

    move-result-object p2

    invoke-virtual {p2, p0}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    throw p1
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 0

    return-void
.end method
