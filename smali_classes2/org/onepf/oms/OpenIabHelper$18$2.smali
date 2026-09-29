.class Lorg/onepf/oms/OpenIabHelper$18$2;
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

    .line 1500
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$18$2;->this$1:Lorg/onepf/oms/OpenIabHelper$18;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$18$2;->val$results:Ljava/util/List;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 1502
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$18$2;->this$1:Lorg/onepf/oms/OpenIabHelper$18;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$18;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->access$2100(Lorg/onepf/oms/OpenIabHelper;)I

    move-result v0

    if-nez v0, :cond_0

    .line 1503
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$18$2;->this$1:Lorg/onepf/oms/OpenIabHelper$18;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$18;->val$consumeMultiListener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$18$2;->this$1:Lorg/onepf/oms/OpenIabHelper$18;

    iget-object v1, v1, Lorg/onepf/oms/OpenIabHelper$18;->val$purchases:Ljava/util/List;

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$18$2;->val$results:Ljava/util/List;

    invoke-interface {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnConsumeMultiFinishedListener;->onConsumeMultiFinished(Ljava/util/List;Ljava/util/List;)V

    :cond_0
    return-void
.end method
