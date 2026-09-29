.class Ljp/colopl/drapro/StartActivity$10;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;


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

    .line 930
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onPostDepositFinished(Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;)V
    .locals 8

    const-string v0, "StartActivity"

    .line 932
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Resultcode:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getStatusCode()I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 934
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getSuccess()Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_1

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->isValidStatusCode()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 935
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    const/4 v2, 0x0

    invoke-static {v0, v2}, Ljp/colopl/drapro/StartActivity;->access$602(Ljp/colopl/drapro/StartActivity;I)I

    .line 941
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getPurchasedSku()Ljava/lang/String;

    move-result-object v0

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0, v3}, Ljp/colopl/drapro/AppConsts;->getProductNameById(Ljava/lang/String;Landroid/app/Activity;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 942
    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 944
    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    const-string v5, "notification_purchase_item"

    const-string v6, "string"

    iget-object v7, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v7}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v4, v5, v6, v7}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v4

    .line 943
    invoke-virtual {v3, v4}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object v3

    new-array v4, v1, [Ljava/lang/Object;

    aput-object v0, v4, v2

    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 946
    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "notification_purchase_item_default"

    const-string v4, "string"

    iget-object v5, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 947
    invoke-virtual {v5}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v5

    .line 946
    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    invoke-virtual {v0, v2}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object v0

    .line 948
    :goto_0
    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v2, v0, v1}, Landroid/widget/Toast;->makeText(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/Toast;->show()V

    const-string v0, "ShopReceiver"

    const-string v1, "buyItem"

    .line 950
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getResultData()Ljava/lang/String;

    move-result-object v2

    invoke-static {v0, v1, v2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 953
    :try_start_0
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getPurchasedSku()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getPurchase()Ljp/colopl/iab/Purchase;

    move-result-object v1

    invoke-virtual {v1}, Ljp/colopl/iab/Purchase;->getOriginalJson()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getPurchase()Ljp/colopl/iab/Purchase;

    move-result-object p1

    invoke-virtual {p1}, Ljp/colopl/iab/Purchase;->getSignature()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, v1, p1}, Ljp/colopl/drapro/InAppBillingHelper;->trackPurtraceData(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljp/colopl/iab/IabException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    .line 957
    invoke-virtual {p1}, Ljp/colopl/iab/IabException;->printStackTrace()V

    .line 970
    :goto_1
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    goto/16 :goto_2

    .line 971
    :cond_1
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getSuccess()Z

    move-result v0

    if-eqz v0, :cond_2

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->isAlreadyCancelled()Z

    move-result v0

    if-eqz v0, :cond_2

    .line 972
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getPurchasedSku()Ljava/lang/String;

    move-result-object p1

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1, v0}, Ljp/colopl/drapro/AppConsts;->getProductNameById(Ljava/lang/String;Landroid/app/Activity;)Ljava/lang/String;

    .line 973
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "notification_purchase_already_cancelled"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 974
    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    .line 973
    invoke-virtual {v0, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    invoke-virtual {p1, v0}, Ljp/colopl/drapro/StartActivity;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 975
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0, p1, v1}, Landroid/widget/Toast;->makeText(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/Toast;->show()V

    .line 976
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    goto/16 :goto_2

    .line 979
    :cond_2
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Ljp/colopl/drapro/StartActivity;->access$608(Ljp/colopl/drapro/StartActivity;)I

    const-string v0, "StartActivity"

    .line 980
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "[IABV3] Post deposit failed. retrying... : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v2}, Ljp/colopl/drapro/StartActivity;->access$600(Ljp/colopl/drapro/StartActivity;)I

    move-result v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 981
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Ljp/colopl/drapro/StartActivity;->access$600(Ljp/colopl/drapro/StartActivity;)I

    move-result v0

    invoke-static {}, Ljp/colopl/drapro/StartActivity;->access$500()I

    move-result v1

    if-ge v0, v1, :cond_3

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getPurchase()Ljp/colopl/iab/Purchase;

    move-result-object v0

    if-eqz v0, :cond_3

    .line 982
    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0}, Landroid/os/Handler;-><init>()V

    new-instance v1, Ljp/colopl/drapro/StartActivity$10$1;

    invoke-direct {v1, p0, p1}, Ljp/colopl/drapro/StartActivity$10$1;-><init>(Ljp/colopl/drapro/StartActivity$10;Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    goto :goto_2

    .line 988
    :cond_3
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "network_error"

    const-string v2, "string"

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 989
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "network_error_occurred"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 990
    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "dialog_button_ok"

    const-string v4, "string"

    iget-object v5, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v5}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 988
    invoke-static {p1, v0, v1, v2}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 991
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    :goto_2
    return-void
.end method
