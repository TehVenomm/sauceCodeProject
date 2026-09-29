.class Ljp/colopl/drapro/StartActivity$10$1;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/StartActivity$10;->onPostDepositFinished(Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Ljp/colopl/drapro/StartActivity$10;

.field final synthetic val$result:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity$10;Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;)V
    .locals 0

    .line 982
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$10$1;->this$1:Ljp/colopl/drapro/StartActivity$10;

    iput-object p2, p0, Ljp/colopl/drapro/StartActivity$10$1;->val$result:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 984
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$10$1;->this$1:Ljp/colopl/drapro/StartActivity$10;

    iget-object v0, v0, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Ljp/colopl/drapro/StartActivity;->access$200(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/drapro/ColoplDepositHelper;

    move-result-object v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$10$1;->val$result:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;

    invoke-virtual {v1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->getPurchase()Ljp/colopl/iab/Purchase;

    move-result-object v1

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$10$1;->this$1:Ljp/colopl/drapro/StartActivity$10;

    iget-object v2, v2, Ljp/colopl/drapro/StartActivity$10;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v2, v2, Ljp/colopl/drapro/StartActivity;->mDepositPostListener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

    invoke-virtual {v0, v1, v2}, Ljp/colopl/drapro/ColoplDepositHelper;->postDepositAsync(Ljp/colopl/iab/Purchase;Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;)V

    return-void
.end method
