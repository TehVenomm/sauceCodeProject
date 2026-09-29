.class Ljp/colopl/iab/IabHelper$2;
.super Ljava/lang/Object;
.source "IabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/iab/IabHelper;->queryInventoryAsync(ZLjava/util/List;Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/iab/IabHelper;

.field final synthetic val$handler:Landroid/os/Handler;

.field final synthetic val$listener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;

.field final synthetic val$moreSkus:Ljava/util/List;

.field final synthetic val$querySkuDetails:Z


# direct methods
.method constructor <init>(Ljp/colopl/iab/IabHelper;ZLjava/util/List;Landroid/os/Handler;Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V
    .locals 0

    .line 622
    iput-object p1, p0, Ljp/colopl/iab/IabHelper$2;->this$0:Ljp/colopl/iab/IabHelper;

    iput-boolean p2, p0, Ljp/colopl/iab/IabHelper$2;->val$querySkuDetails:Z

    iput-object p3, p0, Ljp/colopl/iab/IabHelper$2;->val$moreSkus:Ljava/util/List;

    iput-object p4, p0, Ljp/colopl/iab/IabHelper$2;->val$handler:Landroid/os/Handler;

    iput-object p5, p0, Ljp/colopl/iab/IabHelper$2;->val$listener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 624
    new-instance v0, Ljp/colopl/iab/IabResult;

    const-string v1, "Inventory refresh successful."

    const/4 v2, 0x0

    invoke-direct {v0, v2, v1}, Ljp/colopl/iab/IabResult;-><init>(ILjava/lang/String;)V

    .line 627
    :try_start_0
    iget-object v1, p0, Ljp/colopl/iab/IabHelper$2;->this$0:Ljp/colopl/iab/IabHelper;

    iget-boolean v2, p0, Ljp/colopl/iab/IabHelper$2;->val$querySkuDetails:Z

    iget-object v3, p0, Ljp/colopl/iab/IabHelper$2;->val$moreSkus:Ljava/util/List;

    invoke-virtual {v1, v2, v3}, Ljp/colopl/iab/IabHelper;->queryInventory(ZLjava/util/List;)Ljp/colopl/iab/Inventory;

    move-result-object v1
    :try_end_0
    .catch Ljp/colopl/iab/IabException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 630
    invoke-virtual {v0}, Ljp/colopl/iab/IabException;->getResult()Ljp/colopl/iab/IabResult;

    move-result-object v0

    const/4 v1, 0x0

    .line 633
    :goto_0
    iget-object v2, p0, Ljp/colopl/iab/IabHelper$2;->this$0:Ljp/colopl/iab/IabHelper;

    invoke-virtual {v2}, Ljp/colopl/iab/IabHelper;->flagEndAsync()V

    .line 637
    iget-object v2, p0, Ljp/colopl/iab/IabHelper$2;->val$handler:Landroid/os/Handler;

    new-instance v3, Ljp/colopl/iab/IabHelper$2$1;

    invoke-direct {v3, p0, v0, v1}, Ljp/colopl/iab/IabHelper$2$1;-><init>(Ljp/colopl/iab/IabHelper$2;Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V

    invoke-virtual {v2, v3}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
