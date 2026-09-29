.class Lorg/onepf/oms/OpenIabHelper$11$1;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper$11;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/oms/OpenIabHelper$11;

.field final synthetic val$availableAppstores:Ljava/util/List;

.field final synthetic val$foundAppstore:Lorg/onepf/oms/Appstore;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper$11;Ljava/util/List;Lorg/onepf/oms/Appstore;)V
    .locals 0

    .line 788
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$11$1;->this$1:Lorg/onepf/oms/OpenIabHelper$11;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$11$1;->val$availableAppstores:Ljava/util/List;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$11$1;->val$foundAppstore:Lorg/onepf/oms/Appstore;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    .locals 2

    .line 792
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$11$1;->val$availableAppstores:Ljava/util/List;

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 793
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$11$1;->val$foundAppstore:Lorg/onepf/oms/Appstore;

    if-eqz v1, :cond_0

    .line 794
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$11$1;->val$foundAppstore:Lorg/onepf/oms/Appstore;

    invoke-interface {v0, v1}, Ljava/util/Collection;->remove(Ljava/lang/Object;)Z

    .line 796
    :cond_0
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$11$1;->this$1:Lorg/onepf/oms/OpenIabHelper$11;

    iget-object v1, v1, Lorg/onepf/oms/OpenIabHelper$11;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v1, v0}, Lorg/onepf/oms/OpenIabHelper;->access$1600(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Collection;)V

    .line 797
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$11$1;->this$1:Lorg/onepf/oms/OpenIabHelper$11;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$11;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-interface {v0, p1}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;->onIabSetupFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    return-void
.end method
