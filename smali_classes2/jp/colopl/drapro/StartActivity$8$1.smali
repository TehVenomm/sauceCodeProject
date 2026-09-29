.class Ljp/colopl/drapro/StartActivity$8$1;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/StartActivity$8;->onIabPurchaseFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Purchase;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Ljp/colopl/drapro/StartActivity$8;

.field final synthetic val$purchase:Ljp/colopl/iab/Purchase;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity$8;Ljp/colopl/iab/Purchase;)V
    .locals 0

    .line 903
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$8$1;->this$1:Ljp/colopl/drapro/StartActivity$8;

    iput-object p2, p0, Ljp/colopl/drapro/StartActivity$8$1;->val$purchase:Ljp/colopl/iab/Purchase;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 905
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$8$1;->this$1:Ljp/colopl/drapro/StartActivity$8;

    iget-object v0, v0, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Ljp/colopl/drapro/StartActivity;->access$300(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/iab/IabHelper;

    move-result-object v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$8$1;->val$purchase:Ljp/colopl/iab/Purchase;

    iget-object v2, p0, Ljp/colopl/drapro/StartActivity$8$1;->this$1:Ljp/colopl/drapro/StartActivity$8;

    iget-object v2, v2, Ljp/colopl/drapro/StartActivity$8;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v2, v2, Ljp/colopl/drapro/StartActivity;->mConsumeFinishedListener:Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;

    invoke-virtual {v0, v1, v2}, Ljp/colopl/iab/IabHelper;->consumeAsync(Ljp/colopl/iab/Purchase;Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;)V

    return-void
.end method
