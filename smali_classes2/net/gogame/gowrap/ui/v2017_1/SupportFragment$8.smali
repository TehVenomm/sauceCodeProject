.class Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$8;
.super Ljava/lang/Object;
.source "SupportFragment.java"

# interfaces
.implements Landroid/widget/ExpandableListView$OnGroupExpandListener;


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

    .line 273
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onGroupExpand(I)V
    .locals 1

    .line 277
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$600(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 278
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$400(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)V

    .line 280
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$700(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;

    move-result-object v0

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/ui/view/AutoClosingExpandableListViewListener;->onGroupExpand(I)V

    return-void
.end method
