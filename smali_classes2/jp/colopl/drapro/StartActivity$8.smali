.class Ljp/colopl/drapro/StartActivity$8;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$OnIabPurchaseFinishedListener;


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

    .line 871
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onIabPurchaseFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Purchase;)V
    .locals 5

    const-string v0, "StartActivity"

    .line 873
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "[IABV3] Purchase finished: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ", purchase: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 875
    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->isCancel()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 876
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_cancel_title"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 877
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "payment_cancel_message"

    const-string v2, "string"

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 878
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "dialog_button_ok"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 876
    invoke-static {p1, p2, v0, v1}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 879
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    return-void

    .line 883
    :cond_0
    invoke-virtual {p1}, Ljp/colopl/iab/IabResult;->isFailure()Z

    move-result p1

    if-eqz p1, :cond_1

    .line 884
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "network_error"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 885
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "network_purchase_error"

    const-string v2, "string"

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 886
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "dialog_button_ok"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 884
    invoke-static {p1, p2, v0, v1}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 887
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    return-void

    .line 891
    :cond_1
    invoke-static {p2}, Ljp/colopl/drapro/StartActivity;->verifyDeveloperPayload(Ljp/colopl/iab/Purchase;)Z

    move-result p1

    if-nez p1, :cond_2

    .line 894
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "network_error"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 895
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "network_purchase_error"

    const-string v2, "string"

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 896
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "dialog_button_ok"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 894
    invoke-static {p1, p2, v0, v1}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 897
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    return-void

    .line 903
    :cond_2
    new-instance p1, Landroid/os/Handler;

    invoke-direct {p1}, Landroid/os/Handler;-><init>()V

    new-instance v0, Ljp/colopl/drapro/StartActivity$8$1;

    invoke-direct {v0, p0, p2}, Ljp/colopl/drapro/StartActivity$8$1;-><init>(Ljp/colopl/drapro/StartActivity$8;Ljp/colopl/iab/Purchase;)V

    invoke-virtual {p1, v0}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
