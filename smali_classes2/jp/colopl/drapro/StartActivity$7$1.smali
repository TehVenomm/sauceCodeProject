.class Ljp/colopl/drapro/StartActivity$7$1;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/StartActivity$7;->onConsumeFinished(Ljp/colopl/iab/Purchase;Ljp/colopl/iab/IabResult;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Ljp/colopl/drapro/StartActivity$7;

.field final synthetic val$purchase:Ljp/colopl/iab/Purchase;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity$7;Ljp/colopl/iab/Purchase;)V
    .locals 0

    .line 833
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$7$1;->this$1:Ljp/colopl/drapro/StartActivity$7;

    iput-object p2, p0, Ljp/colopl/drapro/StartActivity$7$1;->val$purchase:Ljp/colopl/iab/Purchase;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 835
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$7$1;->this$1:Ljp/colopl/drapro/StartActivity$7;

    iget-object v0, v0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Ljp/colopl/drapro/StartActivity;->access$200(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/drapro/ColoplDepositHelper;

    move-result-object v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$7$1;->val$purchase:Ljp/colopl/iab/Purchase;

    invoke-virtual {v0, v1}, Ljp/colopl/drapro/ColoplDepositHelper;->addUndepositedPurchase(Ljp/colopl/iab/Purchase;)Z

    .line 836
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$7$1;->this$1:Ljp/colopl/drapro/StartActivity$7;

    iget-object v0, v0, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Ljp/colopl/drapro/StartActivity;->access$200(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/drapro/ColoplDepositHelper;

    move-result-object v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$7$1;->val$purchase:Ljp/colopl/iab/Purchase;

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$7$1;->this$1:Ljp/colopl/drapro/StartActivity$7;

    iget-object v2, v2, Ljp/colopl/drapro/StartActivity$7;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v2, v2, Ljp/colopl/drapro/StartActivity;->mDepositPostListener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

    invoke-virtual {v0, v1, v2}, Ljp/colopl/drapro/ColoplDepositHelper;->postDepositAsync(Ljp/colopl/iab/Purchase;Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;)V

    return-void
.end method
