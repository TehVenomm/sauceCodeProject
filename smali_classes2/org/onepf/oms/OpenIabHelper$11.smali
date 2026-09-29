.class Lorg/onepf/oms/OpenIabHelper$11;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper;->checkBillingAndFinish(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/Collection;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;

.field final synthetic val$appstores:Ljava/util/Collection;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

.field final synthetic val$packageName:Ljava/lang/String;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Collection;Ljava/lang/String;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 771
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$11;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$11;->val$appstores:Ljava/util/Collection;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$11;->val$packageName:Ljava/lang/String;

    iput-object p4, p0, Lorg/onepf/oms/OpenIabHelper$11;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 774
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 775
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$11;->val$appstores:Ljava/util/Collection;

    invoke-interface {v1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lorg/onepf/oms/Appstore;

    .line 776
    iget-object v3, p0, Lorg/onepf/oms/OpenIabHelper$11;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v3, v2}, Lorg/onepf/oms/OpenIabHelper;->access$1302(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/Appstore;)Lorg/onepf/oms/Appstore;

    .line 777
    iget-object v3, p0, Lorg/onepf/oms/OpenIabHelper$11;->val$packageName:Ljava/lang/String;

    invoke-interface {v2, v3}, Lorg/onepf/oms/Appstore;->isBillingAvailable(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_0

    iget-object v3, p0, Lorg/onepf/oms/OpenIabHelper$11;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v3, v2}, Lorg/onepf/oms/OpenIabHelper;->access$1400(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/Appstore;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 778
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 781
    :cond_1
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$11;->this$0:Lorg/onepf/oms/OpenIabHelper;

    new-instance v2, Ljava/util/HashSet;

    invoke-direct {v2, v0}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    invoke-static {v1, v2}, Lorg/onepf/oms/OpenIabHelper;->access$1500(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Set;)Lorg/onepf/oms/Appstore;

    move-result-object v1

    if-nez v1, :cond_3

    .line 784
    invoke-interface {v0}, Ljava/util/List;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_2

    const/4 v1, 0x0

    goto :goto_1

    :cond_2
    const/4 v1, 0x0

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/Appstore;

    .line 788
    :cond_3
    :goto_1
    new-instance v2, Lorg/onepf/oms/OpenIabHelper$11$1;

    invoke-direct {v2, p0, v0, v1}, Lorg/onepf/oms/OpenIabHelper$11$1;-><init>(Lorg/onepf/oms/OpenIabHelper$11;Ljava/util/List;Lorg/onepf/oms/Appstore;)V

    .line 800
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$11;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->access$1800(Lorg/onepf/oms/OpenIabHelper;)Landroid/os/Handler;

    move-result-object v0

    new-instance v3, Lorg/onepf/oms/OpenIabHelper$11$2;

    invoke-direct {v3, p0, v2, v1}, Lorg/onepf/oms/OpenIabHelper$11$2;-><init>(Lorg/onepf/oms/OpenIabHelper$11;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V

    invoke-virtual {v0, v3}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
