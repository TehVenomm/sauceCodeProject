.class Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$2;
.super Ljava/lang/Object;
.source "SupportFragment.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V
    .locals 0

    .line 138
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    .line 142
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/EditText;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    .line 143
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$100(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;Ljava/lang/String;)V

    .line 144
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/os/Handler;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$2;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$200(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Ljava/lang/Runnable;

    move-result-object v1

    const-wide/16 v2, 0x3e8

    invoke-virtual {v0, v1, v2, v3}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    return-void
.end method
