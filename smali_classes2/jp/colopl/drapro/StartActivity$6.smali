.class Ljp/colopl/drapro/StartActivity$6;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Ljp/colopl/drapro/StartActivity;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/StartActivity;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity;)V
    .locals 0

    .line 691
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onQueryInventoryFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V
    .locals 6

    const-string v0, "StartActivity"

    const-string v1, "[IABV3] onQueryInventoryFinished"

    .line 693
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 694
    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->isFailure()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string p2, "StartActivity"

    .line 695
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] onQueryInventoryFinished problem : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 696
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    return-void

    :cond_0
    const-string p1, "StartActivity"

    const-string v0, "[IABV3] onQueryInventoryFinished success"

    .line 699
    invoke-static {p1, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 701
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mPurchaseList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->clear()V

    .line 702
    invoke-virtual {p2}, Ljp/colopl/iab/Inventory;->getAllPurchases()Ljava/util/List;

    move-result-object p1

    const/4 v0, 0x0

    if-eqz p1, :cond_3

    .line 703
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v1

    if-lez v1, :cond_3

    .line 704
    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "promo_tag"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 705
    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    invoke-virtual {v2, v1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    .line 706
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 v2, 0x0

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljp/colopl/iab/Purchase;

    .line 708
    invoke-virtual {v3}, Ljp/colopl/iab/Purchase;->getSku()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4, v1}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v4

    const/4 v5, -0x1

    if-eq v4, v5, :cond_2

    const-string v3, "StartActivity"

    const-string v4, "Skip Promotion Item"

    .line 709
    invoke-static {v3, v4}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 715
    :cond_2
    invoke-static {v3}, Ljp/colopl/drapro/StartActivity;->verifyDeveloperPayload(Ljp/colopl/iab/Purchase;)Z

    move-result v4

    if-eqz v4, :cond_1

    const-string v2, "StartActivity"

    .line 716
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "[IABV3] onQueryInventoryFinished verifyDeveloperPayload inventoryList success "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 717
    invoke-virtual {v3}, Ljp/colopl/iab/Purchase;->toLog()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    .line 716
    invoke-static {v2, v4}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v2, 0x1

    .line 719
    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v4, v4, Ljp/colopl/drapro/StartActivity;->mPurchaseList:Ljava/util/ArrayList;

    invoke-virtual {v4, v3}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_3
    const/4 v2, 0x0

    .line 724
    :cond_4
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->clear()V

    .line 726
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$200(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/drapro/ColoplDepositHelper;

    move-result-object p1

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper;->getUndepositedPurchase()Ljava/util/ArrayList;

    move-result-object p1

    if-eqz p1, :cond_6

    .line 727
    invoke-virtual {p1}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-lez v1, :cond_6

    .line 728
    invoke-virtual {p1}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_5
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_6

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljp/colopl/iab/Purchase;

    .line 729
    invoke-static {v1}, Ljp/colopl/drapro/StartActivity;->verifyDeveloperPayload(Ljp/colopl/iab/Purchase;)Z

    move-result v3

    if-eqz v3, :cond_5

    const-string v3, "StartActivity"

    .line 730
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "[IABV3] onQueryInventoryFinished verifyDeveloperPayload mUndepositedPurcahseList success "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 732
    invoke-virtual {v1}, Ljp/colopl/iab/Purchase;->toLog()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    .line 730
    invoke-static {v3, v4}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 733
    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v3, v3, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {v3, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 740
    :cond_6
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->size()I

    move-result p1

    if-lez p1, :cond_7

    .line 741
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljp/colopl/iab/Purchase;

    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getSku()Ljava/lang/String;

    move-result-object p1

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1, p2}, Ljp/colopl/drapro/AppConsts;->getProductNameById(Ljava/lang/String;Landroid/app/Activity;)Ljava/lang/String;

    move-result-object p1

    const-string p2, "StartActivity"

    .line 743
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] onQueryInventoryFinished mUndepositedPurcahseList productName "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 744
    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2, p1}, Ljp/colopl/drapro/StartActivity;->showDepositDialog(Ljava/lang/String;)V

    goto :goto_2

    :cond_7
    if-eqz v2, :cond_8

    .line 747
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mPurchaseList:Ljava/util/ArrayList;

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljp/colopl/iab/Purchase;

    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getSku()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Ljp/colopl/iab/Inventory;->getSkuDetails(Ljava/lang/String;)Ljp/colopl/iab/SkuDetails;

    move-result-object p1

    invoke-virtual {p1}, Ljp/colopl/iab/SkuDetails;->getTitle()Ljava/lang/String;

    move-result-object p1

    const-string p2, "StartActivity"

    .line 748
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] onQueryInventoryFinished mPurchaseList productName "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 749
    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2, p1}, Ljp/colopl/drapro/StartActivity;->showConsumeDialog(Ljava/lang/String;)V

    goto :goto_2

    .line 816
    :cond_8
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$200(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/drapro/ColoplDepositHelper;

    move-result-object p1

    invoke-static {}, Ljp/colopl/drapro/InAppBillingHelper;->getProductId()Ljava/lang/String;

    move-result-object p2

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$6;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v0, v0, Ljp/colopl/drapro/StartActivity;->mDepositPrepareListener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

    invoke-virtual {p1, p2, v0}, Ljp/colopl/drapro/ColoplDepositHelper;->prepareDepositAsync(Ljava/lang/String;Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;)V

    :goto_2
    return-void
.end method
