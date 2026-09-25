.class Ljp/colopl/drapro/StartActivity$9;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;


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

    .line 912
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$9;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onPrepareDepositFinished(Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;)V
    .locals 9

    .line 914
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->getSuccess()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 918
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$9;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Ljp/colopl/drapro/StartActivity;->access$300(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/iab/IabHelper;

    move-result-object v1

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$9;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->getItemId()Ljava/lang/String;

    move-result-object v3

    const-string v4, "inapp"

    const/16 v5, 0x2711

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$9;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v6, v0, Ljp/colopl/drapro/StartActivity;->mPurchaseFinishedListener:Ljp/colopl/iab/IabHelper$OnIabPurchaseFinishedListener;

    .line 919
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->getPayload()Ljava/lang/String;

    move-result-object v7

    sget-object v8, Ljp/colopl/drapro/InAppBillingHelper;->userIdHash:Ljava/lang/String;

    .line 918
    invoke-virtual/range {v1 .. v8}, Ljp/colopl/iab/IabHelper;->launchPurchaseFlow(Landroid/app/Activity;Ljava/lang/String;Ljava/lang/String;ILjp/colopl/iab/IabHelper$OnIabPurchaseFinishedListener;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    const-string v0, "StartActivity"

    const-string v1, "[IABV3] Prepare deposit failed. Show Error."

    .line 921
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 922
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$9;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->getErrorTitle()I

    move-result v1

    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->getErrorMessage()I

    move-result p1

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$9;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 923
    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "ok"

    const-string v4, "string"

    iget-object v5, p0, Ljp/colopl/drapro/StartActivity$9;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v5}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 922
    invoke-static {v0, v1, p1, v2}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 924
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$9;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    :goto_0
    return-void
.end method
