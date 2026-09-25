.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask$1;
.super Ljava/lang/Object;
.source "SupportFormFragment.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->onPostExecute(Ljava/lang/Void;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;)V
    .locals 0

    .line 374
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask$1;->this$1:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClosed()V
    .locals 1

    .line 378
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask$1;->this$1:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/Activity;->onBackPressed()V

    return-void
.end method
