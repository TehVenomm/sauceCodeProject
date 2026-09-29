.class Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;
.super Ljava/lang/Object;
.source "SupportFragment.java"

# interfaces
.implements Landroid/view/View$OnFocusChangeListener;


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

    .line 223
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onFocusChange(Landroid/view/View;Z)V
    .locals 2

    if-eqz p2, :cond_0

    .line 228
    new-instance p2, Landroid/graphics/Rect;

    invoke-direct {p2}, Landroid/graphics/Rect;-><init>()V

    .line 229
    new-instance v0, Landroid/graphics/Rect;

    invoke-direct {v0}, Landroid/graphics/Rect;-><init>()V

    .line 230
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/ExpandableListView;

    move-result-object v1

    invoke-virtual {v1, p2}, Landroid/widget/ExpandableListView;->getGlobalVisibleRect(Landroid/graphics/Rect;)Z

    .line 231
    invoke-virtual {p1, v0}, Landroid/view/View;->getGlobalVisibleRect(Landroid/graphics/Rect;)Z

    .line 232
    iget p1, v0, Landroid/graphics/Rect;->top:I

    iget p2, p2, Landroid/graphics/Rect;->top:I

    sub-int/2addr p1, p2

    .line 233
    iget-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-static {p2}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;)Landroid/widget/ExpandableListView;

    move-result-object p2

    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7$1;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFragment$7;I)V

    invoke-virtual {p2, v0}, Landroid/widget/ExpandableListView;->post(Ljava/lang/Runnable;)Z

    :cond_0
    return-void
.end method
