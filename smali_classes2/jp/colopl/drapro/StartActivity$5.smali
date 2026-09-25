.class Ljp/colopl/drapro/StartActivity$5;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljava/lang/Runnable;


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

.field final synthetic val$gotInventoryListener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity;Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V
    .locals 0

    .line 666
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$5;->this$0:Ljp/colopl/drapro/StartActivity;

    iput-object p2, p0, Ljp/colopl/drapro/StartActivity$5;->val$gotInventoryListener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5

    .line 670
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$5;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$5;->this$0:Ljp/colopl/drapro/StartActivity;

    .line 671
    invoke-virtual {v1}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "payment_checking_inventory"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/StartActivity$5;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    const/4 v2, 0x0

    .line 670
    invoke-virtual {v0, v2, v1}, Ljp/colopl/drapro/StartActivity;->showProgressDialog2(II)V

    .line 673
    iget-object v0, p0, Ljp/colopl/drapro/StartActivity$5;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-static {v0}, Ljp/colopl/drapro/StartActivity;->access$300(Ljp/colopl/drapro/StartActivity;)Ljp/colopl/iab/IabHelper;

    move-result-object v0

    iget-object v1, p0, Ljp/colopl/drapro/StartActivity$5;->val$gotInventoryListener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;

    invoke-virtual {v0, v1}, Ljp/colopl/iab/IabHelper;->queryInventoryAsync(Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;)V

    return-void
.end method
