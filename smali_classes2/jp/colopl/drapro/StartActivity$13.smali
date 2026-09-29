.class Ljp/colopl/drapro/StartActivity$13;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/StartActivity;->showDepositDialog(Ljava/lang/String;)V
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

    .line 1081
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$13;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 2

    .line 1084
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$13;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Ljp/colopl/iab/Purchase;

    .line 1087
    new-instance v0, Landroid/os/Handler;

    invoke-direct {v0}, Landroid/os/Handler;-><init>()V

    new-instance v1, Ljp/colopl/drapro/StartActivity$13$1;

    invoke-direct {v1, p0, p2}, Ljp/colopl/drapro/StartActivity$13$1;-><init>(Ljp/colopl/drapro/StartActivity$13;Ljp/colopl/iab/Purchase;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    goto :goto_0

    .line 1093
    :cond_0
    iget-object p1, p0, Ljp/colopl/drapro/StartActivity$13;->this$0:Ljp/colopl/drapro/StartActivity;

    iget-object p1, p1, Ljp/colopl/drapro/StartActivity;->mUndepositedPurcahseList:Ljava/util/ArrayList;

    invoke-virtual {p1}, Ljava/util/ArrayList;->clear()V

    return-void
.end method
