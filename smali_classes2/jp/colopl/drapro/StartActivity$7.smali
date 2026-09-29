.class Ljp/colopl/drapro/StartActivity$7;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;


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

    .line 824
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onConsumeFinished(Ljp/colopl/iab/Purchase;Ljp/colopl/iab/IabResult;)V
    .locals 5

    .line 828
    invoke-virtual {p2}, Ljp/colopl/iab/IabResult;->isSuccess()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 832
    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    const/4 v0, 0x0

    invoke-static {p2, v0}, Ljp/colopl/drapro/StartActivity;->access$402(Ljp/colopl/drapro/StartActivity;I)I

    .line 833
    new-instance p2, Landroid/os/Handler;

    invoke-direct {p2}, Landroid/os/Handler;-><init>()V

    new-instance v0, Ljp/colopl/drapro/StartActivity$7$1;

    invoke-direct {v0, p0, p1}, Ljp/colopl/drapro/StartActivity$7$1;-><init>(Ljp/colopl/drapro/StartActivity$7;Ljp/colopl/iab/Purchase;)V

    invoke-virtual {p2, v0}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    goto/16 :goto_0

    :cond_0
    const-string v0, "StartActivity"

    .line 841
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "[IABV3] Consumption failed. consume item "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 842
    invoke-virtual {p2}, Ljp/colopl/iab/IabResult;->getResponse()I

    move-result p2

    const/16 v0, 0x8

    if-ne p2, v0, :cond_1

    .line 844
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "network_error"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 845
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "network_purchase_delay"

    const-string v2, "string"

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 846
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "ok"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 844
    invoke-static {p1, p2, v0, v1}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 847
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    return-void

    .line 851
    :cond_1
    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p2}, Ljp/colopl/drapro/StartActivity;->access$408(Ljp/colopl/drapro/StartActivity;)I

    .line 852
    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p2}, Ljp/colopl/drapro/StartActivity;->access$400(Ljp/colopl/drapro/StartActivity;)I

    move-result p2

    invoke-static {}, Ljp/colopl/drapro/StartActivity;->access$500()I

    move-result v0

    if-ge p2, v0, :cond_2

    if-eqz p1, :cond_2

    const-string p2, "StartActivity"

    .line 853
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] Consumption finished. retry "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, "  retry="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v1}, Ljp/colopl/drapro/StartActivity;->access$400(Ljp/colopl/drapro/StartActivity;)I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p2, v0}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 854
    new-instance p2, Landroid/os/Handler;

    invoke-direct {p2}, Landroid/os/Handler;-><init>()V

    new-instance v0, Ljp/colopl/drapro/StartActivity$7$2;

    invoke-direct {v0, p0, p1}, Ljp/colopl/drapro/StartActivity$7$2;-><init>(Ljp/colopl/drapro/StartActivity$7;Ljp/colopl/iab/Purchase;)V

    invoke-virtual {p2, v0}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    goto :goto_0

    .line 860
    :cond_2
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p2, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "network_error"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 861
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "network_error_occurred"

    const-string v2, "string"

    iget-object v3, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 862
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "ok"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 860
    invoke-static {p1, p2, v0, v1}, Ljp/colopl/drapro/StartActivity;->access$000(Ljp/colopl/drapro/StartActivity;III)V

    .line 863
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {p1}, Ljp/colopl/drapro/StartActivity;->access$100(Ljp/colopl/drapro/StartActivity;)V

    :goto_0
    return-void
.end method
