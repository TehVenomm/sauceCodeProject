.class Lorg/onepf/oms/OpenIabHelper$12;
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

    .line 809
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$12;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$12;->val$appstores:Ljava/util/Collection;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$12;->val$packageName:Ljava/lang/String;

    iput-object p4, p0, Lorg/onepf/oms/OpenIabHelper$12;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 813
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$12;->val$appstores:Ljava/util/Collection;

    invoke-interface {v0}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/Appstore;

    .line 814
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$12;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2, v1}, Lorg/onepf/oms/OpenIabHelper;->access$1302(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/Appstore;)Lorg/onepf/oms/Appstore;

    .line 815
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$12;->val$packageName:Ljava/lang/String;

    invoke-interface {v1, v2}, Lorg/onepf/oms/Appstore;->isBillingAvailable(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$12;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2, v1}, Lorg/onepf/oms/OpenIabHelper;->access$1400(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/Appstore;)Z

    move-result v2

    if-eqz v2, :cond_0

    goto :goto_0

    :cond_1
    const/4 v1, 0x0

    .line 821
    :goto_0
    new-instance v0, Lorg/onepf/oms/OpenIabHelper$12$1;

    invoke-direct {v0, p0, v1}, Lorg/onepf/oms/OpenIabHelper$12$1;-><init>(Lorg/onepf/oms/OpenIabHelper$12;Lorg/onepf/oms/Appstore;)V

    .line 837
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$12;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2}, Lorg/onepf/oms/OpenIabHelper;->access$1800(Lorg/onepf/oms/OpenIabHelper;)Landroid/os/Handler;

    move-result-object v2

    new-instance v3, Lorg/onepf/oms/OpenIabHelper$12$2;

    invoke-direct {v3, p0, v0, v1}, Lorg/onepf/oms/OpenIabHelper$12$2;-><init>(Lorg/onepf/oms/OpenIabHelper$12;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Lorg/onepf/oms/Appstore;)V

    invoke-virtual {v2, v3}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
