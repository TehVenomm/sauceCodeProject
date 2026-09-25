.class Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$1;
.super Ljava/lang/Object;
.source "HelpFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;)V
    .locals 0

    .line 31
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 35
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object p1

    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;-><init>()V

    invoke-interface {p1, v0}, Lnet/gogame/gowrap/ui/UIContext;->pushFragment(Landroid/app/Fragment;)V

    return-void
.end method
