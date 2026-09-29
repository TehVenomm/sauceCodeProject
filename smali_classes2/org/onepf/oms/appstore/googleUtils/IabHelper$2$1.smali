.class Lorg/onepf/oms/appstore/googleUtils/IabHelper$2$1;
.super Ljava/lang/Object;
.source "IabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/appstore/googleUtils/IabHelper$2;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lorg/onepf/oms/appstore/googleUtils/IabHelper$2;

.field final synthetic val$inv_f:Lorg/onepf/oms/appstore/googleUtils/Inventory;

.field final synthetic val$result_f:Lorg/onepf/oms/appstore/googleUtils/IabResult;


# direct methods
.method constructor <init>(Lorg/onepf/oms/appstore/googleUtils/IabHelper$2;Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V
    .locals 0

    .line 660
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2$1;->this$1:Lorg/onepf/oms/appstore/googleUtils/IabHelper$2;

    iput-object p2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2$1;->val$result_f:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    iput-object p3, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2$1;->val$inv_f:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 662
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2$1;->this$1:Lorg/onepf/oms/appstore/googleUtils/IabHelper$2;

    iget-object v0, v0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;

    iget-object v1, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2$1;->val$result_f:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    iget-object v2, p0, Lorg/onepf/oms/appstore/googleUtils/IabHelper$2$1;->val$inv_f:Lorg/onepf/oms/appstore/googleUtils/Inventory;

    invoke-interface {v0, v1, v2}, Lorg/onepf/oms/appstore/googleUtils/IabHelper$QueryInventoryFinishedListener;->onQueryInventoryFinished(Lorg/onepf/oms/appstore/googleUtils/IabResult;Lorg/onepf/oms/appstore/googleUtils/Inventory;)V

    return-void
.end method
