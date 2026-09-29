.class Ljp/colopl/iab/IabHelper$2$1;
.super Ljava/lang/Object;
.source "IabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/iab/IabHelper$2;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Ljp/colopl/iab/IabHelper$2;

.field final synthetic val$inv_f:Ljp/colopl/iab/Inventory;

.field final synthetic val$result_f:Ljp/colopl/iab/IabResult;


# direct methods
.method constructor <init>(Ljp/colopl/iab/IabHelper$2;Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V
    .locals 0

    .line 637
    iput-object p1, p0, Ljp/colopl/iab/IabHelper$2$1;->this$1:Ljp/colopl/iab/IabHelper$2;

    iput-object p2, p0, Ljp/colopl/iab/IabHelper$2$1;->val$result_f:Ljp/colopl/iab/IabResult;

    iput-object p3, p0, Ljp/colopl/iab/IabHelper$2$1;->val$inv_f:Ljp/colopl/iab/Inventory;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 639
    iget-object v0, p0, Ljp/colopl/iab/IabHelper$2$1;->this$1:Ljp/colopl/iab/IabHelper$2;

    iget-object v0, v0, Ljp/colopl/iab/IabHelper$2;->val$listener:Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;

    iget-object v1, p0, Ljp/colopl/iab/IabHelper$2$1;->val$result_f:Ljp/colopl/iab/IabResult;

    iget-object v2, p0, Ljp/colopl/iab/IabHelper$2$1;->val$inv_f:Ljp/colopl/iab/Inventory;

    invoke-interface {v0, v1, v2}, Ljp/colopl/iab/IabHelper$QueryInventoryFinishedListener;->onQueryInventoryFinished(Ljp/colopl/iab/IabResult;Ljp/colopl/iab/Inventory;)V

    return-void
.end method
