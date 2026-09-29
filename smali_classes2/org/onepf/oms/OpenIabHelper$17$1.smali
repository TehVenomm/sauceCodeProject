.class Lorg/onepf/oms/OpenIabHelper$17$1;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper$17;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/oms/OpenIabHelper$17;

.field final synthetic val$inv_f:Lorg/onepf/oms/appstore/googleUtils/Inventory;

.field final synthetic val$result_f:Lorg/onepf/oms/appstore/googleUtils/IabResult;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper$17;Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V
    .locals 0

    .line 1432
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$17$1;->this$1:Lorg/onepf/oms/OpenIabHelper$17;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$17$1;->val$result_f:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$17$1;->val$inv_f:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 1434
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$17$1;->this$1:Lorg/onepf/oms/OpenIabHelper$17;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$17;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v0}, Lorg/onepf/oms/OpenIabHelper;->access$2100(Lorg/onepf/oms/OpenIabHelper;)I

    move-result v0

    if-nez v0, :cond_0

    .line 1435
    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$17$1;->this$1:Lorg/onepf/oms/OpenIabHelper$17;

    iget-object v0, v0, Lorg/onepf/oms/OpenIabHelper$17;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$17$1;->val$result_f:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$17$1;->val$inv_f:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-interface {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;->onQueryInventoryFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V

    :cond_0
    return-void
.end method
