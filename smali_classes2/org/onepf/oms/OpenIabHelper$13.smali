.class Lorg/onepf/oms/OpenIabHelper$13;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper;->discoverOpenStores()Ljava/util/List;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;

.field final synthetic val$countDownLatch:Ljava/util/concurrent/CountDownLatch;

.field final synthetic val$openAppstores:Ljava/util/List;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/List;Ljava/util/concurrent/CountDownLatch;)V
    .locals 0

    .line 954
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$13;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$13;->val$openAppstores:Ljava/util/List;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$13;->val$countDownLatch:Ljava/util/concurrent/CountDownLatch;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public openStoresDiscovered(Ljava/util/List;)V
    .locals 1
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/Appstore;",
            ">;)V"
        }
    .end annotation

    .line 957
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$13;->val$openAppstores:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 958
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$13;->val$countDownLatch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p1}, Ljava/lang/Object;->notify()V

    return-void
.end method
