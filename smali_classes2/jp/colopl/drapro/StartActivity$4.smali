.class Ljp/colopl/drapro/StartActivity$4;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/StartActivity;->restoreInventory(Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/StartActivity;

.field final synthetic val$showErrorDialog:Z


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity;Z)V
    .locals 0

    .line 595
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iput-boolean p2, p0, Ljp/colopl/drapro/StartActivity$4;->val$showErrorDialog:Z

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onQueryInventoryFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V
    .locals 5

    const-string v0, "StartActivity"

    const-string v1, "[IABV3] onQueryInventoryFinished"

    .line 597
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 599
    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->isFailure()Z

    move-result p1

    if-eqz p1, :cond_1

    .line 602
    iget-boolean p1, p0, Ljp/colopl/drapro/StartActivity$4;->val$showErrorDialog:Z

    if-eqz p1, :cond_0

    .line 603
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 604
    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_check_inventory_error_title"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 605
    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    .line 604
    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 606
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "payment_check_inventory_error_message"

    const-string v2, "string"

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 607
    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 606
    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 608
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "dialog_button_ok"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 603
    invoke-static {p1, p2, v0, v1}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 610
    :cond_0
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    return-void

    .line 615
    :cond_1
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mPurchaseList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->clear()V

    .line 616
    invoke-virtual {p2}, Ljp/colopl/iab/Inventory;->getAllPurchases()Ljava/util/List;

    move-result-object p1

    const/4 v0, 0x0

    if-eqz p1, :cond_3

    .line 617
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v1

    if-lez v1, :cond_3

    .line 618
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 v1, 0x0

    :cond_2
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljp/colopl/iab/Purchase;

    .line 621
    invoke-static {v2}, Ljp/colopl/drapro/StartActivity;->verifyDeveloperPayload(Ljp/colopl/iab/Purchase;)Z

    move-result v3

    if-eqz v3, :cond_2

    const/4 v1, 0x1

    .line 623
    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v3, v3, Ljp/colopl/drapro/StartActivity;->mPurchaseList:Ljava/util/ArrayList;

    invoke-virtual {v3, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_3
    const/4 v1, 0x0

    .line 628
    :cond_4
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->clear()V

    .line 630
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$200(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/drapro/ColoplDepositHelper;

    move-result-object p1

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper;->getUndepositedPurchase()Ljava/util/ArrayList;

    move-result-object p1

    if-eqz p1, :cond_6

    .line 631
    invoke-virtual {p1}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-lez v2, :cond_6

    .line 632
    invoke-virtual {p1}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_5
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_6

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljp/colopl/iab/Purchase;

    .line 633
    invoke-static {v2}, Ljp/colopl/drapro/StartActivity;->verifyDeveloperPayload(Ljp/colopl/iab/Purchase;)Z

    move-result v3

    if-eqz v3, :cond_5

    .line 634
    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v3, v3, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {v3, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_1

    :cond_6
    const-string p1, "StartActivity"

    .line 639
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "[IABV3] onQueryInventoryFinished inventory check : has="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v3, " size="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v3, v3, Ljp/colopl/drapro/StartActivity;->mPurchaseList:Ljava/util/ArrayList;

    .line 640
    invoke-virtual {v3}, Ljava/util/ArrayList;->size()I

    move-result v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 639
    invoke-static {p1, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 641
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->size()I

    move-result p1

    if-lez p1, :cond_7

    .line 642
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljp/colopl/iab/Purchase;

    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getSku()Ljava/lang/String;

    move-result-object p1

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1, p2}, Ljp/colopl/drapro/AppConsts;->getProductNameById(Ljava/lang/String;Landroid/app/Activity;)Ljava/lang/String;

    move-result-object p1

    .line 644
    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2, p1}, Ljp/colopl/drapro/StartActivity;->showDepositDialog(Ljava/lang/String;)V

    goto :goto_2

    :cond_7
    if-eqz v1, :cond_8

    .line 647
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

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

    .line 648
    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2, p1}, Ljp/colopl/drapro/StartActivity;->showConsumeDialog(Ljava/lang/String;)V

    goto :goto_2

    .line 651
    :cond_8
    iget-boolean p1, p0, Ljp/colopl/drapro/StartActivity$4;->val$showErrorDialog:Z

    if-eqz p1, :cond_9

    .line 652
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 653
    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_no_purchased_item_title"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 654
    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    .line 653
    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 655
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "payment_no_purchased_item_message"

    const-string v2, "string"

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 656
    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 655
    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 657
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "dialog_button_ok"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 652
    invoke-static {p1, p2, v0, v1}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 659
    :cond_9
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$4;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    :goto_2
    const-string p1, "StartActivity"

    const-string p2, "[IABV3] enabling dialog to select action."

    .line 661
    invoke-static {p1, p2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
