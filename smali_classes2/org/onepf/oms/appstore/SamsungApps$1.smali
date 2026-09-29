.class Lorg/onepf/oms/appstore/SamsungApps$1;
.super Ljava/lang/Object;
.source "SamsungApps.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/SamsungApps;->isBillingAvailable(Ljava/lang/String;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/appstore/SamsungApps;

.field final synthetic val$mainLatch:Ljava/util/concurrent/CountDownLatch;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/SamsungApps;Ljava/util/concurrent/CountDownLatch;)V
    .locals 0

    .line 130
    iput-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps$1;->this$0:Lorg/onepf/oms/appstore/SamsungApps;

    iput-object p2, p0, Lorg/onepf/oms/appstore/SamsungApps$1;->val$mainLatch:Ljava/util/concurrent/CountDownLatch;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    .locals 1
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabResult;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 132
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->isSuccess()Z

    move-result p1

    if-eqz p1, :cond_0

    .line 133
    new-instance p1, Ljava/lang/Thread;

    new-instance v0, Lorg/onepf/oms/appstore/SamsungApps$1$1;

    invoke-direct {v0, p0}, Lorg/onepf/oms/appstore/SamsungApps$1$1;-><init>(Lorg/onepf/oms/appstore/SamsungApps$1;)V

    invoke-direct {p1, v0}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    invoke-virtual {p1}, Ljava/lang/Thread;->start()V

    goto :goto_0

    .line 151
    :cond_0
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps$1;->this$0:Lorg/onepf/oms/appstore/SamsungApps;

    invoke-virtual {p1}, Lorg/onepf/oms/appstore/SamsungApps;->getInAppBillingService()Lorg/onepf/oms/AppstoreInAppBillingService;

    move-result-object p1

    invoke-interface {p1}, Lorg/onepf/oms/AppstoreInAppBillingService;->dispose()V

    .line 152
    iget-object p1, p0, Lorg/onepf/oms/appstore/SamsungApps$1;->val$mainLatch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p1}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    :goto_0
    return-void
.end method
