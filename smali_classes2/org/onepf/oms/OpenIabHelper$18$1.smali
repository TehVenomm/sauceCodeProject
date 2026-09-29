.class Lorg/onepf/oms/OpenIabHelper$18$1;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper$18;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/oms/OpenIabHelper$18;

.field final synthetic val$results:Ljava/util/List;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper$18;Ljava/util/List;)V
    .locals 0

    .line 1491
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$18$1;->this$1:Lorg/onepf/oms/OpenIabHelper$18;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$18$1;->val$results:Ljava/util/List;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 1493
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$18$1;->this$1:Lorg/onepf/oms/OpenIabHelper$18;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$18;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->access$2100(Lorg/onepf/oms/OpenIabHelper;)I

    move-result v0

    if-nez v0, :cond_0

    .line 1494
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$18$1;->this$1:Lorg/onepf/oms/OpenIabHelper$18;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$18;->val$consumeListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$18$1;->this$1:Lorg/onepf/oms/OpenIabHelper$18;

    iget-object v1, v1, Lorg/onepf/oms/OpenIabHelper$18;->val$purchases:Ljava/util/List;

    const/4 v2, 0x0

    invoke-interface {v1, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/appstore/googleUtils/Purchase;

    iget-object v3, p0, Lorg/onepf/oms/OpenIabHelper$18$1;->val$results:Ljava/util/List;

    invoke-interface {v3, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    invoke-interface {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeFinishedListener;->onConsumeFinished(Lorg/onepf/oms/appstore/googleUtils/Purchase;Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    :cond_0
    return-void
.end method
